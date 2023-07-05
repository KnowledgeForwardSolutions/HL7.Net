namespace HL7.Net.Core;

/// <summary>
///   Abstract base record for an entry in an HL7 data definition table.
/// </summary>
public abstract record TableEntry
{
   protected TableEntry(
      String value = "",
      Presence presence = Presence.Present)
   {
      Value = value;
      Presence = presence;
   }

   public static implicit operator String(TableEntry field) => field.Value;

   /// <summary>
   ///   The value for this table entry.
   /// </summary>
   public String Value { get; private init; }

   /// <inheritdoc/>
   public Presence Presence { get; private set; }
}
