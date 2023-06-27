namespace HL7.Net.Core;

/// <summary>
///   A composite field containing a timestamp. Implements HL7 V2.2 spec section
///   2.8.10.7.
/// </summary>
public sealed record TimestampField
{
   private static readonly List<FieldSpecification> _fieldSpecifications = new()
   {
      new FieldSpecification("TS", 1, "DateTime", 26, HL7Datatype.DateTimeComponent, Optionality.Required, "N"),
      new FieldSpecification("TS", 2, "DegreeOfPrecision", 1, HL7Datatype.TimestampDegreeOfPrecisionComponent, Optionality.Optional, "N"),
   };
}
