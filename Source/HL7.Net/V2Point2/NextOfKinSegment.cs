namespace HL7.Net.V2Point2;

public class NextOfKinSegment : ISegment
{
   internal NextOfKinSegment() { }

   private static readonly List<FieldSpecification> _fieldSpecifications = new()
   {
      new FieldSpecification(SegmentIDs.NextOfKinSegment, 1, "Set ID - Next of Kin", 4, HL7Datatype.SI_SequenceID, Optionality.Required, "N"),
      new FieldSpecification(SegmentIDs.NextOfKinSegment, 2, "Name", 48, HL7Datatype.PN_PersonName, Optionality.Optional, "N"),
      new FieldSpecification(SegmentIDs.NextOfKinSegment, 3, "Relationship", 60, HL7Datatype.CE_CodedElement, Optionality.Optional, "N"),
      new FieldSpecification(SegmentIDs.NextOfKinSegment, 4, "Address", 106, HL7Datatype.AD_Address, Optionality.Optional, "N"),
      new FieldSpecification(SegmentIDs.NextOfKinSegment, 5, "Phone Number", 40, HL7Datatype.TN_TelephoneNumber, Optionality.Optional, "3"),
      new FieldSpecification(SegmentIDs.NextOfKinSegment, 6, "Business Phone Number", 40, HL7Datatype.TN_TelephoneNumber, Optionality.Optional, "N"),
      new FieldSpecification(SegmentIDs.NextOfKinSegment, 7, "Contact Role", 60, HL7Datatype.CE_CodedElement, Optionality.Optional, "N"),
      new FieldSpecification(SegmentIDs.NextOfKinSegment, 8, "Start Date", 8, HL7Datatype.DT_Date, Optionality.Optional, "N"),
      new FieldSpecification(SegmentIDs.NextOfKinSegment, 9, "End Date", 8, HL7Datatype.DT_Date, Optionality.Optional, "N"),
      new FieldSpecification(SegmentIDs.NextOfKinSegment, 10, "Next of Kin Job Title", 60, HL7Datatype.ST_String, Optionality.Optional, "N"),
      new FieldSpecification(SegmentIDs.NextOfKinSegment, 11, "Next of Kin Job Code/Class", 20, HL7Datatype.CM_Composite, Optionality.Optional, "N"),
      new FieldSpecification(SegmentIDs.NextOfKinSegment, 12, "Next of Kin Employee Number", 20, HL7Datatype.ST_String, Optionality.Optional, "N"),
      new FieldSpecification(SegmentIDs.NextOfKinSegment, 13, "Organization Name", 60, HL7Datatype.ST_String, Optionality.Optional, "N"),
   };

   /// <inheritdoc/>
   public String SegmentID => SegmentIDs.NextOfKinSegment;

   /// <summary>
   ///   For those messages that permit segments to repeat, the Set ID field is 
   ///   used to identify the repetitions.
   /// </summary>
   public StringField SetID { get; private set; } = default!;

   /// <summary>
   ///   Name of the next of kin.
   /// </summary>
   public StringField Name { get; private set; } = default!;

   /// <summary>
   ///   Defines the actual personal relationship that the next of kin has to 
   ///   the patient.
   /// </summary>
   public StringField Relationship { get; private set; } = default!;

   /// <summary>
   ///   Defines the address of the associated party.
   /// </summary>
   public StringField Address { get; private set; } = default!;

   /// <summary>
   ///   Defines the telephone number of the associated party.
   /// </summary>
   public StringField PhoneNumber { get; private set; } = default!;

   /// <summary>
   ///   Defines the business telephone number of the associated party.
   /// </summary>
   public StringField BusinessPhoneNumber { get; private set; } = default!;

   /// <summary>
   ///   Indicates the specific relationship role.
   /// </summary>
   public StringField ContactRole { get; private set; } = default!;

   /// <summary>
   ///   Start of relationship.
   /// </summary>
   public StringField StartDate { get; private set; } = default!;

   /// <summary>
   ///   End of relationship.
   /// </summary>
   public StringField EndDate { get; private set; } = default!;

   /// <summary>
   ///   The title of the next of kin at their place of employment. 
   /// </summary>
   public StringField NextOfKinJobTitle { get; private set; } = default!;

   /// <summary>
   ///   The employers Job Code or Employee Classification used for the next of 
   ///   kin at their place of employment.
   /// </summary>
   public StringField NextOfKinJobCodeOrClass { get; private set; } = default!;

   /// <summary>
   ///   Number the employer assigns to the employee that is acting as next of 
   ///   kin.
   /// </summary>
   public StringField NextOfKinJobEmployeeNumber { get; private set; } = default!;

   /// <summary>
   ///   In cases where an employer serves as next of kin, this is the name of 
   ///   the organization which serves as the next of kin.This field may also be 
   ///   used to communicate the name of the organization where the next of kin 
   ///   works. 
   /// </summary>
   public StringField OrganizationName { get; private set; } = default!;

   internal static NextOfKinSegment Parse(
      ReadOnlySpan<Char> segmentText,
      EncodingDetails encodingDetails,
      Int32 lineNumber,
      ProcessingLog log)
   {
      var segment = new NextOfKinSegment();

      // Enumerate the fields and skip over the first field which contains the
      // segment ID.
      var fieldEnumerator = segmentText.ToFields(encodingDetails.FieldSeparator);
      fieldEnumerator.MoveNext();

      segment.SetID = StringField.Parse(
         ref fieldEnumerator,
         encodingDetails,
         _fieldSpecifications[0],
         lineNumber,
         log);

      segment.Name = StringField.Parse(
         ref fieldEnumerator,
         encodingDetails,
         _fieldSpecifications[1],
         lineNumber,
         log);

      segment.Relationship = StringField.Parse(
         ref fieldEnumerator,
         encodingDetails,
         _fieldSpecifications[2],
         lineNumber,
         log);

      segment.Address = StringField.Parse(
         ref fieldEnumerator,
         encodingDetails,
         _fieldSpecifications[3],
         lineNumber,
         log);

      segment.PhoneNumber = StringField.Parse(
         ref fieldEnumerator,
         encodingDetails,
         _fieldSpecifications[4],
         lineNumber,
         log);

      segment.BusinessPhoneNumber = StringField.Parse(
         ref fieldEnumerator,
         encodingDetails,
         _fieldSpecifications[5],
         lineNumber,
         log);

      segment.ContactRole = StringField.Parse(
         ref fieldEnumerator,
         encodingDetails,
         _fieldSpecifications[6],
         lineNumber,
         log);

      segment.StartDate = StringField.Parse(
         ref fieldEnumerator,
         encodingDetails,
         _fieldSpecifications[7],
         lineNumber,
         log);

      segment.EndDate = StringField.Parse(
         ref fieldEnumerator,
         encodingDetails,
         _fieldSpecifications[8],
         lineNumber,
         log);

      segment.NextOfKinJobTitle = StringField.Parse(
         ref fieldEnumerator,
         encodingDetails,
         _fieldSpecifications[9],
         lineNumber,
         log);

      segment.NextOfKinJobCodeOrClass = StringField.Parse(
         ref fieldEnumerator,
         encodingDetails,
         _fieldSpecifications[10],
         lineNumber,
         log);

      segment.NextOfKinJobEmployeeNumber = StringField.Parse(
         ref fieldEnumerator,
         encodingDetails,
         _fieldSpecifications[11],
         lineNumber,
         log);

      segment.OrganizationName = StringField.Parse(
         ref fieldEnumerator,
         encodingDetails,
         _fieldSpecifications[12],
         lineNumber,
         log);

      return segment;
   }
}
