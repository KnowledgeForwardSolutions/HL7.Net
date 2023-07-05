namespace HL7.Net.Tests.Unit.Core.Tables;

public class Other_TimestampDegreeOfPrecisionTests : IParsableTests<Other_TimestampDegreeOfPrecision>
{
   public Other_TimestampDegreeOfPrecisionTests() : base(
      Other_TimestampDegreeOfPrecision.NotPresent,
      Other_TimestampDegreeOfPrecision.PresentButNull,
      new FieldSpecification(
         "TS",
         1,
         "DegreeOfPrecision",
         1,
         HL7Datatype.Other_TimestampDegreeOfPrecision,
         Optionality.Optional,
         "N"),
      EncodingDetails.DefaultEncodingDetails)
   { }
}

