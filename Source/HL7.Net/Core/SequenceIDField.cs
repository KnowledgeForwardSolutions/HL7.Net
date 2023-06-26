namespace HL7.Net.Core;

/// <summary>
///   A field containing an integer sequence id. Implements HL7 V2.2 spec 
///   section 2.8.10.12.
/// </summary>
public sealed record SequenceIDField
{
   /// <summary>
   ///   Represents an sequence id field that is not present.
   /// </summary>
   public static readonly SequenceIDField NotPresent = new(null, FieldPresence.NotPresent);

   /// <summary>
   ///   Represents a sequence id field that is present but is null.
   /// </summary>
   public static readonly SequenceIDField PresentButNull = new(null, FieldPresence.PresentButNull);

   internal SequenceIDField(
      Int32? value,
      FieldPresence fieldPresence = FieldPresence.Present)
   {
      Value = value;
      FieldPresence = fieldPresence;
   }

   public static implicit operator Int32?(SequenceIDField field) => field.Value;

   /// <summary>
   ///   Identifies if this field is present in the message and if the value of
   ///   the field is null or not.
   /// </summary>
   public FieldPresence FieldPresence { get; init; }

   /// <summary>
   ///   The value of this field.
   /// </summary>
   public Int32? Value { get; init; }

   internal static SequenceIDField Parse(
      ref FieldEnumerator fieldEnumerator,
      FieldSpecification fieldSpecification,
      Int32 lineNumber,
      ProcessingLog log)
   {
      if (!fieldEnumerator.MoveNext() || fieldEnumerator.Current.IsEmpty)
      {
         log.LogFieldNotPresent(lineNumber, fieldSpecification);
         return SequenceIDField.NotPresent;
      }

      if (fieldEnumerator.Current.IsPresentButNull())
      {
         log.LogFieldPresentButNull(lineNumber, fieldSpecification);
         return SequenceIDField.PresentButNull;
      }

      var fieldContents = fieldEnumerator.Current.Length > fieldSpecification.Length
         ? fieldEnumerator.Current[..fieldSpecification.Length]
         : fieldEnumerator.Current;
      if (Int32.TryParse(fieldContents, GeneralConstants.SequenceIdStyle, null, out var value))
      {
         log.LogFieldPresentButPossiblyTruncated(
            lineNumber,
            fieldSpecification,
            fieldEnumerator.Current);
         return new SequenceIDField(value);
      }

      var message = String.Format(
         Messages.InvalidNumericValue,
         fieldSpecification.FieldDescription);
      log.LogError(message, lineNumber, fieldSpecification, fieldEnumerator.Current.ToString());
      return SequenceIDField.NotPresent;
   }
}
