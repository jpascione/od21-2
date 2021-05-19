//This file is automatically generated.
//Do not attempt to make changes to this file because the changes will be erased and overwritten.
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;

namespace OpenDentBusiness.Crud{
	public class InsBlueBookRuleCrud {
		///<summary>Gets one InsBlueBookRule object from the database using the primary key.  Returns null if not found.</summary>
		public static InsBlueBookRule SelectOne(long insBlueBookRuleNum) {
			string command="SELECT * FROM insbluebookrule "
				+"WHERE InsBlueBookRuleNum = "+POut.Long(insBlueBookRuleNum);
			List<InsBlueBookRule> list=TableToList(Db.GetTable(command));
			if(list.Count==0) {
				return null;
			}
			return list[0];
		}

		///<summary>Gets one InsBlueBookRule object from the database using a query.</summary>
		public static InsBlueBookRule SelectOne(string command) {
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				throw new ApplicationException("Not allowed to send sql directly.  Rewrite the calling class to not use this query:\r\n"+command);
			}
			List<InsBlueBookRule> list=TableToList(Db.GetTable(command));
			if(list.Count==0) {
				return null;
			}
			return list[0];
		}

		///<summary>Gets a list of InsBlueBookRule objects from the database using a query.</summary>
		public static List<InsBlueBookRule> SelectMany(string command) {
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				throw new ApplicationException("Not allowed to send sql directly.  Rewrite the calling class to not use this query:\r\n"+command);
			}
			List<InsBlueBookRule> list=TableToList(Db.GetTable(command));
			return list;
		}

		///<summary>Converts a DataTable to a list of objects.</summary>
		public static List<InsBlueBookRule> TableToList(DataTable table) {
			List<InsBlueBookRule> retVal=new List<InsBlueBookRule>();
			InsBlueBookRule insBlueBookRule;
			foreach(DataRow row in table.Rows) {
				insBlueBookRule=new InsBlueBookRule();
				insBlueBookRule.InsBlueBookRuleNum= PIn.Long  (row["InsBlueBookRuleNum"].ToString());
				insBlueBookRule.ItemOrder         = PIn.Int   (row["ItemOrder"].ToString());
				insBlueBookRule.RuleType          = (OpenDentBusiness.InsBlueBookRuleType)PIn.Int(row["RuleType"].ToString());
				insBlueBookRule.LimitValue        = PIn.Int   (row["LimitValue"].ToString());
				insBlueBookRule.LimitType         = (OpenDentBusiness.InsBlueBookRuleLimitType)PIn.Int(row["LimitType"].ToString());
				retVal.Add(insBlueBookRule);
			}
			return retVal;
		}

		///<summary>Converts a list of InsBlueBookRule into a DataTable.</summary>
		public static DataTable ListToTable(List<InsBlueBookRule> listInsBlueBookRules,string tableName="") {
			if(string.IsNullOrEmpty(tableName)) {
				tableName="InsBlueBookRule";
			}
			DataTable table=new DataTable(tableName);
			table.Columns.Add("InsBlueBookRuleNum");
			table.Columns.Add("ItemOrder");
			table.Columns.Add("RuleType");
			table.Columns.Add("LimitValue");
			table.Columns.Add("LimitType");
			foreach(InsBlueBookRule insBlueBookRule in listInsBlueBookRules) {
				table.Rows.Add(new object[] {
					POut.Long  (insBlueBookRule.InsBlueBookRuleNum),
					POut.Int   (insBlueBookRule.ItemOrder),
					POut.Int   ((int)insBlueBookRule.RuleType),
					POut.Int   (insBlueBookRule.LimitValue),
					POut.Int   ((int)insBlueBookRule.LimitType),
				});
			}
			return table;
		}

		///<summary>Inserts one InsBlueBookRule into the database.  Returns the new priKey.</summary>
		public static long Insert(InsBlueBookRule insBlueBookRule) {
			return Insert(insBlueBookRule,false);
		}

		///<summary>Inserts one InsBlueBookRule into the database.  Provides option to use the existing priKey.</summary>
		public static long Insert(InsBlueBookRule insBlueBookRule,bool useExistingPK) {
			if(!useExistingPK && PrefC.RandomKeys) {
				insBlueBookRule.InsBlueBookRuleNum=ReplicationServers.GetKey("insbluebookrule","InsBlueBookRuleNum");
			}
			string command="INSERT INTO insbluebookrule (";
			if(useExistingPK || PrefC.RandomKeys) {
				command+="InsBlueBookRuleNum,";
			}
			command+="ItemOrder,RuleType,LimitValue,LimitType) VALUES(";
			if(useExistingPK || PrefC.RandomKeys) {
				command+=POut.Long(insBlueBookRule.InsBlueBookRuleNum)+",";
			}
			command+=
				     POut.Int   (insBlueBookRule.ItemOrder)+","
				+    POut.Int   ((int)insBlueBookRule.RuleType)+","
				+    POut.Int   (insBlueBookRule.LimitValue)+","
				+    POut.Int   ((int)insBlueBookRule.LimitType)+")";
			if(useExistingPK || PrefC.RandomKeys) {
				Db.NonQ(command);
			}
			else {
				insBlueBookRule.InsBlueBookRuleNum=Db.NonQ(command,true,"InsBlueBookRuleNum","insBlueBookRule");
			}
			return insBlueBookRule.InsBlueBookRuleNum;
		}

		///<summary>Inserts one InsBlueBookRule into the database.  Returns the new priKey.  Doesn't use the cache.</summary>
		public static long InsertNoCache(InsBlueBookRule insBlueBookRule) {
			return InsertNoCache(insBlueBookRule,false);
		}

		///<summary>Inserts one InsBlueBookRule into the database.  Provides option to use the existing priKey.  Doesn't use the cache.</summary>
		public static long InsertNoCache(InsBlueBookRule insBlueBookRule,bool useExistingPK) {
			bool isRandomKeys=Prefs.GetBoolNoCache(PrefName.RandomPrimaryKeys);
			string command="INSERT INTO insbluebookrule (";
			if(!useExistingPK && isRandomKeys) {
				insBlueBookRule.InsBlueBookRuleNum=ReplicationServers.GetKeyNoCache("insbluebookrule","InsBlueBookRuleNum");
			}
			if(isRandomKeys || useExistingPK) {
				command+="InsBlueBookRuleNum,";
			}
			command+="ItemOrder,RuleType,LimitValue,LimitType) VALUES(";
			if(isRandomKeys || useExistingPK) {
				command+=POut.Long(insBlueBookRule.InsBlueBookRuleNum)+",";
			}
			command+=
				     POut.Int   (insBlueBookRule.ItemOrder)+","
				+    POut.Int   ((int)insBlueBookRule.RuleType)+","
				+    POut.Int   (insBlueBookRule.LimitValue)+","
				+    POut.Int   ((int)insBlueBookRule.LimitType)+")";
			if(useExistingPK || isRandomKeys) {
				Db.NonQ(command);
			}
			else {
				insBlueBookRule.InsBlueBookRuleNum=Db.NonQ(command,true,"InsBlueBookRuleNum","insBlueBookRule");
			}
			return insBlueBookRule.InsBlueBookRuleNum;
		}

		///<summary>Updates one InsBlueBookRule in the database.</summary>
		public static void Update(InsBlueBookRule insBlueBookRule) {
			string command="UPDATE insbluebookrule SET "
				+"ItemOrder         =  "+POut.Int   (insBlueBookRule.ItemOrder)+", "
				+"RuleType          =  "+POut.Int   ((int)insBlueBookRule.RuleType)+", "
				+"LimitValue        =  "+POut.Int   (insBlueBookRule.LimitValue)+", "
				+"LimitType         =  "+POut.Int   ((int)insBlueBookRule.LimitType)+" "
				+"WHERE InsBlueBookRuleNum = "+POut.Long(insBlueBookRule.InsBlueBookRuleNum);
			Db.NonQ(command);
		}

		///<summary>Updates one InsBlueBookRule in the database.  Uses an old object to compare to, and only alters changed fields.  This prevents collisions and concurrency problems in heavily used tables.  Returns true if an update occurred.</summary>
		public static bool Update(InsBlueBookRule insBlueBookRule,InsBlueBookRule oldInsBlueBookRule) {
			string command="";
			if(insBlueBookRule.ItemOrder != oldInsBlueBookRule.ItemOrder) {
				if(command!="") { command+=",";}
				command+="ItemOrder = "+POut.Int(insBlueBookRule.ItemOrder)+"";
			}
			if(insBlueBookRule.RuleType != oldInsBlueBookRule.RuleType) {
				if(command!="") { command+=",";}
				command+="RuleType = "+POut.Int   ((int)insBlueBookRule.RuleType)+"";
			}
			if(insBlueBookRule.LimitValue != oldInsBlueBookRule.LimitValue) {
				if(command!="") { command+=",";}
				command+="LimitValue = "+POut.Int(insBlueBookRule.LimitValue)+"";
			}
			if(insBlueBookRule.LimitType != oldInsBlueBookRule.LimitType) {
				if(command!="") { command+=",";}
				command+="LimitType = "+POut.Int   ((int)insBlueBookRule.LimitType)+"";
			}
			if(command=="") {
				return false;
			}
			command="UPDATE insbluebookrule SET "+command
				+" WHERE InsBlueBookRuleNum = "+POut.Long(insBlueBookRule.InsBlueBookRuleNum);
			Db.NonQ(command);
			return true;
		}

		///<summary>Returns true if Update(InsBlueBookRule,InsBlueBookRule) would make changes to the database.
		///Does not make any changes to the database and can be called before remoting role is checked.</summary>
		public static bool UpdateComparison(InsBlueBookRule insBlueBookRule,InsBlueBookRule oldInsBlueBookRule) {
			if(insBlueBookRule.ItemOrder != oldInsBlueBookRule.ItemOrder) {
				return true;
			}
			if(insBlueBookRule.RuleType != oldInsBlueBookRule.RuleType) {
				return true;
			}
			if(insBlueBookRule.LimitValue != oldInsBlueBookRule.LimitValue) {
				return true;
			}
			if(insBlueBookRule.LimitType != oldInsBlueBookRule.LimitType) {
				return true;
			}
			return false;
		}

		///<summary>Deletes one InsBlueBookRule from the database.</summary>
		public static void Delete(long insBlueBookRuleNum) {
			string command="DELETE FROM insbluebookrule "
				+"WHERE InsBlueBookRuleNum = "+POut.Long(insBlueBookRuleNum);
			Db.NonQ(command);
		}

		///<summary>Deletes many InsBlueBookRules from the database.</summary>
		public static void DeleteMany(List<long> listInsBlueBookRuleNums) {
			if(listInsBlueBookRuleNums==null || listInsBlueBookRuleNums.Count==0) {
				return;
			}
			string command="DELETE FROM insbluebookrule "
				+"WHERE InsBlueBookRuleNum IN("+string.Join(",",listInsBlueBookRuleNums.Select(x => POut.Long(x)))+")";
			Db.NonQ(command);
		}

	}
}