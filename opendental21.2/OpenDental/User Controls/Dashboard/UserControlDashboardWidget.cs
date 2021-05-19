﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using OpenDentBusiness;
using CodeBase;
using OpenDental.UI;
using SparksToothChart;
using System.Drawing.Drawing2D;

namespace OpenDental {
	public partial class UserControlDashboardWidget:UserControl {
		private SheetDef _sheetDefWidget;
		private Sheet _sheetWidget;
		private Patient _pat;
		private List<SheetField> _listFieldsDrawnToGraphics=new List<SheetField>();
		///<summary>Event is fired when the 'Close' context menu item is clicked.</summary>
		public event WidgetClosedHandler WidgetClosed=null;
		///<summary>Event is fired when the 'Refresh' context menu item is clicked.</summary>
		public event RefreshClickedHandler RefreshClicked=null;

		public SheetDef SheetDefWidget {
			get {
				return _sheetDefWidget;
			}
			set {
				_sheetDefWidget=value;
				Invalidate();
			}
		}

		public UserControlDashboardWidget(SheetDef sheetDefWidget) {
			InitializeComponent();
			_sheetDefWidget=sheetDefWidget;
			Size=new Size(_sheetDefWidget.Width,_sheetDefWidget.Height);
			Name=POut.Long(_sheetDefWidget.SheetDefNum);
			ContextMenuStrip=contextMenu;
		}

		public bool Initialize() {
			if(!TryRefreshData()) {
				return false;
			}
			if(IsHandleCreated) {
				RefreshView();
			}
			return true;
		}

		public bool TrySetData(PatientDashboardDataEventArgs data) {
			if(_sheetDefWidget==null || data==null) {//Hasn't been through initialize, or unexpected data.
				return false;
			}
			if(data.StaticTextData!=null) {//Only change static text fields if the required data is available.
				SetStaticFields(data.Pat,data.Fam,data.StaticTextData);
			}
			SetOrRefreshData((ctr,sheetField) => ctr.SetData(data,sheetField));
			return true;
		}

		public bool TryRefreshData() {
			if(_sheetDefWidget==null) {
				return false;
			}
			RefreshPatient();//Patient is used in RefreshDefAndFields.
			if(!RefreshDefAndFields()) {
				return false;
			}
			SetOrRefreshData((ctr,sheetField) => ctr.RefreshData(_pat,sheetField));
			return true;
		}

		private void SetOrRefreshData(Action<IDashWidgetField,SheetField> actionSetOrRefresh) {
			foreach(Control ctr in UIHelper.GetAllControls(this)) {
				SheetField sheetField=_sheetWidget.SheetFields.FirstOrDefault(x => GetSheetFieldID(x)==ctr.Name);
				if(ctr is IDashWidgetField) {
					actionSetOrRefresh((IDashWidgetField)ctr,sheetField);
				}
			}
		}

		private void RefreshPatient() {
			_pat=Patients.GetPat(FormOpenDental.CurPatNum);
		}

		private bool RefreshDefAndFields() {
			try {
				_sheetDefWidget=SheetDefs.GetSheetDef(_sheetDefWidget.SheetDefNum);
			}
			catch(Exception ex) {
				ex.DoNothing();
				this.InvokeIfRequired(() => CloseWidget());
				return false;
			}
			SetStaticFields(_pat);
			return true;
		}

		private void SetStaticFields(Patient pat,Family fam=null,StaticTextData staticTextData=null) {
			_sheetWidget=SheetUtil.CreateSheet(_sheetDefWidget,pat?.PatNum??0);
			SheetFiller.FillFields(_sheetWidget,pat:pat,fam:fam,staticTextData:staticTextData);
		}

		public void RefreshView() {
			this.InvokeIfRequired(() => {
				RefreshDimensions();
				_listFieldsDrawnToGraphics.Clear();
				foreach(SheetField sheetField in _sheetWidget.SheetFields) {
					ODException.SwallowAnyException(() => RefreshSheetField(sheetField));
				}
				CleanupDisplay();
				if(!_listFieldsDrawnToGraphics.IsNullOrEmpty()) {
					Invalidate();
				}
			});
		}

		private void RefreshDimensions() {
			if(IsWidgetClosed()) {
				//Since we BeginInvoke back to the MainThread, it's possible to have queued up this method, and then Closed the Widget,which sets 
				//_sheetDefWidget=null, before this method executes.
				return;
			}
			Size=new Size(_sheetDefWidget.Width,_sheetDefWidget.Height);
		}

		///<summary>Handles how each type of SheetField should be drawn to the control.</summary>
		private void RefreshSheetField(SheetField field) {
			if(IsWidgetClosed()) {
				//Since we BeginInvoke back to the MainThread, it's possible to have queued up this method, and then Closed the Widget,which sets 
				//_sheetDefWidget=null, before this method executes.
				return;
			}
			if(IsSheetFieldDrawnDirectlyToGraphics(field)) {
				_listFieldsDrawnToGraphics.Add(field);
				return;
			}
			Type type=GetControlTypeForDisplay(field);
			Control ctr=UIHelper.GetAllControls(this).FirstOrDefault(x => x.Name==GetSheetFieldID(field) && x.GetType()==type);
			if(ctr==null) {
				ctr=CreateControl(field,type);
			}
			ctr.Location=new Point(field.XPos,field.YPos);
			ctr.Text=field.FieldValue;
			ctr.ForeColor=field.ItemColor;
			ctr.Size=new Size(field.Width,field.Height);
			if(ctr is IDashWidgetField) {
				((IDashWidgetField)ctr).RefreshView();
			}
			ctr.Visible=true;
		}

		protected override void OnPaint(PaintEventArgs e) {
			base.OnPaint(e);
			//Shapes need to be drawn directly to the hosting UserContrlDashboardWidget to support overlapping control transparency.
			foreach(SheetField field in _listFieldsDrawnToGraphics) {
				using(Pen pen=new Pen(field.ItemColor)) {
					switch(field.FieldType) {
						case SheetFieldType.Line:
							Point p1=new Point(field.XPos,field.YPos);
							Point p2=new Point(field.XPos+field.Width,field.YPos+field.Height);
							e.Graphics.DrawLine(pen,p1,p2);
							break;
						case SheetFieldType.Rectangle:
							e.Graphics.DrawRectangle(pen,field.XPos,field.YPos,field.Width,field.Height);
							break;
						default:
							continue;
					}
				}
			}
		}

		///<summary>Determines if the SheetFieldDef is supported in a Patient Dashboard.</summary>
		public static bool IsSheetFieldDefSupported(SheetFieldDef sheetFieldDef) {
			SheetField field=new SheetField() {
				FieldName=sheetFieldDef.FieldName,
				FieldType=sheetFieldDef.FieldType,
			};
			if(IsSheetFieldDrawnDirectlyToGraphics(field)) {
				return true;
			}
			try {
				GetControlTypeForDisplay(field);
				return true;
			}
			catch {
				return false;
			}
		}
		
		///<summary>Determines which Type of Control will be used in the UserControlDashboardWidget for various SheetField.FieldTypes</summary>
		private static Type GetControlTypeForDisplay(SheetField field) {
			Type type=typeof(Control);
			switch(field.FieldType) {
				case SheetFieldType.StaticText:
					type=typeof(DashLabel);
					break;
				case SheetFieldType.PatImage:
					type=typeof(DashPatPicture);
					break;
				case SheetFieldType.Grid:
					type=typeof(DashApptGrid);
					break;
				case SheetFieldType.Special:
					if(field.FieldName=="toothChart") {
						type=typeof(DashToothChart);
					}
					else if(field.FieldName=="toothChartLegend") {
						type=typeof(DashToothChartLegend);
					}
					else if(field.FieldName=="individualInsurance") {
						type=typeof(DashIndividualInsurance);
					}
					else if(field.FieldName=="familyInsurance") {
						type=typeof(DashFamilyInsurance);
					}
					break;
				default:
					throw new NotImplementedException("FieldType: "+field.FieldType.GetDescription()+" has not been implemented for Dashboards.");
			}
			return type;
		}

		///<summary>Determines if a SheetField will be drawn directly to the UserControlDashboardWidget's graphics, rather than creating its own control.
		///</summary>
		private static bool IsSheetFieldDrawnDirectlyToGraphics(SheetField field) {
			switch(field.FieldType) {
				case SheetFieldType.Line:
				case SheetFieldType.Rectangle:
					return true;
				default:
					return false;
			}
		}

		///<summary>Removes any controls that are no longer included in the SheetDef, i.e. SheetFieldDef was removed in between refreshes.</summary>
		private void CleanupDisplay() {
			if(IsWidgetClosed()) {
				return;
			}
			for(int i=Controls.Count-1;i>=0;i--) {
				Control ctr=Controls[i];
				if(!ListTools.In(ctr.Name,_sheetWidget.SheetFields.Select(x => GetSheetFieldID(x)))) {
					CloseControl(ctr);//Old sheetfield/def that was removed from the SheetDef in between refreshes.
					continue;
				}
			}
		}

		private void CloseControl(Control ctr) {
			Controls.Remove(ctr);
			if((ctr as PictureBox)!=null) {
				((PictureBox)ctr).Image?.Dispose();
				((PictureBox)ctr).Image=null;
			}
			if((ctr as ODPictureBox)!=null) {
				((ODPictureBox)ctr).Image?.Dispose();
				((ODPictureBox)ctr).Image=null;
			}
			ctr.Dispose();
		}

		///<summary>Gets a unique id for the field, since the field has not been inserted into the database and therefore does not have a PK.</summary>
		private string GetSheetFieldID(SheetField field) {
			//Since field has not been inserted into the db, field.SheetFieldNum=0.  It's possible to have two fields with the same FieldName.
			//Use NameTypeXPosYPos as a likely unique id.
			return (field.FieldName+field.FieldType.GetDescription()+field.XPos+field.YPos+field.Width+field.Height);
		}

		///<summary>Creates a new Control of Type type, setting its Name as a unique identifier corresponding to properties in field.
		///Hooks up appropriate events so that drag/drop on the control causes the UserControlDashboardWidget to be dragged/dropped.</summary>
		private Control CreateControl(SheetField field, Type type) {
			Control ctr=(Control)Activator.CreateInstance(type);
			//Since field has not been inserted into the db, field.SheetFieldNum=0.  It's possible to have two fields with the same FieldName.
			//Use NameXPosYPos as a likely unique id.
			ctr.Name=GetSheetFieldID(field);
			Lan.C(this,new Control[] { ctr });
			Controls.Add(ctr);
			if(ctr is IDashWidgetField) {
				((IDashWidgetField)ctr).RefreshData(_pat,field);
			}
			return ctr;
		}
		
		private void closeToolStripMenuItem_Click(object sender,EventArgs e) {
			CloseWidget();
		}

		
		private void MenuItemRefresh_Click(object sender,EventArgs e) {
			if(RefreshClicked!=null) {
				//Try to invoke the Refresh event first.  This will allow the UserControlDashboard to use its dedicated refresh thread, which will only start
				//if it is not already in the process of running it's own refresh.
				RefreshClicked.Invoke(this,e);
				return;
			}
		}

		private bool IsWidgetClosed() {
			if(_sheetDefWidget==null && _sheetWidget==null) {
				return true;
			}
			return false;
		}

		public void CloseWidget() {
			foreach(Control ctr in UIHelper.GetAllControls(this)) {
				CloseControl(ctr);
			}
			WidgetClosed?.Invoke(this,new EventArgs());
			_sheetDefWidget=null;
			_sheetWidget=null;
		}

		public delegate void WidgetClosedHandler(UserControlDashboardWidget sender,EventArgs e);
		public delegate void ApptEditHandler(List<ApptOther> listApptOther);
		public delegate void RefreshClickedHandler(UserControlDashboardWidget sender,EventArgs e);
	}
}
