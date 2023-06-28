namespace HL7.Net.Core;

/// <summary>
///   A field containing string data. Implements HL7 V2.2 spec section 2.8.10.1.
///   String data is left justified and trailing blanks are preserved.
/// </summary>
public sealed record StringField : IPresence
{
   /// <summary>
   ///   Represents a string field that is not present.
   /// </summary>
   public static readonly StringField NotPresent = new(null, Presence.NotPresent);

   /// <summary>
   ///   Represents a string field that is present but is null.
   /// </summary>
   public static readonly StringField PresentButNull = new(null, Presence.PresentButNull);

   internal StringField(
      String? value,
      Presence fieldPresence = Presence.Present)
   {
      Value = value;
      Presence = fieldPresence;
   }

   public static implicit operator String?(StringField field) => field.Value;

   /// <inheritdoc/>
   public Presence Presence { get; init; }

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
      if (!fieldEnumerator.MoveNext())
      {
         log.LogFieldNotPresent(lineNumber, fieldSpecification);
         return StringField.NotPresent;
      }

      var fieldContents = fieldEnumerator.Current;
      var decoded = fieldContents.GetDecodedString(encodingDetails).TrimStart();

      if (String.IsNullOrEmpty(decoded))
      {
         log.LogFieldNotPresent(lineNumber, fieldSpecification);
         return StringField.NotPresent;
      }
      if (decoded == GeneralConstants.PresentButNullValue)
      {
         log.LogFieldPresentButNull(lineNumber, fieldSpecification);
         return StringField.PresentButNull;
      }
      if (decoded.Length > fieldSpecification.Length)
      {
         var message = String.Format(
            Messages.LogFieldPresentButTruncated,
            fieldSpecification.FieldDescription,
            fieldSpecification.Length);
         log.LogWarning(
            message,
            lineNumber,
            fieldSpecification,
            fieldContents.ToString());
         return new StringField(decoded[..fieldSpecification.Length]);
      }

      log.LogFieldPresent(lineNumber, fieldSpecification, fieldContents);
      return new StringField(decoded);
   }
}
