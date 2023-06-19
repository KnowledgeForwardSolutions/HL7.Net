namespace HL7.Net.Tests.Unit.Validation;

public class GuardsTests
{
   #region ThrowIfEnumIsNotDefined Tests
   // ==========================================================================
   // ==========================================================================

   [Fact]
   public void Guards_ThrowIfEnumIsNotDefined_ShouldNotThrow_WhenValueIsDefinedEnumMember()
   {
      // Arrange.
      var parameter = Optionality.Conditional;
      var act = () => Guards.ThrowIfEnumIsNotDefined(parameter, x => x.IsDefined());

      // Act/assert.
      act.Should().NotThrow();
   }

   [Fact]
   public void Guards_ThrowIfEnumIsNotDefined_ShouldThrowArgumentOutOfRangeException_WhenValueIsNotADefinedEnumMember()
   {
      // Arrange.
      var parameter = (Optionality)99;
      var act = () => Guards.ThrowIfEnumIsNotDefined(parameter, x => x.IsDefined());

      var expectedParameterName = nameof(parameter);
      var expectedMessage = String.Format(Messages.UndefinedEnumValue, typeof(Optionality).Name);

      // Act/assert.
      act.Should().Throw<ArgumentOutOfRangeException>()
         .WithParameterName(expectedParameterName)
         .WithMessage(expectedMessage + "*");
   }

   [Fact]
   public void Guards_ThrowIfEnumIsNotDefined_ShouldThrowWithExpectedMessage_WhenMessageIsSupplied()
   {
      // Arrange.
      var parameter = (Optionality)99;
      var message = "Not a valid option!";
      var act = () => Guards.ThrowIfEnumIsNotDefined(parameter, x => x.IsDefined(), message);

      // Act/assert.
      act.Should().Throw<ArgumentOutOfRangeException>()
         .WithMessage(message + "*");
   }

   [Fact]
   public void Guards_ThrowIfEnumIsNotDefined_ShouldThrowWithExpectedParameterName_WhenParameterNameIsSupplied()
   {
      // Arrange.
      var parameter = (Optionality)99;
      var parameterName = "Egon";
      var act = () => Guards.ThrowIfEnumIsNotDefined(parameter, x => x.IsDefined(), valueExpression: parameterName);

      // Act/assert.
      act.Should().Throw<ArgumentOutOfRangeException>()
         .WithParameterName(parameterName);
   }

   [Fact]
   public void Guards_ThrowIfEnumIsNotDefined_ShouldThrowArgumentNullException_WhenIsDefinedIsNull()
   {
      // Arrange.
      var parameter = Optionality.Required;
      Func<Optionality, Boolean> isDefined = null!;
      var act = () => Guards.ThrowIfEnumIsNotDefined(parameter, isDefined);

      // Act/assert.
      act.Should().Throw<ArgumentNullException>()
         .WithParameterName(nameof(isDefined));
   }

   #endregion

   #region ThrowIfLessThan (Integer) Tests
   // ==========================================================================
   // ==========================================================================

   [Fact]
   public void Guards_IntegerThrowIfLessThan_ShouldNotThrow_WhenValueIsGreaterThanLowerBound()
   {
      // Arrange.
      var lowerBound = 42;
      var parameter = lowerBound + 1;
      var act = () => Guards.ThrowIfLessThan(parameter, lowerBound);

      // Act/assert.
      act.Should().NotThrow();
   }

   [Fact]
   public void Guards_IntegerThrowIfLessThan_ShouldNotThrow_WhenValueIsEqualToLowerBound()
   {
      // Arrange.
      var lowerBound = 42;
      var parameter = lowerBound;
      var act = () => Guards.ThrowIfLessThan(parameter, lowerBound);

      // Act/assert.
      act.Should().NotThrow();
   }

   [Fact]
   public void Guards_IntegerThrowIfLessThan_ShouldThrowArgumentOutOfRangeException_WhenValueIsLessThanLowerBound()
   {
      // Arrange.
      var lowerBound = 42;
      var parameter = lowerBound - 1;
      var act = () => Guards.ThrowIfLessThan(parameter, lowerBound);

      var expectedParameterName = nameof(parameter);
      var expectedMessage = String.Format(Messages.ValueLessThan, lowerBound);

      // Act/assert.
      act.Should().Throw<ArgumentOutOfRangeException>()
         .WithParameterName(expectedParameterName)
         .WithMessage(expectedMessage + "*");
   }

   [Fact]
   public void Guards_IntegerThrowIfLessThan_ShouldThrowWithExpectedMessage_WhenMessageIsSupplied()
   {
      // Arrange.
      var lowerBound = 42;
      var parameter = lowerBound - 1;
      var message = "Value too low!";
      var act = () => Guards.ThrowIfLessThan(parameter, lowerBound, message);

      // Act/assert.
      act.Should().Throw<ArgumentOutOfRangeException>()
         .WithMessage(message + "*");
   }

   [Fact]
   public void Guards_IntegerThrowIfLessThan_ShouldThrowWithExpectedParameterName_WhenParameterNameIsSupplied()
   {
      // Arrange.
      var lowerBound = 42;
      var parameter = lowerBound - 1;
      var parameterName = "Bob";
      var act = () => Guards.ThrowIfLessThan(parameter, lowerBound, valueExpression: parameterName);

      // Act/assert.
      act.Should().Throw<ArgumentOutOfRangeException>()
         .WithParameterName(parameterName);
   }

   #endregion

   #region ThrowIfNullOrWhiteSpace Tests
   // ==========================================================================
   // ==========================================================================

   [Fact]
   public void Guards_ThrowIfNullOrWhiteSpace_ShouldNotThrow_WhenValueIsNotNullOrEmptyOrWhiteSpace()
   {
      // Arrange.
      var parameter = "This is a test";
      var act = () => Guards.ThrowIfNullOrWhiteSpace(parameter);

      // Act/assert.
      act.Should().NotThrow();
   }

   [Fact]
   public void Guards_ThrowIfNullOrWhiteSpace_ShouldThrowArgumentNullException_WhenValueIsNull()
   {
      // Arrange.
      String parameter = null!;
      var act = () => Guards.ThrowIfNullOrWhiteSpace(parameter);

      var expectedParameterName = nameof(parameter);

      // Act/assert.
      act.Should().Throw<ArgumentNullException>()
         .WithParameterName(expectedParameterName);
   }

   [Theory]
   [InlineData("")]
   [InlineData("\t")]
   public void Guards_ThrowIfNullOrWhiteSpace_ShouldThrowArgumentNullException_WhenValueIsEmptyOrWhiteSpace(String parameter)
   {
      // Arrange.
      var act = () => Guards.ThrowIfNullOrWhiteSpace(parameter);

      var expectedParameterName = nameof(parameter);

      // Act/assert.
      act.Should().Throw<ArgumentException>()
         .WithParameterName(expectedParameterName);
   }

   [Fact]
   public void Guards_ThrowIfNullOrWhiteSpace_ShouldThrowWithExpectedMessage_WhenNullMessageIsSupplied()
   {
      // Arrange.
      String parameter = null!;
      var nullMessage = "Can't be null!";
      var act = () => Guards.ThrowIfNullOrWhiteSpace(parameter, nullMessage);

      // Act/assert.
      act.Should().Throw<ArgumentNullException>()
         .WithMessage(nullMessage + "*");
   }

   [Fact]
   public void Guards_ThrowIfNullOrWhiteSpace_ShouldThrowWithExpectedMessage_WhenWhiteSpaceMessageIsSupplied()
   {
      // Arrange.
      String parameter = null!;
      var whitespaceMessage = "Can't empty or whitespace!";
      var act = () => Guards.ThrowIfNullOrWhiteSpace(parameter, whitespaceMessage);

      // Act/assert.
      act.Should().Throw<ArgumentNullException>()
         .WithMessage(whitespaceMessage + "*");
   }

   [Fact]
   public void Guards_ThrowIfNullOrWhiteSpace_ShouldThrowWithExpectedParameterName_WhenParameterNameIsSupplied()
   {
      // Arrange.
      String parameter = null!;
      var parameterName = "someOtherName";
      var act = () => Guards.ThrowIfNullOrWhiteSpace(parameter, strExpression: parameterName);

      // Act/assert.
      act.Should().Throw<ArgumentNullException>()
         .WithParameterName(parameterName);
   }

   #endregion
}
