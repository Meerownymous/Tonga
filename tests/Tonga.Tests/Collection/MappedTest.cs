

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
            Mapped._(item => item, new List<string>()).GetEnumerator();
            //Assert.Contains(
            //    0,
            //    Mapped._(
            //        i => i + 1,
            //        AsEnumerable._(-1, 1, 2)
            //    )
            //);
        }

        [Fact]
        public void TransformsList()
        {
            Assert.Contains(
                "HELLO",
                new Mapped<string, string>(
                    input =>
                    input.ToUpper(),
                    AsEnumerable._("hello", "world", "друг")
                )
            );
        }

        [Fact]
        public void TransformsEmptyList()
        {
            Assert.Empty(
                new Mapped<String, IText>(
                    input => new Upper(AsText._(input)),
                    new List<string>()
                )
            );
        }

    }
}
