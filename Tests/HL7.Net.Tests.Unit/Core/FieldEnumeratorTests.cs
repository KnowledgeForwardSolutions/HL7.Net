using System.ComponentModel.DataAnnotations;

namespace HL7.Net.Tests.Unit.Core;

public class FieldEnumeratorTests
{
   private const String _nextOfKinSegment = "NK1|1|NUCLEAR^NELDA^W|SPO^SPOUSE||||NK^NEXT OF KIN";
   private const Char _fieldSeparator = '|';
   private const Char _subfieldSeparator = '^';
   private const Char _escapeCharacter = '\\';

   private static readonly List<String> _fields = new()
   {
      "NK1",
      "1",
      "NUCLEAR^NELDA^W",
      "SPO^SPOUSE",
      "",
      "",
      "",
      "NK^NEXT OF KIN",
   };

   private static readonly List<String> _nameSubfields = new() { "NUCLEAR", "NELDA", "W" };
   private static readonly List<String> _relationshipSubfields = new() { "SPO", "SPOUSE" };
   private static readonly List<String> _contactRoleSubfields = new() { "NK", "NEXT OF KIN" };

   #region Constructor Tests
   // ==========================================================================
   // ==========================================================================

   [Fact]
   public void FieldEnumerator_Constructor_ShouldReturnObject_WhenAllValuesSupplied()
   {
      // Arrange.
      var span = _nextOfKinSegment.AsSpan();

      // Act.
      var sut = new FieldEnumerator(span, _fieldSeparator, _escapeCharacter);

      // Assert.
      sut.Current.ToString().Should().BeEmpty();
   }

   [Fact]
   public void FieldEnumerator_Constructor_ShouldReturnObject_WhenEscapeCharacterIsDefault()
   {
      // Arrange.
      var span = _nextOfKinSegment.AsSpan();

      // Act.
      var sut = new FieldEnumerator(span, _fieldSeparator, _escapeCharacter);

      // Assert.
      sut.Current.ToString().Should().BeEmpty();
   }

   [Fact]
   public void FieldEnumerator_Constructor_ShouldReturnObject_WhenSpanIsBasedOnNullString()
   {
      // Arrange.
      string str = null!;
      var span = str.AsSpan();

      // Act.
      var sut = new FieldEnumerator(span, _fieldSeparator, _escapeCharacter);

      // Assert.
      sut.Current.ToString().Should().BeEmpty();
   }

   [Fact]
   public void FieldEnumerator_Constructor_ShouldReturnObject_WhenSpanIsBasedOnEmptyString()
   {
      // Arrange.
      var str = string.Empty;
      var span = str.AsSpan();

      // Act.
      var sut = new FieldEnumerator(span, _fieldSeparator, _escapeCharacter);

      // Assert.
      sut.Current.ToString().Should().BeEmpty();
   }

   #endregion

   #region Enumeration Tests
   // ==========================================================================
   // ==========================================================================

   [Fact]
   public void FieldEnumerator_Enumeration_ShouldReturnExpectedResults_WhenMultipleFieldsArePresent()
   {
      // Arrange.
      var span = _nextOfKinSegment.AsSpan();
      var sut = new FieldEnumerator(span, _fieldSeparator, _escapeCharacter);

      // Act.
      var results = new List<string>();
      foreach (var field in sut)
      {
         results.Add(field.ToString());
      }

      // Assert.
      results.Should().BeEquivalentTo(_fields);
   }

   [Theory]
   [InlineData(null)]
   [InlineData("")]
   public void FieldEnumerator_Enumeration_ShouldReturnEmpty_WhenSourceStringIsNullOrEmpty(string str)
   {
      // Arrange.
      var span = str.AsSpan();
      var sut = new FieldEnumerator(span, _fieldSeparator, _escapeCharacter);

      // Act.
      var results = new List<string>();
      foreach (var line in sut)
      {
         results.Add(line.ToString());
      }

      // Assert.
      results.Should().BeEmpty();
   }

   [Fact]
   public void FieldEnumerator_Enumeration_ShouldReturnSingleLine_WhenSourceStringIsOneField()
   {
      // Arrange
      var str = _fields[3];
      var span = str.AsSpan();
      var sut = new FieldEnumerator(span, _fieldSeparator, _escapeCharacter);

      // Act.
      var results = new List<string>();
      foreach (var line in sut)
      {
         results.Add(line.ToString());
      }

      // Assert.
      results.Should().HaveCount(1);
      results.First().Should().Be(str);
   }

   [Fact]
   public void FieldEnumerator_Enumeration_ShouldReturnExpectedResults_WhenSourceStringHasEmptyFields()
   {
      // Arrange.
      var str = "First^^Last^Generation";
      var span = str.AsSpan();
      var sut = new FieldEnumerator(span, _subfieldSeparator, _escapeCharacter);

      var expectedResults = new List<string>()
      {
         "First",
         String.Empty,
         "Last",
         "Generation"
      };

      // Act.
      var results = new List<string>();
      foreach (var line in sut)
      {
         results.Add(line.ToString());
      }

      // Assert.
      results.Should().BeEquivalentTo(expectedResults);
   }

   [Fact]
   public void FieldEnumerator_Enumeration_ShouldReturnExpectedResults_WhenSourceStringHasLeadingEmptyFields()
   {
      // Arrange.
      var str = "^First^Middle^Last^Generation";
      var span = str.AsSpan();
      var sut = new FieldEnumerator(span, _subfieldSeparator, _escapeCharacter);

      var expectedResults = new List<string>()
      {
         String.Empty,
         "First",
         "Middle",
         "Last",
         "Generation"
      };

      // Act.
      var results = new List<string>();
      foreach (var line in sut)
      {
         results.Add(line.ToString());
      }

      // Assert.
      results.Should().BeEquivalentTo(expectedResults);
   }

   [Fact]
   public void FieldEnumerator_Enumeration_ShouldReturnExpectedResults_WhenSourceStringHasTrailingEmptyFields()
   {
      // Arrange.
      var str = "Title^First^Middle^Last^^";
      var span = str.AsSpan();
      var sut = new FieldEnumerator(span, _subfieldSeparator, _escapeCharacter);

      // Note only one trailing empty field in results.
      var expectedResults = new List<string>()
      {
         "Title",
         "First",
         "Middle",
         "Last",
         String.Empty
      };

      // Act.
      var results = new List<string>();
      foreach (var line in sut)
      {
         results.Add(line.ToString());
      }

      // Assert.
      results.Should().BeEquivalentTo(expectedResults);
   }

   [Fact]
   public void FieldEnumerator_Enumeration_ShouldReturnExpectedResults_WhenSourceStringContainsAnEscapedSeparatorCharacter()
   {
      // Arrange.
      var str = @"ABC|de\|fg|HIJ";
      var span = str.AsSpan();
      var sut = new FieldEnumerator(span, _fieldSeparator, _escapeCharacter);

      var expectedResults = new List<string>()
      {
         "ABC",
         @"de\|fg",
         "HIJ"
      };

      // Act.
      var results = new List<string>();
      foreach (var line in sut)
      {
         results.Add(line.ToString());
      }

      // Assert.
      results.Should().BeEquivalentTo(expectedResults);
   }

   [Fact]
   public void FieldEnumerator_Enumeration_ShouldReturnExpectedResults_WhenSourceStringContainsMultipleEscapedSeparatorCharacters()
   {
      // Arrange.
      var str = @"ABC|d\|e\|f\|g|HIJ";
      var span = str.AsSpan();
      var sut = new FieldEnumerator(span, _fieldSeparator, _escapeCharacter);

      var expectedResults = new List<string>()
      {
         "ABC",
         @"d\|e\|f\|g",
         "HIJ"
      };

      // Act.
      var results = new List<string>();
      foreach (var line in sut)
      {
         results.Add(line.ToString());
      }

      // Assert.
      results.Should().BeEquivalentTo(expectedResults);
   }

   [Fact]
   public void FieldEnumerator_Enumeration_ShouldReturnExpectedResults_WhenSourceStringContainsOneFieldWithEscapedSeparatorCharacter()
   {
      // Arrange.
      var str = @"ABC\|defgHIJ";
      var span = str.AsSpan();
      var sut = new FieldEnumerator(span, _fieldSeparator, _escapeCharacter);

      var expectedResults = new List<string>()
      {
         @"ABC\|defgHIJ"
      };

      // Act.
      var results = new List<string>();
      foreach (var line in sut)
      {
         results.Add(line.ToString());
      }

      // Assert.
      results.Should().BeEquivalentTo(expectedResults);
   }

   [Fact]
   public void FieldEnumerator_Enumeration_ShouldReturnExpectedResults_WhenSourceStringContainsOneFieldWithTrailingEscape()
   {
      // Arrange.
      var str = @"ABC\";
      var span = str.AsSpan();
      var sut = new FieldEnumerator(span, _fieldSeparator, _escapeCharacter);

      var expectedResults = new List<string>()
      {
         @"ABC\"
      };

      // Act.
      var results = new List<string>();
      foreach (var line in sut)
      {
         results.Add(line.ToString());
      }

      // Assert.
      results.Should().BeEquivalentTo(expectedResults);
   }

   #endregion

   #region Current Property Tests
   // ==========================================================================
   // ==========================================================================

   [Fact]
   public void FieldEnumerator_Current_ShouldReturnExpectedValue_WhenObjectIsInitialized()
   {
      // Arrange. 
      var span = _nextOfKinSegment.AsSpan();
      var sut = new FieldEnumerator(span, _fieldSeparator, _escapeCharacter);

      // Act/assert.
      sut.Current.ToString().Should().BeEmpty();
   }

   [Fact]
   public void FieldEnumerator_Current_ShouldReturnExpectedValue_WhenMoveNextIsSuccessful()
   {
      // Arrange. 
      var span = _nextOfKinSegment.AsSpan();
      var sut = new FieldEnumerator(span, _fieldSeparator, _escapeCharacter);
      sut.MoveNext();

      // Act/assert.
      sut.Current.ToString().Should().Be(_fields.First());
   }

   [Fact]
   public void FieldEnumerator_Current_ShouldReturnExpectedValue_WhenMoveNextHitsEndOfString()
   {
      // Arrange. 
      var span = _nextOfKinSegment.AsSpan();
      var sut = new FieldEnumerator(span, _fieldSeparator, _escapeCharacter);
      sut.MoveNext();
      sut.MoveNext();
      sut.MoveNext();
      sut.MoveNext();
      sut.MoveNext();
      sut.MoveNext();
      sut.MoveNext();
      sut.MoveNext();
      sut.MoveNext();

      // Act/assert.
      sut.Current.ToString().Should().Be(_fields.Last());
   }

   #endregion

   #region GetEnumerator Method Tests
   // ==========================================================================
   // ==========================================================================

   [Fact]
   public void FieldEnumerator_GetEnumerator_ShouldReturnExpectedValue()
   {
      // Arrange.
      var span = _nextOfKinSegment.AsSpan();
      var sut = new FieldEnumerator(span, _fieldSeparator, _escapeCharacter);

      // Act.
      var enumerator = sut.GetEnumerator();

      // Assert.
      enumerator.Current.ToString().Should().BeEmpty();
   }

   #endregion

   #region MoveNext Method Tests
   // ==========================================================================
   // ==========================================================================

   [Fact]
   public void FieldEnumerator_MoveNext_ShouldReturnFalse_WhenOriginalStringIsEmpty()
   {
      // Arrange.
      var span = string.Empty.AsSpan();
      var sut = new FieldEnumerator(span, _fieldSeparator, _escapeCharacter);

      // Act/assert.
      sut.MoveNext().Should().BeFalse();
   }

   [Fact]
   public void FieldEnumerator_MoveNext_ShouldReturnTrue_WhenOriginalStringContainsOneField()
   {
      // Arrange.
      var span = "   ".AsSpan();
      var sut = new FieldEnumerator(span, _fieldSeparator, _escapeCharacter);

      // Act/assert.
      sut.MoveNext().Should().BeTrue();
   }

   [Fact]
   public void FieldEnumerator_MoveNext_ShouldReturnTrue_WhenOriginalStringContainsMultipleFields()
   {
      // Arrange.
      var span = _nextOfKinSegment.AsSpan();
      var sut = new FieldEnumerator(span, _fieldSeparator, _escapeCharacter);

      // Act/assert.
      sut.MoveNext().Should().BeTrue();
   }

   [Fact]
   public void FieldEnumerator_MoveNext_ShouldReturnTrue_WhenEndOfSequenceIsReached()
   {
      // Arrange.
      var span = _nextOfKinSegment.AsSpan();
      var sut = new FieldEnumerator(span, _fieldSeparator, _escapeCharacter);
      sut.MoveNext();
      sut.MoveNext();
      sut.MoveNext();
      sut.MoveNext();
      sut.MoveNext();
      sut.MoveNext();
      sut.MoveNext();
      sut.MoveNext();

      // Act/assert.
      sut.MoveNext().Should().BeFalse();
   }

   #endregion
}
