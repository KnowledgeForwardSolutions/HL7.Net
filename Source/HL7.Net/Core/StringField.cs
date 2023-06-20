namespace HL7.Net.Core;

/// <summary>
///   A field containing string data. Implements HL7 V2.2 spec section 2.8.10.1.
///   String data is left justified and trailing blanks are preserved.
/// </summary>
public sealed record StringField
{
   /// <summary>
   ///   Represents a string field that is not present.
   /// </summary>
   public static readonly StringField NotPresent = new(null, FieldPresence.NotPresent);

   /// <summary>
   ///   Represents a string field that is present but is null.
   /// </summary>
   public static readonly StringField PresentButNull = new(null, FieldPresence.PresentButNull);

   internal StringField(
      String? value,
      FieldPresence fieldPresence = FieldPresence.Present)
   {
      Value = value;
      FieldPresence = fieldPresence;
   }

   public static implicit operator String?(StringField field) => field.Value;

   /// <summary>
   ///   Identifies if this field is present in the message and if the value of
   ///   the field is null or not.
   /// </summary>
   public FieldPresence FieldPresence { get; init; }

   /// <summary>
   ///   The value of this field.
   /// </summary>
   public String? Value { get; init; }

   internal static StringField Parse(
      ref FieldEnumerator fieldEnumerator,
      EncodingDetails encodingDetails,
      FieldSpecification fieldSpecification,
      Int32 lineNumber,
      ProcessingLog log)
   {
      if (!fieldEnumerator.MoveNext() || fieldEnumerator.Current.IsEmpty)
      {
         log.LogFieldNotPresent(lineNumber, fieldSpecification);
         return StringField.NotPresent;
      }

      var fieldContents = fieldEnumerator.Current;
      if (fieldContents.Length == 2 && fieldContents[0] == '"' && fieldContents[1] == '"')
      {
         log.LogFieldPresentButNull(lineNumber, fieldSpecification);
         return StringField.PresentButNull;
      }

      var raw = fieldContents.ToString();
      if (raw.Length > fieldSpecification.Length)
      {
         log.LogWarning(
            $"String value truncated to field maximum length ({fieldSpecification.Length})",
            lineNumber,
            fieldSpecification,
            raw);
      }
      return new StringField(raw.Length > fieldSpecification.Length 
         ? raw[..fieldSpecification.Length] 
         : raw);
   }
}
