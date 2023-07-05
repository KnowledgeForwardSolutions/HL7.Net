// Ignore Spelling: sut

namespace HL7.Net.Tests.Unit.Core.Tables;

public class T0104_VersionIDTests : IParsableTests<T0104_VersionID>
{
   public T0104_VersionIDTests() : base(
      T0104_VersionID.NotPresent,
      T0104_VersionID.PresentButNull,
      new FieldSpecification(
         "MSH",
         1,
         "Version ID",
         1,
         HL7Datatype.T0104_VersionID,
         Optionality.Optional,
         "N"),
      EncodingDetails.DefaultEncodingDetails)
   { }

   #region Defined Instance Tests
   // ==========================================================================
   // ==========================================================================

   public static TheoryData<T0104_VersionID, String, Presence> InstanceTestData = new()
   {
      { T0104_VersionID.NotPresent, String.Empty, Presence.NotPresent },
      { T0104_VersionID.PresentButNull, String.Empty, Presence.PresentButNull },
      { T0104_VersionID.V2Point0, "2.0", Presence.Present },
      { T0104_VersionID.V2Point0D, "2.0D", Presence.Present },
      { T0104_VersionID.V2Point1, "2.1", Presence.Present },
      { T0104_VersionID.V2Point2, "2.2", Presence.Present },
      { T0104_VersionID.V2Point3, "2.3", Presence.Present },
      { T0104_VersionID.V2Point3Point1, "2.3.1", Presence.Present },
      { T0104_VersionID.V2Point4, "2.4", Presence.Present },
      { T0104_VersionID.V2Point5, "2.5", Presence.Present },
      { T0104_VersionID.V2Point5Point1, "2.5.1", Presence.Present },
      { T0104_VersionID.V2Point6, "2.6", Presence.Present },
      { T0104_VersionID.V2Point7, "2.7", Presence.Present },
      { T0104_VersionID.V2Point7Point1, "2.7.1", Presence.Present },
      { T0104_VersionID.V2Point8, "2.8", Presence.Present },
   };

   [Theory]
   [MemberData(nameof(InstanceTestData))]
   public void T0104_VersionID_DefinedInstance_ShouldHaveExpectedValueAndPresence(
      T0104_VersionID sut,
      String expectedValue,
      Presence expectedPresence)
   {
      // Assert.
      sut.Value.Should().Be(expectedValue);
      sut.Presence.Should().Be(expectedPresence);
   }

   #endregion

   #region Operator Tests
   // ==========================================================================
   // ==========================================================================

   public static TheoryData<T0104_VersionID, T0104_VersionID> LessThanData = new()
   {
      { T0104_VersionID.V2Point0, T0104_VersionID.V2Point0D },
      { T0104_VersionID.V2Point0D, T0104_VersionID.V2Point1 },
      { T0104_VersionID.V2Point1, T0104_VersionID.V2Point2 },
      { T0104_VersionID.V2Point2, T0104_VersionID.V2Point3 },
      { T0104_VersionID.V2Point3, T0104_VersionID.V2Point3Point1 },
      { T0104_VersionID.V2Point3Point1, T0104_VersionID.V2Point4 },
      { T0104_VersionID.V2Point4, T0104_VersionID.V2Point5 },
      { T0104_VersionID.V2Point5, T0104_VersionID.V2Point5Point1 },
      { T0104_VersionID.V2Point5Point1, T0104_VersionID.V2Point6 },
      { T0104_VersionID.V2Point6, T0104_VersionID.V2Point7 },
      { T0104_VersionID.V2Point7, T0104_VersionID.V2Point7Point1 },
      { T0104_VersionID.V2Point7Point1, T0104_VersionID.V2Point8 },
   };

   public static TheoryData<T0104_VersionID, T0104_VersionID> EqualData = new()
   {
      { T0104_VersionID.V2Point0, T0104_VersionID.V2Point0 },
      { T0104_VersionID.V2Point0D, T0104_VersionID.V2Point0D },
      { T0104_VersionID.V2Point1, T0104_VersionID.V2Point1 },
      { T0104_VersionID.V2Point2, T0104_VersionID.V2Point2 },
      { T0104_VersionID.V2Point3, T0104_VersionID.V2Point3 },
      { T0104_VersionID.V2Point3Point1, T0104_VersionID.V2Point3Point1 },
      { T0104_VersionID.V2Point4, T0104_VersionID.V2Point4 },
      { T0104_VersionID.V2Point5, T0104_VersionID.V2Point5 },
      { T0104_VersionID.V2Point5Point1, T0104_VersionID.V2Point5Point1 },
      { T0104_VersionID.V2Point6, T0104_VersionID.V2Point6 },
      { T0104_VersionID.V2Point7, T0104_VersionID.V2Point7 },
      { T0104_VersionID.V2Point7Point1, T0104_VersionID.V2Point7Point1 },
      { T0104_VersionID.V2Point8, T0104_VersionID.V2Point8 },
   };

   public static TheoryData<T0104_VersionID, T0104_VersionID> GreaterThanData = new()
   {
      { T0104_VersionID.V2Point0D, T0104_VersionID.V2Point0 },    
      { T0104_VersionID.V2Point1, T0104_VersionID.V2Point0D },      
      { T0104_VersionID.V2Point2, T0104_VersionID.V2Point1 },       
      { T0104_VersionID.V2Point3, T0104_VersionID.V2Point2 },       
      { T0104_VersionID.V2Point3Point1, T0104_VersionID.V2Point3 },       
      { T0104_VersionID.V2Point4, T0104_VersionID.V2Point3Point1 }, 
      { T0104_VersionID.V2Point5, T0104_VersionID.V2Point4 },       
      { T0104_VersionID.V2Point5Point1, T0104_VersionID.V2Point5 },       
      { T0104_VersionID.V2Point6, T0104_VersionID.V2Point5Point1 }, 
      { T0104_VersionID.V2Point7, T0104_VersionID.V2Point6 },       
      { T0104_VersionID.V2Point7Point1, T0104_VersionID.V2Point7 },
      { T0104_VersionID.V2Point8, T0104_VersionID.V2Point7Point1 }, 
   };

   [Theory]
   [MemberData(nameof(LessThanData))]
   public void T00104_VersionID_LessThanOperator_ShouldReturnTrue_WhenLeftIsLessThanRight(
      T0104_VersionID left,
      T0104_VersionID right)
      => (left < right).Should().BeTrue();

   [Theory]
   [MemberData(nameof(EqualData))]
   [MemberData(nameof(GreaterThanData))]
   public void T00104_VersionID_LessThanOperator_ShouldReturnFalse_WhenLeftIsGreaterThanOrEqualToRight(
      T0104_VersionID left,
      T0104_VersionID right)
      => (left < right).Should().BeFalse();

   [Theory]
   [MemberData(nameof(LessThanData))]
   [MemberData(nameof(EqualData))]
   public void T00104_VersionID_LessThanOrEqualOperator_ShouldReturnTrue_WhenLeftIsLessThanOrEqualToRight(
      T0104_VersionID left,
      T0104_VersionID right)
      => (left <= right).Should().BeTrue();

   [Theory]
   [MemberData(nameof(GreaterThanData))]
   public void T00104_VersionID_LessThanOrEqualOperator_ShouldReturnFalse_WhenLeftIsGreaterThanRight(
      T0104_VersionID left,
      T0104_VersionID right)
      => (left <= right).Should().BeFalse();

   [Theory]
   [MemberData(nameof(GreaterThanData))]
   public void T00104_VersionID_GreaterThanOperator_ShouldReturnTrue_WhenLeftIsGreaterThanRight(
      T0104_VersionID left,
      T0104_VersionID right)
      => (left > right).Should().BeTrue();

   [Theory]
   [MemberData(nameof(LessThanData))]
   [MemberData(nameof(EqualData))]
   public void T00104_VersionID_GreaterThanOperator_ShouldReturnFalse_WhenLeftIsLessThanOrEqualToRight(
      T0104_VersionID left,
      T0104_VersionID right)
      => (left > right).Should().BeFalse();

   [Theory]
   [MemberData(nameof(GreaterThanData))]
   [MemberData(nameof(EqualData))]
   public void T00104_VersionID_GreaterThanOrEqualOperator_ShouldReturnTrue_WhenLeftIsGreaterThanOrEqualToRight(
      T0104_VersionID left,
      T0104_VersionID right)
      => (left >= right).Should().BeTrue();

   [Theory]
   [MemberData(nameof(LessThanData))]
   public void T00104_VersionID_GreaterThanOrEqualOperator_ShouldReturnFalse_WhenLeftIsLessThanRight(
      T0104_VersionID left,
      T0104_VersionID right)
      => (left >= right).Should().BeFalse();

   #endregion

   #region Implicit String Converter Tests
   // ==========================================================================
   // ==========================================================================

   [Fact]
   public void T0104_VersionID_StringConverter_ShouldReturnExpectedValue()
   {
      // Arrange.
      var sut = T0104_VersionID.V2Point3Point1;

      // Act.
      String str = sut;

      // Assert.
      str.Should().Be(sut.Value);
   }

   #endregion

   #region Parse Method Tests
   // ==========================================================================
   // ==========================================================================

   public static TheoryData<T0104_VersionID> ParseSuccessData = new()
   {
      { T0104_VersionID.V2Point0 },
      { T0104_VersionID.V2Point0D },
      { T0104_VersionID.V2Point1 },
      { T0104_VersionID.V2Point2 },
      { T0104_VersionID.V2Point3 },
      { T0104_VersionID.V2Point3Point1 },
      { T0104_VersionID.V2Point4 },
      { T0104_VersionID.V2Point5 },
      { T0104_VersionID.V2Point5Point1 },
      { T0104_VersionID.V2Point6 },
      { T0104_VersionID.V2Point7 },
      { T0104_VersionID.V2Point7Point1 },
      { T0104_VersionID.V2Point8 },
   };

   [Theory]
   [MemberData(nameof(ParseSuccessData))]
   public void T0104_VersionID_Parse_ShouldReturnExpectedTableValue_WhenFieldContainsAValidTableEntry(
      T0104_VersionID expected)
   {
      // Arrange.
      var fieldContents = expected.Value;
      var line = $"MSH|{fieldContents}|asdf".AsSpan();
      var fieldEnumerator = line.ToFields(_encodingDetails.FieldSeparator, _encodingDetails.EscapeCharacter);
      fieldEnumerator.MoveNext();
      var versionID = T0104_VersionID.V2Point3;
      var log = new ProcessingLog();

      // Act.
      var field = T0104_VersionID.Parse(
         ref fieldEnumerator,
         _fieldSpecification,
         versionID,
         _lineNumber,
         log);

      // Assert.
      field.Should().Be(expected);
   }

   [Fact]
   public void T0104_VersionID_Parse_ShouldLogFieldPresent_WhenFieldContainsAValidTableEntry()
   {
      // Arrange.
      var fieldContents = T0104_VersionID.V2Point3Point1.Value;
      var line = $"MSH|{fieldContents}|asdf".AsSpan();
      var fieldEnumerator = line.ToFields(_encodingDetails.FieldSeparator, _encodingDetails.EscapeCharacter);
      fieldEnumerator.MoveNext();
      var versionID = T0104_VersionID.V2Point8;
      var log = new ProcessingLog();

      var expectedLogEntry = LogEntry.GetFieldPresentEntry(
         _lineNumber,
         _fieldSpecification.FieldDescription,
         fieldContents);

      // Act.
      _ = T0104_VersionID.Parse(
         ref fieldEnumerator,
         _fieldSpecification,
         versionID,
         _lineNumber,
         log);

      // Assert.
      log.Should().HaveCount(1);
      log.First().Should().Be(expectedLogEntry);
   }

   [Fact]
   public void T0104_VersionID_Parse_ShouldReturnNotPresentInstance_WhenFieldContainsAnInvalidValue()
   {
      // Arrange.
      var fieldContents = "Zz";
      var line = $"MSH|{fieldContents}|asdf".AsSpan();
      var fieldEnumerator = line.ToFields(_encodingDetails.FieldSeparator, _encodingDetails.EscapeCharacter);
      fieldEnumerator.MoveNext();
      var versionID = T0104_VersionID.V2Point3Point1;
      var log = new ProcessingLog();

      // Act.
      var field = T0104_VersionID.Parse(
         ref fieldEnumerator,
         _fieldSpecification,
         versionID,
         _lineNumber,
         log);

      // Assert.
      field.Should().Be(T0104_VersionID.NotPresent);
   }

   [Fact]
   public void T0104_VersionID_Parse_ShouldLogError_WhenFieldContainsAnInvalidValue()
   {
      // Arrange.
      var fieldContents = "Zz";
      var line = $"MSH|{fieldContents}|asdf".AsSpan();
      var fieldEnumerator = line.ToFields(_encodingDetails.FieldSeparator, _encodingDetails.EscapeCharacter);
      fieldEnumerator.MoveNext();
      var versionID = T0104_VersionID.V2Point7;
      var log = new ProcessingLog();

      var expectedLogEntry = LogEntry.GetUnrecognizedTableValueEntry(
         _lineNumber,
         _fieldSpecification.Datatype,
         _fieldSpecification.FieldDescription,
         fieldContents.AsSpan());

      // Act.
      _ = T0104_VersionID.Parse(
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
