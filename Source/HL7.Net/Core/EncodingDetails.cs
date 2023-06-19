namespace HL7.Net.Core;

/// <summary>
///   Specifies the separator and encoding characters used when processing HL7
///   text messages.
/// </summary>
/// <param name="fieldSeparator">
///   The character used to delimit fields in a segment.
/// </param>
/// <param name="componentSeparator">
///   Separates adjacent components of data fields.
/// </param>
/// <param name="repetitionSeparator">
///   Separates multiple occurrences of a field.
/// </param>
/// <param name="escapeCharacter">
///   Escape character for TX and FT fields.
/// </param>
/// <param name="subComponentSeparator">
///   Separates adjacent subcomponents of data fields.
/// </param>
internal record EncodingDetails(
      Char FieldSeparator,
      Char ComponentSeparator,
      Char RepetitionSeparator,
      Char EscapeCharacter,
      Char SubComponentSeparator);