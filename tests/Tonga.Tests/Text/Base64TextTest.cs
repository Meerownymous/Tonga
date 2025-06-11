using System;
using Tonga.Bytes;
using Tonga.IO;
using Tonga.Text;
using Xunit;

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
                    str.AsText()
                        .AsBytes()
                        .AsBase64Encoded()
                        .AsText()
                        .Str(),
                    new AsConduit(new Uri(tempFile.Value()))
                )
            ).Yield();

            Assert.True(
                new Comparable(
                    new Uri(tempFile.Value()).AsText()
                        .AsBase64Decoded()
                ).CompareTo(
                    str.AsText()
                ) == 0
            );
        }
    }
}
