namespace HL7.Net.V2Point2;

public sealed class HL7Message : IHL7Message
{
   private readonly List<ISegment> _segments = new();

   /// <inheritdoc/>
   public IReadOnlyList<ISegment> Segments => _segments;

   /// <summary>
   ///   Parse the suppled <paramref name="messageText"/> into an HL7 V2.2
   ///   object graph.
   /// </summary>
   /// <param name="messageText">
   ///   The message text to parse.
   /// </param>
   /// <returns>
   ///   An <see cref="HL7Message"/> object.
   /// </returns>
   public static HL7Message Parse(
      ReadOnlySpan<Char> messageText,
      out ProcessingLog log)
   {
      log = new ProcessingLog();
      var message = new HL7Message();
      var lineNumber = 0;
      var encodingDetails = new EncodingDetails('\0', '\0', '\0', '\0', '\0');

      foreach (var line in messageText.ToLines())
      {
         if (line.IsEmpty)
         {
            log.LogWarning("Empty line ignored", lineNumber);
         }
         else
         {
            var segmentID = line[..3].ToString();
            ISegment segment = segmentID switch
            {
               SegmentIDs.EventTypeSegment => EventTypeSegment.Parse(
                  line,
                  encodingDetails,
                  lineNumber, 
                  log),
               SegmentIDs.MessageHeaderSegment => MessageHeaderSegment.Parse(
                  line,
                  ref encodingDetails,
                  lineNumber, 
                  log),
               SegmentIDs.NextOfKinSegment => NextOfKinSegment.Parse(
                  line,
                  encodingDetails,
                  lineNumber,
                  log),
               SegmentIDs.PatientIdentificationSegment => PatientIdentificationSegment.Parse(
                  line,
                  encodingDetails,
                  lineNumber,
                  log),
               SegmentIDs.PatientVisitSegment => PatientVisitSegment.Parse(
                  line,
                  encodingDetails,
                  lineNumber,
                  log),
               _ => UnrecognizedSegment(log, lineNumber, segmentID)
            };

            if (segment is not null)
            {
               message._segments.Add(segment);
            }
         }
         lineNumber++;
      };

      return message;
   }

   private static ISegment UnrecognizedSegment(
      ProcessingLog log,
      Int32 lineNumber, 
      String segmentID)
   {
      log.LogError(
         "Unrecognized segment ID",
         lineNumber,
         segmentID: segmentID);

      return null!;
   }
}
