//This file is automatically generated.
//Do not attempt to make changes to this file because the changes will be erased and overwritten.
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;

namespace OpenDentBusiness.Crud{
	public class CertEmployeeCrud {
		///<summary>Gets one CertEmployee object from the database using the primary key.  Returns null if not found.</summary>
		public static CertEmployee SelectOne(long certEmployeeNum) {
			string command="SELECT * FROM certemployee "
				+"WHERE CertEmployeeNum = "+POut.Long(certEmployeeNum);
			List<CertEmployee> list=TableToList(Db.GetTable(command));
			if(list.Count==0) {
				return null;
			}
			return list[0];
		}

		///<summary>Gets one CertEmployee object from the database using a query.</summary>
		public static CertEmployee SelectOne(string command) {
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				throw new ApplicationException("Not allowed to send sql directly.  Rewrite the calling class to not use this query:\r\n"+command);
			}
			List<CertEmployee> list=TableToList(Db.GetTable(command));
			if(list.Count==0) {
				return null;
			}
			return list[0];
		}

		///<summary>Gets a list of CertEmployee objects from the database using a query.</summary>
		public static List<CertEmployee> SelectMany(string command) {
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				throw new ApplicationException("Not allowed to send sql directly.  Rewrite the calling class to not use this query:\r\n"+command);
			}
			List<CertEmployee> list=TableToList(Db.GetTable(command));
			return list;
		}

		///<summary>Converts a DataTable to a list of objects.</summary>
		public static List<CertEmployee> TableToList(DataTable table) {
			List<CertEmployee> retVal=new List<CertEmployee>();
			CertEmployee certEmployee;
			foreach(DataRow row in table.Rows) {
				certEmployee=new CertEmployee();
				certEmployee.CertEmployeeNum= PIn.Long  (row["CertEmployeeNum"].ToString());
				certEmployee.CertNum        = PIn.Long  (row["CertNum"].ToString());
				certEmployee.EmployeeNum    = PIn.Long  (row["EmployeeNum"].ToString());
				certEmployee.DateCompleted  = PIn.Date  (row["DateCompleted"].ToString());
				certEmployee.Note           = PIn.String(row["Note"].ToString());
				certEmployee.UserNum        = PIn.Long  (row["UserNum"].ToString());
				retVal.Add(certEmployee);
			}
			return retVal;
		}

		///<summary>Converts a list of CertEmployee into a DataTable.</summary>
		public static DataTable ListToTable(List<CertEmployee> listCertEmployees,string tableName="") {
			if(string.IsNullOrEmpty(tableName)) {
				tableName="CertEmployee";
			}
			DataTable table=new DataTable(tableName);
			table.Columns.Add("CertEmployeeNum");
			table.Columns.Add("CertNum");
			table.Columns.Add("EmployeeNum");
			table.Columns.Add("DateCompleted");
			table.Columns.Add("Note");
			table.Columns.Add("UserNum");
			foreach(CertEmployee certEmployee in listCertEmployees) {
				table.Rows.Add(new object[] {
					POut.Long  (certEmployee.CertEmployeeNum),
					POut.Long  (certEmployee.CertNum),
					POut.Long  (certEmployee.EmployeeNum),
					POut.DateT (certEmployee.DateCompleted,false),
					            certEmployee.Note,
					POut.Long  (certEmployee.UserNum),
				});
			}
			return table;
		}

		///<summary>Inserts one CertEmployee into the database.  Returns the new priKey.</summary>
		public static long Insert(CertEmployee certEmployee) {
			return Insert(certEmployee,false);
		}

		///<summary>Inserts one CertEmployee into the database.  Provides option to use the existing priKey.</summary>
		public static long Insert(CertEmployee certEmployee,bool useExistingPK) {
			if(!useExistingPK && PrefC.RandomKeys) {
				certEmployee.CertEmployeeNum=ReplicationServers.GetKey("certemployee","CertEmployeeNum");
			}
			string command="INSERT INTO certemployee (";
			if(useExistingPK || PrefC.RandomKeys) {
				command+="CertEmployeeNum,";
			}
			command+="CertNum,EmployeeNum,DateCompleted,Note,UserNum) VALUES(";
			if(useExistingPK || PrefC.RandomKeys) {
				command+=POut.Long(certEmployee.CertEmployeeNum)+",";
			}
			command+=
				     POut.Long  (certEmployee.CertNum)+","
				+    POut.Long  (certEmployee.EmployeeNum)+","
				+    POut.Date  (certEmployee.DateCompleted)+","
				+"'"+POut.String(certEmployee.Note)+"',"
				+    POut.Long  (certEmployee.UserNum)+")";
			if(useExistingPK || PrefC.RandomKeys) {
				Db.NonQ(command);
			}
			else {
				certEmployee.CertEmployeeNum=Db.NonQ(command,true,"CertEmployeeNum","certEmployee");
			}
			return certEmployee.CertEmployeeNum;
		}

		///<summary>Inserts one CertEmployee into the database.  Returns the new priKey.  Doesn't use the cache.</summary>
		public static long InsertNoCache(CertEmployee certEmployee) {
			return InsertNoCache(certEmployee,false);
		}

		///<summary>Inserts one CertEmployee into the database.  Provides option to use the existing priKey.  Doesn't use the cache.</summary>
		public static long InsertNoCache(CertEmployee certEmployee,bool useExistingPK) {
			bool isRandomKeys=Prefs.GetBoolNoCache(PrefName.RandomPrimaryKeys);
			string command="INSERT INTO certemployee (";
			if(!useExistingPK && isRandomKeys) {
				certEmployee.CertEmployeeNum=ReplicationServers.GetKeyNoCache("certemployee","CertEmployeeNum");
			}
			if(isRandomKeys || useExistingPK) {
				command+="CertEmployeeNum,";
			}
			command+="CertNum,EmployeeNum,DateCompleted,Note,UserNum) VALUES(";
			if(isRandomKeys || useExistingPK) {
				command+=POut.Long(certEmployee.CertEmployeeNum)+",";
			}
			command+=
				     POut.Long  (certEmployee.CertNum)+","
				+    POut.Long  (certEmployee.EmployeeNum)+","
				+    POut.Date  (certEmployee.DateCompleted)+","
				+"'"+POut.String(certEmployee.Note)+"',"
				+    POut.Long  (certEmployee.UserNum)+")";
			if(useExistingPK || isRandomKeys) {
				Db.NonQ(command);
			}
			else {
				certEmployee.CertEmployeeNum=Db.NonQ(command,true,"CertEmployeeNum","certEmployee");
			}
			return certEmployee.CertEmployeeNum;
		}

		///<summary>Updates one CertEmployee in the database.</summary>
		public static void Update(CertEmployee certEmployee) {
			string command="UPDATE certemployee SET "
				+"CertNum        =  "+POut.Long  (certEmployee.CertNum)+", "
				+"EmployeeNum    =  "+POut.Long  (certEmployee.EmployeeNum)+", "
				+"DateCompleted  =  "+POut.Date  (certEmployee.DateCompleted)+", "
				+"Note           = '"+POut.String(certEmployee.Note)+"', "
				+"UserNum        =  "+POut.Long  (certEmployee.UserNum)+" "
				+"WHERE CertEmployeeNum = "+POut.Long(certEmployee.CertEmployeeNum);
			Db.NonQ(command);
		}

		///<summary>Updates one CertEmployee in the database.  Uses an old object to compare to, and only alters changed fields.  This prevents collisions and concurrency problems in heavily used tables.  Returns true if an update occurred.</summary>
		public static bool Update(CertEmployee certEmployee,CertEmployee oldCertEmployee) {
			string command="";
			if(certEmployee.CertNum != oldCertEmployee.CertNum) {
				if(command!="") { command+=",";}
				command+="CertNum = "+POut.Long(certEmployee.CertNum)+"";
			}
			if(certEmployee.EmployeeNum != oldCertEmployee.EmployeeNum) {
				if(command!="") { command+=",";}
				command+="EmployeeNum = "+POut.Long(certEmployee.EmployeeNum)+"";
			}
			if(certEmployee.DateCompleted.Date != oldCertEmployee.DateCompleted.Date) {
				if(command!="") { command+=",";}
				command+="DateCompleted = "+POut.Date(certEmployee.DateCompleted)+"";
			}
			if(certEmployee.Note != oldCertEmployee.Note) {
				if(command!="") { command+=",";}
				command+="Note = '"+POut.String(certEmployee.Note)+"'";
			}
			if(certEmployee.UserNum != oldCertEmployee.UserNum) {
				if(command!="") { command+=",";}
				command+="UserNum = "+POut.Long(certEmployee.UserNum)+"";
			}
			if(command=="") {
				return false;
			}
			command="UPDATE certemployee SET "+command
				+" WHERE CertEmployeeNum = "+POut.Long(certEmployee.CertEmployeeNum);
			Db.NonQ(command);
			return true;
		}

		///<summary>Returns true if Update(CertEmployee,CertEmployee) would make changes to the database.
		///Does not make any changes to the database and can be called before remoting role is checked.</summary>
		public static bool UpdateComparison(CertEmployee certEmployee,CertEmployee oldCertEmployee) {
			if(certEmployee.CertNum != oldCertEmployee.CertNum) {
				return true;
			}
			if(certEmployee.EmployeeNum != oldCertEmployee.EmployeeNum) {
				return true;
			}
			if(certEmployee.DateCompleted.Date != oldCertEmployee.DateCompleted.Date) {
				return true;
			}
			if(certEmployee.Note != oldCertEmployee.Note) {
				return true;
			}
			if(certEmployee.UserNum != oldCertEmployee.UserNum) {
				return true;
			}
			return false;
		}

		///<summary>Deletes one CertEmployee from the database.</summary>
		public static void Delete(long certEmployeeNum) {
			string command="DELETE FROM certemployee "
				+"WHERE CertEmployeeNum = "+POut.Long(certEmployeeNum);
			Db.NonQ(command);
		}

		///<summary>Deletes many CertEmployees from the database.</summary>
		public static void DeleteMany(List<long> listCertEmployeeNums) {
			if(listCertEmployeeNums==null || listCertEmployeeNums.Count==0) {
				return;
			}
			string command="DELETE FROM certemployee "
				+"WHERE CertEmployeeNum IN("+string.Join(",",listCertEmployeeNums.Select(x => POut.Long(x)))+")";
			Db.NonQ(command);
		}

	}
}