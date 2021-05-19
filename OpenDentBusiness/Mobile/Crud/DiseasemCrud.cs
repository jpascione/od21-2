//This file is automatically generated.
//Do not attempt to make changes to this file because the changes will be erased and overwritten.
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Drawing;

namespace OpenDentBusiness.Mobile.Crud{
	internal class DiseasemCrud {
		///<summary>Gets one Diseasem object from the database using primaryKey1(CustomerNum) and primaryKey2.  Returns null if not found.</summary>
		internal static Diseasem SelectOne(long customerNum,long diseaseNum){
			string command="SELECT * FROM diseasem "
				+"WHERE CustomerNum = "+POut.Long(customerNum)+" AND DiseaseNum = "+POut.Long(diseaseNum);
			List<Diseasem> list=TableToList(Db.GetTable(command));
			if(list.Count==0) {
				return null;
			}
			return list[0];
		}

		///<summary>Gets one Diseasem object from the database using a query.</summary>
		internal static Diseasem SelectOne(string command){
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				throw new ApplicationException("Not allowed to send sql directly.  Rewrite the calling class to not use this query:\r\n"+command);
			}
			List<Diseasem> list=TableToList(Db.GetTable(command));
			if(list.Count==0) {
				return null;
			}
			return list[0];
		}

		///<summary>Gets a list of Diseasem objects from the database using a query.</summary>
		internal static List<Diseasem> SelectMany(string command){
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				throw new ApplicationException("Not allowed to send sql directly.  Rewrite the calling class to not use this query:\r\n"+command);
			}
			List<Diseasem> list=TableToList(Db.GetTable(command));
			return list;
		}

		///<summary>Converts a DataTable to a list of objects.</summary>
		internal static List<Diseasem> TableToList(DataTable table){
			List<Diseasem> retVal=new List<Diseasem>();
			Diseasem diseasem;
			for(int i=0;i<table.Rows.Count;i++) {
				diseasem=new Diseasem();
				diseasem.CustomerNum  = PIn.Long  (table.Rows[i]["CustomerNum"].ToString());
				diseasem.DiseaseNum   = PIn.Long  (table.Rows[i]["DiseaseNum"].ToString());
				diseasem.PatNum       = PIn.Long  (table.Rows[i]["PatNum"].ToString());
				diseasem.DiseaseDefNum= PIn.Long  (table.Rows[i]["DiseaseDefNum"].ToString());
				diseasem.PatNote      = PIn.String(table.Rows[i]["PatNote"].ToString());
				diseasem.ICD9Num      = PIn.Long  (table.Rows[i]["ICD9Num"].ToString());
				diseasem.ProbStatus   = (ProblemStatus)PIn.Int(table.Rows[i]["ProbStatus"].ToString());
				diseasem.DateStart    = PIn.Date  (table.Rows[i]["DateStart"].ToString());
				diseasem.DateStop     = PIn.Date  (table.Rows[i]["DateStop"].ToString());
				retVal.Add(diseasem);
			}
			return retVal;
		}

		///<summary>Usually set useExistingPK=true.  Inserts one Diseasem into the database.</summary>
		internal static long Insert(Diseasem diseasem,bool useExistingPK){
			if(!useExistingPK) {
				diseasem.DiseaseNum=ReplicationServers.GetKey("diseasem","DiseaseNum");
			}
			string command="INSERT INTO diseasem (";
			command+="DiseaseNum,";
			command+="CustomerNum,PatNum,DiseaseDefNum,PatNote,ICD9Num,ProbStatus,DateStart,DateStop) VALUES(";
			command+=POut.Long(diseasem.DiseaseNum)+",";
			command+=
				     POut.Long  (diseasem.CustomerNum)+","
				+    POut.Long  (diseasem.PatNum)+","
				+    POut.Long  (diseasem.DiseaseDefNum)+","
				+"'"+POut.String(diseasem.PatNote)+"',"
				+    POut.Long  (diseasem.ICD9Num)+","
				+    POut.Int   ((int)diseasem.ProbStatus)+","
				+    POut.Date  (diseasem.DateStart)+","
				+    POut.Date  (diseasem.DateStop)+")";
			Db.NonQ(command);//There is no autoincrement in the mobile server.
			return diseasem.DiseaseNum;
		}

		///<summary>Updates one Diseasem in the database.</summary>
		internal static void Update(Diseasem diseasem){
			string command="UPDATE diseasem SET "
				+"PatNum       =  "+POut.Long  (diseasem.PatNum)+", "
				+"DiseaseDefNum=  "+POut.Long  (diseasem.DiseaseDefNum)+", "
				+"PatNote      = '"+POut.String(diseasem.PatNote)+"', "
				+"ICD9Num      =  "+POut.Long  (diseasem.ICD9Num)+", "
				+"ProbStatus   =  "+POut.Int   ((int)diseasem.ProbStatus)+", "
				+"DateStart    =  "+POut.Date  (diseasem.DateStart)+", "
				+"DateStop     =  "+POut.Date  (diseasem.DateStop)+" "
				+"WHERE CustomerNum = "+POut.Long(diseasem.CustomerNum)+" AND DiseaseNum = "+POut.Long(diseasem.DiseaseNum);
			Db.NonQ(command);
		}

		///<summary>Deletes one Diseasem from the database.</summary>
		internal static void Delete(long customerNum,long diseaseNum){
			string command="DELETE FROM diseasem "
				+"WHERE CustomerNum = "+POut.Long(customerNum)+" AND DiseaseNum = "+POut.Long(diseaseNum);
			Db.NonQ(command);
		}

		///<summary>Converts one Disease object to its mobile equivalent.  Warning! CustomerNum will always be 0.</summary>
		internal static Diseasem ConvertToM(Disease disease){
			Diseasem diseasem=new Diseasem();
			//CustomerNum cannot be set.  Remains 0.
			diseasem.DiseaseNum   =disease.DiseaseNum;
			diseasem.PatNum       =disease.PatNum;
			diseasem.DiseaseDefNum=disease.DiseaseDefNum;
			diseasem.PatNote      =disease.PatNote;
			diseasem.ICD9Num      =ICD9s.GetByCode(DiseaseDefs.GetItem(disease.DiseaseDefNum).ICD9Code).ICD9Num;
			diseasem.ProbStatus   =disease.ProbStatus;
			diseasem.DateStart    =disease.DateStart;
			diseasem.DateStop     =disease.DateStop;
			return diseasem;
		}

	}
}