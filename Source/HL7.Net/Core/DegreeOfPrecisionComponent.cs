namespace HL7.Net.Core;

/// <summary>
///   Degree of precision component of a timestamp field. Implements HL7 V2.2 
///   spec section 2.8.10.7.
/// </summary>
public record DegreeOfPrecisionComponent : IPresence
{
   /// <summary>
   ///   Represents a degree of precision component that is not present.
   /// </summary>
   public static readonly DegreeOfPrecisionComponent NotPresent = new(null, Presence.NotPresent);

   /// <summary>
   ///   Represents a degree of precision component that is present but is null.
   /// </summary>
   public static readonly DegreeOfPrecisionComponent PresentButNull = new(null, Presence.PresentButNull);

   internal DegreeOfPrecisionComponent(
      Char? value,
      Presence fieldPresence = Presence.Present)
   {
      Value = value;
      Presence = fieldPresence;
   }

   public static implicit operator Char?(DegreeOfPrecisionComponent field) => field.Value;

   /// <inheritdoc/>
   public Presence Presence { get; init; }

   /// <summary>
   ///   The value of this component.
   /// </summary>
   public Char? Value { get; init; }

   internal static DegreeOfPrecisionComponent Parse(
      ref FieldEnumerator fieldEnumerator,
      FieldSpecification fieldSpecification,
      Int32 lineNumber,
      ProcessingLog log)
   {
      if (!fieldEnumerator.MoveNext() || fieldEnumerator.Current.IsEmpty)
      {
         log.LogFieldNotPresent(lineNumber, fieldSpecification);
         return DegreeOfPrecisionComponent.NotPresent;
      }

      var fieldContents = fieldEnumerator.Current;
      if (fieldContents.IsPresentButNull())
      {
         log.LogFieldPresentButNull(lineNumber, fieldSpecification);
         return DegreeOfPrecisionComponent.PresentButNull;
      }

      var value = fieldContents[0] switch
      {
         'Y' => fieldContents[0],
         'L' => fieldContents[0],
         'D' => fieldContents[0],
         'H' => fieldContents[0],
         'M' => fieldContents[0],
         'S' => fieldContents[0],
         _ => '\0'
      };

      if (value == '\0' || fieldContents.Length != 1)
      {
         var message = String.Format(
            Messages.InvalidTimestampDegreeOfPrecision,
            fieldSpecification.FieldDescription);
         log.LogError(message, lineNumber, fieldSpecification, fieldContents.ToString());
         return DegreeOfPrecisionComponent.NotPresent;
      }

      log.LogFieldPresent(lineNumber, fieldSpecification, fieldContents);
      return new DegreeOfPrecisionComponent(value);
   }
}
