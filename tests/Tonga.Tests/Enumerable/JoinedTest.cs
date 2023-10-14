

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
                        new ManyOf<string>("hello", "world", "друг"),
                        new ManyOf<string>("how", "are", "you"),
                        new ManyOf<string>("what's", "up")
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
                           str => new ManyOf<string>(str),
                           new ManyOf<string>("x")
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
                        new ManyOf<string>("hello", "world", "друг"),
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
