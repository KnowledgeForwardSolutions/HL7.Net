namespace HL7.Net.Core.Tables;

/// <summary>
///   HL7 Timestamp type degree of precision.
/// </summary>
public record Other_TimestampDegreeOfPrecision
   : TableEntry, IParsable<Other_TimestampDegreeOfPrecision>
{
   private Other_TimestampDegreeOfPrecision(
      String value = "",
      Presence presence = Presence.Present)
      : base(value, presence) { }

   public static readonly Other_TimestampDegreeOfPrecision NotPresent = new(presence: Presence.NotPresent);
   public static readonly Other_TimestampDegreeOfPrecision PresentButNull = new(presence: Presence.PresentButNull);

   public static readonly Other_TimestampDegreeOfPrecision Year = new("Y");
   public static readonly Other_TimestampDegreeOfPrecision Month = new("L");
   public static readonly Other_TimestampDegreeOfPrecision Day = new("D");
   public static readonly Other_TimestampDegreeOfPrecision Hour = new("H");
   public static readonly Other_TimestampDegreeOfPrecision Minute = new("M");
   public static readonly Other_TimestampDegreeOfPrecision Second = new("S");

   public static Other_TimestampDegreeOfPrecision Parse(
      ref FieldEnumerator fieldEnumerator,
      FieldSpecification fieldSpecification,
      T0104_VersionID versionID,                // Ignored
      Int32 lineNumber,
      IProcessingLog log)
   {
      if (!fieldEnumerator.MoveNext() || fieldEnumerator.Current.IsEmpty)
      {
         log.LogFieldNotPresent(lineNumber, fieldSpecification);
         return NotPresent;
      }

      if (fieldEnumerator.Current.IsPresentButNull())
      {
         log.LogFieldPresentButNull(lineNumber, fieldSpecification);
         return PresentButNull;
      }

      var fieldContents = fieldEnumerator.Current.ToString();
      var field = fieldContents switch
      {
         "Y" => Year,
         "L" => Month,
         "D" => Day,
         "H" => Hour,
         "M" => Minute,
         "S" => Second,
         _ => NotPresent
      };

      if (field == NotPresent)
      {
         log.LogUnrecognizedTableValue(lineNumber, fieldSpecification, fieldContents);
         return field;
      }

      log.LogFieldPresent(lineNumber, fieldSpecification, fieldContents);
      return field;
   }
}
