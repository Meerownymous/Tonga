using System;
using Tonga.Bytes;
using Tonga.Func;
using Tonga.IO;
using Tonga.Text;
using Xunit;
using Base64Decoded = Tonga.Text.Base64Decoded;
using Base64Encoded = Tonga.Bytes.Base64Encoded;

namespace Tonga.Tests.Text
{
    public sealed class Base64DecodedTextTest
    {
        [Theory]
        [InlineData("A fancy text")]
        [InlineData("A fancy text with \n line break")]
        [InlineData("A fancy text with € special character")]
        public void DecodesFromFile(string text)
        {
            using var tempFile = new TempFile("test.txt");
            ReadAll._(
                new TeeOnRead(
                    AsText._(
                        new Base64Encoded(
                            new AsBytes(
                                AsText._(text)
                            )
                        )
                    ).AsString(),
                    new AsConduit(new Uri(tempFile.Value()))
                )
            ).Invoke();

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
