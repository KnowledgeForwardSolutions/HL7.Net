namespace HL7.Net.Tests.Unit.TestData;

/// <summary>
///   Theory data that combines a version id with table entries that are either
///   valid or invalid for the specified version.
/// </summary>
public class TableEntryByVersionMatrixTheoryData<TType> : TheoryData<T0104_VersionID, TType> where TType: TableEntry
{
   /// <param name="requestedVersionId">
   ///   Requested version.
   /// </param>
   /// <param name="tableEntries">
   ///   Dictionary of table entries and their associated minimum valid version.
   /// </param>
   /// <param name="validEntries">
   ///   <see langword="true"/> if the theory data should be valid for the 
   ///   <paramref name="requestedVersionId"/>; otherwise 
   ///   <see langword="false"/> for data that is not valid for the
   ///   <paramref name="requestedVersionId"/>.
   /// </param>
   public TableEntryByVersionMatrixTheoryData(
      T0104_VersionID requestedVersionId,
      Dictionary<TType, T0104_VersionID> tableEntries,
      Boolean validEntries = true)
   {
      foreach(var (entry, minimumValidVersion) in tableEntries)
      {
         if ((minimumValidVersion <= requestedVersionId && validEntries)
            || minimumValidVersion > requestedVersionId && !validEntries)
         {
            Add(requestedVersionId, entry);
         }
      }
   }
}
