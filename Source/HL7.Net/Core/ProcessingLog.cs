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

   public void LogInformation(
      String message,
      Int32 lineNumber,
      String? segmentID = null,
      Int32? fieldNumber = null,
      String? rawData = null)
      => _logs.Add(new LogEntry(LogLevel.Information, message, lineNumber, segmentID, fieldNumber, rawData));

   public void LogError(
      String message,
      Int32 lineNumber,
      String? segmentID = null,
      Int32? fieldNumber = null,
      String? rawData = null)
   {
      _logs.Add(new LogEntry(LogLevel.Error, message, lineNumber, segmentID, fieldNumber, rawData));
      if ((Int32) HighestLogLevel < (Int32) LogLevel.Error)
      {
         HighestLogLevel = LogLevel.Error;
      }
   }

   public void LogFatalError(
      String message,
      Int32 lineNumber,
      String? segmentID = null,
      Int32? fieldNumber = null,
      String? rawData = null)
   {
      _logs.Add(new LogEntry(LogLevel.FatalError, message, lineNumber, segmentID, fieldNumber, rawData));
      if ((Int32)HighestLogLevel < (Int32)LogLevel.FatalError)
      {
         HighestLogLevel = LogLevel.FatalError;
      }
   }

   public void LogWarning(
      String message,
      Int32 lineNumber,
      String? segmentID = null,
      Int32? fieldNumber = null,
      String? rawData = null)
   {
      _logs.Add(new LogEntry(LogLevel.Warning, message, lineNumber, segmentID, fieldNumber, rawData));
      if ((Int32)HighestLogLevel < (Int32)LogLevel.Warning)
      {
         HighestLogLevel = LogLevel.Warning;
      }
   }

   IEnumerator IEnumerable.GetEnumerator() => throw new NotImplementedException();
}
