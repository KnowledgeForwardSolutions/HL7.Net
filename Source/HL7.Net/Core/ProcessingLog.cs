namespace HL7.Net.Core;

/// <summary>
///   Log describing the process of reading or writing an HL7 message.
/// </summary>
public class ProcessingLog : IEnumerable<LogEntry>
{
   private readonly List<LogEntry> _logs = new();

   /// <summary>
   ///   Log creation is reserved for HL7.Net only.
   /// </summary>
   internal ProcessingLog() { }

   /// <summary>
   ///   The number of log entries.
   /// </summary>
   public Int32 Count => _logs.Count;

   /// <summary>
   ///   The greatest log level encountered.
   /// </summary>
   public LogLevel HighestLogLevel { get; private set; }

   /// <summary>
   ///   Enumerator that iterates over the collection of log entries.
   /// </summary>
   public IEnumerator<LogEntry> GetEnumerator() => _logs.GetEnumerator();

   internal void LogError(
      String message,
      Int32 lineNumber,
      FieldSpecification? fieldSpecification = null,
      String? rawData = null)
   {
      _logs.Add(new LogEntry(LogLevel.Error, message, lineNumber, fieldSpecification?.FieldDescription, rawData));
      if (HighestLogLevel < LogLevel.Error)
      {
         HighestLogLevel = LogLevel.Error;
      }
   }

   internal void LogFatalError(
      String message,
      Int32 lineNumber,
      FieldSpecification? fieldSpecification = null,
      String? rawData = null)
   {
      _logs.Add(new LogEntry(LogLevel.FatalError, message, lineNumber, fieldSpecification?.FieldDescription, rawData));
      if (HighestLogLevel < LogLevel.FatalError)
      {
         HighestLogLevel = LogLevel.FatalError;
      }
   }

   internal void LogFieldNotPresent(Int32 lineNumber, FieldSpecification fieldSpecification)
   {
      String message;
      if (fieldSpecification.Optionality == Optionality.Required)
      {
         message = String.Format(Messages.LogRequiredFieldNotPresent, fieldSpecification.FieldDescription);
         LogError(message, lineNumber, fieldSpecification);
         return;
      }

      message = String.Format(Messages.LogFieldNotPresent, fieldSpecification.FieldDescription);
      LogInformation(message, lineNumber, fieldSpecification);
   }

   internal void LogFieldPresent(
      Int32 lineNumber,
      FieldSpecification fieldSpecification,
      ReadOnlySpan<Char> fieldContents)
   {
      var message = String.Format(Messages.LogFieldPresent, fieldSpecification.FieldDescription);
      LogInformation(message, lineNumber, fieldSpecification, fieldContents.ToString());
   }

   internal void LogFieldPresentButNull(Int32 lineNumber, FieldSpecification fieldSpecification)
   {
      var message = String.Format(Messages.LogFieldPresentButNull, fieldSpecification.FieldDescription);
      LogInformation(message, lineNumber, fieldSpecification, "\"\"");
   }

   internal void LogInformation(
      String message,
      Int32 lineNumber,
      FieldSpecification? fieldSpecification = null,
      String? rawData = null)
      => _logs.Add(new LogEntry(LogLevel.Information, message, lineNumber, fieldSpecification?.FieldDescription, rawData));

   internal void LogWarning(
      String message,
      Int32 lineNumber,
      FieldSpecification? fieldSpecification = null,
      String? rawData = null)
   {
      _logs.Add(new LogEntry(LogLevel.Warning, message, lineNumber, fieldSpecification?.FieldDescription, rawData));
      if (HighestLogLevel < LogLevel.Warning)
      {
         HighestLogLevel = LogLevel.Warning;
      }
   }

   IEnumerator IEnumerable.GetEnumerator() => throw new NotImplementedException();
}
