//This file is automatically generated.
//Do not attempt to make changes to this file because the changes will be erased and overwritten.
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;

namespace OpenDentBusiness.Crud{
	public class CustRefEntryCrud {
		///<summary>Gets one CustRefEntry object from the database using the primary key.  Returns null if not found.</summary>
		public static CustRefEntry SelectOne(long custRefEntryNum) {
			string command="SELECT * FROM custrefentry "
				+"WHERE CustRefEntryNum = "+POut.Long(custRefEntryNum);
			List<CustRefEntry> list=TableToList(Db.GetTable(command));
			if(list.Count==0) {
				return null;
			}
			return list[0];
		}

		///<summary>Gets one CustRefEntry object from the database using a query.</summary>
		public static CustRefEntry SelectOne(string command) {
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				throw new ApplicationException("Not allowed to send sql directly.  Rewrite the calling class to not use this query:\r\n"+command);
			}
			List<CustRefEntry> list=TableToList(Db.GetTable(command));
			if(list.Count==0) {
				return null;
			}
			return list[0];
		}

		///<summary>Gets a list of CustRefEntry objects from the database using a query.</summary>
		public static List<CustRefEntry> SelectMany(string command) {
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				throw new ApplicationException("Not allowed to send sql directly.  Rewrite the calling class to not use this query:\r\n"+command);
			}
			List<CustRefEntry> list=TableToList(Db.GetTable(command));
			return list;
		}

		///<summary>Converts a DataTable to a list of objects.</summary>
		public static List<CustRefEntry> TableToList(DataTable table) {
			List<CustRefEntry> retVal=new List<CustRefEntry>();
			CustRefEntry custRefEntry;
			foreach(DataRow row in table.Rows) {
				custRefEntry=new CustRefEntry();
				custRefEntry.CustRefEntryNum= PIn.Long  (row["CustRefEntryNum"].ToString());
				custRefEntry.PatNumCust     = PIn.Long  (row["PatNumCust"].ToString());
				custRefEntry.PatNumRef      = PIn.Long  (row["PatNumRef"].ToString());
				custRefEntry.DateEntry      = PIn.Date  (row["DateEntry"].ToString());
				custRefEntry.Note           = PIn.String(row["Note"].ToString());
				retVal.Add(custRefEntry);
			}
			return retVal;
		}

		///<summary>Converts a list of CustRefEntry into a DataTable.</summary>
		public static DataTable ListToTable(List<CustRefEntry> listCustRefEntrys,string tableName="") {
			if(string.IsNullOrEmpty(tableName)) {
				tableName="CustRefEntry";
			}
			DataTable table=new DataTable(tableName);
			table.Columns.Add("CustRefEntryNum");
			table.Columns.Add("PatNumCust");
			table.Columns.Add("PatNumRef");
			table.Columns.Add("DateEntry");
			table.Columns.Add("Note");
			foreach(CustRefEntry custRefEntry in listCustRefEntrys) {
				table.Rows.Add(new object[] {
					POut.Long  (custRefEntry.CustRefEntryNum),
					POut.Long  (custRefEntry.PatNumCust),
					POut.Long  (custRefEntry.PatNumRef),
					POut.DateT (custRefEntry.DateEntry,false),
					            custRefEntry.Note,
				});
			}
			return table;
		}

		///<summary>Inserts one CustRefEntry into the database.  Returns the new priKey.</summary>
		public static long Insert(CustRefEntry custRefEntry) {
			return Insert(custRefEntry,false);
		}

		///<summary>Inserts one CustRefEntry into the database.  Provides option to use the existing priKey.</summary>
		public static long Insert(CustRefEntry custRefEntry,bool useExistingPK) {
			if(!useExistingPK && PrefC.RandomKeys) {
				custRefEntry.CustRefEntryNum=ReplicationServers.GetKey("custrefentry","CustRefEntryNum");
			}
			string command="INSERT INTO custrefentry (";
			if(useExistingPK || PrefC.RandomKeys) {
				command+="CustRefEntryNum,";
			}
			command+="PatNumCust,PatNumRef,DateEntry,Note) VALUES(";
			if(useExistingPK || PrefC.RandomKeys) {
				command+=POut.Long(custRefEntry.CustRefEntryNum)+",";
			}
			command+=
				     POut.Long  (custRefEntry.PatNumCust)+","
				+    POut.Long  (custRefEntry.PatNumRef)+","
				+    POut.Date  (custRefEntry.DateEntry)+","
				+"'"+POut.String(custRefEntry.Note)+"')";
			if(useExistingPK || PrefC.RandomKeys) {
				Db.NonQ(command);
			}
			else {
				custRefEntry.CustRefEntryNum=Db.NonQ(command,true,"CustRefEntryNum","custRefEntry");
			}
			return custRefEntry.CustRefEntryNum;
		}

		///<summary>Inserts one CustRefEntry into the database.  Returns the new priKey.  Doesn't use the cache.</summary>
		public static long InsertNoCache(CustRefEntry custRefEntry) {
			return InsertNoCache(custRefEntry,false);
		}

		///<summary>Inserts one CustRefEntry into the database.  Provides option to use the existing priKey.  Doesn't use the cache.</summary>
		public static long InsertNoCache(CustRefEntry custRefEntry,bool useExistingPK) {
			bool isRandomKeys=Prefs.GetBoolNoCache(PrefName.RandomPrimaryKeys);
			string command="INSERT INTO custrefentry (";
			if(!useExistingPK && isRandomKeys) {
				custRefEntry.CustRefEntryNum=ReplicationServers.GetKeyNoCache("custrefentry","CustRefEntryNum");
			}
			if(isRandomKeys || useExistingPK) {
				command+="CustRefEntryNum,";
			}
			command+="PatNumCust,PatNumRef,DateEntry,Note) VALUES(";
			if(isRandomKeys || useExistingPK) {
				command+=POut.Long(custRefEntry.CustRefEntryNum)+",";
			}
			command+=
				     POut.Long  (custRefEntry.PatNumCust)+","
				+    POut.Long  (custRefEntry.PatNumRef)+","
				+    POut.Date  (custRefEntry.DateEntry)+","
				+"'"+POut.String(custRefEntry.Note)+"')";
			if(useExistingPK || isRandomKeys) {
				Db.NonQ(command);
			}
			else {
				custRefEntry.CustRefEntryNum=Db.NonQ(command,true,"CustRefEntryNum","custRefEntry");
			}
			return custRefEntry.CustRefEntryNum;
		}

		///<summary>Updates one CustRefEntry in the database.</summary>
		public static void Update(CustRefEntry custRefEntry) {
			string command="UPDATE custrefentry SET "
				+"PatNumCust     =  "+POut.Long  (custRefEntry.PatNumCust)+", "
				+"PatNumRef      =  "+POut.Long  (custRefEntry.PatNumRef)+", "
				+"DateEntry      =  "+POut.Date  (custRefEntry.DateEntry)+", "
				+"Note           = '"+POut.String(custRefEntry.Note)+"' "
				+"WHERE CustRefEntryNum = "+POut.Long(custRefEntry.CustRefEntryNum);
			Db.NonQ(command);
		}

		///<summary>Updates one CustRefEntry in the database.  Uses an old object to compare to, and only alters changed fields.  This prevents collisions and concurrency problems in heavily used tables.  Returns true if an update occurred.</summary>
		public static bool Update(CustRefEntry custRefEntry,CustRefEntry oldCustRefEntry) {
			string command="";
			if(custRefEntry.PatNumCust != oldCustRefEntry.PatNumCust) {
				if(command!="") { command+=",";}
				command+="PatNumCust = "+POut.Long(custRefEntry.PatNumCust)+"";
			}
			if(custRefEntry.PatNumRef != oldCustRefEntry.PatNumRef) {
				if(command!="") { command+=",";}
				command+="PatNumRef = "+POut.Long(custRefEntry.PatNumRef)+"";
			}
			if(custRefEntry.DateEntry.Date != oldCustRefEntry.DateEntry.Date) {
				if(command!="") { command+=",";}
				command+="DateEntry = "+POut.Date(custRefEntry.DateEntry)+"";
			}
			if(custRefEntry.Note != oldCustRefEntry.Note) {
				if(command!="") { command+=",";}
				command+="Note = '"+POut.String(custRefEntry.Note)+"'";
			}
			if(command=="") {
				return false;
			}
			command="UPDATE custrefentry SET "+command
				+" WHERE CustRefEntryNum = "+POut.Long(custRefEntry.CustRefEntryNum);
			Db.NonQ(command);
			return true;
		}

		///<summary>Returns true if Update(CustRefEntry,CustRefEntry) would make changes to the database.
		///Does not make any changes to the database and can be called before remoting role is checked.</summary>
		public static bool UpdateComparison(CustRefEntry custRefEntry,CustRefEntry oldCustRefEntry) {
			if(custRefEntry.PatNumCust != oldCustRefEntry.PatNumCust) {
				return true;
			}
			if(custRefEntry.PatNumRef != oldCustRefEntry.PatNumRef) {
				return true;
			}
			if(custRefEntry.DateEntry.Date != oldCustRefEntry.DateEntry.Date) {
				return true;
			}
			if(custRefEntry.Note != oldCustRefEntry.Note) {
				return true;
			}
			return false;
		}

		///<summary>Deletes one CustRefEntry from the database.</summary>
		public static void Delete(long custRefEntryNum) {
			string command="DELETE FROM custrefentry "
				+"WHERE CustRefEntryNum = "+POut.Long(custRefEntryNum);
			Db.NonQ(command);
		}

		///<summary>Deletes many CustRefEntrys from the database.</summary>
		public static void DeleteMany(List<long> listCustRefEntryNums) {
			if(listCustRefEntryNums==null || listCustRefEntryNums.Count==0) {
				return;
			}
			string command="DELETE FROM custrefentry "
				+"WHERE CustRefEntryNum IN("+string.Join(",",listCustRefEntryNums.Select(x => POut.Long(x)))+")";
			Db.NonQ(command);
		}

	}
}