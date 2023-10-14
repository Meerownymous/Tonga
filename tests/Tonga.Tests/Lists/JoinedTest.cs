

using System.Collections.Generic;
using Xunit;

namespace Tonga.List.Tests
{
    public sealed class JoinedTest
    {
        [Fact]
        public void TransformsList()
        {
            Assert.Equal(
                8,
                new Enumerable.LengthOf(
                    new Joined<string>(
                        new ListOf<string>("hello", "world", "друг"),
                        new ListOf<string>("how", "are", "you"),
                        new ListOf<string>("what's", "up")
                    )
                ).Value()
            );
        }

        [Fact]
        public void JoinsEnumerables()
        {
            Assert.Equal(
                1,
                new Enumerable.LengthOf(
                    new Joined<string>(
                        new Mapped<string, IList<string>>(
                           str => new ListOf<string>(str),
                           new ListOf<string>("x")
                        )
                    )
                ).Value()
            );
        }

        [Fact]
        public void JoinsSingleElemtns()
        {
            Assert.Equal(
                8,
                new Enumerable.LengthOf(
                    new Joined<string>(
                        new ListOf<string>("hello", "world", "друг"),
                        "how",
                        "are",
                        "you",
                        "what's",
                        "up"
                    )
                ).Value()
            );
        }
    }
}
