//This file is automatically generated.
//Do not attempt to make changes to this file because the changes will be erased and overwritten.
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;

namespace OpenDentBusiness.Crud{
	public class VitalsignCrud {
		///<summary>Gets one Vitalsign object from the database using the primary key.  Returns null if not found.</summary>
		public static Vitalsign SelectOne(long vitalsignNum) {
			string command="SELECT * FROM vitalsign "
				+"WHERE VitalsignNum = "+POut.Long(vitalsignNum);
			List<Vitalsign> list=TableToList(Db.GetTable(command));
			if(list.Count==0) {
				return null;
			}
			return list[0];
		}

		///<summary>Gets one Vitalsign object from the database using a query.</summary>
		public static Vitalsign SelectOne(string command) {
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				throw new ApplicationException("Not allowed to send sql directly.  Rewrite the calling class to not use this query:\r\n"+command);
			}
			List<Vitalsign> list=TableToList(Db.GetTable(command));
			if(list.Count==0) {
				return null;
			}
			return list[0];
		}

		///<summary>Gets a list of Vitalsign objects from the database using a query.</summary>
		public static List<Vitalsign> SelectMany(string command) {
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				throw new ApplicationException("Not allowed to send sql directly.  Rewrite the calling class to not use this query:\r\n"+command);
			}
			List<Vitalsign> list=TableToList(Db.GetTable(command));
			return list;
		}

		///<summary>Converts a DataTable to a list of objects.</summary>
		public static List<Vitalsign> TableToList(DataTable table) {
			List<Vitalsign> retVal=new List<Vitalsign>();
			Vitalsign vitalsign;
			foreach(DataRow row in table.Rows) {
				vitalsign=new Vitalsign();
				vitalsign.VitalsignNum      = PIn.Long  (row["VitalsignNum"].ToString());
				vitalsign.PatNum            = PIn.Long  (row["PatNum"].ToString());
				vitalsign.Height            = PIn.Float (row["Height"].ToString());
				vitalsign.Weight            = PIn.Float (row["Weight"].ToString());
				vitalsign.BpSystolic        = PIn.Int   (row["BpSystolic"].ToString());
				vitalsign.BpDiastolic       = PIn.Int   (row["BpDiastolic"].ToString());
				vitalsign.DateTaken         = PIn.Date  (row["DateTaken"].ToString());
				vitalsign.HasFollowupPlan   = PIn.Bool  (row["HasFollowupPlan"].ToString());
				vitalsign.IsIneligible      = PIn.Bool  (row["IsIneligible"].ToString());
				vitalsign.Documentation     = PIn.String(row["Documentation"].ToString());
				vitalsign.ChildGotNutrition = PIn.Bool  (row["ChildGotNutrition"].ToString());
				vitalsign.ChildGotPhysCouns = PIn.Bool  (row["ChildGotPhysCouns"].ToString());
				vitalsign.WeightCode        = PIn.String(row["WeightCode"].ToString());
				vitalsign.HeightExamCode    = PIn.String(row["HeightExamCode"].ToString());
				vitalsign.WeightExamCode    = PIn.String(row["WeightExamCode"].ToString());
				vitalsign.BMIExamCode       = PIn.String(row["BMIExamCode"].ToString());
				vitalsign.EhrNotPerformedNum= PIn.Long  (row["EhrNotPerformedNum"].ToString());
				vitalsign.PregDiseaseNum    = PIn.Long  (row["PregDiseaseNum"].ToString());
				vitalsign.BMIPercentile     = PIn.Int   (row["BMIPercentile"].ToString());
				vitalsign.Pulse             = PIn.Int   (row["Pulse"].ToString());
				retVal.Add(vitalsign);
			}
			return retVal;
		}

		///<summary>Converts a list of Vitalsign into a DataTable.</summary>
		public static DataTable ListToTable(List<Vitalsign> listVitalsigns,string tableName="") {
			if(string.IsNullOrEmpty(tableName)) {
				tableName="Vitalsign";
			}
			DataTable table=new DataTable(tableName);
			table.Columns.Add("VitalsignNum");
			table.Columns.Add("PatNum");
			table.Columns.Add("Height");
			table.Columns.Add("Weight");
			table.Columns.Add("BpSystolic");
			table.Columns.Add("BpDiastolic");
			table.Columns.Add("DateTaken");
			table.Columns.Add("HasFollowupPlan");
			table.Columns.Add("IsIneligible");
			table.Columns.Add("Documentation");
			table.Columns.Add("ChildGotNutrition");
			table.Columns.Add("ChildGotPhysCouns");
			table.Columns.Add("WeightCode");
			table.Columns.Add("HeightExamCode");
			table.Columns.Add("WeightExamCode");
			table.Columns.Add("BMIExamCode");
			table.Columns.Add("EhrNotPerformedNum");
			table.Columns.Add("PregDiseaseNum");
			table.Columns.Add("BMIPercentile");
			table.Columns.Add("Pulse");
			foreach(Vitalsign vitalsign in listVitalsigns) {
				table.Rows.Add(new object[] {
					POut.Long  (vitalsign.VitalsignNum),
					POut.Long  (vitalsign.PatNum),
					POut.Float (vitalsign.Height),
					POut.Float (vitalsign.Weight),
					POut.Int   (vitalsign.BpSystolic),
					POut.Int   (vitalsign.BpDiastolic),
					POut.DateT (vitalsign.DateTaken,false),
					POut.Bool  (vitalsign.HasFollowupPlan),
					POut.Bool  (vitalsign.IsIneligible),
					            vitalsign.Documentation,
					POut.Bool  (vitalsign.ChildGotNutrition),
					POut.Bool  (vitalsign.ChildGotPhysCouns),
					            vitalsign.WeightCode,
					            vitalsign.HeightExamCode,
					            vitalsign.WeightExamCode,
					            vitalsign.BMIExamCode,
					POut.Long  (vitalsign.EhrNotPerformedNum),
					POut.Long  (vitalsign.PregDiseaseNum),
					POut.Int   (vitalsign.BMIPercentile),
					POut.Int   (vitalsign.Pulse),
				});
			}
			return table;
		}

		///<summary>Inserts one Vitalsign into the database.  Returns the new priKey.</summary>
		public static long Insert(Vitalsign vitalsign) {
			return Insert(vitalsign,false);
		}

		///<summary>Inserts one Vitalsign into the database.  Provides option to use the existing priKey.</summary>
		public static long Insert(Vitalsign vitalsign,bool useExistingPK) {
			if(!useExistingPK && PrefC.RandomKeys) {
				vitalsign.VitalsignNum=ReplicationServers.GetKey("vitalsign","VitalsignNum");
			}
			string command="INSERT INTO vitalsign (";
			if(useExistingPK || PrefC.RandomKeys) {
				command+="VitalsignNum,";
			}
			command+="PatNum,Height,Weight,BpSystolic,BpDiastolic,DateTaken,HasFollowupPlan,IsIneligible,Documentation,ChildGotNutrition,ChildGotPhysCouns,WeightCode,HeightExamCode,WeightExamCode,BMIExamCode,EhrNotPerformedNum,PregDiseaseNum,BMIPercentile,Pulse) VALUES(";
			if(useExistingPK || PrefC.RandomKeys) {
				command+=POut.Long(vitalsign.VitalsignNum)+",";
			}
			command+=
				     POut.Long  (vitalsign.PatNum)+","
				+    POut.Float (vitalsign.Height)+","
				+    POut.Float (vitalsign.Weight)+","
				+    POut.Int   (vitalsign.BpSystolic)+","
				+    POut.Int   (vitalsign.BpDiastolic)+","
				+    POut.Date  (vitalsign.DateTaken)+","
				+    POut.Bool  (vitalsign.HasFollowupPlan)+","
				+    POut.Bool  (vitalsign.IsIneligible)+","
				+    DbHelper.ParamChar+"paramDocumentation,"
				+    POut.Bool  (vitalsign.ChildGotNutrition)+","
				+    POut.Bool  (vitalsign.ChildGotPhysCouns)+","
				+"'"+POut.String(vitalsign.WeightCode)+"',"
				+"'"+POut.String(vitalsign.HeightExamCode)+"',"
				+"'"+POut.String(vitalsign.WeightExamCode)+"',"
				+"'"+POut.String(vitalsign.BMIExamCode)+"',"
				+    POut.Long  (vitalsign.EhrNotPerformedNum)+","
				+    POut.Long  (vitalsign.PregDiseaseNum)+","
				+    POut.Int   (vitalsign.BMIPercentile)+","
				+    POut.Int   (vitalsign.Pulse)+")";
			if(vitalsign.Documentation==null) {
				vitalsign.Documentation="";
			}
			OdSqlParameter paramDocumentation=new OdSqlParameter("paramDocumentation",OdDbType.Text,POut.StringParam(vitalsign.Documentation));
			if(useExistingPK || PrefC.RandomKeys) {
				Db.NonQ(command,paramDocumentation);
			}
			else {
				vitalsign.VitalsignNum=Db.NonQ(command,true,"VitalsignNum","vitalsign",paramDocumentation);
			}
			return vitalsign.VitalsignNum;
		}

		///<summary>Inserts one Vitalsign into the database.  Returns the new priKey.  Doesn't use the cache.</summary>
		public static long InsertNoCache(Vitalsign vitalsign) {
			return InsertNoCache(vitalsign,false);
		}

		///<summary>Inserts one Vitalsign into the database.  Provides option to use the existing priKey.  Doesn't use the cache.</summary>
		public static long InsertNoCache(Vitalsign vitalsign,bool useExistingPK) {
			bool isRandomKeys=Prefs.GetBoolNoCache(PrefName.RandomPrimaryKeys);
			string command="INSERT INTO vitalsign (";
			if(!useExistingPK && isRandomKeys) {
				vitalsign.VitalsignNum=ReplicationServers.GetKeyNoCache("vitalsign","VitalsignNum");
			}
			if(isRandomKeys || useExistingPK) {
				command+="VitalsignNum,";
			}
			command+="PatNum,Height,Weight,BpSystolic,BpDiastolic,DateTaken,HasFollowupPlan,IsIneligible,Documentation,ChildGotNutrition,ChildGotPhysCouns,WeightCode,HeightExamCode,WeightExamCode,BMIExamCode,EhrNotPerformedNum,PregDiseaseNum,BMIPercentile,Pulse) VALUES(";
			if(isRandomKeys || useExistingPK) {
				command+=POut.Long(vitalsign.VitalsignNum)+",";
			}
			command+=
				     POut.Long  (vitalsign.PatNum)+","
				+    POut.Float (vitalsign.Height)+","
				+    POut.Float (vitalsign.Weight)+","
				+    POut.Int   (vitalsign.BpSystolic)+","
				+    POut.Int   (vitalsign.BpDiastolic)+","
				+    POut.Date  (vitalsign.DateTaken)+","
				+    POut.Bool  (vitalsign.HasFollowupPlan)+","
				+    POut.Bool  (vitalsign.IsIneligible)+","
				+    DbHelper.ParamChar+"paramDocumentation,"
				+    POut.Bool  (vitalsign.ChildGotNutrition)+","
				+    POut.Bool  (vitalsign.ChildGotPhysCouns)+","
				+"'"+POut.String(vitalsign.WeightCode)+"',"
				+"'"+POut.String(vitalsign.HeightExamCode)+"',"
				+"'"+POut.String(vitalsign.WeightExamCode)+"',"
				+"'"+POut.String(vitalsign.BMIExamCode)+"',"
				+    POut.Long  (vitalsign.EhrNotPerformedNum)+","
				+    POut.Long  (vitalsign.PregDiseaseNum)+","
				+    POut.Int   (vitalsign.BMIPercentile)+","
				+    POut.Int   (vitalsign.Pulse)+")";
			if(vitalsign.Documentation==null) {
				vitalsign.Documentation="";
			}
			OdSqlParameter paramDocumentation=new OdSqlParameter("paramDocumentation",OdDbType.Text,POut.StringParam(vitalsign.Documentation));
			if(useExistingPK || isRandomKeys) {
				Db.NonQ(command,paramDocumentation);
			}
			else {
				vitalsign.VitalsignNum=Db.NonQ(command,true,"VitalsignNum","vitalsign",paramDocumentation);
			}
			return vitalsign.VitalsignNum;
		}

		///<summary>Updates one Vitalsign in the database.</summary>
		public static void Update(Vitalsign vitalsign) {
			string command="UPDATE vitalsign SET "
				+"PatNum            =  "+POut.Long  (vitalsign.PatNum)+", "
				+"Height            =  "+POut.Float (vitalsign.Height)+", "
				+"Weight            =  "+POut.Float (vitalsign.Weight)+", "
				+"BpSystolic        =  "+POut.Int   (vitalsign.BpSystolic)+", "
				+"BpDiastolic       =  "+POut.Int   (vitalsign.BpDiastolic)+", "
				+"DateTaken         =  "+POut.Date  (vitalsign.DateTaken)+", "
				+"HasFollowupPlan   =  "+POut.Bool  (vitalsign.HasFollowupPlan)+", "
				+"IsIneligible      =  "+POut.Bool  (vitalsign.IsIneligible)+", "
				+"Documentation     =  "+DbHelper.ParamChar+"paramDocumentation, "
				+"ChildGotNutrition =  "+POut.Bool  (vitalsign.ChildGotNutrition)+", "
				+"ChildGotPhysCouns =  "+POut.Bool  (vitalsign.ChildGotPhysCouns)+", "
				+"WeightCode        = '"+POut.String(vitalsign.WeightCode)+"', "
				+"HeightExamCode    = '"+POut.String(vitalsign.HeightExamCode)+"', "
				+"WeightExamCode    = '"+POut.String(vitalsign.WeightExamCode)+"', "
				+"BMIExamCode       = '"+POut.String(vitalsign.BMIExamCode)+"', "
				+"EhrNotPerformedNum=  "+POut.Long  (vitalsign.EhrNotPerformedNum)+", "
				+"PregDiseaseNum    =  "+POut.Long  (vitalsign.PregDiseaseNum)+", "
				+"BMIPercentile     =  "+POut.Int   (vitalsign.BMIPercentile)+", "
				+"Pulse             =  "+POut.Int   (vitalsign.Pulse)+" "
				+"WHERE VitalsignNum = "+POut.Long(vitalsign.VitalsignNum);
			if(vitalsign.Documentation==null) {
				vitalsign.Documentation="";
			}
			OdSqlParameter paramDocumentation=new OdSqlParameter("paramDocumentation",OdDbType.Text,POut.StringParam(vitalsign.Documentation));
			Db.NonQ(command,paramDocumentation);
		}

		///<summary>Updates one Vitalsign in the database.  Uses an old object to compare to, and only alters changed fields.  This prevents collisions and concurrency problems in heavily used tables.  Returns true if an update occurred.</summary>
		public static bool Update(Vitalsign vitalsign,Vitalsign oldVitalsign) {
			string command="";
			if(vitalsign.PatNum != oldVitalsign.PatNum) {
				if(command!="") { command+=",";}
				command+="PatNum = "+POut.Long(vitalsign.PatNum)+"";
			}
			if(vitalsign.Height != oldVitalsign.Height) {
				if(command!="") { command+=",";}
				command+="Height = "+POut.Float(vitalsign.Height)+"";
			}
			if(vitalsign.Weight != oldVitalsign.Weight) {
				if(command!="") { command+=",";}
				command+="Weight = "+POut.Float(vitalsign.Weight)+"";
			}
			if(vitalsign.BpSystolic != oldVitalsign.BpSystolic) {
				if(command!="") { command+=",";}
				command+="BpSystolic = "+POut.Int(vitalsign.BpSystolic)+"";
			}
			if(vitalsign.BpDiastolic != oldVitalsign.BpDiastolic) {
				if(command!="") { command+=",";}
				command+="BpDiastolic = "+POut.Int(vitalsign.BpDiastolic)+"";
			}
			if(vitalsign.DateTaken.Date != oldVitalsign.DateTaken.Date) {
				if(command!="") { command+=",";}
				command+="DateTaken = "+POut.Date(vitalsign.DateTaken)+"";
			}
			if(vitalsign.HasFollowupPlan != oldVitalsign.HasFollowupPlan) {
				if(command!="") { command+=",";}
				command+="HasFollowupPlan = "+POut.Bool(vitalsign.HasFollowupPlan)+"";
			}
			if(vitalsign.IsIneligible != oldVitalsign.IsIneligible) {
				if(command!="") { command+=",";}
				command+="IsIneligible = "+POut.Bool(vitalsign.IsIneligible)+"";
			}
			if(vitalsign.Documentation != oldVitalsign.Documentation) {
				if(command!="") { command+=",";}
				command+="Documentation = "+DbHelper.ParamChar+"paramDocumentation";
			}
			if(vitalsign.ChildGotNutrition != oldVitalsign.ChildGotNutrition) {
				if(command!="") { command+=",";}
				command+="ChildGotNutrition = "+POut.Bool(vitalsign.ChildGotNutrition)+"";
			}
			if(vitalsign.ChildGotPhysCouns != oldVitalsign.ChildGotPhysCouns) {
				if(command!="") { command+=",";}
				command+="ChildGotPhysCouns = "+POut.Bool(vitalsign.ChildGotPhysCouns)+"";
			}
			if(vitalsign.WeightCode != oldVitalsign.WeightCode) {
				if(command!="") { command+=",";}
				command+="WeightCode = '"+POut.String(vitalsign.WeightCode)+"'";
			}
			if(vitalsign.HeightExamCode != oldVitalsign.HeightExamCode) {
				if(command!="") { command+=",";}
				command+="HeightExamCode = '"+POut.String(vitalsign.HeightExamCode)+"'";
			}
			if(vitalsign.WeightExamCode != oldVitalsign.WeightExamCode) {
				if(command!="") { command+=",";}
				command+="WeightExamCode = '"+POut.String(vitalsign.WeightExamCode)+"'";
			}
			if(vitalsign.BMIExamCode != oldVitalsign.BMIExamCode) {
				if(command!="") { command+=",";}
				command+="BMIExamCode = '"+POut.String(vitalsign.BMIExamCode)+"'";
			}
			if(vitalsign.EhrNotPerformedNum != oldVitalsign.EhrNotPerformedNum) {
				if(command!="") { command+=",";}
				command+="EhrNotPerformedNum = "+POut.Long(vitalsign.EhrNotPerformedNum)+"";
			}
			if(vitalsign.PregDiseaseNum != oldVitalsign.PregDiseaseNum) {
				if(command!="") { command+=",";}
				command+="PregDiseaseNum = "+POut.Long(vitalsign.PregDiseaseNum)+"";
			}
			if(vitalsign.BMIPercentile != oldVitalsign.BMIPercentile) {
				if(command!="") { command+=",";}
				command+="BMIPercentile = "+POut.Int(vitalsign.BMIPercentile)+"";
			}
			if(vitalsign.Pulse != oldVitalsign.Pulse) {
				if(command!="") { command+=",";}
				command+="Pulse = "+POut.Int(vitalsign.Pulse)+"";
			}
			if(command=="") {
				return false;
			}
			if(vitalsign.Documentation==null) {
				vitalsign.Documentation="";
			}
			OdSqlParameter paramDocumentation=new OdSqlParameter("paramDocumentation",OdDbType.Text,POut.StringParam(vitalsign.Documentation));
			command="UPDATE vitalsign SET "+command
				+" WHERE VitalsignNum = "+POut.Long(vitalsign.VitalsignNum);
			Db.NonQ(command,paramDocumentation);
			return true;
		}

		///<summary>Returns true if Update(Vitalsign,Vitalsign) would make changes to the database.
		///Does not make any changes to the database and can be called before remoting role is checked.</summary>
		public static bool UpdateComparison(Vitalsign vitalsign,Vitalsign oldVitalsign) {
			if(vitalsign.PatNum != oldVitalsign.PatNum) {
				return true;
			}
			if(vitalsign.Height != oldVitalsign.Height) {
				return true;
			}
			if(vitalsign.Weight != oldVitalsign.Weight) {
				return true;
			}
			if(vitalsign.BpSystolic != oldVitalsign.BpSystolic) {
				return true;
			}
			if(vitalsign.BpDiastolic != oldVitalsign.BpDiastolic) {
				return true;
			}
			if(vitalsign.DateTaken.Date != oldVitalsign.DateTaken.Date) {
				return true;
			}
			if(vitalsign.HasFollowupPlan != oldVitalsign.HasFollowupPlan) {
				return true;
			}
			if(vitalsign.IsIneligible != oldVitalsign.IsIneligible) {
				return true;
			}
			if(vitalsign.Documentation != oldVitalsign.Documentation) {
				return true;
			}
			if(vitalsign.ChildGotNutrition != oldVitalsign.ChildGotNutrition) {
				return true;
			}
			if(vitalsign.ChildGotPhysCouns != oldVitalsign.ChildGotPhysCouns) {
				return true;
			}
			if(vitalsign.WeightCode != oldVitalsign.WeightCode) {
				return true;
			}
			if(vitalsign.HeightExamCode != oldVitalsign.HeightExamCode) {
				return true;
			}
			if(vitalsign.WeightExamCode != oldVitalsign.WeightExamCode) {
				return true;
			}
			if(vitalsign.BMIExamCode != oldVitalsign.BMIExamCode) {
				return true;
			}
			if(vitalsign.EhrNotPerformedNum != oldVitalsign.EhrNotPerformedNum) {
				return true;
			}
			if(vitalsign.PregDiseaseNum != oldVitalsign.PregDiseaseNum) {
				return true;
			}
			if(vitalsign.BMIPercentile != oldVitalsign.BMIPercentile) {
				return true;
			}
			if(vitalsign.Pulse != oldVitalsign.Pulse) {
				return true;
			}
			return false;
		}

		///<summary>Deletes one Vitalsign from the database.</summary>
		public static void Delete(long vitalsignNum) {
			string command="DELETE FROM vitalsign "
				+"WHERE VitalsignNum = "+POut.Long(vitalsignNum);
			Db.NonQ(command);
		}

		///<summary>Deletes many Vitalsigns from the database.</summary>
		public static void DeleteMany(List<long> listVitalsignNums) {
			if(listVitalsignNums==null || listVitalsignNums.Count==0) {
				return;
			}
			string command="DELETE FROM vitalsign "
				+"WHERE VitalsignNum IN("+string.Join(",",listVitalsignNums.Select(x => POut.Long(x)))+")";
			Db.NonQ(command);
		}

	}
}