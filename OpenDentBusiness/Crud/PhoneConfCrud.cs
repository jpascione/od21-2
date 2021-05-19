//This file is automatically generated.
//Do not attempt to make changes to this file because the changes will be erased and overwritten.
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;

namespace OpenDentBusiness.Crud{
	public class PhoneConfCrud {
		///<summary>Gets one PhoneConf object from the database using the primary key.  Returns null if not found.</summary>
		public static PhoneConf SelectOne(long phoneConfNum) {
			string command="SELECT * FROM phoneconf "
				+"WHERE PhoneConfNum = "+POut.Long(phoneConfNum);
			List<PhoneConf> list=TableToList(Db.GetTable(command));
			if(list.Count==0) {
				return null;
			}
			return list[0];
		}

		///<summary>Gets one PhoneConf object from the database using a query.</summary>
		public static PhoneConf SelectOne(string command) {
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				throw new ApplicationException("Not allowed to send sql directly.  Rewrite the calling class to not use this query:\r\n"+command);
			}
			List<PhoneConf> list=TableToList(Db.GetTable(command));
			if(list.Count==0) {
				return null;
			}
			return list[0];
		}

		///<summary>Gets a list of PhoneConf objects from the database using a query.</summary>
		public static List<PhoneConf> SelectMany(string command) {
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				throw new ApplicationException("Not allowed to send sql directly.  Rewrite the calling class to not use this query:\r\n"+command);
			}
			List<PhoneConf> list=TableToList(Db.GetTable(command));
			return list;
		}

		///<summary>Converts a DataTable to a list of objects.</summary>
		public static List<PhoneConf> TableToList(DataTable table) {
			List<PhoneConf> retVal=new List<PhoneConf>();
			PhoneConf phoneConf;
			foreach(DataRow row in table.Rows) {
				phoneConf=new PhoneConf();
				phoneConf.PhoneConfNum    = PIn.Long  (row["PhoneConfNum"].ToString());
				phoneConf.ButtonIndex     = PIn.Int   (row["ButtonIndex"].ToString());
				phoneConf.Occupants       = PIn.Int   (row["Occupants"].ToString());
				phoneConf.Extension       = PIn.Int   (row["Extension"].ToString());
				phoneConf.DateTimeReserved= PIn.DateT (row["DateTimeReserved"].ToString());
				phoneConf.UserNum         = PIn.Long  (row["UserNum"].ToString());
				phoneConf.SiteNum         = PIn.Long  (row["SiteNum"].ToString());
				retVal.Add(phoneConf);
			}
			return retVal;
		}

		///<summary>Converts a list of PhoneConf into a DataTable.</summary>
		public static DataTable ListToTable(List<PhoneConf> listPhoneConfs,string tableName="") {
			if(string.IsNullOrEmpty(tableName)) {
				tableName="PhoneConf";
			}
			DataTable table=new DataTable(tableName);
			table.Columns.Add("PhoneConfNum");
			table.Columns.Add("ButtonIndex");
			table.Columns.Add("Occupants");
			table.Columns.Add("Extension");
			table.Columns.Add("DateTimeReserved");
			table.Columns.Add("UserNum");
			table.Columns.Add("SiteNum");
			foreach(PhoneConf phoneConf in listPhoneConfs) {
				table.Rows.Add(new object[] {
					POut.Long  (phoneConf.PhoneConfNum),
					POut.Int   (phoneConf.ButtonIndex),
					POut.Int   (phoneConf.Occupants),
					POut.Int   (phoneConf.Extension),
					POut.DateT (phoneConf.DateTimeReserved,false),
					POut.Long  (phoneConf.UserNum),
					POut.Long  (phoneConf.SiteNum),
				});
			}
			return table;
		}

		///<summary>Inserts one PhoneConf into the database.  Returns the new priKey.</summary>
		public static long Insert(PhoneConf phoneConf) {
			return Insert(phoneConf,false);
		}

		///<summary>Inserts one PhoneConf into the database.  Provides option to use the existing priKey.</summary>
		public static long Insert(PhoneConf phoneConf,bool useExistingPK) {
			if(!useExistingPK && PrefC.RandomKeys) {
				phoneConf.PhoneConfNum=ReplicationServers.GetKey("phoneconf","PhoneConfNum");
			}
			string command="INSERT INTO phoneconf (";
			if(useExistingPK || PrefC.RandomKeys) {
				command+="PhoneConfNum,";
			}
			command+="ButtonIndex,Occupants,Extension,DateTimeReserved,UserNum,SiteNum) VALUES(";
			if(useExistingPK || PrefC.RandomKeys) {
				command+=POut.Long(phoneConf.PhoneConfNum)+",";
			}
			command+=
				     POut.Int   (phoneConf.ButtonIndex)+","
				+    POut.Int   (phoneConf.Occupants)+","
				+    POut.Int   (phoneConf.Extension)+","
				+    POut.DateT (phoneConf.DateTimeReserved)+","
				+    POut.Long  (phoneConf.UserNum)+","
				+    POut.Long  (phoneConf.SiteNum)+")";
			if(useExistingPK || PrefC.RandomKeys) {
				Db.NonQ(command);
			}
			else {
				phoneConf.PhoneConfNum=Db.NonQ(command,true,"PhoneConfNum","phoneConf");
			}
			return phoneConf.PhoneConfNum;
		}

		///<summary>Inserts one PhoneConf into the database.  Returns the new priKey.  Doesn't use the cache.</summary>
		public static long InsertNoCache(PhoneConf phoneConf) {
			return InsertNoCache(phoneConf,false);
		}

		///<summary>Inserts one PhoneConf into the database.  Provides option to use the existing priKey.  Doesn't use the cache.</summary>
		public static long InsertNoCache(PhoneConf phoneConf,bool useExistingPK) {
			bool isRandomKeys=Prefs.GetBoolNoCache(PrefName.RandomPrimaryKeys);
			string command="INSERT INTO phoneconf (";
			if(!useExistingPK && isRandomKeys) {
				phoneConf.PhoneConfNum=ReplicationServers.GetKeyNoCache("phoneconf","PhoneConfNum");
			}
			if(isRandomKeys || useExistingPK) {
				command+="PhoneConfNum,";
			}
			command+="ButtonIndex,Occupants,Extension,DateTimeReserved,UserNum,SiteNum) VALUES(";
			if(isRandomKeys || useExistingPK) {
				command+=POut.Long(phoneConf.PhoneConfNum)+",";
			}
			command+=
				     POut.Int   (phoneConf.ButtonIndex)+","
				+    POut.Int   (phoneConf.Occupants)+","
				+    POut.Int   (phoneConf.Extension)+","
				+    POut.DateT (phoneConf.DateTimeReserved)+","
				+    POut.Long  (phoneConf.UserNum)+","
				+    POut.Long  (phoneConf.SiteNum)+")";
			if(useExistingPK || isRandomKeys) {
				Db.NonQ(command);
			}
			else {
				phoneConf.PhoneConfNum=Db.NonQ(command,true,"PhoneConfNum","phoneConf");
			}
			return phoneConf.PhoneConfNum;
		}

		///<summary>Updates one PhoneConf in the database.</summary>
		public static void Update(PhoneConf phoneConf) {
			string command="UPDATE phoneconf SET "
				+"ButtonIndex     =  "+POut.Int   (phoneConf.ButtonIndex)+", "
				+"Occupants       =  "+POut.Int   (phoneConf.Occupants)+", "
				+"Extension       =  "+POut.Int   (phoneConf.Extension)+", "
				+"DateTimeReserved=  "+POut.DateT (phoneConf.DateTimeReserved)+", "
				+"UserNum         =  "+POut.Long  (phoneConf.UserNum)+", "
				+"SiteNum         =  "+POut.Long  (phoneConf.SiteNum)+" "
				+"WHERE PhoneConfNum = "+POut.Long(phoneConf.PhoneConfNum);
			Db.NonQ(command);
		}

		///<summary>Updates one PhoneConf in the database.  Uses an old object to compare to, and only alters changed fields.  This prevents collisions and concurrency problems in heavily used tables.  Returns true if an update occurred.</summary>
		public static bool Update(PhoneConf phoneConf,PhoneConf oldPhoneConf) {
			string command="";
			if(phoneConf.ButtonIndex != oldPhoneConf.ButtonIndex) {
				if(command!="") { command+=",";}
				command+="ButtonIndex = "+POut.Int(phoneConf.ButtonIndex)+"";
			}
			if(phoneConf.Occupants != oldPhoneConf.Occupants) {
				if(command!="") { command+=",";}
				command+="Occupants = "+POut.Int(phoneConf.Occupants)+"";
			}
			if(phoneConf.Extension != oldPhoneConf.Extension) {
				if(command!="") { command+=",";}
				command+="Extension = "+POut.Int(phoneConf.Extension)+"";
			}
			if(phoneConf.DateTimeReserved != oldPhoneConf.DateTimeReserved) {
				if(command!="") { command+=",";}
				command+="DateTimeReserved = "+POut.DateT(phoneConf.DateTimeReserved)+"";
			}
			if(phoneConf.UserNum != oldPhoneConf.UserNum) {
				if(command!="") { command+=",";}
				command+="UserNum = "+POut.Long(phoneConf.UserNum)+"";
			}
			if(phoneConf.SiteNum != oldPhoneConf.SiteNum) {
				if(command!="") { command+=",";}
				command+="SiteNum = "+POut.Long(phoneConf.SiteNum)+"";
			}
			if(command=="") {
				return false;
			}
			command="UPDATE phoneconf SET "+command
				+" WHERE PhoneConfNum = "+POut.Long(phoneConf.PhoneConfNum);
			Db.NonQ(command);
			return true;
		}

		///<summary>Returns true if Update(PhoneConf,PhoneConf) would make changes to the database.
		///Does not make any changes to the database and can be called before remoting role is checked.</summary>
		public static bool UpdateComparison(PhoneConf phoneConf,PhoneConf oldPhoneConf) {
			if(phoneConf.ButtonIndex != oldPhoneConf.ButtonIndex) {
				return true;
			}
			if(phoneConf.Occupants != oldPhoneConf.Occupants) {
				return true;
			}
			if(phoneConf.Extension != oldPhoneConf.Extension) {
				return true;
			}
			if(phoneConf.DateTimeReserved != oldPhoneConf.DateTimeReserved) {
				return true;
			}
			if(phoneConf.UserNum != oldPhoneConf.UserNum) {
				return true;
			}
			if(phoneConf.SiteNum != oldPhoneConf.SiteNum) {
				return true;
			}
			return false;
		}

		///<summary>Deletes one PhoneConf from the database.</summary>
		public static void Delete(long phoneConfNum) {
			string command="DELETE FROM phoneconf "
				+"WHERE PhoneConfNum = "+POut.Long(phoneConfNum);
			Db.NonQ(command);
		}

		///<summary>Deletes many PhoneConfs from the database.</summary>
		public static void DeleteMany(List<long> listPhoneConfNums) {
			if(listPhoneConfNums==null || listPhoneConfNums.Count==0) {
				return;
			}
			string command="DELETE FROM phoneconf "
				+"WHERE PhoneConfNum IN("+string.Join(",",listPhoneConfNums.Select(x => POut.Long(x)))+")";
			Db.NonQ(command);
		}

	}
}