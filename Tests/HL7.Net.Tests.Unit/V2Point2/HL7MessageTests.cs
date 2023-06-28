namespace HL7.Net.Tests.Unit.V2Point2;

public class HL7MessageTests
{
   #region Parse Method Tests
   // ==========================================================================
   // ==========================================================================

   [Fact]
   public void HL7Message_Parse_ShouldReturnExpectedResult_WhenMessageIsAdmitPatient()
   {
      // Arrange.
      var messageText = TestMessages.AdmitPatient;
      var defaultTimezoneOffset = new TimeSpan(1, 0, 0);

      // Act.
      var message = HL7Message.Parse(
         messageText.AsSpan(),
         defaultTimezoneOffset,
         out var log);

      // Assert.
      message.Should().NotBeNull();
      log.Should().NotBeNull();

      message.Segments.Should().HaveCount(5);
      message.Segments[0].SegmentID.Should().Be(SegmentIDs.MessageHeaderSegment);
      message.Segments[1].SegmentID.Should().Be(SegmentIDs.EventTypeSegment);
      message.Segments[2].SegmentID.Should().Be(SegmentIDs.PatientIdentificationSegment);
      message.Segments[3].SegmentID.Should().Be(SegmentIDs.NextOfKinSegment);
      message.Segments[4].SegmentID.Should().Be(SegmentIDs.PatientVisitSegment);
   }

   #endregion
}
