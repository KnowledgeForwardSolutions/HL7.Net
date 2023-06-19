namespace HL7.Net;

/// <summary>
///   A single segment in an HL7 message.
/// </summary>
public interface ISegment
{
   /// <summary>
   ///   Three character code that uniquely identifies the segment type.
   /// </summary>
   String SegmentID { get; }
}
