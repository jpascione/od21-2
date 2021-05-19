//This file is automatically generated.
//Do not attempt to make changes to this file because the changes will be erased and overwritten.
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;

namespace OpenDentBusiness.Crud{
	public class UserodCrud {
		///<summary>Gets one Userod object from the database using the primary key.  Returns null if not found.</summary>
		public static Userod SelectOne(long userNum) {
			string command="SELECT * FROM userod "
				+"WHERE UserNum = "+POut.Long(userNum);
			List<Userod> list=TableToList(Db.GetTable(command));
			if(list.Count==0) {
				return null;
			}
			return list[0];
		}

		///<summary>Gets one Userod object from the database using a query.</summary>
		public static Userod SelectOne(string command) {
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				throw new ApplicationException("Not allowed to send sql directly.  Rewrite the calling class to not use this query:\r\n"+command);
			}
			List<Userod> list=TableToList(Db.GetTable(command));
			if(list.Count==0) {
				return null;
			}
			return list[0];
		}

		///<summary>Gets a list of Userod objects from the database using a query.</summary>
		public static List<Userod> SelectMany(string command) {
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				throw new ApplicationException("Not allowed to send sql directly.  Rewrite the calling class to not use this query:\r\n"+command);
			}
			List<Userod> list=TableToList(Db.GetTable(command));
			return list;
		}

		///<summary>Converts a DataTable to a list of objects.</summary>
		public static List<Userod> TableToList(DataTable table) {
			List<Userod> retVal=new List<Userod>();
			Userod userod;
			foreach(DataRow row in table.Rows) {
				userod=new Userod();
				userod.UserNum                   = PIn.Long  (row["UserNum"].ToString());
				userod.UserName                  = PIn.String(row["UserName"].ToString());
				userod.Password                  = PIn.String(row["Password"].ToString());
				userod.UserGroupNum              = PIn.Long  (row["UserGroupNum"].ToString());
				userod.EmployeeNum               = PIn.Long  (row["EmployeeNum"].ToString());
				userod.ClinicNum                 = PIn.Long  (row["ClinicNum"].ToString());
				userod.ProvNum                   = PIn.Long  (row["ProvNum"].ToString());
				userod.IsHidden                  = PIn.Bool  (row["IsHidden"].ToString());
				userod.TaskListInBox             = PIn.Long  (row["TaskListInBox"].ToString());
				userod.AnesthProvType            = PIn.Int   (row["AnesthProvType"].ToString());
				userod.DefaultHidePopups         = PIn.Bool  (row["DefaultHidePopups"].ToString());
				userod.PasswordIsStrong          = PIn.Bool  (row["PasswordIsStrong"].ToString());
				userod.ClinicIsRestricted        = PIn.Bool  (row["ClinicIsRestricted"].ToString());
				userod.InboxHidePopups           = PIn.Bool  (row["InboxHidePopups"].ToString());
				userod.UserNumCEMT               = PIn.Long  (row["UserNumCEMT"].ToString());
				userod.DateTFail                 = PIn.DateT (row["DateTFail"].ToString());
				userod.FailedAttempts            = PIn.Byte  (row["FailedAttempts"].ToString());
				userod.DomainUser                = PIn.String(row["DomainUser"].ToString());
				userod.IsPasswordResetRequired   = PIn.Bool  (row["IsPasswordResetRequired"].ToString());
				userod.MobileWebPin              = PIn.String(row["MobileWebPin"].ToString());
				userod.MobileWebPinFailedAttempts= PIn.Byte  (row["MobileWebPinFailedAttempts"].ToString());
				userod.DateTLastLogin            = PIn.DateT (row["DateTLastLogin"].ToString());
				retVal.Add(userod);
			}
			return retVal;
		}

		///<summary>Converts a list of Userod into a DataTable.</summary>
		public static DataTable ListToTable(List<Userod> listUserods,string tableName="") {
			if(string.IsNullOrEmpty(tableName)) {
				tableName="Userod";
			}
			DataTable table=new DataTable(tableName);
			table.Columns.Add("UserNum");
			table.Columns.Add("UserName");
			table.Columns.Add("Password");
			table.Columns.Add("UserGroupNum");
			table.Columns.Add("EmployeeNum");
			table.Columns.Add("ClinicNum");
			table.Columns.Add("ProvNum");
			table.Columns.Add("IsHidden");
			table.Columns.Add("TaskListInBox");
			table.Columns.Add("AnesthProvType");
			table.Columns.Add("DefaultHidePopups");
			table.Columns.Add("PasswordIsStrong");
			table.Columns.Add("ClinicIsRestricted");
			table.Columns.Add("InboxHidePopups");
			table.Columns.Add("UserNumCEMT");
			table.Columns.Add("DateTFail");
			table.Columns.Add("FailedAttempts");
			table.Columns.Add("DomainUser");
			table.Columns.Add("IsPasswordResetRequired");
			table.Columns.Add("MobileWebPin");
			table.Columns.Add("MobileWebPinFailedAttempts");
			table.Columns.Add("DateTLastLogin");
			foreach(Userod userod in listUserods) {
				table.Rows.Add(new object[] {
					POut.Long  (userod.UserNum),
					            userod.UserName,
					            userod.Password,
					POut.Long  (userod.UserGroupNum),
					POut.Long  (userod.EmployeeNum),
					POut.Long  (userod.ClinicNum),
					POut.Long  (userod.ProvNum),
					POut.Bool  (userod.IsHidden),
					POut.Long  (userod.TaskListInBox),
					POut.Int   (userod.AnesthProvType),
					POut.Bool  (userod.DefaultHidePopups),
					POut.Bool  (userod.PasswordIsStrong),
					POut.Bool  (userod.ClinicIsRestricted),
					POut.Bool  (userod.InboxHidePopups),
					POut.Long  (userod.UserNumCEMT),
					POut.DateT (userod.DateTFail,false),
					POut.Byte  (userod.FailedAttempts),
					            userod.DomainUser,
					POut.Bool  (userod.IsPasswordResetRequired),
					            userod.MobileWebPin,
					POut.Byte  (userod.MobileWebPinFailedAttempts),
					POut.DateT (userod.DateTLastLogin,false),
				});
			}
			return table;
		}

		///<summary>Inserts one Userod into the database.  Returns the new priKey.</summary>
		public static long Insert(Userod userod) {
			return Insert(userod,false);
		}

		///<summary>Inserts one Userod into the database.  Provides option to use the existing priKey.</summary>
		public static long Insert(Userod userod,bool useExistingPK) {
			if(!useExistingPK && PrefC.RandomKeys) {
				userod.UserNum=ReplicationServers.GetKey("userod","UserNum");
			}
			string command="INSERT INTO userod (";
			if(useExistingPK || PrefC.RandomKeys) {
				command+="UserNum,";
			}
			command+="UserName,Password,UserGroupNum,EmployeeNum,ClinicNum,ProvNum,IsHidden,TaskListInBox,AnesthProvType,DefaultHidePopups,PasswordIsStrong,ClinicIsRestricted,InboxHidePopups,UserNumCEMT,DateTFail,FailedAttempts,DomainUser,IsPasswordResetRequired,MobileWebPin,MobileWebPinFailedAttempts,DateTLastLogin) VALUES(";
			if(useExistingPK || PrefC.RandomKeys) {
				command+=POut.Long(userod.UserNum)+",";
			}
			command+=
				 "'"+POut.String(userod.UserName)+"',"
				+"'"+POut.String(userod.Password)+"',"
				+    POut.Long  (userod.UserGroupNum)+","
				+    POut.Long  (userod.EmployeeNum)+","
				+    POut.Long  (userod.ClinicNum)+","
				+    POut.Long  (userod.ProvNum)+","
				+    POut.Bool  (userod.IsHidden)+","
				+    POut.Long  (userod.TaskListInBox)+","
				+    POut.Int   (userod.AnesthProvType)+","
				+    POut.Bool  (userod.DefaultHidePopups)+","
				+    POut.Bool  (userod.PasswordIsStrong)+","
				+    POut.Bool  (userod.ClinicIsRestricted)+","
				+    POut.Bool  (userod.InboxHidePopups)+","
				+    POut.Long  (userod.UserNumCEMT)+","
				+    POut.DateT (userod.DateTFail)+","
				+    POut.Byte  (userod.FailedAttempts)+","
				+"'"+POut.String(userod.DomainUser)+"',"
				+    POut.Bool  (userod.IsPasswordResetRequired)+","
				+"'"+POut.String(userod.MobileWebPin)+"',"
				+    POut.Byte  (userod.MobileWebPinFailedAttempts)+","
				+    POut.DateT (userod.DateTLastLogin)+")";
			if(useExistingPK || PrefC.RandomKeys) {
				Db.NonQ(command);
			}
			else {
				userod.UserNum=Db.NonQ(command,true,"UserNum","userod");
			}
			return userod.UserNum;
		}

		///<summary>Inserts one Userod into the database.  Returns the new priKey.  Doesn't use the cache.</summary>
		public static long InsertNoCache(Userod userod) {
			return InsertNoCache(userod,false);
		}

		///<summary>Inserts one Userod into the database.  Provides option to use the existing priKey.  Doesn't use the cache.</summary>
		public static long InsertNoCache(Userod userod,bool useExistingPK) {
			bool isRandomKeys=Prefs.GetBoolNoCache(PrefName.RandomPrimaryKeys);
			string command="INSERT INTO userod (";
			if(!useExistingPK && isRandomKeys) {
				userod.UserNum=ReplicationServers.GetKeyNoCache("userod","UserNum");
			}
			if(isRandomKeys || useExistingPK) {
				command+="UserNum,";
			}
			command+="UserName,Password,UserGroupNum,EmployeeNum,ClinicNum,ProvNum,IsHidden,TaskListInBox,AnesthProvType,DefaultHidePopups,PasswordIsStrong,ClinicIsRestricted,InboxHidePopups,UserNumCEMT,DateTFail,FailedAttempts,DomainUser,IsPasswordResetRequired,MobileWebPin,MobileWebPinFailedAttempts,DateTLastLogin) VALUES(";
			if(isRandomKeys || useExistingPK) {
				command+=POut.Long(userod.UserNum)+",";
			}
			command+=
				 "'"+POut.String(userod.UserName)+"',"
				+"'"+POut.String(userod.Password)+"',"
				+    POut.Long  (userod.UserGroupNum)+","
				+    POut.Long  (userod.EmployeeNum)+","
				+    POut.Long  (userod.ClinicNum)+","
				+    POut.Long  (userod.ProvNum)+","
				+    POut.Bool  (userod.IsHidden)+","
				+    POut.Long  (userod.TaskListInBox)+","
				+    POut.Int   (userod.AnesthProvType)+","
				+    POut.Bool  (userod.DefaultHidePopups)+","
				+    POut.Bool  (userod.PasswordIsStrong)+","
				+    POut.Bool  (userod.ClinicIsRestricted)+","
				+    POut.Bool  (userod.InboxHidePopups)+","
				+    POut.Long  (userod.UserNumCEMT)+","
				+    POut.DateT (userod.DateTFail)+","
				+    POut.Byte  (userod.FailedAttempts)+","
				+"'"+POut.String(userod.DomainUser)+"',"
				+    POut.Bool  (userod.IsPasswordResetRequired)+","
				+"'"+POut.String(userod.MobileWebPin)+"',"
				+    POut.Byte  (userod.MobileWebPinFailedAttempts)+","
				+    POut.DateT (userod.DateTLastLogin)+")";
			if(useExistingPK || isRandomKeys) {
				Db.NonQ(command);
			}
			else {
				userod.UserNum=Db.NonQ(command,true,"UserNum","userod");
			}
			return userod.UserNum;
		}

		///<summary>Updates one Userod in the database.</summary>
		public static void Update(Userod userod) {
			string command="UPDATE userod SET "
				+"UserName                  = '"+POut.String(userod.UserName)+"', "
				+"Password                  = '"+POut.String(userod.Password)+"', "
				+"UserGroupNum              =  "+POut.Long  (userod.UserGroupNum)+", "
				+"EmployeeNum               =  "+POut.Long  (userod.EmployeeNum)+", "
				+"ClinicNum                 =  "+POut.Long  (userod.ClinicNum)+", "
				+"ProvNum                   =  "+POut.Long  (userod.ProvNum)+", "
				+"IsHidden                  =  "+POut.Bool  (userod.IsHidden)+", "
				+"TaskListInBox             =  "+POut.Long  (userod.TaskListInBox)+", "
				+"AnesthProvType            =  "+POut.Int   (userod.AnesthProvType)+", "
				+"DefaultHidePopups         =  "+POut.Bool  (userod.DefaultHidePopups)+", "
				+"PasswordIsStrong          =  "+POut.Bool  (userod.PasswordIsStrong)+", "
				+"ClinicIsRestricted        =  "+POut.Bool  (userod.ClinicIsRestricted)+", "
				+"InboxHidePopups           =  "+POut.Bool  (userod.InboxHidePopups)+", "
				+"UserNumCEMT               =  "+POut.Long  (userod.UserNumCEMT)+", "
				+"DateTFail                 =  "+POut.DateT (userod.DateTFail)+", "
				+"FailedAttempts            =  "+POut.Byte  (userod.FailedAttempts)+", "
				+"DomainUser                = '"+POut.String(userod.DomainUser)+"', "
				+"IsPasswordResetRequired   =  "+POut.Bool  (userod.IsPasswordResetRequired)+", "
				+"MobileWebPin              = '"+POut.String(userod.MobileWebPin)+"', "
				+"MobileWebPinFailedAttempts=  "+POut.Byte  (userod.MobileWebPinFailedAttempts)+", "
				+"DateTLastLogin            =  "+POut.DateT (userod.DateTLastLogin)+" "
				+"WHERE UserNum = "+POut.Long(userod.UserNum);
			Db.NonQ(command);
		}

		///<summary>Updates one Userod in the database.  Uses an old object to compare to, and only alters changed fields.  This prevents collisions and concurrency problems in heavily used tables.  Returns true if an update occurred.</summary>
		public static bool Update(Userod userod,Userod oldUserod) {
			string command="";
			if(userod.UserName != oldUserod.UserName) {
				if(command!="") { command+=",";}
				command+="UserName = '"+POut.String(userod.UserName)+"'";
			}
			if(userod.Password != oldUserod.Password) {
				if(command!="") { command+=",";}
				command+="Password = '"+POut.String(userod.Password)+"'";
			}
			if(userod.UserGroupNum != oldUserod.UserGroupNum) {
				if(command!="") { command+=",";}
				command+="UserGroupNum = "+POut.Long(userod.UserGroupNum)+"";
			}
			if(userod.EmployeeNum != oldUserod.EmployeeNum) {
				if(command!="") { command+=",";}
				command+="EmployeeNum = "+POut.Long(userod.EmployeeNum)+"";
			}
			if(userod.ClinicNum != oldUserod.ClinicNum) {
				if(command!="") { command+=",";}
				command+="ClinicNum = "+POut.Long(userod.ClinicNum)+"";
			}
			if(userod.ProvNum != oldUserod.ProvNum) {
				if(command!="") { command+=",";}
				command+="ProvNum = "+POut.Long(userod.ProvNum)+"";
			}
			if(userod.IsHidden != oldUserod.IsHidden) {
				if(command!="") { command+=",";}
				command+="IsHidden = "+POut.Bool(userod.IsHidden)+"";
			}
			if(userod.TaskListInBox != oldUserod.TaskListInBox) {
				if(command!="") { command+=",";}
				command+="TaskListInBox = "+POut.Long(userod.TaskListInBox)+"";
			}
			if(userod.AnesthProvType != oldUserod.AnesthProvType) {
				if(command!="") { command+=",";}
				command+="AnesthProvType = "+POut.Int(userod.AnesthProvType)+"";
			}
			if(userod.DefaultHidePopups != oldUserod.DefaultHidePopups) {
				if(command!="") { command+=",";}
				command+="DefaultHidePopups = "+POut.Bool(userod.DefaultHidePopups)+"";
			}
			if(userod.PasswordIsStrong != oldUserod.PasswordIsStrong) {
				if(command!="") { command+=",";}
				command+="PasswordIsStrong = "+POut.Bool(userod.PasswordIsStrong)+"";
			}
			if(userod.ClinicIsRestricted != oldUserod.ClinicIsRestricted) {
				if(command!="") { command+=",";}
				command+="ClinicIsRestricted = "+POut.Bool(userod.ClinicIsRestricted)+"";
			}
			if(userod.InboxHidePopups != oldUserod.InboxHidePopups) {
				if(command!="") { command+=",";}
				command+="InboxHidePopups = "+POut.Bool(userod.InboxHidePopups)+"";
			}
			if(userod.UserNumCEMT != oldUserod.UserNumCEMT) {
				if(command!="") { command+=",";}
				command+="UserNumCEMT = "+POut.Long(userod.UserNumCEMT)+"";
			}
			if(userod.DateTFail != oldUserod.DateTFail) {
				if(command!="") { command+=",";}
				command+="DateTFail = "+POut.DateT(userod.DateTFail)+"";
			}
			if(userod.FailedAttempts != oldUserod.FailedAttempts) {
				if(command!="") { command+=",";}
				command+="FailedAttempts = "+POut.Byte(userod.FailedAttempts)+"";
			}
			if(userod.DomainUser != oldUserod.DomainUser) {
				if(command!="") { command+=",";}
				command+="DomainUser = '"+POut.String(userod.DomainUser)+"'";
			}
			if(userod.IsPasswordResetRequired != oldUserod.IsPasswordResetRequired) {
				if(command!="") { command+=",";}
				command+="IsPasswordResetRequired = "+POut.Bool(userod.IsPasswordResetRequired)+"";
			}
			if(userod.MobileWebPin != oldUserod.MobileWebPin) {
				if(command!="") { command+=",";}
				command+="MobileWebPin = '"+POut.String(userod.MobileWebPin)+"'";
			}
			if(userod.MobileWebPinFailedAttempts != oldUserod.MobileWebPinFailedAttempts) {
				if(command!="") { command+=",";}
				command+="MobileWebPinFailedAttempts = "+POut.Byte(userod.MobileWebPinFailedAttempts)+"";
			}
			if(userod.DateTLastLogin != oldUserod.DateTLastLogin) {
				if(command!="") { command+=",";}
				command+="DateTLastLogin = "+POut.DateT(userod.DateTLastLogin)+"";
			}
			if(command=="") {
				return false;
			}
			command="UPDATE userod SET "+command
				+" WHERE UserNum = "+POut.Long(userod.UserNum);
			Db.NonQ(command);
			return true;
		}

		///<summary>Returns true if Update(Userod,Userod) would make changes to the database.
		///Does not make any changes to the database and can be called before remoting role is checked.</summary>
		public static bool UpdateComparison(Userod userod,Userod oldUserod) {
			if(userod.UserName != oldUserod.UserName) {
				return true;
			}
			if(userod.Password != oldUserod.Password) {
				return true;
			}
			if(userod.UserGroupNum != oldUserod.UserGroupNum) {
				return true;
			}
			if(userod.EmployeeNum != oldUserod.EmployeeNum) {
				return true;
			}
			if(userod.ClinicNum != oldUserod.ClinicNum) {
				return true;
			}
			if(userod.ProvNum != oldUserod.ProvNum) {
				return true;
			}
			if(userod.IsHidden != oldUserod.IsHidden) {
				return true;
			}
			if(userod.TaskListInBox != oldUserod.TaskListInBox) {
				return true;
			}
			if(userod.AnesthProvType != oldUserod.AnesthProvType) {
				return true;
			}
			if(userod.DefaultHidePopups != oldUserod.DefaultHidePopups) {
				return true;
			}
			if(userod.PasswordIsStrong != oldUserod.PasswordIsStrong) {
				return true;
			}
			if(userod.ClinicIsRestricted != oldUserod.ClinicIsRestricted) {
				return true;
			}
			if(userod.InboxHidePopups != oldUserod.InboxHidePopups) {
				return true;
			}
			if(userod.UserNumCEMT != oldUserod.UserNumCEMT) {
				return true;
			}
			if(userod.DateTFail != oldUserod.DateTFail) {
				return true;
			}
			if(userod.FailedAttempts != oldUserod.FailedAttempts) {
				return true;
			}
			if(userod.DomainUser != oldUserod.DomainUser) {
				return true;
			}
			if(userod.IsPasswordResetRequired != oldUserod.IsPasswordResetRequired) {
				return true;
			}
			if(userod.MobileWebPin != oldUserod.MobileWebPin) {
				return true;
			}
			if(userod.MobileWebPinFailedAttempts != oldUserod.MobileWebPinFailedAttempts) {
				return true;
			}
			if(userod.DateTLastLogin != oldUserod.DateTLastLogin) {
				return true;
			}
			return false;
		}

		///<summary>Updates columns that do not have the 'IsNotCemtColumn' attribute. Uses the 'UserNumCEMT' column instead of the PK column.</summary>
		public static void UpdateCemt(Userod userod) {
			string command="UPDATE userod SET "
				+"UserName          = '"+POut.String(userod.UserName)+"', "
				+"Password          = '"+POut.String(userod.Password)+"', "
				+"ClinicNum         =  "+POut.Long  (userod.ClinicNum)+", "
				+"IsHidden          =  "+POut.Bool  (userod.IsHidden)+", "
				+"TaskListInBox     =  "+POut.Long  (userod.TaskListInBox)+", "
				+"AnesthProvType    =  "+POut.Int   (userod.AnesthProvType)+", "
				+"DefaultHidePopups =  "+POut.Bool  (userod.DefaultHidePopups)+", "
				+"PasswordIsStrong  =  "+POut.Bool  (userod.PasswordIsStrong)+", "
				+"ClinicIsRestricted=  "+POut.Bool  (userod.ClinicIsRestricted)+", "
				+"InboxHidePopups   =  "+POut.Bool  (userod.InboxHidePopups)+", "
				+"DomainUser        = '"+POut.String(userod.DomainUser)+"', "
				+"DateTLastLogin    =  "+POut.DateT (userod.DateTLastLogin)+" "
				+"WHERE UserNumCEMT = "+POut.Long(userod.UserNumCEMT);
			Db.NonQ(command);
		}

		///<summary>Deletes one Userod from the database.</summary>
		public static void Delete(long userNum) {
			string command="DELETE FROM userod "
				+"WHERE UserNum = "+POut.Long(userNum);
			Db.NonQ(command);
		}

		///<summary>Deletes many Userods from the database.</summary>
		public static void DeleteMany(List<long> listUserNums) {
			if(listUserNums==null || listUserNums.Count==0) {
				return;
			}
			string command="DELETE FROM userod "
				+"WHERE UserNum IN("+string.Join(",",listUserNums.Select(x => POut.Long(x)))+")";
			Db.NonQ(command);
		}

	}
}