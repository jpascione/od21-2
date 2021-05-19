﻿using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OpenDentBusiness.AutoComm {
	public class TagReplacer {
			
		///<summary>Replaces any tags in the AutoCommObj.</summary>
		protected virtual void ReplaceTagsChild(StringBuilder retVal,AutoCommObj autoCommObj,bool isEmail) { }
		///<summary>Replaces any aggregate tags.</summary>
		protected virtual void ReplaceTagsAggregateChild(StringBuilder sbTemplate,StringBuilder sbAutoCommObjsAggregate) { }
		///<summary>Replaces one individual tag. Case insensitive.</summary>
		protected void ReplaceOneTag(StringBuilder template,string tagToReplace,string replaceValue,bool isEmailBody) {
			OpenDentBusiness.ReplaceTags.ReplaceOneTag(template,tagToReplace,replaceValue,isEmailBody);
		}

		///<summary>Replaces all tags with appropriate values if possible.</summary>
		public string ReplaceTags(string template,AutoCommObj autoCommObj,Clinic clinic,bool isEmailBody,bool isSecure=false) {
			if(string.IsNullOrEmpty(template)) {
				return template;
			}
			if(isSecure) {
				return template;//mass emails will call a different method to handle replacements when sending. 
			}
			StringBuilder retVal = new StringBuilder();
			retVal.Append(template);
			ReplaceOneTag(retVal,"[Namef]",autoCommObj.NameF,isEmailBody);//Might be preferred name.
			ReplaceOneTag(retVal,"[ClinicName]",clinic.Description,isEmailBody);
			ReplaceOneTag(retVal,"[ClinicPhone]",TelephoneNumbers.ReFormat(clinic.Phone.ToString()),isEmailBody);
			ReplaceOneTag(retVal,"[OfficePhone]",TelephoneNumbers.ReFormat(OpenDentBusiness.Clinics.GetOfficePhone(clinic)),isEmailBody);
			ReplaceOneTag(retVal,"[OfficeName]",OpenDentBusiness.Clinics.GetOfficeName(clinic),isEmailBody);
			ReplaceOneTag(retVal,"[PracticeName]",PrefC.GetString(PrefName.PracticeTitle),isEmailBody);
			ReplaceOneTag(retVal,"[PracticePhone]",TelephoneNumbers.ReFormat(PrefC.GetString(PrefName.PracticePhone)),isEmailBody);
			ReplaceOneTag(retVal,"[ProvName]",OpenDentBusiness.Providers.GetFormalName(autoCommObj.ProvNum),isEmailBody);
			ReplaceOneTag(retVal,"[ProvAbbr]",OpenDentBusiness.Providers.GetAbbr(autoCommObj.ProvNum),isEmailBody);
			ReplaceOneTag(retVal,"[EmailDisclaimer]",OpenDentBusiness.EmailMessages.GetEmailDisclaimer(clinic.ClinicNum),isEmailBody);
			ReplaceTagsChild(retVal,autoCommObj,isEmailBody);
			return retVal.ToString();
		}

		///<summary>Groups the AutoCommObjs by patient when replacing tags.</summary>
		public string ReplaceTagsAggregate(string templateAll,string templateSingle,Clinic clinicCur,List<AutoCommObj> listAutoCommObjs,
			AutoCommObj primaryAutoCommObj,bool isEmailBody)
		{
			if(string.IsNullOrEmpty(templateAll)) {
				return templateAll;
			}
			StringBuilder sbAgg=new StringBuilder();
			//If there are multiple appointments for the same patient in this group, we are going to use just the first appointment to fill out the template.
			foreach(AutoCommObj autoCommObj in listAutoCommObjs.GroupBy(x => x.PatNum).Select(x => x.OrderBy(y => y.DateTimeEvent).First())) {
				StringBuilder sbOneAutoCommObj=new StringBuilder();
				sbOneAutoCommObj.Append(ReplaceTags(templateSingle,autoCommObj,clinicCur,isEmailBody));
				sbAgg.AppendLine(sbOneAutoCommObj.ToString());
			}
			StringBuilder retVal=new StringBuilder();
			retVal.Append(ReplaceTags(templateAll,primaryAutoCommObj,clinicCur,isEmailBody));
			ReplaceTagsAggregateChild(retVal,sbAgg);
			return retVal.ToString();
		}
	}
}
