

using System;
using Xunit;
using Tonga.Bytes;
using Tonga.IO;

namespace Tonga.Text.Test
{
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
                new LengthOf(
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
                ).Value();

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
}
