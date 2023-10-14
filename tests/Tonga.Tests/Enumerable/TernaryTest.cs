using System;
using Xunit;
using Tonga.Enumerable;

namespace Tonga.Enumerable.Test
{
    public sealed class TernaryTest
    {
        [Fact]
        public void EnumeratesLeftWhenMatching()
        {
            Assert.Equal(
                "1 a",
                string.Join(" ",
                    new Ternary<string>(
                        Params.Of("1", "a"),
                        Params.Of("2", "b"),
                        ()=> true
                    )
                )
            );
        }

        [Fact]
        public void EnumeratesRightWhenNotMatching()
        {
            Assert.Equal(
                "2 b",
                string.Join(" ",
                    new Ternary<string>(
                        Params.Of("1", "a"),
                        Params.Of("2", "b"),
                        () => false
                    )
                )
            );
        }
    }
}

