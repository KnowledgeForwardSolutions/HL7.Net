namespace HL7.Net.Core;

/// <summary>
///   Values that specify if a field is optional, required or conditional.
/// </summary>
public enum Optionality
{
   /// <summary>
   ///   The field is optional.
   /// </summary>
   Optional,

   /// <summary>
   ///   The field is required.
   /// </summary>
   Required,

   /// <summary>
   ///   The field is conditional, depending on the trigger event.
   /// </summary>
   Conditional
}

public static partial class EnumExtensions
{
   /// <summary>
   ///   Check if the <paramref name="value"/> is a defined member of the
   ///   <see cref="Optionality"/> enum.
   /// </summary>
   /// <param name="value">
   ///   The value to check.
   /// </param>
   /// <returns>
   ///   <see langword="true"/> if <paramref name="value"/> is a defined member 
   ///   of the <see cref="Optionality"/> enum; otherwise 
   ///   <see langword="false"/>.
   /// </returns>
   public static Boolean IsDefined(this Optionality value)
      => value switch
      {
         Optionality.Optional => true,
         Optionality.Required => true,
         Optionality.Conditional => true,
         _ => false,
      };
}