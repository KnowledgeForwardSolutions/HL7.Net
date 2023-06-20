namespace HL7.Net.Tests.Unit.Core;

public class ProcessingLogTests
{
   private static readonly FieldSpecification _fieldSpecification = new(
      "TST",
      1,
      "Test Field",
      15,
      HL7Datatype.ST_String,
      Optionality.Optional,
      "N");
   private const Int32 _lineNumber = 42;

   #region Internal Constructor Tests
   // ==========================================================================
   // ==========================================================================

   [Fact]
   public void ProcessingLog_Constructor_ShouldCreateEmptyLog()
   {
      // Act.
      var sut = new ProcessingLog();

      // Assert.
      sut.Should().NotBeNull();
      sut.Should().BeEmpty();
      sut.Count.Should().Be(0);
      sut.HighestLogLevel.Should().Be(LogLevel.Information);
   }

   #endregion

   #region Inherent Enumerable Tests
   // ==========================================================================
   // ==========================================================================

   [Fact]
   public void ProcessingLog_InherentEnumerable_ShouldReturnEmpty_WhenLogIsEmpty()
   {
      // Arrange.
      var sut = new ProcessingLog();

      // Act.
      var entries = sut.ToList();

      // Assert.
      entries.Should().BeEmpty();
   }

   [Fact]
   public void ProcessingLog_InherentEnumerable_ShouldReturnExpectedItems_WhenLogIsPopulated()
   {
      // Arrange.
      var sut = new ProcessingLog();
      sut.LogInformation("Information", 1);
      sut.LogWarning("Warning", 2);
      sut.LogError("Error", 3);
      sut.LogFatalError("FatalError", 4);

      var expected = new List<LogEntry>
      {
         new LogEntry(LogLevel.Information, "Information", 1),
         new LogEntry(LogLevel.Warning, "Warning", 2),
         new LogEntry(LogLevel.Error, "Error", 3),
         new LogEntry(LogLevel.FatalError, "FatalError", 4),
      };

      // Assert.
      sut.Should().Equal(expected);
   }

   #endregion

   #region Count Property Tests
   // ==========================================================================
   // ==========================================================================

   [Theory]
   [InlineData(0)]
   [InlineData(1)]
   [InlineData(5)]
   public void ProcessingLog_Count_ShouldReflectNumberOfLogEntries(Int32 numItems)
   {
      // Arrange.
      var sut = new ProcessingLog();

      // Act.
      for(var i = 0; i < numItems; i++)
      {
         sut.LogInformation("This is a test", i);
      }

      // Assert.
      sut.Count.Should().Be(numItems);
      sut.Should().HaveCount(numItems);
   }

   #endregion

   #region HighestLogLevel Property Tests
   // ==========================================================================
   // ==========================================================================

   [Fact]
   public void ProcessingLog_HighestLogLevel_ShouldBeInformation_WhenLogIsEmpty()
   {
      // Arrange.
      var sut = new ProcessingLog();

      // Assert.
      sut.HighestLogLevel.Should().Be(LogLevel.Information);
   }

   [Fact]
   public void ProcessingLog_HighestLogLevel_ShouldBeWarning_WhenWarningIsTheMostSevereEntry()
   {
      // Arrange.
      var sut = new ProcessingLog();

      // Act.
      sut.LogInformation("Information", 1);
      sut.LogWarning("Warning", 2);
      sut.LogInformation("Information", 3);

      // Assert.
      sut.HighestLogLevel.Should().Be(LogLevel.Warning);
   }

   [Fact]
   public void ProcessingLog_HighestLogLevel_ShouldBeError_WhenErrorIsTheMostSevereEntry()
   {
      // Arrange.
      var sut = new ProcessingLog();

      // Act.
      sut.LogInformation("Information", 1);
      sut.LogWarning("Warning", 2);
      sut.LogError("Error", 3);
      sut.LogWarning("Warning", 4);
      sut.LogInformation("Information", 5);

      // Assert.
      sut.HighestLogLevel.Should().Be(LogLevel.Error);
   }

   [Fact]
   public void ProcessingLog_HighestLogLevel_ShouldBeFatalError_WhenFatalErrorIsTheMostSevereEntry()
   {
      // Arrange.
      var sut = new ProcessingLog();

      // Act.
      sut.LogInformation("Information", 1);
      sut.LogWarning("Warning", 2);
      sut.LogError("Error", 3);
      sut.LogFatalError("Fatal Error", 4);
      sut.LogError("Error", 5);
      sut.LogWarning("Warning", 6);
      sut.LogInformation("Information", 7);

      // Assert.
      sut.HighestLogLevel.Should().Be(LogLevel.FatalError);
   }

   #endregion

   #region LogError Method Tests
   // ==========================================================================
   // ==========================================================================

   [Fact]
   public void ProcessingLog_LogError_ShouldAddExpectedEntry_WhenOnlyRequiredParametersAreIncluded()
   {
      // Arrange.
      var sut = new ProcessingLog();

      var message = "My message";

      var expectedEntry = new LogEntry(LogLevel.Error, message, _lineNumber);

      // Act.
      sut.LogError(message, _lineNumber);

      // Assert.
      sut.First().Should().Be(expectedEntry);
   }

   [Fact]
   public void ProcessingLog_LogError_ShouldAddExpectedEntry_WhenFieldSpecificationIsIncluded()
   {
      // Arrange.
      var sut = new ProcessingLog();

      var message = "My message";

      var expectedEntry = new LogEntry(
         LogLevel.Error, 
         message, 
         _lineNumber, 
         _fieldSpecification.FieldDescription);

      // Act.
      sut.LogError(message, _lineNumber, _fieldSpecification);

      // Assert.
      sut.First().Should().Be(expectedEntry);
   }

   [Fact]
   public void ProcessingLog_LogError_ShouldAddExpectedEntry_WhenRawDataIsIncluded()
   {
      // Arrange.
      var sut = new ProcessingLog();

      var message = "My message";
      var rawData = "this is a test...this is only a test...";

      var expectedEntry = new LogEntry(
         LogLevel.Error,
         message,
         _lineNumber,
         RawData: rawData);

      // Act.
      sut.LogError(message, _lineNumber, rawData: rawData);

      // Assert.
      sut.First().Should().Be(expectedEntry);
   }

   [Fact]
   public void ProcessingLog_LogError_ShouldAddExpectedEntry_WhenAllParametersAreIncluded()
   {
      // Arrange.
      var sut = new ProcessingLog();

      var message = "My message";
      var rawData = "this is a test...this is only a test...";

      var expectedEntry = new LogEntry(
         LogLevel.Error,
         message,
         _lineNumber,
         _fieldSpecification.FieldDescription,
         rawData);

      // Act.
      sut.LogError(message, _lineNumber, _fieldSpecification, rawData);

      // Assert.
      sut.First().Should().Be(expectedEntry);
   }

   #endregion

   #region LogFatalError Method Tests
   // ==========================================================================
   // ==========================================================================

   [Fact]
   public void ProcessingLog_LogFatalError_ShouldAddExpectedEntry_WhenOnlyRequiredParametersAreIncluded()
   {
      // Arrange.
      var sut = new ProcessingLog();

      var message = "My message";

      var expectedEntry = new LogEntry(LogLevel.FatalError, message, _lineNumber);

      // Act.
      sut.LogFatalError(message, _lineNumber);

      // Assert.
      sut.First().Should().Be(expectedEntry);
   }

   [Fact]
   public void ProcessingLog_LogFatalError_ShouldAddExpectedEntry_WhenFieldSpecificationIsIncluded()
   {
      // Arrange.
      var sut = new ProcessingLog();

      var message = "My message";

      var expectedEntry = new LogEntry(
         LogLevel.FatalError,
         message,
         _lineNumber,
         _fieldSpecification.FieldDescription);

      // Act.
      sut.LogFatalError(message, _lineNumber, _fieldSpecification);

      // Assert.
      sut.First().Should().Be(expectedEntry);
   }

   [Fact]
   public void ProcessingLog_LogFatalError_ShouldAddExpectedEntry_WhenRawDataIsIncluded()
   {
      // Arrange.
      var sut = new ProcessingLog();

      var message = "My message";
      var rawData = "this is a test...this is only a test...";

      var expectedEntry = new LogEntry(
         LogLevel.FatalError,
         message,
         _lineNumber,
         RawData: rawData);

      // Act.
      sut.LogFatalError(message, _lineNumber, rawData: rawData);

      // Assert.
      sut.First().Should().Be(expectedEntry);
   }

   [Fact]
   public void ProcessingLog_LogFatalError_ShouldAddExpectedEntry_WhenAllParametersAreIncluded()
   {
      // Arrange.
      var sut = new ProcessingLog();

      var message = "My message";
      var rawData = "this is a test...this is only a test...";

      var expectedEntry = new LogEntry(
         LogLevel.FatalError,
         message,
         _lineNumber,
         _fieldSpecification.FieldDescription,
         rawData);

      // Act.
      sut.LogFatalError(message, _lineNumber, _fieldSpecification, rawData);

      // Assert.
      sut.First().Should().Be(expectedEntry);
   }

   #endregion

   #region LogFieldNotPresent Method Tests
   // ==========================================================================
   // ==========================================================================

   [Fact]
   public void ProcessingLog_LogNotPresent_ShouldAddExpectedEntry_WhenFieldIsOptional()
   {
      // Arrange.
      var sut = new ProcessingLog();

      var message = String.Format(Messages.LogFieldNotPresent, _fieldSpecification.FieldDescription);

      var expectedEntry = new LogEntry(
         LogLevel.Information,
         message,
         _lineNumber,
         _fieldSpecification.FieldDescription);

      // Act.
      sut.LogFieldNotPresent(_lineNumber, _fieldSpecification);

      // Assert.
      sut.First().Should().Be(expectedEntry);
   }

   [Fact]
   public void ProcessingLog_LogNotPresent_ShouldAddExpectedEntry_WhenFieldIsRequired()
   {
      // Arrange.
      var sut = new ProcessingLog();

      var fieldSpecification = _fieldSpecification with { Optionality = Optionality.Required };
      var message = String.Format(Messages.LogRequiredFieldNotPresent, fieldSpecification.FieldDescription);

      var expectedEntry = new LogEntry(
         LogLevel.Error,
         message,
         _lineNumber,
         fieldSpecification.FieldDescription);

      // Act.
      sut.LogFieldNotPresent(_lineNumber, fieldSpecification);

      // Assert.
      sut.First().Should().Be(expectedEntry);
   }

   #endregion

   #region LogFieldPresent Method Tests
   // ==========================================================================
   // ==========================================================================

   [Fact]
   public void ProcessingLog_LogFieldPresent_ShouldAddExpectedEntry()
   {
      // Arrange.
      var sut = new ProcessingLog();

      var message = String.Format(Messages.LogFieldPresent, _fieldSpecification.FieldDescription);
      var rawData = "this is a test...this is only a test...";

      var expectedEntry = new LogEntry(
         LogLevel.Information,
         message,
         _lineNumber,
         _fieldSpecification.FieldDescription,
         rawData);

      // Act.
      sut.LogFieldPresent(_lineNumber, _fieldSpecification, rawData.AsSpan());

      // Assert.
      sut.First().Should().Be(expectedEntry);
   }

   #endregion

   #region LogFieldPresentButNull Method Tests
   // ==========================================================================
   // ==========================================================================

   [Fact]
   public void ProcessingLog_LogFieldPresentButNull_ShouldAddExpectedEntry()
   {
      // Arrange.
      var sut = new ProcessingLog();

      var message = String.Format(Messages.LogFieldPresentButNull, _fieldSpecification.FieldDescription);
      var rawData = "\"\"";

      var expectedEntry = new LogEntry(
         LogLevel.Information,
         message,
         _lineNumber,
         _fieldSpecification.FieldDescription,
         rawData);

      // Act.
      sut.LogFieldPresentButNull(_lineNumber, _fieldSpecification);

      // Assert.
      sut.First().Should().Be(expectedEntry);
   }

   #endregion

   #region LogInformation Method Tests
   // ==========================================================================
   // ==========================================================================

   [Fact]
   public void ProcessingLog_LogInformation_ShouldAddExpectedEntry_WhenOnlyRequiredParametersAreIncluded()
   {
      // Arrange.
      var sut = new ProcessingLog();

      var message = "My message";

      var expectedEntry = new LogEntry(LogLevel.Information, message, _lineNumber);

      // Act.
      sut.LogInformation(message, _lineNumber);

      // Assert.
      sut.First().Should().Be(expectedEntry);
   }

   [Fact]
   public void ProcessingLog_LogInformation_ShouldAddExpectedEntry_WhenFieldSpecificationIsIncluded()
   {
      // Arrange.
      var sut = new ProcessingLog();

      var message = "My message";

      var expectedEntry = new LogEntry(
         LogLevel.Information,
         message,
         _lineNumber,
         _fieldSpecification.FieldDescription);

      // Act.
      sut.LogInformation(message, _lineNumber, _fieldSpecification);

      // Assert.
      sut.First().Should().Be(expectedEntry);
   }

   [Fact]
   public void ProcessingLog_LogInformation_ShouldAddExpectedEntry_WhenRawDataIsIncluded()
   {
      // Arrange.
      var sut = new ProcessingLog();

      var message = "My message";
      var rawData = "this is a test...this is only a test...";

      var expectedEntry = new LogEntry(
         LogLevel.Information,
         message,
         _lineNumber,
         RawData: rawData);

      // Act.
      sut.LogInformation(message, _lineNumber, rawData: rawData);

      // Assert.
      sut.First().Should().Be(expectedEntry);
   }

   [Fact]
   public void ProcessingLog_LogInformation_ShouldAddExpectedEntry_WhenAllParametersAreIncluded()
   {
      // Arrange.
      var sut = new ProcessingLog();

      var message = "My message";
      var rawData = "this is a test...this is only a test...";

      var expectedEntry = new LogEntry(
         LogLevel.Information,
         message,
         _lineNumber,
         _fieldSpecification.FieldDescription,
         rawData);

      // Act.
      sut.LogInformation(message, _lineNumber, _fieldSpecification, rawData);

      // Assert.
      sut.First().Should().Be(expectedEntry);
   }

   #endregion

   #region LogWarning Method Tests
   // ==========================================================================
   // ==========================================================================

   [Fact]
   public void ProcessingLog_LogWarning_ShouldAddExpectedEntry_WhenOnlyRequiredParametersAreIncluded()
   {
      // Arrange.
      var sut = new ProcessingLog();

      var message = "My message";

      var expectedEntry = new LogEntry(LogLevel.Warning, message, _lineNumber);

      // Act.
      sut.LogWarning(message, _lineNumber);

      // Assert.
      sut.First().Should().Be(expectedEntry);
   }

   [Fact]
   public void ProcessingLog_LogWarning_ShouldAddExpectedEntry_WhenFieldSpecificationIsIncluded()
   {
      // Arrange.
      var sut = new ProcessingLog();

      var message = "My message";

      var expectedEntry = new LogEntry(
         LogLevel.Warning,
         message,
         _lineNumber,
         _fieldSpecification.FieldDescription);

      // Act.
      sut.LogWarning(message, _lineNumber, _fieldSpecification);

      // Assert.
      sut.First().Should().Be(expectedEntry);
   }

   [Fact]
   public void ProcessingLog_LogWarning_ShouldAddExpectedEntry_WhenRawDataIsIncluded()
   {
      // Arrange.
      var sut = new ProcessingLog();

      var message = "My message";
      var rawData = "this is a test...this is only a test...";

      var expectedEntry = new LogEntry(
         LogLevel.Warning,
         message,
         _lineNumber,
         RawData: rawData);

      // Act.
      sut.LogWarning(message, _lineNumber, rawData: rawData);

      // Assert.
      sut.First().Should().Be(expectedEntry);
   }

   [Fact]
   public void ProcessingLog_LogWarning_ShouldAddExpectedEntry_WhenAllParametersAreIncluded()
   {
      // Arrange.
      var sut = new ProcessingLog();

      var message = "My message";
      var rawData = "this is a test...this is only a test...";

      var expectedEntry = new LogEntry(
         LogLevel.Warning,
         message,
         _lineNumber,
         _fieldSpecification.FieldDescription,
         rawData);

      // Act.
      sut.LogWarning(message, _lineNumber, _fieldSpecification, rawData);

      // Assert.
      sut.First().Should().Be(expectedEntry);
   }

   #endregion
}
