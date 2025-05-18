using System;
using System.IO;
using Tonga.IO;
using Tonga.Text;
using Xunit;

namespace Tonga.Tests.IO
{
    public sealed class ConduitWithFallbackTest
    {
        [Fact]
        public void ReadsAlternativeInput()
        {
            Assert.True(
                AsText._(
                    new ConduitWithFallback(
                        new AsConduit(
                            () => throw new Exception()
                        ),
                        new AsConduit("hello, world!")
                    )
                ).AsString().EndsWith("world!"),
                "Can't read alternative source"
            );
        }

    }
}
