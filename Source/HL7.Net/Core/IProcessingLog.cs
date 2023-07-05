namespace HL7.Net.Core;

public interface IProcessingLog : IEnumerable<LogEntry>
{
   void LogError(
      String message,
      Int32 lineNumber,
      FieldSpecification? fieldSpecification = null,
      String? rawData = null);

   void LogFatalError(
      String message,
      Int32 lineNumber,
      FieldSpecification? fieldSpecification = null,
      String? rawData = null);

   void LogFieldNotPresent(Int32 lineNumber, FieldSpecification fieldSpecification);

   void LogFieldPresent(
      Int32 lineNumber,
      FieldSpecification fieldSpecification,
      ReadOnlySpan<Char> fieldContents,
      Boolean checkTruncated = false);

   void LogFieldPresentButNull(Int32 lineNumber, FieldSpecification fieldSpecification);

   void LogInformation(
      String message,
      Int32 lineNumber,
      FieldSpecification? fieldSpecification = null,
      String? rawData = null);

   void LogWarning(
      String message,
      Int32 lineNumber,
      FieldSpecification? fieldSpecification = null,
      String? rawData = null);

   void LogUnrecognizedTableValue(
      Int32 lineNumber,
      FieldSpecification fieldSpecification,
      ReadOnlySpan<Char> fieldContents);
}
