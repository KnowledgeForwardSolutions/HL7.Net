// Ignore Spelling: Parsable

namespace HL7.Net;

public interface IParsable<T>
{

   static abstract T Parse(
      ref FieldEnumerator fieldEnumerator,
      FieldSpecification fieldSpecification,
      T0104_VersionID versionID,
      Int32 lineNumber,
      IProcessingLog log);
}
