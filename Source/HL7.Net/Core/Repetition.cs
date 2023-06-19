namespace HL7.Net.Core;

/// <summary>
///   Identifies if a field may repeat as well as the maximum number of 
///   repetitions allowed.
/// </summary>
public struct Repetition
{
   private const String No = "N";
   private const String Yes = "Y";

   /// <summary>
   ///   Initialize a new <see cref="Repetition"/> struct.
   /// </summary>
   /// <param name="specification">
   ///   Optional. String repetition specification. May be 'N' for no repetition, 
   ///   'Y' for unlimited repetition or the string representation of an integer 
   ///   greater than one (1) for a specific maximum number of repetitions.
   ///   Defaults to 'N'.
   /// </param>
   /// <exception cref="ArgumentNullException">
   ///   <paramref name="specification"/> is <see langword="null"/>.
   /// </exception>
   /// <exception cref="ArgumentException">
   ///   <paramref name="specification"/> is <see cref="String.Empty"/>.
   ///   - or -
   ///   <paramref name="specification"/> is all whitespace characters.
   ///   - or -
   ///   <paramref name="specification"/> is not a valid repetition specification.
   /// </exception>
   public Repetition(String? specification = No)
   {
      Guards.ThrowIfNullOrWhiteSpace(specification);

      MaxRepetitions = specification switch
      {
         No => 0,
         Yes => Int32.MaxValue,
         _ => Int32.TryParse(specification, out var maxRepetitions) && maxRepetitions > 1
            ? maxRepetitions
            : throw new ArgumentException(
                  Messages.InvalidRepetitionSpecification,
                  nameof(specification))
      };
   }

   /// <summary>
   ///   Initialize a new <see cref="Repetition"/> struct with a specific number
   ///   of allowed repetitions.
   /// </summary>
   /// <param name="maxRepetitions">
   ///   The maximum number of repetitions allowed for the field. Must be 
   ///   greater than one (1).
   /// </param>
   /// <exception cref="ArgumentOutOfRangeException">
   ///   <paramref name="repetitions"/> is less than one (1).
   /// </exception>
   public Repetition(Int32 repetitions)
   {
      Guards.ThrowIfLessThan(
         repetitions, 
         2, 
         Messages.InvalidNumberOfRepetitionsAllowed);

      MaxRepetitions = repetitions;
   }

   public static implicit operator Repetition(String repetition) => new(repetition);

   public static implicit operator Repetition(Int32 repetitions) => new(repetitions);

   /// <summary>
   ///   The maximum number of repetitions allowed for the field or zero (0) if
   ///   repetition is not allowed.
   /// </summary>
   public Int32 MaxRepetitions { get; private set; }

   /// <summary>
   ///   <see langword="true"/> if the field allows repetition; otherwise 
   ///   <see langword="false"/>.
   /// </summary>
   public readonly Boolean RepetitionAllowed => MaxRepetitions > 1;

   /// <summary>
   ///   Return the string representation of this object.
   /// </summary>
   public override String ToString()
      => MaxRepetitions switch
      {
         0 => No,
         Int32.MaxValue => Yes,
         _ => MaxRepetitions.ToString()
      };
}
