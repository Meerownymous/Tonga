using Tonga.Fact;
using Tonga.Text;
using Xunit;

namespace Tonga.Tests.Text;

public sealed class IsNumberTests
{
    [Fact]
    public void DetectsNumber()
    {
        AssertFact.True(
            new IsNumber("1,234.56")
        );
    }

    [Fact]
    public void DetectsCustomCultureNumber()
    {
        AssertFact.True(
            new IsNumber(
                "1234,56",
                new System.Globalization.NumberFormatInfo
                {
                    NumberDecimalSeparator = ","
                }
            )
        );
    }

    [Fact]
    public void DetectsNumberFromText()
    {
        AssertFact.True(
            new IsNumber("1,234.56")
        );
    }

    [Fact]
    public void DetectsCustomCultureNumberFromText()
    {
        AssertFact.True(
            new IsNumber(
                "1234,56",
                new System.Globalization.NumberFormatInfo
                {
                    NumberDecimalSeparator = ","
                }
            )
        );
    }

    [Fact]
    public void DetectsNoNumber()
    {
        AssertFact.False(
            new IsNumber("not a number")
        );
    }

    [Fact]
    public void DetectsNoNumberFromText()
    {
        AssertFact.False(
            new IsNumber("not a number")
        );
    }
}
