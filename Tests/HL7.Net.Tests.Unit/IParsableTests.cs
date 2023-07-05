// Ignore Spelling: Parsable

namespace HL7.Net.Tests.Unit;

// See https://blog.ploeh.dk/2011/05/09/GenericunittestingwithxUnit.net/ for 
// more info on abstract test classes.
public abstract class IParsableTests<T> where T : IParsable<T>
{
   protected readonly T _notPresent;
   protected readonly T _presentButNull;
   protected readonly FieldSpecification _fieldSpecification;
   protected readonly EncodingDetails _encodingDetails;
   protected const Int32 _lineNumber = 10;

   public IParsableTests(
      T notPresent, 
      T presentButNull,
      FieldSpecification fieldSpecification,
      EncodingDetails encodingDetails)
   {
      _notPresent = notPresent;
      _presentButNull = presentButNull;
      _fieldSpecification = fieldSpecification;
      _encodingDetails = encodingDetails;
   }

   #region Parse Method Tests
   // ==========================================================================
   // ==========================================================================

   [Theory]
   [InlineData(Optionality.Optional)]
   [InlineData(Optionality.Required)]
   public void IParsable_Parse_ShouldReturnNotPresent_WhenContentsAreEmpty(Optionality optionality)
   {
      // Arrange.
      var separator = _encodingDetails.FieldSeparator;
      var fieldContents = "";
      var line = $"{separator}{fieldContents}{separator}".AsSpan();
      var fieldEnumerator = line.ToFields(separator, _encodingDetails.EscapeCharacter);
      fieldEnumerator.MoveNext();
      var versionID = T0104_VersionID.V2Point3;
      var log = new ProcessingLog();
      var fieldSpecification = _fieldSpecification with { Optionality = optionality };

      // Act.
      var field = T.Parse(
         ref fieldEnumerator,
         fieldSpecification,
         versionID,
         _lineNumber,
         log);

      // Assert.
      field.Should().Be(_notPresent);
   }

   [Theory]
   [InlineData(Optionality.Optional)]
   [InlineData(Optionality.Required)]
   public void IParsable_Parse_ShouldLogFieldNotPresent_WhenElementContentsAreEmpty(Optionality optionality)
   {
      var separator = _encodingDetails.ComponentSeparator;
      var fieldContents = "";
      var line = $"{separator}{fieldContents}{separator}".AsSpan();
      var fieldEnumerator = line.ToFields(separator, _encodingDetails.EscapeCharacter);
      fieldEnumerator.MoveNext();
      var versionID = T0104_VersionID.V2Point8;

      var mock = new Mock<IProcessingLog>();
      mock.Setup(m => m.LogFieldNotPresent(It.IsAny<Int32>(), It.IsAny<FieldSpecification>())).Verifiable();
      var log = mock.Object;

      var fieldSpecification = _fieldSpecification with { Optionality = optionality };

      // Act.
      _ = T.Parse(
         ref fieldEnumerator,
         fieldSpecification,
         versionID,
         _lineNumber,
         log);

      // Assert.
      mock.Verify(m => m.LogFieldNotPresent(_lineNumber, fieldSpecification), Times.Once); 
   }

   [Theory]
   [InlineData(Optionality.Optional)]
   [InlineData(Optionality.Required)]
   public void IParsable_Parse_ShouldReturnNotPresent_WhenElementSequenceIsBeyondEndOfData(Optionality optionality)
   {
      // Arrange.
      var separator = _encodingDetails.SubComponentSeparator;
      var line = $"ASDF{separator}qwerty{separator}".AsSpan();
      var fieldEnumerator = line.ToFields(separator, _encodingDetails.EscapeCharacter);
      fieldEnumerator.MoveNext();
      fieldEnumerator.MoveNext();
      var versionID = T0104_VersionID.V2Point3Point1;
      var log = new ProcessingLog();
      var fieldSpecification = _fieldSpecification with { Optionality = optionality, Sequence = 3 };

      // Act.
      var field = T.Parse(
         ref fieldEnumerator,
         fieldSpecification,
         versionID,
         _lineNumber,
         log);

      // Assert.
      field.Should().Be(_notPresent);
   }

   [Theory]
   [InlineData(Optionality.Optional)]
   [InlineData(Optionality.Required)]
   public void IParsable_Parse_ShouldLogFieldNotPresent_WhenElementSequenceIsBeyondEndOfData(Optionality optionality)
   {
      // Arrange.
      var separator = _encodingDetails.FieldSeparator;
      var line = $"ASDF{separator}qwerty{separator}".AsSpan();
      var fieldEnumerator = line.ToFields(separator, _encodingDetails.EscapeCharacter);
      fieldEnumerator.MoveNext();
      fieldEnumerator.MoveNext();
      var versionID = T0104_VersionID.V2Point5;

      var mock = new Mock<IProcessingLog>();
      mock.Setup(m => m.LogFieldNotPresent(It.IsAny<Int32>(), It.IsAny<FieldSpecification>())).Verifiable();
      var log = mock.Object;

      var fieldSpecification = _fieldSpecification with { Optionality = optionality, Sequence = 3 };

      // Act.
      _ = T.Parse(
         ref fieldEnumerator,
         fieldSpecification,
         versionID,
         _lineNumber,
         log);

      // Assert.
      mock.Verify(m => m.LogFieldNotPresent(_lineNumber, fieldSpecification), Times.Once);
   }

   [Fact]
   public void IParsable_Parse_ShouldReturnPresentButNull_WhenContentsAreTwoDoubleQuotes()
   {
      // Arrange.
      var separator = _encodingDetails.FieldSeparator;
      var fieldContents = GeneralConstants.PresentButNullValue;
      var line = $"{separator}{fieldContents}{separator}".AsSpan();
      var fieldEnumerator = line.ToFields(separator, _encodingDetails.EscapeCharacter);
      fieldEnumerator.MoveNext();
      var versionID = T0104_VersionID.V2Point7Point1;
      var log = new ProcessingLog();

      // Act.
      var field = T.Parse(
         ref fieldEnumerator,
         _fieldSpecification,
         versionID,
         _lineNumber,
         log);

      // Assert.
      field.Should().Be(_presentButNull);
   }

   [Fact]
   public void IParsable_Parse_ShouldLogFieldPresentButNull_WhenContentsAreTwoDoubleQuotes()
   {
      // Arrange.
      var separator = _encodingDetails.FieldSeparator;
      var fieldContents = GeneralConstants.PresentButNullValue;
      var line = $"{separator}{fieldContents}{separator}".AsSpan();
      var fieldEnumerator = line.ToFields(separator, _encodingDetails.EscapeCharacter);
      fieldEnumerator.MoveNext();
      var versionID = T0104_VersionID.V2Point6;

      var mock = new Mock<IProcessingLog>();
      mock.Setup(m => m.LogFieldPresentButNull(It.IsAny<Int32>(), It.IsAny<FieldSpecification>())).Verifiable();
      var log = mock.Object;


      // Act.
      _ = T.Parse(
         ref fieldEnumerator,
         _fieldSpecification,
         versionID,
         _lineNumber,
         log);

      // Assert.
      mock.Verify(m => m.LogFieldPresentButNull(_lineNumber, _fieldSpecification), Times.Once);
   }

   #endregion
}
