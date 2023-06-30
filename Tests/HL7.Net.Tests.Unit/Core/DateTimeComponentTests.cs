namespace HL7.Net.Tests.Unit.Core;

public class DateTimeComponentTests
{
   private static readonly EncodingDetails _encodingDetails = EncodingDetails.DefaultEncodingDetails;
   private static readonly FieldSpecification _fieldSpecification = new(
      "TS",
      1,
      "DateTime",
      26,
      HL7Datatype.DateTimeComponent,
      Optionality.Required,
      "N");
   private static readonly TimeSpan _defaultTimezoneOffset = new TimeSpan(-1, 0, 0);
   private const Int32 _lineNumber = 10;

   #region Internal Constructor Tests
   // ==========================================================================
   // ==========================================================================

   [Fact]
   public void DateTimeComponent_Constructor_ShouldCreateObject_WhenValueIsSupplied()
   {
      var value = new DateTimeOffset(new DateTime(2023, 6, 8, 19, 20, 21, 223, 4), new TimeSpan(6, 0, 0));

      // Act.
      var sut = new DateTimeComponent(value);

      // Assert.
      sut.Should().NotBeNull();
      sut.Value.Should().Be(value);
      sut.Presence.Should().Be(Presence.Present);
   }

   [Fact]
   public void DateTimeComponent_Constructor_ShouldCreateObject_WhenValueAndFieldPresenceAreSupplied()
   {
      DateTimeOffset? value = null!;

      // Act.
      var sut = new DateTimeComponent(value, Presence.NotPresent);

      // Assert.
      sut.Should().NotBeNull();
      sut.Value.Should().Be(value);
      sut.Presence.Should().Be(Presence.NotPresent);
   }

   #endregion

   #region Implicit DateTime Converter Tests
   // ==========================================================================
   // ==========================================================================

   [Fact]
   public void DateTimeComponent_ImplicitDecimalConverter_ShouldReturnExpectedValue_WhenFieldIsNotEmpty()
   {
      // Arrange.
      var value = new DateTimeOffset(new DateTime(2023, 6, 8, 19, 20, 21, 223, 4), new TimeSpan(6, 0, 0));
      var sut = new DateTimeComponent(value);

      // Act.
      DateTimeOffset? result = sut;

      // Assert.
      result.Should().Be(value);
   }

   [Fact]
   public void DateTimeComponent_ImplicitDecimalConverter_ShouldReturnExpectedValue_WhenFieldIsNotPresent()
   {
      // Arrange.
      var sut = DateTimeComponent.NotPresent;

      // Act.
      DateTimeOffset? result = sut;

      // Assert.
      result.Should().BeNull();
   }

   #endregion

   #region NotPresent Instance Tests
   // ==========================================================================
   // ==========================================================================

   [Fact]
   public void DateTimeComponent_NotPresent_ShouldReturnExpectedValue()
   {
      // Act.
      var sut = DateTimeComponent.NotPresent;

      // Assert.
      sut.Should().NotBeNull();
      sut.Value.Should().BeNull();
      sut.Presence.Should().Be(Presence.NotPresent);
   }

   #endregion

   #region PresentButNull Instance Tests
   // ==========================================================================
   // ==========================================================================

   [Fact]
   public void DateTimeComponent_PresentButNull_ShouldReturnExpectedValue()
   {
      // Act.
      var sut = DateTimeComponent.PresentButNull;

      // Assert.
      sut.Should().NotBeNull();
      sut.Value.Should().BeNull();
      sut.Presence.Should().Be(Presence.PresentButNull);
   }

   #endregion

   #region Parse Method Tests
   // ==========================================================================
   // ==========================================================================

   public static TheoryData<String, DateTimeOffset> ValidDateTimeData => new()
   {
      { "20230608", new DateTimeOffset(new DateTime(2023, 6, 8), _defaultTimezoneOffset) },
      { "20230608+0800", new DateTimeOffset(new DateTime(2023, 6, 8), new TimeSpan(8, 0, 0)) },
      { "202306081920", new DateTimeOffset(new DateTime(2023, 6, 8, 19, 20, 0), _defaultTimezoneOffset) },
      { "202306080000", new DateTimeOffset(new DateTime(2023, 6, 8, 0, 0, 0), _defaultTimezoneOffset) },
      { "202306081920-0330", new DateTimeOffset(new DateTime(2023, 6, 8, 19, 20, 0), new TimeSpan(-3, -30, 0)) },
      { "20230608192021", new DateTimeOffset(new DateTime(2023, 6, 8, 19, 20, 21), _defaultTimezoneOffset) },
      { "20230608192021+1100", new DateTimeOffset(new DateTime(2023, 6, 8, 19, 20, 21), new TimeSpan(11, 0, 0)) },
      { "20230608192021.2234", new DateTimeOffset(new DateTime(2023, 6, 8, 19, 20, 21, 223, 400), _defaultTimezoneOffset) },
      { "20230608192021.2234+0600", new DateTimeOffset(new DateTime(2023, 6, 8, 19, 20, 21, 223, 400), new TimeSpan(6, 0, 0)) },
      { "20230608192021.2234-0700", new DateTimeOffset(new DateTime(2023, 6, 8, 19, 20, 21, 223, 400), new TimeSpan(-7, 0, 0)) },
   };

   [Theory]
   [MemberData(nameof(ValidDateTimeData))]
   public void DateTimeComponent_Parse_ShouldReturnDateTimeValue_WhenFieldContainsValidTimestampValue(
      String fieldContents,
      DateTimeOffset expectedValue)
   {
      // Arrange.
      var line = $"{fieldContents}^M".AsSpan();
      var fieldEnumerator = line.ToFields(_encodingDetails.ComponentSeparator, _encodingDetails.EscapeCharacter);
      var log = new ProcessingLog();

      var expected = new DateTimeComponent(expectedValue);

      // Act.
      var field = DateTimeComponent.Parse(
         ref fieldEnumerator,
         _fieldSpecification,
         _defaultTimezoneOffset,
         _lineNumber,
         log);

      // Assert.
      field.Should().Be(expected);
   }

   [Fact]
   public void DateTimeComponent_Parse_ShouldLogExpectedEntry_WhenFieldContainsValidTimestampValue()
   {
      // Arrange.
      var fieldContents = "20230608192021.2234+0600";
      var line = $"{fieldContents}^M".AsSpan();
      var fieldEnumerator = line.ToFields(_encodingDetails.ComponentSeparator, _encodingDetails.EscapeCharacter);
      var log = new ProcessingLog();

      var expectedLogEntry = LogEntry.GetFieldPresentEntry(
         _lineNumber,
         _fieldSpecification.FieldDescription,
         fieldContents);

      // Act.
      _ = DateTimeComponent.Parse(
         ref fieldEnumerator,
         _fieldSpecification,
         _defaultTimezoneOffset,
         _lineNumber,
         log);

      // Assert.
      log.Should().HaveCount(1);
      log.First().Should().Be(expectedLogEntry);
   }

   [Theory]
   [InlineData("20232201")]            // Invalid month
   [InlineData("20231033")]            // Invalid day
   [InlineData("2023010133")]          // Invalid hour
   [InlineData("202301011077")]        // Invalid second
   [InlineData("2023112")]             // Shorter than accepted format
   [InlineData("202311221")]
   [InlineData("20231122112")]
   [InlineData("2023112211223")]
   [InlineData("20231122112233.111")]
   [InlineData("20231122112233.1111+601")]
   [InlineData("asdf")]
   [InlineData(" 20230101")]           // Leading/trailing whitespace
   [InlineData("20230101 ")]
   [InlineData(" 20230101 ")]
   public void DateTimeComponent_Parse_ShouldReturnNotPresentInstance_WhenFieldContainsAnInvalidTimestampValue(String fieldContents)
   {
      // Arrange.
      var line = $"{fieldContents}^M".AsSpan();
      var fieldEnumerator = line.ToFields(_encodingDetails.ComponentSeparator, _encodingDetails.EscapeCharacter);
      var log = new ProcessingLog();

      // Act.
      var field = DateTimeComponent.Parse(
         ref fieldEnumerator,
         _fieldSpecification,
         _defaultTimezoneOffset,
         _lineNumber,
         log);

      // Assert.
      field.Should().Be(DateTimeComponent.NotPresent);
   }

   [Theory]
   [InlineData("20232201")]            // Invalid month
   [InlineData("20231033")]            // Invalid day
   [InlineData("2023010133")]          // Invalid hour
   [InlineData("202301011077")]        // Invalid second
   [InlineData("2023112")]             // Shorter than accepted format
   [InlineData("202311221")]
   [InlineData("20231122112")]
   [InlineData("2023112211223")]
   [InlineData("20231122112233.111")]
   [InlineData("20231122112233.1111+601")]
   [InlineData("asdf")]
   [InlineData(" 20230101")]           // Leading/trailing whitespace
   [InlineData("20230101 ")]
   [InlineData(" 20230101 ")]
   public void DateTimeComponent_Parse_ShouldLogError_WhenFieldContainsAnInvalidNumericValue(String fieldContents)
   {
      // Arrange.
      var line = $"{fieldContents}^M".AsSpan();
      var fieldEnumerator = line.ToFields(_encodingDetails.ComponentSeparator, _encodingDetails.EscapeCharacter);
      var log = new ProcessingLog();

      var message = String.Format(
         Messages.InvalidTimestampValue,
         _fieldSpecification.FieldDescription);
      var expectedLogEntry = new LogEntry(
         LogLevel.Error,
         message,
         _lineNumber,
         _fieldSpecification.FieldDescription,
         fieldContents);

      // Act.
      _ = DateTimeComponent.Parse(
         ref fieldEnumerator,
         _fieldSpecification,
         _defaultTimezoneOffset,
         _lineNumber,
         log);

      // Assert.
      log.Should().HaveCount(1);
      log.First().Should().Be(expectedLogEntry);
   }

   [Theory]
   [InlineData(Optionality.Optional)]
   [InlineData(Optionality.Required)]
   public void DateTimeComponent_Parse_ShouldReturnNotPresentInstance_WhenFieldIsBeyondEndOfSuppliedFields(Optionality optionality)
   {
      // Arrange.
      var line = $"asdf^M".AsSpan();
      var fieldEnumerator = line.ToFields(_encodingDetails.ComponentSeparator, _encodingDetails.EscapeCharacter);
      fieldEnumerator.MoveNext();
      fieldEnumerator.MoveNext();
      var log = new ProcessingLog();
      var fieldSpecification = _fieldSpecification with { Optionality = optionality, Sequence = 2 };

      // Act.
      var field = DateTimeComponent.Parse(
         ref fieldEnumerator,
         fieldSpecification,
         _defaultTimezoneOffset,
         _lineNumber,
         log);

      // Assert.
      field.Should().Be(DateTimeComponent.NotPresent);
   }

   [Fact]
   public void DateTimeComponent_Parse_ShouldLogFieldNotPresent_WhenOptionalFieldIsBeyondEndOfSuppliedFields()
   {
      // Arrange.
      var line = $"asdf^M".AsSpan();
      var fieldEnumerator = line.ToFields(_encodingDetails.ComponentSeparator, _encodingDetails.EscapeCharacter);
      fieldEnumerator.MoveNext();
      fieldEnumerator.MoveNext();
      var log = new ProcessingLog();
      var fieldSpecification = _fieldSpecification with { Optionality = Optionality.Optional, Sequence = 2 };

      var expectedLogEntry = LogEntry.GetOptionalFieldNotPresentEntry(
         _lineNumber,
         fieldSpecification.FieldDescription);

      // Act.
      _ = DateTimeComponent.Parse(
         ref fieldEnumerator,
         fieldSpecification,
         _defaultTimezoneOffset,
         _lineNumber,
         log);

      // Assert.
      log.Should().HaveCount(1);
      log.First().Should().Be(expectedLogEntry);
   }

   [Fact]
   public void DateTimeComponent_Parse_ShouldLogRequiredFieldNotPresent_WhenRequiredFieldIsBeyondEndOfSuppliedFields()
   {
      // Arrange.
      var line = $"asdf^M".AsSpan();
      var fieldEnumerator = line.ToFields(_encodingDetails.ComponentSeparator, _encodingDetails.EscapeCharacter);
      fieldEnumerator.MoveNext();
      fieldEnumerator.MoveNext();
      var log = new ProcessingLog();
      var fieldSpecification = _fieldSpecification with { Optionality = Optionality.Required, Sequence = 2 };

      var expectedLogEntry = LogEntry.GetRequiredFieldNotPresentEntry(
         _lineNumber,
         fieldSpecification.FieldDescription);

      // Act.
      _ = DateTimeComponent.Parse(
         ref fieldEnumerator,
         fieldSpecification,
         _defaultTimezoneOffset,
         _lineNumber,
         log);

      // Assert.
      log.Should().HaveCount(1);
      log.First().Should().Be(expectedLogEntry);
   }

   [Theory]
   [InlineData(Optionality.Optional)]
   [InlineData(Optionality.Required)]
   public void DateTimeComponent_Parse_ShouldReturnNotPresentInstance_WhenFieldIsEmpty(Optionality optionality)
   {
      // Arrange.
      var line = $"^M".AsSpan();
      var fieldEnumerator = line.ToFields(_encodingDetails.ComponentSeparator, _encodingDetails.EscapeCharacter);
      var log = new ProcessingLog();
      var fieldSpecification = _fieldSpecification with { Optionality = optionality };

      // Act.
      var field = DateTimeComponent.Parse(
         ref fieldEnumerator,
         fieldSpecification,
         _defaultTimezoneOffset,
         _lineNumber,
         log);

      // Assert.
      field.Should().Be(DateTimeComponent.NotPresent);
   }

   [Fact]
   public void DateTimeComponent_Parse_ShouldLogFieldNotPresent_WhenOptionalFieldIsEmpty()
   {
      // Arrange.
      var line = $"^M".AsSpan();
      var fieldEnumerator = line.ToFields(_encodingDetails.ComponentSeparator, _encodingDetails.EscapeCharacter);
      var log = new ProcessingLog();
      var fieldSpecification = _fieldSpecification with { Optionality = Optionality.Optional };

      var expectedLogEntry = LogEntry.GetOptionalFieldNotPresentEntry(
         _lineNumber,
         fieldSpecification.FieldDescription);

      // Act.
      _ = DateTimeComponent.Parse(
         ref fieldEnumerator,
         fieldSpecification,
         _defaultTimezoneOffset,
         _lineNumber,
         log);

      // Assert.
      log.Should().HaveCount(1);
      log.First().Should().Be(expectedLogEntry);
   }

   [Fact]
   public void DateTimeComponent_Parse_ShouldLogRequiredFieldNotPresent_WhenRequiredFieldIsEmpty()
   {
      // Arrange.
      var line = $"^M".AsSpan();
      var fieldEnumerator = line.ToFields(_encodingDetails.ComponentSeparator, _encodingDetails.EscapeCharacter);
      var log = new ProcessingLog();
      var fieldSpecification = _fieldSpecification with { Optionality = Optionality.Required };

      var expectedLogEntry = LogEntry.GetRequiredFieldNotPresentEntry(
         _lineNumber,
         fieldSpecification.FieldDescription);

      // Act.
      _ = DateTimeComponent.Parse(
         ref fieldEnumerator,
         fieldSpecification,
         _defaultTimezoneOffset,
         _lineNumber,
         log);

      // Assert.
      log.Should().HaveCount(1);
      log.First().Should().Be(expectedLogEntry);
   }

   [Fact]
   public void DateTimeComponent_Parse_ShouldReturnPresentButNullInstance_WhenFieldIsTwoDoubleQuotes()
   {
      // Arrange.
      var line = "\"\"^M".AsSpan();
      var fieldEnumerator = line.ToFields(_encodingDetails.ComponentSeparator, _encodingDetails.EscapeCharacter);
      var log = new ProcessingLog();

      // Act.
      var field = DateTimeComponent.Parse(
         ref fieldEnumerator,
         _fieldSpecification,
         _defaultTimezoneOffset,
         _lineNumber,
         log);

      // Assert.
      field.Should().Be(DateTimeComponent.PresentButNull);
   }

   [Fact]
   public void DateTimeComponent_Parse_ShouldLogFieldPresentButNull_WhenFieldIsTwoDoubleQuotes()
   {
      // Arrange.
      var line = "\"\"^M".AsSpan();
      var fieldEnumerator = line.ToFields(_encodingDetails.ComponentSeparator, _encodingDetails.EscapeCharacter);
      var log = new ProcessingLog();

      var expectedLogEntry = LogEntry.GetFieldPresentButNullEntry(
         _lineNumber,
         _fieldSpecification.FieldDescription);

      // Act.
      _ = DateTimeComponent.Parse(
         ref fieldEnumerator,
         _fieldSpecification,
         _defaultTimezoneOffset,
         _lineNumber,
         log);

      // Assert.
      log.Should().HaveCount(1);
      log.First().Should().Be(expectedLogEntry);
   }

   #endregion
}
