//This file is automatically generated.
//Do not attempt to make changes to this file because the changes will be erased and overwritten.
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;

namespace OpenDentBusiness.Crud{
	public class FeeSchedGroupCrud {
		///<summary>Gets one FeeSchedGroup object from the database using the primary key.  Returns null if not found.</summary>
		public static FeeSchedGroup SelectOne(long feeSchedGroupNum) {
			string command="SELECT * FROM feeschedgroup "
				+"WHERE FeeSchedGroupNum = "+POut.Long(feeSchedGroupNum);
			List<FeeSchedGroup> list=TableToList(Db.GetTable(command));
			if(list.Count==0) {
				return null;
			}
			return list[0];
		}

		///<summary>Gets one FeeSchedGroup object from the database using a query.</summary>
		public static FeeSchedGroup SelectOne(string command) {
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				throw new ApplicationException("Not allowed to send sql directly.  Rewrite the calling class to not use this query:\r\n"+command);
			}
			List<FeeSchedGroup> list=TableToList(Db.GetTable(command));
			if(list.Count==0) {
				return null;
			}
			return list[0];
		}

		///<summary>Gets a list of FeeSchedGroup objects from the database using a query.</summary>
		public static List<FeeSchedGroup> SelectMany(string command) {
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				throw new ApplicationException("Not allowed to send sql directly.  Rewrite the calling class to not use this query:\r\n"+command);
			}
			List<FeeSchedGroup> list=TableToList(Db.GetTable(command));
			return list;
		}

		///<summary>Converts a DataTable to a list of objects.</summary>
		public static List<FeeSchedGroup> TableToList(DataTable table) {
			List<FeeSchedGroup> retVal=new List<FeeSchedGroup>();
			FeeSchedGroup feeSchedGroup;
			foreach(DataRow row in table.Rows) {
				feeSchedGroup=new FeeSchedGroup();
				feeSchedGroup.FeeSchedGroupNum= PIn.Long  (row["FeeSchedGroupNum"].ToString());
				feeSchedGroup.Description     = PIn.String(row["Description"].ToString());
				feeSchedGroup.FeeSchedNum     = PIn.Long  (row["FeeSchedNum"].ToString());
				feeSchedGroup.ClinicNums      = PIn.String(row["ClinicNums"].ToString());
				retVal.Add(feeSchedGroup);
			}
			return retVal;
		}

		///<summary>Converts a list of FeeSchedGroup into a DataTable.</summary>
		public static DataTable ListToTable(List<FeeSchedGroup> listFeeSchedGroups,string tableName="") {
			if(string.IsNullOrEmpty(tableName)) {
				tableName="FeeSchedGroup";
			}
			DataTable table=new DataTable(tableName);
			table.Columns.Add("FeeSchedGroupNum");
			table.Columns.Add("Description");
			table.Columns.Add("FeeSchedNum");
			table.Columns.Add("ClinicNums");
			foreach(FeeSchedGroup feeSchedGroup in listFeeSchedGroups) {
				table.Rows.Add(new object[] {
					POut.Long  (feeSchedGroup.FeeSchedGroupNum),
					            feeSchedGroup.Description,
					POut.Long  (feeSchedGroup.FeeSchedNum),
					            feeSchedGroup.ClinicNums,
				});
			}
			return table;
		}

		///<summary>Inserts one FeeSchedGroup into the database.  Returns the new priKey.</summary>
		public static long Insert(FeeSchedGroup feeSchedGroup) {
			return Insert(feeSchedGroup,false);
		}

		///<summary>Inserts one FeeSchedGroup into the database.  Provides option to use the existing priKey.</summary>
		public static long Insert(FeeSchedGroup feeSchedGroup,bool useExistingPK) {
			if(!useExistingPK && PrefC.RandomKeys) {
				feeSchedGroup.FeeSchedGroupNum=ReplicationServers.GetKey("feeschedgroup","FeeSchedGroupNum");
			}
			string command="INSERT INTO feeschedgroup (";
			if(useExistingPK || PrefC.RandomKeys) {
				command+="FeeSchedGroupNum,";
			}
			command+="Description,FeeSchedNum,ClinicNums) VALUES(";
			if(useExistingPK || PrefC.RandomKeys) {
				command+=POut.Long(feeSchedGroup.FeeSchedGroupNum)+",";
			}
			command+=
				 "'"+POut.String(feeSchedGroup.Description)+"',"
				+    POut.Long  (feeSchedGroup.FeeSchedNum)+","
				+"'"+POut.String(feeSchedGroup.ClinicNums)+"')";
			if(useExistingPK || PrefC.RandomKeys) {
				Db.NonQ(command);
			}
			else {
				feeSchedGroup.FeeSchedGroupNum=Db.NonQ(command,true,"FeeSchedGroupNum","feeSchedGroup");
			}
			return feeSchedGroup.FeeSchedGroupNum;
		}

		///<summary>Inserts one FeeSchedGroup into the database.  Returns the new priKey.  Doesn't use the cache.</summary>
		public static long InsertNoCache(FeeSchedGroup feeSchedGroup) {
			return InsertNoCache(feeSchedGroup,false);
		}

		///<summary>Inserts one FeeSchedGroup into the database.  Provides option to use the existing priKey.  Doesn't use the cache.</summary>
		public static long InsertNoCache(FeeSchedGroup feeSchedGroup,bool useExistingPK) {
			bool isRandomKeys=Prefs.GetBoolNoCache(PrefName.RandomPrimaryKeys);
			string command="INSERT INTO feeschedgroup (";
			if(!useExistingPK && isRandomKeys) {
				feeSchedGroup.FeeSchedGroupNum=ReplicationServers.GetKeyNoCache("feeschedgroup","FeeSchedGroupNum");
			}
			if(isRandomKeys || useExistingPK) {
				command+="FeeSchedGroupNum,";
			}
			command+="Description,FeeSchedNum,ClinicNums) VALUES(";
			if(isRandomKeys || useExistingPK) {
				command+=POut.Long(feeSchedGroup.FeeSchedGroupNum)+",";
			}
			command+=
				 "'"+POut.String(feeSchedGroup.Description)+"',"
				+    POut.Long  (feeSchedGroup.FeeSchedNum)+","
				+"'"+POut.String(feeSchedGroup.ClinicNums)+"')";
			if(useExistingPK || isRandomKeys) {
				Db.NonQ(command);
			}
			else {
				feeSchedGroup.FeeSchedGroupNum=Db.NonQ(command,true,"FeeSchedGroupNum","feeSchedGroup");
			}
			return feeSchedGroup.FeeSchedGroupNum;
		}

		///<summary>Updates one FeeSchedGroup in the database.</summary>
		public static void Update(FeeSchedGroup feeSchedGroup) {
			string command="UPDATE feeschedgroup SET "
				+"Description     = '"+POut.String(feeSchedGroup.Description)+"', "
				+"FeeSchedNum     =  "+POut.Long  (feeSchedGroup.FeeSchedNum)+", "
				+"ClinicNums      = '"+POut.String(feeSchedGroup.ClinicNums)+"' "
				+"WHERE FeeSchedGroupNum = "+POut.Long(feeSchedGroup.FeeSchedGroupNum);
			Db.NonQ(command);
		}

		///<summary>Updates one FeeSchedGroup in the database.  Uses an old object to compare to, and only alters changed fields.  This prevents collisions and concurrency problems in heavily used tables.  Returns true if an update occurred.</summary>
		public static bool Update(FeeSchedGroup feeSchedGroup,FeeSchedGroup oldFeeSchedGroup) {
			string command="";
			if(feeSchedGroup.Description != oldFeeSchedGroup.Description) {
				if(command!="") { command+=",";}
				command+="Description = '"+POut.String(feeSchedGroup.Description)+"'";
			}
			if(feeSchedGroup.FeeSchedNum != oldFeeSchedGroup.FeeSchedNum) {
				if(command!="") { command+=",";}
				command+="FeeSchedNum = "+POut.Long(feeSchedGroup.FeeSchedNum)+"";
			}
			if(feeSchedGroup.ClinicNums != oldFeeSchedGroup.ClinicNums) {
				if(command!="") { command+=",";}
				command+="ClinicNums = '"+POut.String(feeSchedGroup.ClinicNums)+"'";
			}
			if(command=="") {
				return false;
			}
			command="UPDATE feeschedgroup SET "+command
				+" WHERE FeeSchedGroupNum = "+POut.Long(feeSchedGroup.FeeSchedGroupNum);
			Db.NonQ(command);
			return true;
		}

		///<summary>Returns true if Update(FeeSchedGroup,FeeSchedGroup) would make changes to the database.
		///Does not make any changes to the database and can be called before remoting role is checked.</summary>
		public static bool UpdateComparison(FeeSchedGroup feeSchedGroup,FeeSchedGroup oldFeeSchedGroup) {
			if(feeSchedGroup.Description != oldFeeSchedGroup.Description) {
				return true;
			}
			if(feeSchedGroup.FeeSchedNum != oldFeeSchedGroup.FeeSchedNum) {
				return true;
			}
			if(feeSchedGroup.ClinicNums != oldFeeSchedGroup.ClinicNums) {
				return true;
			}
			return false;
		}

		///<summary>Deletes one FeeSchedGroup from the database.</summary>
		public static void Delete(long feeSchedGroupNum) {
			string command="DELETE FROM feeschedgroup "
				+"WHERE FeeSchedGroupNum = "+POut.Long(feeSchedGroupNum);
			Db.NonQ(command);
		}

		///<summary>Deletes many FeeSchedGroups from the database.</summary>
		public static void DeleteMany(List<long> listFeeSchedGroupNums) {
			if(listFeeSchedGroupNums==null || listFeeSchedGroupNums.Count==0) {
				return;
			}
			string command="DELETE FROM feeschedgroup "
				+"WHERE FeeSchedGroupNum IN("+string.Join(",",listFeeSchedGroupNums.Select(x => POut.Long(x)))+")";
			Db.NonQ(command);
		}

	}
}