namespace HL7.Net.Tests.Unit.Core;

public class LogEntryTests
{
   private static readonly FieldSpecification _fieldSpecification = new(
      "TST",
      1,
      "Test Field",
      42,
      HL7Datatype.ST_String,
      Optionality.Optional,
      "N");
   private const Int32 _lineNumber = 10;
   private const String _fieldContents = "This is a test";

   #region GetFieldPresentEntry Method Tests
   // ==========================================================================
   // ==========================================================================

   [Fact]
   public void LogEntry_GetFieldPresentEntry_ShouldReturnExpectedValue()
   {
      // Arrange.
      var span = _fieldContents.AsSpan();

      var expected = new LogEntry(
         LogLevel.Information,
         String.Format(Messages.LogFieldPresent, _fieldSpecification.FieldDescription),
         _lineNumber,
         _fieldSpecification.FieldDescription,
         _fieldContents);

      // Act.
      var entry = LogEntry.GetFieldPresentEntry(
         _lineNumber, 
         _fieldSpecification.FieldDescription,
         span);

      // Assert.
      entry.Should().Be(expected);
   }

   #endregion

   #region GetFieldPresentButNullEntry Method Tests
   // ==========================================================================
   // ==========================================================================

   [Fact]
   public void LogEntry_GetFieldPresentButNullEntry_ShouldReturnExpectedValue()
   {
      // Arrange.
      var expected = new LogEntry(
         LogLevel.Information,
         String.Format(Messages.LogFieldPresentButNull, _fieldSpecification.FieldDescription),
         _lineNumber,
         _fieldSpecification.FieldDescription,
         GeneralConstants.PresentButNullValue);

      // Act.
      var entry = LogEntry.GetFieldPresentButNullEntry(
         _lineNumber,
         _fieldSpecification.FieldDescription);

      // Assert.
      entry.Should().Be(expected);
   }

   #endregion

   #region GetFieldPresentButTruncatedEntry Method Tests
   // ==========================================================================
   // ==========================================================================

   [Fact]
   public void LogEntry_GetFieldPresentButTruncatedEntry_ShouldReturnExpectedValue()
   {
      // Arrange.
      var fieldContents = "ABCDEFGHIJKLMNOPQRSTUVWXYZ_1234567890_abcdefghijklmnopqrstuvwxyz";
      var span = fieldContents.AsSpan();

      var expected = new LogEntry(
         LogLevel.Warning,
         String.Format(Messages.LogFieldPresentButTruncated, _fieldSpecification.FieldDescription, _fieldSpecification.Length),
         _lineNumber,
         _fieldSpecification.FieldDescription,
         fieldContents);

      // Act.
      var entry = LogEntry.GetFieldPresentButTruncatedEntry(
         _lineNumber,
         _fieldSpecification.FieldDescription,
         _fieldSpecification.Length,
         span);

      // Assert.
      entry.Should().Be(expected);
   }

   #endregion

   #region GetOptionalFieldNotPresentEntry Method Tests
   // ==========================================================================
   // ==========================================================================

   [Fact]
   public void LogEntry_GetOptionalFieldNotPresentEntry_ShouldReturnExpectedValue()
   {
      // Arrange.
      var expected = new LogEntry(
         LogLevel.Information,
         String.Format(Messages.LogFieldNotPresent, _fieldSpecification.FieldDescription),
         _lineNumber,
         _fieldSpecification.FieldDescription);

      // Act.
      var entry = LogEntry.GetOptionalFieldNotPresentEntry(
         _lineNumber,
         _fieldSpecification.FieldDescription);

      // Assert.
      entry.Should().Be(expected);
   }

   #endregion

   #region GetRequiredFieldNotPresentEntry Method Tests
   // ==========================================================================
   // ==========================================================================

   [Fact]
   public void LogEntry_GetRequiredFieldNotPresentEntry_ShouldReturnExpectedValue()
   {
      // Arrange.
      var expected = new LogEntry(
         LogLevel.Error,
         String.Format(Messages.LogRequiredFieldNotPresent, _fieldSpecification.FieldDescription),
         _lineNumber,
         _fieldSpecification.FieldDescription);

      // Act.
      var entry = LogEntry.GetRequiredFieldNotPresentEntry(
         _lineNumber,
         _fieldSpecification.FieldDescription);

      // Assert.
      entry.Should().Be(expected);
   }

   #endregion
}
