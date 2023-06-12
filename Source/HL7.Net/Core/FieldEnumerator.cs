namespace HL7.Net.Core;

/// <summary>
///   Enumerator that breaks a span of characters into sub-spans delimited by a
///   separator character.
/// </summary>
public ref struct FieldEnumerator
{
   private readonly Char _separator;
   private ReadOnlySpan<Char> _text;

   public FieldEnumerator(ReadOnlySpan<Char> text, Char separator)
   {
      _text = text;
      _separator = separator;
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
