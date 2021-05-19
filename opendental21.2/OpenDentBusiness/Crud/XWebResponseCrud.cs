//This file is automatically generated.
//Do not attempt to make changes to this file because the changes will be erased and overwritten.
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;

namespace OpenDentBusiness.Crud{
	public class XWebResponseCrud {
		///<summary>Gets one XWebResponse object from the database using the primary key.  Returns null if not found.</summary>
		public static XWebResponse SelectOne(long xWebResponseNum) {
			string command="SELECT * FROM xwebresponse "
				+"WHERE XWebResponseNum = "+POut.Long(xWebResponseNum);
			List<XWebResponse> list=TableToList(Db.GetTable(command));
			if(list.Count==0) {
				return null;
			}
			return list[0];
		}

		///<summary>Gets one XWebResponse object from the database using a query.</summary>
		public static XWebResponse SelectOne(string command) {
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				throw new ApplicationException("Not allowed to send sql directly.  Rewrite the calling class to not use this query:\r\n"+command);
			}
			List<XWebResponse> list=TableToList(Db.GetTable(command));
			if(list.Count==0) {
				return null;
			}
			return list[0];
		}

		///<summary>Gets a list of XWebResponse objects from the database using a query.</summary>
		public static List<XWebResponse> SelectMany(string command) {
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				throw new ApplicationException("Not allowed to send sql directly.  Rewrite the calling class to not use this query:\r\n"+command);
			}
			List<XWebResponse> list=TableToList(Db.GetTable(command));
			return list;
		}

		///<summary>Converts a DataTable to a list of objects.</summary>
		public static List<XWebResponse> TableToList(DataTable table) {
			List<XWebResponse> retVal=new List<XWebResponse>();
			XWebResponse xWebResponse;
			foreach(DataRow row in table.Rows) {
				xWebResponse=new XWebResponse();
				xWebResponse.XWebResponseNum      = PIn.Long  (row["XWebResponseNum"].ToString());
				xWebResponse.PatNum               = PIn.Long  (row["PatNum"].ToString());
				xWebResponse.ProvNum              = PIn.Long  (row["ProvNum"].ToString());
				xWebResponse.ClinicNum            = PIn.Long  (row["ClinicNum"].ToString());
				xWebResponse.PaymentNum           = PIn.Long  (row["PaymentNum"].ToString());
				xWebResponse.DateTEntry           = PIn.DateT (row["DateTEntry"].ToString());
				xWebResponse.DateTUpdate          = PIn.DateT (row["DateTUpdate"].ToString());
				xWebResponse.TransactionStatus    = (OpenDentBusiness.XWebTransactionStatus)PIn.Int(row["TransactionStatus"].ToString());
				xWebResponse.ResponseCode         = PIn.Int   (row["ResponseCode"].ToString());
				string xWebResponseCode=row["XWebResponseCode"].ToString();
				if(xWebResponseCode=="") {
					xWebResponse.XWebResponseCode   =(OpenDentBusiness.XWebResponseCodes)0;
				}
				else try{
					xWebResponse.XWebResponseCode   =(OpenDentBusiness.XWebResponseCodes)Enum.Parse(typeof(OpenDentBusiness.XWebResponseCodes),xWebResponseCode);
				}
				catch{
					xWebResponse.XWebResponseCode   =(OpenDentBusiness.XWebResponseCodes)0;
				}
				xWebResponse.ResponseDescription  = PIn.String(row["ResponseDescription"].ToString());
				xWebResponse.OTK                  = PIn.String(row["OTK"].ToString());
				xWebResponse.HpfUrl               = PIn.String(row["HpfUrl"].ToString());
				xWebResponse.HpfExpiration        = PIn.DateT (row["HpfExpiration"].ToString());
				xWebResponse.TransactionID        = PIn.String(row["TransactionID"].ToString());
				xWebResponse.TransactionType      = PIn.String(row["TransactionType"].ToString());
				xWebResponse.Alias                = PIn.String(row["Alias"].ToString());
				xWebResponse.CardType             = PIn.String(row["CardType"].ToString());
				xWebResponse.CardBrand            = PIn.String(row["CardBrand"].ToString());
				xWebResponse.CardBrandShort       = PIn.String(row["CardBrandShort"].ToString());
				xWebResponse.MaskedAcctNum        = PIn.String(row["MaskedAcctNum"].ToString());
				xWebResponse.Amount               = PIn.Double(row["Amount"].ToString());
				xWebResponse.ApprovalCode         = PIn.String(row["ApprovalCode"].ToString());
				xWebResponse.CardCodeResponse     = PIn.String(row["CardCodeResponse"].ToString());
				xWebResponse.ReceiptID            = PIn.Int   (row["ReceiptID"].ToString());
				xWebResponse.ExpDate              = PIn.String(row["ExpDate"].ToString());
				xWebResponse.EntryMethod          = PIn.String(row["EntryMethod"].ToString());
				xWebResponse.ProcessorResponse    = PIn.String(row["ProcessorResponse"].ToString());
				xWebResponse.BatchNum             = PIn.Int   (row["BatchNum"].ToString());
				xWebResponse.BatchAmount          = PIn.Double(row["BatchAmount"].ToString());
				xWebResponse.AccountExpirationDate= PIn.Date  (row["AccountExpirationDate"].ToString());
				xWebResponse.DebugError           = PIn.String(row["DebugError"].ToString());
				xWebResponse.PayNote              = PIn.String(row["PayNote"].ToString());
				xWebResponse.CCSource             = (OpenDentBusiness.CreditCardSource)PIn.Int(row["CCSource"].ToString());
				xWebResponse.OrderId              = PIn.String(row["OrderId"].ToString());
				retVal.Add(xWebResponse);
			}
			return retVal;
		}

		///<summary>Converts a list of XWebResponse into a DataTable.</summary>
		public static DataTable ListToTable(List<XWebResponse> listXWebResponses,string tableName="") {
			if(string.IsNullOrEmpty(tableName)) {
				tableName="XWebResponse";
			}
			DataTable table=new DataTable(tableName);
			table.Columns.Add("XWebResponseNum");
			table.Columns.Add("PatNum");
			table.Columns.Add("ProvNum");
			table.Columns.Add("ClinicNum");
			table.Columns.Add("PaymentNum");
			table.Columns.Add("DateTEntry");
			table.Columns.Add("DateTUpdate");
			table.Columns.Add("TransactionStatus");
			table.Columns.Add("ResponseCode");
			table.Columns.Add("XWebResponseCode");
			table.Columns.Add("ResponseDescription");
			table.Columns.Add("OTK");
			table.Columns.Add("HpfUrl");
			table.Columns.Add("HpfExpiration");
			table.Columns.Add("TransactionID");
			table.Columns.Add("TransactionType");
			table.Columns.Add("Alias");
			table.Columns.Add("CardType");
			table.Columns.Add("CardBrand");
			table.Columns.Add("CardBrandShort");
			table.Columns.Add("MaskedAcctNum");
			table.Columns.Add("Amount");
			table.Columns.Add("ApprovalCode");
			table.Columns.Add("CardCodeResponse");
			table.Columns.Add("ReceiptID");
			table.Columns.Add("ExpDate");
			table.Columns.Add("EntryMethod");
			table.Columns.Add("ProcessorResponse");
			table.Columns.Add("BatchNum");
			table.Columns.Add("BatchAmount");
			table.Columns.Add("AccountExpirationDate");
			table.Columns.Add("DebugError");
			table.Columns.Add("PayNote");
			table.Columns.Add("CCSource");
			table.Columns.Add("OrderId");
			foreach(XWebResponse xWebResponse in listXWebResponses) {
				table.Rows.Add(new object[] {
					POut.Long  (xWebResponse.XWebResponseNum),
					POut.Long  (xWebResponse.PatNum),
					POut.Long  (xWebResponse.ProvNum),
					POut.Long  (xWebResponse.ClinicNum),
					POut.Long  (xWebResponse.PaymentNum),
					POut.DateT (xWebResponse.DateTEntry,false),
					POut.DateT (xWebResponse.DateTUpdate,false),
					POut.Int   ((int)xWebResponse.TransactionStatus),
					POut.Int   (xWebResponse.ResponseCode),
					POut.Int   ((int)xWebResponse.XWebResponseCode),
					            xWebResponse.ResponseDescription,
					            xWebResponse.OTK,
					            xWebResponse.HpfUrl,
					POut.DateT (xWebResponse.HpfExpiration,false),
					            xWebResponse.TransactionID,
					            xWebResponse.TransactionType,
					            xWebResponse.Alias,
					            xWebResponse.CardType,
					            xWebResponse.CardBrand,
					            xWebResponse.CardBrandShort,
					            xWebResponse.MaskedAcctNum,
					POut.Double(xWebResponse.Amount),
					            xWebResponse.ApprovalCode,
					            xWebResponse.CardCodeResponse,
					POut.Int   (xWebResponse.ReceiptID),
					            xWebResponse.ExpDate,
					            xWebResponse.EntryMethod,
					            xWebResponse.ProcessorResponse,
					POut.Int   (xWebResponse.BatchNum),
					POut.Double(xWebResponse.BatchAmount),
					POut.DateT (xWebResponse.AccountExpirationDate,false),
					            xWebResponse.DebugError,
					            xWebResponse.PayNote,
					POut.Int   ((int)xWebResponse.CCSource),
					            xWebResponse.OrderId,
				});
			}
			return table;
		}

		///<summary>Inserts one XWebResponse into the database.  Returns the new priKey.</summary>
		public static long Insert(XWebResponse xWebResponse) {
			return Insert(xWebResponse,false);
		}

		///<summary>Inserts one XWebResponse into the database.  Provides option to use the existing priKey.</summary>
		public static long Insert(XWebResponse xWebResponse,bool useExistingPK) {
			if(!useExistingPK && PrefC.RandomKeys) {
				xWebResponse.XWebResponseNum=ReplicationServers.GetKey("xwebresponse","XWebResponseNum");
			}
			string command="INSERT INTO xwebresponse (";
			if(useExistingPK || PrefC.RandomKeys) {
				command+="XWebResponseNum,";
			}
			command+="PatNum,ProvNum,ClinicNum,PaymentNum,DateTEntry,DateTUpdate,TransactionStatus,ResponseCode,XWebResponseCode,ResponseDescription,OTK,HpfUrl,HpfExpiration,TransactionID,TransactionType,Alias,CardType,CardBrand,CardBrandShort,MaskedAcctNum,Amount,ApprovalCode,CardCodeResponse,ReceiptID,ExpDate,EntryMethod,ProcessorResponse,BatchNum,BatchAmount,AccountExpirationDate,DebugError,PayNote,CCSource,OrderId) VALUES(";
			if(useExistingPK || PrefC.RandomKeys) {
				command+=POut.Long(xWebResponse.XWebResponseNum)+",";
			}
			command+=
				     POut.Long  (xWebResponse.PatNum)+","
				+    POut.Long  (xWebResponse.ProvNum)+","
				+    POut.Long  (xWebResponse.ClinicNum)+","
				+    POut.Long  (xWebResponse.PaymentNum)+","
				+    DbHelper.Now()+","
				+    POut.DateT (xWebResponse.DateTUpdate)+","
				+    POut.Int   ((int)xWebResponse.TransactionStatus)+","
				+    POut.Int   (xWebResponse.ResponseCode)+","
				+"'"+POut.String(xWebResponse.XWebResponseCode.ToString())+"',"
				+"'"+POut.String(xWebResponse.ResponseDescription)+"',"
				+"'"+POut.String(xWebResponse.OTK)+"',"
				+    DbHelper.ParamChar+"paramHpfUrl,"
				+    POut.DateT (xWebResponse.HpfExpiration)+","
				+"'"+POut.String(xWebResponse.TransactionID)+"',"
				+"'"+POut.String(xWebResponse.TransactionType)+"',"
				+"'"+POut.String(xWebResponse.Alias)+"',"
				+"'"+POut.String(xWebResponse.CardType)+"',"
				+"'"+POut.String(xWebResponse.CardBrand)+"',"
				+"'"+POut.String(xWebResponse.CardBrandShort)+"',"
				+"'"+POut.String(xWebResponse.MaskedAcctNum)+"',"
				+		 POut.Double(xWebResponse.Amount)+","
				+"'"+POut.String(xWebResponse.ApprovalCode)+"',"
				+"'"+POut.String(xWebResponse.CardCodeResponse)+"',"
				+    POut.Int   (xWebResponse.ReceiptID)+","
				+"'"+POut.String(xWebResponse.ExpDate)+"',"
				+"'"+POut.String(xWebResponse.EntryMethod)+"',"
				+"'"+POut.String(xWebResponse.ProcessorResponse)+"',"
				+    POut.Int   (xWebResponse.BatchNum)+","
				+		 POut.Double(xWebResponse.BatchAmount)+","
				+    POut.Date  (xWebResponse.AccountExpirationDate)+","
				+    DbHelper.ParamChar+"paramDebugError,"
				+    DbHelper.ParamChar+"paramPayNote,"
				+    POut.Int   ((int)xWebResponse.CCSource)+","
				+"'"+POut.String(xWebResponse.OrderId)+"')";
			if(xWebResponse.HpfUrl==null) {
				xWebResponse.HpfUrl="";
			}
			OdSqlParameter paramHpfUrl=new OdSqlParameter("paramHpfUrl",OdDbType.Text,POut.StringParam(xWebResponse.HpfUrl));
			if(xWebResponse.DebugError==null) {
				xWebResponse.DebugError="";
			}
			OdSqlParameter paramDebugError=new OdSqlParameter("paramDebugError",OdDbType.Text,POut.StringParam(xWebResponse.DebugError));
			if(xWebResponse.PayNote==null) {
				xWebResponse.PayNote="";
			}
			OdSqlParameter paramPayNote=new OdSqlParameter("paramPayNote",OdDbType.Text,POut.StringParam(xWebResponse.PayNote));
			if(useExistingPK || PrefC.RandomKeys) {
				Db.NonQ(command,paramHpfUrl,paramDebugError,paramPayNote);
			}
			else {
				xWebResponse.XWebResponseNum=Db.NonQ(command,true,"XWebResponseNum","xWebResponse",paramHpfUrl,paramDebugError,paramPayNote);
			}
			return xWebResponse.XWebResponseNum;
		}

		///<summary>Inserts one XWebResponse into the database.  Returns the new priKey.  Doesn't use the cache.</summary>
		public static long InsertNoCache(XWebResponse xWebResponse) {
			return InsertNoCache(xWebResponse,false);
		}

		///<summary>Inserts one XWebResponse into the database.  Provides option to use the existing priKey.  Doesn't use the cache.</summary>
		public static long InsertNoCache(XWebResponse xWebResponse,bool useExistingPK) {
			bool isRandomKeys=Prefs.GetBoolNoCache(PrefName.RandomPrimaryKeys);
			string command="INSERT INTO xwebresponse (";
			if(!useExistingPK && isRandomKeys) {
				xWebResponse.XWebResponseNum=ReplicationServers.GetKeyNoCache("xwebresponse","XWebResponseNum");
			}
			if(isRandomKeys || useExistingPK) {
				command+="XWebResponseNum,";
			}
			command+="PatNum,ProvNum,ClinicNum,PaymentNum,DateTEntry,DateTUpdate,TransactionStatus,ResponseCode,XWebResponseCode,ResponseDescription,OTK,HpfUrl,HpfExpiration,TransactionID,TransactionType,Alias,CardType,CardBrand,CardBrandShort,MaskedAcctNum,Amount,ApprovalCode,CardCodeResponse,ReceiptID,ExpDate,EntryMethod,ProcessorResponse,BatchNum,BatchAmount,AccountExpirationDate,DebugError,PayNote,CCSource,OrderId) VALUES(";
			if(isRandomKeys || useExistingPK) {
				command+=POut.Long(xWebResponse.XWebResponseNum)+",";
			}
			command+=
				     POut.Long  (xWebResponse.PatNum)+","
				+    POut.Long  (xWebResponse.ProvNum)+","
				+    POut.Long  (xWebResponse.ClinicNum)+","
				+    POut.Long  (xWebResponse.PaymentNum)+","
				+    DbHelper.Now()+","
				+    POut.DateT (xWebResponse.DateTUpdate)+","
				+    POut.Int   ((int)xWebResponse.TransactionStatus)+","
				+    POut.Int   (xWebResponse.ResponseCode)+","
				+"'"+POut.String(xWebResponse.XWebResponseCode.ToString())+"',"
				+"'"+POut.String(xWebResponse.ResponseDescription)+"',"
				+"'"+POut.String(xWebResponse.OTK)+"',"
				+    DbHelper.ParamChar+"paramHpfUrl,"
				+    POut.DateT (xWebResponse.HpfExpiration)+","
				+"'"+POut.String(xWebResponse.TransactionID)+"',"
				+"'"+POut.String(xWebResponse.TransactionType)+"',"
				+"'"+POut.String(xWebResponse.Alias)+"',"
				+"'"+POut.String(xWebResponse.CardType)+"',"
				+"'"+POut.String(xWebResponse.CardBrand)+"',"
				+"'"+POut.String(xWebResponse.CardBrandShort)+"',"
				+"'"+POut.String(xWebResponse.MaskedAcctNum)+"',"
				+	   POut.Double(xWebResponse.Amount)+","
				+"'"+POut.String(xWebResponse.ApprovalCode)+"',"
				+"'"+POut.String(xWebResponse.CardCodeResponse)+"',"
				+    POut.Int   (xWebResponse.ReceiptID)+","
				+"'"+POut.String(xWebResponse.ExpDate)+"',"
				+"'"+POut.String(xWebResponse.EntryMethod)+"',"
				+"'"+POut.String(xWebResponse.ProcessorResponse)+"',"
				+    POut.Int   (xWebResponse.BatchNum)+","
				+	   POut.Double(xWebResponse.BatchAmount)+","
				+    POut.Date  (xWebResponse.AccountExpirationDate)+","
				+    DbHelper.ParamChar+"paramDebugError,"
				+    DbHelper.ParamChar+"paramPayNote,"
				+    POut.Int   ((int)xWebResponse.CCSource)+","
				+"'"+POut.String(xWebResponse.OrderId)+"')";
			if(xWebResponse.HpfUrl==null) {
				xWebResponse.HpfUrl="";
			}
			OdSqlParameter paramHpfUrl=new OdSqlParameter("paramHpfUrl",OdDbType.Text,POut.StringParam(xWebResponse.HpfUrl));
			if(xWebResponse.DebugError==null) {
				xWebResponse.DebugError="";
			}
			OdSqlParameter paramDebugError=new OdSqlParameter("paramDebugError",OdDbType.Text,POut.StringParam(xWebResponse.DebugError));
			if(xWebResponse.PayNote==null) {
				xWebResponse.PayNote="";
			}
			OdSqlParameter paramPayNote=new OdSqlParameter("paramPayNote",OdDbType.Text,POut.StringParam(xWebResponse.PayNote));
			if(useExistingPK || isRandomKeys) {
				Db.NonQ(command,paramHpfUrl,paramDebugError,paramPayNote);
			}
			else {
				xWebResponse.XWebResponseNum=Db.NonQ(command,true,"XWebResponseNum","xWebResponse",paramHpfUrl,paramDebugError,paramPayNote);
			}
			return xWebResponse.XWebResponseNum;
		}

		///<summary>Updates one XWebResponse in the database.</summary>
		public static void Update(XWebResponse xWebResponse) {
			string command="UPDATE xwebresponse SET "
				+"PatNum               =  "+POut.Long  (xWebResponse.PatNum)+", "
				+"ProvNum              =  "+POut.Long  (xWebResponse.ProvNum)+", "
				+"ClinicNum            =  "+POut.Long  (xWebResponse.ClinicNum)+", "
				+"PaymentNum           =  "+POut.Long  (xWebResponse.PaymentNum)+", "
				//DateTEntry not allowed to change
				+"DateTUpdate          =  "+POut.DateT (xWebResponse.DateTUpdate)+", "
				+"TransactionStatus    =  "+POut.Int   ((int)xWebResponse.TransactionStatus)+", "
				+"ResponseCode         =  "+POut.Int   (xWebResponse.ResponseCode)+", "
				+"XWebResponseCode     = '"+POut.String(xWebResponse.XWebResponseCode.ToString())+"', "
				+"ResponseDescription  = '"+POut.String(xWebResponse.ResponseDescription)+"', "
				+"OTK                  = '"+POut.String(xWebResponse.OTK)+"', "
				+"HpfUrl               =  "+DbHelper.ParamChar+"paramHpfUrl, "
				+"HpfExpiration        =  "+POut.DateT (xWebResponse.HpfExpiration)+", "
				+"TransactionID        = '"+POut.String(xWebResponse.TransactionID)+"', "
				+"TransactionType      = '"+POut.String(xWebResponse.TransactionType)+"', "
				+"Alias                = '"+POut.String(xWebResponse.Alias)+"', "
				+"CardType             = '"+POut.String(xWebResponse.CardType)+"', "
				+"CardBrand            = '"+POut.String(xWebResponse.CardBrand)+"', "
				+"CardBrandShort       = '"+POut.String(xWebResponse.CardBrandShort)+"', "
				+"MaskedAcctNum        = '"+POut.String(xWebResponse.MaskedAcctNum)+"', "
				+"Amount               =  "+POut.Double(xWebResponse.Amount)+", "
				+"ApprovalCode         = '"+POut.String(xWebResponse.ApprovalCode)+"', "
				+"CardCodeResponse     = '"+POut.String(xWebResponse.CardCodeResponse)+"', "
				+"ReceiptID            =  "+POut.Int   (xWebResponse.ReceiptID)+", "
				+"ExpDate              = '"+POut.String(xWebResponse.ExpDate)+"', "
				+"EntryMethod          = '"+POut.String(xWebResponse.EntryMethod)+"', "
				+"ProcessorResponse    = '"+POut.String(xWebResponse.ProcessorResponse)+"', "
				+"BatchNum             =  "+POut.Int   (xWebResponse.BatchNum)+", "
				+"BatchAmount          =  "+POut.Double(xWebResponse.BatchAmount)+", "
				+"AccountExpirationDate=  "+POut.Date  (xWebResponse.AccountExpirationDate)+", "
				+"DebugError           =  "+DbHelper.ParamChar+"paramDebugError, "
				+"PayNote              =  "+DbHelper.ParamChar+"paramPayNote, "
				+"CCSource             =  "+POut.Int   ((int)xWebResponse.CCSource)+", "
				+"OrderId              = '"+POut.String(xWebResponse.OrderId)+"' "
				+"WHERE XWebResponseNum = "+POut.Long(xWebResponse.XWebResponseNum);
			if(xWebResponse.HpfUrl==null) {
				xWebResponse.HpfUrl="";
			}
			OdSqlParameter paramHpfUrl=new OdSqlParameter("paramHpfUrl",OdDbType.Text,POut.StringParam(xWebResponse.HpfUrl));
			if(xWebResponse.DebugError==null) {
				xWebResponse.DebugError="";
			}
			OdSqlParameter paramDebugError=new OdSqlParameter("paramDebugError",OdDbType.Text,POut.StringParam(xWebResponse.DebugError));
			if(xWebResponse.PayNote==null) {
				xWebResponse.PayNote="";
			}
			OdSqlParameter paramPayNote=new OdSqlParameter("paramPayNote",OdDbType.Text,POut.StringParam(xWebResponse.PayNote));
			Db.NonQ(command,paramHpfUrl,paramDebugError,paramPayNote);
		}

		///<summary>Updates one XWebResponse in the database.  Uses an old object to compare to, and only alters changed fields.  This prevents collisions and concurrency problems in heavily used tables.  Returns true if an update occurred.</summary>
		public static bool Update(XWebResponse xWebResponse,XWebResponse oldXWebResponse) {
			string command="";
			if(xWebResponse.PatNum != oldXWebResponse.PatNum) {
				if(command!="") { command+=",";}
				command+="PatNum = "+POut.Long(xWebResponse.PatNum)+"";
			}
			if(xWebResponse.ProvNum != oldXWebResponse.ProvNum) {
				if(command!="") { command+=",";}
				command+="ProvNum = "+POut.Long(xWebResponse.ProvNum)+"";
			}
			if(xWebResponse.ClinicNum != oldXWebResponse.ClinicNum) {
				if(command!="") { command+=",";}
				command+="ClinicNum = "+POut.Long(xWebResponse.ClinicNum)+"";
			}
			if(xWebResponse.PaymentNum != oldXWebResponse.PaymentNum) {
				if(command!="") { command+=",";}
				command+="PaymentNum = "+POut.Long(xWebResponse.PaymentNum)+"";
			}
			//DateTEntry not allowed to change
			if(xWebResponse.DateTUpdate != oldXWebResponse.DateTUpdate) {
				if(command!="") { command+=",";}
				command+="DateTUpdate = "+POut.DateT(xWebResponse.DateTUpdate)+"";
			}
			if(xWebResponse.TransactionStatus != oldXWebResponse.TransactionStatus) {
				if(command!="") { command+=",";}
				command+="TransactionStatus = "+POut.Int   ((int)xWebResponse.TransactionStatus)+"";
			}
			if(xWebResponse.ResponseCode != oldXWebResponse.ResponseCode) {
				if(command!="") { command+=",";}
				command+="ResponseCode = "+POut.Int(xWebResponse.ResponseCode)+"";
			}
			if(xWebResponse.XWebResponseCode != oldXWebResponse.XWebResponseCode) {
				if(command!="") { command+=",";}
				command+="XWebResponseCode = '"+POut.String(xWebResponse.XWebResponseCode.ToString())+"'";
			}
			if(xWebResponse.ResponseDescription != oldXWebResponse.ResponseDescription) {
				if(command!="") { command+=",";}
				command+="ResponseDescription = '"+POut.String(xWebResponse.ResponseDescription)+"'";
			}
			if(xWebResponse.OTK != oldXWebResponse.OTK) {
				if(command!="") { command+=",";}
				command+="OTK = '"+POut.String(xWebResponse.OTK)+"'";
			}
			if(xWebResponse.HpfUrl != oldXWebResponse.HpfUrl) {
				if(command!="") { command+=",";}
				command+="HpfUrl = "+DbHelper.ParamChar+"paramHpfUrl";
			}
			if(xWebResponse.HpfExpiration != oldXWebResponse.HpfExpiration) {
				if(command!="") { command+=",";}
				command+="HpfExpiration = "+POut.DateT(xWebResponse.HpfExpiration)+"";
			}
			if(xWebResponse.TransactionID != oldXWebResponse.TransactionID) {
				if(command!="") { command+=",";}
				command+="TransactionID = '"+POut.String(xWebResponse.TransactionID)+"'";
			}
			if(xWebResponse.TransactionType != oldXWebResponse.TransactionType) {
				if(command!="") { command+=",";}
				command+="TransactionType = '"+POut.String(xWebResponse.TransactionType)+"'";
			}
			if(xWebResponse.Alias != oldXWebResponse.Alias) {
				if(command!="") { command+=",";}
				command+="Alias = '"+POut.String(xWebResponse.Alias)+"'";
			}
			if(xWebResponse.CardType != oldXWebResponse.CardType) {
				if(command!="") { command+=",";}
				command+="CardType = '"+POut.String(xWebResponse.CardType)+"'";
			}
			if(xWebResponse.CardBrand != oldXWebResponse.CardBrand) {
				if(command!="") { command+=",";}
				command+="CardBrand = '"+POut.String(xWebResponse.CardBrand)+"'";
			}
			if(xWebResponse.CardBrandShort != oldXWebResponse.CardBrandShort) {
				if(command!="") { command+=",";}
				command+="CardBrandShort = '"+POut.String(xWebResponse.CardBrandShort)+"'";
			}
			if(xWebResponse.MaskedAcctNum != oldXWebResponse.MaskedAcctNum) {
				if(command!="") { command+=",";}
				command+="MaskedAcctNum = '"+POut.String(xWebResponse.MaskedAcctNum)+"'";
			}
			if(xWebResponse.Amount != oldXWebResponse.Amount) {
				if(command!="") { command+=",";}
				command+="Amount = "+POut.Double(xWebResponse.Amount)+"";
			}
			if(xWebResponse.ApprovalCode != oldXWebResponse.ApprovalCode) {
				if(command!="") { command+=",";}
				command+="ApprovalCode = '"+POut.String(xWebResponse.ApprovalCode)+"'";
			}
			if(xWebResponse.CardCodeResponse != oldXWebResponse.CardCodeResponse) {
				if(command!="") { command+=",";}
				command+="CardCodeResponse = '"+POut.String(xWebResponse.CardCodeResponse)+"'";
			}
			if(xWebResponse.ReceiptID != oldXWebResponse.ReceiptID) {
				if(command!="") { command+=",";}
				command+="ReceiptID = "+POut.Int(xWebResponse.ReceiptID)+"";
			}
			if(xWebResponse.ExpDate != oldXWebResponse.ExpDate) {
				if(command!="") { command+=",";}
				command+="ExpDate = '"+POut.String(xWebResponse.ExpDate)+"'";
			}
			if(xWebResponse.EntryMethod != oldXWebResponse.EntryMethod) {
				if(command!="") { command+=",";}
				command+="EntryMethod = '"+POut.String(xWebResponse.EntryMethod)+"'";
			}
			if(xWebResponse.ProcessorResponse != oldXWebResponse.ProcessorResponse) {
				if(command!="") { command+=",";}
				command+="ProcessorResponse = '"+POut.String(xWebResponse.ProcessorResponse)+"'";
			}
			if(xWebResponse.BatchNum != oldXWebResponse.BatchNum) {
				if(command!="") { command+=",";}
				command+="BatchNum = "+POut.Int(xWebResponse.BatchNum)+"";
			}
			if(xWebResponse.BatchAmount != oldXWebResponse.BatchAmount) {
				if(command!="") { command+=",";}
				command+="BatchAmount = "+POut.Double(xWebResponse.BatchAmount)+"";
			}
			if(xWebResponse.AccountExpirationDate.Date != oldXWebResponse.AccountExpirationDate.Date) {
				if(command!="") { command+=",";}
				command+="AccountExpirationDate = "+POut.Date(xWebResponse.AccountExpirationDate)+"";
			}
			if(xWebResponse.DebugError != oldXWebResponse.DebugError) {
				if(command!="") { command+=",";}
				command+="DebugError = "+DbHelper.ParamChar+"paramDebugError";
			}
			if(xWebResponse.PayNote != oldXWebResponse.PayNote) {
				if(command!="") { command+=",";}
				command+="PayNote = "+DbHelper.ParamChar+"paramPayNote";
			}
			if(xWebResponse.CCSource != oldXWebResponse.CCSource) {
				if(command!="") { command+=",";}
				command+="CCSource = "+POut.Int   ((int)xWebResponse.CCSource)+"";
			}
			if(xWebResponse.OrderId != oldXWebResponse.OrderId) {
				if(command!="") { command+=",";}
				command+="OrderId = '"+POut.String(xWebResponse.OrderId)+"'";
			}
			if(command=="") {
				return false;
			}
			if(xWebResponse.HpfUrl==null) {
				xWebResponse.HpfUrl="";
			}
			OdSqlParameter paramHpfUrl=new OdSqlParameter("paramHpfUrl",OdDbType.Text,POut.StringParam(xWebResponse.HpfUrl));
			if(xWebResponse.DebugError==null) {
				xWebResponse.DebugError="";
			}
			OdSqlParameter paramDebugError=new OdSqlParameter("paramDebugError",OdDbType.Text,POut.StringParam(xWebResponse.DebugError));
			if(xWebResponse.PayNote==null) {
				xWebResponse.PayNote="";
			}
			OdSqlParameter paramPayNote=new OdSqlParameter("paramPayNote",OdDbType.Text,POut.StringParam(xWebResponse.PayNote));
			command="UPDATE xwebresponse SET "+command
				+" WHERE XWebResponseNum = "+POut.Long(xWebResponse.XWebResponseNum);
			Db.NonQ(command,paramHpfUrl,paramDebugError,paramPayNote);
			return true;
		}

		///<summary>Returns true if Update(XWebResponse,XWebResponse) would make changes to the database.
		///Does not make any changes to the database and can be called before remoting role is checked.</summary>
		public static bool UpdateComparison(XWebResponse xWebResponse,XWebResponse oldXWebResponse) {
			if(xWebResponse.PatNum != oldXWebResponse.PatNum) {
				return true;
			}
			if(xWebResponse.ProvNum != oldXWebResponse.ProvNum) {
				return true;
			}
			if(xWebResponse.ClinicNum != oldXWebResponse.ClinicNum) {
				return true;
			}
			if(xWebResponse.PaymentNum != oldXWebResponse.PaymentNum) {
				return true;
			}
			//DateTEntry not allowed to change
			if(xWebResponse.DateTUpdate != oldXWebResponse.DateTUpdate) {
				return true;
			}
			if(xWebResponse.TransactionStatus != oldXWebResponse.TransactionStatus) {
				return true;
			}
			if(xWebResponse.ResponseCode != oldXWebResponse.ResponseCode) {
				return true;
			}
			if(xWebResponse.XWebResponseCode != oldXWebResponse.XWebResponseCode) {
				return true;
			}
			if(xWebResponse.ResponseDescription != oldXWebResponse.ResponseDescription) {
				return true;
			}
			if(xWebResponse.OTK != oldXWebResponse.OTK) {
				return true;
			}
			if(xWebResponse.HpfUrl != oldXWebResponse.HpfUrl) {
				return true;
			}
			if(xWebResponse.HpfExpiration != oldXWebResponse.HpfExpiration) {
				return true;
			}
			if(xWebResponse.TransactionID != oldXWebResponse.TransactionID) {
				return true;
			}
			if(xWebResponse.TransactionType != oldXWebResponse.TransactionType) {
				return true;
			}
			if(xWebResponse.Alias != oldXWebResponse.Alias) {
				return true;
			}
			if(xWebResponse.CardType != oldXWebResponse.CardType) {
				return true;
			}
			if(xWebResponse.CardBrand != oldXWebResponse.CardBrand) {
				return true;
			}
			if(xWebResponse.CardBrandShort != oldXWebResponse.CardBrandShort) {
				return true;
			}
			if(xWebResponse.MaskedAcctNum != oldXWebResponse.MaskedAcctNum) {
				return true;
			}
			if(xWebResponse.Amount != oldXWebResponse.Amount) {
				return true;
			}
			if(xWebResponse.ApprovalCode != oldXWebResponse.ApprovalCode) {
				return true;
			}
			if(xWebResponse.CardCodeResponse != oldXWebResponse.CardCodeResponse) {
				return true;
			}
			if(xWebResponse.ReceiptID != oldXWebResponse.ReceiptID) {
				return true;
			}
			if(xWebResponse.ExpDate != oldXWebResponse.ExpDate) {
				return true;
			}
			if(xWebResponse.EntryMethod != oldXWebResponse.EntryMethod) {
				return true;
			}
			if(xWebResponse.ProcessorResponse != oldXWebResponse.ProcessorResponse) {
				return true;
			}
			if(xWebResponse.BatchNum != oldXWebResponse.BatchNum) {
				return true;
			}
			if(xWebResponse.BatchAmount != oldXWebResponse.BatchAmount) {
				return true;
			}
			if(xWebResponse.AccountExpirationDate.Date != oldXWebResponse.AccountExpirationDate.Date) {
				return true;
			}
			if(xWebResponse.DebugError != oldXWebResponse.DebugError) {
				return true;
			}
			if(xWebResponse.PayNote != oldXWebResponse.PayNote) {
				return true;
			}
			if(xWebResponse.CCSource != oldXWebResponse.CCSource) {
				return true;
			}
			if(xWebResponse.OrderId != oldXWebResponse.OrderId) {
				return true;
			}
			return false;
		}

		///<summary>Deletes one XWebResponse from the database.</summary>
		public static void Delete(long xWebResponseNum) {
			string command="DELETE FROM xwebresponse "
				+"WHERE XWebResponseNum = "+POut.Long(xWebResponseNum);
			Db.NonQ(command);
		}

		///<summary>Deletes many XWebResponses from the database.</summary>
		public static void DeleteMany(List<long> listXWebResponseNums) {
			if(listXWebResponseNums==null || listXWebResponseNums.Count==0) {
				return;
			}
			string command="DELETE FROM xwebresponse "
				+"WHERE XWebResponseNum IN("+string.Join(",",listXWebResponseNums.Select(x => POut.Long(x)))+")";
			Db.NonQ(command);
		}

	}
}