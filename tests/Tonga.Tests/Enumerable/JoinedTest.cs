

using System.Collections.Generic;
using Tonga.Scalar;
using Xunit;

namespace Tonga.Enumerable.Test
{
    public sealed class JoinedTest
    {
        [Fact]
        public void TransformsList()
        {
            Assert.True(
                new LengthOf(
                    new Joined<string>(
                        EnumerableOf.Pipe("hello", "world", "друг"),
                        EnumerableOf.Pipe("how", "are", "you"),
                        EnumerableOf.Pipe("what's", "up")
                    )
                ).Value() == 8,
            "Can't concatenate enumerables together");
        }

        [Fact]
        public void JoinsEnumerables()
        {
            Assert.True(
                new LengthOf(
                    new Joined<IEnumerable<string>>(
                        new Mapped<string, IEnumerable<string>>(
                           str => EnumerableOf.Pipe(str),
                           EnumerableOf.Pipe("x")
                        )
                )).Value() == 1,
            "cannot join mapped iterables together");
        }

        [Fact]
        public void JoinsSingleElemtns()
        {
            Assert.True(
                new LengthOf(
                    new Joined<string>(
                        EnumerableOf.Pipe("hello", "world", "друг"),
                        "how",
                        "are",
                        "you",
                        "what's",
                        "up"
                    )
                ).Value() == 8,
            "Can't concatenate enumerable with ingle values");
        }
    }
}
