// Ignore Spelling: Nee

namespace HL7.Net.Core.Tables;

/// <summary>
///   HL7 V2.2 Table 0190 - Address Type.
/// </summary>
public record T0190_AddressType 
   : TableEntry, IParsable<T0190_AddressType>,
      V2Point2.Tables.IT0190_AddressType,
      V2Point3.Tables.IT0190_AddressType,
      V2Point3Point1.Tables.IT0190_AddressType,
      V2Point4.Tables.IT0190_AddressType,
      V2Point5.Tables.IT0190_AddressType,
      V2Point5Point1.Tables.IT0190_AddressType,
      V2Point6.Tables.IT0190_AddressType,
      V2Point7.Tables.IT0190_AddressType,
      V2Point7Point1.Tables.IT0190_AddressType,
      V2Point8.Tables.IT0190_AddressType
{
   private T0190_AddressType(
      String value = "",
      Presence presence = Presence.Present)
      : base(value, presence) { }

   public static readonly T0190_AddressType NotPresent = new(presence: Presence.NotPresent);
   public static readonly T0190_AddressType PresentButNull = new(presence: Presence.PresentButNull);

   // V2.2
   public static readonly T0190_AddressType Business = new("B");
   public static readonly T0190_AddressType CurrentOrTemporary = new("C");
   public static readonly T0190_AddressType Home = new("H");
   public static readonly T0190_AddressType Mailing = new("M");
   public static readonly T0190_AddressType Office = new("O");
   public static readonly T0190_AddressType Permanent = new("P");

   // V2.3
   public static readonly T0190_AddressType CountryOfOrigin = new("F");

   // V2.3.1
   public static readonly T0190_AddressType BadAddress = new("BA");
   public static readonly T0190_AddressType BirthDeliveryLocation = new("BDL");
   public static readonly T0190_AddressType ResidenceAtBirth = new("BR");
   public static readonly T0190_AddressType Legal = new("L");
   public static readonly T0190_AddressType Birth_Nee = new("N");
   public static readonly T0190_AddressType RegistryHome = new("RH");

   // V2.6
   public static readonly T0190_AddressType BillingAddress = new("BI");
   public static readonly T0190_AddressType ServiceLocation = new("S");
   public static readonly T0190_AddressType ShippingAddress = new("SH");
   public static readonly T0190_AddressType Vacation = new("V");

   // V2.7
   public static readonly T0190_AddressType TubeAddress = new("TM");

   //public static implicit operator String(T0190_AddressType field) => field.Value;

   public static T0190_AddressType Parse(
      ref FieldEnumerator fieldEnumerator,
      FieldSpecification fieldSpecification,
      T0104_VersionID versionID,
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
      T0190_AddressType field;
      if (versionID < T0104_VersionID.V2Point3)
      {
         field = FromStringV2Point2(fieldContents);
      }
      else if (versionID < T0104_VersionID.V2Point3Point1)
      {
         field = FromStringV2Point3(fieldContents);
      }
      else if (versionID < T0104_VersionID.V2Point6)
      {
         field = FromStringV2Point3Point1(fieldContents);
      }
      else if (versionID < T0104_VersionID.V2Point7)
      {
         field = FromStringV2Point6(fieldContents);
      }
      else
      {
         field = FromStringV2Point7(fieldContents);
      }

      if (field == NotPresent)
      {
         log.LogUnrecognizedTableValue(lineNumber, fieldSpecification, fieldContents);
         return field;
      }

      log.LogFieldPresent(lineNumber, fieldSpecification, fieldContents);
      return field;
   }

   private static T0190_AddressType FromStringV2Point2(String str)
      => str switch
      {
         "B" => Business,
         "C" => CurrentOrTemporary,
         "H" => Home,
         "M" => Mailing,
         "O" => Office,
         "P" => Permanent,
         _ => NotPresent
      };

   private static T0190_AddressType FromStringV2Point3(String str)
      => str switch
      {
         "F" => CountryOfOrigin,
         _ => FromStringV2Point2(str)
      };

   private static T0190_AddressType FromStringV2Point3Point1(String str)
      => str switch
      {
         "BA" => BadAddress,
         "BDL" => BirthDeliveryLocation,
         "BR" => ResidenceAtBirth,
         "L" => Legal,
         "N" => Birth_Nee,
         "RH" => RegistryHome,
         _ => FromStringV2Point3(str)
      };

   private static T0190_AddressType FromStringV2Point6(String str)
      => str switch
      {
         "BI" => BillingAddress,
         "S" => ServiceLocation,
         "SH" => ShippingAddress,
         "V" => Vacation,
         _ => FromStringV2Point3Point1(str)
      };

   private static T0190_AddressType FromStringV2Point7(String str)
      => str switch
      {
         "TM" => TubeAddress,
         _ => FromStringV2Point6(str)
      };
}
