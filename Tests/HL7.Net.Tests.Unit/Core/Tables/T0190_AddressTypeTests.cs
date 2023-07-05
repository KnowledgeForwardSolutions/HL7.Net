// Ignore Spelling: sut

namespace HL7.Net.Tests.Unit.Core.Tables;

public class T0190_AddressTypeTests : IParsableTests<T0190_AddressType>
{
   public T0190_AddressTypeTests() : base(
      T0190_AddressType.NotPresent,
      T0190_AddressType.PresentButNull,
      new FieldSpecification(
         "AD",
         1,
         "AddressType",
         1,
         HL7Datatype.T0190_AddressType,
         Optionality.Optional,
         "N"),
      EncodingDetails.DefaultEncodingDetails) { }

   #region Defined Instance Tests
   // ==========================================================================
   // ==========================================================================

   public static TheoryData<T0190_AddressType, String, Presence> InstanceTestData = new()
   {
      { T0190_AddressType.NotPresent, String.Empty, Presence.NotPresent },
      { T0190_AddressType.PresentButNull, String.Empty, Presence.PresentButNull },
      { T0190_AddressType.Business, "B", Presence.Present },
      { T0190_AddressType.CurrentOrTemporary, "C", Presence.Present },
      { T0190_AddressType.Home, "H", Presence.Present },
      { T0190_AddressType.Mailing, "M", Presence.Present },
      { T0190_AddressType.Office, "O", Presence.Present },
      { T0190_AddressType.Permanent, "P", Presence.Present },
      { T0190_AddressType.CountryOfOrigin, "F", Presence.Present },
      { T0190_AddressType.BadAddress, "BA", Presence.Present },
      { T0190_AddressType.BirthDeliveryLocation, "BDL", Presence.Present },
      { T0190_AddressType.ResidenceAtBirth, "BR", Presence.Present },
      { T0190_AddressType.Legal, "L", Presence.Present },
      { T0190_AddressType.Birth_Nee, "N", Presence.Present },
      { T0190_AddressType.RegistryHome, "RH", Presence.Present },
      { T0190_AddressType.BillingAddress, "BI", Presence.Present },
      { T0190_AddressType.ServiceLocation, "S", Presence.Present },
      { T0190_AddressType.ShippingAddress, "SH", Presence.Present },
      { T0190_AddressType.Vacation, "V", Presence.Present },
      { T0190_AddressType.TubeAddress, "TM", Presence.Present },
   };

   [Theory]
   [MemberData(nameof(InstanceTestData))]
   public void T0190_AddressType_DefinedInstance_ShouldHaveExpectedValueAndPresence(
      T0190_AddressType sut,
      String expectedValue,
      Presence expectedPresence)
   {
      // Assert.
      sut.Value.Should().Be(expectedValue);
      sut.Presence.Should().Be(expectedPresence);
   }

   #endregion

   #region Implicit String Converter Tests
   // ==========================================================================
   // ==========================================================================

   [Fact]
   public void T0190_AddressType_StringConverter_ShouldReturnExpectedValue()
   {
      // Arrange.
      var sut = T0190_AddressType.BirthDeliveryLocation;

      // Act.
      String str = sut;

      // Assert.
      str.Should().Be(sut.Value);
   }

   #endregion

   #region Parse Method Tests
   // ==========================================================================
   // ==========================================================================

   public static Dictionary<T0190_AddressType, T0104_VersionID> TableEntriesByVersion = new()
   {
      { T0190_AddressType.Business, T0104_VersionID.V2Point2 },
      { T0190_AddressType.CurrentOrTemporary, T0104_VersionID.V2Point2 },
      { T0190_AddressType.Home, T0104_VersionID.V2Point2 },
      { T0190_AddressType.Mailing, T0104_VersionID.V2Point2 },
      { T0190_AddressType.Office, T0104_VersionID.V2Point2 },
      { T0190_AddressType.Permanent, T0104_VersionID.V2Point2 },
      { T0190_AddressType.CountryOfOrigin, T0104_VersionID.V2Point3 },
      { T0190_AddressType.BadAddress, T0104_VersionID.V2Point3Point1 },
      { T0190_AddressType.BirthDeliveryLocation, T0104_VersionID.V2Point3Point1 },
      { T0190_AddressType.ResidenceAtBirth, T0104_VersionID.V2Point3Point1 },
      { T0190_AddressType.Legal, T0104_VersionID.V2Point3Point1 },
      { T0190_AddressType.Birth_Nee, T0104_VersionID.V2Point3Point1 },
      { T0190_AddressType.RegistryHome, T0104_VersionID.V2Point3Point1 },
      { T0190_AddressType.BillingAddress, T0104_VersionID.V2Point6 },
      { T0190_AddressType.ServiceLocation, T0104_VersionID.V2Point6 },
      { T0190_AddressType.ShippingAddress, T0104_VersionID.V2Point6 },
      { T0190_AddressType.Vacation, T0104_VersionID.V2Point6 },
      { T0190_AddressType.TubeAddress, T0104_VersionID.V2Point7 },
   };

   public static TableEntryByVersionMatrixTheoryData<T0190_AddressType> V2Point2ParseSuccessData = new(T0104_VersionID.V2Point2, TableEntriesByVersion);

   public static TableEntryByVersionMatrixTheoryData<T0190_AddressType> V2Point3ParseSuccessData = new(T0104_VersionID.V2Point3, TableEntriesByVersion);

   public static TableEntryByVersionMatrixTheoryData<T0190_AddressType> V2Point3Point1ParseSuccessData = new(T0104_VersionID.V2Point3Point1, TableEntriesByVersion);

   public static TableEntryByVersionMatrixTheoryData<T0190_AddressType> V2Point4ParseSuccessData = new(T0104_VersionID.V2Point4, TableEntriesByVersion);

   public static TableEntryByVersionMatrixTheoryData<T0190_AddressType> V2Point5ParseSuccessData = new(T0104_VersionID.V2Point5, TableEntriesByVersion);

   public static TableEntryByVersionMatrixTheoryData<T0190_AddressType> V2Point5Point1ParseSuccessData = new(T0104_VersionID.V2Point5Point1, TableEntriesByVersion);

   public static TableEntryByVersionMatrixTheoryData<T0190_AddressType> V2Point6ParseSuccessData = new(T0104_VersionID.V2Point6, TableEntriesByVersion);

   public static TableEntryByVersionMatrixTheoryData<T0190_AddressType> V2Point7ParseSuccessData = new(T0104_VersionID.V2Point7, TableEntriesByVersion);

   public static TableEntryByVersionMatrixTheoryData<T0190_AddressType> V2Point7Point1ParseSuccessData = new(T0104_VersionID.V2Point7Point1, TableEntriesByVersion);

   public static TableEntryByVersionMatrixTheoryData<T0190_AddressType> V2Point8ParseSuccessData = new(T0104_VersionID.V2Point8, TableEntriesByVersion);

   public static TableEntryByVersionMatrixTheoryData<T0190_AddressType> V2Point2ParseInvalidForVersionData = new(T0104_VersionID.V2Point2, TableEntriesByVersion, false);

   public static TableEntryByVersionMatrixTheoryData<T0190_AddressType> V2Point3ParseInvalidForVersionData = new(T0104_VersionID.V2Point3, TableEntriesByVersion, false);

   public static TableEntryByVersionMatrixTheoryData<T0190_AddressType> V2Point3Point1ParseInvalidForVersionData = new(T0104_VersionID.V2Point3Point1, TableEntriesByVersion, false);

   public static TableEntryByVersionMatrixTheoryData<T0190_AddressType> V2Point4ParseInvalidForVersionData = new(T0104_VersionID.V2Point4, TableEntriesByVersion, false);

   public static TableEntryByVersionMatrixTheoryData<T0190_AddressType> V2Point5ParseInvalidForVersionData = new(T0104_VersionID.V2Point5, TableEntriesByVersion, false);

   public static TableEntryByVersionMatrixTheoryData<T0190_AddressType> V2Point5Point1ParseInvalidForVersionData = new(T0104_VersionID.V2Point5Point1, TableEntriesByVersion, false);

   public static TableEntryByVersionMatrixTheoryData<T0190_AddressType> V2Point6ParseInvalidForVersionData = new(T0104_VersionID.V2Point6, TableEntriesByVersion, false);

   public static TableEntryByVersionMatrixTheoryData<T0190_AddressType> V2Point7ParseInvalidForVersionData = new(T0104_VersionID.V2Point7, TableEntriesByVersion, false);

   public static TableEntryByVersionMatrixTheoryData<T0190_AddressType> V2Point7Point1ParseInvalidForVersionData = new(T0104_VersionID.V2Point7Point1, TableEntriesByVersion, false);

   public static TableEntryByVersionMatrixTheoryData<T0190_AddressType> V2Point8ParseInvalidForVersionData = new(T0104_VersionID.V2Point8, TableEntriesByVersion, false);

   [Theory]
   [MemberData(nameof(V2Point2ParseSuccessData))]
   [MemberData(nameof(V2Point3ParseSuccessData))]
   [MemberData(nameof(V2Point3Point1ParseSuccessData))]
   [MemberData(nameof(V2Point4ParseSuccessData))]
   [MemberData(nameof(V2Point5ParseSuccessData))]
   [MemberData(nameof(V2Point5Point1ParseSuccessData))]
   [MemberData(nameof(V2Point6ParseSuccessData))]
   [MemberData(nameof(V2Point7ParseSuccessData))]
   [MemberData(nameof(V2Point7Point1ParseSuccessData))]
   [MemberData(nameof(V2Point8ParseSuccessData))]
   public void T0190_AddressType_Parse_ShouldReturnExpectedTableValue_WhenFieldContainsAValidTableEntryForTheSpecifiedVersion(
      T0104_VersionID versionID,
      T0190_AddressType expected)
   {
      // Arrange.
      var fieldContents = expected.Value;
      var line = $"AD^{fieldContents}^asdf".AsSpan();
      var fieldEnumerator = line.ToFields(_encodingDetails.ComponentSeparator, _encodingDetails.EscapeCharacter);
      fieldEnumerator.MoveNext();
      var log = new ProcessingLog();

      // Act.
      var field = T0190_AddressType.Parse(
         ref fieldEnumerator,
         _fieldSpecification,
         versionID,
         _lineNumber,
         log);

      // Assert.
      field.Should().Be(expected);
   }

   [Fact]
   public void T0190_AddressType_Parse_ShouldLogFieldPresent_WhenFieldContainsAValidTableEntry()
   {
      // Arrange.
      var fieldContents = T0190_AddressType.TubeAddress.Value;
      var line = $"AD^{fieldContents}^asdf".AsSpan();
      var fieldEnumerator = line.ToFields(_encodingDetails.ComponentSeparator, _encodingDetails.EscapeCharacter);
      fieldEnumerator.MoveNext();
      var versionID = T0104_VersionID.V2Point8;
      var log = new ProcessingLog();

      var expectedLogEntry = LogEntry.GetFieldPresentEntry(
         _lineNumber,
         _fieldSpecification.FieldDescription,
         fieldContents);

      // Act.
      _ = T0190_AddressType.Parse(
         ref fieldEnumerator,
         _fieldSpecification,
         versionID,
         _lineNumber,
         log);

      // Assert.
      log.Should().HaveCount(1);
      log.First().Should().Be(expectedLogEntry);
   }

   [Theory]
   [MemberData(nameof(V2Point2ParseInvalidForVersionData))]
   [MemberData(nameof(V2Point3ParseInvalidForVersionData))]
   [MemberData(nameof(V2Point3Point1ParseInvalidForVersionData))]
   [MemberData(nameof(V2Point4ParseInvalidForVersionData))]
   [MemberData(nameof(V2Point5ParseInvalidForVersionData))]
   [MemberData(nameof(V2Point5Point1ParseInvalidForVersionData))]
   [MemberData(nameof(V2Point6ParseInvalidForVersionData))]
   [MemberData(nameof(V2Point7ParseInvalidForVersionData))]
   [MemberData(nameof(V2Point7Point1ParseInvalidForVersionData))]
   [MemberData(nameof(V2Point8ParseInvalidForVersionData))]
   public void T0104_VersionID_Parse_ShouldReturnNotPresentInstance_WhenFieldContainsAnInvalidValueForTheSpecifiedVersion(
      T0104_VersionID versionID,
      T0190_AddressType expected)      
   {
      // Arrange.
      var fieldContents = expected.Value;
      var line = $"AD^{fieldContents}^asdf".AsSpan();
      var fieldEnumerator = line.ToFields(_encodingDetails.ComponentSeparator, _encodingDetails.EscapeCharacter);
      fieldEnumerator.MoveNext();
      var log = new ProcessingLog();

      // Act.
      var field = T0190_AddressType.Parse(
         ref fieldEnumerator,
         _fieldSpecification,
         versionID,
         _lineNumber,
         log);

      // Assert.
      field.Should().Be(T0190_AddressType.NotPresent);
   }

   [Fact]
   public void T0190_AddressType_Parse_ShouldLogError_WhenFieldContainsAnInvalidValue()
   {
      // Arrange.
      var fieldContents = T0190_AddressType.Vacation.Value;
      var line = $"AD^{fieldContents}^asdf".AsSpan();
      var fieldEnumerator = line.ToFields(_encodingDetails.ComponentSeparator, _encodingDetails.EscapeCharacter);
      fieldEnumerator.MoveNext();
      var versionID = T0104_VersionID.V2Point4;
      var log = new ProcessingLog();

      var expectedLogEntry = LogEntry.GetUnrecognizedTableValueEntry(
         _lineNumber,
         _fieldSpecification.Datatype,
         _fieldSpecification.FieldDescription,
         fieldContents.AsSpan());

      // Act.
      _ = T0190_AddressType.Parse(
         ref fieldEnumerator,
         _fieldSpecification,
         versionID,
         _lineNumber,
         log);

      // Assert.
      log.Should().HaveCount(1);
      log.First().Should().Be(expectedLogEntry);
   }

   #endregion
}
