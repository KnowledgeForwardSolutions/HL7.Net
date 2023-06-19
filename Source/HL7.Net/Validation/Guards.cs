namespace HL7.Net.Validation;

public static class Guards
{
   /// <summary>
   ///   Throw an <see cref="ArgumentOutOfRangeException"/> if the enum
   ///   <paramref name="value"/> is not a defined member of the enumeration.
   /// </summary>
   /// <param name="value">
   ///   The integer argument to validate.
   /// </param>
   /// <param name="isDefined">
   ///   Delegate that determines of <paramref name="value"/> is a member of the
   ///   enumeration.
   /// </param>
   /// <param name="message">
   ///   Message to use if an exception is thrown.
   /// </param>
   /// <param name="valueExpression">
   ///   Defaults to the caller name for <paramref name="value"/>.
   /// </param>
   /// <exception cref="ArgumentOutOfRangeException">
   ///   <paramref name="value"/> is not a member of the enumeration.
   /// </exception>
   /// <exception cref="ArgumentNullException">
   ///   <paramref name="isDefined"/> is <see langword="null"/>.
   /// </exception>
   public static void ThrowIfEnumIsNotDefined<T>(
      T value,
      Func<T, Boolean> isDefined,
      String? message = null,
      [CallerArgumentExpression(nameof(value))] String? valueExpression = null) where T : struct, System.Enum
   {
      ArgumentNullException.ThrowIfNull(isDefined, nameof(isDefined));

      if (!isDefined(value))
      {
         throw new ArgumentOutOfRangeException(
            valueExpression,
            value,
            message ?? String.Format(Messages.UndefinedEnumValue, typeof(T).Name));
      }
   }

   /// <summary>
   ///   Throw an <see cref="ArgumentOutOfRangeException"/> if the integer
   ///   <paramref name="value"/> is less than the <paramref name="lowerBound"/>.
   /// </summary>
   /// <param name="value">
   ///   The integer argument to validate.
   /// </param>
   /// <param name="lowerBound">
   ///   The minimum valid value.
   /// </param>
   /// <param name="message">
   ///   Message to use if an exception is thrown.
   /// </param>
   /// <param name="valueExpression">
   ///   Defaults to the caller name for <paramref name="value"/>.
   /// </param>
   /// <exception cref="ArgumentOutOfRangeException">
   ///   <paramref name="value"/> is less than <paramref name="lowerBound"/>.
   /// </exception>
   public static void ThrowIfLessThan(
      Int32 value,
      Int32 lowerBound,
      String? message = null,
      [CallerArgumentExpression(nameof(value))] String? valueExpression = null)
   {
      if (value < lowerBound)
      {
         throw new ArgumentOutOfRangeException(
            valueExpression,
            value,
            message ?? String.Format(Messages.ValueLessThan, lowerBound));
      }
   }

   /// <summary>
   ///   Throw an exception if <paramref name="str"/> is <see langword="null"/>, 
   ///   <see cref="String.Empty"/> or all whitespace characters.
   /// </summary>
   /// <param name="str">
   ///   The string argument to validate.
   /// </param>
   /// <param name="nullMessage">
   ///   Optional. Message to use if <paramref name="str"/> is 
   ///   <see langword="null"/>.
   /// </param>
   /// <param name="whitespaceMessage">
   ///   Optional. Message to use if <paramref name="str"/> is 
   ///   <see cref="String.Empty"/> or all whitespace characters.
   /// </param>
   /// <param name="strExpression">
   ///   Defaults to the caller name for <paramref name="str"/>.
   /// </param>
   /// <exception cref="ArgumentNullException">
   ///   <paramref name="str"/> is <see langword="null"/>.
   /// </exception>
   /// <exception cref="ArgumentException">
   ///   <paramref name="str"/> is <see cref="String.Empty"/> or all whitespace 
   ///   characters.
   /// </exception>
   public static void ThrowIfNullOrWhiteSpace(
      String? str, 
      String? nullMessage = null,
      String? whitespaceMessage = null,
      [CallerArgumentExpression(nameof(str))] String? strExpression = null)
   {
      if (str is null)
      {
         throw new ArgumentNullException(
            strExpression, 
            nullMessage ?? Messages.StringValueIsNullOrWhiteSpace);
      }

      if (String.IsNullOrWhiteSpace(str))
      {
         throw new ArgumentException(
            whitespaceMessage ?? Messages.StringValueIsNullOrWhiteSpace,
            strExpression);
      }
   }
}
