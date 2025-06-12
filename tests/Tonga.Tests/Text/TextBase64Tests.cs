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
                str.AsText()
                    .AsBytes()
                    .AsBase64Encoded()
                    .AsText()
                    .Str(),
                new Uri(tempFile.Value()).AsConduit()
            )
        ).Trigger();

        Assert.True(
            new Comparable(
                new Uri(tempFile.Value()).AsText()
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
        Assert.Equal(
            text.AsText()
                .AsBytes()
                .AsBase64Encoded()
                .AsText()
                .Str(),
            new Base64Encoded(text).Str()
        );
    }
}
