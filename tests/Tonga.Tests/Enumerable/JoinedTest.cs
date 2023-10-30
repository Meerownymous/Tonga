

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
            Assert.Equal(
                8,
                LengthOf._(
                    Joined._(
                        AsEnumerable._("hello", "world", "друг"),
                        AsEnumerable._("how", "are", "you"),
                        AsEnumerable._("what's", "up")
                    )
                ).Value()
            );
        }

        [Fact]
        public void JoinsEnumerables()
        {
            Assert.Equal(
                1,
                LengthOf._(
                    Joined._(
                        Mapped._(
                           str => AsEnumerable._(str),
                           AsEnumerable._("x")
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
                LengthOf._(
                    Joined._(
                        AsEnumerable._("hello", "world", "друг"),
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
