namespace HL7.Net.Core;

/// <summary>
///   A single entry in an HL7.Net read/write log.
/// </summary>
/// <param name="LogLevel">
///   The level or severity of this entry.
/// </param>
/// <param name="Message">
///   Descriptive text for this entry.
/// </param>
/// <param name="LineNumber">
///   The line number in the HL7 text associated with this entry.
/// </param>
/// <param name="FieldDescriptor">
///   Optional. The field description in the form 
///   SegmentID.Sequence{[Index]}{.ComponentSequence}/FieldName{.ComponentFieldName}
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
   String Message,
   Int32 LineNumber,
   String? FieldDescription = null,
   String? RawData = null)
{
   internal static LogEntry GetFieldPresentEntry(
      Int32 lineNumber,
      String fieldDescription,
      ReadOnlySpan<Char> fieldContents)
      => new(
         LogLevel.Information,
         String.Format(Messages.LogFieldPresent, fieldDescription),
         lineNumber,
         fieldDescription,
         fieldContents.ToString());

   internal static LogEntry GetFieldPresentButNullEntry(
      Int32 lineNumber,
      String fieldDescription)
      => new(
         LogLevel.Information,
         String.Format(Messages.LogFieldPresentButNull, fieldDescription),
         lineNumber,
         fieldDescription,
         GeneralConstants.PresentButNullValue);

   internal static LogEntry GetFieldPresentButTruncatedEntry(
      Int32 lineNumber,
      String fieldDescription,
      Int32 length,
      ReadOnlySpan<Char> fieldContents)
      => new(
         LogLevel.Warning,
         String.Format(Messages.LogFieldPresentButTruncated, fieldDescription, length),
         lineNumber,
         fieldDescription,
         fieldContents.ToString());

   internal static LogEntry GetOptionalFieldNotPresentEntry(
      Int32 lineNumber,
      String fieldDescription)
      => new(
         LogLevel.Information,
         String.Format(Messages.LogFieldNotPresent, fieldDescription),
         lineNumber,
         fieldDescription);

   internal static LogEntry GetRequiredFieldNotPresentEntry(
      Int32 lineNumber,
      String fieldDescription)
      => new(
         LogLevel.Error,
         String.Format(Messages.LogRequiredFieldNotPresent, fieldDescription),
         lineNumber,
         fieldDescription);

   internal static LogEntry GetUnrecognizedTableValueEntry(
      Int32 lineNumber,
      HL7Datatype datatype,
      String fieldDescription,
      ReadOnlySpan<Char> fieldContents)
      => new(
         LogLevel.Error,
         String.Format(Messages.UnrecognizedTableEntry, fieldDescription, datatype),
         lineNumber,
         fieldDescription,
         fieldContents.ToString());
}