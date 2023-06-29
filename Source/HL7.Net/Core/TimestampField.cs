namespace HL7.Net.Core;

/// <summary>
///   A composite field containing a timestamp. Implements HL7 V2.2 spec section
///   2.8.10.7.
/// </summary>
public sealed record TimestampField : IPresence
{
   internal TimestampField() { }

   private static readonly List<FieldSpecification> _fieldSpecifications = new()
   {
      new FieldSpecification("TS", 1, "DateTime", 26, HL7Datatype.DateTimeComponent, Optionality.Required, "N"),
      new FieldSpecification("TS", 2, "DegreeOfPrecision", 1, HL7Datatype.TimestampDegreeOfPrecisionComponent, Optionality.Optional, "N"),
   };

   /// <summary>
   ///   Represents a timestamp field that is not present.
   /// </summary>
   public static readonly TimestampField NotPresent = new()
   {
      Timestamp = DateTimeComponent.NotPresent,
      DegreeOfPrecision = DegreeOfPrecisionComponent.NotPresent
   };

   /// <summary>
   ///   Represents a timestamp field that is present but is null.
   /// </summary>
   public static readonly TimestampField PresentButNull = new()
   {
      Timestamp = DateTimeComponent.NotPresent,
      DegreeOfPrecision = DegreeOfPrecisionComponent.NotPresent,
      Presence = Presence.PresentButNull
   };

   /// <inheritdoc/>
   public Presence Presence { get; private set; } = Presence.NotPresent;

   /// <summary>
   ///   The date/time portion (including timezone) of a timestamp field.
   /// </summary>
   public DateTimeComponent Timestamp { get; private set; } = default!;

   /// <summary>
   ///   The degree of precision of the date/time portion.
   /// </summary>
   public DegreeOfPrecisionComponent DegreeOfPrecision { get; private set; } = default!;

   internal static TimestampField Parse(
      ref FieldEnumerator fieldEnumerator,
      EncodingDetails encodingDetails,
      FieldSpecification fieldSpecification,
      TimeSpan defaultTimezoneOffset,
      Int32 lineNumber,
      ProcessingLog log)
   {
      if (!fieldEnumerator.MoveNext() || fieldEnumerator.Current.IsEmpty)
      {
         log.LogFieldNotPresent(lineNumber, fieldSpecification);
         return TimestampField.NotPresent;
      }

      var fieldContents = fieldEnumerator.Current;
      if (fieldContents.IsPresentButNull())
      {
         log.LogFieldPresentButNull(lineNumber, fieldSpecification);
         return TimestampField.PresentButNull;
      }

      var field = new TimestampField();

      var componentEnumerator = fieldContents.ToFields(
         encodingDetails.ComponentSeparator,
         encodingDetails.EscapeCharacter);

      field.Timestamp = DateTimeComponent.Parse(
         ref componentEnumerator,
         _fieldSpecifications[0],
         defaultTimezoneOffset,
         lineNumber,
         log);

      field.DegreeOfPrecision = DegreeOfPrecisionComponent.Parse(
         ref componentEnumerator,
         _fieldSpecifications[1],
         lineNumber,
         log);

      field.Presence = Presence.Present;

      return field;
   }
}
