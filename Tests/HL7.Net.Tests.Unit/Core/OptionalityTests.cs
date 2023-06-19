namespace HL7.Net.Tests.Unit.Core;

public class OptionalityTests
{
   #region Value Tests
   // ==========================================================================
   // ==========================================================================

   [Fact]
   public void Optionality_DefaultValue_ShouldBeOptional()
   {
      // Act.
      Optionality sut = default!;

      // Assert.
      sut.Should().Be(Optionality.Optional);
   }

   #endregion

   #region IsDefined Tests
   // ==========================================================================
   // ==========================================================================

   [Theory]
   [InlineData(Optionality.Optional)]
   [InlineData(Optionality.Required)]
   [InlineData(Optionality.Conditional)]
   public void Optionality_IsDefined_ShouldReturnTrue_WhenValueIsADefinedMember(Optionality value)
      => value.IsDefined().Should().BeTrue();

   [Fact]
   public void Optionality_IsDefined_ShouldReturnFalse_WhenValueIsNotADefinedMember()
   {
      // Arrange.
      var value = (Optionality)99;

      // Act/assert.
      value.IsDefined().Should().BeFalse();
   }

   #endregion
}
