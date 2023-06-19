namespace HL7.Net.Tests.Unit.Core;

public class RepetitionTests
{
   #region Constructor (String) Tests
   // ==========================================================================
   // ==========================================================================

   [Fact]
   public void Repetition_StringConstructor_ReturnsExpectedObject_WhenSpecificationIsN()
   {
      var specification = "N";

      // Act.
      var sut = new Repetition(specification);

      // Assert.
      sut.MaxRepetitions.Should().Be(0);
   }

   [Fact]
   public void Repetition_StringConstructor_ReturnsExpectedObject_WhenSpecificationIsY()
   {
      var specification = "Y";

      // Act.
      var sut = new Repetition(specification);

      // Assert.
      sut.MaxRepetitions.Should().Be(Int32.MaxValue);
   }

   [Fact]
   public void Repetition_StringConstructor_ReturnsExpectedObject_WhenSpecificationIsValidInteger()
   {
      var maxRepetitions = 42;
      var specification = maxRepetitions.ToString();

      // Act.
      var sut = new Repetition(specification);

      // Assert.
      sut.MaxRepetitions.Should().Be(maxRepetitions);
   }

   [Fact]
   public void Repetition_StringConstructor_ReturnsExpectedObject_WhenSpecificationIsNotSupplied()
   {
      // Act.
      var sut = new Repetition();

      // Assert.
      sut.MaxRepetitions.Should().Be(0);
   }

   [Fact]
   public void Repetition_StringConstructor_ShouldThrowArgumentNullException_WhenSpecificationIsNull()
   {
      String specification = null!;
      var act = () => _ = new Repetition(specification);

      // Act/assert.
      act.Should().Throw<ArgumentNullException>()
         .WithParameterName(nameof(specification));
   }

   [Theory]
   [InlineData("")]
   [InlineData("\t")]
   public void Repetition_StringConstructor_ShouldThrowArgumentException_WhenSpecificationIsEmpty(String specification)
   {
      var act = () => _ = new Repetition(specification);

      // Act/assert.
      act.Should().Throw<ArgumentException>()
         .WithParameterName(nameof(specification));
   }

   [Theory]
   [InlineData("X")]
   [InlineData("0")]
   [InlineData("-1")]
   public void Repetition_StringConstructor_ShouldThrowArgumentException_WhenSpecificationIsInvalid(String specification)
   {
      var act = () => _ = new Repetition(specification);
      var expectedMessage = Messages.InvalidRepetitionSpecification;

      // Act/assert.
      act.Should().Throw<ArgumentException>()
         .WithParameterName(nameof(specification))
         .WithMessage(expectedMessage + "*");
   }

   #endregion

   #region Constructor (Int32) Tests
   // ==========================================================================
   // ==========================================================================

   [Fact]
   public void Repetition_Int32Constructor_ShouldReturnExpectedObject_WhenRepetitionsParameterIsValid()
   {
      // Arrange.
      var repetitions = 42;

      // Act.
      var sut = new Repetition(repetitions);

      // Assert.
      sut.MaxRepetitions.Should().Be(repetitions);
   }

   [Theory]
   [InlineData(1)]
   [InlineData(0)]
   [InlineData(-1)]
   public void Repetition_StringConstructor_ShouldThrowArgumentOutOfRangeException_WhenRepetitionsParameterIsInvalid(Int32 repetitions)
   {
      var act = () => _ = new Repetition(repetitions);
      var expectedMessage = Messages.InvalidNumberOfRepetitionsAllowed;

      // Act/assert.
      act.Should().Throw<ArgumentOutOfRangeException>()
         .WithParameterName(nameof(repetitions))
         .WithMessage(expectedMessage + "*");
   }

   #endregion

   #region Implicit Conversion (String) Tests
   // ==========================================================================
   // ==========================================================================

   [Fact]
   public void Repetition_StringImplicitConversion_ShouldReturnExpectedObject_WhenStringSpecificationIsValid()
   {
      // Arrange.
      var specification = "Y";
      Repetition sut;

      // Act.
      sut = specification;

      // Assert.
      sut.MaxRepetitions.Should().Be(Int32.MaxValue);
   }

   #endregion

   #region Implicit Conversion (Int32) Tests
   // ==========================================================================
   // ==========================================================================

   [Fact]
   public void Repetition_Int32ImplicitConversion_ShouldReturnExpectedObject_WhenRepetitionsParameterIsValid()
   {
      // Arrange.
      var repetitions = 42;
      Repetition sut;

      // Act.
      sut = repetitions;

      // Assert.
      sut.MaxRepetitions.Should().Be(repetitions);
   }

   #endregion

   #region MaxRepetitions Property Tests
   // ==========================================================================
   // ==========================================================================

   [Fact]
   public void Repetition_MaxRepetitions_ShouldReturnZero_WhenStringSpecificationIsN()
   {
      // Arrange.
      var sut = new Repetition("N");

      // Act/assert.
      sut.MaxRepetitions.Should().Be(0);
   }

   [Fact]
   public void Repetition_MaxRepetitions_ShouldReturnInt32Max_WhenStringSpecificationIsY()
   {
      // Arrange.
      var sut = new Repetition("Y");

      // Act/assert.
      sut.MaxRepetitions.Should().Be(Int32.MaxValue);
   }

   [Fact]
   public void Repetition_MaxRepetitions_ShouldReturnExpectedValue_WhenStringSpecificationIsInteger()
   {
      // Arrange.
      var repetitions = 42;
      var specification = repetitions.ToString();
      var sut = new Repetition(specification);

      // Act/assert.
      sut.MaxRepetitions.Should().Be(repetitions);
   }

   [Fact]
   public void Repetition_MaxRepetitions_ShouldReturnExpectedValue_WhenIntegerConstructorIsUsed()
   {
      // Arrange.
      var repetitions = 42;
      var sut = new Repetition(repetitions);

      // Act/assert.
      sut.MaxRepetitions.Should().Be(repetitions);
   }

   #endregion

   #region RepetitionAllowed Property Tests
   // ==========================================================================
   // ==========================================================================

   [Fact]
   public void Repetition_RepetitionAllowed_ShouldReturnFalse_WhenStringSpecificationIsN()
   {
      // Arrange.
      var sut = new Repetition("N");

      // Act/assert.
      sut.RepetitionAllowed.Should().BeFalse();
   }

   [Theory]
   [InlineData("Y")]
   [InlineData("42")]
   public void Repetition_RepetitionAllowed_ShouldReturnTrue_WhenStringSpecificationIsYOrInteger(String specification)
   {
      // Arrange.
      var sut = new Repetition(specification);

      // Act/assert.
      sut.RepetitionAllowed.Should().BeTrue();
   }

   [Fact]
   public void Repetition_RepetitionAllowed_ShouldReturnTrue_WhenIntegerConstructorIsUsed()
   {
      // Arrange.
      var repetitions = 42;
      var sut = new Repetition(repetitions);

      // Act/assert.
      sut.RepetitionAllowed.Should().BeTrue();
   }

   #endregion

   #region ToString Method Tests
   // ==========================================================================
   // ==========================================================================

   [Theory]
   [InlineData("N")]
   [InlineData("Y")]
   [InlineData("42")]
   public void Repetition_ToString_ShouldReturnOriginalSpecification_WhenStringConstructorIsUsed(String specification)
   {
      // Arrange.
      var sut = new Repetition(specification);

      // Act/assert.
      sut.ToString().Should().Be(specification);
   }

   [Fact]
   public void Repetition_ToString_ShouldReturnStringNumberOfRepetitions_WhenIntegerConstructorIsUsed()
   {
      // Arrange.
      var repetitions = 42;
      var sut = new Repetition(repetitions);

      var expectedResult = repetitions.ToString();

      // Act/assert.
      sut.ToString().Should().Be(expectedResult);
   }

   #endregion
}
