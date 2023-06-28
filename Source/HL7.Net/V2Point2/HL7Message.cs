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
      TimeSpan defaultTimezoneOffset,
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
                  defaultTimezoneOffset,
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
               _ => UnrecognizedSegment(log, lineNumber, line.ToString())
            };

            if (segment is not null)
            {
               message._segments.Add(segment);
               if (segment.SegmentID == SegmentIDs.MessageHeaderSegment)
               {
                  var msh = segment as MessageHeaderSegment;
                  HandleHeader(msh!, ref encodingDetails, ref defaultTimezoneOffset);
               }
            }
         }
         lineNumber++;
      };

      return message;
   }

   /// <summary>
   ///  If a new header segment was encountered (BSH, FSH, MSH) then update the
   ///  encoding details and default timezone offset to use while parsing 
   ///  following segments.
   /// </summary>
   /// <param name="header">
   ///   The new header segment.
   /// </param>
   /// <param name="encodingDetails">
   ///   The encoding details to use when parsing a segment.
   /// </param>
   /// <param name="defaultTimezoneOffset">
   ///   The default timezone offset to use when parsing time or timestamp 
   ///   fields.
   /// </param>
   private static void HandleHeader(
      MessageHeaderSegment header,
      ref EncodingDetails encodingDetails,
      ref TimeSpan defaultTimezoneOffset)
   {
      encodingDetails = new EncodingDetails(
         header!.FieldSeparator,
         header.EncodingCharacters.ComponentSeparator,
         header.EncodingCharacters.RepetitionSeparator,
         header.EncodingCharacters.EscapeCharacter,
         header.EncodingCharacters.SubComponentSeparator);
      if (header.DateTimeOfMessage.IsPresentNotNull())
      {
         defaultTimezoneOffset = ((DateTimeOffset)header.DateTimeOfMessage.Timestamp.Value!).Offset;
      }
   }

   private static ISegment UnrecognizedSegment(
      ProcessingLog log,
      Int32 lineNumber, 
      String line)
   {
      log.LogError(
         "Unrecognized segment ID",
         lineNumber,
         rawData: line);

      return null!;
   }
}
