using Tonga.List;
using Tonga.Scalar;
using Tonga.Text;
using Xunit;
using Mapped = Tonga.List.Mapped;

namespace Tonga.Tests.List;

public sealed class MappedTest
{
    [Fact]
    public void TransformsList()
    {
        Assert.Equal(
            "HELLO",
            ItemAt._(
                Mapped._(
                    input => new Upper(AsText._(input)),
                    new AsList<string>("hello", "world", "damn")
                ),
                0
            ).Value().Str()
        );
    }

    [Fact]
    public void TransformsEmptyList()
    {
        Assert.Equal(
            0,
            Length._(
                Mapped._(
                    input => new Upper(AsText._(input)),
                    new AsList<string>()
                )
            ).Value()
        );
    }
}
