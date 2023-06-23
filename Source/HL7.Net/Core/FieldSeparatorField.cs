namespace HL7.Net.Core;

/// <summary>
///   Defines the field separator used in an HL7 message.
/// </summary>
public sealed record FieldSeparatorField
{
   internal FieldSeparatorField(
      Char value, 
      String? rawValue,
      FieldPresence fieldPresence = FieldPresence.Present)
   {
      Value = value;
      RawValue = rawValue;
      FieldPresence = fieldPresence;
   }

   public static implicit operator Char(FieldSeparatorField field) => field.Value;

   /// <summary>
   ///   Identifies if this field is present in the message and if the value of
   ///   the field is null or not.
   /// </summary>
   public FieldPresence FieldPresence { get; init; }

   /// <summary>
   ///   The raw text value for this field that was read from an HL7 message.
   /// </summary>
   public String? RawValue { get; init; }

   /// <summary>
   ///   The value of this field.
   /// </summary>
   public Char Value { get; init; }

   internal static FieldSeparatorField Parse(
      ReadOnlySpan<Char> span,
      FieldSpecification fieldSpecification,
      Int32 lineNumber,
      ProcessingLog log)
   {
      // Expects entire line with first 3 characters = segment ID and 4th 
      // character being the field separator.
      if (span.Length < 4)
      {
         log.LogFatalError(
            $"Missing required field - {fieldSpecification.FieldName}",
            lineNumber,
            fieldSpecification);
         return new FieldSeparatorField('\0', null, FieldPresence.NotPresent);
      }

      var fieldSeparator = span[3];
      var raw = fieldSeparator.ToString();
      
      return new FieldSeparatorField(fieldSeparator, raw);
   }
}
