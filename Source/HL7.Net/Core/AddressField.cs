namespace HL7.Net.Core;

/// <summary>
///   A composite field containing an address. Implements HL7 V2.2 spec section
///   2.8.10.10.
/// </summary>
public sealed record AddressField : IPresence
{
   internal AddressField() { }

   private static readonly List<FieldSpecification> _fieldSpecifications = new()
   {
      new FieldSpecification("PN", 1, "Street Address", 48, HL7Datatype.ST_String, Optionality.Optional, "N"),
      new FieldSpecification("PN", 2, "Other Designation", 48, HL7Datatype.ST_String, Optionality.Optional, "N"),
      new FieldSpecification("PN", 3, "City", 48, HL7Datatype.ST_String, Optionality.Optional, "N"),
      new FieldSpecification("PN", 4, "State or Province", 2, HL7Datatype.ST_String, Optionality.Optional, "N"),
      new FieldSpecification("PN", 5, "Zip or Postal Code", 10, HL7Datatype.ST_String, Optionality.Optional, "N"),
      new FieldSpecification("PN", 6, "Country", 48, HL7Datatype.ST_String, Optionality.Optional, "N"),
      new FieldSpecification("PN", 7, "Type", 48, HL7Datatype.ST_String, Optionality.Optional, "N"),
      new FieldSpecification("PN", 8, "Other Geographic Designation", 48, HL7Datatype.ST_String, Optionality.Optional, "N"),
   };

   /// <summary>
   ///   Represents an address field that is not present.
   /// </summary>
   public static readonly AddressField NotPresent = new();

   /// <summary>
   ///   Represents an address field that is present but is null.
   /// </summary>
   public static readonly AddressField PresentButNull = new()
   {
      Presence = Presence.PresentButNull
   };

   /// <inheritdoc/>
   public Presence Presence { get; private set; } = Presence.NotPresent;

   /// <summary>
   ///   The street address.
   /// </summary>
   public StringField StreetAddress { get; private set; } = StringField.NotPresent;

   /// <summary>
   ///   Additional street address data.
   /// </summary>
   public StringField OtherDesignation { get; private set; } = StringField.NotPresent;

   /// <summary>
   ///   The city.
   /// </summary>
   public StringField City { get; private set; } = StringField.NotPresent;

   /// <summary>
   ///   The state or province designation.
   /// </summary>
   public StringField StateOrProvince { get; private set; } = StringField.NotPresent;

   /// <summary>
   ///   The US ZipCode or other national postal code.
   /// </summary>
   public StringField ZipOrPostalCode { get; private set; } = StringField.NotPresent;

   /// <summary>
   ///   The country.
   /// </summary>
   public StringField Country { get; private set; } = StringField.NotPresent;

   /// <summary>
   ///   The address type.
   /// </summary>
   public StringField Type { get; private set; } = StringField.NotPresent;

   /// <summary>
   ///   Additional address data (example, bioregion, SMSA, etc.).
   /// </summary>
   public StringField OtherGeographicDesignation { get; private set; } = StringField.NotPresent;

}
