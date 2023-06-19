namespace HL7.Net.Tests.Unit.Core;

public class LineEnumeratorTests
{
    private const string _multilineWithCRLF = "This is a test.\r\nThis is only a test.\r\nFor the next sixty seconds...";
    private const string _muiltilineWithNewLine = "This is a test.\nThis is only a test.\nFor the next sixty seconds...";

    private readonly List<string> _lines = new()
   {
      "This is a test.",
      "This is only a test.",
      "For the next sixty seconds..."
   };

    #region Constructor Tests
    // ==========================================================================
    // ==========================================================================

    [Fact]
    public void LineEnumerator_Constructor_ShouldReturnObject_WhenAllValuesSupplied()
    {
        // Arrange.
        var span = _multilineWithCRLF.AsSpan();

        // Act.
        var sut = new LineEnumerator(span);

        // Assert.
        sut.Current.ToString().Should().BeEmpty();
    }

    [Fact]
    public void LineEnumerator_Constructor_ShouldReturnObject_WhenSpanIsBasedOnNullString()
    {
        // Arrange.
        string str = null!;
        var span = str.AsSpan();

        // Act.
        var sut = new LineEnumerator(span);

        // Assert.
        sut.Current.ToString().Should().BeEmpty();
    }

    [Fact]
    public void LineEnumerator_Constructor_ShouldReturnObject_WhenSpanIsBasedOnEmptyString()
    {
        // Arrange.
        var str = string.Empty;
        var span = str.AsSpan();

        // Act.
        var sut = new LineEnumerator(span);

        // Assert.
        sut.Current.ToString().Should().BeEmpty();
    }

    #endregion

    #region Enumeration Tests
    // ==========================================================================
    // ==========================================================================

    [Fact]
    public void LineEnumerator_Enumeration_ShouldReturnExpectedResults_WhenWindowsLineTerminatorsAreUsed()
    {
        // Arrange.
        var span = _multilineWithCRLF.AsSpan();
        var sut = new LineEnumerator(span);

        // Act.
        var results = new List<string>();
        foreach (var line in sut)
        {
            results.Add(line.ToString());
        }

        // Assert.
        results.Should().BeEquivalentTo(_lines);
    }

    [Fact]
    public void LineEnumerator_Enumeration_ShouldReturnExpectedResults_WhenUnixLineTerminatorsAreUsed()
    {
        // Arrange.
        var span = _muiltilineWithNewLine.AsSpan();
        var sut = new LineEnumerator(span);

        // Act.
        var results = new List<string>();
        foreach (var line in sut)
        {
            results.Add(line.ToString());
        }

        // Assert.
        results.Should().BeEquivalentTo(_lines);
    }

    [Theory]
    [InlineData(null)]
    [InlineData("")]
    public void LineEnumerator_Enumeration_ShouldReturnEmpty_WhenSourceStringIsNullOrEmpty(string str)
    {
        // Arrange.
        var span = str.AsSpan();
        var sut = new LineEnumerator(span);

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
    public void LineEnumerator_Enumeration_ShouldReturnSingleLine_WhenSourceStringIsOneLine()
    {
        // Arrange
        var str = "The quick red fox jumped over the lazy brown dog";
        var span = str.AsSpan();
        var sut = new LineEnumerator(span);

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
    public void LineEnumerator_Enumeration_ShouldReturnExpectedResults_WhenSourceStringHasEmptyLines()
    {
        // Arrange.
        var str = "The quick red fox\n\n   \n\njumped over the lazy brown dog";
        var span = str.AsSpan();
        var sut = new LineEnumerator(span);

        var expectedResults = new List<string>()
      {
         "The quick red fox",
         string.Empty,
         "   ",
         string.Empty,
         "jumped over the lazy brown dog"
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
    public void LineEnumerator_Enumeration_ShouldReturnExpectedResults_WhenSourceStringHasLeadingEmptyLines()
    {
        // Arrange.
        var str = "\r\n\r\nThe quick red fox jumped over the lazy brown dog";
        var span = str.AsSpan();
        var sut = new LineEnumerator(span);

        var expectedResults = new List<string>()
      {
         string.Empty,
         string.Empty,
         "The quick red fox jumped over the lazy brown dog"
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
    public void LineEnumerator_Enumeration_ShouldReturnExpectedResults_WhenSourceStringHasTrailingEmptyLines()
    {
        // Arrange.
        var str = "The quick red fox jumped over the lazy brown dog\r\n\r\n";
        var span = str.AsSpan();
        var sut = new LineEnumerator(span);

        // Note only one trailing empty line in results.
        var expectedResults = new List<string>()
      {
         "The quick red fox jumped over the lazy brown dog",
         string.Empty,
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
    public void LineEnumerator_Enumeration_ShouldReturnExpectedResults_WhenSourceStringOnlyHasEmptyLines()
    {
        // Arrange.
        var str = "\n\n\n\n\n";
        var span = str.AsSpan();
        var sut = new LineEnumerator(span);

        var expectedResults = new List<string>()
      {
         string.Empty,
         string.Empty,
         string.Empty,
         string.Empty,
         string.Empty,
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
    public void LineEnumerator_Current_ShouldReturnExpectedValue_WhenObjectIsInitialized()
    {
        // Arrange. 
        var span = _muiltilineWithNewLine.AsSpan();
        var sut = new LineEnumerator(span);

        // Act/assert.
        sut.Current.ToString().Should().BeEmpty();
    }

    [Fact]
    public void LineEnumerator_Current_ShouldReturnExpectedValue_WhenMoveNextIsSuccessful()
    {
        // Arrange. 
        var span = _multilineWithCRLF.AsSpan();
        var sut = new LineEnumerator(span);
        sut.MoveNext();

        // Act/assert.
        sut.Current.ToString().Should().Be("This is a test.");
    }

    [Fact]
    public void LineEnumerator_Current_ShouldReturnExpectedValue_WhenMoveNextHitsEndOfString()
    {
        // Arrange. 
        var span = _muiltilineWithNewLine.AsSpan();
        var sut = new LineEnumerator(span);
        sut.MoveNext();
        sut.MoveNext();
        sut.MoveNext();
        sut.MoveNext();

        // Act/assert.
        sut.Current.ToString().Should().Be("For the next sixty seconds...");
    }

    #endregion

    #region GetEnumerator Method Tests
    // ==========================================================================
    // ==========================================================================

    [Fact]
    public void LineEnumerator_GetEnumerator_ShouldReturnExpectedValue()
    {
        // Arrange.
        var span = _muiltilineWithNewLine.AsSpan();
        var sut = new LineEnumerator(span);

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
    public void LineEnumerator_MoveNext_ShouldReturnFalse_WhenOriginalStringIsEmpty()
    {
        // Arrange.
        var span = string.Empty.AsSpan();
        var sut = new LineEnumerator(span);

        // Act/assert.
        sut.MoveNext().Should().BeFalse();
    }

    [Fact]
    public void LineEnumerator_MoveNext_ShouldReturnTrue_WhenOriginalStringContainsOneLine()
    {
        // Arrange.
        var span = "   ".AsSpan();
        var sut = new LineEnumerator(span);

        // Act/assert.
        sut.MoveNext().Should().BeTrue();
    }

    [Fact]
    public void LineEnumerator_MoveNext_ShouldReturnTrue_WhenOriginalStringContainsMultipleLines()
    {
        // Arrange.
        var span = _multilineWithCRLF.AsSpan();
        var sut = new LineEnumerator(span);

        // Act/assert.
        sut.MoveNext().Should().BeTrue();
    }

    [Fact]
    public void LineEnumerator_MoveNext_ShouldReturnTrue_WhenEndOfSequenceIsReached()
    {
        // Arrange.
        var span = _muiltilineWithNewLine.AsSpan();
        var sut = new LineEnumerator(span);
        sut.MoveNext();
        sut.MoveNext();
        sut.MoveNext();

        // Act/assert.
        sut.MoveNext().Should().BeFalse();
    }

    #endregion
}
