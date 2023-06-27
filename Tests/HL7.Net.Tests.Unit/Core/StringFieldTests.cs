namespace HL7.Net.Tests.Unit.Core;

public class StringFieldTests
{
   private static readonly EncodingDetails _encodingDetails = EncodingDetails.DefaultEncodingDetails;
   private static readonly FieldSpecification _fieldSpecification = new(
      "TST",
      1,
      "Test Field",
      42,
      HL7Datatype.ST_String,
      Optionality.Optional,
      "N");
   private const Int32 _lineNumber = 10;

   #region Internal Constructor Tests
   // ==========================================================================
   // ==========================================================================

   [Fact]
   public void StringField_Constructor_ShouldCreateObject_WhenStringValueIsSupplied()
   {
      // Arrange.  Note: we don't check the full range of string values (null,
      // String.Empty, whitespace) because those cases are handled by the Parse
      // method.
      var value = "asdf";

      // Act.
      var sut = new StringField(value);

      // Assert.
      sut.Should().NotBeNull();
      sut.Value.Should().Be(value);
      sut.FieldPresence.Should().Be(FieldPresence.Present);
   }

   [Fact]
   public void StringField_Constructor_ShouldCreateObject_WhenStringValueAndFieldPresenceAreSupplied()
   {
      String? value = null!;

      // Act.
      var sut = new StringField(value, FieldPresence.NotPresent);

      // Assert.
      sut.Should().NotBeNull();
      sut.Value.Should().Be(value);
      sut.FieldPresence.Should().Be(FieldPresence.NotPresent);
   }

   #endregion

   #region Implicit String Converter Tests
   // ==========================================================================
   // ==========================================================================

   [Fact]
   public void StringField_ImplicitStringConverter_ShouldReturnExpectedValue_WhenFieldIsNotEmpty()
   {
      // Arrange.
      var text = "The quick red fox, blah, blah...";
      var sut = new StringField(text);

      // Act.
      String? str = sut;

      // Assert.
      str.Should().Be(text);
   }

   [Fact]
   public void StringField_ImplicitStringConverter_ShouldReturnExpectedValue_WhenFieldIsNotPresent()
   {
      // Arrange.
      var sut = StringField.NotPresent;

      // Act.
      String? str = sut;

      // Assert.
      str.Should().BeNull();
   }

   #endregion

   #region NotPresent Property Tests
   // ==========================================================================
   // ==========================================================================

   [Fact]
   public void StringField_NotPresent_ShouldReturnExpectedValue()
   {
      // Act.
      var sut = StringField.NotPresent;

      // Assert.
      sut.Should().NotBeNull();
      sut.Value.Should().BeNull();
      sut.FieldPresence.Should().Be(FieldPresence.NotPresent);
   }

   #endregion

   #region PresentButNull Property Tests
   // ==========================================================================
   // ==========================================================================

   [Fact]
   public void StringField_PresentButNull_ShouldReturnExpectedValue()
   {
      // Act.
      var sut = StringField.PresentButNull;

      // Assert.
      sut.Should().NotBeNull();
      sut.Value.Should().BeNull();
      sut.FieldPresence.Should().Be(FieldPresence.PresentButNull);
   }

   #endregion

   #region Parse Method Tests
   // ==========================================================================
   // ==========================================================================

   [Fact]
   public void StringField_Parse_ShouldReturnEntireFieldContents_WhenNonEmptyFieldIsLessThanMaxLengthAndDoesNotHaveLeadingWhiteSpace()
   {
      // Arrange.
      var line = "TST|This is a test...|This is only a test...|For the next sixty seconds...".AsSpan();
      var fieldEnumerator = line.ToFields(_encodingDetails.FieldSeparator, _encodingDetails.EscapeCharacter);
      fieldEnumerator.MoveNext();
      var log = new ProcessingLog();

      var expected = new StringField("This is a test...");

      // Act.
      var field = StringField.Parse(
         ref fieldEnumerator,
         _encodingDetails,
         _fieldSpecification,
         _lineNumber,
         log);

      // Assert.
      field.Should().Be(expected);
   }

   [Fact]
   public void StringField_Parse_ShouldLogFieldPresent_WhenNonEmptyFieldIsLessThanMaxLengthAndDoesNotHaveLeadingWhiteSpace()
   {
      // Arrange.
      var line = "TST|This is a test...|This is only a test...|For the next sixty seconds...".AsSpan();
      var fieldEnumerator = line.ToFields(_encodingDetails.FieldSeparator, _encodingDetails.EscapeCharacter);
      fieldEnumerator.MoveNext();
      var log = new ProcessingLog();

      var message = String.Format(Messages.LogFieldPresent, _fieldSpecification.FieldDescription);

      var expectedLogEntry = new LogEntry(
         LogLevel.Information,
         message,
         _lineNumber,
         _fieldSpecification.FieldDescription,
         "This is a test...");

      // Act.
      _ = StringField.Parse(
         ref fieldEnumerator,
         _encodingDetails,
         _fieldSpecification,
         _lineNumber,
         log);

      // Assert.
      log.Should().HaveCount(1);
      log.First().Should().Be(expectedLogEntry);
   }

   [Fact]
   public void StringField_Parse_ShouldTrimLeadingWhiteSpace_WhenNonEmptyFieldIsLessThanMaxLengthButDoesHaveLeadingWhiteSpace()
   {
      // Arrange.
      var line = "TST|   This is a test...|This is only a test...|For the next sixty seconds...".AsSpan();
      var fieldEnumerator = line.ToFields(_encodingDetails.FieldSeparator, _encodingDetails.EscapeCharacter);
      fieldEnumerator.MoveNext();
      var log = new ProcessingLog();

      var expected = new StringField("This is a test...");

      // Act.
      var field = StringField.Parse(
         ref fieldEnumerator,
         _encodingDetails,
         _fieldSpecification,
         _lineNumber,
         log);

      // Assert.
      field.Should().Be(expected);
   }

   [Fact]
   public void StringField_Parse_ShouldLogFieldPresent_WhenNonEmptyFieldIsLessThanMaxLengthButDoesHaveLeadingWhiteSpace()
   {
      // Arrange.
      var line = "TST|   This is a test...|This is only a test...|For the next sixty seconds...".AsSpan();
      var fieldEnumerator = line.ToFields(_encodingDetails.FieldSeparator, _encodingDetails.EscapeCharacter);
      fieldEnumerator.MoveNext();
      var log = new ProcessingLog();

      var message = String.Format(Messages.LogFieldPresent, _fieldSpecification.FieldDescription);

      var expectedLogEntry = new LogEntry(
         LogLevel.Information,
         message,
         _lineNumber,
         _fieldSpecification.FieldDescription,
         "   This is a test...");

      // Act.
      _ = StringField.Parse(
         ref fieldEnumerator,
         _encodingDetails,
         _fieldSpecification,
         _lineNumber,
         log);

      // Assert.
      log.Should().HaveCount(1);
      log.First().Should().Be(expectedLogEntry);
   }

   [Fact]
   public void StringField_Parse_ShouldTruncateValue_WhenNonEmptyFieldIsLongerThanMaxLengthAndDoesNotHaveLeadingWhiteSpace()
   {
      // Arrange.
      var line = "TST|This is a test...|This is only a test...|For the next sixty seconds...".AsSpan();
      var fieldEnumerator = line.ToFields(_encodingDetails.FieldSeparator, _encodingDetails.EscapeCharacter);
      fieldEnumerator.MoveNext();
      var log = new ProcessingLog();
      var fieldSpecification = _fieldSpecification with { Length = 10 };

      var expected = new StringField("This is a ");

      // Act.
      var field = StringField.Parse(
         ref fieldEnumerator,
         _encodingDetails,
         fieldSpecification,
         _lineNumber,
         log);

      // Assert.
      field.Should().Be(expected);
   }

   [Fact]
   public void StringField_Parse_ShouldLogFieldPresent_WhenNonEmptyFieldIsLongerThanMaxLengthAndDoesNotHaveLeadingWhiteSpace()
   {
      // Arrange.
      var line = "TST|This is a test...|This is only a test...|For the next sixty seconds...".AsSpan();
      var fieldEnumerator = line.ToFields(_encodingDetails.FieldSeparator, _encodingDetails.EscapeCharacter);
      fieldEnumerator.MoveNext();
      var log = new ProcessingLog();
      var fieldSpecification = _fieldSpecification with { Length = 10 };

      var message = String.Format(
         Messages.LogFieldPresentButTruncated, 
         fieldSpecification.FieldDescription,
         fieldSpecification.Length);

      var expectedLogEntry = new LogEntry(
         LogLevel.Warning,
         message,
         _lineNumber,
         _fieldSpecification.FieldDescription,
         "This is a test...");

      // Act.
      _ = StringField.Parse(
         ref fieldEnumerator,
         _encodingDetails,
         fieldSpecification,
         _lineNumber,
         log);

      // Assert.
      log.Should().HaveCount(1);
      log.First().Should().Be(expectedLogEntry);
   }

   [Fact]
   public void StringField_Parse_ShouldTruncateValue_WhenTrimmedDecodedValueExceedsFieldLength()
   {
      // Arrange.
      var line = "TST|   This is a test...|This is only a test...|For the next sixty seconds...".AsSpan();
      var fieldEnumerator = line.ToFields(_encodingDetails.FieldSeparator, _encodingDetails.EscapeCharacter);
      fieldEnumerator.MoveNext();
      var log = new ProcessingLog();
      var fieldSpecification = _fieldSpecification with { Length = 10 };

      var expected = new StringField("This is a ");

      // Act.
      var field = StringField.Parse(
         ref fieldEnumerator,
         _encodingDetails,
         fieldSpecification,
         _lineNumber,
         log);

      // Assert.
      field.Should().Be(expected);
   }

   [Fact]
   public void StringField_Parse_ShouldLogFieldPresent_WhenTrimmedDecodedValueExceedsFieldLength()
   {
      // Arrange.
      var line = "TST|   This is a test...|This is only a test...|For the next sixty seconds...".AsSpan();
      var fieldEnumerator = line.ToFields(_encodingDetails.FieldSeparator, _encodingDetails.EscapeCharacter);
      fieldEnumerator.MoveNext();
      var log = new ProcessingLog();
      var fieldSpecification = _fieldSpecification with { Length = 10 };

      var message = String.Format(
         Messages.LogFieldPresentButTruncated,
         fieldSpecification.FieldDescription,
         fieldSpecification.Length);

      var expectedLogEntry = new LogEntry(
         LogLevel.Warning,
         message,
         _lineNumber,
         _fieldSpecification.FieldDescription,
         "   This is a test...");

      // Act.
      _ = StringField.Parse(
         ref fieldEnumerator,
         _encodingDetails,
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
   public void StringField_Parse_ShouldReturnNotPresentInstance_WhenFieldIsEmpty(Optionality optionality)
   {
      // Arrange.
      var line = "TST||This is a test..".AsSpan();
      var fieldEnumerator = line.ToFields(_encodingDetails.FieldSeparator, _encodingDetails.EscapeCharacter);
      fieldEnumerator.MoveNext();
      var log = new ProcessingLog();
      var fieldSpecification = _fieldSpecification with { Optionality = optionality };

      // Act.
      var field = StringField.Parse(
         ref fieldEnumerator, 
         _encodingDetails,
         fieldSpecification, 
         _lineNumber,
         log);

      // Assert.
      field.Should().Be(StringField.NotPresent);
   }

   [Fact]
   public void StringField_Parse_ShouldLogFieldNotPresent_WhenOptionalFieldIsEmpty()
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
      _ = StringField.Parse(
         ref fieldEnumerator,
         _encodingDetails,
         _fieldSpecification,
         _lineNumber,
         log);

      // Assert.
      log.Should().HaveCount(1);
      log.First().Should().Be(expectedLogEntry);
   }

   [Fact]
   public void StringField_Parse_ShouldLogFieldNotPresent_WhenRequiredFieldIsEmpty()
   {
      // Arrange.
      var line = "TST||This is a test..".AsSpan();
      var fieldEnumerator = line.ToFields(_encodingDetails.FieldSeparator, _encodingDetails.EscapeCharacter);
      fieldEnumerator.MoveNext();
      var log = new ProcessingLog();

      var fieldSpecification = _fieldSpecification with { Optionality = Optionality.Required };
      var message = String.Format(
         Messages.LogRequiredFieldNotPresent,
         _fieldSpecification.FieldDescription);
      var expectedLogEntry = new LogEntry(
         LogLevel.Error,
         message,
         _lineNumber,
         fieldSpecification.FieldDescription);

      // Act.
      _ = StringField.Parse(
         ref fieldEnumerator,
         _encodingDetails,
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
   public void StringField_Parse_ShouldReturnNotPresentInstance_WhenFieldIsAllLeadingWhiteSpace(Optionality optionality)
   {
      // Arrange.
      var line = "TST|   |This is a test..".AsSpan();
      var fieldEnumerator = line.ToFields(_encodingDetails.FieldSeparator, _encodingDetails.EscapeCharacter);
      fieldEnumerator.MoveNext();
      var log = new ProcessingLog();
      var fieldSpecification = _fieldSpecification with { Optionality = optionality };

      // Act.
      var field = StringField.Parse(
         ref fieldEnumerator,
         _encodingDetails,
         fieldSpecification,
         _lineNumber,
         log);

      // Assert.
      field.Should().Be(StringField.NotPresent);
   }

   [Fact]
   public void StringField_Parse_ShouldReturnPresentButNullInstance_WhenFieldIsTwoDoubleQuotes()
   {
      // Arrange.
      var line = "TST|\"\"|This is a test..".AsSpan();
      var fieldEnumerator = line.ToFields(_encodingDetails.FieldSeparator, _encodingDetails.EscapeCharacter);
      fieldEnumerator.MoveNext();
      var log = new ProcessingLog();

      // Act.
      var field = StringField.Parse(
         ref fieldEnumerator,
         _encodingDetails,
         _fieldSpecification,
         _lineNumber,
         log);

      // Assert.
      field.Should().Be(StringField.PresentButNull);
   }

   [Fact]
   public void StringField_Parse_ShouldLogFieldPresentButNull_WhenFieldIsTwoDoubleQuotes()
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
      _ = StringField.Parse(
         ref fieldEnumerator,
         _encodingDetails,
         _fieldSpecification,
         _lineNumber,
         log);

      // Assert.
      log.Should().HaveCount(1);
      log.First().Should().Be(expectedLogEntry);
   }

   [Theory]
   [InlineData(DefaultSeparators.FieldSeparator)]
   [InlineData(DefaultSeparators.ComponentSeparator)]
   [InlineData(DefaultSeparators.RepetitionSeparator)]
   [InlineData(DefaultSeparators.EscapeCharacter)]
   [InlineData(DefaultSeparators.SubComponentSeparator)]
   public void StringField_Parse_ShouldDecodeEscapedCharacters_WhenRawDataContainsEscapedCharacters(Char escapedChar)
   {
      // Arrange.
      var line = $"TST|This is a test{_encodingDetails.EscapeCharacter}{escapedChar}This is only a test|abc|qwerty".AsSpan();
      var fieldEnumerator = line.ToFields(_encodingDetails.FieldSeparator, _encodingDetails.EscapeCharacter);
      fieldEnumerator.MoveNext();
      var log = new ProcessingLog();

      var expectedValue = $"This is a test{escapedChar}This is only a test";

      var expected = new StringField(expectedValue);

      // Act.
      var field = StringField.Parse(
         ref fieldEnumerator,
         _encodingDetails,
         _fieldSpecification,
         _lineNumber,
         log);

      // Assert.
      field.Should().Be(expected);
   }

   [Theory]
   [InlineData(DefaultSeparators.FieldSeparator)]
   [InlineData(DefaultSeparators.ComponentSeparator)]
   [InlineData(DefaultSeparators.RepetitionSeparator)]
   [InlineData(DefaultSeparators.EscapeCharacter)]
   [InlineData(DefaultSeparators.SubComponentSeparator)]
   public void StringField_Parse_ShouldLogFieldPresent_WhenRawDataContainsEscapedCharacters(Char escapedChar)
   {
      // Arrange.
      var line = $"TST|This is a test{_encodingDetails.EscapeCharacter}{escapedChar}This is only a test|abc|qwerty".AsSpan();
      var fieldEnumerator = line.ToFields(_encodingDetails.FieldSeparator, _encodingDetails.EscapeCharacter);
      fieldEnumerator.MoveNext();
      var log = new ProcessingLog();

      var message = String.Format(Messages.LogFieldPresent, _fieldSpecification.FieldDescription);

      var expectedLogEntry = new LogEntry(
         LogLevel.Information,
         message,
         _lineNumber,
         _fieldSpecification.FieldDescription,
         $"This is a test{_encodingDetails.EscapeCharacter}{escapedChar}This is only a test");

      // Act.
      _ = StringField.Parse(
         ref fieldEnumerator,
         _encodingDetails,
         _fieldSpecification,
         _lineNumber,
         log);

      // Assert.
      log.Should().HaveCount(1);
      log.First().Should().Be(expectedLogEntry);
   }

   [Theory]
   [InlineData(DefaultSeparators.FieldSeparator)]
   [InlineData(DefaultSeparators.ComponentSeparator)]
   [InlineData(DefaultSeparators.RepetitionSeparator)]
   [InlineData(DefaultSeparators.EscapeCharacter)]
   [InlineData(DefaultSeparators.SubComponentSeparator)]
   public void StringField_Parse_ShouldDecodeEscapedCharactersAndTruncateValue_WhenRawDataContainsEscapedCharactersAndDecodedValueExceedsFieldMaxLength(Char escapedChar)
   {
      // Arrange.
      var line = $"TST|This is a test{_encodingDetails.EscapeCharacter}{escapedChar}This is only a test|abc|qwerty".AsSpan();
      var fieldEnumerator = line.ToFields(_encodingDetails.FieldSeparator, _encodingDetails.EscapeCharacter);
      fieldEnumerator.MoveNext();
      var log = new ProcessingLog();
      var fieldSpecification = _fieldSpecification with { Length = 20 };

      var expectedValue = $"This is a test{escapedChar}This ";

      var expected = new StringField(expectedValue);

      // Act.
      var field = StringField.Parse(
         ref fieldEnumerator,
         _encodingDetails,
         fieldSpecification,
         _lineNumber,
         log);

      // Assert.
      field.Should().Be(expected);
   }

   [Theory]
   [InlineData(DefaultSeparators.FieldSeparator)]
   [InlineData(DefaultSeparators.ComponentSeparator)]
   [InlineData(DefaultSeparators.RepetitionSeparator)]
   [InlineData(DefaultSeparators.EscapeCharacter)]
   [InlineData(DefaultSeparators.SubComponentSeparator)]
   public void StringField_Parse_ShouldLogFieldPresent_WhenRawDataContainsEscapedCharactersAndDecodedValueExceedsFieldMaxLength(Char escapedChar)
   {
      // Arrange.
      var line = $"TST|This is a test{_encodingDetails.EscapeCharacter}{escapedChar}This is only a test|abc|qwerty".AsSpan();
      var fieldEnumerator = line.ToFields(_encodingDetails.FieldSeparator, _encodingDetails.EscapeCharacter);
      fieldEnumerator.MoveNext();
      var log = new ProcessingLog();
      var fieldSpecification = _fieldSpecification with { Length = 20 };

      var message = String.Format(Messages.LogFieldPresent, _fieldSpecification.FieldDescription);

      var expectedLogEntry = new LogEntry(
         LogLevel.Information,
         message,
         _lineNumber,
         _fieldSpecification.FieldDescription,
         $"This is a test{_encodingDetails.EscapeCharacter}{escapedChar}This is only a test");

      // Act.
      _ = StringField.Parse(
         ref fieldEnumerator,
         _encodingDetails,
         _fieldSpecification,
         _lineNumber,
         log);

      // Assert.
      log.Should().HaveCount(1);
      log.First().Should().Be(expectedLogEntry);
   }

   #endregion
}
