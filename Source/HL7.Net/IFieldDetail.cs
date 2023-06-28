namespace HL7.Net;

/// <summary>
///   Common properties of a field in a segment or a composite element.
/// </summary>
public interface IFieldDetail
{
   /// <summary>
   ///   The name of this field in the segment or composite element.
   /// </summary>
   String FieldName { get; }

   /// <summary>
   ///   The relative position of this field within the segment or composite
   ///   element.
   /// </summary>
   Int32 Index { get; }

   /// <summary>
   ///   Identifies if this field is optional, required or conditionally 
   ///   required.
   /// </summary>
   Optionality Optionality { get; }

   /// <summary>
   ///   Identifies if this field is present in the message and if the value of
   ///   the field is null or not.
   /// </summary>
   Presence FieldPresence { get; }

   /// <summary>
   ///   The raw text value for this field that was read from an HL7 message.
   /// </summary>
   String? RawValue { get; }
}
