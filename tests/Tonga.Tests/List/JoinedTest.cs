

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
                new Scalar.LengthOf(
                    new Joined<string>(
                        AsList._("hello", "world", "друг"),
                        new AsList<string>("how", "are", "you"),
                        new AsList<string>("what's", "up")
                    )
                ).Value()
            );
        }

        [Fact]
        public void JoinsEnumerables()
        {
            Assert.Equal(
                1,
                new Scalar.LengthOf(
                    new Joined<string>(
                        new Mapped<string, IList<string>>(
                           str => new AsList<string>(str),
                           new AsList<string>("x")
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
                new Scalar.LengthOf(
                    new Joined<string>(
                        new AsList<string>("hello", "world", "друг"),
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
