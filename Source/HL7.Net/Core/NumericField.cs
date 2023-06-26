namespace HL7.Net.Core;

/// <summary>
///   A field containing numeric data. Implements HL7 V2.2 spec section 2.8.10.4.
/// </summary>
public sealed record NumericField
{
   /// <summary>
   ///   Represents an numeric field that is not present.
   /// </summary>
   public static readonly NumericField NotPresent = new(null, FieldPresence.NotPresent);

   /// <summary>
   ///   Represents a numeric field that is present but is null.
   /// </summary>
   public static readonly NumericField PresentButNull = new(null, FieldPresence.PresentButNull);

   internal NumericField(
      Decimal? value,
      FieldPresence fieldPresence = FieldPresence.Present)
   {
      Value = value;
      FieldPresence = fieldPresence;
   }

   public static implicit operator Decimal?(NumericField field) => field.Value;

   /// <summary>
   ///   Identifies if this field is present in the message and if the value of
   ///   the field is null or not.
   /// </summary>
   public FieldPresence FieldPresence { get; init; }

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
         log.LogFieldPresentButPossiblyTruncated(
            lineNumber, 
            fieldSpecification, 
            fieldEnumerator.Current);
         return new NumericField(value);
      }

      var message = String.Format(
         Messages.InvalidNumericValue,
         fieldSpecification.FieldDescription);
      log.LogError(message, lineNumber, fieldSpecification, fieldEnumerator.Current.ToString());
      return NumericField.NotPresent;
   }
}
