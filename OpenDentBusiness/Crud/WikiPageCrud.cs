//This file is automatically generated.
//Do not attempt to make changes to this file because the changes will be erased and overwritten.
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;

namespace OpenDentBusiness.Crud{
	public class WikiPageCrud {
		///<summary>Gets one WikiPage object from the database using the primary key.  Returns null if not found.</summary>
		public static WikiPage SelectOne(long wikiPageNum) {
			string command="SELECT * FROM wikipage "
				+"WHERE WikiPageNum = "+POut.Long(wikiPageNum);
			List<WikiPage> list=TableToList(Db.GetTable(command));
			if(list.Count==0) {
				return null;
			}
			return list[0];
		}

		///<summary>Gets one WikiPage object from the database using a query.</summary>
		public static WikiPage SelectOne(string command) {
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				throw new ApplicationException("Not allowed to send sql directly.  Rewrite the calling class to not use this query:\r\n"+command);
			}
			List<WikiPage> list=TableToList(Db.GetTable(command));
			if(list.Count==0) {
				return null;
			}
			return list[0];
		}

		///<summary>Gets a list of WikiPage objects from the database using a query.</summary>
		public static List<WikiPage> SelectMany(string command) {
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				throw new ApplicationException("Not allowed to send sql directly.  Rewrite the calling class to not use this query:\r\n"+command);
			}
			List<WikiPage> list=TableToList(Db.GetTable(command));
			return list;
		}

		///<summary>Converts a DataTable to a list of objects.</summary>
		public static List<WikiPage> TableToList(DataTable table) {
			List<WikiPage> retVal=new List<WikiPage>();
			WikiPage wikiPage;
			foreach(DataRow row in table.Rows) {
				wikiPage=new WikiPage();
				wikiPage.WikiPageNum         = PIn.Long  (row["WikiPageNum"].ToString());
				wikiPage.UserNum             = PIn.Long  (row["UserNum"].ToString());
				wikiPage.PageTitle           = PIn.String(row["PageTitle"].ToString());
				wikiPage.KeyWords            = PIn.String(row["KeyWords"].ToString());
				wikiPage.PageContent         = PIn.String(row["PageContent"].ToString());
				wikiPage.DateTimeSaved       = PIn.DateT (row["DateTimeSaved"].ToString());
				wikiPage.IsDeleted           = PIn.Bool  (row["IsDeleted"].ToString());
				wikiPage.IsDraft             = PIn.Bool  (row["IsDraft"].ToString());
				wikiPage.IsLocked            = PIn.Bool  (row["IsLocked"].ToString());
				wikiPage.PageContentPlainText= PIn.String(row["PageContentPlainText"].ToString());
				retVal.Add(wikiPage);
			}
			return retVal;
		}

		///<summary>Converts a list of WikiPage into a DataTable.</summary>
		public static DataTable ListToTable(List<WikiPage> listWikiPages,string tableName="") {
			if(string.IsNullOrEmpty(tableName)) {
				tableName="WikiPage";
			}
			DataTable table=new DataTable(tableName);
			table.Columns.Add("WikiPageNum");
			table.Columns.Add("UserNum");
			table.Columns.Add("PageTitle");
			table.Columns.Add("KeyWords");
			table.Columns.Add("PageContent");
			table.Columns.Add("DateTimeSaved");
			table.Columns.Add("IsDeleted");
			table.Columns.Add("IsDraft");
			table.Columns.Add("IsLocked");
			table.Columns.Add("PageContentPlainText");
			foreach(WikiPage wikiPage in listWikiPages) {
				table.Rows.Add(new object[] {
					POut.Long  (wikiPage.WikiPageNum),
					POut.Long  (wikiPage.UserNum),
					            wikiPage.PageTitle,
					            wikiPage.KeyWords,
					            wikiPage.PageContent,
					POut.DateT (wikiPage.DateTimeSaved,false),
					POut.Bool  (wikiPage.IsDeleted),
					POut.Bool  (wikiPage.IsDraft),
					POut.Bool  (wikiPage.IsLocked),
					            wikiPage.PageContentPlainText,
				});
			}
			return table;
		}

		///<summary>Inserts one WikiPage into the database.  Returns the new priKey.</summary>
		public static long Insert(WikiPage wikiPage) {
			return Insert(wikiPage,false);
		}

		///<summary>Inserts one WikiPage into the database.  Provides option to use the existing priKey.</summary>
		public static long Insert(WikiPage wikiPage,bool useExistingPK) {
			if(!useExistingPK && PrefC.RandomKeys) {
				wikiPage.WikiPageNum=ReplicationServers.GetKey("wikipage","WikiPageNum");
			}
			string command="INSERT INTO wikipage (";
			if(useExistingPK || PrefC.RandomKeys) {
				command+="WikiPageNum,";
			}
			command+="UserNum,PageTitle,KeyWords,PageContent,DateTimeSaved,IsDeleted,IsDraft,IsLocked,PageContentPlainText) VALUES(";
			if(useExistingPK || PrefC.RandomKeys) {
				command+=POut.Long(wikiPage.WikiPageNum)+",";
			}
			command+=
				     POut.Long  (wikiPage.UserNum)+","
				+"'"+POut.String(wikiPage.PageTitle)+"',"
				+"'"+POut.String(wikiPage.KeyWords)+"',"
				+    DbHelper.ParamChar+"paramPageContent,"
				+    DbHelper.Now()+","
				+    POut.Bool  (wikiPage.IsDeleted)+","
				+    POut.Bool  (wikiPage.IsDraft)+","
				+    POut.Bool  (wikiPage.IsLocked)+","
				+    DbHelper.ParamChar+"paramPageContentPlainText)";
			if(wikiPage.PageContent==null) {
				wikiPage.PageContent="";
			}
			OdSqlParameter paramPageContent=new OdSqlParameter("paramPageContent",OdDbType.Text,POut.StringParam(wikiPage.PageContent));
			if(wikiPage.PageContentPlainText==null) {
				wikiPage.PageContentPlainText="";
			}
			OdSqlParameter paramPageContentPlainText=new OdSqlParameter("paramPageContentPlainText",OdDbType.Text,POut.StringParam(wikiPage.PageContentPlainText));
			if(useExistingPK || PrefC.RandomKeys) {
				Db.NonQ(command,paramPageContent,paramPageContentPlainText);
			}
			else {
				wikiPage.WikiPageNum=Db.NonQ(command,true,"WikiPageNum","wikiPage",paramPageContent,paramPageContentPlainText);
			}
			return wikiPage.WikiPageNum;
		}

		///<summary>Inserts one WikiPage into the database.  Returns the new priKey.  Doesn't use the cache.</summary>
		public static long InsertNoCache(WikiPage wikiPage) {
			return InsertNoCache(wikiPage,false);
		}

		///<summary>Inserts one WikiPage into the database.  Provides option to use the existing priKey.  Doesn't use the cache.</summary>
		public static long InsertNoCache(WikiPage wikiPage,bool useExistingPK) {
			bool isRandomKeys=Prefs.GetBoolNoCache(PrefName.RandomPrimaryKeys);
			string command="INSERT INTO wikipage (";
			if(!useExistingPK && isRandomKeys) {
				wikiPage.WikiPageNum=ReplicationServers.GetKeyNoCache("wikipage","WikiPageNum");
			}
			if(isRandomKeys || useExistingPK) {
				command+="WikiPageNum,";
			}
			command+="UserNum,PageTitle,KeyWords,PageContent,DateTimeSaved,IsDeleted,IsDraft,IsLocked,PageContentPlainText) VALUES(";
			if(isRandomKeys || useExistingPK) {
				command+=POut.Long(wikiPage.WikiPageNum)+",";
			}
			command+=
				     POut.Long  (wikiPage.UserNum)+","
				+"'"+POut.String(wikiPage.PageTitle)+"',"
				+"'"+POut.String(wikiPage.KeyWords)+"',"
				+    DbHelper.ParamChar+"paramPageContent,"
				+    DbHelper.Now()+","
				+    POut.Bool  (wikiPage.IsDeleted)+","
				+    POut.Bool  (wikiPage.IsDraft)+","
				+    POut.Bool  (wikiPage.IsLocked)+","
				+    DbHelper.ParamChar+"paramPageContentPlainText)";
			if(wikiPage.PageContent==null) {
				wikiPage.PageContent="";
			}
			OdSqlParameter paramPageContent=new OdSqlParameter("paramPageContent",OdDbType.Text,POut.StringParam(wikiPage.PageContent));
			if(wikiPage.PageContentPlainText==null) {
				wikiPage.PageContentPlainText="";
			}
			OdSqlParameter paramPageContentPlainText=new OdSqlParameter("paramPageContentPlainText",OdDbType.Text,POut.StringParam(wikiPage.PageContentPlainText));
			if(useExistingPK || isRandomKeys) {
				Db.NonQ(command,paramPageContent,paramPageContentPlainText);
			}
			else {
				wikiPage.WikiPageNum=Db.NonQ(command,true,"WikiPageNum","wikiPage",paramPageContent,paramPageContentPlainText);
			}
			return wikiPage.WikiPageNum;
		}

		///<summary>Updates one WikiPage in the database.</summary>
		public static void Update(WikiPage wikiPage) {
			string command="UPDATE wikipage SET "
				+"UserNum             =  "+POut.Long  (wikiPage.UserNum)+", "
				+"PageTitle           = '"+POut.String(wikiPage.PageTitle)+"', "
				+"KeyWords            = '"+POut.String(wikiPage.KeyWords)+"', "
				+"PageContent         =  "+DbHelper.ParamChar+"paramPageContent, "
				+"DateTimeSaved       =  "+POut.DateT (wikiPage.DateTimeSaved)+", "
				+"IsDeleted           =  "+POut.Bool  (wikiPage.IsDeleted)+", "
				+"IsDraft             =  "+POut.Bool  (wikiPage.IsDraft)+", "
				+"IsLocked            =  "+POut.Bool  (wikiPage.IsLocked)+", "
				+"PageContentPlainText=  "+DbHelper.ParamChar+"paramPageContentPlainText "
				+"WHERE WikiPageNum = "+POut.Long(wikiPage.WikiPageNum);
			if(wikiPage.PageContent==null) {
				wikiPage.PageContent="";
			}
			OdSqlParameter paramPageContent=new OdSqlParameter("paramPageContent",OdDbType.Text,POut.StringParam(wikiPage.PageContent));
			if(wikiPage.PageContentPlainText==null) {
				wikiPage.PageContentPlainText="";
			}
			OdSqlParameter paramPageContentPlainText=new OdSqlParameter("paramPageContentPlainText",OdDbType.Text,POut.StringParam(wikiPage.PageContentPlainText));
			Db.NonQ(command,paramPageContent,paramPageContentPlainText);
		}

		///<summary>Updates one WikiPage in the database.  Uses an old object to compare to, and only alters changed fields.  This prevents collisions and concurrency problems in heavily used tables.  Returns true if an update occurred.</summary>
		public static bool Update(WikiPage wikiPage,WikiPage oldWikiPage) {
			string command="";
			if(wikiPage.UserNum != oldWikiPage.UserNum) {
				if(command!="") { command+=",";}
				command+="UserNum = "+POut.Long(wikiPage.UserNum)+"";
			}
			if(wikiPage.PageTitle != oldWikiPage.PageTitle) {
				if(command!="") { command+=",";}
				command+="PageTitle = '"+POut.String(wikiPage.PageTitle)+"'";
			}
			if(wikiPage.KeyWords != oldWikiPage.KeyWords) {
				if(command!="") { command+=",";}
				command+="KeyWords = '"+POut.String(wikiPage.KeyWords)+"'";
			}
			if(wikiPage.PageContent != oldWikiPage.PageContent) {
				if(command!="") { command+=",";}
				command+="PageContent = "+DbHelper.ParamChar+"paramPageContent";
			}
			if(wikiPage.DateTimeSaved != oldWikiPage.DateTimeSaved) {
				if(command!="") { command+=",";}
				command+="DateTimeSaved = "+POut.DateT(wikiPage.DateTimeSaved)+"";
			}
			if(wikiPage.IsDeleted != oldWikiPage.IsDeleted) {
				if(command!="") { command+=",";}
				command+="IsDeleted = "+POut.Bool(wikiPage.IsDeleted)+"";
			}
			if(wikiPage.IsDraft != oldWikiPage.IsDraft) {
				if(command!="") { command+=",";}
				command+="IsDraft = "+POut.Bool(wikiPage.IsDraft)+"";
			}
			if(wikiPage.IsLocked != oldWikiPage.IsLocked) {
				if(command!="") { command+=",";}
				command+="IsLocked = "+POut.Bool(wikiPage.IsLocked)+"";
			}
			if(wikiPage.PageContentPlainText != oldWikiPage.PageContentPlainText) {
				if(command!="") { command+=",";}
				command+="PageContentPlainText = "+DbHelper.ParamChar+"paramPageContentPlainText";
			}
			if(command=="") {
				return false;
			}
			if(wikiPage.PageContent==null) {
				wikiPage.PageContent="";
			}
			OdSqlParameter paramPageContent=new OdSqlParameter("paramPageContent",OdDbType.Text,POut.StringParam(wikiPage.PageContent));
			if(wikiPage.PageContentPlainText==null) {
				wikiPage.PageContentPlainText="";
			}
			OdSqlParameter paramPageContentPlainText=new OdSqlParameter("paramPageContentPlainText",OdDbType.Text,POut.StringParam(wikiPage.PageContentPlainText));
			command="UPDATE wikipage SET "+command
				+" WHERE WikiPageNum = "+POut.Long(wikiPage.WikiPageNum);
			Db.NonQ(command,paramPageContent,paramPageContentPlainText);
			return true;
		}

		///<summary>Returns true if Update(WikiPage,WikiPage) would make changes to the database.
		///Does not make any changes to the database and can be called before remoting role is checked.</summary>
		public static bool UpdateComparison(WikiPage wikiPage,WikiPage oldWikiPage) {
			if(wikiPage.UserNum != oldWikiPage.UserNum) {
				return true;
			}
			if(wikiPage.PageTitle != oldWikiPage.PageTitle) {
				return true;
			}
			if(wikiPage.KeyWords != oldWikiPage.KeyWords) {
				return true;
			}
			if(wikiPage.PageContent != oldWikiPage.PageContent) {
				return true;
			}
			if(wikiPage.DateTimeSaved != oldWikiPage.DateTimeSaved) {
				return true;
			}
			if(wikiPage.IsDeleted != oldWikiPage.IsDeleted) {
				return true;
			}
			if(wikiPage.IsDraft != oldWikiPage.IsDraft) {
				return true;
			}
			if(wikiPage.IsLocked != oldWikiPage.IsLocked) {
				return true;
			}
			if(wikiPage.PageContentPlainText != oldWikiPage.PageContentPlainText) {
				return true;
			}
			return false;
		}

		///<summary>Deletes one WikiPage from the database.</summary>
		public static void Delete(long wikiPageNum) {
			string command="DELETE FROM wikipage "
				+"WHERE WikiPageNum = "+POut.Long(wikiPageNum);
			Db.NonQ(command);
		}

		///<summary>Deletes many WikiPages from the database.</summary>
		public static void DeleteMany(List<long> listWikiPageNums) {
			if(listWikiPageNums==null || listWikiPageNums.Count==0) {
				return;
			}
			string command="DELETE FROM wikipage "
				+"WHERE WikiPageNum IN("+string.Join(",",listWikiPageNums.Select(x => POut.Long(x)))+")";
			Db.NonQ(command);
		}

	}
}