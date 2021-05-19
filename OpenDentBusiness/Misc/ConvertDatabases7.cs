using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using CodeBase;
using static OpenDentBusiness.LargeTableHelper;//so you don't have to type the class name each time.

namespace OpenDentBusiness {
	public partial class ConvertDatabases {
		#region Helper Methods
		private static bool IsUsingReplication() {
			string command="SELECT COUNT(*) FROM replicationserver";
			if(Db.GetCount(command)!="0") {
				return true;
			}
			command="SHOW MASTER STATUS ";
			DataTable tableReplicationMasterStatus=Db.GetTable(command);
			command="SHOW SLAVE STATUS";
			DataTable tableSlaveStatus=Db.GetTable(command);
			if(tableReplicationMasterStatus.Rows.Count > 0 || tableSlaveStatus.Rows.Count > 0) {
				return true;
			}
			//Last check Galera cluster (NADG)
			command="SHOW GLOBAL VARIABLES LIKE '%wsrep_on%' ";
			tableSlaveStatus=Db.GetTable(command);
			for(int i = 0;i<tableSlaveStatus.Rows.Count;i++) {
				DataRow row=tableSlaveStatus.Rows[i];
				if(row["wsrep_on"]!=null && PIn.String(row["wsrep_on"].ToString())=="ON") {
					command=$"SELECT COUNT(DISTINCT wcm.node_uuid) ";
					command+="FROM mysql.wsrep_cluster wc ";
					command+="INNER JOIN mysql.wsrep_cluster_members wcm ON wc.cluster_uuid=wcm.cluster_uuid ";
					int count=Db.GetInt(command);
					if(count>0) {
						return true;
					}
				}
			}
			return false;
		}
		#endregion
		private static void To20_5_1() {
			string command;
			DataTable table;
			command="DROP TABLE IF EXISTS imagingdevice";
			Db.NonQ(command);
			command=@"CREATE TABLE imagingdevice (
				ImagingDeviceNum bigint NOT NULL auto_increment PRIMARY KEY,
				Description varchar(255) NOT NULL,
				ComputerName varchar(255) NOT NULL,
				DeviceType tinyint NOT NULL,
				TwainName varchar(255) NOT NULL,
				ItemOrder int NOT NULL,
				ShowTwainUI tinyint NOT NULL
				) DEFAULT CHARSET=utf8";
			Db.NonQ(command);
			command="DROP TABLE IF EXISTS eservicelog";
			Db.NonQ(command);
			command=@"CREATE TABLE eservicelog (
				EServiceLogNum bigint NOT NULL auto_increment PRIMARY KEY,
				EServiceCode tinyint NOT NULL,
				EServiceOperation tinyint NOT NULL,
				LogDateTime datetime NOT NULL DEFAULT '0001-01-01 00:00:00',
				AptNum bigint NOT NULL,
				PatNum bigint NOT NULL,
				PayNum bigint NOT NULL,
				SheetNum bigint NOT NULL,
				INDEX(EServiceCode),
				INDEX(EServiceOperation),
				INDEX(AptNum),
				INDEX(PatNum),
				INDEX(PayNum),
				INDEX(SheetNum)
				) DEFAULT CHARSET=utf8";
			Db.NonQ(command);
			command="ALTER TABLE mountitemdef ADD RotateOnAcquire int NOT NULL";
			Db.NonQ(command);
			command="ALTER TABLE mountitem ADD RotateOnAcquire int NOT NULL";
			Db.NonQ(command);
			command="ALTER TABLE mountitemdef ADD ToothNumbers varchar(255) NOT NULL";
			Db.NonQ(command);
			command="ALTER TABLE mountitem ADD ToothNumbers varchar(255) NOT NULL";
			Db.NonQ(command);
			command="ALTER TABLE wikilistheaderwidth ADD IsHidden tinyint NOT NULL";
			Db.NonQ(command);
			command="ALTER TABLE procedurecode ADD PaintText varchar(255) NOT NULL";
			Db.NonQ(command);
			command="UPDATE procedurecode SET PaintText='W' WHERE PaintType=15";//ToothPaintingType.Text
			Db.NonQ(command);
			command="SELECT COUNT(*) FROM preference WHERE PrefName='EnterpriseExactMatchPhone'";
			//This preference might have already been added in 20.2.49.
			if(Db.GetScalar(command)=="0") {
				command="INSERT INTO preference(PrefName,ValueString) VALUES('EnterpriseExactMatchPhone','0')";
				Db.NonQ(command);
				command="INSERT INTO preference(PrefName,ValueString) VALUES('EnterpriseExactMatchPhoneNumDigits','10')";
				Db.NonQ(command);
			}
			command="INSERT INTO alertcategory (IsHQCategory,InternalName,Description) VALUES(1,'SupplementalBackups','Supplemental Backups')";
			long alertCatNum=Db.NonQ(command, true);
			command=$@"UPDATE alertcategorylink SET AlertCategoryNum={POut.Long(alertCatNum)} WHERE AlertType=23";//23=SupplementalBackups
			Db.NonQ(command);
			command="SELECT UserNum,ClinicNum FROM alertsub WHERE AlertCategoryNum=1";
			table=Db.GetTable(command);
			for(int i=0;i<table.Rows.Count;i++) {
				command=$@"INSERT INTO alertsub (UserNum,ClinicNum,AlertCategoryNum) 
					VALUES(
						{POut.Long(PIn.Long(table.Rows[i]["UserNum"].ToString()))},
						{POut.Long(PIn.Long(table.Rows[i]["ClinicNum"].ToString()))},
						{POut.Long(alertCatNum)}
					)";
				Db.NonQ(command);
			}
			command="INSERT INTO preference(PrefName,ValueString) VALUES('SameForFamilyCheckboxesUnchecked','0')";
			Db.NonQ(command);
			command="UPDATE procedurecode SET PaintType=17 WHERE ProcCode IN('D1510','D1516','D1517')";//17=SpaceMaintainer
			Db.NonQ(command);
			command="UPDATE procedurecode SET TreatArea=7 WHERE ProcCode IN('D1516','D1517')";//7=ToothRange for bilateral
			Db.NonQ(command);
			AlterTable("toothinitial","ToothInitialNum",new ColNameAndDef("DrawText","varchar(255) NOT NULL"));
			command="INSERT INTO preference(PrefName,ValueString) VALUES('IncomeTransfersTreatNegativeProductionAsIncome','1')";
			Db.NonQ(command);
			command="ALTER TABLE mount ADD ProvNum bigint NOT NULL";
			Db.NonQ(command);
			command="ALTER TABLE document ADD ProvNum bigint NOT NULL";
			Db.NonQ(command);
			command="SELECT COUNT(*) FROM preference WHERE PrefName='EnterpriseAllowRefreshWhileTyping'";
			//This preference might have already been added in 20.3.41
			if(Db.GetScalar(command)=="0") {
				command="INSERT INTO preference(PrefName,ValueString) VALUES('EnterpriseAllowRefreshWhileTyping','1')";
				Db.NonQ(command);
			}
			command="ALTER TABLE activeinstance ADD ConnectionType tinyint(4) NOT NULL";
			Db.NonQ(command);
			command="INSERT INTO preference(PrefName,ValueString) VALUES('SaveDXCSOAPAsXML','0')";
			Db.NonQ(command);		
			command="ALTER TABLE clinic ADD TimeZone varchar(75) NOT NULL";
			Db.NonQ(command);
			command="INSERT INTO preference(PrefName,ValueString) VALUES ('WebFormsDownloadAlertFrequency','3600000')";//1 hour in ms
			Db.NonQ(command);
			command="SELECT AlertCategoryNum FROM alertcategory WHERE InternalName='OdAllTypes'";
			alertCatNum=Db.GetLong(command);
			command=$@"INSERT INTO alertcategorylink (AlertCategoryNum,AlertType) VALUES({POut.Long(alertCatNum)},30)";//30=WebformsReady
			Db.NonQ(command);
			command="INSERT INTO preference(PrefName,ValueString) VALUES('AutoImportFolder','')";
			Db.NonQ(command);
			try {
				command="SELECT * FROM userod";
				table=Db.GetTable(command);
				command="SELECT ValueString FROM preference WHERE PrefName='DomainObjectGuid'";
				string domainObjectGuid=Db.GetScalar(command);
				for(int i=0;i<table.Rows.Count;i++) {
					if(!table.Rows[i]["DomainUser"].ToString().IsNullOrEmpty()) {
						string newDomainUser=domainObjectGuid+"\\"+table.Rows[i]["DomainUser"].ToString();
						command="UPDATE userod ";
						command+="SET DomainUser='"+POut.String(newDomainUser)+"' ";
						command+="WHERE UserNum="+POut.String(table.Rows[i]["UserNum"].ToString());
						Db.NonQ(command);
					}
				}
			}
			catch(Exception ex) {
				ex.DoNothing();
			}
			command="INSERT INTO preference(PrefName,ValueString) VALUES('ShowIncomeTransferManager','1')";
			Db.NonQ(command);	
			command="INSERT INTO preference(PrefName,ValueString) VALUES('ClaimPayByTotalSplitsAuto','1')";
			Db.NonQ(command);
			command="SELECT valuestring FROM preference WHERE PrefName='SheetsDefaultStatement'";
			long sheetDefNumDefault=PIn.Long(Db.GetScalar(command));
			command=$@"INSERT INTO preference(PrefName,ValueString) VALUES('SheetsDefaultReceipt','{POut.Long(sheetDefNumDefault)}')";
			Db.NonQ(command);	
			command=$@"INSERT INTO preference(PrefName,ValueString) VALUES('SheetsDefaultInvoice','{POut.Long(sheetDefNumDefault)}')";
			Db.NonQ(command);	
			command=$@"INSERT INTO preference(PrefName,ValueString) VALUES('SheetsDefaultLimited','{POut.Long(sheetDefNumDefault)}')";
			Db.NonQ(command);
			command="SELECT DISTINCT UserGroupNum FROM grouppermission";
			table=Db.GetTable(command);
			long groupNum;
			foreach(DataRow row in table.Rows) {
				groupNum=PIn.Long(row["UserGroupNum"].ToString());
				command="INSERT INTO grouppermission (UserGroupNum,PermType) "
				   +"VALUES("+POut.Long(groupNum)+",200)";//200 - FormAdded, Used for logging form creation in EClipboard.
				Db.NonQ(command);
			}
			command="INSERT INTO preference(PrefName,ValueString) VALUES('SalesTaxDefaultProvider','0')";
			Db.NonQ(command);
			command="INSERT INTO preference(PrefName,ValueString) VALUES('SalesTaxDoAutomate','0')";
			Db.NonQ(command);
			command="ALTER TABLE program ADD CustErr varchar(255) NOT NULL";
			Db.NonQ(command);
			command="DROP TABLE IF EXISTS cert";
			Db.NonQ(command);
			command=@"CREATE TABLE cert (
				CertNum bigint NOT NULL auto_increment PRIMARY KEY,
				Description varchar(255) NOT NULL,
				WikiPageLink varchar(255) NOT NULL,
				ItemOrder int NOT NULL
				) DEFAULT CHARSET=utf8";
			Db.NonQ(command);
			command="DROP TABLE IF EXISTS certemployee";
			Db.NonQ(command);
			command=@"CREATE TABLE certemployee (
				CertEmployeeNum bigint NOT NULL auto_increment PRIMARY KEY,
				DateCompleted date NOT NULL DEFAULT '0001-01-01',
				Note varchar(255) NOT NULL,
				UserNum bigint NOT NULL,
				INDEX(UserNum)
				) DEFAULT CHARSET=utf8";
			Db.NonQ(command);
			command="DROP TABLE IF EXISTS certlinkcategory";
			Db.NonQ(command);
			command=@"CREATE TABLE certlinkcategory (
				CertLinkCategoryNum bigint NOT NULL auto_increment PRIMARY KEY,
				CertNum bigint NOT NULL,
				CertCategoryNum bigint NOT NULL,
				INDEX(CertNum),
				INDEX(CertCategoryNum)
				) DEFAULT CHARSET=utf8";
			Db.NonQ(command);
			command="SELECT DISTINCT UserGroupNum FROM grouppermission";
			table=Db.GetTable(command);
			for(int i=0;i<table.Rows.Count;i++) {
				groupNum=PIn.Long(table.Rows[i]["UserGroupNum"].ToString());
				command="INSERT INTO grouppermission (UserGroupNum,PermType) "
					+"VALUES("+POut.Long(groupNum)+",201)";//201 - Used to restrict access to Image Exporting when necessary.
				Db.NonQ(command);
			}
			command="INSERT INTO preference(PrefName,ValueString) VALUES('DefaultImageImportFolder','')";
			Db.NonQ(command);
			command="SELECT DISTINCT UserGroupNum FROM grouppermission";
			table=Db.GetTable(command);
			for(int i=0;i<table.Rows.Count;i++) {
				groupNum=PIn.Long(table.Rows[i]["UserGroupNum"].ToString());
				command="INSERT INTO grouppermission (UserGroupNum,PermType) "
				+"VALUES("+POut.Long(groupNum)+",202)";//202 - Used to restrict access to Image Create.
				Db.NonQ(command);
			}
			command="DROP TABLE IF EXISTS statementprod";
			Db.NonQ(command);
			command=@"CREATE TABLE statementprod (
					StatementProdNum bigint NOT NULL auto_increment PRIMARY KEY,
					StatementNum bigint NOT NULL,
					FKey bigint NOT NULL,
					ProdType tinyint NOT NULL,
					LateChargeAdjNum bigint NOT NULL,
					INDEX(StatementNum),
					INDEX(FKey),
					INDEX(ProdType),
					INDEX(LateChargeAdjNum)
					) DEFAULT CHARSET=utf8";
			Db.NonQ(command);
			command="SELECT MAX(ItemOrder)+1 FROM definition WHERE Category=1";//1 is AdjTypes
			int order=PIn.Int(Db.GetCount(command));
			command="INSERT INTO definition (Category,ItemName,ItemOrder,ItemValue) "
				+"VALUES (1,'Late Charge',"+POut.Int(order)+",'+')";//1 is AdjTypes
			long defNum=Db.NonQ(command,true);
			command="INSERT INTO preference(PrefName,ValueString) VALUES('LateChargeAdjustmentType','"+POut.Long(defNum)+"')";
			Db.NonQ(command);
			command="INSERT INTO preference(PrefName,ValueString) VALUES('LateChargeLastRunDate','')";
			Db.NonQ(command);
			command="INSERT INTO preference(PrefName,ValueString) VALUES('LateChargeExcludeAccountNoTil','0')";
			Db.NonQ(command);
			command="INSERT INTO preference(PrefName,ValueString) VALUES('LateChargeExcludeBalancesLessThan','')";
			Db.NonQ(command);
			command="INSERT INTO preference(PrefName,ValueString) VALUES('LateChargePercent','')";
			Db.NonQ(command);
			command="INSERT INTO preference(PrefName,ValueString) VALUES('LateChargeMin','')";
			Db.NonQ(command);
			command="INSERT INTO preference(PrefName,ValueString) VALUES('LateChargeMax','')";
			Db.NonQ(command);
			command="INSERT INTO preference(PrefName,ValueString) VALUES('LateChargeDateRangeStart','')";
			Db.NonQ(command);
			command="INSERT INTO preference(PrefName,ValueString) VALUES('LateChargeDateRangeEnd','')";
			Db.NonQ(command);
			command="INSERT INTO preference(PrefName,ValueString) VALUES('LateChargeDefaultBillingTypes','')";
			Db.NonQ(command);
			command="INSERT INTO preference(PrefName,ValueString) VALUES('ShowFeatureLateCharges','0')";
			Db.NonQ(command);
			command="INSERT INTO preference(PrefName,ValueString) VALUES('RecurringChargesInactivateDeclinedCards','0')";
			Db.NonQ(command);//Unset by default (same effect as Off for now, for backwards compatibility).
			command="ALTER TABLE creditcard ADD IsRecurringActive tinyint NOT NULL";
			Db.NonQ(command);
			command="UPDATE creditcard SET IsRecurringActive=1";//Default to true for existing credit cards.
			Db.NonQ(command);
		}//End of 20_5_1() method

		private static void To20_5_2() {
			string command;
			DataTable table;
			command="DROP TABLE IF EXISTS discountplansub";
			Db.NonQ(command);
			command=@"CREATE TABLE discountplansub (
				DiscountSubNum bigint NOT NULL auto_increment PRIMARY KEY,
				DiscountPlanNum bigint NOT NULL,
				PatNum bigint NOT NULL,
				DateEffective date NOT NULL DEFAULT '0001-01-01',
				DateTerm date NOT NULL DEFAULT '0001-01-01',
				INDEX(DiscountPlanNum),
				INDEX(PatNum)
				) DEFAULT CHARSET=utf8";
			Db.NonQ(command);
			command="SELECT PatNum,DiscountPlanNum FROM patient WHERE DiscountPlanNum>0";
			table=Db.GetTable(command);
			long patNum;
			for(int i=0;i<table.Rows.Count;i++) {
				patNum=PIn.Long(table.Rows[i]["PatNum"].ToString());
				command=$@"INSERT INTO discountplansub (DiscountPlanNum,PatNum) 
					VALUES(
						{POut.Long(PIn.Long(table.Rows[i]["DiscountPlanNum"].ToString()))},
						{POut.Long(patNum)}
					)";
				Db.NonQ(command);
				//Optionally set the DiscountPlanNum to 0
				command="UPDATE patient ";
				command+="SET DiscountPlanNum=0 ";
				command+="WHERE PatNum="+POut.Long(patNum);
				Db.NonQ(command);
			}
			command="DELETE FROM grouppermission WHERE PermType=89";//Permission already existed, but not enforced. Refreshing this Permission from scratch.
			Db.NonQ(command);
			command="SELECT DISTINCT UserGroupNum FROM grouppermission";
			table=Db.GetTable(command);
			long groupNum;
			for(int i=0;i<table.Rows.Count;i++) {
				groupNum=PIn.Long(table.Rows[i]["UserGroupNum"].ToString());
				command="INSERT INTO grouppermission (UserGroupNum,PermType) "
				+"VALUES("+POut.Long(groupNum)+",89)";//89 - Used to restrict access to Image Edit.
				Db.NonQ(command);
			}
			command="ALTER TABLE programproperty ADD IsMasked tinyint NOT NULL";
			Db.NonQ(command);
			command="SELECT ProgramPropertyNum FROM programproperty WHERE PropertyDesc LIKE '%password%'";
			List<long> listPasswordPropNums=Db.GetListLong(command);
			for(int i=0;i<listPasswordPropNums.Count;i++) {
				command=$"UPDATE programproperty SET IsMasked={POut.Bool(true)} WHERE ProgramPropertyNum={POut.Long(listPasswordPropNums[i])}";
				Db.NonQ(command);
			}
			command="INSERT INTO preference(PrefName,ValueString) VALUES('ConfirmPostcardFamMessage','We would like to confirm your appointments. [FamilyApptList].')";
			Db.NonQ(command);
			command="INSERT INTO preference(PrefName,ValueString) VALUES('ConfirmEmailFamMessage','We would like to confirm your appointments. [FamilyApptList].')";
			Db.NonQ(command);
			command="INSERT INTO preference(PrefName,ValueString) VALUES('ConfirmTextFamMessage','We would like to confirm your appointments. [FamilyApptList].')";
			Db.NonQ(command);
			command="INSERT INTO preference(PrefName,ValueString) VALUES('ConfirmGroupByFamily','0')";
			Db.NonQ(command);
			command="ALTER TABLE orthochart ADD ProvNum bigint NOT NULL";
			Db.NonQ(command);
		}//End of 20_5_2() method

		private static void To20_5_3() {
			string command;
			DataTable table;
			command="INSERT INTO preference(PrefName,ValueString) VALUES('LateChargeExcludeExistingLateCharges','0')";
			Db.NonQ(command);
			command="ALTER TABLE statementprod ADD DocNum bigint NOT NULL,ADD INDEX (DocNum)";
			Db.NonQ(command);
			command="ALTER TABLE emailaddress ADD DownloadInbox tinyint NOT NULL";
			Db.NonQ(command);
		}//End of 20_5_3() method

		private static void To20_5_4() {
			string command;
			DataTable table;
			command=$"SELECT * FROM ebill WHERE ElectPassword!=''";
			table=Db.GetTable(command);
			foreach(DataRow row in table.Rows) {
				CDT.Class1.Encrypt(row["ElectPassword"].ToString(),out string encPW);
				long ebillNum=PIn.Long(row["EbillNum"].ToString());
				command=$"UPDATE ebill SET ElectPassword='{POut.String(encPW)}' WHERE EbillNum={POut.Long(ebillNum)}";
				Db.NonQ(command);
			}
			command="DROP TABLE IF EXISTS certemployee";
			Db.NonQ(command);
			command=@"CREATE TABLE certemployee (
				CertEmployeeNum bigint NOT NULL auto_increment PRIMARY KEY,
				CertNum bigint NOT NULL,
				EmployeeNum bigint NOT NULL,
				DateCompleted date NOT NULL DEFAULT '0001-01-01',
				Note varchar(255) NOT NULL,
				UserNum bigint NOT NULL,
				INDEX(UserNum),
				INDEX (CertNum),
				INDEX (EmployeeNum)
				) DEFAULT CHARSET=utf8";
			Db.NonQ(command);
			long groupNum;
			command="SELECT DISTINCT UserGroupNum FROM grouppermission";
			table=Db.GetTable(command);
			for(int i=0;i<table.Rows.Count;i++) {
				groupNum=PIn.Long(table.Rows[i]["UserGroupNum"].ToString());
				command="INSERT INTO grouppermission (UserGroupNum,PermType) "
				+"VALUES("+POut.Long(groupNum)+",203)";//203 - Permission to update Employee Certifications.
				Db.NonQ(command);
			}
			command="SELECT DISTINCT UserGroupNum FROM grouppermission";
			table=Db.GetTable(command);
			for(int i=0;i<table.Rows.Count;i++) {
				groupNum=PIn.Long(table.Rows[i]["UserGroupNum"].ToString());
				command="INSERT INTO grouppermission (UserGroupNum,PermType) "
				+"VALUES("+POut.Long(groupNum)+",204)";//204 - Permission to set up Certifications.
				Db.NonQ(command);
			}
		}//End of 20_5_4() method

		private static void To20_5_5() {
			string command;
			DataTable table;
			command="ALTER TABLE cert ADD IsHidden tinyint NOT NULL";
			Db.NonQ(command);
			if(!ColumnExists(GetCurrentDatabase(),"IsSentToQuickBooksOnline","deposit")) {
				command="ALTER TABLE deposit ADD IsSentToQuickBooksOnline tinyint NOT NULL";
				Db.NonQ(command);
			}
		}//End of 20_5_5() method

		private static void To20_5_11() {
			string command="ALTER TABLE carecreditwebresponse ADD HasLogged tinyint NOT NULL";
			Db.NonQ(command);
			//Allen says this is what we want to do.
			command=$@"SELECT COALESCE(MIN(SheetDefNum),0) FROM sheetdef WHERE SheetType={POut.Enum<SheetTypeEnum>(SheetTypeEnum.Statement)}";
			long sheetDefNumDefault=PIn.Long(Db.GetScalar(command));
			command=$@"UPDATE preference SET ValueString={POut.Long(sheetDefNumDefault)} WHERE PrefName='SheetsDefaultStatement'";
			Db.NonQ(command);	
			command=$@"UPDATE preference SET ValueString={POut.Long(sheetDefNumDefault)} WHERE PrefName='SheetsDefaultReceipt'";
			Db.NonQ(command);	
			command=$@"UPDATE preference SET ValueString={POut.Long(sheetDefNumDefault)} WHERE PrefName='SheetsDefaultInvoice'";
			Db.NonQ(command);	
			command=$@"UPDATE preference SET ValueString={POut.Long(sheetDefNumDefault)} WHERE PrefName='SheetsDefaultLimited'";
			Db.NonQ(command);
		}

		private static void To20_5_13() {
			string command;
			command="SELECT program.Path FROM program WHERE ProgName='DentalEye'";
			string programPath=Db.GetScalar(command);//only one path
			string newPath=programPath.Replace("DentalEye.exe","CmdLink.exe");//shouldn't launch from this exe anymore
			command="UPDATE program SET Path='"+POut.String(newPath)+"' WHERE ProgName='DentalEye'";
			Db.NonQ(command);
			string note="Please set the file path to open CmdLink.exe in order to send patient data to DentalEye. Ex: C:\\Program Files (x86)\\DentalEye\\CmdLink.exe.";
			command="UPDATE program SET Note='"+POut.String(note)+"' WHERE ProgName='DentalEye'";
			Db.NonQ(command);
			command="INSERT INTO preference (PrefName,ValueString) VALUES('BillingElectIncludeAdjustDescript',1)";
			Db.NonQ(command);
			command="SELECT COUNT(*) FROM preference WHERE preference.PrefName='PdfLaunchWindow';";
			if(PIn.Int(Db.GetCount(command))==0) {
				command="INSERT INTO preference (PrefName,ValueString) VALUES('PdfLaunchWindow','0');";//false by default
				Db.NonQ(command);
			}
		}//End of 20_5_13() method

		private static void To20_5_17() {
			string command;
			command="ALTER TABLE cert ADD CertCategoryNum bigint NOT NULL";
			Db.NonQ(command);
			command="UPDATE cert SET CertCategoryNum=(SELECT CertCategoryNum FROM certlinkcategory WHERE cert.CertNum=certlinkcategory.CertNum)";
			Db.NonQ(command);
			command="DROP TABLE IF EXISTS certlinkcategory";
			Db.NonQ(command);
		}//End of 20_5_17() method

		private static void To20_5_32() {
			string command;
			//alertcategorylink.AlertCategoryNum for SBs may have been set incorrectly in 20.5.1
			command="SELECT AlertCategoryNum FROM alertcategory WHERE InternalName='SupplementalBackups' AND IsHQCategory=1";
			long alertCatNum=Db.GetLong(command);
			command=$@"UPDATE alertcategorylink SET AlertCategoryNum={POut.Long(alertCatNum)} WHERE AlertType=23";//23=SupplementalBackups
			Db.NonQ(command);
			command="UPDATE alertitem SET ClinicNum=-1 WHERE Type=23 AND ClinicNum=0";//23=SupplementalBackups
			Db.NonQ(command);
		}//End of 20_5_32() method

		private static void To20_5_33() {
			string command;
			DoseSpotSelfReportedInvalidNote();
		}

		private static void To20_5_34() {
			string command;
			DataTable table;
			command="SELECT DISTINCT UserGroupNum FROM grouppermission WHERE PermType=149";//Currently has Payment Plan Edit permissions
			table=Db.GetTable(command);
			long groupNum;
			foreach(DataRow row in table.Rows) {
				groupNum=PIn.Long(row["UserGroupNum"].ToString());
				command="INSERT INTO grouppermission (UserGroupNum,PermType) "
				   +"VALUES("+POut.Long(groupNum)+",208)";//208 - Will be given Payment Plan Charge Date Edit permissions
				Db.NonQ(command);
			}
		}

		private static void To20_5_38() {
			string command;
			command="UPDATE program SET Path='"+POut.String(@"C:\Program Files\3Shape\Dental Desktop\Plugins\ThreeShape.PracticeManagementIntegration\DentalDesktopCmd.exe")+"' "
				+"WHERE Path='"+POut.String(@"C:\Program Files\3Shape\Dental Desktop\Plugins\ThreeShape.PMSIntegration\DentalDesktopCmd.exe")+"' "
				+"AND ProgName='"+POut.String("ThreeShape")+"'";
			Db.NonQ(command);
		}//End of 20_5_38() method

		private static void To20_5_41() {
			string command;
			command="SELECT program.Path FROM program WHERE ProgName='DentalEye'";
			string programPath=Db.GetScalar(command);//only one path
			if(programPath.Contains("DentalEye.exe\\CmdLink.exe")) {//this isn't a valid path, so fix it
				programPath=programPath.Replace("\\DentalEye.exe","");
				command="UPDATE program SET Path='"+POut.String(programPath)+"' WHERE ProgName='DentalEye'";
				Db.NonQ(command);
			}
		}//End of 20_5_41() method

		private static void To20_5_48() {
			string command;
			if(!IndexExists("claim","PatNum,ClaimStatus,ClaimType")) {
				command="ALTER TABLE claim ADD INDEX PatStatusType (PatNum,ClaimStatus,ClaimType)";
				List<string> listIndexNames=GetIndexNames("claim","PatNum");
				if(listIndexNames.Count>0) {
					command+=","+string.Join(",",listIndexNames.Select(x => $"DROP INDEX {x}"));
				}
				Db.NonQ(command);
			}
		}

		private static void To21_1_1() {
			string command;
			DataTable table;
			//Set the ExistingPat 2FA prefs pref to defautl vals for all clinics
			command="SELECT ClinicNum FROM clinic";
			List<long> listClinicNums=Db.GetListLong(command);
			if(listClinicNums.Count>0) {
				foreach(long clinicNum in listClinicNums) {
					command="INSERT INTO clinicpref(ClinicNum,PrefName,ValueString) VALUES("+clinicNum+",'WebSchedExistingPatDoAuthEmail','1')"; //Default to true
					Db.NonQ(command);
					command="INSERT INTO clinicpref(ClinicNum,PrefName,ValueString) VALUES("+clinicNum+",'WebSchedExistingPatDoAuthText','0')"; //Default to false
					Db.NonQ(command);
				}
			}
			command="INSERT INTO preference(PrefName,ValueString) VALUES('WebSchedExistingPatDoAuthEmail','1')"; //Default to true
			Db.NonQ(command);
			command="INSERT INTO preference(PrefName,ValueString) VALUES('WebSchedExistingPatDoAuthText','0')"; //Default to false
			Db.NonQ(command);
			command="ALTER TABLE payplan ADD DynamicPayPlanTPOption tinyint NOT NULL";//will default to 0 'None' which should be ok since 1 will get set once TP added and OK clicked.
			Db.NonQ(command);
			if(!IndexExists("smsfrommobile","ClinicNum,SmsStatus,IsHidden")) {
				command="ALTER TABLE smsfrommobile ADD INDEX ClinicStatusHidden (ClinicNum,SmsStatus,IsHidden)";
				List<string> listIndexesToDrop=GetIndexNames("smsfrommobile","ClinicNum");
				if(!listIndexesToDrop.IsNullOrEmpty()) {
					command+=","+string.Join(",",listIndexesToDrop.Select(x => "DROP INDEX "+x));
				}
				Db.NonQ(command);
			}
			command="ALTER TABLE tsitranslog CHANGE DemandType ServiceType TINYINT NOT NULL";
			Db.NonQ(command);
			command="UPDATE program SET ProgDesc='Central Data Storage from centraldatastorage.com' "
				+"WHERE ProgName='CentralDataStorage' "
				+"AND ProgDesc='Cental Data Storage from centraldatastorage.com'";
			Db.NonQ(command);
			command="ALTER TABLE discountplan ADD PlanNote text NOT NULL";
			Db.NonQ(command);
			command="ALTER TABLE discountplansub ADD SubNote text NOT NULL";
			Db.NonQ(command);
			command="INSERT INTO preference(PrefName,ValueString) VALUES('EmailHostingUseNoReply','1')";//default to true.
			Db.NonQ(command);
			command="INSERT INTO preference(PrefName,ValueString) VALUES('EmailSecureStatus','0')";//default to NotActivated or Enabled.
			Db.NonQ(command);
			command="INSERT INTO preference(PrefName,ValueString) VALUES('Ins834IsEmployerCreate','1')"; //Default to true
			Db.NonQ(command);
			command="ALTER TABLE document MODIFY COLUMN Note MEDIUMTEXT NOT NULL";
			Db.NonQ(command);
			command="ALTER TABLE apptreminderrule ADD TemplateFailureAutoReply text NOT NULL";
			Db.NonQ(command);
			command="UPDATE apptreminderrule SET TemplateFailureAutoReply='There was an error confirming your appointment with [OfficeName]."
				+" Please call [OfficePhone] to confirm.' WHERE TypeCur=1"; //1 - ConfirmationFutureDay
			Db.NonQ(command);
			command="ALTER TABLE discountplan ADD ExamFreqLimit int NOT NULL,ADD XrayFreqLimit int NOT NULL,ADD ProphyFreqLimit int NOT NULL,"
				+"ADD FluorideFreqLimit int NOT NULL,ADD PerioFreqLimit int NOT NULL,ADD LimitedExamFreqLimit int NOT NULL,ADD PAFreqLimit int NOT NULL";
			Db.NonQ(command);
			command="INSERT INTO preference(PrefName,ValueString) VALUES('DiscountPlanExamCodes','')";
			Db.NonQ(command);
			command="INSERT INTO preference(PrefName,ValueString) VALUES('DiscountPlanXrayCodes','')";
			Db.NonQ(command);
			command="INSERT INTO preference(PrefName,ValueString) VALUES('DiscountPlanProphyCodes','')";
			Db.NonQ(command);
			command="INSERT INTO preference(PrefName,ValueString) VALUES('DiscountPlanFluorideCodes','')";
			Db.NonQ(command);
			command="INSERT INTO preference(PrefName,ValueString) VALUES('DiscountPlanPerioCodes','')";
			Db.NonQ(command);
			command="INSERT INTO preference(PrefName,ValueString) VALUES('DiscountPlanLimitedCodes','')";
			Db.NonQ(command);
			command="INSERT INTO preference(PrefName,ValueString) VALUES('DiscountPlanPACodes','')";
			Db.NonQ(command);
			command="INSERT INTO preference(PrefName,ValueString) VALUES('CloudAllowedIpAddresses','')";
			Db.NonQ(command);
			command="SELECT DISTINCT UserGroupNum FROM grouppermission";
			table=Db.GetTable(command);
			long groupNum;
			for(int i=0;i<table.Rows.Count;i++) {
				 groupNum=PIn.Long(table.Rows[i]["UserGroupNum"].ToString());
				 command="INSERT INTO grouppermission (UserGroupNum,PermType) VALUES("+POut.Long(groupNum)+",206)";
				 Db.NonQ(command);
			}
			command="TRUNCATE TABLE eservicelog";
			Db.NonQ(command);
			command="ALTER TABLE eservicelog DROP COLUMN SheetNum";
			Db.NonQ(command);
			command="ALTER TABLE eservicelog DROP COLUMN PayNum";
			Db.NonQ(command);
			command="ALTER TABLE eservicelog DROP COLUMN AptNum";
			Db.NonQ(command);
			command="ALTER TABLE eservicelog DROP COLUMN EServiceOperation";
			Db.NonQ(command);
			command="ALTER TABLE eservicelog DROP COLUMN EServiceCode";
			Db.NonQ(command);
			command="ALTER TABLE eservicelog ADD EServiceType tinyint";
			Db.NonQ(command);
			command="ALTER TABLE eservicelog ADD EServiceAction smallint";
			Db.NonQ(command);
			command="ALTER TABLE eservicelog ADD KeyType smallint";
			Db.NonQ(command);
			command="ALTER TABLE eservicelog ADD LogGuid VARCHAR(16)";
			Db.NonQ(command);
			command="ALTER TABLE eservicelog ADD ClinicNum bigint";
			Db.NonQ(command);
			command="ALTER TABLE eservicelog ADD FKey bigint";
			Db.NonQ(command);
			if(!IndexExists("eservicelog","LogDateTime,ClinicNum")) {
				command="ALTER TABLE eservicelog ADD INDEX ClinicDateTime (LogDateTime,ClinicNum)";
				List<string> listIndexesToDrop=GetIndexNames("eservicelog","ClinicNum");
				if(!listIndexesToDrop.IsNullOrEmpty()) {
					command+=","+string.Join(",",listIndexesToDrop.Select(x => "DROP INDEX "+x));
				}
				Db.NonQ(command);
			}
			command="ALTER TABLE discountplan ADD AnnualMax double NOT NULL DEFAULT -1";
			Db.NonQ(command);
		}//End of 21_1_1() method

		private static void To21_1_3() {
			string command;
			DataTable table;
			//This was done way back in 16.4 but the SheetsDefaultTreatmentPlan preference did not get fully implemented.
			//This commit is to officially implement SheetsDefaultTreatmentPlan (along with clinic specific overrides for the pref).
			//Therefore, the current value in the preference table needs to be updated with the most recent SheetDefNum that would be currently used.
			//The following code mimics the behavior of SheetDefs.GetInternalOrCustom() which is being used for TP sheets at the time of this commit.
			//E.g. ListSheetDefs.OrderBy(x => x.Description).ThenBy(x => x.SheetDefNum).FirstOrDefault()
			command=@"SELECT SheetDefNum
				FROM sheetdef 
				WHERE SheetType=17
				ORDER BY Description,SheetDefNum
				LIMIT 1";
			table=Db.GetTable(command);//GetScalar won't work with this particular query because it may not return a row (no custom sheet def).
			long treatmentPlanSheetDefNum=0;
			if(table.Rows.Count > 0) {
				treatmentPlanSheetDefNum=PIn.Long(table.Rows[0]["SheetDefNum"].ToString());
			}
			command=$@"UPDATE preference SET ValueString='{POut.Long(treatmentPlanSheetDefNum)}' WHERE PrefName='SheetsDefaultTreatmentPlan'";
			Db.NonQ(command);
		}

		private static void To21_1_5() {
			string command;
			DataTable table;
			command="UPDATE alertitem SET ClinicNum=-1 WHERE Type=23 AND ClinicNum=0";//23=SupplementalBackups
			Db.NonQ(command);
		}

		private static void To21_1_6() {
			string command;
			DataTable table;
			command="INSERT INTO preference(PrefName,ValueString) VALUES('EraAutomationBehavior','1')";//1 - EraAutomationMode.ReviewAll by default.
			Db.NonQ(command);
			command="ALTER TABLE carrier ADD EraAutomationOverride tinyint NOT NULL";//0 - EraAutomationMode.UseGlobal by default.
			Db.NonQ(command);
			DoseSpotSelfReportedInvalidNote();
			command="SELECT preference.ValueString FROM preference WHERE preference.PrefName IN ('AppointmentTimeArrivedTrigger')";
			List<long> listDefNums=Db.GetListLong(command,hasExceptions:false).Where(x => x!=0).Distinct().ToList();
			string defNums=string.Join(",",listDefNums);
			command="INSERT INTO preference (PrefName,ValueString) VALUES ('ApptConfirmByodEnabled','"+defNums+"')";
			Db.NonQ(command);
			command="SELECT preference.ValueString FROM preference WHERE preference.PrefName IN ('AppointmentTimeArrivedTrigger',"
				+"'AppointmentTimeSeatedTrigger','AppointmentTimeDismissedTrigger')";
			listDefNums=Db.GetListLong(command,hasExceptions:false).Where(x => x!=0).Distinct().ToList();
			defNums=string.Join(",",listDefNums);
			command="INSERT INTO preference (PrefName,ValueString) VALUES ('ApptConfirmExcludeEclipboard','"+defNums+"')";
			Db.NonQ(command);
			command="ALTER TABLE emailmessage ADD MsgType varchar(255) NOT NULL,ADD FailReason varchar(255) NOT NULL";
			Db.NonQ(command);
			//Set all current emailmessage.MsgType with 'Legacy'
			//Alter the SecDateTEdit column to not automatically update when setting the MsgType to 'Legacy'. We want to perserve the actual last time it was modified
			command="ALTER TABLE emailmessage CHANGE SecDateTEdit SecDateTEdit TIMESTAMP NOT NULL DEFAULT CURRENT_TIMESTAMP";
			Db.NonQ(command);
			//This is how we can get it to work for both MySQL 5.5 and 5.6
			command="ALTER TABLE emailmessage ALTER COLUMN SecDateTEdit DROP DEFAULT";
			Db.NonQ(command);
			command="UPDATE emailmessage SET MsgType='Legacy'";//EmailMessageSource.Legacy
			Db.NonQ(command);
			command="ALTER TABLE emailmessage CHANGE SecDateTEdit SecDateTEdit TIMESTAMP NOT NULL DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP";
			Db.NonQ(command);
		}//End of 21_1_6() method

		private static void To21_1_9() {
			string command;
			DataTable table;
			command="SELECT DISTINCT UserGroupNum FROM grouppermission WHERE PermType=208";
			table=Db.GetTable(command);
			if(table.Rows.Count==0) {
				command="SELECT DISTINCT UserGroupNum FROM grouppermission WHERE PermType=149";//Currently has Payment Plan Edit permissions
				table=Db.GetTable(command);
				long groupNum;
				foreach(DataRow row in table.Rows) {
					groupNum=PIn.Long(row["UserGroupNum"].ToString());
					command="INSERT INTO grouppermission (UserGroupNum,PermType) "
						 +"VALUES("+POut.Long(groupNum)+",208)";//208 - Will be given Payment Plan Charge Date Edit permissions
					Db.NonQ(command);
				}
			}
		}//End of 21_1_9() method

		private static void To21_1_13() {
			string command;
			command="SELECT DISTINCT ClinicNum FROM clinicpref";
			List<long> listClinicNums=Db.GetListLong(command);
			for(int i=0;i<listClinicNums.Count;i++) {
				command=$@"SELECT ClinicPrefNum FROM clinicpref WHERE PrefName='WebSchedExistingPatDoAuthEmail' AND ClinicNum={POut.Long(listClinicNums[i])}";
				List<long> listClinicPrefNums=Db.GetListLong(command);
				if(listClinicPrefNums.Count>1) {
					//We will not delete the last one.
					for(int j=0;j<listClinicPrefNums.Count-1;j++) {
						command=@$"DELETE FROM clinicpref WHERE ClinicPrefNum={POut.Long(listClinicPrefNums[j])}";
						Db.NonQ(command);
					}
				}
				command=$@"SELECT ClinicPrefNum FROM clinicpref WHERE PrefName='WebSchedExistingPatDoAuthText' AND ClinicNum={POut.Long(listClinicNums[i])}";
				listClinicPrefNums=Db.GetListLong(command);
				if(listClinicPrefNums.Count>1) {
					//We will not delete the last one.
					for(int j=0;j<listClinicPrefNums.Count-1;j++) {
						command=@$"DELETE FROM clinicpref WHERE ClinicPrefNum={POut.Long(listClinicPrefNums[j])}";
						Db.NonQ(command);
					}
				}
			}
			command="UPDATE program SET Path='"+POut.String(@"C:\Program Files\3Shape\Dental Desktop\Plugins\ThreeShape.PracticeManagementIntegration\DentalDesktopCmd.exe")+"' "
				+"WHERE Path='"+POut.String(@"C:\Program Files\3Shape\Dental Desktop\Plugins\ThreeShape.PMSIntegration\DentalDesktopCmd.exe")+"' "
				+"AND ProgName='"+POut.String("ThreeShape")+"'";
			Db.NonQ(command);
		}//End of 21_1_13() method

		private static void To21_1_16() {
			string command;
			DataTable table;
			if(!IndexExists("payment","PayType")) {
				command="ALTER TABLE payment ADD INDEX (PayType)";//FK to definition.DefNum with category 10
				Db.NonQ(command);
			}
			command="SELECT program.Path FROM program WHERE ProgName='DentalEye'";
			string programPath=Db.GetScalar(command);//only one path
			if(programPath.Contains("DentalEye.exe\\CmdLink.exe")) {//this isn't a valid path, so fix it
				programPath=programPath.Replace("\\DentalEye.exe","");
				command="UPDATE program SET Path='"+POut.String(programPath)+"' WHERE ProgName='DentalEye'";
				Db.NonQ(command);
			}
		}//End of 21_1_16() method

		private static void To21_1_22() {
			string command;
			if(!IndexExists("claim","PatNum,ClaimStatus,ClaimType")) {
				command="ALTER TABLE claim ADD INDEX PatStatusType (PatNum,ClaimStatus,ClaimType)";
				List<string> listIndexNames=GetIndexNames("claim","PatNum");
				if(listIndexNames.Count>0) {
					command+=","+string.Join(",",listIndexNames.Select(x => $"DROP INDEX {x}"));
				}
				Db.NonQ(command);
			}
		}//End of 21_1_22() method

		private static void To21_1_28() {
			//Update existing discountplans with all frequency limitations set to 0, to have unlimited frequency limitation.
			string command=$@"UPDATE discountplan
					SET ExamFreqLimit=-1,XrayFreqLimit=-1,ProphyFreqLimit=-1,FluorideFreqLimit=-1,PerioFreqLimit=-1,LimitedExamFreqLimit=-1,PAFreqLimit=-1
					WHERE ExamFreqLimit=0 AND XrayFreqLimit=0 AND ProphyFreqLimit=0 AND FluorideFreqLimit=0 AND PerioFreqLimit=0 AND LimitedExamFreqLimit=0 AND PAFreqLimit=0";
			Db.NonQ(command);
		}//End of 21_1_28() method

		private static void To21_2_1() {
			string command;
			DataTable table;
			command="INSERT INTO preference(PrefName,ValueString) VALUES('EmailDefaultSendPlatform','Secure')";//Defaults to SecureEmail(EmailHosting)
			Db.NonQ(command);
			command="ALTER TABLE payplancharge ADD IsOffset tinyint NOT NULL";
			Db.NonQ(command);
			command="ALTER TABLE eclipboardsheetdef ADD MinAge INT NOT NULL DEFAULT -1";
			Db.NonQ(command);
			command="ALTER TABLE eclipboardsheetdef ADD MaxAge INT NOT NULL DEFAULT -1";
			Db.NonQ(command);
			command="ALTER TABLE apptview ADD WaitingRmName tinyint NOT NULL";
			Db.NonQ(command);
			//This set of code will get and encrypt third party passwords that are stored in the database as plaintext.
			//XVWeb, Appriss Client, and XCharge passwords have already been encrypted.
			//Question: Should we only be grabbing enabled programs?
			command=$"SELECT ProgramNum from program WHERE ProgName In ('{ProgramName.XVWeb.ToString()}','{ProgramName.Xcharge.ToString()}','{ProgramName.SFTP.ToString()}')";
			List<long> listProgNums=Db.GetListLong(command);
			string listStrPrognums=string.Join(",",listProgNums);
			command=$"SELECT ProgramPropertyNum,PropertyValue from programproperty WHERE IsMasked=1 " +//Find all passwords
				$"AND PropertyValue!='' "	+																															 //that have a value
				$"AND PropertyDesc!='{POut.String(PdmpProperty.ApprissAuthPassword)}' " +								 //aren't the client key password for Appriss
				$"AND ProgramNum NOT IN ({listStrPrognums})";																					   //and aren't in our list of programnums
			table=Db.GetTable(command);
			long progPropertyNum;
			string password;
			string obfuscatedPassword="";
			for(int i=0;i<table.Rows.Count;i++) {
				progPropertyNum=PIn.Long(table.Rows[i]["ProgramPropertyNum"].ToString());
				password=PIn.String(table.Rows[i]["PropertyValue"].ToString());
				try {
					if(CDT.Class1.Decrypt(password,out _) || !CDT.Class1.Encrypt(password,out obfuscatedPassword)) {
						continue;
					}
					command=$@"UPDATE programproperty SET PropertyValue='{obfuscatedPassword}' WHERE ProgramPropertyNum={POut.Long(progPropertyNum)}";
					Db.NonQ(command);
				}
				catch(Exception ex) {
					ex.DoNothing();
				}
			}
			command="DROP TABLE IF EXISTS transactioninvoice";
			Db.NonQ(command);
			command=@"CREATE TABLE transactioninvoice (
					TransactionInvoiceNum bigint NOT NULL auto_increment PRIMARY KEY,
					FileName varchar(255) NOT NULL,
					InvoiceData text NOT NULL
					) DEFAULT CHARSET=utf8";
			Db.NonQ(command);
			command="ALTER TABLE transaction ADD TransactionInvoiceNum bigint NOT NULL";
			Db.NonQ(command);
			command="ALTER TABLE transaction ADD INDEX (TransactionInvoiceNum)";
			Db.NonQ(command);
			command="ALTER TABLE eservicelog MODIFY LogGuid VARCHAR(36) NOT NULL";
			Db.NonQ(command);
			command="UPDATE program SET Path='"+POut.String(@"C:\Program Files\3Shape\Dental Desktop\Plugins\ThreeShape.PracticeManagementIntegration\DentalDesktopCmd.exe")+"' "
				+"WHERE Path='"+POut.String(@"C:\Program Files\3Shape\Dental Desktop\Plugins\ThreeShape.PMSIntegration\DentalDesktopCmd.exe")+"' "
				+"AND ProgName='"+POut.String("ThreeShape")+"'";
			Db.NonQ(command);
			command="ALTER TABLE referral ADD BusinessName varchar(255) NOT NULL";
			Db.NonQ(command);
			command="ALTER TABLE referral ADD DisplayNote varchar(4000) NOT NULL";
			Db.NonQ(command);
			command="ALTER table transactioninvoice MODIFY COLUMN InvoiceData mediumtext";
			Db.NonQ(command);
			//This set of code will encrypt HL7 passwords (each location has it's own HL7 table row, so there may be multiple).
			command=@"SELECT HL7DefNum,SftpPassword
				FROM hl7def
				WHERE SftpPassword!=''";//Don't have to set if password has no value.
			table=Db.GetTable(command);
			long hl7DefNum;
			for(int i=0;i<table.Rows.Count;i++) {
				hl7DefNum=PIn.Long(table.Rows[i]["HL7DefNum"].ToString());
				password=PIn.String(table.Rows[i]["SftpPassword"].ToString());
				try {
					if(CDT.Class1.Decrypt(password,out _) || !CDT.Class1.Encrypt(password,out obfuscatedPassword)) {
						continue;
					}
					command=$@"UPDATE hl7def SET SftpPassword='{obfuscatedPassword}' WHERE HL7DefNum={POut.Long(hl7DefNum)}";
					Db.NonQ(command);
				}
				catch(Exception ex) {
					ex.DoNothing();
				}
			}
			//This set of code will update abbreviations for 2020 and 2021 ADA codes if the user has already used D code tools.
			//A list of the codes we need to add abbreviations for.
			List<string> listStrCodes=new List<string>() {"D0419","D1551","D1552","D1553","D1556","D1557","D1558","D2753","D5284","D5286","D6082","D6083",
				"D6084","D6086","D6087","D6088","D6097","D6098","D6099","D6120","D6121","D6122","D6123","D6195","D6243","D6753","D6784","D7922","D8696","D8697",
				"D8698","D8699","D8701","D8702","D8703","D8704","D9997","D0604","D0605","D0701","D0702","D0703","D0704","D0705","D0706","D0707","D0708","D0709",
				"D1321","D1355","D2928","D3471","D3472","D3473","D3501","D3502","D3503","D5995","D5996","D6191","D6192","D7961","D7962","D7993","D7994"};
			string strCodes=string.Join(",",listStrCodes.Select(x => $"'{POut.String(x)}'"));
			List<string> listAbbrs=new List<string>() {"SLFL","RBSMMAX","RBSMMAN","REMFSMQ","REMFUSMQ","REMFSPMAX","REMFBSMAN","PFMT","RPDUFQ","RPDURQ",
				"IMBPFMB","IMPPFMN","IMPPFMT","IMPFMCB","IMPFMCN","IMPFMCT","ABUFPMT","IMPRETPFMB","IMPRETPFMN","IMPRETPFMT","IMPRETFMCB","IMPRETFMCN",
				"IMPRETFMCT","ABURETPFMT","PONPFMT","RETPFMT","3/4RETFMCT","SOCKMED","ORREPAIRMAX","ORREPAIRMAN","ORRECMAX","ORRECMAN","REFRETMAX","REFRETMAN",
				"REPLRETMAX","REPLRETMAN","CASEMANAGE","Antig","Antib","PanC","CephC","2DC","3DC","EOC","OCCC","PAC","BWXC","FMXC","ConSA","CPM","PCR","RRA",
				"RRB","RRM","SERA","SERB","SERM","PMCU","PMCL","SPABPL","SMATPL","FRENB","FRENL","CRANIMP","ZYGIMP"};
			command=$@"SELECT CodeNum,AbbrDesc,ProcCode FROM procedurecode
				WHERE ProcCode IN ({strCodes})";  
			table=Db.GetTable(command);
			long codeNum;
			string abbrDesc;
			string procCode;
			int index;
			for(int i=0;i<table.Rows.Count;i++) {
				codeNum=PIn.Long(table.Rows[i]["CodeNum"].ToString());
				abbrDesc=PIn.String(table.Rows[i]["AbbrDesc"].ToString());
				procCode=PIn.String(table.Rows[i]["ProcCode"].ToString());
				if(!abbrDesc.IsNullOrEmpty()) {//Customers can add their own abbreviations, so skip abbreviation if not blank.
					continue;
				}
				index=listStrCodes.IndexOf(procCode);//Get index of code
				if(index==-1) {
					continue;
				}
				command=$@"UPDATE procedurecode SET AbbrDesc='{POut.String(listAbbrs[index])}' WHERE CodeNum={POut.Long(codeNum)}";
				Db.NonQ(command);
			}
			command="DROP TABLE IF EXISTS referralcliniclink";
			Db.NonQ(command);
			command=@"CREATE TABLE referralcliniclink (
				ReferralClinicLinkNum bigint NOT NULL auto_increment PRIMARY KEY,
				ReferralNum bigint NOT NULL,
				ClinicNum bigint NOT NULL,
				INDEX(ReferralNum),
				INDEX(ClinicNum)
				) DEFAULT CHARSET=utf8";
			Db.NonQ(command);
			//Set the eClipboardShowPHI 2FA prefs pref to defautl vals for all clinics
			command="SELECT ClinicNum FROM clinic";
			List<long> listClinicNums=Db.GetListLong(command);
			if(listClinicNums.Count>0) {
				foreach(long clinicNum in listClinicNums) {
					command="INSERT INTO clinicpref(ClinicNum,PrefName,ValueString) VALUES("+clinicNum+",'EClipboardDoTwoFactorAuth','0')"; //Default to false
					Db.NonQ(command);
				}
			}
			command="INSERT INTO preference(PrefName,ValueString) VALUES('EClipboardDoTwoFactorAuth','0')"; //Default to false
			Db.NonQ(command);
			if(!IndexExists("fee","FeeSched,CodeNum,ClinicNum,ProvNum")) {//may have been added manually
				command="ALTER TABLE fee ADD INDEX FeeSchedCodeClinicProv (FeeSched,CodeNum,ClinicNum,ProvNum)";
				List<string> listIndexNames=GetIndexNames("fee","FeeSched");
				if(listIndexNames.Count>0) {
					command+=","+string.Join(",",listIndexNames.Select(x => $"DROP INDEX {x}"));
				}
				Db.NonQ(command);
			}
			command="DROP TABLE IF EXISTS hieclinic";
			Db.NonQ(command);
			command=@"CREATE TABLE hieclinic (
					HieClinicNum bigint NOT NULL auto_increment PRIMARY KEY,
					ClinicNum bigint NOT NULL,
					SupportedCarrierFlags tinyint NOT NULL,
					PathExportCCD varchar(255) NOT NULL,
					TimeOfDayExportCCD bigint NOT NULL,
					IsEnabled tinyint NOT NULL,
					INDEX(ClinicNum),
					INDEX(TimeOfDayExportCCD)
					) DEFAULT CHARSET=utf8";
			Db.NonQ(command);
			command="DROP TABLE IF EXISTS hiequeue";
			Db.NonQ(command);
			command=@"CREATE TABLE hiequeue (
					HieQueueNum bigint NOT NULL auto_increment PRIMARY KEY,
					PatNum bigint NOT NULL,
					INDEX(PatNum)
					) DEFAULT CHARSET=utf8";
			Db.NonQ(command);
			//Set of code to disable Dental Intel.
			command=$"SELECT ProgramNum from program WHERE ProgName='DentalIntel'";
			long progNum=Db.GetLong(command);
			command=$@"UPDATE program SET Enabled=0,IsDisabledByHQ=1 WHERE ProgramNum={POut.Long(progNum)}";
			Db.NonQ(command);//Disable the program, since this will not appear anywhere anymore.
			command=$@"UPDATE programproperty SET PropertyValue='1'
				WHERE ProgramNum={POut.Long(progNum)}
				AND PropertyDesc='Disable Advertising HQ'";//Set 'Disable Advertising HQ' affiliated to Dental Intel to true.
			Db.NonQ(command);
			//Insert RayBridge bridge (new version of SMARTDent)----------------------------------------------------------------- 
			command="INSERT INTO program (ProgName,ProgDesc,Enabled,Path,CommandLine,Note" 
				+") VALUES(" 
				+"'RayBridge', " 
				+"'SMARTDent New from www.raymedical.com', " 
				+"'0', " 
				+"'"+POut.String(@"C:\Ray\RayBridge\RayBridge.exe")+"', " 
				+"'"+POut.String(@"")+"', "//leave blank if none 
				+"'')"; 
			long programNum=Db.NonQ(command,true); 
			command="INSERT INTO programproperty (ProgramNum,PropertyDesc,PropertyValue" 
				+") VALUES(" 
				+"'"+POut.Long(programNum)+"', " 
				+"'Enter 0 to use PatientNum, or 1 to use ChartNum', "
				+"'0')"; 
			Db.NonQ(command); 
			command="INSERT INTO programproperty (ProgramNum,PropertyDesc,PropertyValue"
				+") VALUES("
				+"'"+POut.Long(programNum)+"',"
				+"'Xml output file path',"
				+"'"+POut.String(@"C:\Ray\PatientInfo.xml")+"'" 
				+")";
			Db.NonQ(command);
			command="INSERT INTO toolbutitem (ProgramNum,ToolBar,ButtonText) " 
				+"VALUES (" 
				+"'"+POut.Long(programNum)+"', " 
				+"'2', "//ToolBarsAvail.ChartModule 
				+"'SmartDent')"; 
			Db.NonQ(command); 
			//end RayBridge bridge
			command="SELECT ValueString FROM preference WHERE PrefName='PracticePhone'";
			string practicePhone=Db.GetScalar(command);
			command="INSERT INTO preference(PrefName,ValueString) VALUES('PracticeBillingPhone','"+POut.String(practicePhone)+"')";
			Db.NonQ(command);
			command="INSERT INTO preference(PrefName,ValueString) VALUES('PracticePayToPhone','"+POut.String(practicePhone)+"')";
			Db.NonQ(command);
			//Insert VisionX bridge----------------------------------------------------------------- 
			command="INSERT INTO program (ProgName,ProgDesc,Enabled,Path,CommandLine,Note"
				+") VALUES("
				+"'VisionX', "
				+"'VisionX from www.airtechniques.com', "
				+"'0', "
				+"'"+POut.String(@"C:\Program Files\Air Techniques\VisionX\Clients\VisionX.exe")+"', "
				+"'', "
				+"'"+POut.String(@"No command line or path is needed.")+"')";
			programNum=Db.NonQ(command,true);//we now have a ProgramNum to work with
			command="INSERT INTO programproperty (ProgramNum,PropertyDesc,PropertyValue"
				+") VALUES("
				+"'"+programNum.ToString()+"', "
				+"'Enter 0 to use PatientNum, or 1 to use ChartNum', "
				+"'0')";
			Db.NonQ(command);
			command="INSERT INTO programproperty (ProgramNum,PropertyDesc,PropertyValue"
				+") VALUES("
				+"'"+programNum.ToString()+"', "
				+"'Text file path', "
				+"'"+POut.String(@"C:\ProgramData\Air Techniques\VisionX\WorkstationService\Examination\DBSWINLegacySupport\patimport.txt")+"')";
			Db.NonQ(command);
			command="INSERT INTO toolbutitem (ProgramNum,ToolBar,ButtonText) "
				+"VALUES ("
				+"'"+programNum.ToString()+"', "
				+"'"+((int)ToolBarsAvail.ChartModule).ToString()+"', "
				+"'VisionX')";
			Db.NonQ(command);
			//end VisionX bridge
			command="INSERT INTO preference(PrefName,ValueString) VALUES('ADPRunIID','')";
			Db.NonQ(command);
			command="INSERT INTO preference(PrefName,ValueString) VALUES('RecurringChargesShowInactive','0')";
			Db.NonQ(command);string upgrading="Upgrading database to version: 21.2.1";
			ODEvent.Fire(ODEventType.ConvertDatabases,upgrading);//No translation in convert script.
			//New alert categorylink, Update Complete - Action Required
			command="SELECT AlertCategoryNum FROM alertcategory WHERE InternalName='OdAllTypes'";
			long alertCatNum=Db.GetLong(command);
			command=$@"INSERT INTO alertcategorylink (AlertCategoryNum,AlertType) VALUES({POut.Long(alertCatNum)},32)";//32=Update
			Db.NonQ(command);
			command="SELECT ValueString FROM preference WHERE PrefName='PatientPhoneUsePhonenumberTable'";
			string patientPhoneUsePhonenumberTable=Db.GetScalar(command);
			if(patientPhoneUsePhonenumberTable!="1") {//YN.Yes=1
				ODEvent.Fire(ODEventType.ConvertDatabases,$"{upgrading} - Synching PhoneNumber table.");//No translation in convert script.
				//The following is copy/paste and adapted from PhoneNumbers.SyncAllPats(), in case that method or PhoneNumber class changes in a future version.
				//0=Other,1=HmPhone,2=WkPhone,3=WirelessPhone
				command=$@"SELECT 0 PhoneNumberNum,PatNum,PhoneNumberVal,'' PhoneNumberDigits,PhoneType
					FROM phonenumber WHERE PhoneType=0 AND PhoneNumberVal!=''
					UNION ALL
					SELECT 0,PatNum,HmPhone,'',1 FROM patient WHERE HmPhone!=''
					UNION ALL
					SELECT 0,PatNum,WkPhone,'',2 FROM patient WHERE WkPhone!=''
					UNION ALL
					SELECT 0,PatNum,WirelessPhone,'',3 FROM patient WHERE WirelessPhone!=''";
				table=Db.GetTable(command);
				command="TRUNCATE TABLE phonenumber";
				Db.NonQ(command);
				command="";
				//Break INSERT query into appropriately sized batches, since this could be a very large list for big offices.
				string insert=$"INSERT INTO phonenumber (PatNum,PhoneNumberVal,PhoneNumberDigits,PhoneType) VALUES ";	
				string values="";
				for(int i=0;i<table.Rows.Count;i++) {
					DataRow row=table.Rows[i];
					long patNum=PIn.Long(row["PatNum"].ToString());
					int phoneType=PIn.Int(row["PhoneType"].ToString());
					string phoneNumberVal=PIn.String(row["PhoneNumberVal"].ToString());
					string phoneNumberDigits="";
					if(!string.IsNullOrWhiteSpace(phoneNumberVal)) {
						//Not using Char.IsDigit because it includes characters like '٣' and '෯'
						phoneNumberDigits=new string(phoneNumberVal.Where(x => x>='0' && x<='9').ToArray()).TrimStart('0','1');
					}
					if(phoneType!=0 && string.IsNullOrEmpty(phoneNumberDigits)) {//PhoneType.Other=0
						continue;
					}
					//PatNum,PhoneNumberVal,PhoneNumberDigits,PhoneType
					string onePhoneNumber=$"({POut.Long(patNum)},'{POut.String(phoneNumberVal)}','{POut.String(phoneNumberDigits)}',{POut.Int(phoneType)})";
					if(!string.IsNullOrWhiteSpace(values)) {
						values+=",";
					}
					values+=onePhoneNumber;
					if($"{insert} {values}".Length+1 > TableBase.MaxAllowedPacketCount) {//+1, see Crud...InsertMany()
						if(string.IsNullOrWhiteSpace(command)) {
							//Safety net in case the first set of values exceeds max allowed packet size.  Direct the user to manually run this code from the UI.
							//(All clinics,error message,Update=32,High=3,Read|Delete|OpenForm=7,FormModuleSetup=17,1=Family Tab,"",0)
							string error="An error occured during the update.  Please click the "
								+"\"Store patient phone numbers in a separate table for patient search\" Sync button in Module Preferences - Family";
							command=$"INSERT INTO alertitem (ClinicNum,Description,Type,Severity,Actions,FormToOpen,FKey,ItemValue,UserNum) VALUES (-1,'{POut.String(error)}',32,3,7,17,1,'',0)";
							//Inserted outside of loop.
							break;
						}
						i--;//This iteration will cause the query to exceed max allowed packet size.
						Db.NonQ(command);//Run the query as of the previous iteration.
						command="";
						values="";//Start next batch.
					}
					else {
						command=$"{insert} {values}";//Continue current batch.
					}
				}
				if(!string.IsNullOrWhiteSpace(command)) {
					Db.NonQ(command);//Run the last batch, if there were any batches.
				}
				command="UPDATE preference SET ValueString='1' WHERE PrefName='PatientPhoneUsePhonenumberTable'";
				Db.NonQ(command);
				ODEvent.Fire(ODEventType.ConvertDatabases,upgrading);//No translation in convert script.
			}
			command="ALTER TABLE carrier ADD OrthoInsPayConsolidate tinyint NOT NULL";//0 - OrthoInsPayConsolidate.Global by default.
			Db.NonQ(command);
			command="DROP TABLE IF EXISTS cloudaddress";
			Db.NonQ(command);
			command=@"CREATE TABLE cloudaddress (
				CloudAddressNum bigint NOT NULL auto_increment PRIMARY KEY,
				IpAddress varchar(50) NOT NULL,
				UserNumLastConnect bigint NOT NULL,
				DateTimeLastConnect datetime NOT NULL DEFAULT '0001-01-01 00:00:00',
				INDEX(UserNumLastConnect)
				) DEFAULT CHARSET=utf8";
			Db.NonQ(command);
			command="SELECT ValueString FROM preference WHERE PrefName='CloudAllowedIpAddresses'";
			string str=Db.GetScalar(command);
			string[] addresses=str.Split(',');
			if(addresses.Length>0) {//If the database has no allowed addresses then we don't need to insert any into the new table.
				command=$"INSERT INTO cloudaddress (IpAddress) Values {string.Join(",",addresses.Select(x => "('"+POut.String(x)+"')"))}";
				Db.NonQ(command);
			}
			//Sync Patient Portal Invites into Automated Messaging preference
			command="SELECT ClinicNum,IsConfirmEnabled,IsConfirmDefault FROM clinic";
			table=Db.GetTable(command);
			for(int i=0;i<table.Rows.Count;i++) {
				long clinicNum=PIn.Long(table.Rows[i]["ClinicNum"].ToString());
				bool isAutoCommEnabled=PIn.Bool(table.Rows[i]["IsConfirmEnabled"].ToString());
				bool isConfirmDefault=PIn.Bool(table.Rows[i]["IsConfirmDefault"].ToString());
				command="SELECT ValueString FROM clinicpref WHERE PrefName='PatientPortalInviteEnabled' AND ClinicNum="+POut.Long(clinicNum);
				string clinicPrefValueString=Db.GetScalar(command);
				bool isPatientPortalInviteEnabled=PIn.Bool(clinicPrefValueString);
				command="SELECT COUNT(*) FROM clinicpref WHERE PrefName='PatientPortalInviteUseDefaults' AND ClinicNum="+POut.Long(clinicNum);
				long count=Db.GetLong(command);
				//If the PatientPortalInviteUseDefaults clinicpref doesn't exist, create it for this clinic.
				//On previous versions, the absence of this preference set use defaults to true. Starting with this version, use defaults will be false if the clinicpref doesn't exist.
				//We need to create this clinicpref to preserve old logic.
				if(count==0) {
					command="INSERT INTO clinicpref(ClinicNum,PrefName,ValueString) VALUES("+clinicNum+",'PatientPortalInviteUseDefaults','1')";
					Db.NonQ(command);
				}
				//If automated messaging is enabled but patient portal invites are not, disable all patient portal invite rules for that clinic
				if(isAutoCommEnabled && !isPatientPortalInviteEnabled) {
					//Disable all of the rules for patient portal invite for the clinic
					command="UPDATE apptreminderrule SET IsEnabled=0 WHERE TypeCur="+POut.Int((int)ApptReminderType.PatientPortalInvite)
						+" AND ClinicNum="+POut.Long(clinicNum);
					Db.NonQ(command);
					//Set patient portal invite use defaults to false for the clinic
					command="UPDATE clinicpref SET ValueString='0' WHERE PrefName='PatientPortalInviteUseDefaults'"
						+" AND ClinicNum="+POut.Long(clinicNum);
					Db.NonQ(command);
				}
				//If automated messaging is not enabled but patient portal invites are, disable all other automated messaging rules for that clinic except for birthdays.
				else if(!isAutoCommEnabled && isPatientPortalInviteEnabled) {
					List<int> listApptReminderTypeValues=new List<int>(){(int)ApptReminderType.PatientPortalInvite,(int)ApptReminderType.Birthday};
					command="UPDATE apptreminderrule SET IsEnabled=0 WHERE TypeCur NOT IN("+String.Join(",",listApptReminderTypeValues.Select(x => POut.Int(x)))+")"
						+" AND ClinicNum="+POut.Long(clinicNum);
					Db.NonQ(command);
					//Turn on autocomm for this clinic, which will allow invites to remain on. We will turn off all other varieties of autocomm below.
					command="UPDATE clinic SET IsConfirmEnabled='1' WHERE ClinicNum="+POut.Long(clinicNum);
					Db.NonQ(command);
					//Set 'UseDefaults' to be false when created.
					isConfirmDefault=false;
				}
				//Create the 'UseDefaults' preferences.
				command="INSERT INTO clinicpref(ClinicNum,PrefName,ValueString) VALUES("+clinicNum+",'ApptArrivalUseDefaults','"+POut.Bool(isConfirmDefault)+"')";
				Db.NonQ(command);
				command="INSERT INTO clinicpref(ClinicNum,PrefName,ValueString) VALUES("+clinicNum+",'ApptConfirmUseDefaults','"+POut.Bool(isConfirmDefault)+"')";
				Db.NonQ(command);
				command="INSERT INTO clinicpref(ClinicNum,PrefName,ValueString) VALUES("+clinicNum+",'ApptReminderUseDefaults','"+POut.Bool(isConfirmDefault)+"')";
				Db.NonQ(command);
				command="INSERT INTO clinicpref(ClinicNum,PrefName,ValueString) VALUES("+clinicNum+",'ApptThankYouUseDefaults','"+POut.Bool(isConfirmDefault)+"')";
				Db.NonQ(command);
			}
			command="ALTER TABLE mobileappdevice ADD IsBYODDevice TINYINT NOT NULL";
			Db.NonQ(command);
			command="INSERT INTO preference(PrefName,ValueString) VALUES('TextPaymentLinkAppointmentBalance','[nameF] please visit this [StatementShortURL] to pay your balance of [StatementBalance] for your recent appointment.')";
			Db.NonQ(command);
			command="INSERT INTO preference(PrefName,ValueString) VALUES('TextPaymentLinkAccountBalance','[nameF] please visit this [StatementShortURL] to pay your balance of [StatementBalance]')";
			Db.NonQ(command);
			command="INSERT INTO preference (PrefName,ValueString) VALUES ('EClipboardImageCaptureDefs','')";
			Db.NonQ(command);
			command="SELECT MAX(ItemOrder) FROM definition WHERE Category=18";
			int order=PIn.Int(Db.GetScalar(command))+1;
			command=$"INSERT INTO definition (Category,ItemOrder,ItemName,ItemValue,ItemColor,IsHidden) VALUES({POut.Long(18)},{POut.Long(order)},'eClipboard','',0,0)";
			Db.NonQ(command);
			//53 - DefCat.eClipboardImageCapture
			command="INSERT INTO definition (Category,ItemName,ItemValue,ItemOrder) VALUES (53,'Photo ID Front','Please take a picture of the front side of your photo ID',0)";
			Db.NonQ(command);
			command="INSERT INTO definition (Category,ItemName,ItemValue,ItemOrder) VALUES (53,'Photo ID Back','Please take a picture of the back side of your photo ID',1)";
			Db.NonQ(command);
			command="INSERT INTO definition (Category,ItemName,ItemValue,ItemOrder) VALUES (53,'Insurance Card Front','Please take a picture of the front side of your insurance card',2)";
			Db.NonQ(command);
			command="INSERT INTO definition (Category,ItemName,ItemValue,ItemOrder) VALUES (53,'Insurance Card Back','Please take a picture of the back side of your insurance card',3)";
			Db.NonQ(command);
			LargeTableHelper.AlterLargeTable("claimproc","ClaimProcNum",new List<Tuple<string,string>> { Tuple.Create("ClaimAdjReasonCodes","varchar(255) NOT NULL") });
			command="ALTER TABLE paysplit ADD PayPlanDebitType tinyint NOT NULL";
			Db.NonQ(command);
			LargeTableHelper.AlterLargeTable("treatplan","TreatPlanNum",
				new List<Tuple<string,string>> { Tuple.Create("MobileAppDeviceNum","bigint NOT NULL DEFAULT 0") },
				new List<Tuple<string,string>> { Tuple.Create("MobileAppDeviceNum","") });
			try {
				if(IsUsingReplication()) {
					string replicationMonitorMsg="Monitoring the slave status is now monitored by the OpenDentalReplicationService. "
					+"Each replication server will need the new OpenDentalReplicationService installed. "
					+"Please visit https://opendental.com and search for 'Slave Monitor' for more information.";
					command=$"INSERT INTO alertitem (ClinicNum,Description,Type,Severity,Actions,FormToOpen,FKey,ItemValue,UserNum) VALUES (-1,'{POut.String(replicationMonitorMsg)}',33,3,5,0,0,'',0)";
					Db.NonQ(command);
					command="SELECT AlertCategoryNum FROM alertcategory WHERE InternalName='OdAllTypes'";
					alertCatNum=Db.GetLong(command);
					command=$@"INSERT INTO alertcategorylink (AlertCategoryNum,AlertType) VALUES({POut.Long(alertCatNum)},33)";//33=ReplicationService warning 
					Db.NonQ(command);
				}
			}
			catch {
				//Do nothing. Treat the office as not using replication.
			}
			command="INSERT INTO preference(PrefName,ValueString) VALUES('RefundAdjustmentType','0')";
			Db.NonQ(command);
			command="DELETE FROM userodpref WHERE ValueString='' AND FkeyType=0";//FkeyType 0=Definition, expanded imaging categories. ValueString '' meant collapsed, which was meaningless.
			Db.NonQ(command);
			command="UPDATE userodpref SET ValueString='' WHERE FkeyType=0";//Expanded imaging categories were full of meaningless junk strings. No ValueString needed.
			Db.NonQ(command);
		}//End of 21_2_1() method
	}
}
