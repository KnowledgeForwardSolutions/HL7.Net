namespace HL7.Net.Core;

/// <summary>
///   A field containing numeric data. Implements HL7 V2.2 spec section 2.8.10.4.
/// </summary>
public sealed record NumericField : IPresence
{
   /// <summary>
   ///   Represents a numeric field that is not present.
   /// </summary>
   public static readonly NumericField NotPresent = new(null, Presence.NotPresent);

   /// <summary>
   ///   Represents a numeric field that is present but is null.
   /// </summary>
   public static readonly NumericField PresentButNull = new(null, Presence.PresentButNull);

   internal NumericField(
      Decimal? value,
      Presence fieldPresence = Presence.Present)
   {
      Value = value;
      Presence = fieldPresence;
   }

   public static implicit operator Decimal?(NumericField field) => field.Value;

   /// <inheritdoc/>
   public Presence Presence { get; init; }

   /// <summary>
   ///   The value of this field.
   /// </summary>
   public Decimal? Value { get; init; }

   internal static NumericField Parse(
      ref FieldEnumerator fieldEnumerator,
      FieldSpecification fieldSpecification,
      Int32 lineNumber,
      ProcessingLog log)
   {
      if (!fieldEnumerator.MoveNext() || fieldEnumerator.Current.IsEmpty)
      {
         log.LogFieldNotPresent(lineNumber, fieldSpecification);
         return NumericField.NotPresent;
      }

      if (fieldEnumerator.Current.IsPresentButNull())
      {
         log.LogFieldPresentButNull(lineNumber, fieldSpecification);
         return NumericField.PresentButNull;
      }

      var fieldContents = fieldEnumerator.Current.Length > fieldSpecification.Length
         ? fieldEnumerator.Current[..fieldSpecification.Length]
         : fieldEnumerator.Current;
      if (Decimal.TryParse(fieldContents, GeneralConstants.NumericValueStyle, null, out var value))
      {
         log.LogFieldPresent(
            lineNumber,
            fieldSpecification,
            fieldEnumerator.Current,
            checkTruncated: true);
         return new NumericField(value);
      }

      var message = String.Format(
         Messages.InvalidNumericValue,
         fieldSpecification.FieldDescription);
      log.LogError(message, lineNumber, fieldSpecification, fieldEnumerator.Current.ToString());
      return NumericField.NotPresent;
   }
}
