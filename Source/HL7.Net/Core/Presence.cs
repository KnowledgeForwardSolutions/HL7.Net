namespace HL7.Net.Core;

/// <summary>
///   Defines the options for a field to be present in an HL7 message.
/// </summary>
public enum Presence
{
   /// <summary>
   ///  The field is not present.
   /// </summary>
   NotPresent,

   /// <summary>
   ///   The field is present and has a non-null value.
   /// </summary>
   Present,

   /// <summary>
   ///   The field is present but has a null value.
   /// </summary>
   PresentButNull
}
