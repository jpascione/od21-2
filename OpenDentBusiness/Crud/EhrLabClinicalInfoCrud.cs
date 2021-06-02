//This file is automatically generated.
//Do not attempt to make changes to this file because the changes will be erased and overwritten.
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using EhrLaboratories;

namespace OpenDentBusiness.Crud{
	public class EhrLabClinicalInfoCrud {
		///<summary>Gets one EhrLabClinicalInfo object from the database using the primary key.  Returns null if not found.</summary>
		public static EhrLabClinicalInfo SelectOne(long ehrLabClinicalInfoNum) {
			string command="SELECT * FROM ehrlabclinicalinfo "
				+"WHERE EhrLabClinicalInfoNum = "+POut.Long(ehrLabClinicalInfoNum);
			List<EhrLabClinicalInfo> list=TableToList(Db.GetTable(command));
			if(list.Count==0) {
				return null;
			}
			return list[0];
		}

		///<summary>Gets one EhrLabClinicalInfo object from the database using a query.</summary>
		public static EhrLabClinicalInfo SelectOne(string command) {
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				throw new ApplicationException("Not allowed to send sql directly.  Rewrite the calling class to not use this query:\r\n"+command);
			}
			List<EhrLabClinicalInfo> list=TableToList(Db.GetTable(command));
			if(list.Count==0) {
				return null;
			}
			return list[0];
		}

		///<summary>Gets a list of EhrLabClinicalInfo objects from the database using a query.</summary>
		public static List<EhrLabClinicalInfo> SelectMany(string command) {
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				throw new ApplicationException("Not allowed to send sql directly.  Rewrite the calling class to not use this query:\r\n"+command);
			}
			List<EhrLabClinicalInfo> list=TableToList(Db.GetTable(command));
			return list;
		}

		///<summary>Converts a DataTable to a list of objects.</summary>
		public static List<EhrLabClinicalInfo> TableToList(DataTable table) {
			List<EhrLabClinicalInfo> retVal=new List<EhrLabClinicalInfo>();
			EhrLabClinicalInfo ehrLabClinicalInfo;
			foreach(DataRow row in table.Rows) {
				ehrLabClinicalInfo=new EhrLabClinicalInfo();
				ehrLabClinicalInfo.EhrLabClinicalInfoNum        = PIn.Long  (row["EhrLabClinicalInfoNum"].ToString());
				ehrLabClinicalInfo.EhrLabNum                    = PIn.Long  (row["EhrLabNum"].ToString());
				ehrLabClinicalInfo.ClinicalInfoID               = PIn.String(row["ClinicalInfoID"].ToString());
				ehrLabClinicalInfo.ClinicalInfoText             = PIn.String(row["ClinicalInfoText"].ToString());
				ehrLabClinicalInfo.ClinicalInfoCodeSystemName   = PIn.String(row["ClinicalInfoCodeSystemName"].ToString());
				ehrLabClinicalInfo.ClinicalInfoIDAlt            = PIn.String(row["ClinicalInfoIDAlt"].ToString());
				ehrLabClinicalInfo.ClinicalInfoTextAlt          = PIn.String(row["ClinicalInfoTextAlt"].ToString());
				ehrLabClinicalInfo.ClinicalInfoCodeSystemNameAlt= PIn.String(row["ClinicalInfoCodeSystemNameAlt"].ToString());
				ehrLabClinicalInfo.ClinicalInfoTextOriginal     = PIn.String(row["ClinicalInfoTextOriginal"].ToString());
				retVal.Add(ehrLabClinicalInfo);
			}
			return retVal;
		}

		///<summary>Converts a list of EhrLabClinicalInfo into a DataTable.</summary>
		public static DataTable ListToTable(List<EhrLabClinicalInfo> listEhrLabClinicalInfos,string tableName="") {
			if(string.IsNullOrEmpty(tableName)) {
				tableName="EhrLabClinicalInfo";
			}
			DataTable table=new DataTable(tableName);
			table.Columns.Add("EhrLabClinicalInfoNum");
			table.Columns.Add("EhrLabNum");
			table.Columns.Add("ClinicalInfoID");
			table.Columns.Add("ClinicalInfoText");
			table.Columns.Add("ClinicalInfoCodeSystemName");
			table.Columns.Add("ClinicalInfoIDAlt");
			table.Columns.Add("ClinicalInfoTextAlt");
			table.Columns.Add("ClinicalInfoCodeSystemNameAlt");
			table.Columns.Add("ClinicalInfoTextOriginal");
			foreach(EhrLabClinicalInfo ehrLabClinicalInfo in listEhrLabClinicalInfos) {
				table.Rows.Add(new object[] {
					POut.Long  (ehrLabClinicalInfo.EhrLabClinicalInfoNum),
					POut.Long  (ehrLabClinicalInfo.EhrLabNum),
					            ehrLabClinicalInfo.ClinicalInfoID,
					            ehrLabClinicalInfo.ClinicalInfoText,
					            ehrLabClinicalInfo.ClinicalInfoCodeSystemName,
					            ehrLabClinicalInfo.ClinicalInfoIDAlt,
					            ehrLabClinicalInfo.ClinicalInfoTextAlt,
					            ehrLabClinicalInfo.ClinicalInfoCodeSystemNameAlt,
					            ehrLabClinicalInfo.ClinicalInfoTextOriginal,
				});
			}
			return table;
		}

		///<summary>Inserts one EhrLabClinicalInfo into the database.  Returns the new priKey.</summary>
		public static long Insert(EhrLabClinicalInfo ehrLabClinicalInfo) {
			return Insert(ehrLabClinicalInfo,false);
		}

		///<summary>Inserts one EhrLabClinicalInfo into the database.  Provides option to use the existing priKey.</summary>
		public static long Insert(EhrLabClinicalInfo ehrLabClinicalInfo,bool useExistingPK) {
			if(!useExistingPK && PrefC.RandomKeys) {
				ehrLabClinicalInfo.EhrLabClinicalInfoNum=ReplicationServers.GetKey("ehrlabclinicalinfo","EhrLabClinicalInfoNum");
			}
			string command="INSERT INTO ehrlabclinicalinfo (";
			if(useExistingPK || PrefC.RandomKeys) {
				command+="EhrLabClinicalInfoNum,";
			}
			command+="EhrLabNum,ClinicalInfoID,ClinicalInfoText,ClinicalInfoCodeSystemName,ClinicalInfoIDAlt,ClinicalInfoTextAlt,ClinicalInfoCodeSystemNameAlt,ClinicalInfoTextOriginal) VALUES(";
			if(useExistingPK || PrefC.RandomKeys) {
				command+=POut.Long(ehrLabClinicalInfo.EhrLabClinicalInfoNum)+",";
			}
			command+=
				     POut.Long  (ehrLabClinicalInfo.EhrLabNum)+","
				+"'"+POut.String(ehrLabClinicalInfo.ClinicalInfoID)+"',"
				+"'"+POut.String(ehrLabClinicalInfo.ClinicalInfoText)+"',"
				+"'"+POut.String(ehrLabClinicalInfo.ClinicalInfoCodeSystemName)+"',"
				+"'"+POut.String(ehrLabClinicalInfo.ClinicalInfoIDAlt)+"',"
				+"'"+POut.String(ehrLabClinicalInfo.ClinicalInfoTextAlt)+"',"
				+"'"+POut.String(ehrLabClinicalInfo.ClinicalInfoCodeSystemNameAlt)+"',"
				+"'"+POut.String(ehrLabClinicalInfo.ClinicalInfoTextOriginal)+"')";
			if(useExistingPK || PrefC.RandomKeys) {
				Db.NonQ(command);
			}
			else {
				ehrLabClinicalInfo.EhrLabClinicalInfoNum=Db.NonQ(command,true,"EhrLabClinicalInfoNum","ehrLabClinicalInfo");
			}
			return ehrLabClinicalInfo.EhrLabClinicalInfoNum;
		}

		///<summary>Inserts one EhrLabClinicalInfo into the database.  Returns the new priKey.  Doesn't use the cache.</summary>
		public static long InsertNoCache(EhrLabClinicalInfo ehrLabClinicalInfo) {
			return InsertNoCache(ehrLabClinicalInfo,false);
		}

		///<summary>Inserts one EhrLabClinicalInfo into the database.  Provides option to use the existing priKey.  Doesn't use the cache.</summary>
		public static long InsertNoCache(EhrLabClinicalInfo ehrLabClinicalInfo,bool useExistingPK) {
			bool isRandomKeys=Prefs.GetBoolNoCache(PrefName.RandomPrimaryKeys);
			string command="INSERT INTO ehrlabclinicalinfo (";
			if(!useExistingPK && isRandomKeys) {
				ehrLabClinicalInfo.EhrLabClinicalInfoNum=ReplicationServers.GetKeyNoCache("ehrlabclinicalinfo","EhrLabClinicalInfoNum");
			}
			if(isRandomKeys || useExistingPK) {
				command+="EhrLabClinicalInfoNum,";
			}
			command+="EhrLabNum,ClinicalInfoID,ClinicalInfoText,ClinicalInfoCodeSystemName,ClinicalInfoIDAlt,ClinicalInfoTextAlt,ClinicalInfoCodeSystemNameAlt,ClinicalInfoTextOriginal) VALUES(";
			if(isRandomKeys || useExistingPK) {
				command+=POut.Long(ehrLabClinicalInfo.EhrLabClinicalInfoNum)+",";
			}
			command+=
				     POut.Long  (ehrLabClinicalInfo.EhrLabNum)+","
				+"'"+POut.String(ehrLabClinicalInfo.ClinicalInfoID)+"',"
				+"'"+POut.String(ehrLabClinicalInfo.ClinicalInfoText)+"',"
				+"'"+POut.String(ehrLabClinicalInfo.ClinicalInfoCodeSystemName)+"',"
				+"'"+POut.String(ehrLabClinicalInfo.ClinicalInfoIDAlt)+"',"
				+"'"+POut.String(ehrLabClinicalInfo.ClinicalInfoTextAlt)+"',"
				+"'"+POut.String(ehrLabClinicalInfo.ClinicalInfoCodeSystemNameAlt)+"',"
				+"'"+POut.String(ehrLabClinicalInfo.ClinicalInfoTextOriginal)+"')";
			if(useExistingPK || isRandomKeys) {
				Db.NonQ(command);
			}
			else {
				ehrLabClinicalInfo.EhrLabClinicalInfoNum=Db.NonQ(command,true,"EhrLabClinicalInfoNum","ehrLabClinicalInfo");
			}
			return ehrLabClinicalInfo.EhrLabClinicalInfoNum;
		}

		///<summary>Updates one EhrLabClinicalInfo in the database.</summary>
		public static void Update(EhrLabClinicalInfo ehrLabClinicalInfo) {
			string command="UPDATE ehrlabclinicalinfo SET "
				+"EhrLabNum                    =  "+POut.Long  (ehrLabClinicalInfo.EhrLabNum)+", "
				+"ClinicalInfoID               = '"+POut.String(ehrLabClinicalInfo.ClinicalInfoID)+"', "
				+"ClinicalInfoText             = '"+POut.String(ehrLabClinicalInfo.ClinicalInfoText)+"', "
				+"ClinicalInfoCodeSystemName   = '"+POut.String(ehrLabClinicalInfo.ClinicalInfoCodeSystemName)+"', "
				+"ClinicalInfoIDAlt            = '"+POut.String(ehrLabClinicalInfo.ClinicalInfoIDAlt)+"', "
				+"ClinicalInfoTextAlt          = '"+POut.String(ehrLabClinicalInfo.ClinicalInfoTextAlt)+"', "
				+"ClinicalInfoCodeSystemNameAlt= '"+POut.String(ehrLabClinicalInfo.ClinicalInfoCodeSystemNameAlt)+"', "
				+"ClinicalInfoTextOriginal     = '"+POut.String(ehrLabClinicalInfo.ClinicalInfoTextOriginal)+"' "
				+"WHERE EhrLabClinicalInfoNum = "+POut.Long(ehrLabClinicalInfo.EhrLabClinicalInfoNum);
			Db.NonQ(command);
		}

		///<summary>Updates one EhrLabClinicalInfo in the database.  Uses an old object to compare to, and only alters changed fields.  This prevents collisions and concurrency problems in heavily used tables.  Returns true if an update occurred.</summary>
		public static bool Update(EhrLabClinicalInfo ehrLabClinicalInfo,EhrLabClinicalInfo oldEhrLabClinicalInfo) {
			string command="";
			if(ehrLabClinicalInfo.EhrLabNum != oldEhrLabClinicalInfo.EhrLabNum) {
				if(command!="") { command+=",";}
				command+="EhrLabNum = "+POut.Long(ehrLabClinicalInfo.EhrLabNum)+"";
			}
			if(ehrLabClinicalInfo.ClinicalInfoID != oldEhrLabClinicalInfo.ClinicalInfoID) {
				if(command!="") { command+=",";}
				command+="ClinicalInfoID = '"+POut.String(ehrLabClinicalInfo.ClinicalInfoID)+"'";
			}
			if(ehrLabClinicalInfo.ClinicalInfoText != oldEhrLabClinicalInfo.ClinicalInfoText) {
				if(command!="") { command+=",";}
				command+="ClinicalInfoText = '"+POut.String(ehrLabClinicalInfo.ClinicalInfoText)+"'";
			}
			if(ehrLabClinicalInfo.ClinicalInfoCodeSystemName != oldEhrLabClinicalInfo.ClinicalInfoCodeSystemName) {
				if(command!="") { command+=",";}
				command+="ClinicalInfoCodeSystemName = '"+POut.String(ehrLabClinicalInfo.ClinicalInfoCodeSystemName)+"'";
			}
			if(ehrLabClinicalInfo.ClinicalInfoIDAlt != oldEhrLabClinicalInfo.ClinicalInfoIDAlt) {
				if(command!="") { command+=",";}
				command+="ClinicalInfoIDAlt = '"+POut.String(ehrLabClinicalInfo.ClinicalInfoIDAlt)+"'";
			}
			if(ehrLabClinicalInfo.ClinicalInfoTextAlt != oldEhrLabClinicalInfo.ClinicalInfoTextAlt) {
				if(command!="") { command+=",";}
				command+="ClinicalInfoTextAlt = '"+POut.String(ehrLabClinicalInfo.ClinicalInfoTextAlt)+"'";
			}
			if(ehrLabClinicalInfo.ClinicalInfoCodeSystemNameAlt != oldEhrLabClinicalInfo.ClinicalInfoCodeSystemNameAlt) {
				if(command!="") { command+=",";}
				command+="ClinicalInfoCodeSystemNameAlt = '"+POut.String(ehrLabClinicalInfo.ClinicalInfoCodeSystemNameAlt)+"'";
			}
			if(ehrLabClinicalInfo.ClinicalInfoTextOriginal != oldEhrLabClinicalInfo.ClinicalInfoTextOriginal) {
				if(command!="") { command+=",";}
				command+="ClinicalInfoTextOriginal = '"+POut.String(ehrLabClinicalInfo.ClinicalInfoTextOriginal)+"'";
			}
			if(command=="") {
				return false;
			}
			command="UPDATE ehrlabclinicalinfo SET "+command
				+" WHERE EhrLabClinicalInfoNum = "+POut.Long(ehrLabClinicalInfo.EhrLabClinicalInfoNum);
			Db.NonQ(command);
			return true;
		}

		///<summary>Returns true if Update(EhrLabClinicalInfo,EhrLabClinicalInfo) would make changes to the database.
		///Does not make any changes to the database and can be called before remoting role is checked.</summary>
		public static bool UpdateComparison(EhrLabClinicalInfo ehrLabClinicalInfo,EhrLabClinicalInfo oldEhrLabClinicalInfo) {
			if(ehrLabClinicalInfo.EhrLabNum != oldEhrLabClinicalInfo.EhrLabNum) {
				return true;
			}
			if(ehrLabClinicalInfo.ClinicalInfoID != oldEhrLabClinicalInfo.ClinicalInfoID) {
				return true;
			}
			if(ehrLabClinicalInfo.ClinicalInfoText != oldEhrLabClinicalInfo.ClinicalInfoText) {
				return true;
			}
			if(ehrLabClinicalInfo.ClinicalInfoCodeSystemName != oldEhrLabClinicalInfo.ClinicalInfoCodeSystemName) {
				return true;
			}
			if(ehrLabClinicalInfo.ClinicalInfoIDAlt != oldEhrLabClinicalInfo.ClinicalInfoIDAlt) {
				return true;
			}
			if(ehrLabClinicalInfo.ClinicalInfoTextAlt != oldEhrLabClinicalInfo.ClinicalInfoTextAlt) {
				return true;
			}
			if(ehrLabClinicalInfo.ClinicalInfoCodeSystemNameAlt != oldEhrLabClinicalInfo.ClinicalInfoCodeSystemNameAlt) {
				return true;
			}
			if(ehrLabClinicalInfo.ClinicalInfoTextOriginal != oldEhrLabClinicalInfo.ClinicalInfoTextOriginal) {
				return true;
			}
			return false;
		}

		///<summary>Deletes one EhrLabClinicalInfo from the database.</summary>
		public static void Delete(long ehrLabClinicalInfoNum) {
			string command="DELETE FROM ehrlabclinicalinfo "
				+"WHERE EhrLabClinicalInfoNum = "+POut.Long(ehrLabClinicalInfoNum);
			Db.NonQ(command);
		}

		///<summary>Deletes many EhrLabClinicalInfos from the database.</summary>
		public static void DeleteMany(List<long> listEhrLabClinicalInfoNums) {
			if(listEhrLabClinicalInfoNums==null || listEhrLabClinicalInfoNums.Count==0) {
				return;
			}
			string command="DELETE FROM ehrlabclinicalinfo "
				+"WHERE EhrLabClinicalInfoNum IN("+string.Join(",",listEhrLabClinicalInfoNums.Select(x => POut.Long(x)))+")";
			Db.NonQ(command);
		}

	}
}