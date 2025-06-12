using Tonga.Text;
using Xunit;

namespace Tonga.Tests.Text;

public sealed class IsNumberTests
{
    [Fact]
    public void DetectsNumber()
    {
        Assert.True(
            new IsNumber("1,234.56").IsTrue()
        );
    }

    [Fact]
    public void DetectsCustomCultureNumber()
    {
        Assert.True(
            new IsNumber(
                "1234,56",
                new System.Globalization.NumberFormatInfo
                {
                    NumberDecimalSeparator = ","
                }
            ).IsTrue()
        );
    }

    [Fact]
    public void DetectsNumberFromText()
    {
        Assert.True(
            new IsNumber("1,234.56".AsText()).IsTrue()
        );
    }

    [Fact]
    public void DetectsCustomCultureNumberFromText()
    {
        Assert.True(
            new IsNumber(
                "1234,56".AsText(),
                new System.Globalization.NumberFormatInfo
                {
                    NumberDecimalSeparator = ","
                }
            ).IsTrue(),
            "Can't read number from text using custom format provider"
        );
    }

    [Fact]
    public void DetectsNoNumber()
    {
        Assert.False(
            new IsNumber("not a number").IsTrue()
        );
    }

    [Fact]
    public void DetectsNoNumberFromText()
    {
        Assert.False(
            new IsNumber("not a number".AsText()).IsTrue()
        );
    }
}
