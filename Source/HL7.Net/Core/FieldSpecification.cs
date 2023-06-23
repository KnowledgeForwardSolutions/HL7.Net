namespace HL7.Net.Core;

/// <summary>
///   Details for a field within a segment or composite element.
/// </summary>
/// <param name="SegmentID">
///   Three character code that identifies the segment type.
/// </param>
/// <param name="Sequence">
///   The one-based relative position of this field within the segment or 
///   composite element.
/// </param>
/// <param name="FieldName">
///   The name of this field in the segment or composite element.
/// </param>
/// <param name="Length">
///   The maximum length of this field an HL7 message.
/// </param>
/// <param name="Datatype">
///   The field datatype.
/// </param>
/// <param name="Optionality">
///   Identifies if this field is optional, required or conditionally 
///   required.
/// </param>
/// <param name="Repetition">
///   Identifies this field may repeat as well as the maximum number of 
///   repetitions allowed.
/// </param>
internal record FieldSpecification(
   String SegmentID,
   Int32 Sequence,
   String FieldName,
   Int32 Length,
   HL7Datatype Datatype,
   Optionality Optionality,
   Repetition Repetition)
{
   public String FieldDescription { get; private init; } = $"{SegmentID}.{Sequence}/{FieldName}";
}
