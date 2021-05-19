//This file is automatically generated.
//Do not attempt to make changes to this file because the changes will be erased and overwritten.
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;

namespace OpenDentBusiness.Crud{
	public class SecurityLogHashCrud {
		///<summary>Gets one SecurityLogHash object from the database using the primary key.  Returns null if not found.</summary>
		public static SecurityLogHash SelectOne(long securityLogHashNum) {
			string command="SELECT * FROM securityloghash "
				+"WHERE SecurityLogHashNum = "+POut.Long(securityLogHashNum);
			List<SecurityLogHash> list=TableToList(Db.GetTable(command));
			if(list.Count==0) {
				return null;
			}
			return list[0];
		}

		///<summary>Gets one SecurityLogHash object from the database using a query.</summary>
		public static SecurityLogHash SelectOne(string command) {
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				throw new ApplicationException("Not allowed to send sql directly.  Rewrite the calling class to not use this query:\r\n"+command);
			}
			List<SecurityLogHash> list=TableToList(Db.GetTable(command));
			if(list.Count==0) {
				return null;
			}
			return list[0];
		}

		///<summary>Gets a list of SecurityLogHash objects from the database using a query.</summary>
		public static List<SecurityLogHash> SelectMany(string command) {
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				throw new ApplicationException("Not allowed to send sql directly.  Rewrite the calling class to not use this query:\r\n"+command);
			}
			List<SecurityLogHash> list=TableToList(Db.GetTable(command));
			return list;
		}

		///<summary>Converts a DataTable to a list of objects.</summary>
		public static List<SecurityLogHash> TableToList(DataTable table) {
			List<SecurityLogHash> retVal=new List<SecurityLogHash>();
			SecurityLogHash securityLogHash;
			foreach(DataRow row in table.Rows) {
				securityLogHash=new SecurityLogHash();
				securityLogHash.SecurityLogHashNum= PIn.Long  (row["SecurityLogHashNum"].ToString());
				securityLogHash.SecurityLogNum    = PIn.Long  (row["SecurityLogNum"].ToString());
				securityLogHash.LogHash           = PIn.String(row["LogHash"].ToString());
				retVal.Add(securityLogHash);
			}
			return retVal;
		}

		///<summary>Converts a list of SecurityLogHash into a DataTable.</summary>
		public static DataTable ListToTable(List<SecurityLogHash> listSecurityLogHashs,string tableName="") {
			if(string.IsNullOrEmpty(tableName)) {
				tableName="SecurityLogHash";
			}
			DataTable table=new DataTable(tableName);
			table.Columns.Add("SecurityLogHashNum");
			table.Columns.Add("SecurityLogNum");
			table.Columns.Add("LogHash");
			foreach(SecurityLogHash securityLogHash in listSecurityLogHashs) {
				table.Rows.Add(new object[] {
					POut.Long  (securityLogHash.SecurityLogHashNum),
					POut.Long  (securityLogHash.SecurityLogNum),
					            securityLogHash.LogHash,
				});
			}
			return table;
		}

		///<summary>Inserts one SecurityLogHash into the database.  Returns the new priKey.</summary>
		public static long Insert(SecurityLogHash securityLogHash) {
			return Insert(securityLogHash,false);
		}

		///<summary>Inserts one SecurityLogHash into the database.  Provides option to use the existing priKey.</summary>
		public static long Insert(SecurityLogHash securityLogHash,bool useExistingPK) {
			if(!useExistingPK && PrefC.RandomKeys) {
				securityLogHash.SecurityLogHashNum=ReplicationServers.GetKey("securityloghash","SecurityLogHashNum");
			}
			string command="INSERT INTO securityloghash (";
			if(useExistingPK || PrefC.RandomKeys) {
				command+="SecurityLogHashNum,";
			}
			command+="SecurityLogNum,LogHash) VALUES(";
			if(useExistingPK || PrefC.RandomKeys) {
				command+=POut.Long(securityLogHash.SecurityLogHashNum)+",";
			}
			command+=
				     POut.Long  (securityLogHash.SecurityLogNum)+","
				+"'"+POut.String(securityLogHash.LogHash)+"')";
			if(useExistingPK || PrefC.RandomKeys) {
				Db.NonQ(command);
			}
			else {
				securityLogHash.SecurityLogHashNum=Db.NonQ(command,true,"SecurityLogHashNum","securityLogHash");
			}
			return securityLogHash.SecurityLogHashNum;
		}

		///<summary>Inserts many SecurityLogHashs into the database.</summary>
		public static void InsertMany(List<SecurityLogHash> listSecurityLogHashs) {
			InsertMany(listSecurityLogHashs,false);
		}

		///<summary>Inserts many SecurityLogHashs into the database.  Provides option to use the existing priKey.</summary>
		public static void InsertMany(List<SecurityLogHash> listSecurityLogHashs,bool useExistingPK) {
			if(!useExistingPK && PrefC.RandomKeys) {
				foreach(SecurityLogHash securityLogHash in listSecurityLogHashs) {
					Insert(securityLogHash);
				}
			}
			else {
				StringBuilder sbCommands=null;
				int index=0;
				int countRows=0;
				while(index < listSecurityLogHashs.Count) {
					SecurityLogHash securityLogHash=listSecurityLogHashs[index];
					StringBuilder sbRow=new StringBuilder("(");
					bool hasComma=false;
					if(sbCommands==null) {
						sbCommands=new StringBuilder();
						sbCommands.Append("INSERT INTO securityloghash (");
						if(useExistingPK) {
							sbCommands.Append("SecurityLogHashNum,");
						}
						sbCommands.Append("SecurityLogNum,LogHash) VALUES ");
						countRows=0;
					}
					else {
						hasComma=true;
					}
					if(useExistingPK) {
						sbRow.Append(POut.Long(securityLogHash.SecurityLogHashNum)); sbRow.Append(",");
					}
					sbRow.Append(POut.Long(securityLogHash.SecurityLogNum)); sbRow.Append(",");
					sbRow.Append("'"+POut.String(securityLogHash.LogHash)+"'"); sbRow.Append(")");
					if(sbCommands.Length+sbRow.Length+1 > TableBase.MaxAllowedPacketCount && countRows > 0) {
						Db.NonQ(sbCommands.ToString());
						sbCommands=null;
					}
					else {
						if(hasComma) {
							sbCommands.Append(",");
						}
						sbCommands.Append(sbRow.ToString());
						countRows++;
						if(index==listSecurityLogHashs.Count-1) {
							Db.NonQ(sbCommands.ToString());
						}
						index++;
					}
				}
			}
		}

		///<summary>Inserts one SecurityLogHash into the database.  Returns the new priKey.  Doesn't use the cache.</summary>
		public static long InsertNoCache(SecurityLogHash securityLogHash) {
			return InsertNoCache(securityLogHash,false);
		}

		///<summary>Inserts one SecurityLogHash into the database.  Provides option to use the existing priKey.  Doesn't use the cache.</summary>
		public static long InsertNoCache(SecurityLogHash securityLogHash,bool useExistingPK) {
			bool isRandomKeys=Prefs.GetBoolNoCache(PrefName.RandomPrimaryKeys);
			string command="INSERT INTO securityloghash (";
			if(!useExistingPK && isRandomKeys) {
				securityLogHash.SecurityLogHashNum=ReplicationServers.GetKeyNoCache("securityloghash","SecurityLogHashNum");
			}
			if(isRandomKeys || useExistingPK) {
				command+="SecurityLogHashNum,";
			}
			command+="SecurityLogNum,LogHash) VALUES(";
			if(isRandomKeys || useExistingPK) {
				command+=POut.Long(securityLogHash.SecurityLogHashNum)+",";
			}
			command+=
				     POut.Long  (securityLogHash.SecurityLogNum)+","
				+"'"+POut.String(securityLogHash.LogHash)+"')";
			if(useExistingPK || isRandomKeys) {
				Db.NonQ(command);
			}
			else {
				securityLogHash.SecurityLogHashNum=Db.NonQ(command,true,"SecurityLogHashNum","securityLogHash");
			}
			return securityLogHash.SecurityLogHashNum;
		}

		///<summary>Updates one SecurityLogHash in the database.</summary>
		public static void Update(SecurityLogHash securityLogHash) {
			string command="UPDATE securityloghash SET "
				+"SecurityLogNum    =  "+POut.Long  (securityLogHash.SecurityLogNum)+", "
				+"LogHash           = '"+POut.String(securityLogHash.LogHash)+"' "
				+"WHERE SecurityLogHashNum = "+POut.Long(securityLogHash.SecurityLogHashNum);
			Db.NonQ(command);
		}

		///<summary>Updates one SecurityLogHash in the database.  Uses an old object to compare to, and only alters changed fields.  This prevents collisions and concurrency problems in heavily used tables.  Returns true if an update occurred.</summary>
		public static bool Update(SecurityLogHash securityLogHash,SecurityLogHash oldSecurityLogHash) {
			string command="";
			if(securityLogHash.SecurityLogNum != oldSecurityLogHash.SecurityLogNum) {
				if(command!="") { command+=",";}
				command+="SecurityLogNum = "+POut.Long(securityLogHash.SecurityLogNum)+"";
			}
			if(securityLogHash.LogHash != oldSecurityLogHash.LogHash) {
				if(command!="") { command+=",";}
				command+="LogHash = '"+POut.String(securityLogHash.LogHash)+"'";
			}
			if(command=="") {
				return false;
			}
			command="UPDATE securityloghash SET "+command
				+" WHERE SecurityLogHashNum = "+POut.Long(securityLogHash.SecurityLogHashNum);
			Db.NonQ(command);
			return true;
		}

		///<summary>Returns true if Update(SecurityLogHash,SecurityLogHash) would make changes to the database.
		///Does not make any changes to the database and can be called before remoting role is checked.</summary>
		public static bool UpdateComparison(SecurityLogHash securityLogHash,SecurityLogHash oldSecurityLogHash) {
			if(securityLogHash.SecurityLogNum != oldSecurityLogHash.SecurityLogNum) {
				return true;
			}
			if(securityLogHash.LogHash != oldSecurityLogHash.LogHash) {
				return true;
			}
			return false;
		}

		///<summary>Deletes one SecurityLogHash from the database.</summary>
		public static void Delete(long securityLogHashNum) {
			string command="DELETE FROM securityloghash "
				+"WHERE SecurityLogHashNum = "+POut.Long(securityLogHashNum);
			Db.NonQ(command);
		}

		///<summary>Deletes many SecurityLogHashs from the database.</summary>
		public static void DeleteMany(List<long> listSecurityLogHashNums) {
			if(listSecurityLogHashNums==null || listSecurityLogHashNums.Count==0) {
				return;
			}
			string command="DELETE FROM securityloghash "
				+"WHERE SecurityLogHashNum IN("+string.Join(",",listSecurityLogHashNums.Select(x => POut.Long(x)))+")";
			Db.NonQ(command);
		}

	}
}