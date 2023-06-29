namespace HL7.Net.Tests.Unit.Core;

public class PersonNameFieldTests
{
   private static readonly EncodingDetails _encodingDetails = EncodingDetails.DefaultEncodingDetails;
   private static readonly FieldSpecification _fieldSpecification = new(
      "PID",
      1,
      "PatientName",
      26,
      HL7Datatype.PN_PersonName,
      Optionality.Required,
      "N");
   private const Int32 _lineNumber = 10;

   private const String _familyName = "Smith";
   private const String _givenName = "John";
   private const String _middleName = "J";
   private const String _suffix = "III";
   private const String _prefix = "DR";
   private const String _degree = "PHD";

   #region NotPresent Instance Tests
   // ==========================================================================
   // ==========================================================================

   [Fact]
   public void PersonNameField_NotPresent_ShouldReturnExpectedValue()
   {
      // Act.
      var sut = PersonNameField.NotPresent;

      // Assert.
      sut.Should().NotBeNull();
      sut.FamilyName.Should().Be(StringField.NotPresent);
      sut.GivenName.Should().Be(StringField.NotPresent);
      sut.MiddleNameOrInitial.Should().Be(StringField.NotPresent);
      sut.Suffix.Should().Be(StringField.NotPresent);
      sut.Prefix.Should().Be(StringField.NotPresent);
      sut.Degree.Should().Be(StringField.NotPresent);
      sut.Presence.Should().Be(Presence.NotPresent);
   }

   #endregion

   #region PresentButNull Instance Tests
   // ==========================================================================
   // ==========================================================================

   [Fact]
   public void PersonNameField_PresentButNull_ShouldReturnExpectedValue()
   {
      // Act.
      var sut = PersonNameField.PresentButNull;

      // Assert.
      sut.Should().NotBeNull();
      sut.FamilyName.Should().Be(StringField.NotPresent);
      sut.GivenName.Should().Be(StringField.NotPresent);
      sut.MiddleNameOrInitial.Should().Be(StringField.NotPresent);
      sut.Suffix.Should().Be(StringField.NotPresent);
      sut.Prefix.Should().Be(StringField.NotPresent);
      sut.Degree.Should().Be(StringField.NotPresent);
      sut.Presence.Should().Be(Presence.PresentButNull);
   }

   #endregion

   #region Parse Method Tests
   // ==========================================================================
   // ==========================================================================

   [Fact]
   public void PersonNameField_Parse_ShouldReturnExpectedValue_WhenAllComponentsSupplied()
   {
      // Arrange.
      var line = $"PID|{_familyName}^{_givenName}^{_middleName}^{_suffix}^{_prefix}^{_degree}|asdf".AsSpan();
      var fieldEnumerator = line.ToFields(_encodingDetails.FieldSeparator, _encodingDetails.EscapeCharacter);
      fieldEnumerator.MoveNext();
      var log = new ProcessingLog();

      // Act.
      var field = PersonNameField.Parse(
         ref fieldEnumerator,
         _encodingDetails,
         _fieldSpecification,
         _lineNumber,
         log);

      // Assert.
      field.Should().NotBeNull();

      field.FamilyName.Value.Should().Be(_familyName);
      field.GivenName.Value.Should().Be(_givenName);
      field.MiddleNameOrInitial.Value.Should().Be(_middleName);
      field.Suffix.Value.Should().Be(_suffix);
      field.Prefix.Value.Should().Be(_prefix);
      field.Degree.Value.Should().Be(_degree);
   }

   [Fact]
   public void PersonNameField_Parse_ShouldLogExpectedEntry_WhenAllComponentsSupplied()
   {
      // Arrange.
      var fieldContents = $"{_familyName}^{_givenName}^{_middleName}^{_suffix}^{_prefix}^{_degree}";
      var line = $"PID|{fieldContents}|asdf".AsSpan();
      var fieldEnumerator = line.ToFields(_encodingDetails.FieldSeparator, _encodingDetails.EscapeCharacter);
      fieldEnumerator.MoveNext();
      var log = new ProcessingLog();

      var expectedLogEntry = LogEntry.GetFieldPresentEntry(
         _lineNumber,
         _fieldSpecification.FieldDescription, 
         fieldContents);

      // Act.
      _ = PersonNameField.Parse(
         ref fieldEnumerator,
         _encodingDetails,
         _fieldSpecification,
         _lineNumber,
         log);

      // Assert.
      log.Should().Contain(expectedLogEntry);
   }

   [Fact]
   public void PersonNameField_Parse_ShouldReturnExpectedValue_WhenSubsetOfComponentsSupplied()
   {
      // Arrange.
      var line = $"PID|{_familyName}^{_givenName}^^^{_prefix}|asdf".AsSpan();
      var fieldEnumerator = line.ToFields(_encodingDetails.FieldSeparator, _encodingDetails.EscapeCharacter);
      fieldEnumerator.MoveNext();
      var log = new ProcessingLog();

      // Act.
      var field = PersonNameField.Parse(
         ref fieldEnumerator,
         _encodingDetails,
         _fieldSpecification,
         _lineNumber,
         log);

      // Assert.
      field.Should().NotBeNull();

      field.FamilyName.Value.Should().Be(_familyName);
      field.GivenName.Value.Should().Be(_givenName);
      field.MiddleNameOrInitial.Presence.Should().Be(Presence.NotPresent);
      field.Suffix.Presence.Should().Be(Presence.NotPresent);
      field.Prefix.Value.Should().Be(_prefix);
      field.Degree.Presence.Should().Be(Presence.NotPresent);
   }

   [Theory]
   [InlineData(Optionality.Optional)]
   [InlineData(Optionality.Required)]
   public void PersonNameField_Parse_ShouldReturnNotPresentInstance_WhenFieldIsBeyondEndOfSuppliedFields(Optionality optionality)
   {
      // Arrange.
      var line = "PID|asdf".AsSpan();
      var fieldEnumerator = line.ToFields(_encodingDetails.FieldSeparator, _encodingDetails.EscapeCharacter);
      fieldEnumerator.MoveNext();
      fieldEnumerator.MoveNext();
      var log = new ProcessingLog();
      var fieldSpecification = _fieldSpecification with { Optionality = optionality, Sequence = 2 };

      // Act.
      var field = PersonNameField.Parse(
         ref fieldEnumerator,
         _encodingDetails,
         fieldSpecification,
         _lineNumber,
         log);

      // Assert.
      field.Should().Be(PersonNameField.NotPresent);
   }

   [Fact]
   public void PersonNameField_Parse_ShouldLogFieldNotPresent_WhenOptionalFieldIsBeyondEndOfSuppliedFields()
   {
      // Arrange.
      var line = "PID|asdf".AsSpan();
      var fieldEnumerator = line.ToFields(_encodingDetails.FieldSeparator, _encodingDetails.EscapeCharacter);
      fieldEnumerator.MoveNext();
      fieldEnumerator.MoveNext();
      var log = new ProcessingLog();
      var fieldSpecification = _fieldSpecification with { Optionality = Optionality.Optional, Sequence = 2 };

      var expectedLogEntry = LogEntry.GetOptionalFieldNotPresentEntry(
         _lineNumber,
         fieldSpecification.FieldDescription);

      // Act.
      _ = PersonNameField.Parse(
         ref fieldEnumerator,
         _encodingDetails,
         fieldSpecification,
         _lineNumber,
         log);

      // Assert.
      log.Should().HaveCount(1);
      log.First().Should().Be(expectedLogEntry);
   }

   [Fact]
   public void PersonNameField_Parse_ShouldLogRequiredFieldNotPresent_WhenRequiredFieldIsBeyondEndOfSuppliedFields()
   {
      // Arrange.
      var line = "PID|asdf".AsSpan();
      var fieldEnumerator = line.ToFields(_encodingDetails.FieldSeparator, _encodingDetails.EscapeCharacter);
      fieldEnumerator.MoveNext();
      fieldEnumerator.MoveNext();
      var log = new ProcessingLog();
      var fieldSpecification = _fieldSpecification with { Optionality = Optionality.Required, Sequence = 2 };

      var expectedLogEntry = LogEntry.GetRequiredFieldNotPresentEntry(
         _lineNumber,
         fieldSpecification.FieldDescription);

      // Act.
      _ = PersonNameField.Parse(
         ref fieldEnumerator,
         _encodingDetails,
         fieldSpecification,
         _lineNumber,
         log);

      // Assert.
      log.Should().HaveCount(1);
      log.First().Should().Be(expectedLogEntry);
   }

   [Theory]
   [InlineData(Optionality.Optional)]
   [InlineData(Optionality.Required)]
   public void PersonNameField_Parse_ShouldReturnNotPresentInstance_WhenFieldIsEmpty(Optionality optionality)
   {
      // Arrange.
      var line = "PID||asdf".AsSpan();
      var fieldEnumerator = line.ToFields(_encodingDetails.FieldSeparator, _encodingDetails.EscapeCharacter);
      fieldEnumerator.MoveNext();
      var log = new ProcessingLog();
      var fieldSpecification = _fieldSpecification with { Optionality = optionality };

      // Act.
      var field = PersonNameField.Parse(
         ref fieldEnumerator,
         _encodingDetails,
         fieldSpecification,
         _lineNumber,
         log);

      // Assert.
      field.Should().Be(PersonNameField.NotPresent);
   }

   [Fact]
   public void PersonNameField_Parse_ShouldLogFieldNotPresent_WhenOptionalFieldIsEmpty()
   {
      // Arrange.
      var line = "PID||asdf".AsSpan();
      var fieldEnumerator = line.ToFields(_encodingDetails.FieldSeparator, _encodingDetails.EscapeCharacter);
      fieldEnumerator.MoveNext();
      var log = new ProcessingLog();
      var fieldSpecification = _fieldSpecification with { Optionality = Optionality.Optional };

      var expectedLogEntry = LogEntry.GetOptionalFieldNotPresentEntry(
         _lineNumber,
         fieldSpecification.FieldDescription);

      // Act.
      _ = PersonNameField.Parse(
         ref fieldEnumerator,
         _encodingDetails,
         fieldSpecification,
         _lineNumber,
         log);

      // Assert.
      log.Should().HaveCount(1);
      log.First().Should().Be(expectedLogEntry);
   }

   [Fact]
   public void PersonNameField_Parse_ShouldLogRequiredFieldNotPresent_WhenRequiredFieldIsEmpty()
   {
      // Arrange.
      var line = "PID||asdf".AsSpan();
      var fieldEnumerator = line.ToFields(_encodingDetails.FieldSeparator, _encodingDetails.EscapeCharacter);
      fieldEnumerator.MoveNext();
      var log = new ProcessingLog();
      var fieldSpecification = _fieldSpecification with { Optionality = Optionality.Required };

      var expectedLogEntry = LogEntry.GetRequiredFieldNotPresentEntry(
         _lineNumber,
         fieldSpecification.FieldDescription);

      // Act.
      _ = PersonNameField.Parse(
         ref fieldEnumerator,
         _encodingDetails,
         fieldSpecification,
         _lineNumber,
         log);

      // Assert.
      log.Should().HaveCount(1);
      log.First().Should().Be(expectedLogEntry);
   }

   [Fact]
   public void PersonNameField_Parse_ShouldReturnPresentButNullInstance_WhenFieldIsTwoDoubleQuotes()
   {
      // Arrange.
      var line = "PID|\"\"|asdf".AsSpan();
      var fieldEnumerator = line.ToFields(_encodingDetails.FieldSeparator, _encodingDetails.EscapeCharacter);
      fieldEnumerator.MoveNext();
      var log = new ProcessingLog();

      // Act.
      var field = PersonNameField.Parse(
         ref fieldEnumerator,
         _encodingDetails,
         _fieldSpecification,
         _lineNumber,
         log);

      // Assert.
      field.Should().Be(PersonNameField.PresentButNull);
   }

   [Fact]
   public void PersonNameField_Parse_ShouldLogFieldPresentButNull_WhenFieldIsTwoDoubleQuotes()
   {
      // Arrange.
      var line = "PID|\"\"|asdf".AsSpan();
      var fieldEnumerator = line.ToFields(_encodingDetails.FieldSeparator, _encodingDetails.EscapeCharacter);
      fieldEnumerator.MoveNext();
      var log = new ProcessingLog();

      var expectedLogEntry = LogEntry.GetFieldPresentButNullEntry(
         _lineNumber,
         _fieldSpecification.FieldDescription);

      // Act.
      _ = PersonNameField.Parse(
         ref fieldEnumerator,
         _encodingDetails,
         _fieldSpecification,
         _lineNumber,
         log);

      // Assert.
      log.Should().HaveCount(1);
      log.First().Should().Be(expectedLogEntry);
   }

   #endregion
}
