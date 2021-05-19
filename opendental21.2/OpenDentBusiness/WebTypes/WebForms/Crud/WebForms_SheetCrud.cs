//This file is automatically generated.
//Do not attempt to make changes to this file because the changes will be erased and overwritten.
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Drawing;

namespace OpenDentBusiness.WebTypes.WebForms.Crud{
	public class WebForms_SheetCrud {
		///<summary>Gets one WebForms_Sheet object from the database using the primary key.  Returns null if not found.</summary>
		public static WebForms_Sheet SelectOne(long sheetID) {
			string command="SELECT * FROM webforms_sheet "
				+"WHERE SheetID = "+POut.Long(sheetID);
			List<WebForms_Sheet> list=TableToList(Db.GetTable(command));
			if(list.Count==0) {
				return null;
			}
			return list[0];
		}

		///<summary>Gets one WebForms_Sheet object from the database using a query.</summary>
		public static WebForms_Sheet SelectOne(string command) {
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				throw new ApplicationException("Not allowed to send sql directly.  Rewrite the calling class to not use this query:\r\n"+command);
			}
			List<WebForms_Sheet> list=TableToList(Db.GetTable(command));
			if(list.Count==0) {
				return null;
			}
			return list[0];
		}

		///<summary>Gets a list of WebForms_Sheet objects from the database using a query.</summary>
		public static List<WebForms_Sheet> SelectMany(string command) {
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				throw new ApplicationException("Not allowed to send sql directly.  Rewrite the calling class to not use this query:\r\n"+command);
			}
			List<WebForms_Sheet> list=TableToList(Db.GetTable(command));
			return list;
		}

		///<summary>Converts a list of WebForms_Sheet into a DataTable.</summary>
		public static DataTable ListToTable(List<WebForms_Sheet> listWebForms_Sheets,string tableName="") {
			if(string.IsNullOrEmpty(tableName)) {
				tableName="WebForms_Sheet";
			}
			DataTable table=new DataTable(tableName);
			table.Columns.Add("SheetID");
			table.Columns.Add("DentalOfficeID");
			table.Columns.Add("Description");
			table.Columns.Add("SheetType");
			table.Columns.Add("DateTimeSheet");
			table.Columns.Add("FontSize");
			table.Columns.Add("FontName");
			table.Columns.Add("Width");
			table.Columns.Add("Height");
			table.Columns.Add("IsLandscape");
			table.Columns.Add("ClinicNum");
			table.Columns.Add("HasMobileLayout");
			table.Columns.Add("SheetDefNum");
			foreach(WebForms_Sheet webForms_Sheet in listWebForms_Sheets) {
				table.Rows.Add(new object[] {
					POut.Long  (webForms_Sheet.SheetID),
					POut.Long  (webForms_Sheet.DentalOfficeID),
					            webForms_Sheet.Description,
					POut.Int   ((int)webForms_Sheet.SheetType),
					POut.DateT (webForms_Sheet.DateTimeSheet,false),
					POut.Float (webForms_Sheet.FontSize),
					            webForms_Sheet.FontName,
					POut.Int   (webForms_Sheet.Width),
					POut.Int   (webForms_Sheet.Height),
					POut.Bool  (webForms_Sheet.IsLandscape),
					POut.Long  (webForms_Sheet.ClinicNum),
					POut.Bool  (webForms_Sheet.HasMobileLayout),
					POut.Long  (webForms_Sheet.SheetDefNum),
				});
			}
			return table;
		}

		///<summary>Converts a DataTable to a list of objects.</summary>
		public static List<WebForms_Sheet> TableToList(DataTable table) {
			List<WebForms_Sheet> retVal=new List<WebForms_Sheet>();
			WebForms_Sheet webForms_Sheet;
			for(int i=0;i<table.Rows.Count;i++) {
				webForms_Sheet=new WebForms_Sheet();
				webForms_Sheet.SheetID        = PIn.Long  (table.Rows[i]["SheetID"].ToString());
				webForms_Sheet.DentalOfficeID = PIn.Long  (table.Rows[i]["DentalOfficeID"].ToString());
				webForms_Sheet.Description    = PIn.String(table.Rows[i]["Description"].ToString());
				webForms_Sheet.SheetType      = (OpenDentBusiness.SheetTypeEnum)PIn.Int(table.Rows[i]["SheetType"].ToString());
				webForms_Sheet.DateTimeSheet  = PIn.DateT (table.Rows[i]["DateTimeSheet"].ToString());
				webForms_Sheet.FontSize       = PIn.Float (table.Rows[i]["FontSize"].ToString());
				webForms_Sheet.FontName       = PIn.String(table.Rows[i]["FontName"].ToString());
				webForms_Sheet.Width          = PIn.Int   (table.Rows[i]["Width"].ToString());
				webForms_Sheet.Height         = PIn.Int   (table.Rows[i]["Height"].ToString());
				webForms_Sheet.IsLandscape    = PIn.Bool  (table.Rows[i]["IsLandscape"].ToString());
				webForms_Sheet.ClinicNum      = PIn.Long  (table.Rows[i]["ClinicNum"].ToString());
				webForms_Sheet.HasMobileLayout= PIn.Bool  (table.Rows[i]["HasMobileLayout"].ToString());
				webForms_Sheet.SheetDefNum    = PIn.Long  (table.Rows[i]["SheetDefNum"].ToString());
				retVal.Add(webForms_Sheet);
			}
			return retVal;
		}

		///<summary>Inserts one WebForms_Sheet into the database.  Returns the new priKey.</summary>
		public static long Insert(WebForms_Sheet webForms_Sheet) {
			return Insert(webForms_Sheet,false);
		}

		///<summary>Inserts one WebForms_Sheet into the database.  Provides option to use the existing priKey.</summary>
		public static long Insert(WebForms_Sheet webForms_Sheet,bool useExistingPK) {
			string command="INSERT INTO webforms_sheet (";
			if(useExistingPK) {
				command+="SheetID,";
			}
			command+="DentalOfficeID,Description,SheetType,DateTimeSheet,FontSize,FontName,Width,Height,IsLandscape,ClinicNum,HasMobileLayout,SheetDefNum) VALUES(";
			if(useExistingPK) {
				command+=POut.Long(webForms_Sheet.SheetID)+",";
			}
			command+=
				     POut.Long  (webForms_Sheet.DentalOfficeID)+","
				+"'"+POut.String(webForms_Sheet.Description)+"',"
				+    POut.Int   ((int)webForms_Sheet.SheetType)+","
				+    POut.DateT (webForms_Sheet.DateTimeSheet)+","
				+    POut.Float (webForms_Sheet.FontSize)+","
				+"'"+POut.String(webForms_Sheet.FontName)+"',"
				+    POut.Int   (webForms_Sheet.Width)+","
				+    POut.Int   (webForms_Sheet.Height)+","
				+    POut.Bool  (webForms_Sheet.IsLandscape)+","
				+    POut.Long  (webForms_Sheet.ClinicNum)+","
				+    POut.Bool  (webForms_Sheet.HasMobileLayout)+","
				+    POut.Long  (webForms_Sheet.SheetDefNum)+")";
			if(useExistingPK) {
				Db.NonQ(command);
			}
			else {
				webForms_Sheet.SheetID=Db.NonQ(command,true,"SheetID","webForms_Sheet");
			}
			return webForms_Sheet.SheetID;
		}

		///<summary>Inserts one WebForms_Sheet into the database.  Returns the new priKey.  Doesn't use the cache.</summary>
		public static long InsertNoCache(WebForms_Sheet webForms_Sheet) {
			return InsertNoCache(webForms_Sheet,false);
		}

		///<summary>Inserts one WebForms_Sheet into the database.  Provides option to use the existing priKey.  Doesn't use the cache.</summary>
		public static long InsertNoCache(WebForms_Sheet webForms_Sheet,bool useExistingPK) {
			string command="INSERT INTO webforms_sheet (";
			if(useExistingPK) {
				command+="SheetID,";
			}
			command+="DentalOfficeID,Description,SheetType,DateTimeSheet,FontSize,FontName,Width,Height,IsLandscape,ClinicNum,HasMobileLayout,SheetDefNum) VALUES(";
			if(useExistingPK) {
				command+=POut.Long(webForms_Sheet.SheetID)+",";
			}
			command+=
				     POut.Long  (webForms_Sheet.DentalOfficeID)+","
				+"'"+POut.String(webForms_Sheet.Description)+"',"
				+    POut.Int   ((int)webForms_Sheet.SheetType)+","
				+    POut.DateT (webForms_Sheet.DateTimeSheet)+","
				+    POut.Float (webForms_Sheet.FontSize)+","
				+"'"+POut.String(webForms_Sheet.FontName)+"',"
				+    POut.Int   (webForms_Sheet.Width)+","
				+    POut.Int   (webForms_Sheet.Height)+","
				+    POut.Bool  (webForms_Sheet.IsLandscape)+","
				+    POut.Long  (webForms_Sheet.ClinicNum)+","
				+    POut.Bool  (webForms_Sheet.HasMobileLayout)+","
				+    POut.Long  (webForms_Sheet.SheetDefNum)+")";
			if(useExistingPK) {
				Db.NonQ(command);
			}
			else {
				webForms_Sheet.SheetID=Db.NonQ(command,true,"SheetID","webForms_Sheet");
			}
			return webForms_Sheet.SheetID;
		}

		///<summary>Updates one WebForms_Sheet in the database.</summary>
		public static void Update(WebForms_Sheet webForms_Sheet) {
			string command="UPDATE webforms_sheet SET "
				+"DentalOfficeID =  "+POut.Long  (webForms_Sheet.DentalOfficeID)+", "
				+"Description    = '"+POut.String(webForms_Sheet.Description)+"', "
				+"SheetType      =  "+POut.Int   ((int)webForms_Sheet.SheetType)+", "
				+"DateTimeSheet  =  "+POut.DateT (webForms_Sheet.DateTimeSheet)+", "
				+"FontSize       =  "+POut.Float (webForms_Sheet.FontSize)+", "
				+"FontName       = '"+POut.String(webForms_Sheet.FontName)+"', "
				+"Width          =  "+POut.Int   (webForms_Sheet.Width)+", "
				+"Height         =  "+POut.Int   (webForms_Sheet.Height)+", "
				+"IsLandscape    =  "+POut.Bool  (webForms_Sheet.IsLandscape)+", "
				+"ClinicNum      =  "+POut.Long  (webForms_Sheet.ClinicNum)+", "
				+"HasMobileLayout=  "+POut.Bool  (webForms_Sheet.HasMobileLayout)+", "
				+"SheetDefNum    =  "+POut.Long  (webForms_Sheet.SheetDefNum)+" "
				+"WHERE SheetID = "+POut.Long(webForms_Sheet.SheetID);
			Db.NonQ(command);
		}

		///<summary>Updates one WebForms_Sheet in the database.  Uses an old object to compare to, and only alters changed fields.  This prevents collisions and concurrency problems in heavily used tables.  Returns true if an update occurred.</summary>
		public static bool Update(WebForms_Sheet webForms_Sheet,WebForms_Sheet oldWebForms_Sheet) {
			string command="";
			if(webForms_Sheet.DentalOfficeID != oldWebForms_Sheet.DentalOfficeID) {
				if(command!="") { command+=",";}
				command+="DentalOfficeID = "+POut.Long(webForms_Sheet.DentalOfficeID)+"";
			}
			if(webForms_Sheet.Description != oldWebForms_Sheet.Description) {
				if(command!="") { command+=",";}
				command+="Description = '"+POut.String(webForms_Sheet.Description)+"'";
			}
			if(webForms_Sheet.SheetType != oldWebForms_Sheet.SheetType) {
				if(command!="") { command+=",";}
				command+="SheetType = "+POut.Int   ((int)webForms_Sheet.SheetType)+"";
			}
			if(webForms_Sheet.DateTimeSheet != oldWebForms_Sheet.DateTimeSheet) {
				if(command!="") { command+=",";}
				command+="DateTimeSheet = "+POut.DateT(webForms_Sheet.DateTimeSheet)+"";
			}
			if(webForms_Sheet.FontSize != oldWebForms_Sheet.FontSize) {
				if(command!="") { command+=",";}
				command+="FontSize = "+POut.Float(webForms_Sheet.FontSize)+"";
			}
			if(webForms_Sheet.FontName != oldWebForms_Sheet.FontName) {
				if(command!="") { command+=",";}
				command+="FontName = '"+POut.String(webForms_Sheet.FontName)+"'";
			}
			if(webForms_Sheet.Width != oldWebForms_Sheet.Width) {
				if(command!="") { command+=",";}
				command+="Width = "+POut.Int(webForms_Sheet.Width)+"";
			}
			if(webForms_Sheet.Height != oldWebForms_Sheet.Height) {
				if(command!="") { command+=",";}
				command+="Height = "+POut.Int(webForms_Sheet.Height)+"";
			}
			if(webForms_Sheet.IsLandscape != oldWebForms_Sheet.IsLandscape) {
				if(command!="") { command+=",";}
				command+="IsLandscape = "+POut.Bool(webForms_Sheet.IsLandscape)+"";
			}
			if(webForms_Sheet.ClinicNum != oldWebForms_Sheet.ClinicNum) {
				if(command!="") { command+=",";}
				command+="ClinicNum = "+POut.Long(webForms_Sheet.ClinicNum)+"";
			}
			if(webForms_Sheet.HasMobileLayout != oldWebForms_Sheet.HasMobileLayout) {
				if(command!="") { command+=",";}
				command+="HasMobileLayout = "+POut.Bool(webForms_Sheet.HasMobileLayout)+"";
			}
			if(webForms_Sheet.SheetDefNum != oldWebForms_Sheet.SheetDefNum) {
				if(command!="") { command+=",";}
				command+="SheetDefNum = "+POut.Long(webForms_Sheet.SheetDefNum)+"";
			}
			if(command=="") {
				return false;
			}
			command="UPDATE webforms_sheet SET "+command
				+" WHERE SheetID = "+POut.Long(webForms_Sheet.SheetID);
			Db.NonQ(command);
			return true;
		}

		///<summary>Returns true if Update(WebForms_Sheet,WebForms_Sheet) would make changes to the database.
		///Does not make any changes to the database and can be called before remoting role is checked.</summary>
		public static bool UpdateComparison(WebForms_Sheet webForms_Sheet,WebForms_Sheet oldWebForms_Sheet) {
			if(webForms_Sheet.DentalOfficeID != oldWebForms_Sheet.DentalOfficeID) {
				return true;
			}
			if(webForms_Sheet.Description != oldWebForms_Sheet.Description) {
				return true;
			}
			if(webForms_Sheet.SheetType != oldWebForms_Sheet.SheetType) {
				return true;
			}
			if(webForms_Sheet.DateTimeSheet != oldWebForms_Sheet.DateTimeSheet) {
				return true;
			}
			if(webForms_Sheet.FontSize != oldWebForms_Sheet.FontSize) {
				return true;
			}
			if(webForms_Sheet.FontName != oldWebForms_Sheet.FontName) {
				return true;
			}
			if(webForms_Sheet.Width != oldWebForms_Sheet.Width) {
				return true;
			}
			if(webForms_Sheet.Height != oldWebForms_Sheet.Height) {
				return true;
			}
			if(webForms_Sheet.IsLandscape != oldWebForms_Sheet.IsLandscape) {
				return true;
			}
			if(webForms_Sheet.ClinicNum != oldWebForms_Sheet.ClinicNum) {
				return true;
			}
			if(webForms_Sheet.HasMobileLayout != oldWebForms_Sheet.HasMobileLayout) {
				return true;
			}
			if(webForms_Sheet.SheetDefNum != oldWebForms_Sheet.SheetDefNum) {
				return true;
			}
			return false;
		}

		///<summary>Deletes one WebForms_Sheet from the database.</summary>
		public static void Delete(long sheetID) {
			string command="DELETE FROM webforms_sheet "
				+"WHERE SheetID = "+POut.Long(sheetID);
			Db.NonQ(command);
		}

	}
}