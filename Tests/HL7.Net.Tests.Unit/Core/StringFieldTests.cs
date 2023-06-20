namespace HL7.Net.Tests.Unit.Core;

public class StringFieldTests
{
   private static readonly EncodingDetails _encodingDetails = new(
      DefaultSeparators.FieldSeparator,
      DefaultSeparators.ComponentSeparator,
      DefaultSeparators.RepetitionSeparator,
      DefaultSeparators.EscapeCharacter,
      DefaultSeparators.SubComponentSeparator);
   private static readonly FieldSpecification _fieldSpecification = new(
      "TST",
      1,
      "Test Field",
      15,
      HL7Datatype.ST_String,
      Optionality.Optional,
      "N");
   private const Int32 _lineNumber = 10;

   #region Internal Constructor Tests
   // ==========================================================================
   // ==========================================================================

   [Fact]
   public void StringField_Constructor_ShouldReturnNotPresentInstance_WhenFieldIsEmpty()
   {
      // Arrange.
      var line = "TST||This is a test..".AsSpan();
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
      field.Should().Be(StringField.NotPresent);
   }

   [Fact]
   public void StringField_Constructor_ShouldLogNotPresentField_WhenFieldIsEmpty()
   {
      // Arrange.
      var line = "TST||This is a test..".AsSpan();
      var fieldEnumerator = line.ToFields(_encodingDetails.FieldSeparator);
      fieldEnumerator.MoveNext();
      var log = new ProcessingLog();

      var expectedLogMessage = String.Format(
         Messages.LogFieldNotPresent,
         _fieldSpecification.FieldDescription);

      // Act.
      var field = StringField.Parse(
         ref fieldEnumerator,
         _encodingDetails,
         _fieldSpecification,
         _lineNumber,
         log);

      // Assert.
      log.Should().HaveCount(1);
      var entry = log.First();
      entry.LogLevel.Should().Be(LogLevel.Information);
      entry.Message.Should().Be(expectedLogMessage);
      entry.FieldDescription.Should().Be(_fieldSpecification.FieldDescription);
      entry.LineNumber.Should().Be(_lineNumber);
      entry.RawData.Should().BeNull();
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
   public void StringField_Constructor_ShouldLogPresentButNullField_WhenFieldIsTwoDoubleQuotes()
   {
      // Arrange.
      var line = "TST|\"\"|This is a test..".AsSpan();
      var fieldEnumerator = line.ToFields(_encodingDetails.FieldSeparator);
      fieldEnumerator.MoveNext();
      var log = new ProcessingLog();

      var expectedLogMessage = String.Format(
         Messages.LogFieldPresentButNull,
         _fieldSpecification.FieldDescription);
      var expectedRawData = "\"\"";

      // Act.
      var field = StringField.Parse(
         ref fieldEnumerator,
         _encodingDetails,
         _fieldSpecification,
         _lineNumber,
         log);

      // Assert.
      log.Should().HaveCount(1);
      var entry = log.First();
      entry.LogLevel.Should().Be(LogLevel.Information);
      entry.Message.Should().Be(expectedLogMessage);
      entry.FieldDescription.Should().Be(_fieldSpecification.FieldDescription);
      entry.LineNumber.Should().Be(_lineNumber);
      entry.RawData.Should().Be(expectedRawData);
   }

   #endregion
}
