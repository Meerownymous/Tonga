using System;
using Tonga.Bytes;
using Tonga.Func;
using Tonga.IO;
using Tonga.Text;
using Xunit;

namespace Tonga.Tests.Text;

public sealed class TextBase64Tests
{
    [Theory]
    [InlineData("A fancy text")]
    [InlineData("A fancy text with \n line break")]
    [InlineData("A fancy text with € special character")]
    public void EncodesText(string text)
    {
        using (var tempFile = new TempFile("test.txt"))
        {
            ReadAll._(
                new TeeInput(
                    AsText._(
                        new Base64Encoded(
                            new AsBytes(
                                AsText._(text)
                            )
                        )
                    ).AsString(),
                    new OutputTo(new Uri(tempFile.Value()))
                )
            ).Invoke();

            Assert.True(
                new Comparable(
                    AsText._(
                        new Uri(tempFile.Value())
                    )
                ).CompareTo(
                    new TextAsBase64(
                        AsText._(text)
                    )
                ) == 0
            );
        }
    }

    [Theory]
    [InlineData("A fancy text")]
    [InlineData("A fancy text with \n line break")]
    [InlineData("A fancy text with € special character")]
    public void EncodesString(string text)
    {
        Assert.Equal(
            AsText._(
                new Base64Encoded(
                    new AsBytes(
                        AsText._(text)
                    )
                )
            ).AsString(),
            new TextAsBase64(text).AsString()
        );
    }
}
