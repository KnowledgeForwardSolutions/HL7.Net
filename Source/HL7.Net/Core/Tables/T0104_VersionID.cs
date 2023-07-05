namespace HL7.Net.Core.Tables;

/// <summary>
///   HL7 V2.2 Table 0104 - Version ID.
/// </summary>
public sealed record T0104_VersionID
   : TableEntry, IParsable<T0104_VersionID>,
      V2Point2.Tables.IT0104_VersionID,
      V2Point3.Tables.IT0104_VersionID,
      V2Point3Point1.Tables.IT0104_VersionID,
      V2Point4.Tables.IT0104_VersionID,
      V2Point5.Tables.IT0104_VersionID,
      V2Point5Point1.Tables.IT0104_VersionID,
      V2Point6.Tables.IT0104_VersionID,
      V2Point7.Tables.IT0104_VersionID,
      V2Point7Point1.Tables.IT0104_VersionID,
      V2Point8.Tables.IT0104_VersionID
{
   private T0104_VersionID(
      String value = "",
      Presence presence = Presence.Present)
      : base(value, presence) { }

   public static readonly T0104_VersionID NotPresent = new(presence: Presence.NotPresent);
   public static readonly T0104_VersionID PresentButNull = new(presence: Presence.PresentButNull);

   public static readonly T0104_VersionID V2Point0 = new("2.0");
   public static readonly T0104_VersionID V2Point0D = new("2.0D");
   public static readonly T0104_VersionID V2Point1 = new("2.1");
   public static readonly T0104_VersionID V2Point2 = new("2.2");
   public static readonly T0104_VersionID V2Point3 = new("2.3");
   public static readonly T0104_VersionID V2Point3Point1 = new("2.3.1");
   public static readonly T0104_VersionID V2Point4 = new("2.4");
   public static readonly T0104_VersionID V2Point5 = new("2.5");
   public static readonly T0104_VersionID V2Point5Point1 = new("2.5.1");
   public static readonly T0104_VersionID V2Point6 = new("2.6");
   public static readonly T0104_VersionID V2Point7 = new("2.7");
   public static readonly T0104_VersionID V2Point7Point1 = new("2.7.1");
   public static readonly T0104_VersionID V2Point8 = new("2.8");

   public static Boolean operator <(T0104_VersionID left, T0104_VersionID right) => String.CompareOrdinal(left, right) < 0;

   public static Boolean operator <=(T0104_VersionID left, T0104_VersionID right) => String.CompareOrdinal(left, right) <= 0;

   public static Boolean operator >(T0104_VersionID left, T0104_VersionID right) => String.CompareOrdinal(left, right) > 0;

   public static Boolean operator >=(T0104_VersionID left, T0104_VersionID right) => String.CompareOrdinal(left, right) >= 0;

   public static T0104_VersionID Parse(
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

      var fieldContents = fieldEnumerator.Current;
      var field = FromString(fieldContents.ToString());

      if (field == NotPresent)
      {
         log.LogUnrecognizedTableValue(lineNumber, fieldSpecification, fieldContents);
         return field;
      }

      log.LogFieldPresent(lineNumber, fieldSpecification, fieldContents);
      return field;
   }

   private static T0104_VersionID FromString(String str)
      => str switch
      {
         "2.0" => V2Point0,
         "2.0D" => V2Point0D,
         "2.1" => V2Point1,
         "2.2" => V2Point2,
         "2.3" => V2Point3,
         "2.3.1" => V2Point3Point1,
         "2.4" => V2Point4,
         "2.5" => V2Point5,
         "2.5.1" => V2Point5Point1,
         "2.6" => V2Point6,
         "2.7" => V2Point7,
         "2.7.1" => V2Point7Point1,
         "2.8" => V2Point8,
         _ => NotPresent
      };
}
