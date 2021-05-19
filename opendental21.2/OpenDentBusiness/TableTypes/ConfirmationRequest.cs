﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Xml.Serialization;

namespace OpenDentBusiness {
	///<summary>Requests that have been sent via EConnector to HQ. HQ will process and update status as responses become available.</summary>
	[Serializable,CrudTable(HasBatchWriteMethods=true)]
	public class ConfirmationRequest : TableBase {
		///<summary>PK. Generated by HQ.</summary>
		[CrudColumn(IsPriKey=true)]
		public long ConfirmationRequestNum;
		//OD does not have RegKeyNum field. This is only kept by HQ in ConfirmationPending table.

		///<summary>FK to clinic.ClinicNum. Generated by OD.</summary>
		public long ClinicNum;
		///<summary>Generated by OD. If true then generate and send MT text message. 
		///If false then OD proper is probably panning on emailing the message/GUID so just enter into ConfirmPending and return.</summary>
		public bool IsForSms;
		///<summary>Generated by OD. If true then generate and send email.</summary>
		public bool IsForEmail;
		///<summary>FK to patient.PatNum. Generated by OD. Patient that this request is linked to. No corresponding field in HQ ConfirmationPending.</summary>
		public long PatNum;
		///<summary>FK to appointment.AptNum. Generated by OD. Will be sent back linked to corresponding newly created ConfirmPending. Allows OD proper to link the newly created ConfirmPending to the input ConfirmationRequest and set the short GUID on the appointment.</summary>
		public long ApptNum;
		///<summary>Generated by OD. Only allowed to be empty is IsForSms==true. In that case then it is assumed that OD proper will probably be emailing confirmation and does not want a text message sent.</summary>
		public string PhonePat;
		///<summary>Generated by OD. Typically the time of the appointment. This is the time at which HQ will consider this unconfirmed and auto terminate.</summary>
		[CrudColumn(SpecialType=CrudSpecialColType.DateT)]
		public DateTime DateTimeConfirmExpire;
		///<summary>Generated by OD. HQ and OD proper are in different timezones. This is used to tell HQ how far in advance to set the DateTimeExpire when entering the row into HQ db. This is a work-around for timezone offset.</summary>
		public int SecondsFromEntryToExpire;
		///<summary>Generated by HQ. Identifies this ConfirmationRequest in future transactions between HQ and OD. Will be used for sms confirmations only.</summary>
		public string ShortGUID;
		///<summary>Generated by HQ. The code that the patient will text back in order to confirm the appointment. If received then it indicates a positive response.</summary>
		public string ConfirmCode;
		///<summary>Generated by OD. Includes [ConfirmCode] replacement tag and (optionally) [URL] replacement tag. OD proper can construct this to be any length.
		///Will be converted to final MsgText and sent to patient once tags are replaced with real values.</summary>
		[CrudColumn(SpecialType=CrudSpecialColType.TextIsClob)]
		public string MsgTextToMobileTemplate;
		///<summary>Generated by HQ. Applied real text to tags from MsgTextTemplate.</summary>
		[CrudColumn(SpecialType=CrudSpecialColType.TextIsClob)]
		public string MsgTextToMobile;
		///<summary></summary>
		[CrudColumn(SpecialType = CrudSpecialColType.TextIsClob)]
		public string EmailSubjTemplate;
		///<summary>.</summary>
		[CrudColumn(SpecialType = CrudSpecialColType.TextIsClob)]
		public string EmailSubj;
		///<summary>Generated by OD. Includes [ConfirmCode] replacement tag and (optionally) [URL] replacement tag. OD proper can construct this to be any length.
		///Will be converted to final EmailText and emailed to patient once tags are replaced with real values.</summary>
		[CrudColumn(SpecialType = CrudSpecialColType.TextIsClob)]
		public string EmailTextTemplate;
		///<summary>Generated by HQ. Applied real text to tags from EmailTextTemplate.</summary>
		[CrudColumn(SpecialType = CrudSpecialColType.TextIsClob)]
		public string EmailText;
		///<summary>Generated by OD. Timestamp when row is created.</summary>
		[CrudColumn(SpecialType=CrudSpecialColType.DateTEntry)]
		public DateTime DateTimeEntry;
		///<summary>Generated by OD. Timestamp when EConnector sent this confirm request to HQ. Stored in local customer timezone.</summary>
		[CrudColumn(SpecialType=CrudSpecialColType.DateT)]
		public DateTime DateTimeConfirmTransmit;
		///<summary>Generated by OD. Timestamp when HQ updates this request to indicate that it has been terminated. RSVPStatusCode will change to its final state at this time. Stored in local customer timezone.</summary>
		[CrudColumn(SpecialType=CrudSpecialColType.DateT)]
		public DateTime DateTimeRSVP;
		///<summary>Enum:RSVPStatusCodes Generated by OD in some cases and HQ in others. Indicates current status in the lifecycle of this ConfirmationRequest.</summary>
		public RSVPStatusCodes RSVPStatus;
		///<summary>Generated by OD in some cases and HQ in others. Any human readable error message generated by either HQ or EConnector. Used for debugging.</summary>
		[CrudColumn(SpecialType=CrudSpecialColType.TextIsClob)]
		public string ResponseDescript;
		///<summary>FK to smstomobile.GuidMessage. Generated at HQ when the confirmation is generated.</summary>
		[CrudColumn(SpecialType=CrudSpecialColType.TextIsClob)]
		public string GuidMessageToMobile;
		///<summary>FK to smsfrommobile.GuidMessage. Generated at HQ when the confirmation pending is terminated with confirmation text message.
		///Also allows SmsFromMobile to be linked to ConfirmationRequest in OD proper.</summary>
		[CrudColumn(SpecialType=CrudSpecialColType.TextIsClob)]
		public string GuidMessageFromMobile;
		///<summary>Generated by HQ. Identifies this ConfirmationRequest in future transactions between HQ and OD. Will be used for email confirmations only.</summary>
		public string ShortGuidEmail;
		/// <summary>The datetime of the appointment when this ConfirmationRequest was created.</summary>
		[CrudColumn(SpecialType=CrudSpecialColType.DateT)]
		public DateTime AptDateTimeOrig;
		///<summary>The TSPrior of the rule that was used to create this ConfirmationRequest.</summary>
		[XmlIgnore]
		[CrudColumn(SpecialType = CrudSpecialColType.TimeSpanLong)]
		public TimeSpan TSPrior;
		///<summary>Generated by HQ. Indicates whether or not we were able to send the text message.</summary>
		public bool SmsSentOk;
		///<summary>Generated by HQ. Indicates whether or not we were able to send the e-mail.</summary>
		public bool EmailSentOk;
		///<summary>Indicates whether the user has chosen to not resend the confirmation request when the AptDateTime has changed.</summary>
		public bool DoNotResend;
		///<summary>FK to ApptReminderRule.ApptReminderRuleNum.</summary>
		public long ApptReminderRuleNum;

		///<summary>Used only for serialization purposes</summary>
		[XmlElement("TSPrior",typeof(long))]
		public long TSPriorXml {
			get {
				return TSPrior.Ticks;
			}
			set {
				TSPrior=TimeSpan.FromTicks(value);
			}
		}

		public ConfirmationRequest Copy() {
			return (ConfirmationRequest)MemberwiseClone();
		}

	}

	public enum RSVPStatusCodes {
		///<summary>Entered manually by something other than EConnector. EConnector will pickup and send to HQ and change to pendingRsvp.</summary>
		[Description("Awaiting Transmit")]
		AwaitingTransmit,
		///<summary>EConnector has sent this to HQ and will remain in this status until it is either terminated or receives a response from the patient.</summary>
		[Description("Pending Rsvp")]
		PendingRsvp,
		///<summary>Patient responded with an affirmative confirmation.</summary>
		[Description("Positive Rsvp")]
		PositiveRsvp,
		///<summary>Patient responded and declined the confirmation.</summary>
		[Description("Negative Rsvp")]
		NegativeRsvp,
		///<summary>Patient responded by requesting a callback.</summary>
		[Description("Callback")]
		Callback,
		///<summary>Patient took no action by the time DateTimeExpired passed and the confirmation was terminated.</summary>
		[Description("Expired")]
		Expired,
		///<summary>HQ or EConnector was unable to create the confirmation so it was terminated prematurely.</summary>
		[Description("Failed")]
		Failed,
		///<summary> 7 - The appointment date/time was changed before the patient responded to the original confirmation.
		///OD proper will simply delete these ConfirmationRequests. HQ will move them to the terminated table and mark them ApptChanged.</summary>
		[Description("ApptChanged")]
		ApptChanged,
	}

}
