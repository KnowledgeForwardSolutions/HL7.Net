namespace HL7.Net.Tests.Unit.Core;

public class SequenceIdFieldTests
{
   private static readonly EncodingDetails _encodingDetails = EncodingDetails.DefaultEncodingDetails;
   private static readonly FieldSpecification _fieldSpecification = new(
      "TST",
      1,
      "Test Field",
      4,
      HL7Datatype.SI_SequenceID,
      Optionality.Optional,
      "N");
   private const Int32 _lineNumber = 10;

   #region Internal Constructor Tests
   // ==========================================================================
   // ==========================================================================

   [Fact]
   public void SequenceIDField_Constructor_ShouldCreateObject_WhenValueIsSupplied()
   {
      var value = 42;

      // Act.
      var sut = new SequenceIDField(value);

      // Assert.
      sut.Should().NotBeNull();
      sut.Value.Should().Be(value);
      sut.Presence.Should().Be(Presence.Present);
   }

   [Fact]
   public void SequenceIDField_Constructor_ShouldCreateObject_WhenValueAndFieldPresenceAreSupplied()
   {
      Int32? value = null!;

      // Act.
      var sut = new SequenceIDField(value, Presence.NotPresent);

      // Assert.
      sut.Should().NotBeNull();
      sut.Value.Should().Be(value);
      sut.Presence.Should().Be(Presence.NotPresent);
   }

   #endregion

   #region Implicit Integer Converter Tests
   // ==========================================================================
   // ==========================================================================

   [Fact]
   public void SequenceIDField_ImplicitIntegerConverter_ShouldReturnExpectedValue_WhenFieldIsNotEmpty()
   {
      // Arrange.
      var value = 42;
      var sut = new SequenceIDField(value);

      // Act.
      Decimal? result = sut;

      // Assert.
      result.Should().Be(value);
   }

   [Fact]
   public void SequenceIDField_ImplicitIntegerConverter_ShouldReturnExpectedValue_WhenFieldIsNotPresent()
   {
      // Arrange.
      var sut = SequenceIDField.NotPresent;

      // Act.
      Decimal? result = sut;

      // Assert.
      result.Should().BeNull();
   }

   #endregion

   #region NotPresent Instance Tests
   // ==========================================================================
   // ==========================================================================

   [Fact]
   public void SequenceIDField_NotPresent_ShouldReturnExpectedValue()
   {
      // Act.
      var sut = SequenceIDField.NotPresent;

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
   public void SequenceIDField_PresentButNull_ShouldReturnExpectedValue()
   {
      // Act.
      var sut = SequenceIDField.PresentButNull;

      // Assert.
      sut.Should().NotBeNull();
      sut.Value.Should().BeNull();
      sut.Presence.Should().Be(Presence.PresentButNull);
   }

   #endregion

   #region Parse Method Tests
   // ==========================================================================
   // ==========================================================================

   [Theory]
   [InlineData("42", 42)]
   [InlineData("1234 ", 1234)]
   public void SequenceIDField_Parse_ShouldReturnIntegerValue_WhenFieldContainsValidIntegerValue(
      String fieldContents,
      Int32 expectedValue)
   {
      // Arrange.
      var line = $"TST|{fieldContents}|This is a test...|This is only a test...".AsSpan();
      var fieldEnumerator = line.ToFields(_encodingDetails.FieldSeparator, _encodingDetails.EscapeCharacter);
      fieldEnumerator.MoveNext();
      var log = new ProcessingLog();

      var expected = new SequenceIDField(expectedValue);

      // Act.
      var field = SequenceIDField.Parse(
         ref fieldEnumerator,
         _fieldSpecification,
         _lineNumber,
         log);

      // Assert.
      field.Should().Be(expected);
   }

   [Fact]
   public void SequenceIDField_Parse_ShouldLogExpectedEntry_WhenFieldContainsValidIntegerValue()
   {
      // Arrange.
      var fieldContents = "42";
      var line = $"TST|{fieldContents}|This is a test...|This is only a test...".AsSpan();
      var fieldEnumerator = line.ToFields(_encodingDetails.FieldSeparator, _encodingDetails.EscapeCharacter);
      fieldEnumerator.MoveNext();
      var log = new ProcessingLog();

      var message = String.Format(Messages.LogFieldPresent, _fieldSpecification.FieldDescription);

      var expectedLogEntry = new LogEntry(
         LogLevel.Information,
         message,
         _lineNumber,
         _fieldSpecification.FieldDescription,
         fieldContents);

      // Act.
      _ = SequenceIDField.Parse(
         ref fieldEnumerator,
         _fieldSpecification,
         _lineNumber,
         log);

      // Assert.
      log.Should().HaveCount(1);
      log.First().Should().Be(expectedLogEntry);
   }

   [Theory]
   [InlineData("$42")]
   [InlineData("42GBP")]
   [InlineData("42-")]
   [InlineData("(42)")]
   [InlineData(" 42")]
   [InlineData("42 ")]
   [InlineData(" 42 ")]
   [InlineData("   ")]
   [InlineData("42,000")]
   [InlineData("42E-3")]
   [InlineData("+42")]
   [InlineData("-42")]
   [InlineData("42.42")]
   [InlineData("123.4567")]
   [InlineData("-123.456")]
   public void SequenceIDField_Parse_ShouldReturnNotPresentInstance_WhenFieldContainsAnInvalidIntegerValue(String fieldContents)
   {
      // Arrange.
      var line = $"TST|{fieldContents}|This is a test...|This is only a test...".AsSpan();
      var fieldEnumerator = line.ToFields(_encodingDetails.FieldSeparator, _encodingDetails.EscapeCharacter);
      fieldEnumerator.MoveNext();
      var log = new ProcessingLog();

      // Act.
      var field = SequenceIDField.Parse(
         ref fieldEnumerator,
         _fieldSpecification,
         _lineNumber,
         log);

      // Assert.
      field.Should().Be(SequenceIDField.NotPresent);
   }

   [Theory]
   [InlineData("$42")]
   [InlineData("42GBP")]
   [InlineData("42-")]
   [InlineData("(42)")]
   [InlineData(" 42")]
   [InlineData("42 ")]
   [InlineData(" 42 ")]
   [InlineData("   ")]
   [InlineData("42,000")]
   [InlineData("42E-3")]
   [InlineData("+42")]
   [InlineData("-42")]
   [InlineData("42.42")]
   [InlineData("123.4567")]
   [InlineData("-123.456")]
   public void SequenceIDField_Parse_ShouldLogError_WhenFieldContainsAnInvalidIntegerValue(String fieldContents)
   {
      // Arrange.
      var line = $"TST|{fieldContents}|This is a test...|This is only a test...".AsSpan();
      var fieldEnumerator = line.ToFields(_encodingDetails.FieldSeparator, _encodingDetails.EscapeCharacter);
      fieldEnumerator.MoveNext();
      var log = new ProcessingLog();

      var message = String.Format(
         Messages.InvalidNumericValue,
         _fieldSpecification.FieldDescription);
      var expectedLogEntry = new LogEntry(
         LogLevel.Error,
         message,
         _lineNumber,
         _fieldSpecification.FieldDescription,
         fieldContents);

      // Act.
      _ = SequenceIDField.Parse(
         ref fieldEnumerator,
         _fieldSpecification,
         _lineNumber,
         log);

      // Assert.
      log.Should().HaveCount(1);
      log.First().Should().Be(expectedLogEntry);
   }

   [Fact]
   public void SequenceIDField_Parse_ShouldTruncateValue_WhenFieldIsLongerThanMaxLength()
   {
      // Arrange.
      var fieldContents = "123456";
      var line = $"TST|{fieldContents}|This is a test...|This is only a test...".AsSpan();
      var fieldEnumerator = line.ToFields(_encodingDetails.FieldSeparator, _encodingDetails.EscapeCharacter);
      fieldEnumerator.MoveNext();
      var log = new ProcessingLog();

      var expectedValue = 1234;
      var expected = new SequenceIDField(expectedValue);

      // Act.
      var field = SequenceIDField.Parse(
         ref fieldEnumerator,
         _fieldSpecification,
         _lineNumber,
         log);

      // Assert.
      field.Should().Be(expected);
   }

   [Fact]
   public void SequenceIDField_Parse_ShouldLogExpectedEntry_WhenFieldIsLongerThanMaxLength()
   {
      // Arrange.
      var fieldContents = "123456";
      var line = $"TST|{fieldContents}|This is a test...|This is only a test...".AsSpan();
      var fieldEnumerator = line.ToFields(_encodingDetails.FieldSeparator, _encodingDetails.EscapeCharacter);
      fieldEnumerator.MoveNext();
      var log = new ProcessingLog();

      var message = String.Format(
         Messages.LogFieldPresentButTruncated,
         _fieldSpecification.FieldDescription,
         _fieldSpecification.Length);
      var expectedLogEntry = new LogEntry(
         LogLevel.Warning,
         message,
         _lineNumber,
         _fieldSpecification.FieldDescription,
         fieldContents);

      // Act.
      _ = SequenceIDField.Parse(
         ref fieldEnumerator,
         _fieldSpecification,
         _lineNumber,
         log);

      // Assert.
      log.Should().HaveCount(1);
      log.First().Should().Be(expectedLogEntry);
   }

   [Theory]
   [InlineData(Optionality.Optional)]
   [InlineData(Optionality.Required)]
   public void SequenceIDField_Parse_ShouldReturnNotPresentInstance_WhenFieldIsBeyondEndOfSuppliedFields(Optionality optionality)
   {
      // Arrange.
      var line = "TST|This is a test..".AsSpan();
      var fieldEnumerator = line.ToFields(_encodingDetails.FieldSeparator, _encodingDetails.EscapeCharacter);
      fieldEnumerator.MoveNext();
      fieldEnumerator.MoveNext();
      var log = new ProcessingLog();
      var fieldSpecification = _fieldSpecification with { Optionality = optionality, Sequence = 2 };

      // Act.
      var field = SequenceIDField.Parse(
         ref fieldEnumerator,
         fieldSpecification,
         _lineNumber,
         log);

      // Assert.
      field.Should().Be(SequenceIDField.NotPresent);
   }

   [Fact]
   public void SequenceIDField_Parse_ShouldLogFieldNotPresent_WhenOptionalFieldIsBeyondEndOfSuppliedFields()
   {
      // Arrange.
      var line = "TST|This is a test..".AsSpan();
      var fieldEnumerator = line.ToFields(_encodingDetails.FieldSeparator, _encodingDetails.EscapeCharacter);
      fieldEnumerator.MoveNext();
      fieldEnumerator.MoveNext();
      var log = new ProcessingLog();
      var fieldSpecification = _fieldSpecification with { Optionality = Optionality.Optional, Sequence = 2 };

      var message = String.Format(
         Messages.LogFieldNotPresent,
         fieldSpecification.FieldDescription);
      var expectedLogEntry = new LogEntry(
         LogLevel.Information,
         message,
         _lineNumber,
         fieldSpecification.FieldDescription);

      // Act.
      _ = SequenceIDField.Parse(
         ref fieldEnumerator,
         _fieldSpecification,
         _lineNumber,
         log);

      // Assert.
      log.Should().HaveCount(1);
      log.First().Should().Be(expectedLogEntry);
   }

   [Fact]
   public void SequenceIDField_Parse_ShouldLogRequiredFieldNotPresent_WhenRequiredFieldIsBeyondEndOfSuppliedFields()
   {
      // Arrange.
      var line = "TST|This is a test..".AsSpan();
      var fieldEnumerator = line.ToFields(_encodingDetails.FieldSeparator, _encodingDetails.EscapeCharacter);
      fieldEnumerator.MoveNext();
      fieldEnumerator.MoveNext();
      var log = new ProcessingLog();
      var fieldSpecification = _fieldSpecification with { Optionality = Optionality.Required, Sequence = 2 };

      var message = String.Format(
         Messages.LogRequiredFieldNotPresent,
         fieldSpecification.FieldDescription);
      var expectedLogEntry = new LogEntry(
         LogLevel.Error,
         message,
         _lineNumber,
         fieldSpecification.FieldDescription);

      // Act.
      _ = SequenceIDField.Parse(
         ref fieldEnumerator,
         fieldSpecification,
         _lineNumber,
         log);

      // Assert.
      log.Should().HaveCount(1);
      log.First().Should().Be(expectedLogEntry);
   }

   [Theory]
   [InlineData(Optionality.Optional)]
   [InlineData(Optionality.Required)]
   public void SequenceIDField_Parse_ShouldReturnNotPresentInstance_WhenFieldIsEmpty(Optionality optionality)
   {
      // Arrange.
      var line = "TST||This is a test..".AsSpan();
      var fieldEnumerator = line.ToFields(_encodingDetails.FieldSeparator, _encodingDetails.EscapeCharacter);
      fieldEnumerator.MoveNext();
      var log = new ProcessingLog();
      var fieldSpecification = _fieldSpecification with { Optionality = optionality };

      // Act.
      var field = SequenceIDField.Parse(
         ref fieldEnumerator,
         fieldSpecification,
         _lineNumber,
         log);

      // Assert.
      field.Should().Be(SequenceIDField.NotPresent);
   }

   [Fact]
   public void SequenceIDField_Parse_ShouldLogFieldNotPresent_WhenOptionalFieldIsEmpty()
   {
      // Arrange.
      var line = "TST||This is a test..".AsSpan();
      var fieldEnumerator = line.ToFields(_encodingDetails.FieldSeparator, _encodingDetails.EscapeCharacter);
      fieldEnumerator.MoveNext();
      var log = new ProcessingLog();

      var message = String.Format(
         Messages.LogFieldNotPresent,
         _fieldSpecification.FieldDescription);
      var expectedLogEntry = new LogEntry(
         LogLevel.Information,
         message,
         _lineNumber,
         _fieldSpecification.FieldDescription);

      // Act.
      _ = SequenceIDField.Parse(
         ref fieldEnumerator,
         _fieldSpecification,
         _lineNumber,
         log);

      // Assert.
      log.Should().HaveCount(1);
      log.First().Should().Be(expectedLogEntry);
   }

   [Fact]
   public void SequenceIDField_Parse_ShouldLogRequiredFieldNotPresent_WhenRequiredFieldIsEmpty()
   {
      // Arrange.
      var line = "TST||This is a test..".AsSpan();
      var fieldEnumerator = line.ToFields(_encodingDetails.FieldSeparator, _encodingDetails.EscapeCharacter);
      fieldEnumerator.MoveNext();
      var log = new ProcessingLog();

      var fieldSpecification = _fieldSpecification with { Optionality = Optionality.Required };
      var message = String.Format(
         Messages.LogRequiredFieldNotPresent,
         fieldSpecification.FieldDescription);
      var expectedLogEntry = new LogEntry(
         LogLevel.Error,
         message,
         _lineNumber,
         fieldSpecification.FieldDescription);

      // Act.
      _ = SequenceIDField.Parse(
         ref fieldEnumerator,
         fieldSpecification,
         _lineNumber,
         log);

      // Assert.
      log.Should().HaveCount(1);
      log.First().Should().Be(expectedLogEntry);
   }

   [Fact]
   public void SequenceIDField_Parse_ShouldReturnPresentButNullInstance_WhenFieldIsTwoDoubleQuotes()
   {
      // Arrange.
      var line = "TST|\"\"|This is a test..".AsSpan();
      var fieldEnumerator = line.ToFields(_encodingDetails.FieldSeparator, _encodingDetails.EscapeCharacter);
      fieldEnumerator.MoveNext();
      var log = new ProcessingLog();

      // Act.
      var field = SequenceIDField.Parse(
         ref fieldEnumerator,
         _fieldSpecification,
         _lineNumber,
         log);

      // Assert.
      field.Should().Be(SequenceIDField.PresentButNull);
   }

   [Fact]
   public void SequenceIDField_Parse_ShouldLogFieldPresentButNull_WhenFieldIsTwoDoubleQuotes()
   {
      // Arrange.
      var line = "TST|\"\"|This is a test..".AsSpan();
      var fieldEnumerator = line.ToFields(_encodingDetails.FieldSeparator, _encodingDetails.EscapeCharacter);
      fieldEnumerator.MoveNext();
      var log = new ProcessingLog();

      var message = String.Format(
         Messages.LogFieldPresentButNull,
         _fieldSpecification.FieldDescription);
      var expectedLogEntry = new LogEntry(
         LogLevel.Information,
         message,
         _lineNumber,
         _fieldSpecification.FieldDescription,
         GeneralConstants.PresentButNullValue);

      // Act.
      _ = SequenceIDField.Parse(
         ref fieldEnumerator,
         _fieldSpecification,
         _lineNumber,
         log);

      // Assert.
      log.Should().HaveCount(1);
      log.First().Should().Be(expectedLogEntry);
   }

   #endregion
}
