namespace HL7.Net.Core;

/// <summary>
///   A single entry in an HL7.Net read/write log.
/// </summary>
/// <param name="LogLevel">
///   The level or severity of this entry.
/// </param>
/// <param name="message">
///   Descriptive text for this entry.
/// </param>
/// <param name="lineNumber">
///   The line number in the HL7 text associated with this entry.
/// </param>
/// <param name="segmentID">
///   Optional. The segment ID of the segment associated with this entry.
/// </param>
/// <param name="fieldNumber">
///   Optional. The field number of the segment field associated with this 
///   entry.
/// </param>
/// <param name="rawData">
///   Optional. The raw data for the field associated with this entry.
/// </param>
public record LogEntry(
   LogLevel LogLevel,
   String message,
   Int32 lineNumber,
   String? segmentID = null,
   Int32? fieldNumber = null,
   String? rawData = null);