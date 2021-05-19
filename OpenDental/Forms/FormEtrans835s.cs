﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Windows.Forms;
using OpenDental.UI;
using OpenDentBusiness;
using CodeBase;

namespace OpenDental {
	public partial class FormEtrans835s:FormODBase {
	
		///<summary>Start date used to populate _listEtranss.</summary>
		private DateTime _reportDateFrom=DateTime.MaxValue;
		///<summary>End date used to populate _listEtranss.</summary>
		private DateTime _reportDateTo=DateTime.MaxValue;
		///<summary>List of clinics user has access to.</summary>
		private List<Clinic> _listUserClinics;
		///<summary>List of every 835 Etrans in date range for etype of EtransType.ERA_835.</summary>
		private List<Etrans> _listAllEtrans=new List<Etrans>();
		///<summary>Dictionary such that they key is an etrans.EtransNum and value is a list of paid claims associated to it from the database.
		///We allow NULL in our List, this way we know that there was a claim object that can not be found and we use this in determining the status.</summary>
		private Dictionary<long,List<Hx835_ShortClaim>> _dictEtransClaims=new Dictionary<long,List<Hx835_ShortClaim>>();
		///<summary>Dictionary such that the key is a claim.ClaimNum and value is a boolean indicating if there is an existing claimPayment.</summary>
		private Dictionary<long,bool> _dictClaimPaymentsExist=new Dictionary<long,bool>();
		///<summary>Dictionary such that they key is an etrans.EtransNum and value is the 835 object.</summary>
		private Dictionary<long,X835> _dictEtrans835s=new Dictionary<long, X835>();
		///<summary>All attaches for every 835.  Used to get status of each 835.</summary>
		private List<Etrans835Attach> _listAllAttaches;
		///<summary>List of all claimProcs associated to all claims for ever 835.</summary>
		private List<Hx835_ShortClaimProc> _listAllClaimProcs;

		public FormEtrans835s() {
			InitializeComponent();
			InitializeLayoutManager();
			Lan.F(this);
		}
		
		private void FormEtrans835s_Load(object sender,EventArgs e) {
			base.SetFilterControlsAndAction((() => FilterAndFillGrid())
				,dateRangePicker,textRangeMin,textRangeMax,textControlId,textCarrier,
				textCheckTrace,comboClinics,listStatus,checkAutomatableCarriersOnly
			);
			dateRangePicker.SetDateTimeFrom(DateTime.Today.AddDays(-7));
			dateRangePicker.SetDateTimeTo(DateTime.Today);
			#region User Clinics
			if(PrefC.HasClinicsEnabled) {
				comboClinics.IsAllSelected=true;//Defaults to 'All' so that 835s with missing clinic will show.
			}
			#endregion
			#region Statuses
			foreach(X835Status status in Enum.GetValues(typeof(X835Status))) {
				if(ListTools.In(status,X835Status.None,X835Status.FinalizedSomeDetached,X835Status.FinalizedAllDetached)) {
					//FinalizedSomeDetached and FinalizedAllDetached are shown via Finalized.
					continue;
				}
				listStatus.Items.Add(Lan.g(this,status.GetDescription()));
				bool isSelected=true;
				if(status==X835Status.Finalized) {
					isSelected=false;
				}
				listStatus.SetSelected(listStatus.Items.Count-1,isSelected);
			}
			#endregion
		}

		private void FormEtrans835s_Shown(object sender,EventArgs e) {
			//This must be in Shown due to the progress bar forcing this window behind other windows.
			FilterAndFillGrid(isRefreshNeeded:true);
		}

		///<summary>Returns false when either _reportDateFrom or _reportDateTo are invalid.</summary>
		private bool ValidateFields() {
			_reportDateFrom=dateRangePicker.GetDateTimeFrom();
			_reportDateTo=dateRangePicker.GetDateTimeTo();
			if(PrefC.HasClinicsEnabled) {
				if(comboClinics.ListSelectedClinicNums.Count==0){
					comboClinics.IsAllSelected=true;//All clinics.
				}
			}
			if(_reportDateFrom==DateTime.MinValue || _reportDateTo==DateTime.MinValue) {
				return false;
			}
			return true;
		}

		///<summary>Fills grid based on values in _listEtrans.
		///Set isRefreshNeeded to true when we need to reinitialize local dictionarys after in memory list is also updated. Required true for first time running.
		///Also allows you to passed in predetermined filter options.</summary>
		private void FillGrid(bool isRefreshNeeded,List<string> listSelectedStatuses,List<long> listSelectedClinicNums,
			string carrierName,string checkTraceNum,string amountMin,string amountMax,string controlId)
		{
			Cursor=Cursors.WaitCursor;
			labelControlId.Visible=PrefC.GetBool(PrefName.EraShowControlIdFilter);
			textControlId.Visible=PrefC.GetBool(PrefName.EraShowControlIdFilter);
			if(isRefreshNeeded) {
				//Clear the local cache so we don't double the memory, or run into issues with SetFilterControlsAndAction().
				_listAllEtrans.Clear();
				_dictEtrans835s.Clear();
				_dictEtransClaims.Clear();
				List<Etrans> listEtrans=new List<Etrans>();
				if(ValidateFields()) {
					DataTable table=Etranss.RefreshHistory(_reportDateFrom,_reportDateTo,new List<EtransType>() { EtransType.ERA_835 });
					foreach(DataRow row in table.Rows) {
						Etrans etrans=new Etrans();
						etrans.EtransNum=PIn.Long(row["EtransNum"].ToString());
						etrans.ClaimNum=PIn.Long(row["ClaimNum"].ToString());
						etrans.Note=row["Note"].ToString();
						etrans.EtransMessageTextNum=PIn.Long(row["EtransMessageTextNum"].ToString());
						etrans.TranSetId835=row["TranSetId835"].ToString();
						etrans.UserNum=Security.CurUser.UserNum;
						etrans.DateTimeTrans=PIn.DateT(row["dateTimeTrans"].ToString());
						listEtrans.Add(etrans);
					}
				}
				List <Etrans835Attach> listAttached=Etrans835Attaches.GetForEtrans(listEtrans.Select(x => x.EtransNum).ToArray());
				Dictionary<long,string> dictEtransMessages=new Dictionary<long, string>();
				List<X12ClaimMatch> list835ClaimMatches=new List<X12ClaimMatch>();
				Dictionary<long,int> dictClaimMatchCount=new Dictionary<long,int>();//1:1 with _listEtranss. Stores how many claim matches each 835 has.
				Dictionary<long,List<Hx835_ShortClaim>> dictEtransNumToListClaims=new Dictionary<long,List<Hx835_ShortClaim>>();
				Dictionary<long,X835> dictEtransNumToX835s=new Dictionary<long,X835>();
				int batchQueryInterval=500;//Every 500 rows we get the next 500 message texts to save memory.
				int rowCur=0;
				UI.ProgressOD progressOD=new UI.ProgressOD();
				progressOD.ActionMain=() => {
					foreach(Etrans etrans in listEtrans) {
						if(rowCur%batchQueryInterval==0) {
							int range=Math.Min(batchQueryInterval,listEtrans.Count-rowCur);//Either the full batchQueryInterval amount or the remaining amount of etrans.
							dictEtransMessages=EtransMessageTexts.GetMessageTexts(listEtrans.GetRange(rowCur,range).Select(x => x.EtransMessageTextNum).ToList(),false);
						}
						rowCur++;
						ProgressBarEvent.Fire(ODEventType.ProgressBar,Lan.g(this,"Processing 835: ")+": "+rowCur+" out of "+listEtrans.Count);
						List <Etrans835Attach> listAttachedTo835=listAttached.FindAll(x => x.EtransNum==etrans.EtransNum);
						X835 x835=new X835(etrans,dictEtransMessages[etrans.EtransMessageTextNum],etrans.TranSetId835,listAttachedTo835,true);
						dictEtransNumToX835s.Add(etrans.EtransNum,x835);
						List<X12ClaimMatch> listClaimMatches=x835.GetClaimMatches();
						dictClaimMatchCount.Add(etrans.EtransNum,listClaimMatches.Count);
						list835ClaimMatches.AddRange(listClaimMatches);
					}
					ProgressBarEvent.Fire(ODEventType.ProgressBar,Lan.g(this,"Gathering internal claim matches."));
					List<long> listClaimNums=Claims.GetClaimFromX12(list835ClaimMatches);//Can return null.
					ProgressBarEvent.Fire(ODEventType.ProgressBar,Lan.g(this,"Building data sets."));
					int claimIndexCur=0;
					List<long> listMatchedClaimNums=new List<long>();
					foreach(Etrans etrans in listEtrans) {
							X835 x835=dictEtransNumToX835s[etrans.EtransNum];
							if(listClaimNums!=null) {
								x835.SetClaimNumsForUnattached(listClaimNums.GetRange(claimIndexCur,dictClaimMatchCount[etrans.EtransNum]));
							}
							claimIndexCur+=dictClaimMatchCount[etrans.EtransNum];
							listMatchedClaimNums.AddRange(x835.ListClaimsPaid.FindAll(x => x.ClaimNum!=0).Select(x => x.ClaimNum).ToList());
					}
					List<Hx835_ShortClaim> listClaims=Hx835_ShortClaim.GetClaimsFromClaimNums(listMatchedClaimNums.Distinct().ToList());
					//The following line includes manually detached and split attaches.
					_listAllAttaches=Etrans835Attaches.GetForEtransNumOrClaimNums(false,listEtrans.Select(x => x.EtransNum).ToList(),listMatchedClaimNums.ToArray());
					_listAllClaimProcs=Hx835_ShortClaimProc.RefreshForClaims(listMatchedClaimNums);
					foreach(Etrans etrans in listEtrans) {
						X835 x835=dictEtransNumToX835s[etrans.EtransNum];
						dictEtransNumToListClaims.Add(etrans.EtransNum,new List<Hx835_ShortClaim>());
						List <long> listSubClaimNums=x835.ListClaimsPaid.FindAll(x => x.ClaimNum!=0).Select(y => y.ClaimNum).ToList();
						List <Hx835_ShortClaim> listClaimsFor835=listClaims.FindAll(x => listSubClaimNums.Contains(x.ClaimNum));
						foreach(Hx835_Claim claim in x835.ListClaimsPaid) {
							Hx835_ShortClaim claimCur=listClaimsFor835.FirstOrDefault(x => x.ClaimNum==claim.ClaimNum);//Can be null.
							dictEtransNumToListClaims[etrans.EtransNum].Add(claimCur);
						}
					}
					GC.Collect();
				};
				progressOD.ShowDialogProgress();
				if(progressOD.IsCancelled){
					return;
				}
				_listAllEtrans=listEtrans;
				_dictEtrans835s=dictEtransNumToX835s;
				_dictEtransClaims=dictEtransNumToListClaims;
				List<long> listClaimNums=_dictEtransClaims.SelectMany(x => x.Value)
					.Where(x => x!=null)
					.Select(x => x.ClaimNum)
					.Distinct()
					.ToList();
				//Every claim num is associated to a bool. True when there is an existing claimPayment.
				_dictClaimPaymentsExist=ClaimPayments.HasClaimPayment(listClaimNums);
				_dictClaimPaymentsExist.Add(-1,false);//-1 for unmatched claims.
				_dictClaimPaymentsExist.Add(0,false);//0 for manually detached claims.
			}
			gridMain.BeginUpdate();
			#region Initilize columns
			gridMain.ListGridColumns.Clear();
			gridMain.ListGridColumns.Add(new GridColumn(Lan.g("TableEtrans835s","Patient Name"),250));
			gridMain.ListGridColumns.Add(new GridColumn(Lan.g("TableEtrans835s","Carrier Name"),190));
			gridMain.ListGridColumns.Add(new GridColumn(Lan.g("TableEtrans835s","Status"),80));
			gridMain.ListGridColumns.Add(new GridColumn(Lan.g("TableEtrans835s","Date"),80,GridSortingStrategy.DateParse));
			gridMain.ListGridColumns.Add(new GridColumn(Lan.g("TableEtrans835s","Amount"),80,GridSortingStrategy.AmountParse));
			if(PrefC.HasClinicsEnabled) {
				gridMain.ListGridColumns.Add(new GridColumn(Lan.g("TableEtrans835s","Clinic"),70));
			}
			gridMain.ListGridColumns.Add(new GridColumn(Lan.g("TableEtrans835s","Code"),37,HorizontalAlignment.Center));
			if(PrefC.GetBool(PrefName.EraShowControlIdFilter)) {
				gridMain.ListGridColumns.Add(new GridColumn(Lan.g("TableEtrans835s","ControlID"),70){ IsWidthDynamic=true });
			}
			gridMain.ListGridColumns.Add(new GridColumn(Lan.g("TableEtrans835s","Note"),250){ IsWidthDynamic=true,DynamicWeight=2 });
			#endregion
			gridMain.ListGridRows.Clear();
			List<Etrans> listEtransFiltered=new List<Etrans>();
			List<X835Status> listEtransStatuses=new List<X835Status>();
			foreach(Etrans etrans in _listAllEtrans) {
				X835 x835=_dictEtrans835s[etrans.EtransNum];
				#region Filter: Carrier Name
				if(carrierName!="" && !x835.PayerName.ToLower().Contains(carrierName.ToLower().Trim())) {
					continue;
				}
				#endregion
				X835Status stat=GetStringStatus(etrans.EtransNum);
				string status=Lan.g(this,stat.GetDescription());
				#region Filter: Status
				if(!listSelectedStatuses.Contains(status.Replace("*",""))) {//The filter will ignore finalized with detached claims.
					continue;
				}
				#endregion
				//List of ClinicNums for the current etrans.ListClaimsPaid from the DB.
				List<long> listClinicNums=_dictEtransClaims[etrans.EtransNum].Select(x => x==null? 0 :x.ClinicNum).Distinct().ToList();
				#region Filter: Clinics
				if(PrefC.HasClinicsEnabled && !listClinicNums.Exists(x => listSelectedClinicNums.Contains(x))) {
					continue;//The ClinicNums associated to the 835 do not match any of the selected ClinicNums, so nothing to show in this 835.
				}
				#endregion
				#region Filter: Check and Trace Value
				if(checkTraceNum!="" && !x835.TransRefNum.ToLower().Contains(checkTraceNum.ToLower().Trim())) {//Trace Number does not match
					continue;
				}
				#endregion
				#region Filter: Insurance Check Range Min and Max
				if(amountMin!="" && x835.InsPaid < PIn.Decimal(amountMin) || amountMax!="" && x835.InsPaid > PIn.Decimal(amountMax)) {
					continue;//Either the InsPaid is below or above our range.
				}
				#endregion
				#region Filter: ControlID
				if(controlId!="" && !x835.ControlId.ToLower().Contains(controlId.ToLower())) {
					continue;
				}
				#endregion
				listEtransFiltered.Add(etrans);
				listEtransStatuses.Add(stat);
			}
			if(checkAutomatableCarriersOnly.Checked) {
				FilterAutomatableERAs(listEtransFiltered,listEtransStatuses);
			}
			for(int i=0;i<listEtransFiltered.Count;i++) {
				X835 x835=_dictEtrans835s[listEtransFiltered[i].EtransNum];
				string status=Lan.g(this,listEtransStatuses[i].GetDescription());
				List<long> listClinicNums=_dictEtransClaims[listEtransFiltered[i].EtransNum]
					.Select(x => x==null? 0 :x.ClinicNum)
					.Distinct()
					.ToList();
				GridRow row=new GridRow();
				#region Column: Patient Name
				List<string> listPatNames=x835.ListClaimsPaid.Select(x => x.PatientName.ToString()).Distinct().ToList();
				string patName=(listPatNames.Count>0 ? listPatNames[0] : "");
				if(listPatNames.Count>1) {
					patName="("+POut.Long(listPatNames.Count)+")";
				}
				row.Cells.Add(patName);
				#endregion
				row.Cells.Add(x835.PayerName);
				row.Cells.Add(status);//See GetStringStatus(...) for possible values.
				row.Cells.Add(POut.Date(listEtransFiltered[i].DateTimeTrans));
				row.Cells.Add(POut.Decimal(x835.InsPaid));
				#region Column: Clinic
				if(PrefC.HasClinicsEnabled) {
					string clinicAbbr="";
					if(listClinicNums.Count==1) {
						if(listClinicNums[0]==0) {
							clinicAbbr=Lan.g(this,"Unassigned");
						}
						else {
							clinicAbbr=Clinics.GetAbbr(listClinicNums[0]);
						}
					}
					else if(listClinicNums.Count>1) {
						clinicAbbr="("+Lan.g(this,"Multiple")+")";
					}
					row.Cells.Add(clinicAbbr);
				}
				#endregion
				row.Cells.Add(x835._paymentMethodCode);
				if(PrefC.GetBool(PrefName.EraShowControlIdFilter)) {
					row.Cells.Add(x835.ControlId);
				}
				row.Cells.Add(listEtransFiltered[i].Note);
				row.Tag=new EraRow(listEtransFiltered[i],listEtransStatuses[i]);
				gridMain.ListGridRows.Add(row);
			}
			gridMain.EndUpdate();
			Cursor=Cursors.Default;
		}

		///<summary>Returns a list of Etrans that are for carriers that allow ERA automation. Removes the status from the list of statuses for 
		///any Etrans that is not added to the list that is returned.</summary>
		private void FilterAutomatableERAs(List<Etrans> listEtrans,List<X835Status> listEtransStatuses) {
			List<InsPlan> listInsPlansForAllClaims=GetInsPlansForClaimsHelper();
			for(int i=listEtrans.Count-1;i>=0;i--) {
				List<Hx835_ShortClaim> listClaims=_dictEtransClaims[listEtrans[i].EtransNum].FindAll(x => x!=null);
				List<Carrier> listCarriersForClaims=GetCarriersForClaimsHelper(listClaims,listInsPlansForAllClaims);
				bool isEtransAutomatable=IsEtransAutomatable(listCarriersForClaims,listEtrans[i]);
				if(isEtransAutomatable) {
					continue;//Keep the etrans and its status in the lists.
				}
				//Otherwise, remove the etrans and its status from the lists if it cannot be automated.
				listEtrans.RemoveAt(i);
				listEtransStatuses.RemoveAt(i);
			}
		}

		///<summary>Returns a list of all of the InsPlans for the Claims attached to the Etrans.</summary>
		private List<InsPlan> GetInsPlansForClaimsHelper() {
			List<long> listInsPlanNumsForAllClaims=_dictEtransClaims//Get PlanNums for all claims attached to the etrans
				.SelectMany(x => x.Value)
				.Where(x => x!=null)
				.Select(x => x.PlanNum)
				.ToList();
			return InsPlans.GetPlans(listInsPlanNumsForAllClaims);
		}

		///<summary>If listClaims is empty, returns an empty list without visiting the DB. Otherwise, gets InsPlans for claims from DB, then returns a list of Carriers for the Insplans.</summary>
		private List<Carrier> GetCarriersForClaimsHelper(List<Hx835_ShortClaim> listClaims,List<InsPlan> listInsPlans) {
			if(listClaims.Count==0) {
				return new List<Carrier>();
			}
			List<long> listPlanNumsForClaims=listClaims
				.Select(x => x.PlanNum)
				.Distinct()
				.ToList();
			List<InsPlan> listInsPlansForClaims=listInsPlans.FindAll(x => ListTools.In(x.PlanNum,listPlanNumsForClaims));
			List<long> listCarrierNumsForClaims=listInsPlansForClaims
				.Select(x => x.CarrierNum)
				.Distinct()
				.ToList();
			return Carriers.GetCarriers(listCarrierNumsForClaims);
		}

		///<summary>Uses the attached claims, carriers with a matching name, or the global EraAutomationBehavior pref to determine if an ERA is automatable.</summary>
		private bool IsEtransAutomatable(List<Carrier> listCarriersForClaims,Etrans etrans) {
			if(listCarriersForClaims.Count>0) {
				//The ERA is automatable if it has any claims attached to it that are for a Carrier that allows automation.
				//The ERA is not automatable if it has one or more claims attached and all Carriers for all attached claims don't allow automation.
				return listCarriersForClaims.Any(x => x.GetEraAutomationMode()!=EraAutomationMode.ReviewAll);
			}
			//If there are no attached claims, we see if the Carrier name from the ERA matches one or more Carriers in the DB.
			string payerName=_dictEtrans835s[etrans.EtransNum].PayerName;
			List<Carrier> listCarrierNameMatches=Carriers.GetExactNames(payerName);
			if(listCarrierNameMatches.Count==0) {
				//If no claims are attached and no carriers have a matching name, we use the global preference for ERA automation.
				return PrefC.GetEnum<EraAutomationMode>(PrefName.EraAutomationBehavior)!=EraAutomationMode.ReviewAll;
			}
			//If we have no attached claims and one or more names match, we allow automation if any name-matched carriers allow it.
			return listCarrierNameMatches.Any(x => x.GetEraAutomationMode()!=EraAutomationMode.ReviewAll);
		}

		private X835Status GetStringStatus(long etransNum) {
			List<Hx835_ShortClaim> listValidClaims=_dictEtransClaims[etransNum].FindAll(x => x!=null);
			//Either description tag or enum.ToString().
			return _dictEtrans835s[etransNum].GetStatus(listValidClaims,_listAllClaimProcs,_listAllAttaches,_dictClaimPaymentsExist);
		}

		///<summary>Called when we need to filter the current in memory contents in _listEtrans. Calls FillGrid()</summary>
		private void FilterAndFillGrid(bool isRefreshNeeded=false) {
			List<string> listSelectedStatuses=new List<string>();
			for(int i=0;i<listStatus.SelectedIndices.Count;i++) {
				listSelectedStatuses.Add(listStatus.Items.GetTextShowingAt(listStatus.SelectedIndices[i]));
			}
			List<long> listClinicNums=null;//A null signifies that clinics are disabled.
			if(PrefC.HasClinicsEnabled) {
				listClinicNums=comboClinics.ListSelectedClinicNums;
			}
			FillGrid(
				isRefreshNeeded:				isRefreshNeeded,
				listSelectedStatuses:		listSelectedStatuses,
				listSelectedClinicNums:	listClinicNums,
				carrierName:						textCarrier.Text,
				checkTraceNum:					textCheckTrace.Text,
				amountMin:							textRangeMin.Text,
				amountMax:							textRangeMax.Text,
				controlId:							textControlId.Text
			);
		}

		private void butRefresh_Click(object sender,EventArgs e) {
			FilterAndFillGrid(isRefreshNeeded:true);
		}

		private void gridMain_DoubleClick(object sender,EventArgs e) {
			int index=gridMain.GetSelectedIndex();
			if(index==-1) {//Clicked in empty space. 
				return;
			}
			//Mimics FormClaimsSend.gridHistory_CellDoubleClick(...)
			Cursor=Cursors.WaitCursor;
			Etrans et=((EraRow)gridMain.ListGridRows[index].Tag).Tran;
			//Sadly this is needed due to FormEtrans835Edit calling Etranss.Update .
			//See Etranss.RefreshHistory(...), this query does not select all etrans columns.
			//Mimics FormClaimsSend.gridHistory_CellDoubleClick(...)
			et=Etranss.GetEtrans(et.EtransNum);
			if(et==null) {
				Cursor=Cursors.Default;
				MsgBox.Show(this,"ERA could not be found, it was most likely deleted.");
				FilterAndFillGrid(isRefreshNeeded:true);
				return;
			}
			EtransL.ViewFormForEra(et,this);
			Cursor=Cursors.Default;
		}

		private void butClose_Click(object sender,EventArgs e) {
			DialogResult=DialogResult.OK;
			Close();
		}

		private class EraRow {
			public Etrans Tran;
			public X835Status Status;

			public EraRow(Etrans tran,X835Status status) {
				Tran=tran;
				Status=status;
			}

		}

	}

}
