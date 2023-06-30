namespace HL7.Net.Core;

/// <summary>
///   Defines the field separator used in an HL7 message.
/// </summary>
public sealed record FieldSeparatorField : IPresence
{
   /// <summary>
   ///   Represents a field separator field that is not present.
   /// </summary>
   public static readonly FieldSeparatorField NotPresent = new('\0', Presence.NotPresent);

   internal FieldSeparatorField(
      Char value, 
      Presence fieldPresence = Presence.Present)
   {
      Value = value;
      Presence = fieldPresence;
   }

   public static implicit operator Char(FieldSeparatorField field) => field.Value;

   /// <inheritdoc/>
   public Presence Presence { get; init; }

   /// <summary>
   ///   The value of this field.
   /// </summary>
   public Char Value { get; init; }

   internal static FieldSeparatorField Parse(
      ReadOnlySpan<Char> span,
      FieldSpecification fieldSpecification,
      Int32 lineNumber,
      ProcessingLog log)
   {
      // Expects entire line with first 3 characters = segment ID and 4th 
      // character being the field separator.
      if (span.Length < 4)
      {
         log.LogFatalError(
            String.Format(Messages.MissingFieldSeparator, fieldSpecification.FieldDescription),
            lineNumber,
            fieldSpecification);
         return FieldSeparatorField.NotPresent;
      }

      var fieldSeparator = span[3];
      log.LogFieldPresent(lineNumber, fieldSpecification, span.Slice(3, 1));
      
      return new FieldSeparatorField(fieldSeparator);
   }
}
