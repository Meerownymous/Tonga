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
            AssertText.EndsWith(
                "world!",
                new BackFalling(
                    new Func<FileInfo>(() => throw new Exception()),
                    "hello, world!"
                )
            );
        }

    }
}
