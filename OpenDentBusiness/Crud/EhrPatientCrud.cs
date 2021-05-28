//This file is automatically generated.
//Do not attempt to make changes to this file because the changes will be erased and overwritten.
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;

namespace OpenDentBusiness.Crud{
	public class EhrPatientCrud {
		///<summary>Gets one EhrPatient object from the database using the primary key.  Returns null if not found.</summary>
		public static EhrPatient SelectOne(long patNum) {
			string command="SELECT * FROM ehrpatient "
				+"WHERE PatNum = "+POut.Long(patNum);
			List<EhrPatient> list=TableToList(Db.GetTable(command));
			if(list.Count==0) {
				return null;
			}
			return list[0];
		}

		///<summary>Gets one EhrPatient object from the database using a query.</summary>
		public static EhrPatient SelectOne(string command) {
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				throw new ApplicationException("Not allowed to send sql directly.  Rewrite the calling class to not use this query:\r\n"+command);
			}
			List<EhrPatient> list=TableToList(Db.GetTable(command));
			if(list.Count==0) {
				return null;
			}
			return list[0];
		}

		///<summary>Gets a list of EhrPatient objects from the database using a query.</summary>
		public static List<EhrPatient> SelectMany(string command) {
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				throw new ApplicationException("Not allowed to send sql directly.  Rewrite the calling class to not use this query:\r\n"+command);
			}
			List<EhrPatient> list=TableToList(Db.GetTable(command));
			return list;
		}

		///<summary>Converts a DataTable to a list of objects.</summary>
		public static List<EhrPatient> TableToList(DataTable table) {
			List<EhrPatient> retVal=new List<EhrPatient>();
			EhrPatient ehrPatient;
			foreach(DataRow row in table.Rows) {
				ehrPatient=new EhrPatient();
				ehrPatient.PatNum               = PIn.Long  (row["PatNum"].ToString());
				ehrPatient.MotherMaidenFname    = PIn.String(row["MotherMaidenFname"].ToString());
				ehrPatient.MotherMaidenLname    = PIn.String(row["MotherMaidenLname"].ToString());
				ehrPatient.VacShareOk           = (OpenDentBusiness.YN)PIn.Int(row["VacShareOk"].ToString());
				ehrPatient.MedicaidState        = PIn.String(row["MedicaidState"].ToString());
				ehrPatient.SexualOrientation    = PIn.String(row["SexualOrientation"].ToString());
				ehrPatient.GenderIdentity       = PIn.String(row["GenderIdentity"].ToString());
				ehrPatient.SexualOrientationNote= PIn.String(row["SexualOrientationNote"].ToString());
				ehrPatient.GenderIdentityNote   = PIn.String(row["GenderIdentityNote"].ToString());
				retVal.Add(ehrPatient);
			}
			return retVal;
		}

		///<summary>Converts a list of EhrPatient into a DataTable.</summary>
		public static DataTable ListToTable(List<EhrPatient> listEhrPatients,string tableName="") {
			if(string.IsNullOrEmpty(tableName)) {
				tableName="EhrPatient";
			}
			DataTable table=new DataTable(tableName);
			table.Columns.Add("PatNum");
			table.Columns.Add("MotherMaidenFname");
			table.Columns.Add("MotherMaidenLname");
			table.Columns.Add("VacShareOk");
			table.Columns.Add("MedicaidState");
			table.Columns.Add("SexualOrientation");
			table.Columns.Add("GenderIdentity");
			table.Columns.Add("SexualOrientationNote");
			table.Columns.Add("GenderIdentityNote");
			foreach(EhrPatient ehrPatient in listEhrPatients) {
				table.Rows.Add(new object[] {
					POut.Long  (ehrPatient.PatNum),
					            ehrPatient.MotherMaidenFname,
					            ehrPatient.MotherMaidenLname,
					POut.Int   ((int)ehrPatient.VacShareOk),
					            ehrPatient.MedicaidState,
					            ehrPatient.SexualOrientation,
					            ehrPatient.GenderIdentity,
					            ehrPatient.SexualOrientationNote,
					            ehrPatient.GenderIdentityNote,
				});
			}
			return table;
		}

		///<summary>Inserts one EhrPatient into the database.  Returns the new priKey.</summary>
		public static long Insert(EhrPatient ehrPatient) {
			return Insert(ehrPatient,false);
		}

		///<summary>Inserts one EhrPatient into the database.  Provides option to use the existing priKey.</summary>
		public static long Insert(EhrPatient ehrPatient,bool useExistingPK) {
			if(!useExistingPK && PrefC.RandomKeys) {
				ehrPatient.PatNum=ReplicationServers.GetKey("ehrpatient","PatNum");
			}
			string command="INSERT INTO ehrpatient (";
			if(useExistingPK || PrefC.RandomKeys) {
				command+="PatNum,";
			}
			command+="MotherMaidenFname,MotherMaidenLname,VacShareOk,MedicaidState,SexualOrientation,GenderIdentity,SexualOrientationNote,GenderIdentityNote) VALUES(";
			if(useExistingPK || PrefC.RandomKeys) {
				command+=POut.Long(ehrPatient.PatNum)+",";
			}
			command+=
				 "'"+POut.String(ehrPatient.MotherMaidenFname)+"',"
				+"'"+POut.String(ehrPatient.MotherMaidenLname)+"',"
				+    POut.Int   ((int)ehrPatient.VacShareOk)+","
				+"'"+POut.String(ehrPatient.MedicaidState)+"',"
				+"'"+POut.String(ehrPatient.SexualOrientation)+"',"
				+"'"+POut.String(ehrPatient.GenderIdentity)+"',"
				+"'"+POut.String(ehrPatient.SexualOrientationNote)+"',"
				+"'"+POut.String(ehrPatient.GenderIdentityNote)+"')";
			if(useExistingPK || PrefC.RandomKeys) {
				Db.NonQ(command);
			}
			else {
				ehrPatient.PatNum=Db.NonQ(command,true,"PatNum","ehrPatient");
			}
			return ehrPatient.PatNum;
		}

		///<summary>Inserts one EhrPatient into the database.  Returns the new priKey.  Doesn't use the cache.</summary>
		public static long InsertNoCache(EhrPatient ehrPatient) {
			return InsertNoCache(ehrPatient,false);
		}

		///<summary>Inserts one EhrPatient into the database.  Provides option to use the existing priKey.  Doesn't use the cache.</summary>
		public static long InsertNoCache(EhrPatient ehrPatient,bool useExistingPK) {
			bool isRandomKeys=Prefs.GetBoolNoCache(PrefName.RandomPrimaryKeys);
			string command="INSERT INTO ehrpatient (";
			if(!useExistingPK && isRandomKeys) {
				ehrPatient.PatNum=ReplicationServers.GetKeyNoCache("ehrpatient","PatNum");
			}
			if(isRandomKeys || useExistingPK) {
				command+="PatNum,";
			}
			command+="MotherMaidenFname,MotherMaidenLname,VacShareOk,MedicaidState,SexualOrientation,GenderIdentity,SexualOrientationNote,GenderIdentityNote) VALUES(";
			if(isRandomKeys || useExistingPK) {
				command+=POut.Long(ehrPatient.PatNum)+",";
			}
			command+=
				 "'"+POut.String(ehrPatient.MotherMaidenFname)+"',"
				+"'"+POut.String(ehrPatient.MotherMaidenLname)+"',"
				+    POut.Int   ((int)ehrPatient.VacShareOk)+","
				+"'"+POut.String(ehrPatient.MedicaidState)+"',"
				+"'"+POut.String(ehrPatient.SexualOrientation)+"',"
				+"'"+POut.String(ehrPatient.GenderIdentity)+"',"
				+"'"+POut.String(ehrPatient.SexualOrientationNote)+"',"
				+"'"+POut.String(ehrPatient.GenderIdentityNote)+"')";
			if(useExistingPK || isRandomKeys) {
				Db.NonQ(command);
			}
			else {
				ehrPatient.PatNum=Db.NonQ(command,true,"PatNum","ehrPatient");
			}
			return ehrPatient.PatNum;
		}

		///<summary>Updates one EhrPatient in the database.</summary>
		public static void Update(EhrPatient ehrPatient) {
			string command="UPDATE ehrpatient SET "
				+"MotherMaidenFname    = '"+POut.String(ehrPatient.MotherMaidenFname)+"', "
				+"MotherMaidenLname    = '"+POut.String(ehrPatient.MotherMaidenLname)+"', "
				+"VacShareOk           =  "+POut.Int   ((int)ehrPatient.VacShareOk)+", "
				+"MedicaidState        = '"+POut.String(ehrPatient.MedicaidState)+"', "
				+"SexualOrientation    = '"+POut.String(ehrPatient.SexualOrientation)+"', "
				+"GenderIdentity       = '"+POut.String(ehrPatient.GenderIdentity)+"', "
				+"SexualOrientationNote= '"+POut.String(ehrPatient.SexualOrientationNote)+"', "
				+"GenderIdentityNote   = '"+POut.String(ehrPatient.GenderIdentityNote)+"' "
				+"WHERE PatNum = "+POut.Long(ehrPatient.PatNum);
			Db.NonQ(command);
		}

		///<summary>Updates one EhrPatient in the database.  Uses an old object to compare to, and only alters changed fields.  This prevents collisions and concurrency problems in heavily used tables.  Returns true if an update occurred.</summary>
		public static bool Update(EhrPatient ehrPatient,EhrPatient oldEhrPatient) {
			string command="";
			if(ehrPatient.MotherMaidenFname != oldEhrPatient.MotherMaidenFname) {
				if(command!="") { command+=",";}
				command+="MotherMaidenFname = '"+POut.String(ehrPatient.MotherMaidenFname)+"'";
			}
			if(ehrPatient.MotherMaidenLname != oldEhrPatient.MotherMaidenLname) {
				if(command!="") { command+=",";}
				command+="MotherMaidenLname = '"+POut.String(ehrPatient.MotherMaidenLname)+"'";
			}
			if(ehrPatient.VacShareOk != oldEhrPatient.VacShareOk) {
				if(command!="") { command+=",";}
				command+="VacShareOk = "+POut.Int   ((int)ehrPatient.VacShareOk)+"";
			}
			if(ehrPatient.MedicaidState != oldEhrPatient.MedicaidState) {
				if(command!="") { command+=",";}
				command+="MedicaidState = '"+POut.String(ehrPatient.MedicaidState)+"'";
			}
			if(ehrPatient.SexualOrientation != oldEhrPatient.SexualOrientation) {
				if(command!="") { command+=",";}
				command+="SexualOrientation = '"+POut.String(ehrPatient.SexualOrientation)+"'";
			}
			if(ehrPatient.GenderIdentity != oldEhrPatient.GenderIdentity) {
				if(command!="") { command+=",";}
				command+="GenderIdentity = '"+POut.String(ehrPatient.GenderIdentity)+"'";
			}
			if(ehrPatient.SexualOrientationNote != oldEhrPatient.SexualOrientationNote) {
				if(command!="") { command+=",";}
				command+="SexualOrientationNote = '"+POut.String(ehrPatient.SexualOrientationNote)+"'";
			}
			if(ehrPatient.GenderIdentityNote != oldEhrPatient.GenderIdentityNote) {
				if(command!="") { command+=",";}
				command+="GenderIdentityNote = '"+POut.String(ehrPatient.GenderIdentityNote)+"'";
			}
			if(command=="") {
				return false;
			}
			command="UPDATE ehrpatient SET "+command
				+" WHERE PatNum = "+POut.Long(ehrPatient.PatNum);
			Db.NonQ(command);
			return true;
		}

		///<summary>Returns true if Update(EhrPatient,EhrPatient) would make changes to the database.
		///Does not make any changes to the database and can be called before remoting role is checked.</summary>
		public static bool UpdateComparison(EhrPatient ehrPatient,EhrPatient oldEhrPatient) {
			if(ehrPatient.MotherMaidenFname != oldEhrPatient.MotherMaidenFname) {
				return true;
			}
			if(ehrPatient.MotherMaidenLname != oldEhrPatient.MotherMaidenLname) {
				return true;
			}
			if(ehrPatient.VacShareOk != oldEhrPatient.VacShareOk) {
				return true;
			}
			if(ehrPatient.MedicaidState != oldEhrPatient.MedicaidState) {
				return true;
			}
			if(ehrPatient.SexualOrientation != oldEhrPatient.SexualOrientation) {
				return true;
			}
			if(ehrPatient.GenderIdentity != oldEhrPatient.GenderIdentity) {
				return true;
			}
			if(ehrPatient.SexualOrientationNote != oldEhrPatient.SexualOrientationNote) {
				return true;
			}
			if(ehrPatient.GenderIdentityNote != oldEhrPatient.GenderIdentityNote) {
				return true;
			}
			return false;
		}

		///<summary>Deletes one EhrPatient from the database.</summary>
		public static void Delete(long patNum) {
			string command="DELETE FROM ehrpatient "
				+"WHERE PatNum = "+POut.Long(patNum);
			Db.NonQ(command);
		}

		///<summary>Deletes many EhrPatients from the database.</summary>
		public static void DeleteMany(List<long> listPatNums) {
			if(listPatNums==null || listPatNums.Count==0) {
				return;
			}
			string command="DELETE FROM ehrpatient "
				+"WHERE PatNum IN("+string.Join(",",listPatNums.Select(x => POut.Long(x)))+")";
			Db.NonQ(command);
		}

	}
}