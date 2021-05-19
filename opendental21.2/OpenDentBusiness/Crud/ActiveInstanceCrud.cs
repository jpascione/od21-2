//This file is automatically generated.
//Do not attempt to make changes to this file because the changes will be erased and overwritten.
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;

namespace OpenDentBusiness.Crud{
	public class ActiveInstanceCrud {
		///<summary>Gets one ActiveInstance object from the database using the primary key.  Returns null if not found.</summary>
		public static ActiveInstance SelectOne(long activeInstanceNum) {
			string command="SELECT * FROM activeinstance "
				+"WHERE ActiveInstanceNum = "+POut.Long(activeInstanceNum);
			List<ActiveInstance> list=TableToList(Db.GetTable(command));
			if(list.Count==0) {
				return null;
			}
			return list[0];
		}

		///<summary>Gets one ActiveInstance object from the database using a query.</summary>
		public static ActiveInstance SelectOne(string command) {
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				throw new ApplicationException("Not allowed to send sql directly.  Rewrite the calling class to not use this query:\r\n"+command);
			}
			List<ActiveInstance> list=TableToList(Db.GetTable(command));
			if(list.Count==0) {
				return null;
			}
			return list[0];
		}

		///<summary>Gets a list of ActiveInstance objects from the database using a query.</summary>
		public static List<ActiveInstance> SelectMany(string command) {
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				throw new ApplicationException("Not allowed to send sql directly.  Rewrite the calling class to not use this query:\r\n"+command);
			}
			List<ActiveInstance> list=TableToList(Db.GetTable(command));
			return list;
		}

		///<summary>Converts a DataTable to a list of objects.</summary>
		public static List<ActiveInstance> TableToList(DataTable table) {
			List<ActiveInstance> retVal=new List<ActiveInstance>();
			ActiveInstance activeInstance;
			foreach(DataRow row in table.Rows) {
				activeInstance=new ActiveInstance();
				activeInstance.ActiveInstanceNum = PIn.Long  (row["ActiveInstanceNum"].ToString());
				activeInstance.ComputerNum       = PIn.Long  (row["ComputerNum"].ToString());
				activeInstance.UserNum           = PIn.Long  (row["UserNum"].ToString());
				activeInstance.ProcessId         = PIn.Long  (row["ProcessId"].ToString());
				activeInstance.DateTimeLastActive= PIn.DateT (row["DateTimeLastActive"].ToString());
				activeInstance.DateTRecorded     = PIn.DateT (row["DateTRecorded"].ToString());
				activeInstance.ConnectionType    = (OpenDentBusiness.ConnectionTypes)PIn.Int(row["ConnectionType"].ToString());
				retVal.Add(activeInstance);
			}
			return retVal;
		}

		///<summary>Converts a list of ActiveInstance into a DataTable.</summary>
		public static DataTable ListToTable(List<ActiveInstance> listActiveInstances,string tableName="") {
			if(string.IsNullOrEmpty(tableName)) {
				tableName="ActiveInstance";
			}
			DataTable table=new DataTable(tableName);
			table.Columns.Add("ActiveInstanceNum");
			table.Columns.Add("ComputerNum");
			table.Columns.Add("UserNum");
			table.Columns.Add("ProcessId");
			table.Columns.Add("DateTimeLastActive");
			table.Columns.Add("DateTRecorded");
			table.Columns.Add("ConnectionType");
			foreach(ActiveInstance activeInstance in listActiveInstances) {
				table.Rows.Add(new object[] {
					POut.Long  (activeInstance.ActiveInstanceNum),
					POut.Long  (activeInstance.ComputerNum),
					POut.Long  (activeInstance.UserNum),
					POut.Long  (activeInstance.ProcessId),
					POut.DateT (activeInstance.DateTimeLastActive,false),
					POut.DateT (activeInstance.DateTRecorded,false),
					POut.Int   ((int)activeInstance.ConnectionType),
				});
			}
			return table;
		}

		///<summary>Inserts one ActiveInstance into the database.  Returns the new priKey.</summary>
		public static long Insert(ActiveInstance activeInstance) {
			return Insert(activeInstance,false);
		}

		///<summary>Inserts one ActiveInstance into the database.  Provides option to use the existing priKey.</summary>
		public static long Insert(ActiveInstance activeInstance,bool useExistingPK) {
			if(!useExistingPK && PrefC.RandomKeys) {
				activeInstance.ActiveInstanceNum=ReplicationServers.GetKey("activeinstance","ActiveInstanceNum");
			}
			string command="INSERT INTO activeinstance (";
			if(useExistingPK || PrefC.RandomKeys) {
				command+="ActiveInstanceNum,";
			}
			command+="ComputerNum,UserNum,ProcessId,DateTimeLastActive,DateTRecorded,ConnectionType) VALUES(";
			if(useExistingPK || PrefC.RandomKeys) {
				command+=POut.Long(activeInstance.ActiveInstanceNum)+",";
			}
			command+=
				     POut.Long  (activeInstance.ComputerNum)+","
				+    POut.Long  (activeInstance.UserNum)+","
				+    POut.Long  (activeInstance.ProcessId)+","
				+    POut.DateT (activeInstance.DateTimeLastActive)+","
				+    POut.DateT (activeInstance.DateTRecorded)+","
				+    POut.Int   ((int)activeInstance.ConnectionType)+")";
			if(useExistingPK || PrefC.RandomKeys) {
				Db.NonQ(command);
			}
			else {
				activeInstance.ActiveInstanceNum=Db.NonQ(command,true,"ActiveInstanceNum","activeInstance");
			}
			return activeInstance.ActiveInstanceNum;
		}

		///<summary>Inserts one ActiveInstance into the database.  Returns the new priKey.  Doesn't use the cache.</summary>
		public static long InsertNoCache(ActiveInstance activeInstance) {
			return InsertNoCache(activeInstance,false);
		}

		///<summary>Inserts one ActiveInstance into the database.  Provides option to use the existing priKey.  Doesn't use the cache.</summary>
		public static long InsertNoCache(ActiveInstance activeInstance,bool useExistingPK) {
			bool isRandomKeys=Prefs.GetBoolNoCache(PrefName.RandomPrimaryKeys);
			string command="INSERT INTO activeinstance (";
			if(!useExistingPK && isRandomKeys) {
				activeInstance.ActiveInstanceNum=ReplicationServers.GetKeyNoCache("activeinstance","ActiveInstanceNum");
			}
			if(isRandomKeys || useExistingPK) {
				command+="ActiveInstanceNum,";
			}
			command+="ComputerNum,UserNum,ProcessId,DateTimeLastActive,DateTRecorded,ConnectionType) VALUES(";
			if(isRandomKeys || useExistingPK) {
				command+=POut.Long(activeInstance.ActiveInstanceNum)+",";
			}
			command+=
				     POut.Long  (activeInstance.ComputerNum)+","
				+    POut.Long  (activeInstance.UserNum)+","
				+    POut.Long  (activeInstance.ProcessId)+","
				+    POut.DateT (activeInstance.DateTimeLastActive)+","
				+    POut.DateT (activeInstance.DateTRecorded)+","
				+    POut.Int   ((int)activeInstance.ConnectionType)+")";
			if(useExistingPK || isRandomKeys) {
				Db.NonQ(command);
			}
			else {
				activeInstance.ActiveInstanceNum=Db.NonQ(command,true,"ActiveInstanceNum","activeInstance");
			}
			return activeInstance.ActiveInstanceNum;
		}

		///<summary>Updates one ActiveInstance in the database.</summary>
		public static void Update(ActiveInstance activeInstance) {
			string command="UPDATE activeinstance SET "
				+"ComputerNum       =  "+POut.Long  (activeInstance.ComputerNum)+", "
				+"UserNum           =  "+POut.Long  (activeInstance.UserNum)+", "
				+"ProcessId         =  "+POut.Long  (activeInstance.ProcessId)+", "
				+"DateTimeLastActive=  "+POut.DateT (activeInstance.DateTimeLastActive)+", "
				+"DateTRecorded     =  "+POut.DateT (activeInstance.DateTRecorded)+", "
				+"ConnectionType    =  "+POut.Int   ((int)activeInstance.ConnectionType)+" "
				+"WHERE ActiveInstanceNum = "+POut.Long(activeInstance.ActiveInstanceNum);
			Db.NonQ(command);
		}

		///<summary>Updates one ActiveInstance in the database.  Uses an old object to compare to, and only alters changed fields.  This prevents collisions and concurrency problems in heavily used tables.  Returns true if an update occurred.</summary>
		public static bool Update(ActiveInstance activeInstance,ActiveInstance oldActiveInstance) {
			string command="";
			if(activeInstance.ComputerNum != oldActiveInstance.ComputerNum) {
				if(command!="") { command+=",";}
				command+="ComputerNum = "+POut.Long(activeInstance.ComputerNum)+"";
			}
			if(activeInstance.UserNum != oldActiveInstance.UserNum) {
				if(command!="") { command+=",";}
				command+="UserNum = "+POut.Long(activeInstance.UserNum)+"";
			}
			if(activeInstance.ProcessId != oldActiveInstance.ProcessId) {
				if(command!="") { command+=",";}
				command+="ProcessId = "+POut.Long(activeInstance.ProcessId)+"";
			}
			if(activeInstance.DateTimeLastActive != oldActiveInstance.DateTimeLastActive) {
				if(command!="") { command+=",";}
				command+="DateTimeLastActive = "+POut.DateT(activeInstance.DateTimeLastActive)+"";
			}
			if(activeInstance.DateTRecorded != oldActiveInstance.DateTRecorded) {
				if(command!="") { command+=",";}
				command+="DateTRecorded = "+POut.DateT(activeInstance.DateTRecorded)+"";
			}
			if(activeInstance.ConnectionType != oldActiveInstance.ConnectionType) {
				if(command!="") { command+=",";}
				command+="ConnectionType = "+POut.Int   ((int)activeInstance.ConnectionType)+"";
			}
			if(command=="") {
				return false;
			}
			command="UPDATE activeinstance SET "+command
				+" WHERE ActiveInstanceNum = "+POut.Long(activeInstance.ActiveInstanceNum);
			Db.NonQ(command);
			return true;
		}

		///<summary>Returns true if Update(ActiveInstance,ActiveInstance) would make changes to the database.
		///Does not make any changes to the database and can be called before remoting role is checked.</summary>
		public static bool UpdateComparison(ActiveInstance activeInstance,ActiveInstance oldActiveInstance) {
			if(activeInstance.ComputerNum != oldActiveInstance.ComputerNum) {
				return true;
			}
			if(activeInstance.UserNum != oldActiveInstance.UserNum) {
				return true;
			}
			if(activeInstance.ProcessId != oldActiveInstance.ProcessId) {
				return true;
			}
			if(activeInstance.DateTimeLastActive != oldActiveInstance.DateTimeLastActive) {
				return true;
			}
			if(activeInstance.DateTRecorded != oldActiveInstance.DateTRecorded) {
				return true;
			}
			if(activeInstance.ConnectionType != oldActiveInstance.ConnectionType) {
				return true;
			}
			return false;
		}

		///<summary>Deletes one ActiveInstance from the database.</summary>
		public static void Delete(long activeInstanceNum) {
			string command="DELETE FROM activeinstance "
				+"WHERE ActiveInstanceNum = "+POut.Long(activeInstanceNum);
			Db.NonQ(command);
		}

		///<summary>Deletes many ActiveInstances from the database.</summary>
		public static void DeleteMany(List<long> listActiveInstanceNums) {
			if(listActiveInstanceNums==null || listActiveInstanceNums.Count==0) {
				return;
			}
			string command="DELETE FROM activeinstance "
				+"WHERE ActiveInstanceNum IN("+string.Join(",",listActiveInstanceNums.Select(x => POut.Long(x)))+")";
			Db.NonQ(command);
		}

	}
}