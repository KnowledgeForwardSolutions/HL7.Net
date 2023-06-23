using System.Text;

namespace HL7.Net.Core;

public static class SpanExtensions
{
   /// <summary>
   ///   Get an enumerator that breaks a span into a series of fields separated
   ///   by the <paramref name="separator"/> character. 
   /// </summary>
   /// <param name="span">
   ///   The span of characters to separate into fields.
   /// </param>
   /// <param name="separator">
   ///   The character used to indicate the end of a field and the start of 
   ///   another.
   /// </param>
   /// <param name="escapeCharacter">
   ///   The character used to "escape" the separator character and allow it to
   ///   be included in a field instead of starting a new field.
   /// </param>
   /// <returns>
   ///   A <see cref="FieldEnumerator"/> object.
   /// </returns>
   public static FieldEnumerator ToFields(
      this ReadOnlySpan<Char> span, 
      Char separator,
      Char escapeCharacter) => new(span, separator, escapeCharacter);

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
      var index = span.IndexOf(encodingDetails.EscapeCharacter);
      if (index == -1)
      {
         return span.ToString();
      }

      var sb = new StringBuilder();
      while(index != -1)
      {
         sb.Append(span[..index]);              // Everything preceding the escape character
         if (index < span.Length - 1)
         {
            sb.Append(span[index + 1]);         // The escaped character
            span = span[(index + 2)..];         // Start again after the escaped character
         }
         else
         {
            span = ReadOnlySpan<Char>.Empty;    // Last character was the escape character
         }
         index = span.IndexOf(encodingDetails.EscapeCharacter);
      }
      sb.Append(span);                          // The remainder
      
      return sb.ToString();
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
