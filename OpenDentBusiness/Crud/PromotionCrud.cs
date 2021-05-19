//This file is automatically generated.
//Do not attempt to make changes to this file because the changes will be erased and overwritten.
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;

namespace OpenDentBusiness.Crud{
	public class PromotionCrud {
		///<summary>Gets one Promotion object from the database using the primary key.  Returns null if not found.</summary>
		public static Promotion SelectOne(long promotionNum) {
			string command="SELECT * FROM promotion "
				+"WHERE PromotionNum = "+POut.Long(promotionNum);
			List<Promotion> list=TableToList(Db.GetTable(command));
			if(list.Count==0) {
				return null;
			}
			return list[0];
		}

		///<summary>Gets one Promotion object from the database using a query.</summary>
		public static Promotion SelectOne(string command) {
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				throw new ApplicationException("Not allowed to send sql directly.  Rewrite the calling class to not use this query:\r\n"+command);
			}
			List<Promotion> list=TableToList(Db.GetTable(command));
			if(list.Count==0) {
				return null;
			}
			return list[0];
		}

		///<summary>Gets a list of Promotion objects from the database using a query.</summary>
		public static List<Promotion> SelectMany(string command) {
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				throw new ApplicationException("Not allowed to send sql directly.  Rewrite the calling class to not use this query:\r\n"+command);
			}
			List<Promotion> list=TableToList(Db.GetTable(command));
			return list;
		}

		///<summary>Converts a DataTable to a list of objects.</summary>
		public static List<Promotion> TableToList(DataTable table) {
			List<Promotion> retVal=new List<Promotion>();
			Promotion promotion;
			foreach(DataRow row in table.Rows) {
				promotion=new Promotion();
				promotion.PromotionNum   = PIn.Long  (row["PromotionNum"].ToString());
				promotion.PromotionName  = PIn.String(row["PromotionName"].ToString());
				promotion.DateTimeCreated= PIn.Date  (row["DateTimeCreated"].ToString());
				promotion.ClinicNum      = PIn.Long  (row["ClinicNum"].ToString());
				promotion.TypePromotion  = (OpenDentBusiness.PromotionType)PIn.Int(row["TypePromotion"].ToString());
				retVal.Add(promotion);
			}
			return retVal;
		}

		///<summary>Converts a list of Promotion into a DataTable.</summary>
		public static DataTable ListToTable(List<Promotion> listPromotions,string tableName="") {
			if(string.IsNullOrEmpty(tableName)) {
				tableName="Promotion";
			}
			DataTable table=new DataTable(tableName);
			table.Columns.Add("PromotionNum");
			table.Columns.Add("PromotionName");
			table.Columns.Add("DateTimeCreated");
			table.Columns.Add("ClinicNum");
			table.Columns.Add("TypePromotion");
			foreach(Promotion promotion in listPromotions) {
				table.Rows.Add(new object[] {
					POut.Long  (promotion.PromotionNum),
					            promotion.PromotionName,
					POut.DateT (promotion.DateTimeCreated,false),
					POut.Long  (promotion.ClinicNum),
					POut.Int   ((int)promotion.TypePromotion),
				});
			}
			return table;
		}

		///<summary>Inserts one Promotion into the database.  Returns the new priKey.</summary>
		public static long Insert(Promotion promotion) {
			return Insert(promotion,false);
		}

		///<summary>Inserts one Promotion into the database.  Provides option to use the existing priKey.</summary>
		public static long Insert(Promotion promotion,bool useExistingPK) {
			if(!useExistingPK && PrefC.RandomKeys) {
				promotion.PromotionNum=ReplicationServers.GetKey("promotion","PromotionNum");
			}
			string command="INSERT INTO promotion (";
			if(useExistingPK || PrefC.RandomKeys) {
				command+="PromotionNum,";
			}
			command+="PromotionName,DateTimeCreated,ClinicNum,TypePromotion) VALUES(";
			if(useExistingPK || PrefC.RandomKeys) {
				command+=POut.Long(promotion.PromotionNum)+",";
			}
			command+=
				 "'"+POut.String(promotion.PromotionName)+"',"
				+    DbHelper.Now()+","
				+    POut.Long  (promotion.ClinicNum)+","
				+    POut.Int   ((int)promotion.TypePromotion)+")";
			if(useExistingPK || PrefC.RandomKeys) {
				Db.NonQ(command);
			}
			else {
				promotion.PromotionNum=Db.NonQ(command,true,"PromotionNum","promotion");
			}
			return promotion.PromotionNum;
		}

		///<summary>Inserts one Promotion into the database.  Returns the new priKey.  Doesn't use the cache.</summary>
		public static long InsertNoCache(Promotion promotion) {
			return InsertNoCache(promotion,false);
		}

		///<summary>Inserts one Promotion into the database.  Provides option to use the existing priKey.  Doesn't use the cache.</summary>
		public static long InsertNoCache(Promotion promotion,bool useExistingPK) {
			bool isRandomKeys=Prefs.GetBoolNoCache(PrefName.RandomPrimaryKeys);
			string command="INSERT INTO promotion (";
			if(!useExistingPK && isRandomKeys) {
				promotion.PromotionNum=ReplicationServers.GetKeyNoCache("promotion","PromotionNum");
			}
			if(isRandomKeys || useExistingPK) {
				command+="PromotionNum,";
			}
			command+="PromotionName,DateTimeCreated,ClinicNum,TypePromotion) VALUES(";
			if(isRandomKeys || useExistingPK) {
				command+=POut.Long(promotion.PromotionNum)+",";
			}
			command+=
				 "'"+POut.String(promotion.PromotionName)+"',"
				+    DbHelper.Now()+","
				+    POut.Long  (promotion.ClinicNum)+","
				+    POut.Int   ((int)promotion.TypePromotion)+")";
			if(useExistingPK || isRandomKeys) {
				Db.NonQ(command);
			}
			else {
				promotion.PromotionNum=Db.NonQ(command,true,"PromotionNum","promotion");
			}
			return promotion.PromotionNum;
		}

		///<summary>Updates one Promotion in the database.</summary>
		public static void Update(Promotion promotion) {
			string command="UPDATE promotion SET "
				+"PromotionName  = '"+POut.String(promotion.PromotionName)+"', "
				//DateTimeCreated not allowed to change
				+"ClinicNum      =  "+POut.Long  (promotion.ClinicNum)+", "
				+"TypePromotion  =  "+POut.Int   ((int)promotion.TypePromotion)+" "
				+"WHERE PromotionNum = "+POut.Long(promotion.PromotionNum);
			Db.NonQ(command);
		}

		///<summary>Updates one Promotion in the database.  Uses an old object to compare to, and only alters changed fields.  This prevents collisions and concurrency problems in heavily used tables.  Returns true if an update occurred.</summary>
		public static bool Update(Promotion promotion,Promotion oldPromotion) {
			string command="";
			if(promotion.PromotionName != oldPromotion.PromotionName) {
				if(command!="") { command+=",";}
				command+="PromotionName = '"+POut.String(promotion.PromotionName)+"'";
			}
			//DateTimeCreated not allowed to change
			if(promotion.ClinicNum != oldPromotion.ClinicNum) {
				if(command!="") { command+=",";}
				command+="ClinicNum = "+POut.Long(promotion.ClinicNum)+"";
			}
			if(promotion.TypePromotion != oldPromotion.TypePromotion) {
				if(command!="") { command+=",";}
				command+="TypePromotion = "+POut.Int   ((int)promotion.TypePromotion)+"";
			}
			if(command=="") {
				return false;
			}
			command="UPDATE promotion SET "+command
				+" WHERE PromotionNum = "+POut.Long(promotion.PromotionNum);
			Db.NonQ(command);
			return true;
		}

		///<summary>Returns true if Update(Promotion,Promotion) would make changes to the database.
		///Does not make any changes to the database and can be called before remoting role is checked.</summary>
		public static bool UpdateComparison(Promotion promotion,Promotion oldPromotion) {
			if(promotion.PromotionName != oldPromotion.PromotionName) {
				return true;
			}
			//DateTimeCreated not allowed to change
			if(promotion.ClinicNum != oldPromotion.ClinicNum) {
				return true;
			}
			if(promotion.TypePromotion != oldPromotion.TypePromotion) {
				return true;
			}
			return false;
		}

		///<summary>Deletes one Promotion from the database.</summary>
		public static void Delete(long promotionNum) {
			string command="DELETE FROM promotion "
				+"WHERE PromotionNum = "+POut.Long(promotionNum);
			Db.NonQ(command);
		}

		///<summary>Deletes many Promotions from the database.</summary>
		public static void DeleteMany(List<long> listPromotionNums) {
			if(listPromotionNums==null || listPromotionNums.Count==0) {
				return;
			}
			string command="DELETE FROM promotion "
				+"WHERE PromotionNum IN("+string.Join(",",listPromotionNums.Select(x => POut.Long(x)))+")";
			Db.NonQ(command);
		}

	}
}