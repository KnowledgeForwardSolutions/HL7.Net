namespace HL7.Net.V2Point2.Tables;

public interface IT0190_AddressType
{
   public static readonly IT0190_AddressType NotPresent = T0190_AddressType.NotPresent;
   public static readonly IT0190_AddressType PresentButNull = T0190_AddressType.PresentButNull;

   public static readonly IT0190_AddressType Business = T0190_AddressType.Business;
   public static readonly IT0190_AddressType CurrentOrTemporary = T0190_AddressType.CurrentOrTemporary;
   public static readonly IT0190_AddressType Home = T0190_AddressType.Home;
   public static readonly IT0190_AddressType Mailing = T0190_AddressType.Mailing;
   public static readonly IT0190_AddressType Office = T0190_AddressType.Office;
   public static readonly IT0190_AddressType Permanent = T0190_AddressType.Permanent;
}
