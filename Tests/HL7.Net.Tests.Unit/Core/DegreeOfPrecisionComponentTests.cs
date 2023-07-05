namespace HL7.Net.Tests.Unit.Core;

public class DegreeOfPrecisionComponentTests
{
   private static readonly EncodingDetails _encodingDetails = EncodingDetails.DefaultEncodingDetails;
   private static readonly FieldSpecification _fieldSpecification = new(
      "TST",
      1,
      "DegreeOfPrecision",
      1,
      HL7Datatype.Other_TimestampDegreeOfPrecision,
      Optionality.Optional,
      "N");
   private const Int32 _lineNumber = 10;

   #region Internal Constructor Tests
   // ==========================================================================
   // ==========================================================================

   [Fact]
   public void DegreeOfPrecisionComponent_Constructor_ShouldCreateObject_WhenValueIsSupplied()
   {
      var value = 'Y';

      // Act.
      var sut = new DegreeOfPrecisionComponent(value);

      // Assert.
      sut.Should().NotBeNull();
      sut.Value.Should().Be(value);
      sut.Presence.Should().Be(Presence.Present);
   }

   [Fact]
   public void DegreeOfPrecisionComponent_Constructor_ShouldCreateObject_WhenValueAndFieldPresenceAreSupplied()
   {
      Char? value = null!;

      // Act.
      var sut = new DegreeOfPrecisionComponent(value, Presence.NotPresent);

      // Assert.
      sut.Should().NotBeNull();
      sut.Value.Should().Be(value);
      sut.Presence.Should().Be(Presence.NotPresent);
   }

   #endregion

   #region Implicit Char Converter Tests
   // ==========================================================================
   // ==========================================================================

   [Fact]
   public void DegreeOfPrecisionComponent_ImplicitCharConverter_ShouldReturnExpectedValue_WhenFieldIsNotEmpty()
   {
      // Arrange.
      var value = 'L';
      var sut = new DegreeOfPrecisionComponent(value);

      // Act.
      Char? result = sut;

      // Assert.
      result.Should().Be(value);
   }

   [Fact]
   public void DegreeOfPrecisionComponent_ImplicitCharConverter_ShouldReturnExpectedValue_WhenFieldIsNotPresent()
   {
      // Arrange.
      var sut = DegreeOfPrecisionComponent.NotPresent;

      // Act.
      Char? result = sut;

      // Assert.
      result.Should().BeNull();
   }

   #endregion

   #region NotPresent Instance Tests
   // ==========================================================================
   // ==========================================================================

   [Fact]
   public void DegreeOfPrecisionComponent_NotPresent_ShouldReturnExpectedValue()
   {
      // Act.
      var sut = DegreeOfPrecisionComponent.NotPresent;

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
   public void DegreeOfPrecisionComponent_PresentButNull_ShouldReturnExpectedValue()
   {
      // Act.
      var sut = DegreeOfPrecisionComponent.PresentButNull;

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
   [InlineData('Y')]
   [InlineData('L')]
   [InlineData('D')]
   [InlineData('H')]
   [InlineData('M')]
   [InlineData('S')]
   public void DegreeOfPrecisionComponent_Parse_ShouldReturnIntegerValue_WhenFieldContainsValidDegreeOfPrecisionValue(Char fieldContents)
   {
      // Arrange.
      var line = $"20230608192021.2234^{fieldContents}".AsSpan();
      var fieldEnumerator = line.ToFields(_encodingDetails.ComponentSeparator, _encodingDetails.EscapeCharacter);
      fieldEnumerator.MoveNext();
      var log = new ProcessingLog();

      var expected = new DegreeOfPrecisionComponent(fieldContents);

      // Act.
      var field = DegreeOfPrecisionComponent.Parse(
         ref fieldEnumerator,
         _fieldSpecification,
         _lineNumber,
         log);

      // Assert.
      field.Should().Be(expected);
   }

   [Fact]
   public void DegreeOfPrecisionComponent_Parse_ShouldLogExpectedEntry_WhenFieldContainsValidDegreeOfPrecisionValue()
   {
      // Arrange.
      var fieldContents = "H";
      var line = $"20230608192021.2234^{fieldContents}".AsSpan();
      var fieldEnumerator = line.ToFields(_encodingDetails.ComponentSeparator, _encodingDetails.EscapeCharacter);
      fieldEnumerator.MoveNext();
      var log = new ProcessingLog();

      var expectedLogEntry = LogEntry.GetFieldPresentEntry(
         _lineNumber,
         _fieldSpecification.FieldDescription,
         fieldContents);

      // Act.
      _ = DegreeOfPrecisionComponent.Parse(
         ref fieldEnumerator,
         _fieldSpecification,
         _lineNumber,
         log);

      // Assert.
      log.Should().HaveCount(1);
      log.First().Should().Be(expectedLogEntry);
   }

   [Theory]
   [InlineData("X")]
   [InlineData("YY")]
   [InlineData(" Y")]
   [InlineData("Y ")]
   [InlineData(" Y ")]
   public void DegreeOfPrecisionComponent_Parse_ShouldReturnNotPresentInstance_WhenFieldContainsAnInvalidValue(String fieldContents)
   {
      // Arrange.
      var line = $"20230608192021.2234^{fieldContents}".AsSpan();
      var fieldEnumerator = line.ToFields(_encodingDetails.ComponentSeparator, _encodingDetails.EscapeCharacter);
      fieldEnumerator.MoveNext();
      var log = new ProcessingLog();

      // Act.
      var field = DegreeOfPrecisionComponent.Parse(
         ref fieldEnumerator,
         _fieldSpecification,
         _lineNumber,
         log);

      // Assert.
      field.Should().Be(DegreeOfPrecisionComponent.NotPresent);
   }

   [Theory]
   [InlineData("X")]
   [InlineData("YY")]
   [InlineData(" Y")]
   [InlineData("Y ")]
   [InlineData(" Y ")]
   public void DegreeOfPrecisionComponent_Parse_ShouldLogError_WhenFieldContainsAnInvalidValue(String fieldContents)
   {
      // Arrange.
      var line = $"20230608192021.2234^{fieldContents}".AsSpan();
      var fieldEnumerator = line.ToFields(_encodingDetails.ComponentSeparator, _encodingDetails.EscapeCharacter);
      fieldEnumerator.MoveNext();
      var log = new ProcessingLog();

      var message = String.Format(
         Messages.InvalidTimestampDegreeOfPrecision,
         _fieldSpecification.FieldDescription);
      var expectedLogEntry = new LogEntry(
         LogLevel.Error,
         message,
         _lineNumber,
         _fieldSpecification.FieldDescription,
         fieldContents);

      // Act.
      _ = DegreeOfPrecisionComponent.Parse(
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
   public void DegreeOfPrecisionComponent_Parse_ShouldReturnNotPresentInstance_WhenFieldIsBeyondEndOfSuppliedFields(Optionality optionality)
   {
      // Arrange.
      var line = $"20230608192021.2234".AsSpan();
      var fieldEnumerator = line.ToFields(_encodingDetails.ComponentSeparator, _encodingDetails.EscapeCharacter);
      fieldEnumerator.MoveNext();
      fieldEnumerator.MoveNext();
      var log = new ProcessingLog();
      var fieldSpecification = _fieldSpecification with { Optionality = optionality, Sequence = 2 };

      // Act.
      var field = DegreeOfPrecisionComponent.Parse(
         ref fieldEnumerator,
         fieldSpecification,
         _lineNumber,
         log);

      // Assert.
      field.Should().Be(DegreeOfPrecisionComponent.NotPresent);
   }

   [Fact]
   public void DegreeOfPrecisionComponent_Parse_ShouldLogFieldNotPresent_WhenOptionalFieldIsBeyondEndOfSuppliedFields()
   {
      // Arrange.
      var line = $"20230608192021.2234".AsSpan();
      var fieldEnumerator = line.ToFields(_encodingDetails.ComponentSeparator, _encodingDetails.EscapeCharacter);
      fieldEnumerator.MoveNext();
      fieldEnumerator.MoveNext();
      var log = new ProcessingLog();
      var fieldSpecification = _fieldSpecification with { Optionality = Optionality.Optional, Sequence = 2 };

      var expectedLogEntry = LogEntry.GetOptionalFieldNotPresentEntry(
         _lineNumber,
         fieldSpecification.FieldDescription);

      // Act.
      _ = DegreeOfPrecisionComponent.Parse(
         ref fieldEnumerator,
         fieldSpecification,
         _lineNumber,
         log);

      // Assert.
      log.Should().HaveCount(1);
      log.First().Should().Be(expectedLogEntry);
   }

   [Fact]
   public void DegreeOfPrecisionComponent_Parse_ShouldLogRequiredFieldNotPresent_WhenRequiredFieldIsBeyondEndOfSuppliedFields()
   {
      // Arrange.
      var line = $"20230608192021.2234".AsSpan();
      var fieldEnumerator = line.ToFields(_encodingDetails.ComponentSeparator, _encodingDetails.EscapeCharacter);
      fieldEnumerator.MoveNext();
      fieldEnumerator.MoveNext();
      var log = new ProcessingLog();
      var fieldSpecification = _fieldSpecification with { Optionality = Optionality.Required, Sequence = 2 };

      var expectedLogEntry = LogEntry.GetRequiredFieldNotPresentEntry(
         _lineNumber,
         fieldSpecification.FieldDescription);

      // Act.
      _ = DegreeOfPrecisionComponent.Parse(
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
   public void DegreeOfPrecisionComponent_Parse_ShouldReturnNotPresentInstance_WhenFieldIsEmpty(Optionality optionality)
   {
      // Arrange.
      var line = $"20230608192021.2234^".AsSpan();
      var fieldEnumerator = line.ToFields(_encodingDetails.ComponentSeparator, _encodingDetails.EscapeCharacter);
      fieldEnumerator.MoveNext();
      var log = new ProcessingLog();
      var fieldSpecification = _fieldSpecification with { Optionality = optionality };

      // Act.
      var field = DegreeOfPrecisionComponent.Parse(
         ref fieldEnumerator,
         fieldSpecification,
         _lineNumber,
         log);

      // Assert.
      field.Should().Be(DegreeOfPrecisionComponent.NotPresent);
   }

   [Fact]
   public void DegreeOfPrecisionComponent_Parse_ShouldLogFieldNotPresent_WhenOptionalFieldIsEmpty()
   {
      // Arrange.
      var line = $"20230608192021.2234^".AsSpan();
      var fieldEnumerator = line.ToFields(_encodingDetails.ComponentSeparator, _encodingDetails.EscapeCharacter);
      fieldEnumerator.MoveNext();
      var log = new ProcessingLog();
      var fieldSpecification = _fieldSpecification with { Optionality = Optionality.Optional };

      var expectedLogEntry = LogEntry.GetOptionalFieldNotPresentEntry(
         _lineNumber,
         fieldSpecification.FieldDescription);

      // Act.
      _ = DegreeOfPrecisionComponent.Parse(
         ref fieldEnumerator,
         fieldSpecification,
         _lineNumber,
         log);

      // Assert.
      log.Should().HaveCount(1);
      log.First().Should().Be(expectedLogEntry);
   }

   [Fact]
   public void DegreeOfPrecisionComponent_Parse_ShouldLogRequiredFieldNotPresent_WhenRequiredFieldIsEmpty()
   {
      // Arrange.
      var line = $"20230608192021.2234^".AsSpan();
      var fieldEnumerator = line.ToFields(_encodingDetails.ComponentSeparator, _encodingDetails.EscapeCharacter);
      fieldEnumerator.MoveNext();
      var log = new ProcessingLog();
      var fieldSpecification = _fieldSpecification with { Optionality = Optionality.Required };

      var expectedLogEntry = LogEntry.GetRequiredFieldNotPresentEntry(
         _lineNumber,
         fieldSpecification.FieldDescription);

      // Act.
      _ = DegreeOfPrecisionComponent.Parse(
         ref fieldEnumerator,
         fieldSpecification,
         _lineNumber,
         log);

      // Assert.
      log.Should().HaveCount(1);
      log.First().Should().Be(expectedLogEntry);
   }

   [Fact]
   public void DegreeOfPrecisionComponent_Parse_ShouldReturnPresentButNullInstance_WhenFieldIsTwoDoubleQuotes()
   {
      // Arrange.
      var line = $"20230608192021.2234^\"\"".AsSpan();
      var fieldEnumerator = line.ToFields(_encodingDetails.ComponentSeparator, _encodingDetails.EscapeCharacter);
      fieldEnumerator.MoveNext();
      var log = new ProcessingLog();

      // Act.
      var field = DegreeOfPrecisionComponent.Parse(
         ref fieldEnumerator,
         _fieldSpecification,
         _lineNumber,
         log);

      // Assert.
      field.Should().Be(DegreeOfPrecisionComponent.PresentButNull);
   }

   [Fact]
   public void DegreeOfPrecisionComponent_Parse_ShouldLogFieldPresentButNull_WhenFieldIsTwoDoubleQuotes()
   {
      // Arrange.
      var line = $"20230608192021.2234^\"\"".AsSpan();
      var fieldEnumerator = line.ToFields(_encodingDetails.ComponentSeparator, _encodingDetails.EscapeCharacter);
      fieldEnumerator.MoveNext();
      var log = new ProcessingLog();

      var expectedLogEntry = LogEntry.GetFieldPresentButNullEntry(
         _lineNumber,
         _fieldSpecification.FieldDescription);

      // Act.
      _ = DegreeOfPrecisionComponent.Parse(
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
