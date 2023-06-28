namespace HL7.Net.Core;

/// <summary>
///   Defines a property that identifies if a field, component or subcomponent
///   is present in a segment or composite element.
/// </summary>
public interface IPresence
{
   /// <summary>
   ///   Identifies if the field, component or subcomponent is present, present
   ///   but null or not present.
   /// </summary>
   Presence Presence { get; }
}

/// <summary>
///   Additional methods for type <see cref="IPresence"/>.
/// </summary>
public static class IPresenceExtensions
{
   /// <summary>
   ///   Check if a item is present.
   /// </summary>
   /// <param name="presence">
   ///   The item presence value to check.
   /// </param>
   /// <returns>
   ///   <see langword="true"/> if the item is present; otherwise 
   ///   <see langword="false"/>.
   /// </returns>
   public static Boolean IsPresent(this IPresence item)
      => item.Presence == Presence.Present || item.Presence == Presence.PresentButNull;

   /// <summary>
   ///   Check if a item is present and not null.
   /// </summary>
   /// <param name="presence">
   ///   The item presence value to check.
   /// </param>
   /// <returns>
   ///   <see langword="true"/> if the item is present and not null; otherwise 
   ///   <see langword="false"/>.
   /// </returns>
   public static Boolean IsPresentNotNull(this IPresence item)
      => item.Presence == Presence.Present;
}
