using System;
using Tonga.Bytes;
using Tonga.IO;
using Tonga.Text;
using Xunit;
using Base64Decoded = Tonga.Bytes.Base64Decoded;

namespace Tonga.Tests.Text
{
    public sealed class Base64DecodedTextTest
    {
        [Theory]
        [InlineData("A fancy text")]
        [InlineData("A fancy text with \n line break")]
        [InlineData("A fancy text with € special character")]
        public void DecodesFromFile(string str)
        {
            using var tempFile = new TempFile("test.txt");
            new FullRead(
                new TeeOnRead(
                    new Tonga.Text.Base64Encoded(str),
                    new ConduitMorph(new Uri(tempFile.Value()))
                )
            ).Trigger();

            AssertText.Equal(
                new Base64Decoded(new Uri(tempFile.Value())),
                str
            );
        }
    }
}
