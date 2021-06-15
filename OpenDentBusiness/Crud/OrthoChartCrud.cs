//This file is automatically generated.
//Do not attempt to make changes to this file because the changes will be erased and overwritten.
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;

namespace OpenDentBusiness.Crud{
	public class OrthoChartCrud {
		///<summary>Gets one OrthoChart object from the database using the primary key.  Returns null if not found.</summary>
		public static OrthoChart SelectOne(long orthoChartNum) {
			string command="SELECT * FROM orthochart "
				+"WHERE OrthoChartNum = "+POut.Long(orthoChartNum);
			List<OrthoChart> list=TableToList(Db.GetTable(command));
			if(list.Count==0) {
				return null;
			}
			return list[0];
		}

		///<summary>Gets one OrthoChart object from the database using a query.</summary>
		public static OrthoChart SelectOne(string command) {
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				throw new ApplicationException("Not allowed to send sql directly.  Rewrite the calling class to not use this query:\r\n"+command);
			}
			List<OrthoChart> list=TableToList(Db.GetTable(command));
			if(list.Count==0) {
				return null;
			}
			return list[0];
		}

		///<summary>Gets a list of OrthoChart objects from the database using a query.</summary>
		public static List<OrthoChart> SelectMany(string command) {
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				throw new ApplicationException("Not allowed to send sql directly.  Rewrite the calling class to not use this query:\r\n"+command);
			}
			List<OrthoChart> list=TableToList(Db.GetTable(command));
			return list;
		}

		///<summary>Converts a DataTable to a list of objects.</summary>
		public static List<OrthoChart> TableToList(DataTable table) {
			List<OrthoChart> retVal=new List<OrthoChart>();
			OrthoChart orthoChart;
			foreach(DataRow row in table.Rows) {
				orthoChart=new OrthoChart();
				orthoChart.OrthoChartNum   = PIn.Long  (row["OrthoChartNum"].ToString());
				orthoChart.PatNum          = PIn.Long  (row["PatNum"].ToString());
				orthoChart.DateService     = PIn.Date  (row["DateService"].ToString());
				orthoChart.FieldName       = PIn.String(row["FieldName"].ToString());
				orthoChart.FieldValue      = PIn.String(row["FieldValue"].ToString());
				orthoChart.UserNum         = PIn.Long  (row["UserNum"].ToString());
				orthoChart.ProvNum         = PIn.Long  (row["ProvNum"].ToString());
				orthoChart.OrthoChartRowNum= PIn.Long  (row["OrthoChartRowNum"].ToString());
				retVal.Add(orthoChart);
			}
			return retVal;
		}

		///<summary>Converts a list of OrthoChart into a DataTable.</summary>
		public static DataTable ListToTable(List<OrthoChart> listOrthoCharts,string tableName="") {
			if(string.IsNullOrEmpty(tableName)) {
				tableName="OrthoChart";
			}
			DataTable table=new DataTable(tableName);
			table.Columns.Add("OrthoChartNum");
			table.Columns.Add("PatNum");
			table.Columns.Add("DateService");
			table.Columns.Add("FieldName");
			table.Columns.Add("FieldValue");
			table.Columns.Add("UserNum");
			table.Columns.Add("ProvNum");
			table.Columns.Add("OrthoChartRowNum");
			foreach(OrthoChart orthoChart in listOrthoCharts) {
				table.Rows.Add(new object[] {
					POut.Long  (orthoChart.OrthoChartNum),
					POut.Long  (orthoChart.PatNum),
					POut.DateT (orthoChart.DateService,false),
					            orthoChart.FieldName,
					            orthoChart.FieldValue,
					POut.Long  (orthoChart.UserNum),
					POut.Long  (orthoChart.ProvNum),
					POut.Long  (orthoChart.OrthoChartRowNum),
				});
			}
			return table;
		}

		///<summary>Inserts one OrthoChart into the database.  Returns the new priKey.</summary>
		public static long Insert(OrthoChart orthoChart) {
			return Insert(orthoChart,false);
		}

		///<summary>Inserts one OrthoChart into the database.  Provides option to use the existing priKey.</summary>
		public static long Insert(OrthoChart orthoChart,bool useExistingPK) {
			if(!useExistingPK && PrefC.RandomKeys) {
				orthoChart.OrthoChartNum=ReplicationServers.GetKey("orthochart","OrthoChartNum");
			}
			string command="INSERT INTO orthochart (";
			if(useExistingPK || PrefC.RandomKeys) {
				command+="OrthoChartNum,";
			}
			command+="PatNum,DateService,FieldName,FieldValue,UserNum,ProvNum,OrthoChartRowNum) VALUES(";
			if(useExistingPK || PrefC.RandomKeys) {
				command+=POut.Long(orthoChart.OrthoChartNum)+",";
			}
			command+=
				     POut.Long  (orthoChart.PatNum)+","
				+    POut.Date  (orthoChart.DateService)+","
				+"'"+POut.String(orthoChart.FieldName)+"',"
				+    DbHelper.ParamChar+"paramFieldValue,"
				+    POut.Long  (orthoChart.UserNum)+","
				+    POut.Long  (orthoChart.ProvNum)+","
				+    POut.Long  (orthoChart.OrthoChartRowNum)+")";
			if(orthoChart.FieldValue==null) {
				orthoChart.FieldValue="";
			}
			OdSqlParameter paramFieldValue=new OdSqlParameter("paramFieldValue",OdDbType.Text,POut.StringParam(orthoChart.FieldValue));
			if(useExistingPK || PrefC.RandomKeys) {
				Db.NonQ(command,paramFieldValue);
			}
			else {
				orthoChart.OrthoChartNum=Db.NonQ(command,true,"OrthoChartNum","orthoChart",paramFieldValue);
			}
			return orthoChart.OrthoChartNum;
		}

		///<summary>Inserts one OrthoChart into the database.  Returns the new priKey.  Doesn't use the cache.</summary>
		public static long InsertNoCache(OrthoChart orthoChart) {
			return InsertNoCache(orthoChart,false);
		}

		///<summary>Inserts one OrthoChart into the database.  Provides option to use the existing priKey.  Doesn't use the cache.</summary>
		public static long InsertNoCache(OrthoChart orthoChart,bool useExistingPK) {
			bool isRandomKeys=Prefs.GetBoolNoCache(PrefName.RandomPrimaryKeys);
			string command="INSERT INTO orthochart (";
			if(!useExistingPK && isRandomKeys) {
				orthoChart.OrthoChartNum=ReplicationServers.GetKeyNoCache("orthochart","OrthoChartNum");
			}
			if(isRandomKeys || useExistingPK) {
				command+="OrthoChartNum,";
			}
			command+="PatNum,DateService,FieldName,FieldValue,UserNum,ProvNum,OrthoChartRowNum) VALUES(";
			if(isRandomKeys || useExistingPK) {
				command+=POut.Long(orthoChart.OrthoChartNum)+",";
			}
			command+=
				     POut.Long  (orthoChart.PatNum)+","
				+    POut.Date  (orthoChart.DateService)+","
				+"'"+POut.String(orthoChart.FieldName)+"',"
				+    DbHelper.ParamChar+"paramFieldValue,"
				+    POut.Long  (orthoChart.UserNum)+","
				+    POut.Long  (orthoChart.ProvNum)+","
				+    POut.Long  (orthoChart.OrthoChartRowNum)+")";
			if(orthoChart.FieldValue==null) {
				orthoChart.FieldValue="";
			}
			OdSqlParameter paramFieldValue=new OdSqlParameter("paramFieldValue",OdDbType.Text,POut.StringParam(orthoChart.FieldValue));
			if(useExistingPK || isRandomKeys) {
				Db.NonQ(command,paramFieldValue);
			}
			else {
				orthoChart.OrthoChartNum=Db.NonQ(command,true,"OrthoChartNum","orthoChart",paramFieldValue);
			}
			return orthoChart.OrthoChartNum;
		}

		///<summary>Updates one OrthoChart in the database.</summary>
		public static void Update(OrthoChart orthoChart) {
			string command="UPDATE orthochart SET "
				+"PatNum          =  "+POut.Long  (orthoChart.PatNum)+", "
				+"DateService     =  "+POut.Date  (orthoChart.DateService)+", "
				+"FieldName       = '"+POut.String(orthoChart.FieldName)+"', "
				+"FieldValue      =  "+DbHelper.ParamChar+"paramFieldValue, "
				+"UserNum         =  "+POut.Long  (orthoChart.UserNum)+", "
				+"ProvNum         =  "+POut.Long  (orthoChart.ProvNum)+", "
				+"OrthoChartRowNum=  "+POut.Long  (orthoChart.OrthoChartRowNum)+" "
				+"WHERE OrthoChartNum = "+POut.Long(orthoChart.OrthoChartNum);
			if(orthoChart.FieldValue==null) {
				orthoChart.FieldValue="";
			}
			OdSqlParameter paramFieldValue=new OdSqlParameter("paramFieldValue",OdDbType.Text,POut.StringParam(orthoChart.FieldValue));
			Db.NonQ(command,paramFieldValue);
		}

		///<summary>Updates one OrthoChart in the database.  Uses an old object to compare to, and only alters changed fields.  This prevents collisions and concurrency problems in heavily used tables.  Returns true if an update occurred.</summary>
		public static bool Update(OrthoChart orthoChart,OrthoChart oldOrthoChart) {
			string command="";
			if(orthoChart.PatNum != oldOrthoChart.PatNum) {
				if(command!="") { command+=",";}
				command+="PatNum = "+POut.Long(orthoChart.PatNum)+"";
			}
			if(orthoChart.DateService.Date != oldOrthoChart.DateService.Date) {
				if(command!="") { command+=",";}
				command+="DateService = "+POut.Date(orthoChart.DateService)+"";
			}
			if(orthoChart.FieldName != oldOrthoChart.FieldName) {
				if(command!="") { command+=",";}
				command+="FieldName = '"+POut.String(orthoChart.FieldName)+"'";
			}
			if(orthoChart.FieldValue != oldOrthoChart.FieldValue) {
				if(command!="") { command+=",";}
				command+="FieldValue = "+DbHelper.ParamChar+"paramFieldValue";
			}
			if(orthoChart.UserNum != oldOrthoChart.UserNum) {
				if(command!="") { command+=",";}
				command+="UserNum = "+POut.Long(orthoChart.UserNum)+"";
			}
			if(orthoChart.ProvNum != oldOrthoChart.ProvNum) {
				if(command!="") { command+=",";}
				command+="ProvNum = "+POut.Long(orthoChart.ProvNum)+"";
			}
			if(orthoChart.OrthoChartRowNum != oldOrthoChart.OrthoChartRowNum) {
				if(command!="") { command+=",";}
				command+="OrthoChartRowNum = "+POut.Long(orthoChart.OrthoChartRowNum)+"";
			}
			if(command=="") {
				return false;
			}
			if(orthoChart.FieldValue==null) {
				orthoChart.FieldValue="";
			}
			OdSqlParameter paramFieldValue=new OdSqlParameter("paramFieldValue",OdDbType.Text,POut.StringParam(orthoChart.FieldValue));
			command="UPDATE orthochart SET "+command
				+" WHERE OrthoChartNum = "+POut.Long(orthoChart.OrthoChartNum);
			Db.NonQ(command,paramFieldValue);
			return true;
		}

		///<summary>Returns true if Update(OrthoChart,OrthoChart) would make changes to the database.
		///Does not make any changes to the database and can be called before remoting role is checked.</summary>
		public static bool UpdateComparison(OrthoChart orthoChart,OrthoChart oldOrthoChart) {
			if(orthoChart.PatNum != oldOrthoChart.PatNum) {
				return true;
			}
			if(orthoChart.DateService.Date != oldOrthoChart.DateService.Date) {
				return true;
			}
			if(orthoChart.FieldName != oldOrthoChart.FieldName) {
				return true;
			}
			if(orthoChart.FieldValue != oldOrthoChart.FieldValue) {
				return true;
			}
			if(orthoChart.UserNum != oldOrthoChart.UserNum) {
				return true;
			}
			if(orthoChart.ProvNum != oldOrthoChart.ProvNum) {
				return true;
			}
			if(orthoChart.OrthoChartRowNum != oldOrthoChart.OrthoChartRowNum) {
				return true;
			}
			return false;
		}

		///<summary>Deletes one OrthoChart from the database.</summary>
		public static void Delete(long orthoChartNum) {
			string command="DELETE FROM orthochart "
				+"WHERE OrthoChartNum = "+POut.Long(orthoChartNum);
			Db.NonQ(command);
		}

		///<summary>Deletes many OrthoCharts from the database.</summary>
		public static void DeleteMany(List<long> listOrthoChartNums) {
			if(listOrthoChartNums==null || listOrthoChartNums.Count==0) {
				return;
			}
			string command="DELETE FROM orthochart "
				+"WHERE OrthoChartNum IN("+string.Join(",",listOrthoChartNums.Select(x => POut.Long(x)))+")";
			Db.NonQ(command);
		}

	}
}