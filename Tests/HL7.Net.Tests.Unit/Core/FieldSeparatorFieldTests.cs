namespace HL7.Net.Tests.Unit.Core;

public class FieldSeparatorFieldTests
{
   private static readonly FieldSpecification _fieldSpecification = new(
      SegmentIDs.MessageHeaderSegment, 
      1, 
      "Field Separator", 
      1, 
      HL7Datatype.ST_String, 
      Optionality.Required, "N");
   private const Int32 _lineNumber = 10;

   #region Internal Constructor Tests
   // ==========================================================================
   // ==========================================================================

   [Fact]
   public void FieldSeparatorField_Constructor_ShouldCreateObject_WhenCharValueIsSupplied()
   {
      var value = '|';

      // Act.
      var sut = new FieldSeparatorField(value);

      // Assert.
      sut.Should().NotBeNull();
      sut.Value.Should().Be(value);
      sut.Presence.Should().Be(Presence.Present);
   }

   [Fact]
   public void FieldSeparatorField_Constructor_ShouldCreateObject_WhenCharValueAndFieldPresenceAreSupplied()
   {
      Char value = default!;

      // Act.
      var sut = new FieldSeparatorField(value, Presence.NotPresent);

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
   public void FieldSeparatorField_ImplicitStringConverter_ShouldReturnExpectedValue_WhenFieldIsNotEmpty()
   {
      // Arrange.
      var value = '|';
      var sut = new FieldSeparatorField(value);

      // Act.
      Char ch = sut;

      // Assert.
      ch.Should().Be(value);
   }

   #endregion

   #region NotPresent Instance Tests
   // ==========================================================================
   // ==========================================================================

   [Fact]
   public void FieldSeparatorField_NotPresent_ShouldReturnExpectedValue()
   {
      // Act.
      var sut = FieldSeparatorField.NotPresent;

      // Assert.
      sut.Should().NotBeNull();
      sut.Value.Should().Be('\0');
      sut.Presence.Should().Be(Presence.NotPresent);
   }

   #endregion

   #region Parse Method Tests
   // ==========================================================================
   // ==========================================================================

   [Fact]
   public void FieldSeparatorField_Parse_ShouldReturnExpectedValue_WhenFieldSeparatorIsPresent()
   {
      // Arrange.
      var line = @"MSH!^\~&!asdf...".AsSpan();
      var log = new ProcessingLog();

      var expected = new FieldSeparatorField('!');

      // Act.
      var field = FieldSeparatorField.Parse(
         line,
         _fieldSpecification,
         _lineNumber,
         log);

      // Assert.
      field.Should().Be(expected);
   }

   [Fact]
   public void FieldSeparatorField_Parse_ShouldLogExpectedEntry_WhenFieldContainsValidValue()
   {
      // Arrange.
      var line = @"MSH!^\~&!asdf...".AsSpan();
      var log = new ProcessingLog();

      var expectedLogEntry = LogEntry.GetFieldPresentEntry(
         _lineNumber,
         _fieldSpecification.FieldDescription,
         line.Slice(3, 1));

      // Act.
      _ = FieldSeparatorField.Parse(
         line,
         _fieldSpecification,
         _lineNumber,
         log);

      // Assert.
      log.Should().HaveCount(1);
      log.First().Should().Be(expectedLogEntry);
   }

   [Fact]
   public void FieldSeparatorField_Parse_ShouldReturnNotPresentInstance_WhenFieldSeparatorIsMissing()
   {
      // Arrange.
      var line = "MSH".AsSpan();
      var log = new ProcessingLog();

      var expected = FieldSeparatorField.NotPresent;

      // Act.
      var field = FieldSeparatorField.Parse(
         line,
         _fieldSpecification,
         _lineNumber,
         log);

      // Assert.
      field.Should().Be(expected);
   }

   [Fact]
   public void FieldSeparatorField_Parse_ShouldLogExpectedError_WhenFieldSeparatorIsMissing()
   {
      // Arrange.
      var line = "MSH".AsSpan();
      var log = new ProcessingLog();

      var expectedLogEntry = new LogEntry(
         LogLevel.FatalError,
         String.Format(Messages.MissingFieldSeparator, _fieldSpecification.FieldDescription),
         _lineNumber,
         _fieldSpecification.FieldDescription);

      // Act.
      _ = FieldSeparatorField.Parse(
         line,
         _fieldSpecification,
         _lineNumber,
         log);

      // Assert.
      log.Should().HaveCount(1);
      log.First().Should().Be(expectedLogEntry);
   }

   #endregion
}
