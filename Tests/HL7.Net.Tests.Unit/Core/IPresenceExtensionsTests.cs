namespace HL7.Net.Tests.Unit.Core;

public class IPresenceExtensionsTests
{
   private class PresenceTester : IPresence
   {
      public Presence Presence { get; set; }
   }

   #region IsPresent Method Tests
   // ==========================================================================
   // ==========================================================================

   public static TheoryData<IPresence> IsPresentSuccessData => new()
   {
      { new PresenceTester { Presence = Presence.Present } },
      { new PresenceTester { Presence = Presence.PresentButNull } },
   };

   [Theory]
   [MemberData(nameof(IsPresentSuccessData))]
   public void IPresenceExtensions_IsPresent_ShouldReturnTrue_WhenItemIsPresent(IPresence item)
      => item.IsPresent().Should().BeTrue();

   [Fact]
   public void IPresenceExtensions_IsPresent_ShouldReturnFalse_WhenItemIsNotPresent()
   {
      // Arrange.
      var item = new PresenceTester { Presence = Presence.NotPresent };

      // Act/assert.
      item.IsPresent().Should().BeFalse();
   }

   #endregion

   #region IsPresentNotNull Method Tests
   // ==========================================================================
   // ==========================================================================

   [Fact]
   public void IPresenceExtensions_IsPresentNotNull_ShouldReturnTrue_WhenItemIsPresentAndNotNull()
   {
      // Arrange.
      var item = new PresenceTester { Presence = Presence.Present };

      // Act/assert.
      item.IsPresentNotNull().Should().BeTrue();
   }

   public static TheoryData<IPresence> IsPresentNotNullFailureData => new()
   {
      { new PresenceTester { Presence = Presence.PresentButNull } },
      { new PresenceTester { Presence = Presence.NotPresent } },
   };

   [Theory]
   [MemberData(nameof(IsPresentNotNullFailureData))]
   public void IPresenceExtensions_IsPresentNotNull_ShouldReturnFalse_WhenItemIsNotPresentOrPresentButNull(IPresence item)
      => item.IsPresentNotNull().Should().BeFalse();

   #endregion
}
