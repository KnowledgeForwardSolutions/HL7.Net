namespace HL7.Net.Core;

public static class SpanExtensions
{
   public static LineEnumerator ToLines(this ReadOnlySpan<Char> span) => new(span);

   public static FieldEnumerator ToFields(this ReadOnlySpan<Char> span, Char separator) => new(span, separator);

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
