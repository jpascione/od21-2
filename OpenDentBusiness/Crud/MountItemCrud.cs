//This file is automatically generated.
//Do not attempt to make changes to this file because the changes will be erased and overwritten.
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;

namespace OpenDentBusiness.Crud{
	public class MountItemCrud {
		///<summary>Gets one MountItem object from the database using the primary key.  Returns null if not found.</summary>
		public static MountItem SelectOne(long mountItemNum) {
			string command="SELECT * FROM mountitem "
				+"WHERE MountItemNum = "+POut.Long(mountItemNum);
			List<MountItem> list=TableToList(Db.GetTable(command));
			if(list.Count==0) {
				return null;
			}
			return list[0];
		}

		///<summary>Gets one MountItem object from the database using a query.</summary>
		public static MountItem SelectOne(string command) {
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				throw new ApplicationException("Not allowed to send sql directly.  Rewrite the calling class to not use this query:\r\n"+command);
			}
			List<MountItem> list=TableToList(Db.GetTable(command));
			if(list.Count==0) {
				return null;
			}
			return list[0];
		}

		///<summary>Gets a list of MountItem objects from the database using a query.</summary>
		public static List<MountItem> SelectMany(string command) {
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				throw new ApplicationException("Not allowed to send sql directly.  Rewrite the calling class to not use this query:\r\n"+command);
			}
			List<MountItem> list=TableToList(Db.GetTable(command));
			return list;
		}

		///<summary>Converts a DataTable to a list of objects.</summary>
		public static List<MountItem> TableToList(DataTable table) {
			List<MountItem> retVal=new List<MountItem>();
			MountItem mountItem;
			foreach(DataRow row in table.Rows) {
				mountItem=new MountItem();
				mountItem.MountItemNum   = PIn.Long  (row["MountItemNum"].ToString());
				mountItem.MountNum       = PIn.Long  (row["MountNum"].ToString());
				mountItem.Xpos           = PIn.Int   (row["Xpos"].ToString());
				mountItem.Ypos           = PIn.Int   (row["Ypos"].ToString());
				mountItem.ItemOrder      = PIn.Int   (row["ItemOrder"].ToString());
				mountItem.Width          = PIn.Int   (row["Width"].ToString());
				mountItem.Height         = PIn.Int   (row["Height"].ToString());
				mountItem.RotateOnAcquire= PIn.Int   (row["RotateOnAcquire"].ToString());
				mountItem.ToothNumbers   = PIn.String(row["ToothNumbers"].ToString());
				retVal.Add(mountItem);
			}
			return retVal;
		}

		///<summary>Converts a list of MountItem into a DataTable.</summary>
		public static DataTable ListToTable(List<MountItem> listMountItems,string tableName="") {
			if(string.IsNullOrEmpty(tableName)) {
				tableName="MountItem";
			}
			DataTable table=new DataTable(tableName);
			table.Columns.Add("MountItemNum");
			table.Columns.Add("MountNum");
			table.Columns.Add("Xpos");
			table.Columns.Add("Ypos");
			table.Columns.Add("ItemOrder");
			table.Columns.Add("Width");
			table.Columns.Add("Height");
			table.Columns.Add("RotateOnAcquire");
			table.Columns.Add("ToothNumbers");
			foreach(MountItem mountItem in listMountItems) {
				table.Rows.Add(new object[] {
					POut.Long  (mountItem.MountItemNum),
					POut.Long  (mountItem.MountNum),
					POut.Int   (mountItem.Xpos),
					POut.Int   (mountItem.Ypos),
					POut.Int   (mountItem.ItemOrder),
					POut.Int   (mountItem.Width),
					POut.Int   (mountItem.Height),
					POut.Int   (mountItem.RotateOnAcquire),
					            mountItem.ToothNumbers,
				});
			}
			return table;
		}

		///<summary>Inserts one MountItem into the database.  Returns the new priKey.</summary>
		public static long Insert(MountItem mountItem) {
			return Insert(mountItem,false);
		}

		///<summary>Inserts one MountItem into the database.  Provides option to use the existing priKey.</summary>
		public static long Insert(MountItem mountItem,bool useExistingPK) {
			if(!useExistingPK && PrefC.RandomKeys) {
				mountItem.MountItemNum=ReplicationServers.GetKey("mountitem","MountItemNum");
			}
			string command="INSERT INTO mountitem (";
			if(useExistingPK || PrefC.RandomKeys) {
				command+="MountItemNum,";
			}
			command+="MountNum,Xpos,Ypos,ItemOrder,Width,Height,RotateOnAcquire,ToothNumbers) VALUES(";
			if(useExistingPK || PrefC.RandomKeys) {
				command+=POut.Long(mountItem.MountItemNum)+",";
			}
			command+=
				     POut.Long  (mountItem.MountNum)+","
				+    POut.Int   (mountItem.Xpos)+","
				+    POut.Int   (mountItem.Ypos)+","
				+    POut.Int   (mountItem.ItemOrder)+","
				+    POut.Int   (mountItem.Width)+","
				+    POut.Int   (mountItem.Height)+","
				+    POut.Int   (mountItem.RotateOnAcquire)+","
				+"'"+POut.String(mountItem.ToothNumbers)+"')";
			if(useExistingPK || PrefC.RandomKeys) {
				Db.NonQ(command);
			}
			else {
				mountItem.MountItemNum=Db.NonQ(command,true,"MountItemNum","mountItem");
			}
			return mountItem.MountItemNum;
		}

		///<summary>Inserts one MountItem into the database.  Returns the new priKey.  Doesn't use the cache.</summary>
		public static long InsertNoCache(MountItem mountItem) {
			return InsertNoCache(mountItem,false);
		}

		///<summary>Inserts one MountItem into the database.  Provides option to use the existing priKey.  Doesn't use the cache.</summary>
		public static long InsertNoCache(MountItem mountItem,bool useExistingPK) {
			bool isRandomKeys=Prefs.GetBoolNoCache(PrefName.RandomPrimaryKeys);
			string command="INSERT INTO mountitem (";
			if(!useExistingPK && isRandomKeys) {
				mountItem.MountItemNum=ReplicationServers.GetKeyNoCache("mountitem","MountItemNum");
			}
			if(isRandomKeys || useExistingPK) {
				command+="MountItemNum,";
			}
			command+="MountNum,Xpos,Ypos,ItemOrder,Width,Height,RotateOnAcquire,ToothNumbers) VALUES(";
			if(isRandomKeys || useExistingPK) {
				command+=POut.Long(mountItem.MountItemNum)+",";
			}
			command+=
				     POut.Long  (mountItem.MountNum)+","
				+    POut.Int   (mountItem.Xpos)+","
				+    POut.Int   (mountItem.Ypos)+","
				+    POut.Int   (mountItem.ItemOrder)+","
				+    POut.Int   (mountItem.Width)+","
				+    POut.Int   (mountItem.Height)+","
				+    POut.Int   (mountItem.RotateOnAcquire)+","
				+"'"+POut.String(mountItem.ToothNumbers)+"')";
			if(useExistingPK || isRandomKeys) {
				Db.NonQ(command);
			}
			else {
				mountItem.MountItemNum=Db.NonQ(command,true,"MountItemNum","mountItem");
			}
			return mountItem.MountItemNum;
		}

		///<summary>Updates one MountItem in the database.</summary>
		public static void Update(MountItem mountItem) {
			string command="UPDATE mountitem SET "
				+"MountNum       =  "+POut.Long  (mountItem.MountNum)+", "
				+"Xpos           =  "+POut.Int   (mountItem.Xpos)+", "
				+"Ypos           =  "+POut.Int   (mountItem.Ypos)+", "
				+"ItemOrder      =  "+POut.Int   (mountItem.ItemOrder)+", "
				+"Width          =  "+POut.Int   (mountItem.Width)+", "
				+"Height         =  "+POut.Int   (mountItem.Height)+", "
				+"RotateOnAcquire=  "+POut.Int   (mountItem.RotateOnAcquire)+", "
				+"ToothNumbers   = '"+POut.String(mountItem.ToothNumbers)+"' "
				+"WHERE MountItemNum = "+POut.Long(mountItem.MountItemNum);
			Db.NonQ(command);
		}

		///<summary>Updates one MountItem in the database.  Uses an old object to compare to, and only alters changed fields.  This prevents collisions and concurrency problems in heavily used tables.  Returns true if an update occurred.</summary>
		public static bool Update(MountItem mountItem,MountItem oldMountItem) {
			string command="";
			if(mountItem.MountNum != oldMountItem.MountNum) {
				if(command!="") { command+=",";}
				command+="MountNum = "+POut.Long(mountItem.MountNum)+"";
			}
			if(mountItem.Xpos != oldMountItem.Xpos) {
				if(command!="") { command+=",";}
				command+="Xpos = "+POut.Int(mountItem.Xpos)+"";
			}
			if(mountItem.Ypos != oldMountItem.Ypos) {
				if(command!="") { command+=",";}
				command+="Ypos = "+POut.Int(mountItem.Ypos)+"";
			}
			if(mountItem.ItemOrder != oldMountItem.ItemOrder) {
				if(command!="") { command+=",";}
				command+="ItemOrder = "+POut.Int(mountItem.ItemOrder)+"";
			}
			if(mountItem.Width != oldMountItem.Width) {
				if(command!="") { command+=",";}
				command+="Width = "+POut.Int(mountItem.Width)+"";
			}
			if(mountItem.Height != oldMountItem.Height) {
				if(command!="") { command+=",";}
				command+="Height = "+POut.Int(mountItem.Height)+"";
			}
			if(mountItem.RotateOnAcquire != oldMountItem.RotateOnAcquire) {
				if(command!="") { command+=",";}
				command+="RotateOnAcquire = "+POut.Int(mountItem.RotateOnAcquire)+"";
			}
			if(mountItem.ToothNumbers != oldMountItem.ToothNumbers) {
				if(command!="") { command+=",";}
				command+="ToothNumbers = '"+POut.String(mountItem.ToothNumbers)+"'";
			}
			if(command=="") {
				return false;
			}
			command="UPDATE mountitem SET "+command
				+" WHERE MountItemNum = "+POut.Long(mountItem.MountItemNum);
			Db.NonQ(command);
			return true;
		}

		///<summary>Returns true if Update(MountItem,MountItem) would make changes to the database.
		///Does not make any changes to the database and can be called before remoting role is checked.</summary>
		public static bool UpdateComparison(MountItem mountItem,MountItem oldMountItem) {
			if(mountItem.MountNum != oldMountItem.MountNum) {
				return true;
			}
			if(mountItem.Xpos != oldMountItem.Xpos) {
				return true;
			}
			if(mountItem.Ypos != oldMountItem.Ypos) {
				return true;
			}
			if(mountItem.ItemOrder != oldMountItem.ItemOrder) {
				return true;
			}
			if(mountItem.Width != oldMountItem.Width) {
				return true;
			}
			if(mountItem.Height != oldMountItem.Height) {
				return true;
			}
			if(mountItem.RotateOnAcquire != oldMountItem.RotateOnAcquire) {
				return true;
			}
			if(mountItem.ToothNumbers != oldMountItem.ToothNumbers) {
				return true;
			}
			return false;
		}

		///<summary>Deletes one MountItem from the database.</summary>
		public static void Delete(long mountItemNum) {
			string command="DELETE FROM mountitem "
				+"WHERE MountItemNum = "+POut.Long(mountItemNum);
			Db.NonQ(command);
		}

		///<summary>Deletes many MountItems from the database.</summary>
		public static void DeleteMany(List<long> listMountItemNums) {
			if(listMountItemNums==null || listMountItemNums.Count==0) {
				return;
			}
			string command="DELETE FROM mountitem "
				+"WHERE MountItemNum IN("+string.Join(",",listMountItemNums.Select(x => POut.Long(x)))+")";
			Db.NonQ(command);
		}

	}
}