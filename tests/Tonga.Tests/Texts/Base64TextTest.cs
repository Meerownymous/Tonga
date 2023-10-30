

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
        public void DecodesFromFile(string text)
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
                        new Base64Decoded(
                            AsText._(
                                new Uri(tempFile.Value())
                            )
                        )
                    ).CompareTo(
                        AsText._(text)
                    ) == 0
                );
            }
        }
    }
}
