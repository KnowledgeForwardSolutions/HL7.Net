namespace HL7.Net.Core;

/// <summary>
///   Defines the additional characters used to encode an HL7 message.
/// </summary>
public sealed record EncodingCharactersField
{
   internal EncodingCharactersField(
      Char componentSeparator,
      Char repetitionSeparator,
      Char escapeCharacter,
      Char subComponentSeparator,
      String? rawValue)
   {
      ComponentSeparator = componentSeparator;
      RepetitionSeparator = repetitionSeparator;
      EscapeCharacter = escapeCharacter;
      SubComponentSeparator = subComponentSeparator;
      RawValue = rawValue;
   }

   /// <summary>
   ///   Separates adjacent components of data fields.
   /// </summary>
   public Char ComponentSeparator { get; init; }

   /// <summary>
   ///   Escape character for TX and FT fields.
   /// </summary>
   public Char EscapeCharacter { get; init; }

   /// <summary>
   ///   Identifies if this field is present in the message and if the value of
   ///   the field is null or not.
   /// </summary>
   public Presence FieldPresence => Presence.Present;

   /// <inheritdoc/>
   public Optionality Optionality => Optionality.Required;

   /// <summary>
   ///   The raw text value for this field that was read from an HL7 message.
   /// </summary>
   public String? RawValue { get; init; }

   /// <summary>
   ///   Separates multiple occurrences of a field.
   /// </summary>
   public Char RepetitionSeparator { get; init; }

   /// <summary>
   ///   Separates adjacent subcomponents of data fields.
   /// </summary>
   public Char SubComponentSeparator { get; init; }

   internal static EncodingCharactersField Parse(
      ref FieldEnumerator fieldEnumerator,
      FieldSpecification fieldSpecification,
      Int32 lineNumber,
      ProcessingLog log)
   {
      String message;
      if (!fieldEnumerator.MoveNext())
      {
         message = String.Format(Messages.LogRequiredFieldNotPresent, fieldSpecification.FieldDescription);
         log.LogFatalError(message, lineNumber, fieldSpecification);
         return new EncodingCharactersField('\0', '\0', '\0', '\0', null);
      }

      var fieldContents = fieldEnumerator.Current;
      if (fieldContents.Length < 2 || fieldContents.Length > 4)
      {
         message = String.Format(Messages.LogFieldInvalidLength, fieldSpecification.FieldDescription);
         log.LogFatalError(message, lineNumber, fieldSpecification);
         return new EncodingCharactersField('\0', '\0', '\0', '\0', null);
      }

      var characters = new Char[4];
      var previousCharacters = new HashSet<Char>();
      for(var index = 0; index < fieldContents.Length; index++)
      {
         var ch = fieldContents[index];
         if (previousCharacters.Contains(ch))
         {
            message = String.Format(
               Messages.LogDuplicateEncodingCharacter,
               fieldSpecification.FieldDescription,
               ch);
            log.LogFatalError(message, lineNumber, fieldSpecification, fieldContents.ToString());
            return new EncodingCharactersField('\0', '\0', '\0', '\0', null);
         }
         previousCharacters.Add(ch);
         characters[index] = ch;
      }

      log.LogFieldPresent(lineNumber, fieldSpecification, fieldContents);

      return new EncodingCharactersField(
         characters[0],
         characters[1],
         characters[2],
         characters[3],
         fieldContents.ToString());
   }
}
