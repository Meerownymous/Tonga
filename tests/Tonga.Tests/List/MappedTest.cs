

using System;
using Xunit;
using Tonga.Enumerable;
using Tonga.Text;
using Tonga.Scalar;

namespace Tonga.List.Tests
{
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
                ).Value().AsString()
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
}
