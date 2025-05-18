using System;
using System.Collections.Generic;
using Tonga.Enumerable;
using Tonga.Text;
using Xunit;
using Mapped = Tonga.Collection.Mapped;

namespace Tonga.Tests.Collection
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
                new Tonga.Collection.Mapped<string, string>(
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
                new Tonga.Collection.Mapped<String, IText>(
                    input => new Upper(AsText._(input)),
                    new List<string>()
                )
            );
        }

    }
}
