namespace HL7.Net.Core;

/// <summary>
///   Enumerator that breaks a span of characters into sub-spans delimited by a
///   separator character.
/// </summary>
public ref struct FieldEnumerator
{
   private readonly Char _escapeCharacter;
   private readonly Char _separator;
   private ReadOnlySpan<Char> _text;

   /// <summary>
   ///   Initialize a new <see cref="FieldEnumerator"/> object.
   /// </summary>
   /// <param name="text">
   ///   The span of characters to process.
   /// </param>
   /// <param name="separator">
   ///   The character used to indicate the end of a field and the start of 
   ///   another.
   /// </param>
   /// <param name="escapeCharacter">
   ///   The character used to "escape" the separator character and allow it to
   ///   be included in a field instead of starting a new field.
   /// </param>
   internal FieldEnumerator(
      ReadOnlySpan<Char> text, 
      Char separator,
      Char escapeCharacter)
   {
      _text = text;
      _separator = separator;
      _escapeCharacter = escapeCharacter;
      Current = default;
   }

   /// <summary>
   ///   The current field.
   /// </summary>
   public ReadOnlySpan<Char> Current { get; private set; }

   /// <summary>
   ///   Gets an enumerator for the sub-spans that represent the fields in the
   ///   original span.
   /// </summary>
   /// <returns>
   ///   The enumerator object.
   /// </returns>
   public FieldEnumerator GetEnumerator() => this;

   /// <summary>
   ///   Advance to the next field in the enumeration.
   /// </summary>
   /// <returns>
   ///   <see langword="true"/> if there are still fields remaining; otherwise 
   ///   <see langword="false"/>.
   /// </returns>
   public Boolean MoveNext()
   {
      var span = _text;

      // Handle end of string.
      if (span.Length == 0)
      {
         return false;
      }

      var index = span.IndexOf(_separator);

      // Skip over escaped separator characters.
      while (index > 0 && span[index - 1] == _escapeCharacter)
      {
         index = span.IndexOf(_separator, index + 1);
      }

      // Handle only one line remaining.
      if (index == -1)
      {
         Current = span;
         _text = ReadOnlySpan<Char>.Empty;
         return true;
      }

      // Handle separator found.
      Current = span[..index];
      _text = span[(index + 1)..];
      return true;
   }
}
