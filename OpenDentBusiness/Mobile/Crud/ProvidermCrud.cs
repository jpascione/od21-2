//This file is automatically generated.
//Do not attempt to make changes to this file because the changes will be erased and overwritten.
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Drawing;

namespace OpenDentBusiness.Mobile.Crud{
	internal class ProvidermCrud {
		///<summary>Gets one Providerm object from the database using primaryKey1(CustomerNum) and primaryKey2.  Returns null if not found.</summary>
		internal static Providerm SelectOne(long customerNum,long provNum){
			string command="SELECT * FROM providerm "
				+"WHERE CustomerNum = "+POut.Long(customerNum)+" AND ProvNum = "+POut.Long(provNum);
			List<Providerm> list=TableToList(Db.GetTable(command));
			if(list.Count==0) {
				return null;
			}
			return list[0];
		}

		///<summary>Gets one Providerm object from the database using a query.</summary>
		internal static Providerm SelectOne(string command){
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				throw new ApplicationException("Not allowed to send sql directly.  Rewrite the calling class to not use this query:\r\n"+command);
			}
			List<Providerm> list=TableToList(Db.GetTable(command));
			if(list.Count==0) {
				return null;
			}
			return list[0];
		}

		///<summary>Gets a list of Providerm objects from the database using a query.</summary>
		internal static List<Providerm> SelectMany(string command){
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				throw new ApplicationException("Not allowed to send sql directly.  Rewrite the calling class to not use this query:\r\n"+command);
			}
			List<Providerm> list=TableToList(Db.GetTable(command));
			return list;
		}

		///<summary>Converts a DataTable to a list of objects.</summary>
		internal static List<Providerm> TableToList(DataTable table){
			List<Providerm> retVal=new List<Providerm>();
			Providerm providerm;
			for(int i=0;i<table.Rows.Count;i++) {
				providerm=new Providerm();
				providerm.CustomerNum= PIn.Long  (table.Rows[i]["CustomerNum"].ToString());
				providerm.ProvNum    = PIn.Long  (table.Rows[i]["ProvNum"].ToString());
				providerm.Abbr       = PIn.String(table.Rows[i]["Abbr"].ToString());
				providerm.IsSecondary= PIn.Bool  (table.Rows[i]["IsSecondary"].ToString());
				providerm.ProvColor  = Color.FromArgb(PIn.Int(table.Rows[i]["ProvColor"].ToString()));
				retVal.Add(providerm);
			}
			return retVal;
		}

		///<summary>Usually set useExistingPK=true.  Inserts one Providerm into the database.</summary>
		internal static long Insert(Providerm providerm,bool useExistingPK){
			if(!useExistingPK) {
				providerm.ProvNum=ReplicationServers.GetKey("providerm","ProvNum");
			}
			string command="INSERT INTO providerm (";
			command+="ProvNum,";
			command+="CustomerNum,Abbr,IsSecondary,ProvColor) VALUES(";
			command+=POut.Long(providerm.ProvNum)+",";
			command+=
				     POut.Long  (providerm.CustomerNum)+","
				+"'"+POut.String(providerm.Abbr)+"',"
				+    POut.Bool  (providerm.IsSecondary)+","
				+    POut.Int   (providerm.ProvColor.ToArgb())+")";
			Db.NonQ(command);//There is no autoincrement in the mobile server.
			return providerm.ProvNum;
		}

		///<summary>Updates one Providerm in the database.</summary>
		internal static void Update(Providerm providerm){
			string command="UPDATE providerm SET "
				+"Abbr       = '"+POut.String(providerm.Abbr)+"', "
				+"IsSecondary=  "+POut.Bool  (providerm.IsSecondary)+", "
				+"ProvColor  =  "+POut.Int   (providerm.ProvColor.ToArgb())+" "
				+"WHERE CustomerNum = "+POut.Long(providerm.CustomerNum)+" AND ProvNum = "+POut.Long(providerm.ProvNum);
			Db.NonQ(command);
		}

		///<summary>Deletes one Providerm from the database.</summary>
		internal static void Delete(long customerNum,long provNum){
			string command="DELETE FROM providerm "
				+"WHERE CustomerNum = "+POut.Long(customerNum)+" AND ProvNum = "+POut.Long(provNum);
			Db.NonQ(command);
		}

		///<summary>Converts one Provider object to its mobile equivalent.  Warning! CustomerNum will always be 0.</summary>
		internal static Providerm ConvertToM(Provider provider){
			Providerm providerm=new Providerm();
			//CustomerNum cannot be set.  Remains 0.
			providerm.ProvNum    =provider.ProvNum;
			providerm.Abbr       =provider.Abbr;
			providerm.IsSecondary=provider.IsSecondary;
			providerm.ProvColor  =provider.ProvColor;
			return providerm;
		}

	}
}