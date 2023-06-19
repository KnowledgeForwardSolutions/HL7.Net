namespace HL7.Net.Core;

/// <summary>
///   Enumerator that breaks a span of characters into sub-spans that represent
///   the lines in the original span. Recognizes both Windows style line 
///   terminators ('\r\n') and Unix style line terminators ('\n').
/// </summary>
public ref struct LineEnumerator
{
   private ReadOnlySpan<Char> _text;

   /// <summary>
   ///   Initialize a new <see cref="LineEnumerator"/>.
   /// </summary>
   /// <param name="text">
   ///   The span of characters to split into lines.
   /// </param>
   public LineEnumerator(ReadOnlySpan<Char> text)
   {
      _text = text;
      Current = default;
   }

   /// <summary>
   ///   The current line.
   /// </summary>
   public ReadOnlySpan<Char> Current { get; private set; }

   /// <summary>
   ///   Gets an enumerator for the sub-spans that represent the lines in the
   ///   original span.
   /// </summary>
   /// <returns>
   ///   The enumerator object.
   /// </returns>
   public LineEnumerator GetEnumerator() => this;

   /// <summary>
   ///   Advance to the next line in the enumeration.
   /// </summary>
   /// <returns>
   ///   <see langword="true"/> if there are still lines remaining; otherwise 
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

      var index = span.IndexOfAny('\r', '\n');

      // Handle only one line remaining.
      if (index == -1)
      {
         Current = span;
         _text = ReadOnlySpan<Char>.Empty;
         return true;
      }

      // Handle line terminator (either Windows or Unix).
      Current = span[..index];
      if (span[index] == '\r' && index < span.Length - 1 && span[index + 1] == '\n')
      {
         index++;
      }
      _text = span[(index + 1)..];
      return true;
   }
}
