//This file is automatically generated.
//Do not attempt to make changes to this file because the changes will be erased and overwritten.
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;

namespace OpenDentBusiness.Crud{
	public class EmailMessageUidCrud {
		///<summary>Gets one EmailMessageUid object from the database using the primary key.  Returns null if not found.</summary>
		public static EmailMessageUid SelectOne(long emailMessageUidNum) {
			string command="SELECT * FROM emailmessageuid "
				+"WHERE EmailMessageUidNum = "+POut.Long(emailMessageUidNum);
			List<EmailMessageUid> list=TableToList(Db.GetTable(command));
			if(list.Count==0) {
				return null;
			}
			return list[0];
		}

		///<summary>Gets one EmailMessageUid object from the database using a query.</summary>
		public static EmailMessageUid SelectOne(string command) {
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				throw new ApplicationException("Not allowed to send sql directly.  Rewrite the calling class to not use this query:\r\n"+command);
			}
			List<EmailMessageUid> list=TableToList(Db.GetTable(command));
			if(list.Count==0) {
				return null;
			}
			return list[0];
		}

		///<summary>Gets a list of EmailMessageUid objects from the database using a query.</summary>
		public static List<EmailMessageUid> SelectMany(string command) {
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				throw new ApplicationException("Not allowed to send sql directly.  Rewrite the calling class to not use this query:\r\n"+command);
			}
			List<EmailMessageUid> list=TableToList(Db.GetTable(command));
			return list;
		}

		///<summary>Converts a DataTable to a list of objects.</summary>
		public static List<EmailMessageUid> TableToList(DataTable table) {
			List<EmailMessageUid> retVal=new List<EmailMessageUid>();
			EmailMessageUid emailMessageUid;
			foreach(DataRow row in table.Rows) {
				emailMessageUid=new EmailMessageUid();
				emailMessageUid.EmailMessageUidNum= PIn.Long  (row["EmailMessageUidNum"].ToString());
				emailMessageUid.MsgId             = PIn.String(row["MsgId"].ToString());
				emailMessageUid.RecipientAddress  = PIn.String(row["RecipientAddress"].ToString());
				retVal.Add(emailMessageUid);
			}
			return retVal;
		}

		///<summary>Converts a list of EmailMessageUid into a DataTable.</summary>
		public static DataTable ListToTable(List<EmailMessageUid> listEmailMessageUids,string tableName="") {
			if(string.IsNullOrEmpty(tableName)) {
				tableName="EmailMessageUid";
			}
			DataTable table=new DataTable(tableName);
			table.Columns.Add("EmailMessageUidNum");
			table.Columns.Add("MsgId");
			table.Columns.Add("RecipientAddress");
			foreach(EmailMessageUid emailMessageUid in listEmailMessageUids) {
				table.Rows.Add(new object[] {
					POut.Long  (emailMessageUid.EmailMessageUidNum),
					            emailMessageUid.MsgId,
					            emailMessageUid.RecipientAddress,
				});
			}
			return table;
		}

		///<summary>Inserts one EmailMessageUid into the database.  Returns the new priKey.</summary>
		public static long Insert(EmailMessageUid emailMessageUid) {
			return Insert(emailMessageUid,false);
		}

		///<summary>Inserts one EmailMessageUid into the database.  Provides option to use the existing priKey.</summary>
		public static long Insert(EmailMessageUid emailMessageUid,bool useExistingPK) {
			if(!useExistingPK && PrefC.RandomKeys) {
				emailMessageUid.EmailMessageUidNum=ReplicationServers.GetKey("emailmessageuid","EmailMessageUidNum");
			}
			string command="INSERT INTO emailmessageuid (";
			if(useExistingPK || PrefC.RandomKeys) {
				command+="EmailMessageUidNum,";
			}
			command+="MsgId,RecipientAddress) VALUES(";
			if(useExistingPK || PrefC.RandomKeys) {
				command+=POut.Long(emailMessageUid.EmailMessageUidNum)+",";
			}
			command+=
				     DbHelper.ParamChar+"paramMsgId,"
				+"'"+POut.String(emailMessageUid.RecipientAddress)+"')";
			if(emailMessageUid.MsgId==null) {
				emailMessageUid.MsgId="";
			}
			OdSqlParameter paramMsgId=new OdSqlParameter("paramMsgId",OdDbType.Text,POut.StringParam(emailMessageUid.MsgId));
			if(useExistingPK || PrefC.RandomKeys) {
				Db.NonQ(command,paramMsgId);
			}
			else {
				emailMessageUid.EmailMessageUidNum=Db.NonQ(command,true,"EmailMessageUidNum","emailMessageUid",paramMsgId);
			}
			return emailMessageUid.EmailMessageUidNum;
		}

		///<summary>Inserts one EmailMessageUid into the database.  Returns the new priKey.  Doesn't use the cache.</summary>
		public static long InsertNoCache(EmailMessageUid emailMessageUid) {
			return InsertNoCache(emailMessageUid,false);
		}

		///<summary>Inserts one EmailMessageUid into the database.  Provides option to use the existing priKey.  Doesn't use the cache.</summary>
		public static long InsertNoCache(EmailMessageUid emailMessageUid,bool useExistingPK) {
			bool isRandomKeys=Prefs.GetBoolNoCache(PrefName.RandomPrimaryKeys);
			string command="INSERT INTO emailmessageuid (";
			if(!useExistingPK && isRandomKeys) {
				emailMessageUid.EmailMessageUidNum=ReplicationServers.GetKeyNoCache("emailmessageuid","EmailMessageUidNum");
			}
			if(isRandomKeys || useExistingPK) {
				command+="EmailMessageUidNum,";
			}
			command+="MsgId,RecipientAddress) VALUES(";
			if(isRandomKeys || useExistingPK) {
				command+=POut.Long(emailMessageUid.EmailMessageUidNum)+",";
			}
			command+=
				     DbHelper.ParamChar+"paramMsgId,"
				+"'"+POut.String(emailMessageUid.RecipientAddress)+"')";
			if(emailMessageUid.MsgId==null) {
				emailMessageUid.MsgId="";
			}
			OdSqlParameter paramMsgId=new OdSqlParameter("paramMsgId",OdDbType.Text,POut.StringParam(emailMessageUid.MsgId));
			if(useExistingPK || isRandomKeys) {
				Db.NonQ(command,paramMsgId);
			}
			else {
				emailMessageUid.EmailMessageUidNum=Db.NonQ(command,true,"EmailMessageUidNum","emailMessageUid",paramMsgId);
			}
			return emailMessageUid.EmailMessageUidNum;
		}

		///<summary>Updates one EmailMessageUid in the database.</summary>
		public static void Update(EmailMessageUid emailMessageUid) {
			string command="UPDATE emailmessageuid SET "
				+"MsgId             =  "+DbHelper.ParamChar+"paramMsgId, "
				+"RecipientAddress  = '"+POut.String(emailMessageUid.RecipientAddress)+"' "
				+"WHERE EmailMessageUidNum = "+POut.Long(emailMessageUid.EmailMessageUidNum);
			if(emailMessageUid.MsgId==null) {
				emailMessageUid.MsgId="";
			}
			OdSqlParameter paramMsgId=new OdSqlParameter("paramMsgId",OdDbType.Text,POut.StringParam(emailMessageUid.MsgId));
			Db.NonQ(command,paramMsgId);
		}

		///<summary>Updates one EmailMessageUid in the database.  Uses an old object to compare to, and only alters changed fields.  This prevents collisions and concurrency problems in heavily used tables.  Returns true if an update occurred.</summary>
		public static bool Update(EmailMessageUid emailMessageUid,EmailMessageUid oldEmailMessageUid) {
			string command="";
			if(emailMessageUid.MsgId != oldEmailMessageUid.MsgId) {
				if(command!="") { command+=",";}
				command+="MsgId = "+DbHelper.ParamChar+"paramMsgId";
			}
			if(emailMessageUid.RecipientAddress != oldEmailMessageUid.RecipientAddress) {
				if(command!="") { command+=",";}
				command+="RecipientAddress = '"+POut.String(emailMessageUid.RecipientAddress)+"'";
			}
			if(command=="") {
				return false;
			}
			if(emailMessageUid.MsgId==null) {
				emailMessageUid.MsgId="";
			}
			OdSqlParameter paramMsgId=new OdSqlParameter("paramMsgId",OdDbType.Text,POut.StringParam(emailMessageUid.MsgId));
			command="UPDATE emailmessageuid SET "+command
				+" WHERE EmailMessageUidNum = "+POut.Long(emailMessageUid.EmailMessageUidNum);
			Db.NonQ(command,paramMsgId);
			return true;
		}

		///<summary>Returns true if Update(EmailMessageUid,EmailMessageUid) would make changes to the database.
		///Does not make any changes to the database and can be called before remoting role is checked.</summary>
		public static bool UpdateComparison(EmailMessageUid emailMessageUid,EmailMessageUid oldEmailMessageUid) {
			if(emailMessageUid.MsgId != oldEmailMessageUid.MsgId) {
				return true;
			}
			if(emailMessageUid.RecipientAddress != oldEmailMessageUid.RecipientAddress) {
				return true;
			}
			return false;
		}

		///<summary>Deletes one EmailMessageUid from the database.</summary>
		public static void Delete(long emailMessageUidNum) {
			string command="DELETE FROM emailmessageuid "
				+"WHERE EmailMessageUidNum = "+POut.Long(emailMessageUidNum);
			Db.NonQ(command);
		}

		///<summary>Deletes many EmailMessageUids from the database.</summary>
		public static void DeleteMany(List<long> listEmailMessageUidNums) {
			if(listEmailMessageUidNums==null || listEmailMessageUidNums.Count==0) {
				return;
			}
			string command="DELETE FROM emailmessageuid "
				+"WHERE EmailMessageUidNum IN("+string.Join(",",listEmailMessageUidNums.Select(x => POut.Long(x)))+")";
			Db.NonQ(command);
		}

	}
}