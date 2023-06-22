namespace HL7.Net.Tests.Unit.Core;

public class SpanExtensionsTests
{
   #region ToFields Method Tests
   // ==========================================================================
   // ==========================================================================

   [Fact]
   public void SpanExtensions_ToFields_ShouldReturnFieldEnumerator()
   {
      // Arrange.
      var span = "This is a test|This is only a test|For the next sixty seconds...".AsSpan();
      var fieldSeparator = '|';

      // Act.
      var result = span.ToFields(fieldSeparator);

      // Assert.
      result.Current.ToString().Should().BeEmpty();
   }

   #endregion

   #region ToLines Method Tests
   // ==========================================================================
   // ==========================================================================

   [Fact]
   public void SpanExtensions_ToLines_ShouldReturnLineEnumerator()
   {
      // Arrange.
      var span = "This is a test\r\nThis is only a test\r\nFor the next sixty seconds...".AsSpan();

      // Act.
      var result = span.ToLines();

      // Assert.
      result.Current.ToString().Should().BeEmpty();
   }

   #endregion

   #region GetDecodedString Method Tests
   // ==========================================================================
   // ==========================================================================

   [Fact]
   public void SpanExtensions_GetDecodedString_ShouldReturnExpectedResult_WhenSpanContainsNoEscapedSequences()
   {
      // Arrange.
      var encodingDetails = EncodingDetails.DefaultEncodingDetails;
      var text = "abc...QWERTY";
      var span = text.AsSpan();

      // Act.
      var decoded = span.GetDecodedString(encodingDetails);

      // Assert.
      decoded.Should().Be(text);
   }

   [Theory]
   [InlineData(DefaultSeparators.FieldSeparator)]
   [InlineData(DefaultSeparators.ComponentSeparator)]
   [InlineData(DefaultSeparators.RepetitionSeparator)]
   [InlineData(DefaultSeparators.EscapeCharacter)]
   [InlineData(DefaultSeparators.SubComponentSeparator)]
   public void SpanExtensions_GetDecodedString_ShouldReturnExpectedResult_WhenSpanContainsSingleEscapeSequence(Char escapedCharacter)
   {
      // Arrange.
      var encodingDetails = EncodingDetails.DefaultEncodingDetails;
      var text = $"abc{encodingDetails.EscapeCharacter}{escapedCharacter}QWERTY";
      var span = text.AsSpan();

      var expected = $"abc{escapedCharacter}QWERTY";

      // Act.
      var decoded = span.GetDecodedString(encodingDetails);

      // Assert.
      decoded.Should().Be(expected);
   }

   [Theory]
   [InlineData(DefaultSeparators.FieldSeparator)]
   [InlineData(DefaultSeparators.ComponentSeparator)]
   [InlineData(DefaultSeparators.RepetitionSeparator)]
   [InlineData(DefaultSeparators.EscapeCharacter)]
   [InlineData(DefaultSeparators.SubComponentSeparator)]
   public void SpanExtensions_GetDecodedString_ShouldReturnExpectedResult_WhenSpanContainsMultipleEscapeSequences(Char escapedCharacter)
   {
      // Arrange.
      var encodingDetails = EncodingDetails.DefaultEncodingDetails;
      var text = $"abc{encodingDetails.EscapeCharacter}{escapedCharacter}QWERTY{encodingDetails.EscapeCharacter}{escapedCharacter}123{encodingDetails.EscapeCharacter}{escapedCharacter}xyz";
      var span = text.AsSpan();

      var expected = $"abc{escapedCharacter}QWERTY{escapedCharacter}123{escapedCharacter}xyz";

      // Act.
      var decoded = span.GetDecodedString(encodingDetails);

      // Assert.
      decoded.Should().Be(expected);
   }

   [Fact]
   public void SpanExtensions_GetDecodedString_ShouldReturnExpectedResult_WhenSpanContainsDifferentEscapeSequences()
   {
      // Arrange.
      var encodingDetails = EncodingDetails.DefaultEncodingDetails;
      var text = $"abc{encodingDetails.EscapeCharacter}{encodingDetails.SubComponentSeparator}QWERTY{encodingDetails.EscapeCharacter}{encodingDetails.EscapeCharacter}123{encodingDetails.EscapeCharacter}{encodingDetails.RepetitionSeparator}xyz{encodingDetails.EscapeCharacter}{encodingDetails.ComponentSeparator}asdf";
      var span = text.AsSpan();

      var expected = $"abc{encodingDetails.SubComponentSeparator}QWERTY{encodingDetails.EscapeCharacter}123{encodingDetails.RepetitionSeparator}xyz{encodingDetails.ComponentSeparator}asdf";

      // Act.
      var decoded = span.GetDecodedString(encodingDetails);

      // Assert.
      decoded.Should().Be(expected);
   }

   [Fact]
   public void SpanExtensions_GetDecodedString_ShouldReturnExpectedResult_WhenSpanIsEmpty()
   {
      // Arrange.
      var encodingDetails = EncodingDetails.DefaultEncodingDetails;
      var span = String.Empty.AsSpan();

      // Act.
      var decoded = span.GetDecodedString(encodingDetails);

      // Assert.
      decoded.Should().BeEmpty();
   }

   [Fact]
   public void SpanExtensions_GetDecodedString_ShouldReturnExpectedResult_WhenSpanContainsInvalidEscapeSequence()
   {
      // Arrange.
      var encodingDetails = EncodingDetails.DefaultEncodingDetails;
      var text = @"This is a test\This is only a test";
      var span = text.AsSpan();

      // Act.
      var decoded = span.GetDecodedString(encodingDetails);

      // Assert.
      decoded.Should().Be(text);
   }

   #endregion
}
