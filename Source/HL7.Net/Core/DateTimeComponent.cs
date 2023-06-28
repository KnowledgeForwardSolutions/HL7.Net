namespace HL7.Net.Core;

/// <summary>
///   Date/time component of a timestamp field. Implements HL7 V2.2 spec section
///   2.8.10.7.
/// </summary>
public sealed record DateTimeComponent : IPresence
{
   private static readonly String[] AcceptedFormats = 
   {
      GeneralConstants.DatePrecisionFormat,
      GeneralConstants.TimestampMinutePrecisionFormat,
      GeneralConstants.TimestampSecondPrecisionFormat,
      GeneralConstants.TimestampSubSecondPrecisionFormat,
   };

   /// <summary>
   ///   Represents a date/time component that is not present.
   /// </summary>
   public static readonly DateTimeComponent NotPresent = new(null, Presence.NotPresent);

   /// <summary>
   ///   Represents a date/time component that is present but is null.
   /// </summary>
   public static readonly DateTimeComponent PresentButNull = new(null, Presence.PresentButNull);

   internal DateTimeComponent(
      DateTimeOffset? value,
      Presence fieldPresence = Presence.Present)
   {
      Value = value;
      Presence = fieldPresence;
   }

   public static implicit operator DateTimeOffset?(DateTimeComponent field) => field.Value;

   /// <inheritdoc/>
   public Presence Presence { get; init; }

   /// <summary>
   ///   The value of this component.
   /// </summary>
   public DateTimeOffset? Value { get; init; }

   internal static DateTimeComponent Parse(
      ref FieldEnumerator fieldEnumerator,
      FieldSpecification fieldSpecification,
      TimeSpan defaultTimezoneOffset,
      Int32 lineNumber,
      ProcessingLog log)
   {
      if (!fieldEnumerator.MoveNext() || fieldEnumerator.Current.IsEmpty)
      {
         log.LogFieldNotPresent(lineNumber, fieldSpecification);
         return DateTimeComponent.NotPresent;
      }

      if (fieldEnumerator.Current.IsPresentButNull())
      {
         log.LogFieldPresentButNull(lineNumber, fieldSpecification);
         return DateTimeComponent.PresentButNull;
      }

      String message;
      var fieldContents = fieldEnumerator.Current;
      var timezoneOffset = fieldContents.IndexOfAny('+', '-');
      var datetimeContents = timezoneOffset == -1
         ? fieldContents : fieldContents[..timezoneOffset];
      if (!DateTime.TryParseExact(
         datetimeContents, 
         AcceptedFormats, 
         CultureInfo.InvariantCulture, 
         DateTimeStyles.None,
         out var datetimeValue))
      {
         message = String.Format(
            Messages.InvalidTimestampValue,
            fieldSpecification.FieldDescription);
         log.LogError(message, lineNumber, fieldSpecification, fieldContents.ToString());
         return DateTimeComponent.NotPresent;
      }

      TimeSpan offsetValue = defaultTimezoneOffset;
      if (timezoneOffset != -1)
      {
         var timezoneContents = fieldContents[timezoneOffset..];
         var format = timezoneContents[0] == '+' 
            ? GeneralConstants.PositiveTimeZoneOffsetFormat
            : GeneralConstants.NegativeTimeZoneOffsetFormat;
         var styles = timezoneContents[0] == '+' 
            ? TimeSpanStyles.None
            : TimeSpanStyles.AssumeNegative;
         if (!TimeSpan.TryParseExact(timezoneContents, format, null, styles, out offsetValue))
         {
            message = String.Format(
               Messages.InvalidTimestampValue,
               fieldSpecification.FieldDescription);
            log.LogError(message, lineNumber, fieldSpecification, fieldContents.ToString());
            return DateTimeComponent.NotPresent;
         }
      }

      log.LogFieldPresent(lineNumber, fieldSpecification, fieldContents);
      return new DateTimeComponent(new DateTimeOffset(datetimeValue, offsetValue));
   }
}
