namespace HL7.Net.V2Point2;

/// <summary>
///   The PID segment is used by all applications as the primary means of 
///   communicating patient identification information. This segment contains 
///   permanent patient identifying, and demographic information that, for the
///   most part, is not likely to change frequently.
/// </summary>
public class PatientIdentificationSegment : ISegment
{
   internal PatientIdentificationSegment() { }

   private static readonly List<FieldSpecification> _fieldSpecifications = new()
   {
      new FieldSpecification(SegmentIDs.PatientIdentificationSegment, 1, "Set ID - Patient ID", 3, HL7Datatype.SI_SequenceID, Optionality.Optional, "N"),
      new FieldSpecification(SegmentIDs.PatientIdentificationSegment, 2, "Patient ID (External ID)", 16, HL7Datatype.CK_CompositeIDWithCheckDigit, Optionality.Optional, "N"),
      new FieldSpecification(SegmentIDs.PatientIdentificationSegment, 3, "Patient ID (Internal ID)", 20, HL7Datatype.CM_Composite, Optionality.Required, "Y"),
      new FieldSpecification(SegmentIDs.PatientIdentificationSegment, 4, "Alternate Patient ID", 12, HL7Datatype.ST_String, Optionality.Optional, "N"),
      new FieldSpecification(SegmentIDs.PatientIdentificationSegment, 5, "Patient Name", 48, HL7Datatype.PN_PersonName, Optionality.Required, "N"),
      new FieldSpecification(SegmentIDs.PatientIdentificationSegment, 6, "Mother's Maiden Name", 30, HL7Datatype.ST_String, Optionality.Optional, "N"),
      new FieldSpecification(SegmentIDs.PatientIdentificationSegment, 7, "Date of Birth", 26, HL7Datatype.TS_Timestamp, Optionality.Optional, "N"),
      new FieldSpecification(SegmentIDs.PatientIdentificationSegment, 8, "Sex", 1, HL7Datatype.ID_CodedValue, Optionality.Optional, "N"),
      new FieldSpecification(SegmentIDs.PatientIdentificationSegment, 9, "Patient Alias", 48, HL7Datatype.PN_PersonName, Optionality.Optional, "Y"),
      new FieldSpecification(SegmentIDs.PatientIdentificationSegment, 10, "Race", 1, HL7Datatype.ID_CodedValue, Optionality.Optional, "N"),
      new FieldSpecification(SegmentIDs.PatientIdentificationSegment, 11, "Patient Address", 106, HL7Datatype.AD_Address, Optionality.Optional, "3"),
      new FieldSpecification(SegmentIDs.PatientIdentificationSegment, 12, "Country Code", 4, HL7Datatype.ID_CodedValue, Optionality.Optional, "N"),
      new FieldSpecification(SegmentIDs.PatientIdentificationSegment, 13, "Phone Number - Home", 40, HL7Datatype.TN_TelephoneNumber, Optionality.Optional, "3"),
      new FieldSpecification(SegmentIDs.PatientIdentificationSegment, 14, "Phone Number - Business", 40, HL7Datatype.TN_TelephoneNumber, Optionality.Optional, "3"),
      new FieldSpecification(SegmentIDs.PatientIdentificationSegment, 15, "Language - Patient", 25, HL7Datatype.ST_String, Optionality.Optional, "N"),
      new FieldSpecification(SegmentIDs.PatientIdentificationSegment, 16, "Marital Status", 1, HL7Datatype.ID_CodedValue, Optionality.Optional, "N"),
      new FieldSpecification(SegmentIDs.PatientIdentificationSegment, 17, "Religion", 3, HL7Datatype.ID_CodedValue, Optionality.Optional, "N"),
      new FieldSpecification(SegmentIDs.PatientIdentificationSegment, 18, "Patient Account Number", 20, HL7Datatype.CK_CompositeIDWithCheckDigit, Optionality.Optional, "N"),
      new FieldSpecification(SegmentIDs.PatientIdentificationSegment, 19, "SSN Number - Patient", 16, HL7Datatype.ST_String, Optionality.Optional, "N"),
      new FieldSpecification(SegmentIDs.PatientIdentificationSegment, 20, "Driver's Lic Num - Patient", 25, HL7Datatype.CM_Composite, Optionality.Optional, "N"),
      new FieldSpecification(SegmentIDs.PatientIdentificationSegment, 21, "Mother's Identifier", 20, HL7Datatype.CK_CompositeIDWithCheckDigit, Optionality.Optional, "N"),
      new FieldSpecification(SegmentIDs.PatientIdentificationSegment, 22, "Ethnic Group", 1, HL7Datatype.ID_CodedValue, Optionality.Optional, "N"),
      new FieldSpecification(SegmentIDs.PatientIdentificationSegment, 23, "Birth Place", 25, HL7Datatype.ST_String, Optionality.Optional, "N"),
      new FieldSpecification(SegmentIDs.PatientIdentificationSegment, 24, "Multiple Birth Indicator", 2, HL7Datatype.ID_CodedValue, Optionality.Optional, "N"),
      new FieldSpecification(SegmentIDs.PatientIdentificationSegment, 25, "Birth Order", 2, HL7Datatype.NM_Numeric, Optionality.Optional, "N"),
      new FieldSpecification(SegmentIDs.PatientIdentificationSegment, 26, "Citizenship", 3, HL7Datatype.ID_CodedValue, Optionality.Optional, "N"),
      new FieldSpecification(SegmentIDs.PatientIdentificationSegment, 27, "Veterans Military Status", 60, HL7Datatype.CE_CodedElement, Optionality.Optional, "N"),
   };

   /// <inheritdoc/>
   public String SegmentID => SegmentIDs.PatientIdentificationSegment;

   /// <summary>
   ///   For those messages that permit segments to repeat, the Set ID field is 
   ///   used to identify the repetitions.
   /// </summary>
   public StringField SetID { get; private set; } = default!;

   /// <summary>
   ///   If the patient is from another institution, outside office, etc., the 
   ///   identifier used by that institution can be shown here.
   /// </summary>
   public StringField ExternalPatientID { get; private set; } = default!;

   /// <summary>
   ///   If the patient is from another institution, outside office, etc., the 
   ///   identifier used by that institution can be shown here.
   /// </summary>
   public StringField InternalPatientId { get; private set; } = default!;

   /// <summary>
   ///   Third number that may be required to identify a patient.
   /// </summary>
   public StringField AlternatePatientID { get; private set; } = default!;

   /// <summary>
   ///   Patient name.
   /// </summary>
   public StringField PatientName { get; private set; } = default!;

   /// <summary>
   ///   Family name under which the mother was born (i.e., before marriage.) 
   ///   Used to disambiguate patients with the same last name.
   /// </summary>
   public StringField MothersMaidenName { get; private set; } = default!;

   /// <summary>
   ///   Patient's date of birth.
   /// </summary>
   public StringField DateOfBirth { get; private set; } = default!;

   /// <summary>
   ///   Patient's sex.
   /// </summary>
   public StringField Sex { get; private set; } = default!;

   /// <summary>
   ///   Name(s) by which the patient has been known at some time.
   /// </summary>
   public StringField PatientAlias { get; private set; } = default!;

   /// <summary>
   ///   Patient ethnic classification.
   /// </summary>
   public StringField Race { get; private set; } = default!;

   /// <summary>
   ///   Mailing address of the patient.
   /// </summary>
   public StringField PatientAddress { get; private set; } = default!;

   /// <summary>
   ///   Patient's country code.
   /// </summary>
   public StringField CountryCode { get; private set; } = default!;

   /// <summary>
   ///   Patient's home telephone number.
   /// </summary>
   public StringField HomePhoneNumber { get; private set; } = default!;

   /// <summary>
   ///   Patient's business telephone number.
   /// </summary>
   public StringField BusinessPhoneNumber { get; private set; } = default!;

   /// <summary>
   ///   Patient's primary language.
   /// </summary>
   public StringField PatientLanguage { get; private set; } = default!;

   /// <summary>
   ///   Patient's marital status.
   /// </summary>
   public StringField MaritalStatus { get; private set; } = default!;

   /// <summary>
   ///   Patient's religion.
   /// </summary>
   public StringField Religion { get; private set; } = default!;

   /// <summary>
   ///   Number assigned by accounting to which all charges, payments, etc. are 
   ///   recorded. It is used to identify the patient's account. 
   /// </summary>
   public StringField PatientAccountNumber { get; private set; } = default!;

   /// <summary>
   ///   Patient's social security number. This number may also be an RR 
   ///   retirement number. 
   /// </summary>
   public StringField PatientSSN { get; private set; } = default!;

   /// <summary>
   ///   Patient's drivers license number. 
   /// </summary>
   public StringField PatientDriversLicenseNumber { get; private set; } = default!;

   /// <summary>
   ///   Used as a link field for newborns.
   /// </summary>
   public StringField MothersIdentifier { get; private set; } = default!;

   /// <summary>
   ///   Further defines patient ancestry.
   /// </summary>
   public StringField EthnicGroup { get; private set; } = default!;

   /// <summary>
   ///   Indicates the location of the patient's birth.
   /// </summary>
   public StringField BirthPlace { get; private set; } = default!;

   /// <summary>
   ///   Indicates if the patient was part of a multiple birth.
   /// </summary>
   public StringField MultipleBirthIndicator { get; private set; } = default!;

   /// <summary>
   ///   If a patient was part of a multiple birth, a value (number) indicating 
   ///   the patient's birth order.
   /// </summary>
   public StringField BirthOrder { get; private set; } = default!;

   /// <summary>
   ///   Indicates the patient's country of citizenship.
   /// </summary>
   public StringField Citizenship { get; private set; } = default!;

   /// <summary>
   ///   Indicates the military status assigned to a veteran.
   /// </summary>
   public StringField VeteransMilitaryStatus { get; private set; } = default!;

   internal static PatientIdentificationSegment Parse(
      ReadOnlySpan<Char> segmentText,
      EncodingDetails encodingDetails,
      Int32 lineNumber,
      ProcessingLog log)
   {
      var segment = new PatientIdentificationSegment();

      // Enumerate the fields and skip over the first field which contains the
      // segment ID.
      var fieldEnumerator = segmentText.ToFields(
         encodingDetails.FieldSeparator,
         encodingDetails.EscapeCharacter);
      fieldEnumerator.MoveNext();

      segment.SetID = StringField.Parse(
         ref fieldEnumerator,
         encodingDetails,
         _fieldSpecifications[0],
         lineNumber,
         log);

      segment.ExternalPatientID = StringField.Parse(
         ref fieldEnumerator,
         encodingDetails,
         _fieldSpecifications[1],
         lineNumber,
         log);

      segment.InternalPatientId = StringField.Parse(
         ref fieldEnumerator,
         encodingDetails,
         _fieldSpecifications[2],
         lineNumber,
         log);

      segment.AlternatePatientID = StringField.Parse(
         ref fieldEnumerator,
         encodingDetails,
         _fieldSpecifications[3],
         lineNumber,
         log);

      segment.PatientName = StringField.Parse(
         ref fieldEnumerator,
         encodingDetails,
         _fieldSpecifications[4],
         lineNumber,
         log);

      segment.MothersMaidenName = StringField.Parse(
         ref fieldEnumerator,
         encodingDetails,
         _fieldSpecifications[5],
         lineNumber,
         log);

      segment.DateOfBirth = StringField.Parse(
         ref fieldEnumerator,
         encodingDetails,
         _fieldSpecifications[6],
         lineNumber,
         log);

      segment.Sex = StringField.Parse(
         ref fieldEnumerator,
         encodingDetails,
         _fieldSpecifications[7],
         lineNumber,
         log);

      segment.PatientAlias = StringField.Parse(
         ref fieldEnumerator,
         encodingDetails,
         _fieldSpecifications[8],
         lineNumber,
         log);

      segment.Race = StringField.Parse(
         ref fieldEnumerator,
         encodingDetails,
         _fieldSpecifications[9],
         lineNumber,
         log);

      segment.PatientAddress = StringField.Parse(
         ref fieldEnumerator,
         encodingDetails,
         _fieldSpecifications[10],
         lineNumber,
         log);

      segment.CountryCode = StringField.Parse(
         ref fieldEnumerator,
         encodingDetails,
         _fieldSpecifications[11],
         lineNumber,
         log);

      segment.HomePhoneNumber = StringField.Parse(
         ref fieldEnumerator,
         encodingDetails,
         _fieldSpecifications[12],
         lineNumber,
         log);

      segment.BusinessPhoneNumber = StringField.Parse(
         ref fieldEnumerator,
         encodingDetails,
         _fieldSpecifications[13],
         lineNumber,
         log);

      segment.PatientLanguage = StringField.Parse(
         ref fieldEnumerator,
         encodingDetails,
         _fieldSpecifications[14],
         lineNumber,
         log);

      segment.MaritalStatus = StringField.Parse(
         ref fieldEnumerator,
         encodingDetails,
         _fieldSpecifications[15],
         lineNumber,
         log);

      segment.Religion = StringField.Parse(
         ref fieldEnumerator,
         encodingDetails,
         _fieldSpecifications[16],
         lineNumber,
         log);

      segment.PatientAccountNumber = StringField.Parse(
         ref fieldEnumerator,
         encodingDetails,
         _fieldSpecifications[17],
         lineNumber,
         log);

      segment.PatientSSN = StringField.Parse(
         ref fieldEnumerator,
         encodingDetails,
         _fieldSpecifications[18],
         lineNumber,
         log);

      segment.PatientDriversLicenseNumber = StringField.Parse(
         ref fieldEnumerator,
         encodingDetails,
         _fieldSpecifications[19],
         lineNumber,
         log);

      segment.MothersIdentifier = StringField.Parse(
         ref fieldEnumerator,
         encodingDetails,
         _fieldSpecifications[20],
         lineNumber,
         log);

      segment.EthnicGroup = StringField.Parse(
         ref fieldEnumerator,
         encodingDetails,
         _fieldSpecifications[21],
         lineNumber,
         log);

      segment.BirthPlace = StringField.Parse(
         ref fieldEnumerator,
         encodingDetails,
         _fieldSpecifications[22],
         lineNumber,
         log);

      segment.MultipleBirthIndicator = StringField.Parse(
         ref fieldEnumerator,
         encodingDetails,
         _fieldSpecifications[23],
         lineNumber,
         log);

      segment.BirthOrder = StringField.Parse(
         ref fieldEnumerator,
         encodingDetails,
         _fieldSpecifications[24],
         lineNumber,
         log);

      segment.Citizenship = StringField.Parse(
         ref fieldEnumerator,
         encodingDetails,
         _fieldSpecifications[25],
         lineNumber,
         log);

      segment.VeteransMilitaryStatus = StringField.Parse(
         ref fieldEnumerator,
         encodingDetails,
         _fieldSpecifications[26],
         lineNumber,
         log);

      return segment;
   }
}
