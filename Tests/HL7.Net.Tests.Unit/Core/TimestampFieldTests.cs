namespace HL7.Net.Tests.Unit.Core;

public class TimestampFieldTests
{
   private static readonly EncodingDetails _encodingDetails = EncodingDetails.DefaultEncodingDetails;
   private static readonly FieldSpecification _fieldSpecification = new(
      "MSH",
      1,
      "Date / Time Of Message",
      26,
      HL7Datatype.TS_Timestamp,
      Optionality.Required,
      "N");
   private static readonly TimeSpan _defaultTimezoneOffset = new TimeSpan(-1, 0, 0);
   private const Int32 _lineNumber = 10;

   #region NotPresent Instance Tests
   // ==========================================================================
   // ==========================================================================

   [Fact]
   public void TimestampField_NotPresent_ShouldReturnExpectedValue()
   {
      // Act.
      var sut = TimestampField.NotPresent;

      // Assert.
      sut.Should().NotBeNull();
      sut.Timestamp.Should().Be(DateTimeComponent.NotPresent);
      sut.DegreeOfPrecision.Should().Be(DegreeOfPrecisionComponent.NotPresent);
      sut.Presence.Should().Be(Presence.NotPresent);
   }

   #endregion

   #region PresentButNull Instance Tests
   // ==========================================================================
   // ==========================================================================

   [Fact]
   public void TimestampField_PresentButNull_ShouldReturnExpectedValue()
   {
      // Act.
      var sut = TimestampField.PresentButNull;

      // Assert.
      sut.Should().NotBeNull();
      sut.Timestamp.Should().Be(DateTimeComponent.NotPresent);
      sut.DegreeOfPrecision.Should().Be(DegreeOfPrecisionComponent.NotPresent);
      sut.Presence.Should().Be(Presence.PresentButNull);
   }

   #endregion

   #region Parse Method Tests
   // ==========================================================================
   // ==========================================================================

   [Fact]
   public void TimestampField_Parse_ShouldReturnExpectedValue_WhenAllComponentsSupplied()
   {
      // Arrange.
      var line = "MSH|198808181126^D|asdf".AsSpan();
      var fieldEnumerator = line.ToFields(_encodingDetails.FieldSeparator, _encodingDetails.EscapeCharacter);
      fieldEnumerator.MoveNext();
      var log = new ProcessingLog();

      var expectedTimestamp = new DateTimeComponent(new DateTimeOffset(1988, 8, 18, 11, 26, 0, _defaultTimezoneOffset));
      var expectedDegreeOfPrecision = new DegreeOfPrecisionComponent('D');
      var expectedFieldPresence = Presence.Present;

      // Act.
      var field = TimestampField.Parse(
         ref fieldEnumerator,
         _encodingDetails,
         _fieldSpecification,
         _defaultTimezoneOffset,
         _lineNumber,
         log);

      // Assert.
      field.Should().NotBeNull(); 

      field.Timestamp.Should().Be(expectedTimestamp);
      field.DegreeOfPrecision.Should().Be(expectedDegreeOfPrecision);   
      field.Presence.Should().Be(expectedFieldPresence);
   }

   [Fact]
   public void TimestampField_Parse_ShouldReturnExpectedValue_WhenDegreeOfPrecisionIsNotSupplied()
   {
      // Arrange.
      var line = "MSH|198808181126|asdf".AsSpan();
      var fieldEnumerator = line.ToFields(_encodingDetails.FieldSeparator, _encodingDetails.EscapeCharacter);
      fieldEnumerator.MoveNext();
      var log = new ProcessingLog();

      var expectedTimestamp = new DateTimeComponent(new DateTimeOffset(1988, 8, 18, 11, 26, 0, _defaultTimezoneOffset));
      var expectedDegreeOfPrecision = DegreeOfPrecisionComponent.NotPresent;
      var expectedFieldPresence = Presence.Present;

      // Act.
      var field = TimestampField.Parse(
         ref fieldEnumerator,
         _encodingDetails,
         _fieldSpecification,
         _defaultTimezoneOffset,
         _lineNumber,
         log);

      // Assert.
      field.Should().NotBeNull();

      field.Timestamp.Should().Be(expectedTimestamp);
      field.DegreeOfPrecision.Should().Be(expectedDegreeOfPrecision);
      field.Presence.Should().Be(expectedFieldPresence);
   }

   [Fact]
   public void TimestampField_Parse_ShouldReportMissingRequiredField_WhenTimestampIsNotSupplied()
   {
      // Arrange.
      var line = "MSH|^H|asdf".AsSpan();
      var fieldEnumerator = line.ToFields(_encodingDetails.FieldSeparator, _encodingDetails.EscapeCharacter);
      fieldEnumerator.MoveNext();
      var log = new ProcessingLog();

      var expectedTimestamp = DateTimeComponent.NotPresent;
      var expectedDegreeOfPrecision = new DegreeOfPrecisionComponent('H');
      var expectedFieldPresence = Presence.Present;

      var message = String.Format(Messages.LogRequiredFieldNotPresent, "TS.1/DateTime");
      var expectedLogEntry = new LogEntry(LogLevel.Error, message, _lineNumber, "TS.1/DateTime");

      // Act.
      var field = TimestampField.Parse(
         ref fieldEnumerator,
         _encodingDetails,
         _fieldSpecification,
         _defaultTimezoneOffset,
         _lineNumber,
         log);

      // Assert.
      field.Should().NotBeNull();

      field.Timestamp.Should().Be(expectedTimestamp);
      field.DegreeOfPrecision.Should().Be(expectedDegreeOfPrecision);
      field.Presence.Should().Be(expectedFieldPresence);

      log.HighestLogLevel.Should().Be(LogLevel.Error);
      log.Should().Contain(expectedLogEntry);
   }

   [Theory]
   [InlineData(Optionality.Optional)]
   [InlineData(Optionality.Required)]
   public void TimestampField_Parse_ShouldReturnNotPresentInstance_WhenFieldIsBeyondEndOfSuppliedFields(Optionality optionality)
   {
      // Arrange.
      var line = "MSH|asdf".AsSpan();
      var fieldEnumerator = line.ToFields(_encodingDetails.FieldSeparator, _encodingDetails.EscapeCharacter);
      fieldEnumerator.MoveNext();
      fieldEnumerator.MoveNext();
      var log = new ProcessingLog();
      var fieldSpecification = _fieldSpecification with { Optionality = optionality, Sequence = 2 };

      // Act.
      var field = TimestampField.Parse(
         ref fieldEnumerator,
         _encodingDetails,
         fieldSpecification,
         _defaultTimezoneOffset,
         _lineNumber,
         log);

      // Assert.
      field.Should().Be(TimestampField.NotPresent);
   }

   [Fact]
   public void TimestampField_Parse_ShouldLogFieldNotPresent_WhenOptionalFieldIsBeyondEndOfSuppliedFields()
   {
      // Arrange.
      var line = "MSH|asdf".AsSpan();
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
      _ = TimestampField.Parse(
         ref fieldEnumerator,
         _encodingDetails,
         fieldSpecification,
         _defaultTimezoneOffset,
         _lineNumber,
         log);

      // Assert.
      log.Should().HaveCount(1);
      log.First().Should().Be(expectedLogEntry);
   }

   [Fact]
   public void TimestampField_Parse_ShouldLogRequiredFieldNotPresent_WhenRequiredFieldIsBeyondEndOfSuppliedFields()
   {
      // Arrange.
      var line = "MSH|asdf".AsSpan();
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
      _ = TimestampField.Parse(
         ref fieldEnumerator,
         _encodingDetails,
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
   public void TimestampField_Parse_ShouldReturnNotPresentInstance_WhenFieldIsEmpty(Optionality optionality)
   {
      // Arrange.
      var line = "MSH||asdf".AsSpan();
      var fieldEnumerator = line.ToFields(_encodingDetails.FieldSeparator, _encodingDetails.EscapeCharacter);
      fieldEnumerator.MoveNext();
      var log = new ProcessingLog();
      var fieldSpecification = _fieldSpecification with { Optionality = optionality };

      // Act.
      var field = TimestampField.Parse(
         ref fieldEnumerator,
         _encodingDetails,
         fieldSpecification,
         _defaultTimezoneOffset,
         _lineNumber,
         log);

      // Assert.
      field.Should().Be(TimestampField.NotPresent);
   }

   [Fact]
   public void TimestampField_Parse_ShouldLogFieldNotPresent_WhenOptionalFieldIsEmpty()
   {
      // Arrange.
      var line = "MSH||asdf".AsSpan();
      var fieldEnumerator = line.ToFields(_encodingDetails.FieldSeparator, _encodingDetails.EscapeCharacter);
      fieldEnumerator.MoveNext();
      var log = new ProcessingLog();
      var fieldSpecification = _fieldSpecification with { Optionality = Optionality.Optional };

      var message = String.Format(
         Messages.LogFieldNotPresent,
         fieldSpecification.FieldDescription);
      var expectedLogEntry = new LogEntry(
         LogLevel.Information,
         message,
         _lineNumber,
         fieldSpecification.FieldDescription);

      // Act.
      _ = TimestampField.Parse(
         ref fieldEnumerator,
         _encodingDetails,
         fieldSpecification,
         _defaultTimezoneOffset,
         _lineNumber,
         log);

      // Assert.
      log.Should().HaveCount(1);
      log.First().Should().Be(expectedLogEntry);
   }

   [Fact]
   public void TimestampField_Parse_ShouldLogRequiredFieldNotPresent_WhenRequiredFieldIsEmpty()
   {
      // Arrange.
      var line = "MSH||asdf".AsSpan();
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
      _ = TimestampField.Parse(
         ref fieldEnumerator,
         _encodingDetails,
         fieldSpecification,
         _defaultTimezoneOffset,
         _lineNumber,
         log);

      // Assert.
      log.Should().HaveCount(1);
      log.First().Should().Be(expectedLogEntry);
   }

   [Fact]
   public void TimestampField_Parse_ShouldReturnPresentButNullInstance_WhenFieldIsTwoDoubleQuotes()
   {
      // Arrange.
      var line = "MSH|\"\"|asdf".AsSpan();
      var fieldEnumerator = line.ToFields(_encodingDetails.FieldSeparator, _encodingDetails.EscapeCharacter);
      fieldEnumerator.MoveNext();
      var log = new ProcessingLog();

      // Act.
      var field = TimestampField.Parse(
         ref fieldEnumerator,
         _encodingDetails,
         _fieldSpecification,
         _defaultTimezoneOffset,
         _lineNumber,
         log);

      // Assert.
      field.Should().Be(TimestampField.PresentButNull);
   }

   [Fact]
   public void TimestampField_Parse_ShouldLogFieldPresentButNull_WhenFieldIsTwoDoubleQuotes()
   {
      // Arrange.
      var line = "MSH|\"\"|asdf".AsSpan();
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
      _ = TimestampField.Parse(
         ref fieldEnumerator,
         _encodingDetails,
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
