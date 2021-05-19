//This file is automatically generated.
//Do not attempt to make changes to this file because the changes will be erased and overwritten.
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;

namespace OpenDentBusiness.Crud{
	public class TaskNoteCrud {
		///<summary>Gets one TaskNote object from the database using the primary key.  Returns null if not found.</summary>
		public static TaskNote SelectOne(long taskNoteNum) {
			string command="SELECT * FROM tasknote "
				+"WHERE TaskNoteNum = "+POut.Long(taskNoteNum);
			List<TaskNote> list=TableToList(Db.GetTable(command));
			if(list.Count==0) {
				return null;
			}
			return list[0];
		}

		///<summary>Gets one TaskNote object from the database using a query.</summary>
		public static TaskNote SelectOne(string command) {
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				throw new ApplicationException("Not allowed to send sql directly.  Rewrite the calling class to not use this query:\r\n"+command);
			}
			List<TaskNote> list=TableToList(Db.GetTable(command));
			if(list.Count==0) {
				return null;
			}
			return list[0];
		}

		///<summary>Gets a list of TaskNote objects from the database using a query.</summary>
		public static List<TaskNote> SelectMany(string command) {
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				throw new ApplicationException("Not allowed to send sql directly.  Rewrite the calling class to not use this query:\r\n"+command);
			}
			List<TaskNote> list=TableToList(Db.GetTable(command));
			return list;
		}

		///<summary>Converts a DataTable to a list of objects.</summary>
		public static List<TaskNote> TableToList(DataTable table) {
			List<TaskNote> retVal=new List<TaskNote>();
			TaskNote taskNote;
			foreach(DataRow row in table.Rows) {
				taskNote=new TaskNote();
				taskNote.TaskNoteNum = PIn.Long  (row["TaskNoteNum"].ToString());
				taskNote.TaskNum     = PIn.Long  (row["TaskNum"].ToString());
				taskNote.UserNum     = PIn.Long  (row["UserNum"].ToString());
				taskNote.DateTimeNote= PIn.DateT (row["DateTimeNote"].ToString());
				taskNote.Note        = PIn.String(row["Note"].ToString());
				retVal.Add(taskNote);
			}
			return retVal;
		}

		///<summary>Converts a list of TaskNote into a DataTable.</summary>
		public static DataTable ListToTable(List<TaskNote> listTaskNotes,string tableName="") {
			if(string.IsNullOrEmpty(tableName)) {
				tableName="TaskNote";
			}
			DataTable table=new DataTable(tableName);
			table.Columns.Add("TaskNoteNum");
			table.Columns.Add("TaskNum");
			table.Columns.Add("UserNum");
			table.Columns.Add("DateTimeNote");
			table.Columns.Add("Note");
			foreach(TaskNote taskNote in listTaskNotes) {
				table.Rows.Add(new object[] {
					POut.Long  (taskNote.TaskNoteNum),
					POut.Long  (taskNote.TaskNum),
					POut.Long  (taskNote.UserNum),
					POut.DateT (taskNote.DateTimeNote,false),
					            taskNote.Note,
				});
			}
			return table;
		}

		///<summary>Inserts one TaskNote into the database.  Returns the new priKey.</summary>
		public static long Insert(TaskNote taskNote) {
			return Insert(taskNote,false);
		}

		///<summary>Inserts one TaskNote into the database.  Provides option to use the existing priKey.</summary>
		public static long Insert(TaskNote taskNote,bool useExistingPK) {
			if(!useExistingPK && PrefC.RandomKeys) {
				taskNote.TaskNoteNum=ReplicationServers.GetKey("tasknote","TaskNoteNum");
			}
			string command="INSERT INTO tasknote (";
			if(useExistingPK || PrefC.RandomKeys) {
				command+="TaskNoteNum,";
			}
			command+="TaskNum,UserNum,DateTimeNote,Note) VALUES(";
			if(useExistingPK || PrefC.RandomKeys) {
				command+=POut.Long(taskNote.TaskNoteNum)+",";
			}
			command+=
				     POut.Long  (taskNote.TaskNum)+","
				+    POut.Long  (taskNote.UserNum)+","
				+    DbHelper.Now()+","
				+    DbHelper.ParamChar+"paramNote)";
			if(taskNote.Note==null) {
				taskNote.Note="";
			}
			OdSqlParameter paramNote=new OdSqlParameter("paramNote",OdDbType.Text,POut.StringParam(taskNote.Note));
			if(useExistingPK || PrefC.RandomKeys) {
				Db.NonQ(command,paramNote);
			}
			else {
				taskNote.TaskNoteNum=Db.NonQ(command,true,"TaskNoteNum","taskNote",paramNote);
			}
			return taskNote.TaskNoteNum;
		}

		///<summary>Inserts one TaskNote into the database.  Returns the new priKey.  Doesn't use the cache.</summary>
		public static long InsertNoCache(TaskNote taskNote) {
			return InsertNoCache(taskNote,false);
		}

		///<summary>Inserts one TaskNote into the database.  Provides option to use the existing priKey.  Doesn't use the cache.</summary>
		public static long InsertNoCache(TaskNote taskNote,bool useExistingPK) {
			bool isRandomKeys=Prefs.GetBoolNoCache(PrefName.RandomPrimaryKeys);
			string command="INSERT INTO tasknote (";
			if(!useExistingPK && isRandomKeys) {
				taskNote.TaskNoteNum=ReplicationServers.GetKeyNoCache("tasknote","TaskNoteNum");
			}
			if(isRandomKeys || useExistingPK) {
				command+="TaskNoteNum,";
			}
			command+="TaskNum,UserNum,DateTimeNote,Note) VALUES(";
			if(isRandomKeys || useExistingPK) {
				command+=POut.Long(taskNote.TaskNoteNum)+",";
			}
			command+=
				     POut.Long  (taskNote.TaskNum)+","
				+    POut.Long  (taskNote.UserNum)+","
				+    DbHelper.Now()+","
				+    DbHelper.ParamChar+"paramNote)";
			if(taskNote.Note==null) {
				taskNote.Note="";
			}
			OdSqlParameter paramNote=new OdSqlParameter("paramNote",OdDbType.Text,POut.StringParam(taskNote.Note));
			if(useExistingPK || isRandomKeys) {
				Db.NonQ(command,paramNote);
			}
			else {
				taskNote.TaskNoteNum=Db.NonQ(command,true,"TaskNoteNum","taskNote",paramNote);
			}
			return taskNote.TaskNoteNum;
		}

		///<summary>Updates one TaskNote in the database.</summary>
		public static void Update(TaskNote taskNote) {
			string command="UPDATE tasknote SET "
				+"TaskNum     =  "+POut.Long  (taskNote.TaskNum)+", "
				+"UserNum     =  "+POut.Long  (taskNote.UserNum)+", "
				+"DateTimeNote=  "+POut.DateT (taskNote.DateTimeNote)+", "
				+"Note        =  "+DbHelper.ParamChar+"paramNote "
				+"WHERE TaskNoteNum = "+POut.Long(taskNote.TaskNoteNum);
			if(taskNote.Note==null) {
				taskNote.Note="";
			}
			OdSqlParameter paramNote=new OdSqlParameter("paramNote",OdDbType.Text,POut.StringParam(taskNote.Note));
			Db.NonQ(command,paramNote);
		}

		///<summary>Updates one TaskNote in the database.  Uses an old object to compare to, and only alters changed fields.  This prevents collisions and concurrency problems in heavily used tables.  Returns true if an update occurred.</summary>
		public static bool Update(TaskNote taskNote,TaskNote oldTaskNote) {
			string command="";
			if(taskNote.TaskNum != oldTaskNote.TaskNum) {
				if(command!="") { command+=",";}
				command+="TaskNum = "+POut.Long(taskNote.TaskNum)+"";
			}
			if(taskNote.UserNum != oldTaskNote.UserNum) {
				if(command!="") { command+=",";}
				command+="UserNum = "+POut.Long(taskNote.UserNum)+"";
			}
			if(taskNote.DateTimeNote != oldTaskNote.DateTimeNote) {
				if(command!="") { command+=",";}
				command+="DateTimeNote = "+POut.DateT(taskNote.DateTimeNote)+"";
			}
			if(taskNote.Note != oldTaskNote.Note) {
				if(command!="") { command+=",";}
				command+="Note = "+DbHelper.ParamChar+"paramNote";
			}
			if(command=="") {
				return false;
			}
			if(taskNote.Note==null) {
				taskNote.Note="";
			}
			OdSqlParameter paramNote=new OdSqlParameter("paramNote",OdDbType.Text,POut.StringParam(taskNote.Note));
			command="UPDATE tasknote SET "+command
				+" WHERE TaskNoteNum = "+POut.Long(taskNote.TaskNoteNum);
			Db.NonQ(command,paramNote);
			return true;
		}

		///<summary>Returns true if Update(TaskNote,TaskNote) would make changes to the database.
		///Does not make any changes to the database and can be called before remoting role is checked.</summary>
		public static bool UpdateComparison(TaskNote taskNote,TaskNote oldTaskNote) {
			if(taskNote.TaskNum != oldTaskNote.TaskNum) {
				return true;
			}
			if(taskNote.UserNum != oldTaskNote.UserNum) {
				return true;
			}
			if(taskNote.DateTimeNote != oldTaskNote.DateTimeNote) {
				return true;
			}
			if(taskNote.Note != oldTaskNote.Note) {
				return true;
			}
			return false;
		}

		///<summary>Deletes one TaskNote from the database.</summary>
		public static void Delete(long taskNoteNum) {
			string command="DELETE FROM tasknote "
				+"WHERE TaskNoteNum = "+POut.Long(taskNoteNum);
			Db.NonQ(command);
		}

		///<summary>Deletes many TaskNotes from the database.</summary>
		public static void DeleteMany(List<long> listTaskNoteNums) {
			if(listTaskNoteNums==null || listTaskNoteNums.Count==0) {
				return;
			}
			string command="DELETE FROM tasknote "
				+"WHERE TaskNoteNum IN("+string.Join(",",listTaskNoteNums.Select(x => POut.Long(x)))+")";
			Db.NonQ(command);
		}

	}
}