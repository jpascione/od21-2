//This file is automatically generated.
//Do not attempt to make changes to this file because the changes will be erased and overwritten.
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;

namespace OpenDentBusiness.Crud{
	public class SchoolClassCrud {
		///<summary>Gets one SchoolClass object from the database using the primary key.  Returns null if not found.</summary>
		public static SchoolClass SelectOne(long schoolClassNum) {
			string command="SELECT * FROM schoolclass "
				+"WHERE SchoolClassNum = "+POut.Long(schoolClassNum);
			List<SchoolClass> list=TableToList(Db.GetTable(command));
			if(list.Count==0) {
				return null;
			}
			return list[0];
		}

		///<summary>Gets one SchoolClass object from the database using a query.</summary>
		public static SchoolClass SelectOne(string command) {
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				throw new ApplicationException("Not allowed to send sql directly.  Rewrite the calling class to not use this query:\r\n"+command);
			}
			List<SchoolClass> list=TableToList(Db.GetTable(command));
			if(list.Count==0) {
				return null;
			}
			return list[0];
		}

		///<summary>Gets a list of SchoolClass objects from the database using a query.</summary>
		public static List<SchoolClass> SelectMany(string command) {
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				throw new ApplicationException("Not allowed to send sql directly.  Rewrite the calling class to not use this query:\r\n"+command);
			}
			List<SchoolClass> list=TableToList(Db.GetTable(command));
			return list;
		}

		///<summary>Converts a DataTable to a list of objects.</summary>
		public static List<SchoolClass> TableToList(DataTable table) {
			List<SchoolClass> retVal=new List<SchoolClass>();
			SchoolClass schoolClass;
			foreach(DataRow row in table.Rows) {
				schoolClass=new SchoolClass();
				schoolClass.SchoolClassNum= PIn.Long  (row["SchoolClassNum"].ToString());
				schoolClass.GradYear      = PIn.Int   (row["GradYear"].ToString());
				schoolClass.Descript      = PIn.String(row["Descript"].ToString());
				retVal.Add(schoolClass);
			}
			return retVal;
		}

		///<summary>Converts a list of SchoolClass into a DataTable.</summary>
		public static DataTable ListToTable(List<SchoolClass> listSchoolClasss,string tableName="") {
			if(string.IsNullOrEmpty(tableName)) {
				tableName="SchoolClass";
			}
			DataTable table=new DataTable(tableName);
			table.Columns.Add("SchoolClassNum");
			table.Columns.Add("GradYear");
			table.Columns.Add("Descript");
			foreach(SchoolClass schoolClass in listSchoolClasss) {
				table.Rows.Add(new object[] {
					POut.Long  (schoolClass.SchoolClassNum),
					POut.Int   (schoolClass.GradYear),
					            schoolClass.Descript,
				});
			}
			return table;
		}

		///<summary>Inserts one SchoolClass into the database.  Returns the new priKey.</summary>
		public static long Insert(SchoolClass schoolClass) {
			return Insert(schoolClass,false);
		}

		///<summary>Inserts one SchoolClass into the database.  Provides option to use the existing priKey.</summary>
		public static long Insert(SchoolClass schoolClass,bool useExistingPK) {
			if(!useExistingPK && PrefC.RandomKeys) {
				schoolClass.SchoolClassNum=ReplicationServers.GetKey("schoolclass","SchoolClassNum");
			}
			string command="INSERT INTO schoolclass (";
			if(useExistingPK || PrefC.RandomKeys) {
				command+="SchoolClassNum,";
			}
			command+="GradYear,Descript) VALUES(";
			if(useExistingPK || PrefC.RandomKeys) {
				command+=POut.Long(schoolClass.SchoolClassNum)+",";
			}
			command+=
				     POut.Int   (schoolClass.GradYear)+","
				+"'"+POut.String(schoolClass.Descript)+"')";
			if(useExistingPK || PrefC.RandomKeys) {
				Db.NonQ(command);
			}
			else {
				schoolClass.SchoolClassNum=Db.NonQ(command,true,"SchoolClassNum","schoolClass");
			}
			return schoolClass.SchoolClassNum;
		}

		///<summary>Inserts one SchoolClass into the database.  Returns the new priKey.  Doesn't use the cache.</summary>
		public static long InsertNoCache(SchoolClass schoolClass) {
			return InsertNoCache(schoolClass,false);
		}

		///<summary>Inserts one SchoolClass into the database.  Provides option to use the existing priKey.  Doesn't use the cache.</summary>
		public static long InsertNoCache(SchoolClass schoolClass,bool useExistingPK) {
			bool isRandomKeys=Prefs.GetBoolNoCache(PrefName.RandomPrimaryKeys);
			string command="INSERT INTO schoolclass (";
			if(!useExistingPK && isRandomKeys) {
				schoolClass.SchoolClassNum=ReplicationServers.GetKeyNoCache("schoolclass","SchoolClassNum");
			}
			if(isRandomKeys || useExistingPK) {
				command+="SchoolClassNum,";
			}
			command+="GradYear,Descript) VALUES(";
			if(isRandomKeys || useExistingPK) {
				command+=POut.Long(schoolClass.SchoolClassNum)+",";
			}
			command+=
				     POut.Int   (schoolClass.GradYear)+","
				+"'"+POut.String(schoolClass.Descript)+"')";
			if(useExistingPK || isRandomKeys) {
				Db.NonQ(command);
			}
			else {
				schoolClass.SchoolClassNum=Db.NonQ(command,true,"SchoolClassNum","schoolClass");
			}
			return schoolClass.SchoolClassNum;
		}

		///<summary>Updates one SchoolClass in the database.</summary>
		public static void Update(SchoolClass schoolClass) {
			string command="UPDATE schoolclass SET "
				+"GradYear      =  "+POut.Int   (schoolClass.GradYear)+", "
				+"Descript      = '"+POut.String(schoolClass.Descript)+"' "
				+"WHERE SchoolClassNum = "+POut.Long(schoolClass.SchoolClassNum);
			Db.NonQ(command);
		}

		///<summary>Updates one SchoolClass in the database.  Uses an old object to compare to, and only alters changed fields.  This prevents collisions and concurrency problems in heavily used tables.  Returns true if an update occurred.</summary>
		public static bool Update(SchoolClass schoolClass,SchoolClass oldSchoolClass) {
			string command="";
			if(schoolClass.GradYear != oldSchoolClass.GradYear) {
				if(command!="") { command+=",";}
				command+="GradYear = "+POut.Int(schoolClass.GradYear)+"";
			}
			if(schoolClass.Descript != oldSchoolClass.Descript) {
				if(command!="") { command+=",";}
				command+="Descript = '"+POut.String(schoolClass.Descript)+"'";
			}
			if(command=="") {
				return false;
			}
			command="UPDATE schoolclass SET "+command
				+" WHERE SchoolClassNum = "+POut.Long(schoolClass.SchoolClassNum);
			Db.NonQ(command);
			return true;
		}

		///<summary>Returns true if Update(SchoolClass,SchoolClass) would make changes to the database.
		///Does not make any changes to the database and can be called before remoting role is checked.</summary>
		public static bool UpdateComparison(SchoolClass schoolClass,SchoolClass oldSchoolClass) {
			if(schoolClass.GradYear != oldSchoolClass.GradYear) {
				return true;
			}
			if(schoolClass.Descript != oldSchoolClass.Descript) {
				return true;
			}
			return false;
		}

		///<summary>Deletes one SchoolClass from the database.</summary>
		public static void Delete(long schoolClassNum) {
			string command="DELETE FROM schoolclass "
				+"WHERE SchoolClassNum = "+POut.Long(schoolClassNum);
			Db.NonQ(command);
		}

		///<summary>Deletes many SchoolClasss from the database.</summary>
		public static void DeleteMany(List<long> listSchoolClassNums) {
			if(listSchoolClassNums==null || listSchoolClassNums.Count==0) {
				return;
			}
			string command="DELETE FROM schoolclass "
				+"WHERE SchoolClassNum IN("+string.Join(",",listSchoolClassNums.Select(x => POut.Long(x)))+")";
			Db.NonQ(command);
		}

	}
}