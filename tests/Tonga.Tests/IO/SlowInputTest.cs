

using System;
using System.Text;
using Xunit;
using Tonga.Text;

namespace Tonga.IO.Tests
{
    public sealed class SlowInputTest
    {
        [Fact]
        public void CalculatesLength()
        {
            String text = "What's up, друг?";
            Assert.Equal(
                new LengthOf(
                    new SlowInput(
                        new InputOf(
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
                new LengthOf(
                    new SlowInput(size)
                ).Value()
            );
        }

    }

}
