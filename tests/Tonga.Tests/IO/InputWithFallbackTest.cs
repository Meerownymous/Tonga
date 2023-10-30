

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
                AsText._(
                    new InputWithFallback(
                        new AsInput(
                            new Uri(Path.GetFullPath("/this-file-is-absent-for-sure.txt"))
                        ),
                        new AsInput("hello, world!")
                    )
                ).AsString().EndsWith("world!"),
                "Can't read alternative source"
            );
        }

    }
}
