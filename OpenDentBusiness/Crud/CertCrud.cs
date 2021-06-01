//This file is automatically generated.
//Do not attempt to make changes to this file because the changes will be erased and overwritten.
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;

namespace OpenDentBusiness.Crud{
	public class CertCrud {
		///<summary>Gets one Cert object from the database using the primary key.  Returns null if not found.</summary>
		public static Cert SelectOne(long certNum) {
			string command="SELECT * FROM cert "
				+"WHERE CertNum = "+POut.Long(certNum);
			List<Cert> list=TableToList(Db.GetTable(command));
			if(list.Count==0) {
				return null;
			}
			return list[0];
		}

		///<summary>Gets one Cert object from the database using a query.</summary>
		public static Cert SelectOne(string command) {
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				throw new ApplicationException("Not allowed to send sql directly.  Rewrite the calling class to not use this query:\r\n"+command);
			}
			List<Cert> list=TableToList(Db.GetTable(command));
			if(list.Count==0) {
				return null;
			}
			return list[0];
		}

		///<summary>Gets a list of Cert objects from the database using a query.</summary>
		public static List<Cert> SelectMany(string command) {
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				throw new ApplicationException("Not allowed to send sql directly.  Rewrite the calling class to not use this query:\r\n"+command);
			}
			List<Cert> list=TableToList(Db.GetTable(command));
			return list;
		}

		///<summary>Converts a DataTable to a list of objects.</summary>
		public static List<Cert> TableToList(DataTable table) {
			List<Cert> retVal=new List<Cert>();
			Cert cert;
			foreach(DataRow row in table.Rows) {
				cert=new Cert();
				cert.CertNum        = PIn.Long  (row["CertNum"].ToString());
				cert.Description    = PIn.String(row["Description"].ToString());
				cert.WikiPageLink   = PIn.String(row["WikiPageLink"].ToString());
				cert.ItemOrder      = PIn.Int   (row["ItemOrder"].ToString());
				cert.IsHidden       = PIn.Bool  (row["IsHidden"].ToString());
				cert.CertCategoryNum= PIn.Long  (row["CertCategoryNum"].ToString());
				retVal.Add(cert);
			}
			return retVal;
		}

		///<summary>Converts a list of Cert into a DataTable.</summary>
		public static DataTable ListToTable(List<Cert> listCerts,string tableName="") {
			if(string.IsNullOrEmpty(tableName)) {
				tableName="Cert";
			}
			DataTable table=new DataTable(tableName);
			table.Columns.Add("CertNum");
			table.Columns.Add("Description");
			table.Columns.Add("WikiPageLink");
			table.Columns.Add("ItemOrder");
			table.Columns.Add("IsHidden");
			table.Columns.Add("CertCategoryNum");
			foreach(Cert cert in listCerts) {
				table.Rows.Add(new object[] {
					POut.Long  (cert.CertNum),
					            cert.Description,
					            cert.WikiPageLink,
					POut.Int   (cert.ItemOrder),
					POut.Bool  (cert.IsHidden),
					POut.Long  (cert.CertCategoryNum),
				});
			}
			return table;
		}

		///<summary>Inserts one Cert into the database.  Returns the new priKey.</summary>
		public static long Insert(Cert cert) {
			return Insert(cert,false);
		}

		///<summary>Inserts one Cert into the database.  Provides option to use the existing priKey.</summary>
		public static long Insert(Cert cert,bool useExistingPK) {
			if(!useExistingPK && PrefC.RandomKeys) {
				cert.CertNum=ReplicationServers.GetKey("cert","CertNum");
			}
			string command="INSERT INTO cert (";
			if(useExistingPK || PrefC.RandomKeys) {
				command+="CertNum,";
			}
			command+="Description,WikiPageLink,ItemOrder,IsHidden,CertCategoryNum) VALUES(";
			if(useExistingPK || PrefC.RandomKeys) {
				command+=POut.Long(cert.CertNum)+",";
			}
			command+=
				 "'"+POut.String(cert.Description)+"',"
				+"'"+POut.String(cert.WikiPageLink)+"',"
				+    POut.Int   (cert.ItemOrder)+","
				+    POut.Bool  (cert.IsHidden)+","
				+    POut.Long  (cert.CertCategoryNum)+")";
			if(useExistingPK || PrefC.RandomKeys) {
				Db.NonQ(command);
			}
			else {
				cert.CertNum=Db.NonQ(command,true,"CertNum","cert");
			}
			return cert.CertNum;
		}

		///<summary>Inserts one Cert into the database.  Returns the new priKey.  Doesn't use the cache.</summary>
		public static long InsertNoCache(Cert cert) {
			return InsertNoCache(cert,false);
		}

		///<summary>Inserts one Cert into the database.  Provides option to use the existing priKey.  Doesn't use the cache.</summary>
		public static long InsertNoCache(Cert cert,bool useExistingPK) {
			bool isRandomKeys=Prefs.GetBoolNoCache(PrefName.RandomPrimaryKeys);
			string command="INSERT INTO cert (";
			if(!useExistingPK && isRandomKeys) {
				cert.CertNum=ReplicationServers.GetKeyNoCache("cert","CertNum");
			}
			if(isRandomKeys || useExistingPK) {
				command+="CertNum,";
			}
			command+="Description,WikiPageLink,ItemOrder,IsHidden,CertCategoryNum) VALUES(";
			if(isRandomKeys || useExistingPK) {
				command+=POut.Long(cert.CertNum)+",";
			}
			command+=
				 "'"+POut.String(cert.Description)+"',"
				+"'"+POut.String(cert.WikiPageLink)+"',"
				+    POut.Int   (cert.ItemOrder)+","
				+    POut.Bool  (cert.IsHidden)+","
				+    POut.Long  (cert.CertCategoryNum)+")";
			if(useExistingPK || isRandomKeys) {
				Db.NonQ(command);
			}
			else {
				cert.CertNum=Db.NonQ(command,true,"CertNum","cert");
			}
			return cert.CertNum;
		}

		///<summary>Updates one Cert in the database.</summary>
		public static void Update(Cert cert) {
			string command="UPDATE cert SET "
				+"Description    = '"+POut.String(cert.Description)+"', "
				+"WikiPageLink   = '"+POut.String(cert.WikiPageLink)+"', "
				+"ItemOrder      =  "+POut.Int   (cert.ItemOrder)+", "
				+"IsHidden       =  "+POut.Bool  (cert.IsHidden)+", "
				+"CertCategoryNum=  "+POut.Long  (cert.CertCategoryNum)+" "
				+"WHERE CertNum = "+POut.Long(cert.CertNum);
			Db.NonQ(command);
		}

		///<summary>Updates one Cert in the database.  Uses an old object to compare to, and only alters changed fields.  This prevents collisions and concurrency problems in heavily used tables.  Returns true if an update occurred.</summary>
		public static bool Update(Cert cert,Cert oldCert) {
			string command="";
			if(cert.Description != oldCert.Description) {
				if(command!="") { command+=",";}
				command+="Description = '"+POut.String(cert.Description)+"'";
			}
			if(cert.WikiPageLink != oldCert.WikiPageLink) {
				if(command!="") { command+=",";}
				command+="WikiPageLink = '"+POut.String(cert.WikiPageLink)+"'";
			}
			if(cert.ItemOrder != oldCert.ItemOrder) {
				if(command!="") { command+=",";}
				command+="ItemOrder = "+POut.Int(cert.ItemOrder)+"";
			}
			if(cert.IsHidden != oldCert.IsHidden) {
				if(command!="") { command+=",";}
				command+="IsHidden = "+POut.Bool(cert.IsHidden)+"";
			}
			if(cert.CertCategoryNum != oldCert.CertCategoryNum) {
				if(command!="") { command+=",";}
				command+="CertCategoryNum = "+POut.Long(cert.CertCategoryNum)+"";
			}
			if(command=="") {
				return false;
			}
			command="UPDATE cert SET "+command
				+" WHERE CertNum = "+POut.Long(cert.CertNum);
			Db.NonQ(command);
			return true;
		}

		///<summary>Returns true if Update(Cert,Cert) would make changes to the database.
		///Does not make any changes to the database and can be called before remoting role is checked.</summary>
		public static bool UpdateComparison(Cert cert,Cert oldCert) {
			if(cert.Description != oldCert.Description) {
				return true;
			}
			if(cert.WikiPageLink != oldCert.WikiPageLink) {
				return true;
			}
			if(cert.ItemOrder != oldCert.ItemOrder) {
				return true;
			}
			if(cert.IsHidden != oldCert.IsHidden) {
				return true;
			}
			if(cert.CertCategoryNum != oldCert.CertCategoryNum) {
				return true;
			}
			return false;
		}

		///<summary>Deletes one Cert from the database.</summary>
		public static void Delete(long certNum) {
			string command="DELETE FROM cert "
				+"WHERE CertNum = "+POut.Long(certNum);
			Db.NonQ(command);
		}

		///<summary>Deletes many Certs from the database.</summary>
		public static void DeleteMany(List<long> listCertNums) {
			if(listCertNums==null || listCertNums.Count==0) {
				return;
			}
			string command="DELETE FROM cert "
				+"WHERE CertNum IN("+string.Join(",",listCertNums.Select(x => POut.Long(x)))+")";
			Db.NonQ(command);
		}

	}
}