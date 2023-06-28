namespace HL7.Net.V2Point2;

/// <summary>
///   The MSH segment defines the intent, source, destination, and some 
///   specifics of the syntax of a message.
/// </summary>
public class MessageHeaderSegment : ISegment
{
   internal MessageHeaderSegment() { }

   private static readonly List<FieldSpecification> _fieldSpecifications = new()
   {
      new FieldSpecification(SegmentIDs.MessageHeaderSegment, 1, "Field Separator", 1, HL7Datatype.ST_String, Optionality.Required, "N"),
      new FieldSpecification(SegmentIDs.MessageHeaderSegment, 2, "Encoding Characters", 4, HL7Datatype.ST_String, Optionality.Required, "N"),
      new FieldSpecification(SegmentIDs.MessageHeaderSegment, 3, "Sending Application", 15, HL7Datatype.ST_String, Optionality.Optional, "N"),
      new FieldSpecification(SegmentIDs.MessageHeaderSegment, 4, "Sending Facility", 20, HL7Datatype.ST_String, Optionality.Optional, "N"),
      new FieldSpecification(SegmentIDs.MessageHeaderSegment, 5, "Receiving Application", 30, HL7Datatype.ST_String, Optionality.Optional, "N"),
      new FieldSpecification(SegmentIDs.MessageHeaderSegment, 6, "Receiving Facility", 30, HL7Datatype.ST_String, Optionality.Optional, "N"),
      new FieldSpecification(SegmentIDs.MessageHeaderSegment, 7, "Date/Time of Message", 26, HL7Datatype.TS_Timestamp, Optionality.Optional, "N"),
      new FieldSpecification(SegmentIDs.MessageHeaderSegment, 8, "Security", 40, HL7Datatype.ST_String, Optionality.Optional, "N"),
      new FieldSpecification(SegmentIDs.MessageHeaderSegment, 9, "Message Type", 7, HL7Datatype.CM_Composite, Optionality.Required, "N"),
      new FieldSpecification(SegmentIDs.MessageHeaderSegment, 10, "Message Control ID", 20, HL7Datatype.ST_String, Optionality.Required, "N"),
      new FieldSpecification(SegmentIDs.MessageHeaderSegment, 11, "Processing ID", 1, HL7Datatype.ID_CodedValue, Optionality.Required, "N"),
      new FieldSpecification(SegmentIDs.MessageHeaderSegment, 12, "Version ID", 8, HL7Datatype.ID_CodedValue, Optionality.Required, "N"),
      new FieldSpecification(SegmentIDs.MessageHeaderSegment, 13, "Sequence Number", 15, HL7Datatype.NM_Numeric, Optionality.Optional, "N"),
      new FieldSpecification(SegmentIDs.MessageHeaderSegment, 14, "Continuation Pointer", 180, HL7Datatype.ST_String, Optionality.Optional, "N"),
      new FieldSpecification(SegmentIDs.MessageHeaderSegment, 15, "Accept Acknowledgment Type", 2, HL7Datatype.ID_CodedValue, Optionality.Optional, "N"),
      new FieldSpecification(SegmentIDs.MessageHeaderSegment, 16, "Application Acknowledgment Type", 2, HL7Datatype.ID_CodedValue, Optionality.Optional, "N"),
      new FieldSpecification(SegmentIDs.MessageHeaderSegment, 17, "Country Code", 2, HL7Datatype.ID_CodedValue, Optionality.Optional, "N"),
   };

   /// <inheritdoc/>
   public String SegmentID => SegmentIDs.MessageHeaderSegment;

   /// <summary>
   ///   Defines the character to be used as a separator for the rest of the 
   ///   message.
   /// </summary>
   public FieldSeparatorField FieldSeparator { get; private set; } = default!;

   /// <summary>
   ///   Defines the component separator, repetition separator, escape character
   ///   and subcomponent separator.
   /// </summary>
   public EncodingCharactersField EncodingCharacters { get; private set; } = default!;

   /// <summary>
   ///   Identifies the sending application for use by lower level protocols.
   /// </summary>
   public StringField SendingApplication { get; private set; } = default!;

   /// <summary>
   ///   Identifies one of several occurrences of the same application within 
   ///   the sending system.
   /// </summary>
   public StringField SendingFacility { get; private set; } = default!;

   /// <summary>
   ///   Identifies the receiving application for use by lower level protocols.
   /// </summary>
   public StringField ReceivingApplication { get; private set; } = default!;

   /// <summary>
   ///   Identifies one of several occurrences of the same application within 
   ///   the receiving system.
   /// </summary>
   public StringField ReceivingFacility { get; private set; } = default!;

   /// <summary>
   ///   The date/time that the sending system created the message.
   /// </summary>
   public TimestampField DateTimeOfMessage { get; private set; } = default!;

   /// <summary>
   ///   Used to implement security features.
   /// </summary>
   public StringField Security { get; private set; } = default!;

   /// <summary>
   ///   Composite element identifying the message type and trigger event for 
   ///   this message.
   /// </summary>
   public StringField MessageType { get; private set; } = default!;

   /// <summary>
   ///   Number or other identifier that uniquely identifies the message.
   /// </summary>
   public StringField MessageControlID { get; private set; } = default!;

   /// <summary>
   ///   Used to decide whether to process the message.
   /// </summary>
   public StringField ProcessingID { get; private set; } = default!;

   /// <summary>
   ///   Matched by the receiving system to its own version to be sure the 
   ///   message will be interpreted correctly. 
   /// </summary>
   public StringField VersionID { get; private set; } = default!;

   /// <summary>
   ///   Non-null value in this field implies that the sequence number protocol 
   ///   is in use. This numeric field incremented by one for each subsequent 
   ///   value.
   /// </summary>
   public StringField SequenceNumber { get; private set; } = default!;

   /// <summary>
   ///   Used to define continuations in application-specific ways.
   /// </summary>
   public StringField ContinuationPointer { get; private set; } = default!;

   /// <summary>
   ///   Defines the conditions under which accept acknowledgments are required 
   ///   to be returned in response to this message.
   /// </summary>
   public StringField AcceptAcknowledgmentType { get; private set; } = default!;

   /// <summary>
   ///   Defines the conditions under which application acknowledgments are 
   ///   required to be returned in response to this message.
   /// </summary>
   public StringField ApplicationAcknowledgmentType { get; private set; } = default!;

   /// <summary>
   ///   Defines the country of origin for the message
   /// </summary>
   public StringField CountryCode { get; private set; } = default!;

   internal static MessageHeaderSegment Parse(
      ReadOnlySpan<Char> segmentText,
      TimeSpan defaultTimezoneOffset,
      Int32 lineNumber,
      ProcessingLog log)
   {
      var segment = new MessageHeaderSegment();

      segment.FieldSeparator = FieldSeparatorField.Parse(
         segmentText,
         _fieldSpecifications[0],
         lineNumber,
         log);

      // Enumerate the fields and skip over the first field which contains the
      // segment ID.
      var fieldEnumerator = segmentText.ToFields(
         segment.FieldSeparator,
         '\0');
      fieldEnumerator.MoveNext();

      segment.EncodingCharacters = EncodingCharactersField.Parse(
         ref fieldEnumerator, 
         _fieldSpecifications[1],
         lineNumber,
         log);

      var encodingDetails = new EncodingDetails(
         segment.FieldSeparator,
         segment.EncodingCharacters.ComponentSeparator,
         segment.EncodingCharacters.RepetitionSeparator,
         segment.EncodingCharacters.EscapeCharacter,
         segment.EncodingCharacters.SubComponentSeparator);

      segment.SendingApplication = StringField.Parse(
         ref fieldEnumerator,
         encodingDetails,
         _fieldSpecifications[2],
         lineNumber,
         log);

      segment.SendingFacility = StringField.Parse(
         ref fieldEnumerator,
         encodingDetails,
         _fieldSpecifications[3],
         lineNumber,
         log);

      segment.ReceivingApplication = StringField.Parse(
         ref fieldEnumerator,
         encodingDetails,
         _fieldSpecifications[4],
         lineNumber,
         log);

      segment.ReceivingFacility = StringField.Parse(
         ref fieldEnumerator,
         encodingDetails,
         _fieldSpecifications[5],
         lineNumber,
         log);

      segment.DateTimeOfMessage = TimestampField.Parse(
         ref fieldEnumerator,
         encodingDetails,
         _fieldSpecifications[6],
         defaultTimezoneOffset,
         lineNumber,
         log);

      segment.Security = StringField.Parse(
         ref fieldEnumerator,
         encodingDetails,
         _fieldSpecifications[7],
         lineNumber,
         log);

      segment.MessageType = StringField.Parse(
         ref fieldEnumerator,
         encodingDetails,
         _fieldSpecifications[8],
         lineNumber,
         log);

      segment.MessageControlID = StringField.Parse(
         ref fieldEnumerator,
         encodingDetails,
         _fieldSpecifications[9],
         lineNumber,
         log);

      segment.ProcessingID = StringField.Parse(
         ref fieldEnumerator,
         encodingDetails,
         _fieldSpecifications[10],
         lineNumber,
         log);

      segment.VersionID = StringField.Parse(
         ref fieldEnumerator,
         encodingDetails,
         _fieldSpecifications[11],
         lineNumber,
         log);

      segment.SequenceNumber = StringField.Parse(
         ref fieldEnumerator,
         encodingDetails,
         _fieldSpecifications[12],
         lineNumber,
         log);

      segment.ContinuationPointer = StringField.Parse(
         ref fieldEnumerator,
         encodingDetails,
         _fieldSpecifications[13],
         lineNumber,
         log);

      segment.AcceptAcknowledgmentType = StringField.Parse(
         ref fieldEnumerator,
         encodingDetails,
         _fieldSpecifications[14],
         lineNumber,
         log);

      segment.ApplicationAcknowledgmentType = StringField.Parse(
         ref fieldEnumerator,
         encodingDetails,
         _fieldSpecifications[15],
         lineNumber,
         log);

      segment.CountryCode = StringField.Parse(
         ref fieldEnumerator,
         encodingDetails,
         _fieldSpecifications[16],
         lineNumber,
         log);

      return segment;
   }
}
