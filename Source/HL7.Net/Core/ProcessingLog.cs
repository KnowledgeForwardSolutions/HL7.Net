namespace HL7.Net.Core;

/// <summary>
///   Log describing the process of reading or writing an HL7 message.
/// </summary>
public class ProcessingLog : IProcessingLog
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

   public void LogError(
      String message,
      Int32 lineNumber,
      FieldSpecification? fieldSpecification = null,
      String? rawData = null)
      => AddLogEntry(new LogEntry(LogLevel.Error, message, lineNumber, fieldSpecification?.FieldDescription, rawData));

   public void LogFatalError(
      String message,
      Int32 lineNumber,
      FieldSpecification? fieldSpecification = null,
      String? rawData = null)
      => AddLogEntry(new LogEntry(LogLevel.FatalError, message, lineNumber, fieldSpecification?.FieldDescription, rawData));

   public void LogFieldNotPresent(Int32 lineNumber, FieldSpecification fieldSpecification)
   {
      if (fieldSpecification.Optionality == Optionality.Required)
      {
         AddLogEntry(LogEntry.GetRequiredFieldNotPresentEntry(lineNumber, fieldSpecification.FieldDescription));
         return;
      }

      AddLogEntry(LogEntry.GetOptionalFieldNotPresentEntry(lineNumber, fieldSpecification.FieldDescription));
   }

   public void LogFieldPresent(
      Int32 lineNumber,
      FieldSpecification fieldSpecification,
      ReadOnlySpan<Char> fieldContents,
      Boolean checkTruncated = false)
   {
      if (checkTruncated && fieldContents.Length > fieldSpecification.Length)
      {
         AddLogEntry(LogEntry.GetFieldPresentButTruncatedEntry(
            lineNumber,
            fieldSpecification.FieldDescription,
            fieldSpecification.Length,
            fieldContents));
         return;
      }

      AddLogEntry(LogEntry.GetFieldPresentEntry(
         lineNumber,
         fieldSpecification.FieldDescription,
         fieldContents));
   }

   public void LogFieldPresentButNull(Int32 lineNumber, FieldSpecification fieldSpecification)
      => AddLogEntry(LogEntry.GetFieldPresentButNullEntry(
         lineNumber,
         fieldSpecification.FieldDescription));

   public void LogInformation(
      String message,
      Int32 lineNumber,
      FieldSpecification? fieldSpecification = null,
      String? rawData = null)
      => _logs.Add(new LogEntry(LogLevel.Information, message, lineNumber, fieldSpecification?.FieldDescription, rawData));

   public void LogWarning(
      String message,
      Int32 lineNumber,
      FieldSpecification? fieldSpecification = null,
      String? rawData = null)
      => AddLogEntry(new LogEntry(LogLevel.Warning, message, lineNumber, fieldSpecification?.FieldDescription, rawData));

   public void LogUnrecognizedTableValue(
      Int32 lineNumber,
      FieldSpecification fieldSpecification,
      ReadOnlySpan<Char> fieldContents)
      => AddLogEntry(LogEntry.GetUnrecognizedTableValueEntry(
         lineNumber,
         fieldSpecification.Datatype,
         fieldSpecification.FieldDescription,
         fieldContents));

   IEnumerator IEnumerable.GetEnumerator() => throw new NotImplementedException();
}
