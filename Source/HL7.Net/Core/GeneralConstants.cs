namespace HL7.Net.Core;

internal static class GeneralConstants
{
   public const String PresentButNullValue = "\"\"";

   public const NumberStyles NumericValueStyle = 
      NumberStyles.AllowLeadingSign | NumberStyles.AllowDecimalPoint;

   public const NumberStyles SequenceIdStyle = NumberStyles.None;

   public const String DatePrecisionFormat = "yyyyMMdd";
   public const String TimeMinutePrecisionFormat = "HHmm";
   public const String TimeSecondPrecisionFormat = "HHmmss";
   public const String TimeSubSecondPrecisionFormat = "HHmmss.ffff";
   public const String TimestampMinutePrecisionFormat = "yyyyMMddHHmm";
   public const String TimestampSecondPrecisionFormat = "yyyyMMddHHmmss";
   public const String TimestampSubSecondPrecisionFormat = "yyyyMMddHHmmss.ffff";

   // Note escaped +/- character in format string. Need to ignore the leading
   // sign character when parsing timespan.
   public const String PositiveTimeZoneOffsetFormat = @"\+hhmm";
   public const String NegativeTimeZoneOffsetFormat = @"\-hhmm";
}
