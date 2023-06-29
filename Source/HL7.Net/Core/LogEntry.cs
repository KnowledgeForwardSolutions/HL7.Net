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
      String fieldContents)
      => new(
         LogLevel.Information,
         String.Format(Messages.LogFieldPresent, fieldDescription),
         lineNumber,
         fieldDescription,
         fieldContents);

   internal static LogEntry GetFieldPresentButNullEntry(
      Int32 lineNumber,
      String fieldDescription)
      => new(
         LogLevel.Information,
         String.Format(Messages.LogFieldPresentButNull, fieldDescription),
         lineNumber,
         fieldDescription,
         GeneralConstants.PresentButNullValue);

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
}