

using System;
using System.Text;
using Xunit;
using Tonga.Text;
using Tonga.Scalar;
using Tonga.Tests.IO;

namespace Tonga.IO.Tests
{
    public sealed class SlowInputTest
    {
        [Fact]
        public void CalculatesLength()
        {
            String text = "What's up, друг?";
            Assert.Equal(
                Length._(
                    new SlowIConduit(
                        new AsConduit(
                            AsText._(text)
                        )
                    )
                ).Value(),
                Encoding.UTF8.GetBytes(text).Length
            );
        }

        [Fact]
        public void ReadsFileContentSlowly()
        {
            long size = 100_000L;
            Assert.Equal(
                size,
                Length._(
                    new SlowIConduit(size)
                ).Value()
            );
        }

    }

}
