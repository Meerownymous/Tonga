

using System;
using Xunit;
using Tonga.Bytes;
using Tonga.IO;

namespace Tonga.Text.Test
{
    public sealed class Base64DecodedTextTest
    {
        [Theory]
        [InlineData("A fancy text")]
        [InlineData("A fancy text with \n line break")]
        [InlineData("A fancy text with € special character")]
        public void DecodeFromFile(string text)
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
                        new Base64Text(
                            new LiveText(
                                new Uri(tempFile.Value())
                            )
                        )
                    ).CompareTo(
                        new LiveText(text)
                    ) == 0
                );
            }
        }
    }
}
