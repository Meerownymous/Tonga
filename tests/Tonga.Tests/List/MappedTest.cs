

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
                new ItemAt<IText>(
                    new Mapped<String, IText>(
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
                new LengthOf(
                    new Mapped<String, IText>(
                        input => new Upper(AsText._(input)),
                        new AsList<string>()
                    )
                ).Value()
            );
        }
    }
}
