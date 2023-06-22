namespace HL7.Net.Core;

public static class SpanExtensions
{
   public static FieldEnumerator ToFields(this ReadOnlySpan<Char> span, Char separator) => new(span, separator);

   public static LineEnumerator ToLines(this ReadOnlySpan<Char> span) => new(span);

   /// <summary>
   ///   Given a span of characters, which may or may not contain escape
   ///   sequences, return a string where escaped characters are replaced by
   ///   their un-escaped value. If the span does not contain escape sequences
   ///   then the entire span is returned as a string.
   /// </summary>
   /// <param name="span">
   ///   The span of characters to decode.
   /// </param>
   /// <param name="encodingDetails">
   ///   Specifies how encoding of a segment or composite field is handled.
   /// </param>
   /// <returns>
   ///   The decoded contents of the input <paramref name="span"/>.
   /// </returns>
   internal static String GetDecodedString(
      this ReadOnlySpan<Char> span,
      EncodingDetails encodingDetails)
   {
      var decoded = span.ToString();
      foreach (var ch in encodingDetails.AllSeparators)
      {
         if (span.IndexOf(ch) != -1)
         {
            var escapedSequence = $"{encodingDetails.EscapeCharacter}{ch}";
            var replaceWith = new String(ch, 1);
            decoded = decoded.Replace(escapedSequence, replaceWith);
         }
      }
      
      return decoded;
   }

   /// <summary>
   ///   Gonna need this to handle escaped delimiters in FieldEnumerator
   /// </summary>
   /// <param name="aSpan"></param>
   /// <param name="aChar"></param>
   /// <param name="startIndex"></param>
   /// <returns></returns>
   public static int IndexOf(this ReadOnlySpan<char> aSpan, char aChar, int startIndex)
   {
      var indexInSlice = aSpan.Slice(startIndex).IndexOf(aChar);

      if (indexInSlice == -1)
      {
         return -1;
      }

      return startIndex + indexInSlice;
   }
}
