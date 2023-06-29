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

   internal void AddLogEntry(LogEntry entry)
   {
      _logs.Add(entry);
      if (HighestLogLevel < entry.LogLevel)
      {
         HighestLogLevel = entry.LogLevel;
      }
   }

   internal void LogError(
      String message,
      Int32 lineNumber,
      FieldSpecification? fieldSpecification = null,
      String? rawData = null)
      => AddLogEntry(new LogEntry(LogLevel.Error, message, lineNumber, fieldSpecification?.FieldDescription, rawData));

   internal void LogFatalError(
      String message,
      Int32 lineNumber,
      FieldSpecification? fieldSpecification = null,
      String? rawData = null)
      => AddLogEntry(new LogEntry(LogLevel.FatalError, message, lineNumber, fieldSpecification?.FieldDescription, rawData));

   internal void LogFieldNotPresent(Int32 lineNumber, FieldSpecification fieldSpecification)
   {
      if (fieldSpecification.Optionality == Optionality.Required)
      {
         AddLogEntry(LogEntry.GetRequiredFieldNotPresentEntry(lineNumber, fieldSpecification.FieldDescription));
         return;
      }

      AddLogEntry(LogEntry.GetOptionalFieldNotPresentEntry(lineNumber, fieldSpecification.FieldDescription));
   }

   internal void LogFieldPresent(
      Int32 lineNumber,
      FieldSpecification fieldSpecification,
      ReadOnlySpan<Char> fieldContents)
      => AddLogEntry(LogEntry.GetFieldPresentEntry(
         lineNumber, 
         fieldSpecification.FieldDescription,
         fieldContents.ToString()));

   internal void LogFieldPresentButNull(Int32 lineNumber, FieldSpecification fieldSpecification)
      => AddLogEntry(LogEntry.GetFieldPresentButNullEntry(
         lineNumber,
         fieldSpecification.FieldDescription));

   internal void LogFieldPresentButPossiblyTruncated(
      Int32 lineNumber,
      FieldSpecification fieldSpecification,
      ReadOnlySpan<Char> fieldContents)
   {
      String message;
      if (fieldContents.Length > fieldSpecification.Length)
      {
         message = String.Format(
            Messages.LogFieldPresentButTruncated,
            fieldSpecification.FieldDescription,
            fieldSpecification.Length);
         LogWarning(message, lineNumber, fieldSpecification, fieldContents.ToString());
      }
      else
      {
         message = String.Format(
            Messages.LogFieldPresent,
            fieldSpecification.FieldDescription);
         LogInformation(message, lineNumber, fieldSpecification, fieldContents.ToString());
      }
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
      => AddLogEntry(new LogEntry(LogLevel.Warning, message, lineNumber, fieldSpecification?.FieldDescription, rawData));

   IEnumerator IEnumerable.GetEnumerator() => throw new NotImplementedException();
}
