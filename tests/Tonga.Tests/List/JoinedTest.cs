using Tonga.List;
using Tonga.Scalar;
using Xunit;

namespace Tonga.Tests.List;

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
