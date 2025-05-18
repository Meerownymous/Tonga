using System;
using System.IO;
using Tonga.IO;
using Tonga.Text;
using Xunit;

namespace Tonga.Tests.IO
{
    public sealed class InputWithFallbackTest
    {
        [Fact]
        public void ReadsAlternativeInput()
        {
            Assert.True(
                AsText._(
                    new InputWithFallback(
                        new Tonga.IO.AsInput(
                            new Uri(Path.GetFullPath("/this-file-is-absent-for-sure.txt"))
                        ),
                        new Tonga.IO.AsInput("hello, world!")
                    )
                ).AsString().EndsWith("world!"),
                "Can't read alternative source"
            );
        }

    }
}
