﻿// Ignore Spelling: Nee

namespace HL7.Net.V2Point7.Tables;

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

   public static readonly IT0190_AddressType CountryOfOrigin = T0190_AddressType.CountryOfOrigin;

   public static readonly IT0190_AddressType BadAddress = T0190_AddressType.BadAddress;
   public static readonly IT0190_AddressType BirthDeliveryLocation = T0190_AddressType.BirthDeliveryLocation;
   public static readonly IT0190_AddressType ResidenceAtBirth = T0190_AddressType.ResidenceAtBirth;
   public static readonly IT0190_AddressType Legal = T0190_AddressType.Legal;
   public static readonly IT0190_AddressType Birth_Nee = T0190_AddressType.Birth_Nee;
   public static readonly IT0190_AddressType RegistryHome = T0190_AddressType.RegistryHome;

   public static readonly IT0190_AddressType BillingAddress = T0190_AddressType.BillingAddress;
   public static readonly IT0190_AddressType ServiceLocation = T0190_AddressType.ServiceLocation;
   public static readonly IT0190_AddressType ShippingAddress = T0190_AddressType.ShippingAddress;
   public static readonly IT0190_AddressType Vacation = T0190_AddressType.Vacation;

   public static readonly IT0190_AddressType TubeAddress = T0190_AddressType.TubeAddress;
}
