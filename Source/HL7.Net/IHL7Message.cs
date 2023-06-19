namespace HL7.Net;

public interface IHL7Message
{
   /// <summary>
   ///   The segments that make up the message, in the order that they appear in
   ///   the original HL7 text.
   /// </summary>
   IReadOnlyList<ISegment> Segments { get; }
}
