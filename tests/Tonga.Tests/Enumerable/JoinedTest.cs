

using System.Collections.Generic;
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
                        Params.Of("hello", "world", "друг"),
                        Params.Of("how", "are", "you"),
                        Params.Of("what's", "up")
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
                           str => Params.Of(str),
                           Params.Of("x")
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
                        Params.Of("hello", "world", "друг"),
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
