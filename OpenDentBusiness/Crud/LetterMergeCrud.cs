//This file is automatically generated.
//Do not attempt to make changes to this file because the changes will be erased and overwritten.
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;

namespace OpenDentBusiness.Crud{
	public class LetterMergeCrud {
		///<summary>Gets one LetterMerge object from the database using the primary key.  Returns null if not found.</summary>
		public static LetterMerge SelectOne(long letterMergeNum) {
			string command="SELECT * FROM lettermerge "
				+"WHERE LetterMergeNum = "+POut.Long(letterMergeNum);
			List<LetterMerge> list=TableToList(Db.GetTable(command));
			if(list.Count==0) {
				return null;
			}
			return list[0];
		}

		///<summary>Gets one LetterMerge object from the database using a query.</summary>
		public static LetterMerge SelectOne(string command) {
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				throw new ApplicationException("Not allowed to send sql directly.  Rewrite the calling class to not use this query:\r\n"+command);
			}
			List<LetterMerge> list=TableToList(Db.GetTable(command));
			if(list.Count==0) {
				return null;
			}
			return list[0];
		}

		///<summary>Gets a list of LetterMerge objects from the database using a query.</summary>
		public static List<LetterMerge> SelectMany(string command) {
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				throw new ApplicationException("Not allowed to send sql directly.  Rewrite the calling class to not use this query:\r\n"+command);
			}
			List<LetterMerge> list=TableToList(Db.GetTable(command));
			return list;
		}

		///<summary>Converts a DataTable to a list of objects.</summary>
		public static List<LetterMerge> TableToList(DataTable table) {
			List<LetterMerge> retVal=new List<LetterMerge>();
			LetterMerge letterMerge;
			foreach(DataRow row in table.Rows) {
				letterMerge=new LetterMerge();
				letterMerge.LetterMergeNum= PIn.Long  (row["LetterMergeNum"].ToString());
				letterMerge.Description   = PIn.String(row["Description"].ToString());
				letterMerge.TemplateName  = PIn.String(row["TemplateName"].ToString());
				letterMerge.DataFileName  = PIn.String(row["DataFileName"].ToString());
				letterMerge.Category      = PIn.Long  (row["Category"].ToString());
				letterMerge.ImageFolder   = PIn.Long  (row["ImageFolder"].ToString());
				retVal.Add(letterMerge);
			}
			return retVal;
		}

		///<summary>Converts a list of LetterMerge into a DataTable.</summary>
		public static DataTable ListToTable(List<LetterMerge> listLetterMerges,string tableName="") {
			if(string.IsNullOrEmpty(tableName)) {
				tableName="LetterMerge";
			}
			DataTable table=new DataTable(tableName);
			table.Columns.Add("LetterMergeNum");
			table.Columns.Add("Description");
			table.Columns.Add("TemplateName");
			table.Columns.Add("DataFileName");
			table.Columns.Add("Category");
			table.Columns.Add("ImageFolder");
			foreach(LetterMerge letterMerge in listLetterMerges) {
				table.Rows.Add(new object[] {
					POut.Long  (letterMerge.LetterMergeNum),
					            letterMerge.Description,
					            letterMerge.TemplateName,
					            letterMerge.DataFileName,
					POut.Long  (letterMerge.Category),
					POut.Long  (letterMerge.ImageFolder),
				});
			}
			return table;
		}

		///<summary>Inserts one LetterMerge into the database.  Returns the new priKey.</summary>
		public static long Insert(LetterMerge letterMerge) {
			return Insert(letterMerge,false);
		}

		///<summary>Inserts one LetterMerge into the database.  Provides option to use the existing priKey.</summary>
		public static long Insert(LetterMerge letterMerge,bool useExistingPK) {
			if(!useExistingPK && PrefC.RandomKeys) {
				letterMerge.LetterMergeNum=ReplicationServers.GetKey("lettermerge","LetterMergeNum");
			}
			string command="INSERT INTO lettermerge (";
			if(useExistingPK || PrefC.RandomKeys) {
				command+="LetterMergeNum,";
			}
			command+="Description,TemplateName,DataFileName,Category,ImageFolder) VALUES(";
			if(useExistingPK || PrefC.RandomKeys) {
				command+=POut.Long(letterMerge.LetterMergeNum)+",";
			}
			command+=
				 "'"+POut.String(letterMerge.Description)+"',"
				+"'"+POut.String(letterMerge.TemplateName)+"',"
				+"'"+POut.String(letterMerge.DataFileName)+"',"
				+    POut.Long  (letterMerge.Category)+","
				+    POut.Long  (letterMerge.ImageFolder)+")";
			if(useExistingPK || PrefC.RandomKeys) {
				Db.NonQ(command);
			}
			else {
				letterMerge.LetterMergeNum=Db.NonQ(command,true,"LetterMergeNum","letterMerge");
			}
			return letterMerge.LetterMergeNum;
		}

		///<summary>Inserts one LetterMerge into the database.  Returns the new priKey.  Doesn't use the cache.</summary>
		public static long InsertNoCache(LetterMerge letterMerge) {
			return InsertNoCache(letterMerge,false);
		}

		///<summary>Inserts one LetterMerge into the database.  Provides option to use the existing priKey.  Doesn't use the cache.</summary>
		public static long InsertNoCache(LetterMerge letterMerge,bool useExistingPK) {
			bool isRandomKeys=Prefs.GetBoolNoCache(PrefName.RandomPrimaryKeys);
			string command="INSERT INTO lettermerge (";
			if(!useExistingPK && isRandomKeys) {
				letterMerge.LetterMergeNum=ReplicationServers.GetKeyNoCache("lettermerge","LetterMergeNum");
			}
			if(isRandomKeys || useExistingPK) {
				command+="LetterMergeNum,";
			}
			command+="Description,TemplateName,DataFileName,Category,ImageFolder) VALUES(";
			if(isRandomKeys || useExistingPK) {
				command+=POut.Long(letterMerge.LetterMergeNum)+",";
			}
			command+=
				 "'"+POut.String(letterMerge.Description)+"',"
				+"'"+POut.String(letterMerge.TemplateName)+"',"
				+"'"+POut.String(letterMerge.DataFileName)+"',"
				+    POut.Long  (letterMerge.Category)+","
				+    POut.Long  (letterMerge.ImageFolder)+")";
			if(useExistingPK || isRandomKeys) {
				Db.NonQ(command);
			}
			else {
				letterMerge.LetterMergeNum=Db.NonQ(command,true,"LetterMergeNum","letterMerge");
			}
			return letterMerge.LetterMergeNum;
		}

		///<summary>Updates one LetterMerge in the database.</summary>
		public static void Update(LetterMerge letterMerge) {
			string command="UPDATE lettermerge SET "
				+"Description   = '"+POut.String(letterMerge.Description)+"', "
				+"TemplateName  = '"+POut.String(letterMerge.TemplateName)+"', "
				+"DataFileName  = '"+POut.String(letterMerge.DataFileName)+"', "
				+"Category      =  "+POut.Long  (letterMerge.Category)+", "
				+"ImageFolder   =  "+POut.Long  (letterMerge.ImageFolder)+" "
				+"WHERE LetterMergeNum = "+POut.Long(letterMerge.LetterMergeNum);
			Db.NonQ(command);
		}

		///<summary>Updates one LetterMerge in the database.  Uses an old object to compare to, and only alters changed fields.  This prevents collisions and concurrency problems in heavily used tables.  Returns true if an update occurred.</summary>
		public static bool Update(LetterMerge letterMerge,LetterMerge oldLetterMerge) {
			string command="";
			if(letterMerge.Description != oldLetterMerge.Description) {
				if(command!="") { command+=",";}
				command+="Description = '"+POut.String(letterMerge.Description)+"'";
			}
			if(letterMerge.TemplateName != oldLetterMerge.TemplateName) {
				if(command!="") { command+=",";}
				command+="TemplateName = '"+POut.String(letterMerge.TemplateName)+"'";
			}
			if(letterMerge.DataFileName != oldLetterMerge.DataFileName) {
				if(command!="") { command+=",";}
				command+="DataFileName = '"+POut.String(letterMerge.DataFileName)+"'";
			}
			if(letterMerge.Category != oldLetterMerge.Category) {
				if(command!="") { command+=",";}
				command+="Category = "+POut.Long(letterMerge.Category)+"";
			}
			if(letterMerge.ImageFolder != oldLetterMerge.ImageFolder) {
				if(command!="") { command+=",";}
				command+="ImageFolder = "+POut.Long(letterMerge.ImageFolder)+"";
			}
			if(command=="") {
				return false;
			}
			command="UPDATE lettermerge SET "+command
				+" WHERE LetterMergeNum = "+POut.Long(letterMerge.LetterMergeNum);
			Db.NonQ(command);
			return true;
		}

		///<summary>Returns true if Update(LetterMerge,LetterMerge) would make changes to the database.
		///Does not make any changes to the database and can be called before remoting role is checked.</summary>
		public static bool UpdateComparison(LetterMerge letterMerge,LetterMerge oldLetterMerge) {
			if(letterMerge.Description != oldLetterMerge.Description) {
				return true;
			}
			if(letterMerge.TemplateName != oldLetterMerge.TemplateName) {
				return true;
			}
			if(letterMerge.DataFileName != oldLetterMerge.DataFileName) {
				return true;
			}
			if(letterMerge.Category != oldLetterMerge.Category) {
				return true;
			}
			if(letterMerge.ImageFolder != oldLetterMerge.ImageFolder) {
				return true;
			}
			return false;
		}

		///<summary>Deletes one LetterMerge from the database.</summary>
		public static void Delete(long letterMergeNum) {
			string command="DELETE FROM lettermerge "
				+"WHERE LetterMergeNum = "+POut.Long(letterMergeNum);
			Db.NonQ(command);
		}

		///<summary>Deletes many LetterMerges from the database.</summary>
		public static void DeleteMany(List<long> listLetterMergeNums) {
			if(listLetterMergeNums==null || listLetterMergeNums.Count==0) {
				return;
			}
			string command="DELETE FROM lettermerge "
				+"WHERE LetterMergeNum IN("+string.Join(",",listLetterMergeNums.Select(x => POut.Long(x)))+")";
			Db.NonQ(command);
		}

	}
}