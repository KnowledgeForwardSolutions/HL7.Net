namespace HL7.Net.V2Point2;

/// <summary>
///   The EVN segment is used to communicate necessary trigger event information 
///   to receiving applications
/// </summary>
public class EventTypeSegment : ISegment
{
   internal EventTypeSegment() { }

   private static readonly List<FieldSpecification> _fieldSpecifications = new()
   {
      new FieldSpecification(SegmentIDs.EventTypeSegment, 1, "Event Type Code", 3, HL7Datatype.ID_CodedValue, Optionality.Required, "N"),
      new FieldSpecification(SegmentIDs.EventTypeSegment, 2, "Date/Time of Event", 26, HL7Datatype.TS_Timestamp, Optionality.Required, "N"),
      new FieldSpecification(SegmentIDs.EventTypeSegment, 3, "Date/Time Planned Event", 26, HL7Datatype.TS_Timestamp, Optionality.Optional, "N"),
      new FieldSpecification(SegmentIDs.EventTypeSegment, 4, "Event Reason Code", 3, HL7Datatype.ID_CodedValue, Optionality.Optional, "N"),
      new FieldSpecification(SegmentIDs.EventTypeSegment, 5, "Operator ID", 5, HL7Datatype.ID_CodedValue, Optionality.Optional, "N"),
   };

   /// <inheritdoc/>
   public String SegmentID => SegmentIDs.EventTypeSegment;

   /// <summary>
   ///   Identifies the trigger event that caused this message to be created.
   /// </summary>
   public StringField EventTypeCode { get; private set; } = default!;

   /// <summary>
   ///   The date/time of the trigger event that caused this message to be 
   ///   created.
   /// </summary>
   public StringField DateTimeOfEvent { get; private set; } = default!;

   /// <summary>
   ///   The date/time that the event is planned.
   /// </summary>
   public StringField DateTimePlannedEvent { get; private set; } = default!;

   /// <summary>
   ///   Describes the reason for this event.
   /// </summary>
   public StringField EventReasonCode { get; private set; } = default!;

   /// <summary>
   ///   Identifies the individual responsible for triggering the event.
   /// </summary>
   public StringField OperatorID { get; private set; } = default!;

   internal static EventTypeSegment Parse(
      ReadOnlySpan<Char> segmentText,
      EncodingDetails encodingDetails,
      Int32 lineNumber,
      ProcessingLog log)
   {
      var segment = new EventTypeSegment();

      // Enumerate the fields and skip over the first field which contains the
      // segment ID.
      var fieldEnumerator = segmentText.ToFields(
         encodingDetails.FieldSeparator,
         encodingDetails.EscapeCharacter);
      fieldEnumerator.MoveNext();

      segment.EventTypeCode = StringField.Parse(
         ref fieldEnumerator,
         encodingDetails,
         _fieldSpecifications[0],
         lineNumber,
         log);

      segment.DateTimeOfEvent = StringField.Parse(
         ref fieldEnumerator,
         encodingDetails,
         _fieldSpecifications[1],
         lineNumber,
         log);

      segment.DateTimePlannedEvent = StringField.Parse(
         ref fieldEnumerator,
         encodingDetails,
         _fieldSpecifications[2],
         lineNumber,
         log);

      segment.EventReasonCode = StringField.Parse(
         ref fieldEnumerator,
         encodingDetails,
         _fieldSpecifications[3],
         lineNumber,
         log);

      segment.OperatorID = StringField.Parse(
         ref fieldEnumerator,
         encodingDetails,
         _fieldSpecifications[4],
         lineNumber,
         log);

      return segment;
   }
}
