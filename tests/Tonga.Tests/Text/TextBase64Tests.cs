using System;
using Tonga.Bytes;
using Tonga.IO;
using Tonga.Text;
using Xunit;
using Base64Encoded = Tonga.Text.Base64Encoded;

namespace Tonga.Tests.Text;

public sealed class TextBase64Tests
{
    [Theory]
    [InlineData("A fancy text")]
    [InlineData("A fancy text with \n line break")]
    [InlineData("A fancy text with € special character")]
    public void EncodesText(string str)
    {
        using var tempFile = new TempFile("test.txt");
        new FullRead(
            new TeeOnRead(
                new Base64Encoded(str),
                new BytesMorph(new Uri(tempFile.Value()))
            )
        ).Trigger();

        Assert.True(
            new Comparable(
                new Uri(tempFile.Value())
            ).CompareTo(
                new Base64Encoded(str)
            ) == 0
        );
    }

    [Theory]
    [InlineData("A fancy text")]
    [InlineData("A fancy text with \n line break")]
    [InlineData("A fancy text with € special character")]
    public void EncodesString(string text)
    {
        AssertText.Equal(
            new Base64Encoded(new BytesMorph(text)),
            new Base64Encoded(text)
        );
    }
}
