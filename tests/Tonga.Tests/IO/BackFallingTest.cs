using System;
using System.IO;
using Tonga.IO;
using Tonga.Text;
using Xunit;

namespace Tonga.Tests.IO
{
    public sealed class BackFallingTest
    {
        [Fact]
        public void ReadsAlternativeInput()
        {
            Assert.EndsWith(
                "world!",
                new BackFalling(
                    new AsConduit(
                        new Func<FileInfo>(() => throw new Exception())
                    ),
                    new AsConduit("hello, world!")
                ).AsText()
                .Str()
            );
        }

    }
}
