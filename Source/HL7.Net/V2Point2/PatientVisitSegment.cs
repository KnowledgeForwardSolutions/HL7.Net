namespace HL7.Net.V2Point2;

/// <summary>
///   The PV1 segment is used by Registration/ADT applications to communicate 
///   information on a visit specific basis. This segment can be used to send 
///   multiple visit statistic records to the same patient account, or single 
///   visit records to more than one account. Individual sites must determine 
///   this segment's use.
/// </summary>
public class PatientVisitSegment : ISegment
{
   internal PatientVisitSegment() { }

   private static readonly List<FieldSpecification> _fieldSpecifications = new()
   {
      new FieldSpecification(SegmentIDs.PatientVisitSegment, 1, "Set ID - Patient Visit", 4, HL7Datatype.SI_SequenceID, Optionality.Optional, "N"),
      new FieldSpecification(SegmentIDs.PatientVisitSegment, 2, "Patient Class", 1, HL7Datatype.ID_CodedValue, Optionality.Required, "N"),
      new FieldSpecification(SegmentIDs.PatientVisitSegment, 3, "Assigned Patient Location", 12, HL7Datatype.CM_Composite, Optionality.Optional, "N"),
      new FieldSpecification(SegmentIDs.PatientVisitSegment, 4, "Admission Type", 2, HL7Datatype.ID_CodedValue, Optionality.Optional, "N"),
      new FieldSpecification(SegmentIDs.PatientVisitSegment, 5, "Preadmit Number", 20, HL7Datatype.ST_String, Optionality.Optional, "N"),
      new FieldSpecification(SegmentIDs.PatientVisitSegment, 6, "Prior Patient Location", 12, HL7Datatype.CM_Composite, Optionality.Optional, "N"),
      new FieldSpecification(SegmentIDs.PatientVisitSegment, 7, "Attending Doctor", 60, HL7Datatype.CN_CompositeIDNumberAndName, Optionality.Optional, "N"),
      new FieldSpecification(SegmentIDs.PatientVisitSegment, 8, "Referring Doctor", 60, HL7Datatype.CN_CompositeIDNumberAndName, Optionality.Optional, "N"),
      new FieldSpecification(SegmentIDs.PatientVisitSegment, 9, "Consulting Doctor", 60, HL7Datatype.CN_CompositeIDNumberAndName, Optionality.Optional, "3"),
      new FieldSpecification(SegmentIDs.PatientVisitSegment, 10, "Hospital Service", 3, HL7Datatype.ID_CodedValue, Optionality.Optional, "N"),
      new FieldSpecification(SegmentIDs.PatientVisitSegment, 11, "Temporary Location", 12, HL7Datatype.CM_Composite, Optionality.Optional, "N"),
      new FieldSpecification(SegmentIDs.PatientVisitSegment, 12, "Preadmit Test Indicator", 2, HL7Datatype.ID_CodedValue, Optionality.Optional, "N"),
      new FieldSpecification(SegmentIDs.PatientVisitSegment, 13, "Readmission Indicator", 2, HL7Datatype.ID_CodedValue, Optionality.Optional, "N"),
      new FieldSpecification(SegmentIDs.PatientVisitSegment, 14, "Admit Source", 2, HL7Datatype.ID_CodedValue, Optionality.Optional, "N"),
      new FieldSpecification(SegmentIDs.PatientVisitSegment, 15, "Ambulatory Status", 2, HL7Datatype.ID_CodedValue, Optionality.Optional, "Y"),
      new FieldSpecification(SegmentIDs.PatientVisitSegment, 16, "VIP Indicator", 2, HL7Datatype.ID_CodedValue, Optionality.Optional, "N"),
      new FieldSpecification(SegmentIDs.PatientVisitSegment, 17, "Admitting Doctor", 60, HL7Datatype.CN_CompositeIDNumberAndName, Optionality.Optional, "N"),
      new FieldSpecification(SegmentIDs.PatientVisitSegment, 18, "Patient Type", 2, HL7Datatype.ID_CodedValue, Optionality.Optional, "N"),
      new FieldSpecification(SegmentIDs.PatientVisitSegment, 19, "Visit Number", 15, HL7Datatype.NM_Numeric, Optionality.Optional, "N"),
      new FieldSpecification(SegmentIDs.PatientVisitSegment, 20, "Financial Class", 50, HL7Datatype.CM_Composite, Optionality.Optional, "4"),
      new FieldSpecification(SegmentIDs.PatientVisitSegment, 21, "Charge Price Indicator", 2, HL7Datatype.ID_CodedValue, Optionality.Optional, "N"),
      new FieldSpecification(SegmentIDs.PatientVisitSegment, 22, "Courtesy Code", 2, HL7Datatype.ID_CodedValue, Optionality.Optional, "N"),
      new FieldSpecification(SegmentIDs.PatientVisitSegment, 23, "Credit Rating", 2, HL7Datatype.ID_CodedValue, Optionality.Optional, "N"),
      new FieldSpecification(SegmentIDs.PatientVisitSegment, 24, "Contract Code", 2, HL7Datatype.ID_CodedValue, Optionality.Optional, "Y"),
      new FieldSpecification(SegmentIDs.PatientVisitSegment, 25, "Contract Effective Date", 8, HL7Datatype.DT_Date, Optionality.Optional, "Y"),
      new FieldSpecification(SegmentIDs.PatientVisitSegment, 26, "Contract Amount", 12, HL7Datatype.NM_Numeric, Optionality.Optional, "Y"),
      new FieldSpecification(SegmentIDs.PatientVisitSegment, 27, "Contract Period", 3, HL7Datatype.NM_Numeric, Optionality.Optional, "Y"),
      new FieldSpecification(SegmentIDs.PatientVisitSegment, 28, "Interest Code", 2, HL7Datatype.ID_CodedValue, Optionality.Optional, "N"),
      new FieldSpecification(SegmentIDs.PatientVisitSegment, 29, "Transfer To Bad Debt Code", 1, HL7Datatype.ID_CodedValue, Optionality.Optional, "N"),
      new FieldSpecification(SegmentIDs.PatientVisitSegment, 30, "Transfer To Bad Debt Date", 8, HL7Datatype.DT_Date, Optionality.Optional, "N"),
      new FieldSpecification(SegmentIDs.PatientVisitSegment, 31, "Bad Debt Agency Code", 10, HL7Datatype.ID_CodedValue, Optionality.Optional, "N"),
      new FieldSpecification(SegmentIDs.PatientVisitSegment, 32, "Bad Debt Transfer Amount", 12, HL7Datatype.NM_Numeric, Optionality.Optional, "N"),
      new FieldSpecification(SegmentIDs.PatientVisitSegment, 33, "Bad Debt Recovery Amount", 12, HL7Datatype.NM_Numeric, Optionality.Optional, "N"),
      new FieldSpecification(SegmentIDs.PatientVisitSegment, 34, "Delete Account Indicator", 1, HL7Datatype.ID_CodedValue, Optionality.Optional, "N"),
      new FieldSpecification(SegmentIDs.PatientVisitSegment, 35, "Delete Account Date", 8, HL7Datatype.DT_Date, Optionality.Optional, "N"),
      new FieldSpecification(SegmentIDs.PatientVisitSegment, 36, "Discharge Disposition", 3, HL7Datatype.ID_CodedValue, Optionality.Optional, "N"),
      new FieldSpecification(SegmentIDs.PatientVisitSegment, 37, "Discharged To Location", 25, HL7Datatype.CM_Composite, Optionality.Optional, "N"),
      new FieldSpecification(SegmentIDs.PatientVisitSegment, 38, "Diet Type", 2, HL7Datatype.ID_CodedValue, Optionality.Optional, "N"),
      new FieldSpecification(SegmentIDs.PatientVisitSegment, 39, "Servicing Facility", 2, HL7Datatype.ID_CodedValue, Optionality.Optional, "N"),
      new FieldSpecification(SegmentIDs.PatientVisitSegment, 40, "Bed Status", 1, HL7Datatype.ID_CodedValue, Optionality.Optional, "N"),
      new FieldSpecification(SegmentIDs.PatientVisitSegment, 41, "Account Status", 2, HL7Datatype.ID_CodedValue, Optionality.Optional, "N"),
      new FieldSpecification(SegmentIDs.PatientVisitSegment, 42, "Pending Location", 12, HL7Datatype.CM_Composite, Optionality.Optional, "N"),
      new FieldSpecification(SegmentIDs.PatientVisitSegment, 43, "Prior Temporary Location", 12, HL7Datatype.CM_Composite, Optionality.Optional, "N"),
      new FieldSpecification(SegmentIDs.PatientVisitSegment, 44, "Admit Date/Time", 26, HL7Datatype.TS_Timestamp, Optionality.Optional, "N"),
      new FieldSpecification(SegmentIDs.PatientVisitSegment, 45, "Discharge Date/Time", 26, HL7Datatype.TS_Timestamp, Optionality.Optional, "N"),
      new FieldSpecification(SegmentIDs.PatientVisitSegment, 46, "Current Patient Balance", 12, HL7Datatype.NM_Numeric, Optionality.Optional, "N"),
      new FieldSpecification(SegmentIDs.PatientVisitSegment, 47, "Total Charges", 12, HL7Datatype.NM_Numeric, Optionality.Optional, "N"),
      new FieldSpecification(SegmentIDs.PatientVisitSegment, 48, "Total Adjustments", 12, HL7Datatype.NM_Numeric, Optionality.Optional, "N"),
      new FieldSpecification(SegmentIDs.PatientVisitSegment, 49, "Total Payments", 12, HL7Datatype.NM_Numeric, Optionality.Optional, "N"),
      new FieldSpecification(SegmentIDs.PatientVisitSegment, 50, "Alternate Visit ID", 20, HL7Datatype.CM_Composite, Optionality.Optional, "N"),
   };

   /// <inheritdoc/>
   public String SegmentID => SegmentIDs.PatientVisitSegment;

   /// <summary>
   ///   Number that uniquely identifies this transaction for the purpose of 
   ///   adding, changing, or deleting the transaction. For those messages that 
   ///   permit segments to repeat, the Set ID field is used to identify the
   ///   repetitions.
   /// </summary>
   public SequenceIDField SetID { get; private set; } = default!;

   /// <summary>
   ///   A common field used by systems to categorize patients by site.
   /// </summary>
   public StringField PatientClass { get; private set; } = default!;

   /// <summary>
   ///   New location is the patient's initial assigned location, or the
   ///   location to which he is being moved.
   /// </summary>
   public StringField AssignedPatientLocation { get; private set; } = default!;

   /// <summary>
   ///   Indicates the circumstance under which the patient was or will be 
   ///   admitted.
   /// </summary>
   public StringField AdmissionType { get; private set; } = default!;

   /// <summary>
   ///   Uniquely identifies the patient's pre-admit account.
   /// </summary>
   public StringField PreadmitNumber { get; private set; } = default!;

   /// <summary>
   ///   Old location is null if the patient is new. It contains the prior 
   ///   patient location if the patient is being transferred.
   /// </summary>
   public StringField PriorPatientLocation { get; private set; } = default!;

   /// <summary>
   ///   Identifies the attending physician.
   /// </summary>
   public StringField AttendingDoctor { get; private set; } = default!;

   /// <summary>
   ///   Identifies the referring physician.
   /// </summary>
   public StringField ReferringDoctor { get; private set; } = default!;

   /// <summary>
   ///   Identifies the consulting physician.
   /// </summary>
   public StringField ConsultingDoctor { get; private set; } = default!;

   /// <summary>
   ///   The treatment or type of surgery the patient is scheduled to receive.
   /// </summary>
   public StringField HospitalService { get; private set; } = default!;

   /// <summary>
   ///   Location other than the assigned location required for a temporary 
   ///   period of time (e.g., OR).
   /// </summary>
   public StringField TemporaryLocation { get; private set; } = default!;

   /// <summary>
   ///   Indicates that the patient must have pre-admission testing done in 
   ///   order to be admitted.
   /// </summary>
   public StringField PreadmitTestIndicator { get; private set; } = default!;

   /// <summary>
   ///   Indicates that a patient is being re-admitted to the facility and the 
   ///   circumstances.
   /// </summary>
   public StringField ReadmissionIndicator { get; private set; } = default!;

   /// <summary>
   ///   Indicates where the patient was admitted.
   /// </summary>
   public StringField AdmitSource { get; private set; } = default!;

   /// <summary>
   ///   Factors that limit patient's mobility.
   /// </summary>
   public StringField AmbulatoryStatus { get; private set; } = default!;

   /// <summary>
   ///   Identifies the type of VIP.
   /// </summary>
   public StringField VipIndicator { get; private set; } = default!;

   /// <summary>
   ///   Identifies the admitting physician.
   /// </summary>
   public StringField AdmittingDoctor { get; private set; } = default!;

   /// <summary>
   ///   Site specific patient type identifier.
   /// </summary>
   public StringField PatientType { get; private set; } = default!;

   /// <summary>
   ///   Unique number assigned to each patient visit.
   /// </summary>
   public StringField VisitNumber { get; private set; } = default!;

   /// <summary>
   ///   Primary financial class assigned to the patient for the purpose of 
   ///   identifying sources of reimbursement. 
   /// </summary>
   public StringField FinancialClass { get; private set; } = default!;

   /// <summary>
   ///   Code used to determine which price schedule is to be used for room and 
   ///   bed charges.
   /// </summary>
   public StringField ChargePriceIndicator { get; private set; } = default!;

   /// <summary>
   ///   Code that indicates whether the patient will be extended certain 
   ///   special courtesies.
   /// </summary>
   public StringField CourtseyCode { get; private set; } = default!;

   /// <summary>
   ///   User-defined code to determine past credit experience.
   /// </summary>
   public StringField CreditRating { get; private set; } = default!;

   /// <summary>
   ///   Identifies the type of contract entered into by the facility and the 
   ///   guarantor for the purpose of settling outstanding account balances.
   /// </summary>
   public StringField ContractCode { get; private set; } = default!;

   /// <summary>
   ///   Date the contract is to start.
   /// </summary>
   public StringField ContractEffectiveDate { get; private set; } = default!;

   /// <summary>
   ///   Amount to be paid by the guarantor each period as per the contract.
   /// </summary>
   public NumericField ContractAmount { get; private set; } = default!;

   /// <summary>
   ///   Specifies the duration of the contract for user-defined periods.
   /// </summary>
   public NumericField ContractPeriod { get; private set; } = default!;

   /// <summary>
   ///   Indicates the amount of interest that will be charged the guarantor on 
   ///   any outstanding amounts.
   /// </summary>
   public StringField InterestCode { get; private set; } = default!;

   /// <summary>
   ///   Indicates the account was transferred to bad debts and the reason.
   /// </summary>
   public StringField TransferToBadDebtCode { get; private set; } = default!;

   /// <summary>
   ///   Date that the account was transferred to a bad debt status.
   /// </summary>
   public StringField TransferToBadDebtDate { get; private set; } = default!;

   /// <summary>
   ///   Uniquely identifies the bad debt agency that the account was 
   ///   transferred to.
   /// </summary>
   public StringField BadDebtAgencyCode { get; private set; } = default!;

   /// <summary>
   ///   Amount that was transferred to a bad debt status.
   /// </summary>
   public NumericField BadDebtTransferAmount { get; private set; } = default!;

   /// <summary>
   ///   Amount recovered from the guarantor on the account.
   /// </summary>
   public NumericField BadDebtRecoveryAmount { get; private set; } = default!;

   /// <summary>
   ///   Indicates that the account was deleted from the file and the reason.
   /// </summary>
   public StringField DeleteAccountIndicator { get; private set; } = default!;

   /// <summary>
   ///   Date that the account was deleted from the file.
   /// </summary>
   public StringField DeleteAccountDate { get; private set; } = default!;

   /// <summary>
   ///   Disposition of the patient at time of discharge (i.e., discharged to 
   ///   home; expired; etc.).
   /// </summary>
   public StringField DischargeDisposition { get; private set; } = default!;

   /// <summary>
   ///   Indicates a facility to which the patient was discharged.
   /// </summary>
   public StringField DischargedToLocation { get; private set; } = default!;

   /// <summary>
   ///   Indicates a special diet type for a patient.
   /// </summary>
   public StringField DietType { get; private set; } = default!;

   /// <summary>
   ///   Used in a multiple facility environment to indicate the facility with 
   ///   which this visit is associated.
   /// </summary>
   public StringField ServingFacility { get; private set; } = default!;

   /// <summary>
   ///   User defined value.
   /// </summary>
   public StringField BedStatus { get; private set; } = default!;

   /// <summary>
   ///   User defined value.
   /// </summary>
   public StringField AccountStatus { get; private set; } = default!;

   /// <summary>
   ///   Indicates the nursing station, room, bed, facility ID and bed status to 
   ///   which the patient may be moved. 
   /// </summary>
   public StringField PendingLocation { get; private set; } = default!;

   /// <summary>
   ///   Can be used when a patient is arriving or departing or for general 
   ///   update events. 
   /// </summary>
   public StringField PriorTemporaryLocation { get; private set; } = default!;

   /// <summary>
   ///   Admit date/time. To be used if the event date/time is different than 
   ///   the admit date and time, i.e., a retroactive update.
   /// </summary>
   public StringField AdmitDateTime { get; private set; } = default!;

   /// <summary>
   ///   Discharge date/time. To be used if the event date/time is different 
   ///   than the discharge date and time, i.e., a retroactive update.
   /// </summary>
   public StringField DischargeDateTime { get; private set; } = default!;

   /// <summary>
   ///   Visit balance due. 
   /// </summary>
   public NumericField CurrentPatientBalance { get; private set; } = default!;

   /// <summary>
   ///   Total visit charges.
   /// </summary>
   public NumericField TotalCharges { get; private set; } = default!;

   /// <summary>
   ///   Total adjustments for visit.
   /// </summary>
   public NumericField TotalAdjustments { get; private set; } = default!;

   /// <summary>
   ///   Total payments for visit.
   /// </summary>
   public NumericField TotalPayments { get; private set; } = default!;

   /// <summary>
   ///   Optional visit ID number to be used if needed
   /// </summary>
   public StringField AlternateVisitID { get; private set; } = default!;

   internal static PatientVisitSegment Parse(
      ReadOnlySpan<Char> segmentText,
      EncodingDetails encodingDetails,
      Int32 lineNumber,
      ProcessingLog log)
   {
      var segment = new PatientVisitSegment();

      // Enumerate the fields and skip over the first field which contains the
      // segment ID.
      var fieldEnumerator = segmentText.ToFields(
         encodingDetails.FieldSeparator, 
         encodingDetails.EscapeCharacter);
      fieldEnumerator.MoveNext();

      segment.SetID = SequenceIDField.Parse(
         ref fieldEnumerator,
         _fieldSpecifications[0],
         lineNumber,
         log);

      segment.PatientClass = StringField.Parse(
         ref fieldEnumerator,
         encodingDetails,
         _fieldSpecifications[1],
         lineNumber,
         log);

      segment.AssignedPatientLocation = StringField.Parse(
         ref fieldEnumerator,
         encodingDetails,
         _fieldSpecifications[2],
         lineNumber,
         log);

      segment.AdmissionType = StringField.Parse(
         ref fieldEnumerator,
         encodingDetails,
         _fieldSpecifications[3],
         lineNumber,
         log);

      segment.PreadmitNumber = StringField.Parse(
         ref fieldEnumerator,
         encodingDetails,
         _fieldSpecifications[4],
         lineNumber,
         log);

      segment.PriorPatientLocation = StringField.Parse(
         ref fieldEnumerator,
         encodingDetails,
         _fieldSpecifications[5],
         lineNumber,
         log);

      segment.AttendingDoctor = StringField.Parse(
         ref fieldEnumerator,
         encodingDetails,
         _fieldSpecifications[6],
         lineNumber,
         log);

      segment.ReferringDoctor = StringField.Parse(
         ref fieldEnumerator,
         encodingDetails,
         _fieldSpecifications[7],
         lineNumber,
         log);

      segment.ConsultingDoctor = StringField.Parse(
         ref fieldEnumerator,
         encodingDetails,
         _fieldSpecifications[8],
         lineNumber,
         log);

      segment.HospitalService = StringField.Parse(
         ref fieldEnumerator,
         encodingDetails,
         _fieldSpecifications[9],
         lineNumber,
         log);

      segment.TemporaryLocation = StringField.Parse(
         ref fieldEnumerator,
         encodingDetails,
         _fieldSpecifications[10],
         lineNumber,
         log);

      segment.PreadmitTestIndicator = StringField.Parse(
         ref fieldEnumerator,
         encodingDetails,
         _fieldSpecifications[11],
         lineNumber,
         log);

      segment.ReadmissionIndicator = StringField.Parse(
         ref fieldEnumerator,
         encodingDetails,
         _fieldSpecifications[12],
         lineNumber,
         log);

      segment.AdmitSource = StringField.Parse(
         ref fieldEnumerator,
         encodingDetails,
         _fieldSpecifications[13],
         lineNumber,
         log);

      segment.AmbulatoryStatus = StringField.Parse(
         ref fieldEnumerator,
         encodingDetails,
         _fieldSpecifications[14],
         lineNumber,
         log);

      segment.VipIndicator = StringField.Parse(
         ref fieldEnumerator,
         encodingDetails,
         _fieldSpecifications[15],
         lineNumber,
         log);

      segment.AdmittingDoctor = StringField.Parse(
         ref fieldEnumerator,
         encodingDetails,
         _fieldSpecifications[16],
         lineNumber,
         log);

      segment.PatientType = StringField.Parse(
         ref fieldEnumerator,
         encodingDetails,
         _fieldSpecifications[17],
         lineNumber,
         log);

      segment.VisitNumber = StringField.Parse(
         ref fieldEnumerator,
         encodingDetails,
         _fieldSpecifications[18],
         lineNumber,
         log);

      segment.FinancialClass = StringField.Parse(
         ref fieldEnumerator,
         encodingDetails,
         _fieldSpecifications[19],
         lineNumber,
         log);

      segment.ChargePriceIndicator = StringField.Parse(
         ref fieldEnumerator,
         encodingDetails,
         _fieldSpecifications[20],
         lineNumber,
         log);

      segment.CourtseyCode = StringField.Parse(
         ref fieldEnumerator,
         encodingDetails,
         _fieldSpecifications[21],
         lineNumber,
         log);

      segment.CreditRating = StringField.Parse(
         ref fieldEnumerator,
         encodingDetails,
         _fieldSpecifications[22],
         lineNumber,
         log);

      segment.ContractCode = StringField.Parse(
         ref fieldEnumerator,
         encodingDetails,
         _fieldSpecifications[23],
         lineNumber,
         log);

      segment.ContractEffectiveDate = StringField.Parse(
         ref fieldEnumerator,
         encodingDetails,
         _fieldSpecifications[24],
         lineNumber,
         log);

      segment.ContractAmount = NumericField.Parse(
         ref fieldEnumerator,
         _fieldSpecifications[25],
         lineNumber,
         log);

      segment.ContractPeriod = NumericField.Parse(
         ref fieldEnumerator,
         _fieldSpecifications[26],
         lineNumber,
         log);

      segment.InterestCode = StringField.Parse(
         ref fieldEnumerator,
         encodingDetails,
         _fieldSpecifications[27],
         lineNumber,
         log);

      segment.TransferToBadDebtCode = StringField.Parse(
         ref fieldEnumerator,
         encodingDetails,
         _fieldSpecifications[28],
         lineNumber,
         log);

      segment.TransferToBadDebtDate = StringField.Parse(
         ref fieldEnumerator,
         encodingDetails,
         _fieldSpecifications[29],
         lineNumber,
         log);

      segment.BadDebtAgencyCode = StringField.Parse(
         ref fieldEnumerator,
         encodingDetails,
         _fieldSpecifications[30],
         lineNumber,
         log);

      segment.BadDebtTransferAmount = NumericField.Parse(
         ref fieldEnumerator,
         _fieldSpecifications[31],
         lineNumber,
         log);

      segment.BadDebtRecoveryAmount = NumericField.Parse(
         ref fieldEnumerator,
         _fieldSpecifications[32],
         lineNumber,
         log);

      segment.DeleteAccountIndicator = StringField.Parse(
         ref fieldEnumerator,
         encodingDetails,
         _fieldSpecifications[33],
         lineNumber,
         log);

      segment.DeleteAccountDate = StringField.Parse(
         ref fieldEnumerator,
         encodingDetails,
         _fieldSpecifications[34],
         lineNumber,
         log);

      segment.DischargeDisposition = StringField.Parse(
         ref fieldEnumerator,
         encodingDetails,
         _fieldSpecifications[35],
         lineNumber,
         log);

      segment.DischargedToLocation = StringField.Parse(
         ref fieldEnumerator,
         encodingDetails,
         _fieldSpecifications[36],
         lineNumber,
         log);

      segment.DietType = StringField.Parse(
         ref fieldEnumerator,
         encodingDetails,
         _fieldSpecifications[37],
         lineNumber,
         log);

      segment.ServingFacility = StringField.Parse(
         ref fieldEnumerator,
         encodingDetails,
         _fieldSpecifications[38],
         lineNumber,
         log);

      segment.BedStatus = StringField.Parse(
         ref fieldEnumerator,
         encodingDetails,
         _fieldSpecifications[39],
         lineNumber,
         log);

      segment.AccountStatus = StringField.Parse(
         ref fieldEnumerator,
         encodingDetails,
         _fieldSpecifications[40],
         lineNumber,
         log);

      segment.PendingLocation = StringField.Parse(
         ref fieldEnumerator,
         encodingDetails,
         _fieldSpecifications[41],
         lineNumber,
         log);

      segment.PriorTemporaryLocation = StringField.Parse(
         ref fieldEnumerator,
         encodingDetails,
         _fieldSpecifications[42],
         lineNumber,
         log);

      segment.AdmitDateTime = StringField.Parse(
         ref fieldEnumerator,
         encodingDetails,
         _fieldSpecifications[43],
         lineNumber,
         log);

      segment.DischargeDateTime = StringField.Parse(
         ref fieldEnumerator,
         encodingDetails,
         _fieldSpecifications[44],
         lineNumber,
         log);

      segment.CurrentPatientBalance = NumericField.Parse(
         ref fieldEnumerator,
         _fieldSpecifications[45],
         lineNumber,
         log);

      segment.TotalCharges = NumericField.Parse(
         ref fieldEnumerator,
         _fieldSpecifications[46],
         lineNumber,
         log);

      segment.TotalAdjustments = NumericField.Parse(
         ref fieldEnumerator,
         _fieldSpecifications[47],
         lineNumber,
         log);

      segment.TotalPayments = NumericField.Parse(
         ref fieldEnumerator,
         _fieldSpecifications[48],
         lineNumber,
         log);

      segment.AlternateVisitID = StringField.Parse(
         ref fieldEnumerator,
         encodingDetails,
         _fieldSpecifications[49],
         lineNumber,
         log);

      return segment;
   }
}
