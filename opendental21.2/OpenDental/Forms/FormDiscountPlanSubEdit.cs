using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using OpenDentBusiness;

namespace OpenDental {
	public partial class FormDiscountPlanSubEdit:FormODBase {
		public DiscountPlan DiscountPlanCur;
		public long PatNum;
		///<summary>The discount sub being edited. Will be null when dropped.</summary>
		public DiscountPlanSub DiscountPlanSubCur;

		public FormDiscountPlanSubEdit() {
			InitializeComponent();
			InitializeLayoutManager();
			Lan.F(this);
		}

		private void FormDiscountPlanSubEdit_Load(object sender,EventArgs e) {
			if(DiscountPlanSubCur!=null) {
				DiscountPlanCur=DiscountPlans.GetPlan(DiscountPlanSubCur.DiscountPlanNum);
				PatNum=DiscountPlanSubCur.PatNum;
				FillForm();
				return;
			}
			//New DiscountPlanSub.
			DiscountPlanSubCur=new DiscountPlanSub();
			DiscountPlanSubCur.IsNew=true;
			DiscountPlanSubCur.DiscountPlanNum=DiscountPlanCur.DiscountPlanNum;
			DiscountPlanSubCur.PatNum=PatNum;
			FillForm();
		}

		private void FillForm() {
			if(DiscountPlanCur!=null) {
				textDescript.Text=DiscountPlanCur.Description;
				textFeeSched.Text=FeeScheds.GetFirstOrDefault(x => x.FeeSchedNum==DiscountPlanCur.FeeSchedNum,true)?.Description??"";
				textAdjustmentType.Text=Defs.GetDef(DefCat.AdjTypes,DiscountPlanCur.DefNum).ItemName;
				textPlanNote.Text=DiscountPlanCur.PlanNote;
				//FormDiscountPlanEdit.cs also uses InsPlanEdit permission for access, DiscountPlanEdit permission is marked as audit trail only.
				if(!Security.IsAuthorized(Permissions.InsPlanEdit,true)) {
					textPlanNote.Enabled=false;
				}
				textPlanNum.Text=DiscountPlanCur.DiscountPlanNum.ToString();
			}
			if(DiscountPlanSubCur==null) {
				return;
			}
			//Existing DiscountPlanSubCur.
			textName.Text=Patients.GetLim(DiscountPlanSubCur.PatNum).GetNameLF();
			if(DiscountPlanSubCur.DateEffective.Year < 1880) {
				textDateEffective.Text="";
			}
			else {
				textDateEffective.Text=DiscountPlanSubCur.DateEffective.ToShortDateString();
			}
			if(DiscountPlanSubCur.DateTerm.Year< 1880) {
				textDateTerm.Text="";
			}
			else {
				textDateTerm.Text=DiscountPlanSubCur.DateTerm.ToShortDateString();
			}
			textSubNote.Text=DiscountPlanSubCur.SubNote;
		}

		private void butDiscountPlans_Click(object sender,EventArgs e) {
			using FormDiscountPlans formDiscountPlans=new FormDiscountPlans();
			formDiscountPlans.DiscountPlanSelected=DiscountPlanCur;
			formDiscountPlans.IsSelectionMode=true;
			if(formDiscountPlans.ShowDialog()==DialogResult.OK) {
				DiscountPlanCur=formDiscountPlans.DiscountPlanSelected;
				FillForm();
			}
		}

		private void butDrop_Click(object sender,EventArgs e) {
			if(DiscountPlanSubCur.IsNew) {
				DialogResult=DialogResult.Cancel;
				return;
			}
			if(!MsgBox.Show(MsgBoxButtons.OKCancel,"Drop Discount Plan?")) {
				return;
			}
			DiscountPlanSubs.UpdateAssociatedDiscountPlanAmts(new List<DiscountPlanSub>{ DiscountPlanSubCur },true);
			DiscountPlanSubs.Delete(DiscountPlanSubCur.DiscountSubNum);
			string logText=Lan.g(this,"The discount plan")+" "+DiscountPlanCur.Description+" "+Lan.g(this,"was dropped.");
			SecurityLogs.MakeLogEntry(Permissions.DiscountPlanAddDrop,DiscountPlanSubCur.PatNum,logText);
			DiscountPlanSubCur=null;
			DialogResult=DialogResult.OK;
		}

		private void butOK_Click(object sender,EventArgs e) {
			if(!textDateEffective.IsValid() || !textDateTerm.IsValid()) {
				MsgBox.Show(this,"Please fix data entry errors first.");
				return;
			}
			DiscountPlanSubCur.DateEffective=PIn.Date(textDateEffective.Text);
			DiscountPlanSubCur.DateTerm=PIn.Date(textDateTerm.Text);
			DiscountPlanSubCur.DiscountPlanNum=DiscountPlanCur.DiscountPlanNum;
			DiscountPlanSubCur.SubNote=textSubNote.Text;
			if(DiscountPlanSubs.HasDiscountPlan(PatNum)) {
				DiscountPlanSubs.Update(DiscountPlanSubCur);
			}
			else {
				DiscountPlanSubs.Insert(DiscountPlanSubCur);
				string logText=Lan.g(this,"The discount plan")+" "+DiscountPlanCur.Description+" "+Lan.g(this,"was added.");
				SecurityLogs.MakeLogEntry(Permissions.DiscountPlanAddDrop,DiscountPlanSubCur.PatNum,logText);
			}
			DiscountPlanSubs.UpdateAssociatedDiscountPlanAmts(new List<DiscountPlanSub>{ DiscountPlanSubCur });
			if(DiscountPlanCur!=null && DiscountPlanCur.PlanNote!=textPlanNote.Text) {
				string logText=Lan.g(this, "Discount plan: ")+DiscountPlanCur.Description + Lan.g(this," plan note changed from \"") + DiscountPlanCur.PlanNote + Lan.g(this,"\" to \"") + textPlanNote.Text +"\"";
				DiscountPlanCur.PlanNote=textPlanNote.Text;
				DiscountPlans.Update(DiscountPlanCur);
				SecurityLogs.MakeLogEntry(Permissions.DiscountPlanEdit,DiscountPlanSubCur.PatNum,logText);
			}
			DialogResult=DialogResult.OK;
		}

		private void butCancel_Click(object sender,EventArgs e) {
			DialogResult=DialogResult.Cancel;
		}
	}
}