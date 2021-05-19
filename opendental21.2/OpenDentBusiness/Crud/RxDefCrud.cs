//This file is automatically generated.
//Do not attempt to make changes to this file because the changes will be erased and overwritten.
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;

namespace OpenDentBusiness.Crud{
	public class RxDefCrud {
		///<summary>Gets one RxDef object from the database using the primary key.  Returns null if not found.</summary>
		public static RxDef SelectOne(long rxDefNum) {
			string command="SELECT * FROM rxdef "
				+"WHERE RxDefNum = "+POut.Long(rxDefNum);
			List<RxDef> list=TableToList(Db.GetTable(command));
			if(list.Count==0) {
				return null;
			}
			return list[0];
		}

		///<summary>Gets one RxDef object from the database using a query.</summary>
		public static RxDef SelectOne(string command) {
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				throw new ApplicationException("Not allowed to send sql directly.  Rewrite the calling class to not use this query:\r\n"+command);
			}
			List<RxDef> list=TableToList(Db.GetTable(command));
			if(list.Count==0) {
				return null;
			}
			return list[0];
		}

		///<summary>Gets a list of RxDef objects from the database using a query.</summary>
		public static List<RxDef> SelectMany(string command) {
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				throw new ApplicationException("Not allowed to send sql directly.  Rewrite the calling class to not use this query:\r\n"+command);
			}
			List<RxDef> list=TableToList(Db.GetTable(command));
			return list;
		}

		///<summary>Converts a DataTable to a list of objects.</summary>
		public static List<RxDef> TableToList(DataTable table) {
			List<RxDef> retVal=new List<RxDef>();
			RxDef rxDef;
			foreach(DataRow row in table.Rows) {
				rxDef=new RxDef();
				rxDef.RxDefNum          = PIn.Long  (row["RxDefNum"].ToString());
				rxDef.Drug              = PIn.String(row["Drug"].ToString());
				rxDef.Sig               = PIn.String(row["Sig"].ToString());
				rxDef.Disp              = PIn.String(row["Disp"].ToString());
				rxDef.Refills           = PIn.String(row["Refills"].ToString());
				rxDef.Notes             = PIn.String(row["Notes"].ToString());
				rxDef.IsControlled      = PIn.Bool  (row["IsControlled"].ToString());
				rxDef.RxCui             = PIn.Long  (row["RxCui"].ToString());
				rxDef.IsProcRequired    = PIn.Bool  (row["IsProcRequired"].ToString());
				rxDef.PatientInstruction= PIn.String(row["PatientInstruction"].ToString());
				retVal.Add(rxDef);
			}
			return retVal;
		}

		///<summary>Converts a list of RxDef into a DataTable.</summary>
		public static DataTable ListToTable(List<RxDef> listRxDefs,string tableName="") {
			if(string.IsNullOrEmpty(tableName)) {
				tableName="RxDef";
			}
			DataTable table=new DataTable(tableName);
			table.Columns.Add("RxDefNum");
			table.Columns.Add("Drug");
			table.Columns.Add("Sig");
			table.Columns.Add("Disp");
			table.Columns.Add("Refills");
			table.Columns.Add("Notes");
			table.Columns.Add("IsControlled");
			table.Columns.Add("RxCui");
			table.Columns.Add("IsProcRequired");
			table.Columns.Add("PatientInstruction");
			foreach(RxDef rxDef in listRxDefs) {
				table.Rows.Add(new object[] {
					POut.Long  (rxDef.RxDefNum),
					            rxDef.Drug,
					            rxDef.Sig,
					            rxDef.Disp,
					            rxDef.Refills,
					            rxDef.Notes,
					POut.Bool  (rxDef.IsControlled),
					POut.Long  (rxDef.RxCui),
					POut.Bool  (rxDef.IsProcRequired),
					            rxDef.PatientInstruction,
				});
			}
			return table;
		}

		///<summary>Inserts one RxDef into the database.  Returns the new priKey.</summary>
		public static long Insert(RxDef rxDef) {
			return Insert(rxDef,false);
		}

		///<summary>Inserts one RxDef into the database.  Provides option to use the existing priKey.</summary>
		public static long Insert(RxDef rxDef,bool useExistingPK) {
			if(!useExistingPK && PrefC.RandomKeys) {
				rxDef.RxDefNum=ReplicationServers.GetKey("rxdef","RxDefNum");
			}
			string command="INSERT INTO rxdef (";
			if(useExistingPK || PrefC.RandomKeys) {
				command+="RxDefNum,";
			}
			command+="Drug,Sig,Disp,Refills,Notes,IsControlled,RxCui,IsProcRequired,PatientInstruction) VALUES(";
			if(useExistingPK || PrefC.RandomKeys) {
				command+=POut.Long(rxDef.RxDefNum)+",";
			}
			command+=
				 "'"+POut.String(rxDef.Drug)+"',"
				+"'"+POut.String(rxDef.Sig)+"',"
				+"'"+POut.String(rxDef.Disp)+"',"
				+"'"+POut.String(rxDef.Refills)+"',"
				+"'"+POut.String(rxDef.Notes)+"',"
				+    POut.Bool  (rxDef.IsControlled)+","
				+    POut.Long  (rxDef.RxCui)+","
				+    POut.Bool  (rxDef.IsProcRequired)+","
				+    DbHelper.ParamChar+"paramPatientInstruction)";
			if(rxDef.PatientInstruction==null) {
				rxDef.PatientInstruction="";
			}
			OdSqlParameter paramPatientInstruction=new OdSqlParameter("paramPatientInstruction",OdDbType.Text,POut.StringParam(rxDef.PatientInstruction));
			if(useExistingPK || PrefC.RandomKeys) {
				Db.NonQ(command,paramPatientInstruction);
			}
			else {
				rxDef.RxDefNum=Db.NonQ(command,true,"RxDefNum","rxDef",paramPatientInstruction);
			}
			return rxDef.RxDefNum;
		}

		///<summary>Inserts one RxDef into the database.  Returns the new priKey.  Doesn't use the cache.</summary>
		public static long InsertNoCache(RxDef rxDef) {
			return InsertNoCache(rxDef,false);
		}

		///<summary>Inserts one RxDef into the database.  Provides option to use the existing priKey.  Doesn't use the cache.</summary>
		public static long InsertNoCache(RxDef rxDef,bool useExistingPK) {
			bool isRandomKeys=Prefs.GetBoolNoCache(PrefName.RandomPrimaryKeys);
			string command="INSERT INTO rxdef (";
			if(!useExistingPK && isRandomKeys) {
				rxDef.RxDefNum=ReplicationServers.GetKeyNoCache("rxdef","RxDefNum");
			}
			if(isRandomKeys || useExistingPK) {
				command+="RxDefNum,";
			}
			command+="Drug,Sig,Disp,Refills,Notes,IsControlled,RxCui,IsProcRequired,PatientInstruction) VALUES(";
			if(isRandomKeys || useExistingPK) {
				command+=POut.Long(rxDef.RxDefNum)+",";
			}
			command+=
				 "'"+POut.String(rxDef.Drug)+"',"
				+"'"+POut.String(rxDef.Sig)+"',"
				+"'"+POut.String(rxDef.Disp)+"',"
				+"'"+POut.String(rxDef.Refills)+"',"
				+"'"+POut.String(rxDef.Notes)+"',"
				+    POut.Bool  (rxDef.IsControlled)+","
				+    POut.Long  (rxDef.RxCui)+","
				+    POut.Bool  (rxDef.IsProcRequired)+","
				+    DbHelper.ParamChar+"paramPatientInstruction)";
			if(rxDef.PatientInstruction==null) {
				rxDef.PatientInstruction="";
			}
			OdSqlParameter paramPatientInstruction=new OdSqlParameter("paramPatientInstruction",OdDbType.Text,POut.StringParam(rxDef.PatientInstruction));
			if(useExistingPK || isRandomKeys) {
				Db.NonQ(command,paramPatientInstruction);
			}
			else {
				rxDef.RxDefNum=Db.NonQ(command,true,"RxDefNum","rxDef",paramPatientInstruction);
			}
			return rxDef.RxDefNum;
		}

		///<summary>Updates one RxDef in the database.</summary>
		public static void Update(RxDef rxDef) {
			string command="UPDATE rxdef SET "
				+"Drug              = '"+POut.String(rxDef.Drug)+"', "
				+"Sig               = '"+POut.String(rxDef.Sig)+"', "
				+"Disp              = '"+POut.String(rxDef.Disp)+"', "
				+"Refills           = '"+POut.String(rxDef.Refills)+"', "
				+"Notes             = '"+POut.String(rxDef.Notes)+"', "
				+"IsControlled      =  "+POut.Bool  (rxDef.IsControlled)+", "
				+"RxCui             =  "+POut.Long  (rxDef.RxCui)+", "
				+"IsProcRequired    =  "+POut.Bool  (rxDef.IsProcRequired)+", "
				+"PatientInstruction=  "+DbHelper.ParamChar+"paramPatientInstruction "
				+"WHERE RxDefNum = "+POut.Long(rxDef.RxDefNum);
			if(rxDef.PatientInstruction==null) {
				rxDef.PatientInstruction="";
			}
			OdSqlParameter paramPatientInstruction=new OdSqlParameter("paramPatientInstruction",OdDbType.Text,POut.StringParam(rxDef.PatientInstruction));
			Db.NonQ(command,paramPatientInstruction);
		}

		///<summary>Updates one RxDef in the database.  Uses an old object to compare to, and only alters changed fields.  This prevents collisions and concurrency problems in heavily used tables.  Returns true if an update occurred.</summary>
		public static bool Update(RxDef rxDef,RxDef oldRxDef) {
			string command="";
			if(rxDef.Drug != oldRxDef.Drug) {
				if(command!="") { command+=",";}
				command+="Drug = '"+POut.String(rxDef.Drug)+"'";
			}
			if(rxDef.Sig != oldRxDef.Sig) {
				if(command!="") { command+=",";}
				command+="Sig = '"+POut.String(rxDef.Sig)+"'";
			}
			if(rxDef.Disp != oldRxDef.Disp) {
				if(command!="") { command+=",";}
				command+="Disp = '"+POut.String(rxDef.Disp)+"'";
			}
			if(rxDef.Refills != oldRxDef.Refills) {
				if(command!="") { command+=",";}
				command+="Refills = '"+POut.String(rxDef.Refills)+"'";
			}
			if(rxDef.Notes != oldRxDef.Notes) {
				if(command!="") { command+=",";}
				command+="Notes = '"+POut.String(rxDef.Notes)+"'";
			}
			if(rxDef.IsControlled != oldRxDef.IsControlled) {
				if(command!="") { command+=",";}
				command+="IsControlled = "+POut.Bool(rxDef.IsControlled)+"";
			}
			if(rxDef.RxCui != oldRxDef.RxCui) {
				if(command!="") { command+=",";}
				command+="RxCui = "+POut.Long(rxDef.RxCui)+"";
			}
			if(rxDef.IsProcRequired != oldRxDef.IsProcRequired) {
				if(command!="") { command+=",";}
				command+="IsProcRequired = "+POut.Bool(rxDef.IsProcRequired)+"";
			}
			if(rxDef.PatientInstruction != oldRxDef.PatientInstruction) {
				if(command!="") { command+=",";}
				command+="PatientInstruction = "+DbHelper.ParamChar+"paramPatientInstruction";
			}
			if(command=="") {
				return false;
			}
			if(rxDef.PatientInstruction==null) {
				rxDef.PatientInstruction="";
			}
			OdSqlParameter paramPatientInstruction=new OdSqlParameter("paramPatientInstruction",OdDbType.Text,POut.StringParam(rxDef.PatientInstruction));
			command="UPDATE rxdef SET "+command
				+" WHERE RxDefNum = "+POut.Long(rxDef.RxDefNum);
			Db.NonQ(command,paramPatientInstruction);
			return true;
		}

		///<summary>Returns true if Update(RxDef,RxDef) would make changes to the database.
		///Does not make any changes to the database and can be called before remoting role is checked.</summary>
		public static bool UpdateComparison(RxDef rxDef,RxDef oldRxDef) {
			if(rxDef.Drug != oldRxDef.Drug) {
				return true;
			}
			if(rxDef.Sig != oldRxDef.Sig) {
				return true;
			}
			if(rxDef.Disp != oldRxDef.Disp) {
				return true;
			}
			if(rxDef.Refills != oldRxDef.Refills) {
				return true;
			}
			if(rxDef.Notes != oldRxDef.Notes) {
				return true;
			}
			if(rxDef.IsControlled != oldRxDef.IsControlled) {
				return true;
			}
			if(rxDef.RxCui != oldRxDef.RxCui) {
				return true;
			}
			if(rxDef.IsProcRequired != oldRxDef.IsProcRequired) {
				return true;
			}
			if(rxDef.PatientInstruction != oldRxDef.PatientInstruction) {
				return true;
			}
			return false;
		}

		///<summary>Deletes one RxDef from the database.</summary>
		public static void Delete(long rxDefNum) {
			string command="DELETE FROM rxdef "
				+"WHERE RxDefNum = "+POut.Long(rxDefNum);
			Db.NonQ(command);
		}

		///<summary>Deletes many RxDefs from the database.</summary>
		public static void DeleteMany(List<long> listRxDefNums) {
			if(listRxDefNums==null || listRxDefNums.Count==0) {
				return;
			}
			string command="DELETE FROM rxdef "
				+"WHERE RxDefNum IN("+string.Join(",",listRxDefNums.Select(x => POut.Long(x)))+")";
			Db.NonQ(command);
		}

	}
}