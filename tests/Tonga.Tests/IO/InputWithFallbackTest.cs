

using System;
using System.IO;
using Xunit;
using Tonga.Text;

namespace Tonga.IO.Tests
{
    public sealed class InputWithFallbackTest
    {
        [Fact]
        public void ReadsAlternativeInput()
        {
            Assert.True(
                new LiveText(
                    new InputWithFallback(
                        new InputOf(
                            new Uri(Path.GetFullPath("/this-file-is-absent-for-sure.txt"))
                        ),
                        new InputOf("hello, world!")
                    )
                ).AsString().EndsWith("world!"),
                "Can't read alternative source"
            );
        }

    }
}
