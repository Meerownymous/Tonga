

using System;
using System.Collections.Generic;
using Xunit;
using Tonga.Enumerable;
using Tonga.Text;

namespace Tonga.Collection.Tests
{
    public sealed class MappedTest
    {

        [Fact]
        public void BehavesAsCollection()
        {
            Assert.Contains(
                0,
                new Mapped<int, int>(
                    i => i + 1,
                    EnumerableOf.Pipe(-1, 1, 2)
                ));
        }

        [Fact]
        public void TransformsList()
        {
            Assert.Contains(
                "HELLO",
                new Mapped<string, string>(
                    input =>
                    input.ToUpper(),
                    EnumerableOf.Pipe("hello", "world", "друг")
                )
            );
        }

        [Fact]
        public void TransformsEmptyList()
        {
            Assert.Empty(
                new Mapped<String, IText>(
                    input => new Upper(new LiveText(input)),
                    new List<string>()
                )
            );
        }

    }
}
