//This file is automatically generated.
//Do not attempt to make changes to this file because the changes will be erased and overwritten.
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;

namespace OpenDentBusiness.Crud{
	public class ContactCrud {
		///<summary>Gets one Contact object from the database using the primary key.  Returns null if not found.</summary>
		public static Contact SelectOne(long contactNum) {
			string command="SELECT * FROM contact "
				+"WHERE ContactNum = "+POut.Long(contactNum);
			List<Contact> list=TableToList(Db.GetTable(command));
			if(list.Count==0) {
				return null;
			}
			return list[0];
		}

		///<summary>Gets one Contact object from the database using a query.</summary>
		public static Contact SelectOne(string command) {
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				throw new ApplicationException("Not allowed to send sql directly.  Rewrite the calling class to not use this query:\r\n"+command);
			}
			List<Contact> list=TableToList(Db.GetTable(command));
			if(list.Count==0) {
				return null;
			}
			return list[0];
		}

		///<summary>Gets a list of Contact objects from the database using a query.</summary>
		public static List<Contact> SelectMany(string command) {
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				throw new ApplicationException("Not allowed to send sql directly.  Rewrite the calling class to not use this query:\r\n"+command);
			}
			List<Contact> list=TableToList(Db.GetTable(command));
			return list;
		}

		///<summary>Converts a DataTable to a list of objects.</summary>
		public static List<Contact> TableToList(DataTable table) {
			List<Contact> retVal=new List<Contact>();
			Contact contact;
			foreach(DataRow row in table.Rows) {
				contact=new Contact();
				contact.ContactNum= PIn.Long  (row["ContactNum"].ToString());
				contact.LName     = PIn.String(row["LName"].ToString());
				contact.FName     = PIn.String(row["FName"].ToString());
				contact.WkPhone   = PIn.String(row["WkPhone"].ToString());
				contact.Fax       = PIn.String(row["Fax"].ToString());
				contact.Category  = PIn.Long  (row["Category"].ToString());
				contact.Notes     = PIn.String(row["Notes"].ToString());
				retVal.Add(contact);
			}
			return retVal;
		}

		///<summary>Converts a list of Contact into a DataTable.</summary>
		public static DataTable ListToTable(List<Contact> listContacts,string tableName="") {
			if(string.IsNullOrEmpty(tableName)) {
				tableName="Contact";
			}
			DataTable table=new DataTable(tableName);
			table.Columns.Add("ContactNum");
			table.Columns.Add("LName");
			table.Columns.Add("FName");
			table.Columns.Add("WkPhone");
			table.Columns.Add("Fax");
			table.Columns.Add("Category");
			table.Columns.Add("Notes");
			foreach(Contact contact in listContacts) {
				table.Rows.Add(new object[] {
					POut.Long  (contact.ContactNum),
					            contact.LName,
					            contact.FName,
					            contact.WkPhone,
					            contact.Fax,
					POut.Long  (contact.Category),
					            contact.Notes,
				});
			}
			return table;
		}

		///<summary>Inserts one Contact into the database.  Returns the new priKey.</summary>
		public static long Insert(Contact contact) {
			return Insert(contact,false);
		}

		///<summary>Inserts one Contact into the database.  Provides option to use the existing priKey.</summary>
		public static long Insert(Contact contact,bool useExistingPK) {
			if(!useExistingPK && PrefC.RandomKeys) {
				contact.ContactNum=ReplicationServers.GetKey("contact","ContactNum");
			}
			string command="INSERT INTO contact (";
			if(useExistingPK || PrefC.RandomKeys) {
				command+="ContactNum,";
			}
			command+="LName,FName,WkPhone,Fax,Category,Notes) VALUES(";
			if(useExistingPK || PrefC.RandomKeys) {
				command+=POut.Long(contact.ContactNum)+",";
			}
			command+=
				 "'"+POut.String(contact.LName)+"',"
				+"'"+POut.String(contact.FName)+"',"
				+"'"+POut.String(contact.WkPhone)+"',"
				+"'"+POut.String(contact.Fax)+"',"
				+    POut.Long  (contact.Category)+","
				+    DbHelper.ParamChar+"paramNotes)";
			if(contact.Notes==null) {
				contact.Notes="";
			}
			OdSqlParameter paramNotes=new OdSqlParameter("paramNotes",OdDbType.Text,POut.StringParam(contact.Notes));
			if(useExistingPK || PrefC.RandomKeys) {
				Db.NonQ(command,paramNotes);
			}
			else {
				contact.ContactNum=Db.NonQ(command,true,"ContactNum","contact",paramNotes);
			}
			return contact.ContactNum;
		}

		///<summary>Inserts one Contact into the database.  Returns the new priKey.  Doesn't use the cache.</summary>
		public static long InsertNoCache(Contact contact) {
			return InsertNoCache(contact,false);
		}

		///<summary>Inserts one Contact into the database.  Provides option to use the existing priKey.  Doesn't use the cache.</summary>
		public static long InsertNoCache(Contact contact,bool useExistingPK) {
			bool isRandomKeys=Prefs.GetBoolNoCache(PrefName.RandomPrimaryKeys);
			string command="INSERT INTO contact (";
			if(!useExistingPK && isRandomKeys) {
				contact.ContactNum=ReplicationServers.GetKeyNoCache("contact","ContactNum");
			}
			if(isRandomKeys || useExistingPK) {
				command+="ContactNum,";
			}
			command+="LName,FName,WkPhone,Fax,Category,Notes) VALUES(";
			if(isRandomKeys || useExistingPK) {
				command+=POut.Long(contact.ContactNum)+",";
			}
			command+=
				 "'"+POut.String(contact.LName)+"',"
				+"'"+POut.String(contact.FName)+"',"
				+"'"+POut.String(contact.WkPhone)+"',"
				+"'"+POut.String(contact.Fax)+"',"
				+    POut.Long  (contact.Category)+","
				+    DbHelper.ParamChar+"paramNotes)";
			if(contact.Notes==null) {
				contact.Notes="";
			}
			OdSqlParameter paramNotes=new OdSqlParameter("paramNotes",OdDbType.Text,POut.StringParam(contact.Notes));
			if(useExistingPK || isRandomKeys) {
				Db.NonQ(command,paramNotes);
			}
			else {
				contact.ContactNum=Db.NonQ(command,true,"ContactNum","contact",paramNotes);
			}
			return contact.ContactNum;
		}

		///<summary>Updates one Contact in the database.</summary>
		public static void Update(Contact contact) {
			string command="UPDATE contact SET "
				+"LName     = '"+POut.String(contact.LName)+"', "
				+"FName     = '"+POut.String(contact.FName)+"', "
				+"WkPhone   = '"+POut.String(contact.WkPhone)+"', "
				+"Fax       = '"+POut.String(contact.Fax)+"', "
				+"Category  =  "+POut.Long  (contact.Category)+", "
				+"Notes     =  "+DbHelper.ParamChar+"paramNotes "
				+"WHERE ContactNum = "+POut.Long(contact.ContactNum);
			if(contact.Notes==null) {
				contact.Notes="";
			}
			OdSqlParameter paramNotes=new OdSqlParameter("paramNotes",OdDbType.Text,POut.StringParam(contact.Notes));
			Db.NonQ(command,paramNotes);
		}

		///<summary>Updates one Contact in the database.  Uses an old object to compare to, and only alters changed fields.  This prevents collisions and concurrency problems in heavily used tables.  Returns true if an update occurred.</summary>
		public static bool Update(Contact contact,Contact oldContact) {
			string command="";
			if(contact.LName != oldContact.LName) {
				if(command!="") { command+=",";}
				command+="LName = '"+POut.String(contact.LName)+"'";
			}
			if(contact.FName != oldContact.FName) {
				if(command!="") { command+=",";}
				command+="FName = '"+POut.String(contact.FName)+"'";
			}
			if(contact.WkPhone != oldContact.WkPhone) {
				if(command!="") { command+=",";}
				command+="WkPhone = '"+POut.String(contact.WkPhone)+"'";
			}
			if(contact.Fax != oldContact.Fax) {
				if(command!="") { command+=",";}
				command+="Fax = '"+POut.String(contact.Fax)+"'";
			}
			if(contact.Category != oldContact.Category) {
				if(command!="") { command+=",";}
				command+="Category = "+POut.Long(contact.Category)+"";
			}
			if(contact.Notes != oldContact.Notes) {
				if(command!="") { command+=",";}
				command+="Notes = "+DbHelper.ParamChar+"paramNotes";
			}
			if(command=="") {
				return false;
			}
			if(contact.Notes==null) {
				contact.Notes="";
			}
			OdSqlParameter paramNotes=new OdSqlParameter("paramNotes",OdDbType.Text,POut.StringParam(contact.Notes));
			command="UPDATE contact SET "+command
				+" WHERE ContactNum = "+POut.Long(contact.ContactNum);
			Db.NonQ(command,paramNotes);
			return true;
		}

		///<summary>Returns true if Update(Contact,Contact) would make changes to the database.
		///Does not make any changes to the database and can be called before remoting role is checked.</summary>
		public static bool UpdateComparison(Contact contact,Contact oldContact) {
			if(contact.LName != oldContact.LName) {
				return true;
			}
			if(contact.FName != oldContact.FName) {
				return true;
			}
			if(contact.WkPhone != oldContact.WkPhone) {
				return true;
			}
			if(contact.Fax != oldContact.Fax) {
				return true;
			}
			if(contact.Category != oldContact.Category) {
				return true;
			}
			if(contact.Notes != oldContact.Notes) {
				return true;
			}
			return false;
		}

		///<summary>Deletes one Contact from the database.</summary>
		public static void Delete(long contactNum) {
			string command="DELETE FROM contact "
				+"WHERE ContactNum = "+POut.Long(contactNum);
			Db.NonQ(command);
		}

		///<summary>Deletes many Contacts from the database.</summary>
		public static void DeleteMany(List<long> listContactNums) {
			if(listContactNums==null || listContactNums.Count==0) {
				return;
			}
			string command="DELETE FROM contact "
				+"WHERE ContactNum IN("+string.Join(",",listContactNums.Select(x => POut.Long(x)))+")";
			Db.NonQ(command);
		}

	}
}