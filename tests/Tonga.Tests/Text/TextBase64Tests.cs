

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
                        new LiveText(
                            new BytesBase64(
                                new BytesOf(
                                    new LiveText(text)
                                )
                            )
                        ).AsString(),
                        new OutputTo(new Uri(tempFile.Value()))
                    )
                ).Value();

                Assert.True(
                    new Comparable(
                        new LiveText(
                            new Uri(tempFile.Value())
                        )
                    ).CompareTo(
                        new TextBase64(
                            new LiveText(text)
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
                new LiveText(
                    new BytesBase64(
                        new BytesOf(
                            new LiveText(text)
                        )
                    )
                ).AsString(),
                new TextBase64(text).AsString()
            );
        }
    }
}
