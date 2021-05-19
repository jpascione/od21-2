//This file is automatically generated.
//Do not attempt to make changes to this file because the changes will be erased and overwritten.
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;

namespace OpenDentBusiness.Crud{
	public class MobileReleaseBuildCrud {
		///<summary>Gets one MobileReleaseBuild object from the database using the primary key.  Returns null if not found.</summary>
		public static MobileReleaseBuild SelectOne(long mobileReleaseBuildNum) {
			string command="SELECT * FROM mobilereleasebuild "
				+"WHERE MobileReleaseBuildNum = "+POut.Long(mobileReleaseBuildNum);
			List<MobileReleaseBuild> list=TableToList(Db.GetTable(command));
			if(list.Count==0) {
				return null;
			}
			return list[0];
		}

		///<summary>Gets one MobileReleaseBuild object from the database using a query.</summary>
		public static MobileReleaseBuild SelectOne(string command) {
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				throw new ApplicationException("Not allowed to send sql directly.  Rewrite the calling class to not use this query:\r\n"+command);
			}
			List<MobileReleaseBuild> list=TableToList(Db.GetTable(command));
			if(list.Count==0) {
				return null;
			}
			return list[0];
		}

		///<summary>Gets a list of MobileReleaseBuild objects from the database using a query.</summary>
		public static List<MobileReleaseBuild> SelectMany(string command) {
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				throw new ApplicationException("Not allowed to send sql directly.  Rewrite the calling class to not use this query:\r\n"+command);
			}
			List<MobileReleaseBuild> list=TableToList(Db.GetTable(command));
			return list;
		}

		///<summary>Converts a DataTable to a list of objects.</summary>
		public static List<MobileReleaseBuild> TableToList(DataTable table) {
			List<MobileReleaseBuild> retVal=new List<MobileReleaseBuild>();
			MobileReleaseBuild mobileReleaseBuild;
			foreach(DataRow row in table.Rows) {
				mobileReleaseBuild=new MobileReleaseBuild();
				mobileReleaseBuild.MobileReleaseBuildNum= PIn.Long  (row["MobileReleaseBuildNum"].ToString());
				mobileReleaseBuild.MobileReleaseNum     = PIn.Long  (row["MobileReleaseNum"].ToString());
				mobileReleaseBuild.BuildVersion         = PIn.String(row["BuildVersion"].ToString());
				string appType=row["AppType"].ToString();
				if(appType=="") {
					mobileReleaseBuild.AppType            =(OpenDentBusiness.AppType)0;
				}
				else try{
					mobileReleaseBuild.AppType            =(OpenDentBusiness.AppType)Enum.Parse(typeof(OpenDentBusiness.AppType),appType);
				}
				catch{
					mobileReleaseBuild.AppType            =(OpenDentBusiness.AppType)0;
				}
				string appStore=row["AppStore"].ToString();
				if(appStore=="") {
					mobileReleaseBuild.AppStore           =(OpenDentBusiness.AppStore)0;
				}
				else try{
					mobileReleaseBuild.AppStore           =(OpenDentBusiness.AppStore)Enum.Parse(typeof(OpenDentBusiness.AppStore),appStore);
				}
				catch{
					mobileReleaseBuild.AppStore           =(OpenDentBusiness.AppStore)0;
				}
				string storeStatus=row["StoreStatus"].ToString();
				if(storeStatus=="") {
					mobileReleaseBuild.StoreStatus        =(OpenDentBusiness.StoreStatus)0;
				}
				else try{
					mobileReleaseBuild.StoreStatus        =(OpenDentBusiness.StoreStatus)Enum.Parse(typeof(OpenDentBusiness.StoreStatus),storeStatus);
				}
				catch{
					mobileReleaseBuild.StoreStatus        =(OpenDentBusiness.StoreStatus)0;
				}
				mobileReleaseBuild.DateTimeStatusChanged= PIn.DateT  (row["DateTimeStatusChanged"].ToString());
				retVal.Add(mobileReleaseBuild);
			}
			return retVal;
		}

		///<summary>Converts a list of MobileReleaseBuild into a DataTable.</summary>
		public static DataTable ListToTable(List<MobileReleaseBuild> listMobileReleaseBuilds,string tableName="") {
			if(string.IsNullOrEmpty(tableName)) {
				tableName="MobileReleaseBuild";
			}
			DataTable table=new DataTable(tableName);
			table.Columns.Add("MobileReleaseBuildNum");
			table.Columns.Add("MobileReleaseNum");
			table.Columns.Add("BuildVersion");
			table.Columns.Add("AppType");
			table.Columns.Add("AppStore");
			table.Columns.Add("StoreStatus");
			table.Columns.Add("DateTimeStatusChanged");
			foreach(MobileReleaseBuild mobileReleaseBuild in listMobileReleaseBuilds) {
				table.Rows.Add(new object[] {
					POut.Long  (mobileReleaseBuild.MobileReleaseBuildNum),
					POut.Long  (mobileReleaseBuild.MobileReleaseNum),
					            mobileReleaseBuild.BuildVersion,
					POut.Int   ((int)mobileReleaseBuild.AppType),
					POut.Int   ((int)mobileReleaseBuild.AppStore),
					POut.Int   ((int)mobileReleaseBuild.StoreStatus),
					POut.DateT (mobileReleaseBuild.DateTimeStatusChanged,false),
				});
			}
			return table;
		}

		///<summary>Inserts one MobileReleaseBuild into the database.  Returns the new priKey.</summary>
		public static long Insert(MobileReleaseBuild mobileReleaseBuild) {
			return Insert(mobileReleaseBuild,false);
		}

		///<summary>Inserts one MobileReleaseBuild into the database.  Provides option to use the existing priKey.</summary>
		public static long Insert(MobileReleaseBuild mobileReleaseBuild,bool useExistingPK) {
			string command="INSERT INTO mobilereleasebuild (";
			if(useExistingPK) {
				command+="MobileReleaseBuildNum,";
			}
			command+="MobileReleaseNum,BuildVersion,AppType,AppStore,StoreStatus,DateTimeStatusChanged) VALUES(";
			if(useExistingPK) {
				command+=POut.Long(mobileReleaseBuild.MobileReleaseBuildNum)+",";
			}
			command+=
				     POut.Long  (mobileReleaseBuild.MobileReleaseNum)+","
				+"'"+POut.String(mobileReleaseBuild.BuildVersion)+"',"
				+"'"+POut.String(mobileReleaseBuild.AppType.ToString())+"',"
				+"'"+POut.String(mobileReleaseBuild.AppStore.ToString())+"',"
				+"'"+POut.String(mobileReleaseBuild.StoreStatus.ToString())+"',"
				+    POut.DateT  (mobileReleaseBuild.DateTimeStatusChanged)+")";
			if(useExistingPK) {
				Db.NonQ(command);
			}
			else {
				mobileReleaseBuild.MobileReleaseBuildNum=Db.NonQ(command,true,"MobileReleaseBuildNum","mobileReleaseBuild");
			}
			return mobileReleaseBuild.MobileReleaseBuildNum;
		}

		///<summary>Inserts one MobileReleaseBuild into the database.  Returns the new priKey.  Doesn't use the cache.</summary>
		public static long InsertNoCache(MobileReleaseBuild mobileReleaseBuild) {
			return InsertNoCache(mobileReleaseBuild,false);
		}

		///<summary>Inserts one MobileReleaseBuild into the database.  Provides option to use the existing priKey.  Doesn't use the cache.</summary>
		public static long InsertNoCache(MobileReleaseBuild mobileReleaseBuild,bool useExistingPK) {
			string command="INSERT INTO mobilereleasebuild (";
			if(useExistingPK) {
				command+="MobileReleaseBuildNum,";
			}
			command+="MobileReleaseNum,BuildVersion,AppType,AppStore,StoreStatus,DateTimeStatusChanged) VALUES(";
			if(useExistingPK) {
				command+=POut.Long(mobileReleaseBuild.MobileReleaseBuildNum)+",";
			}
			command+=
				     POut.Long  (mobileReleaseBuild.MobileReleaseNum)+","
				+"'"+POut.String(mobileReleaseBuild.BuildVersion)+"',"
				+"'"+POut.String(mobileReleaseBuild.AppType.ToString())+"',"
				+"'"+POut.String(mobileReleaseBuild.AppStore.ToString())+"',"
				+"'"+POut.String(mobileReleaseBuild.StoreStatus.ToString())+"',"
				+    POut.DateT  (mobileReleaseBuild.DateTimeStatusChanged)+")";
			if(useExistingPK) {
				Db.NonQ(command);
			}
			else {
				mobileReleaseBuild.MobileReleaseBuildNum=Db.NonQ(command,true,"MobileReleaseBuildNum","mobileReleaseBuild");
			}
			return mobileReleaseBuild.MobileReleaseBuildNum;
		}

		///<summary>Updates one MobileReleaseBuild in the database.</summary>
		public static void Update(MobileReleaseBuild mobileReleaseBuild) {
			string command="UPDATE mobilereleasebuild SET "
				+"MobileReleaseNum     =  "+POut.Long  (mobileReleaseBuild.MobileReleaseNum)+", "
				+"BuildVersion         = '"+POut.String(mobileReleaseBuild.BuildVersion)+"', "
				+"AppType              = '"+POut.String(mobileReleaseBuild.AppType.ToString())+"', "
				+"AppStore             = '"+POut.String(mobileReleaseBuild.AppStore.ToString())+"', "
				+"StoreStatus          = '"+POut.String(mobileReleaseBuild.StoreStatus.ToString())+"', "
				+"DateTimeStatusChanged=  "+POut.DateT  (mobileReleaseBuild.DateTimeStatusChanged)+" "
				+"WHERE MobileReleaseBuildNum = "+POut.Long(mobileReleaseBuild.MobileReleaseBuildNum);
			Db.NonQ(command);
		}

		///<summary>Updates one MobileReleaseBuild in the database.  Uses an old object to compare to, and only alters changed fields.  This prevents collisions and concurrency problems in heavily used tables.  Returns true if an update occurred.</summary>
		public static bool Update(MobileReleaseBuild mobileReleaseBuild,MobileReleaseBuild oldMobileReleaseBuild) {
			string command="";
			if(mobileReleaseBuild.MobileReleaseNum != oldMobileReleaseBuild.MobileReleaseNum) {
				if(command!="") { command+=",";}
				command+="MobileReleaseNum = "+POut.Long(mobileReleaseBuild.MobileReleaseNum)+"";
			}
			if(mobileReleaseBuild.BuildVersion != oldMobileReleaseBuild.BuildVersion) {
				if(command!="") { command+=",";}
				command+="BuildVersion = '"+POut.String(mobileReleaseBuild.BuildVersion)+"'";
			}
			if(mobileReleaseBuild.AppType != oldMobileReleaseBuild.AppType) {
				if(command!="") { command+=",";}
				command+="AppType = '"+POut.String(mobileReleaseBuild.AppType.ToString())+"'";
			}
			if(mobileReleaseBuild.AppStore != oldMobileReleaseBuild.AppStore) {
				if(command!="") { command+=",";}
				command+="AppStore = '"+POut.String(mobileReleaseBuild.AppStore.ToString())+"'";
			}
			if(mobileReleaseBuild.StoreStatus != oldMobileReleaseBuild.StoreStatus) {
				if(command!="") { command+=",";}
				command+="StoreStatus = '"+POut.String(mobileReleaseBuild.StoreStatus.ToString())+"'";
			}
			if(mobileReleaseBuild.DateTimeStatusChanged != oldMobileReleaseBuild.DateTimeStatusChanged) {
				if(command!="") { command+=",";}
				command+="DateTimeStatusChanged = "+POut.DateT(mobileReleaseBuild.DateTimeStatusChanged)+"";
			}
			if(command=="") {
				return false;
			}
			command="UPDATE mobilereleasebuild SET "+command
				+" WHERE MobileReleaseBuildNum = "+POut.Long(mobileReleaseBuild.MobileReleaseBuildNum);
			Db.NonQ(command);
			return true;
		}

		///<summary>Returns true if Update(MobileReleaseBuild,MobileReleaseBuild) would make changes to the database.
		///Does not make any changes to the database and can be called before remoting role is checked.</summary>
		public static bool UpdateComparison(MobileReleaseBuild mobileReleaseBuild,MobileReleaseBuild oldMobileReleaseBuild) {
			if(mobileReleaseBuild.MobileReleaseNum != oldMobileReleaseBuild.MobileReleaseNum) {
				return true;
			}
			if(mobileReleaseBuild.BuildVersion != oldMobileReleaseBuild.BuildVersion) {
				return true;
			}
			if(mobileReleaseBuild.AppType != oldMobileReleaseBuild.AppType) {
				return true;
			}
			if(mobileReleaseBuild.AppStore != oldMobileReleaseBuild.AppStore) {
				return true;
			}
			if(mobileReleaseBuild.StoreStatus != oldMobileReleaseBuild.StoreStatus) {
				return true;
			}
			if(mobileReleaseBuild.DateTimeStatusChanged != oldMobileReleaseBuild.DateTimeStatusChanged) {
				return true;
			}
			return false;
		}

		///<summary>Deletes one MobileReleaseBuild from the database.</summary>
		public static void Delete(long mobileReleaseBuildNum) {
			string command="DELETE FROM mobilereleasebuild "
				+"WHERE MobileReleaseBuildNum = "+POut.Long(mobileReleaseBuildNum);
			Db.NonQ(command);
		}

		///<summary>Deletes many MobileReleaseBuilds from the database.</summary>
		public static void DeleteMany(List<long> listMobileReleaseBuildNums) {
			if(listMobileReleaseBuildNums==null || listMobileReleaseBuildNums.Count==0) {
				return;
			}
			string command="DELETE FROM mobilereleasebuild "
				+"WHERE MobileReleaseBuildNum IN("+string.Join(",",listMobileReleaseBuildNums.Select(x => POut.Long(x)))+")";
			Db.NonQ(command);
		}

	}
}