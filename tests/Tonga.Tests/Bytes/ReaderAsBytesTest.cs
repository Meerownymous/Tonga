

using System;
using System.IO;
using Xunit;
using Tonga.IO;
using Tonga.Text;

namespace Tonga.Bytes.Tests
{
    public sealed class ReaderAsBytesTest
    {
        [Fact]
        public void ReadsString()
        {
            String source = "hello, друг!";
            Assert.True(
            new LiveText(
                new ReaderAsBytes(
                    new StreamReader(
                        new InputOf(source).Stream())
                )
            ).AsString() == source);
        }
    }
}
