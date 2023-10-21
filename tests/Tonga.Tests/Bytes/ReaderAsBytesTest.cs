

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
            Assert.Equal(
                "hello, друг!",
                AsText._(
                    new ReaderAsBytes(
                        new StreamReader(
                            new InputOf("hello, друг!").Stream())
                    )
                ).AsString()
            );
        }
    }
}
