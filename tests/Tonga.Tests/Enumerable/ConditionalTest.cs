using Tonga.Enumerable;
using Xunit;

namespace Tonga.Tests.Enumerable
{
    public sealed class ConditionalTest
    {
        [Fact]
        public void EnumeratesLeftWhenMatching()
        {
            Assert.Equal(
                "1 a",
                string.Join(
                    " ",
                    new Conditional<string>(
                        new AsEnumerable<string>("1", "a"),
                        new AsEnumerable<string>("2", "b"),
                        () => true
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
                    new Conditional<string>(
                        new AsEnumerable<string>("1", "a"),
                        new AsEnumerable<string>("2", "b"),
                        () => false
                    )
                )
            );
        }
    }
}

