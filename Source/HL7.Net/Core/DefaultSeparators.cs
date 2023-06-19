namespace HL7.Net.Core;

/// <summary>
///   Default separator characters suggested by the HL7 specification.
/// </summary>
public static class DefaultSeparators
{
   /// <summary>
   ///   Separates adjacent components of data fields.
   /// </summary>
   public const Char ComponentSeparator = '^';

   /// <summary>
   ///   Escape character for TX and FT fields.
   /// </summary>
   public const Char EscapeCharacter = '\\';

   /// <summary>
   ///   Separated two adjacent fields within a segment.
   /// </summary>
   public const Char FieldSeparator = '|';

   /// <summary>
   ///   Separates multiple occurrences of a field.
   /// </summary>
   public const Char RepetitionSeparator = '~';

   /// <summary>
   ///   Terminates a segment. May not be changed/overridden.
   /// </summary>
   public const Char SegmentTerminator = '\r';

   /// <summary>
   ///   Separates adjacent subcomponents of data fields.
   /// </summary>
   public const Char SubComponentSeparator = '&';
}
