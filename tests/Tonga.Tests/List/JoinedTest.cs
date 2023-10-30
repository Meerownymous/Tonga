

using System.Collections.Generic;
using Tonga.Scalar;
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
                Length._(
                    Joined._(
                        AsList._("hello", "world", "друг"),
                        AsList._("how", "are", "you"),
                        AsList._("what's", "up")
                    )
                ).Value()
            );
        }

        [Fact]
        public void JoinsEnumerables()
        {
            Assert.Equal(
                1,
                Length._(
                    Joined._(
                        Mapped._(
                           str => AsList._(str),
                           AsList._("x")
                        )
                    )
                ).Value()
            );
        }
    }
}
