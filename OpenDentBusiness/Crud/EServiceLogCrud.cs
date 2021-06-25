//This file is automatically generated.
//Do not attempt to make changes to this file because the changes will be erased and overwritten.
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;

namespace OpenDentBusiness.Crud{
	public class EServiceLogCrud {
		///<summary>Gets one EServiceLog object from the database using the primary key.  Returns null if not found.</summary>
		public static EServiceLog SelectOne(long eServiceLogNum) {
			string command="SELECT * FROM eservicelog "
				+"WHERE EServiceLogNum = "+POut.Long(eServiceLogNum);
			List<EServiceLog> list=TableToList(Db.GetTable(command));
			if(list.Count==0) {
				return null;
			}
			return list[0];
		}

		///<summary>Gets one EServiceLog object from the database using a query.</summary>
		public static EServiceLog SelectOne(string command) {
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				throw new ApplicationException("Not allowed to send sql directly.  Rewrite the calling class to not use this query:\r\n"+command);
			}
			List<EServiceLog> list=TableToList(Db.GetTable(command));
			if(list.Count==0) {
				return null;
			}
			return list[0];
		}

		///<summary>Gets a list of EServiceLog objects from the database using a query.</summary>
		public static List<EServiceLog> SelectMany(string command) {
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				throw new ApplicationException("Not allowed to send sql directly.  Rewrite the calling class to not use this query:\r\n"+command);
			}
			List<EServiceLog> list=TableToList(Db.GetTable(command));
			return list;
		}

		///<summary>Converts a DataTable to a list of objects.</summary>
		public static List<EServiceLog> TableToList(DataTable table) {
			List<EServiceLog> retVal=new List<EServiceLog>();
			EServiceLog eServiceLog;
			foreach(DataRow row in table.Rows) {
				eServiceLog=new EServiceLog();
				eServiceLog.EServiceLogNum= PIn.Long  (row["EServiceLogNum"].ToString());
				eServiceLog.KeyType       = (OpenDentBusiness.FKeyType)PIn.Int(row["KeyType"].ToString());
				eServiceLog.EServiceType  = (OpenDentBusiness.eServiceType)PIn.Int(row["EServiceType"].ToString());
				eServiceLog.EServiceAction= (OpenDentBusiness.eServiceAction)PIn.Int(row["EServiceAction"].ToString());
				eServiceLog.LogDateTime   = PIn.DateT (row["LogDateTime"].ToString());
				eServiceLog.PatNum        = PIn.Long  (row["PatNum"].ToString());
				eServiceLog.ClinicNum     = PIn.Long  (row["ClinicNum"].ToString());
				eServiceLog.LogGuid       = PIn.String(row["LogGuid"].ToString());
				eServiceLog.FKey          = PIn.Long  (row["FKey"].ToString());
				retVal.Add(eServiceLog);
			}
			return retVal;
		}

		///<summary>Converts a list of EServiceLog into a DataTable.</summary>
		public static DataTable ListToTable(List<EServiceLog> listEServiceLogs,string tableName="") {
			if(string.IsNullOrEmpty(tableName)) {
				tableName="EServiceLog";
			}
			DataTable table=new DataTable(tableName);
			table.Columns.Add("EServiceLogNum");
			table.Columns.Add("KeyType");
			table.Columns.Add("EServiceType");
			table.Columns.Add("EServiceAction");
			table.Columns.Add("LogDateTime");
			table.Columns.Add("PatNum");
			table.Columns.Add("ClinicNum");
			table.Columns.Add("LogGuid");
			table.Columns.Add("FKey");
			foreach(EServiceLog eServiceLog in listEServiceLogs) {
				table.Rows.Add(new object[] {
					POut.Long  (eServiceLog.EServiceLogNum),
					POut.Int   ((int)eServiceLog.KeyType),
					POut.Int   ((int)eServiceLog.EServiceType),
					POut.Int   ((int)eServiceLog.EServiceAction),
					POut.DateT (eServiceLog.LogDateTime,false),
					POut.Long  (eServiceLog.PatNum),
					POut.Long  (eServiceLog.ClinicNum),
					            eServiceLog.LogGuid,
					POut.Long  (eServiceLog.FKey),
				});
			}
			return table;
		}

		///<summary>Inserts one EServiceLog into the database.  Returns the new priKey.</summary>
		public static long Insert(EServiceLog eServiceLog) {
			return Insert(eServiceLog,false);
		}

		///<summary>Inserts one EServiceLog into the database.  Provides option to use the existing priKey.</summary>
		public static long Insert(EServiceLog eServiceLog,bool useExistingPK) {
			if(!useExistingPK && PrefC.RandomKeys) {
				eServiceLog.EServiceLogNum=ReplicationServers.GetKey("eservicelog","EServiceLogNum");
			}
			string command="INSERT INTO eservicelog (";
			if(useExistingPK || PrefC.RandomKeys) {
				command+="EServiceLogNum,";
			}
			command+="KeyType,EServiceType,EServiceAction,LogDateTime,PatNum,ClinicNum,LogGuid,FKey) VALUES(";
			if(useExistingPK || PrefC.RandomKeys) {
				command+=POut.Long(eServiceLog.EServiceLogNum)+",";
			}
			command+=
				     POut.Int   ((int)eServiceLog.KeyType)+","
				+    POut.Int   ((int)eServiceLog.EServiceType)+","
				+    POut.Int   ((int)eServiceLog.EServiceAction)+","
				+    DbHelper.Now()+","
				+    POut.Long  (eServiceLog.PatNum)+","
				+    POut.Long  (eServiceLog.ClinicNum)+","
				+"'"+POut.String(eServiceLog.LogGuid)+"',"
				+    POut.Long  (eServiceLog.FKey)+")";
			if(useExistingPK || PrefC.RandomKeys) {
				Db.NonQ(command);
			}
			else {
				eServiceLog.EServiceLogNum=Db.NonQ(command,true,"EServiceLogNum","eServiceLog");
			}
			return eServiceLog.EServiceLogNum;
		}

		///<summary>Inserts one EServiceLog into the database.  Returns the new priKey.  Doesn't use the cache.</summary>
		public static long InsertNoCache(EServiceLog eServiceLog) {
			return InsertNoCache(eServiceLog,false);
		}

		///<summary>Inserts one EServiceLog into the database.  Provides option to use the existing priKey.  Doesn't use the cache.</summary>
		public static long InsertNoCache(EServiceLog eServiceLog,bool useExistingPK) {
			bool isRandomKeys=Prefs.GetBoolNoCache(PrefName.RandomPrimaryKeys);
			string command="INSERT INTO eservicelog (";
			if(!useExistingPK && isRandomKeys) {
				eServiceLog.EServiceLogNum=ReplicationServers.GetKeyNoCache("eservicelog","EServiceLogNum");
			}
			if(isRandomKeys || useExistingPK) {
				command+="EServiceLogNum,";
			}
			command+="KeyType,EServiceType,EServiceAction,LogDateTime,PatNum,ClinicNum,LogGuid,FKey) VALUES(";
			if(isRandomKeys || useExistingPK) {
				command+=POut.Long(eServiceLog.EServiceLogNum)+",";
			}
			command+=
				     POut.Int   ((int)eServiceLog.KeyType)+","
				+    POut.Int   ((int)eServiceLog.EServiceType)+","
				+    POut.Int   ((int)eServiceLog.EServiceAction)+","
				+    DbHelper.Now()+","
				+    POut.Long  (eServiceLog.PatNum)+","
				+    POut.Long  (eServiceLog.ClinicNum)+","
				+"'"+POut.String(eServiceLog.LogGuid)+"',"
				+    POut.Long  (eServiceLog.FKey)+")";
			if(useExistingPK || isRandomKeys) {
				Db.NonQ(command);
			}
			else {
				eServiceLog.EServiceLogNum=Db.NonQ(command,true,"EServiceLogNum","eServiceLog");
			}
			return eServiceLog.EServiceLogNum;
		}

		///<summary>Updates one EServiceLog in the database.</summary>
		public static void Update(EServiceLog eServiceLog) {
			string command="UPDATE eservicelog SET "
				+"KeyType       =  "+POut.Int   ((int)eServiceLog.KeyType)+", "
				+"EServiceType  =  "+POut.Int   ((int)eServiceLog.EServiceType)+", "
				+"EServiceAction=  "+POut.Int   ((int)eServiceLog.EServiceAction)+", "
				//LogDateTime not allowed to change
				+"PatNum        =  "+POut.Long  (eServiceLog.PatNum)+", "
				+"ClinicNum     =  "+POut.Long  (eServiceLog.ClinicNum)+", "
				+"LogGuid       = '"+POut.String(eServiceLog.LogGuid)+"', "
				+"FKey          =  "+POut.Long  (eServiceLog.FKey)+" "
				+"WHERE EServiceLogNum = "+POut.Long(eServiceLog.EServiceLogNum);
			Db.NonQ(command);
		}

		///<summary>Updates one EServiceLog in the database.  Uses an old object to compare to, and only alters changed fields.  This prevents collisions and concurrency problems in heavily used tables.  Returns true if an update occurred.</summary>
		public static bool Update(EServiceLog eServiceLog,EServiceLog oldEServiceLog) {
			string command="";
			if(eServiceLog.KeyType != oldEServiceLog.KeyType) {
				if(command!="") { command+=",";}
				command+="KeyType = "+POut.Int   ((int)eServiceLog.KeyType)+"";
			}
			if(eServiceLog.EServiceType != oldEServiceLog.EServiceType) {
				if(command!="") { command+=",";}
				command+="EServiceType = "+POut.Int   ((int)eServiceLog.EServiceType)+"";
			}
			if(eServiceLog.EServiceAction != oldEServiceLog.EServiceAction) {
				if(command!="") { command+=",";}
				command+="EServiceAction = "+POut.Int   ((int)eServiceLog.EServiceAction)+"";
			}
			//LogDateTime not allowed to change
			if(eServiceLog.PatNum != oldEServiceLog.PatNum) {
				if(command!="") { command+=",";}
				command+="PatNum = "+POut.Long(eServiceLog.PatNum)+"";
			}
			if(eServiceLog.ClinicNum != oldEServiceLog.ClinicNum) {
				if(command!="") { command+=",";}
				command+="ClinicNum = "+POut.Long(eServiceLog.ClinicNum)+"";
			}
			if(eServiceLog.LogGuid != oldEServiceLog.LogGuid) {
				if(command!="") { command+=",";}
				command+="LogGuid = '"+POut.String(eServiceLog.LogGuid)+"'";
			}
			if(eServiceLog.FKey != oldEServiceLog.FKey) {
				if(command!="") { command+=",";}
				command+="FKey = "+POut.Long(eServiceLog.FKey)+"";
			}
			if(command=="") {
				return false;
			}
			command="UPDATE eservicelog SET "+command
				+" WHERE EServiceLogNum = "+POut.Long(eServiceLog.EServiceLogNum);
			Db.NonQ(command);
			return true;
		}

		///<summary>Returns true if Update(EServiceLog,EServiceLog) would make changes to the database.
		///Does not make any changes to the database and can be called before remoting role is checked.</summary>
		public static bool UpdateComparison(EServiceLog eServiceLog,EServiceLog oldEServiceLog) {
			if(eServiceLog.KeyType != oldEServiceLog.KeyType) {
				return true;
			}
			if(eServiceLog.EServiceType != oldEServiceLog.EServiceType) {
				return true;
			}
			if(eServiceLog.EServiceAction != oldEServiceLog.EServiceAction) {
				return true;
			}
			//LogDateTime not allowed to change
			if(eServiceLog.PatNum != oldEServiceLog.PatNum) {
				return true;
			}
			if(eServiceLog.ClinicNum != oldEServiceLog.ClinicNum) {
				return true;
			}
			if(eServiceLog.LogGuid != oldEServiceLog.LogGuid) {
				return true;
			}
			if(eServiceLog.FKey != oldEServiceLog.FKey) {
				return true;
			}
			return false;
		}

		///<summary>Deletes one EServiceLog from the database.</summary>
		public static void Delete(long eServiceLogNum) {
			string command="DELETE FROM eservicelog "
				+"WHERE EServiceLogNum = "+POut.Long(eServiceLogNum);
			Db.NonQ(command);
		}

		///<summary>Deletes many EServiceLogs from the database.</summary>
		public static void DeleteMany(List<long> listEServiceLogNums) {
			if(listEServiceLogNums==null || listEServiceLogNums.Count==0) {
				return;
			}
			string command="DELETE FROM eservicelog "
				+"WHERE EServiceLogNum IN("+string.Join(",",listEServiceLogNums.Select(x => POut.Long(x)))+")";
			Db.NonQ(command);
		}

	}
}