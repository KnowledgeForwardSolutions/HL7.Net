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
   public void StringField_Constructor_ShouldReturnExpectedField_WhenFieldIsNotEmpty()
   {
      // Arrange.
      var line = "TST|This is a test...|This is only a test...|For the next sixty seconds...".AsSpan();
      var fieldEnumerator = line.ToFields(_encodingDetails.FieldSeparator);
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
   public void StringField_Constructor_ShouldLogExpectedEntry_WhenFieldIsNotEmpty()
   {
      // Arrange.
      var line = "TST|This is a test...|This is only a test...|For the next sixty seconds...".AsSpan();
      var fieldEnumerator = line.ToFields(_encodingDetails.FieldSeparator);
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
   public void StringField_Constructor_ShouldReturnExpectedField_WhenRawDataExceedsFieldLength()
   {
      // Arrange.
      var line = "TST|This is a test...|This is only a test...|For the next sixty seconds...".AsSpan();
      var fieldEnumerator = line.ToFields(_encodingDetails.FieldSeparator);
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
   public void StringField_Constructor_ShouldLogExpectedEntry_WhenRawDataExceedsFieldLength()
   {
      // Arrange.
      var line = "TST|This is a test...|This is only a test...|For the next sixty seconds...".AsSpan();
      var fieldEnumerator = line.ToFields(_encodingDetails.FieldSeparator);
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

   [Theory]
   [InlineData(Optionality.Optional)]
   [InlineData(Optionality.Required)]
   public void StringField_Constructor_ShouldReturnNotPresentInstance_WhenFieldIsEmpty(Optionality optionality)
   {
      // Arrange.
      var line = "TST||This is a test..".AsSpan();
      var fieldEnumerator = line.ToFields(_encodingDetails.FieldSeparator);
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
   public void StringField_Constructor_ShouldLogExpectedEntry_WhenOptionalFieldIsEmpty()
   {
      // Arrange.
      var line = "TST||This is a test..".AsSpan();
      var fieldEnumerator = line.ToFields(_encodingDetails.FieldSeparator);
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
   public void StringField_Constructor_ShouldLogExpectedEntry_WhenRequiredFieldIsEmpty()
   {
      // Arrange.
      var line = "TST||This is a test..".AsSpan();
      var fieldEnumerator = line.ToFields(_encodingDetails.FieldSeparator);
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

   [Fact]
   public void StringField_Constructor_ShouldReturnPresentButNullInstance_WhenFieldIsTwoDoubleQuotes()
   {
      // Arrange.
      var line = "TST|\"\"|This is a test..".AsSpan();
      var fieldEnumerator = line.ToFields(_encodingDetails.FieldSeparator);
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
   public void StringField_Constructor_ShouldLogExpectedEntry_WhenFieldIsTwoDoubleQuotes()
   {
      // Arrange.
      var line = "TST|\"\"|This is a test..".AsSpan();
      var fieldEnumerator = line.ToFields(_encodingDetails.FieldSeparator);
      fieldEnumerator.MoveNext();
      var log = new ProcessingLog();

      var message = String.Format(
         Messages.LogFieldPresentButNull,
         _fieldSpecification.FieldDescription);
      var rawData = "\"\"";
      var expectedLogEntry = new LogEntry(
         LogLevel.Information,
         message,
         _lineNumber,
         _fieldSpecification.FieldDescription,
         rawData);

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
   public void StringField_Constructor_ShouldReturnExpectedField_WhenRawDataContainsEscapedCharacters(Char escapedChar)
   {
      // Arrange.
      var line = $"TST|This is a test{_encodingDetails.EscapeCharacter}{escapedChar}This is only a test|abc|qwerty".AsSpan();
      var fieldEnumerator = line.ToFields(_encodingDetails.FieldSeparator);
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
   public void StringField_Constructor_ShouldLogExpectedEntry_WhenRawDataContainsEscapedCharacters(Char escapedChar)
   {
      // Arrange.
      var line = $"TST|This is a test{_encodingDetails.EscapeCharacter}{escapedChar}This is only a test|abc|qwerty".AsSpan();
      var fieldEnumerator = line.ToFields(_encodingDetails.FieldSeparator);
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
   public void StringField_Constructor_ShouldReturnExpectedField_WhenRawDataContainsEscapedCharactersAndDecodedValueExceedsFieldMaxLength(Char escapedChar)
   {
      // Arrange.
      var line = $"TST|This is a test{_encodingDetails.EscapeCharacter}{escapedChar}This is only a test|abc|qwerty".AsSpan();
      var fieldEnumerator = line.ToFields(_encodingDetails.FieldSeparator);
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
   public void StringField_Constructor_ShouldLogExpectedEntry_WhenRawDataContainsEscapedCharactersAndDecodedValueExceedsFieldMaxLength(Char escapedChar)
   {
      // Arrange.
      var line = $"TST|This is a test{_encodingDetails.EscapeCharacter}{escapedChar}This is only a test|abc|qwerty".AsSpan();
      var fieldEnumerator = line.ToFields(_encodingDetails.FieldSeparator);
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
