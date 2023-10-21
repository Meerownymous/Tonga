

using System.Collections.Generic;
using Xunit;
using Tonga.Text;
using Tonga.Scalar;

namespace Tonga.Enumerable.Test
{
    public sealed class AsEnumerableTest
    {
        [Fact]
        public void ConvertsScalarsToEnumerable()
        {
            Assert.True(
                new LengthOf(
                    AsEnumerable._(
                        "a", "b", "c"
                    )
                ).Value() == 3,
                "Can't convert scalars to iterable");
        }

        [Fact]
        public void IsSticky()
        {
            var lst = new List<string>();
            var length =
                new LengthOf(
                    AsEnumerable._(() =>
                    {
                        lst.Add("something");
                        return lst;
                    })
                );

            var a = length.Value();
            var b = length.Value();
            Assert.Equal(a, b);
        }

        [Fact]
        public void ConvertsScalarsToEnumerableTyped()
        {
            Assert.True(
                new LengthOf(
                    AsEnumerable._(
                        "a", "b", "c"
                    )
                ).Value() == 3,
                "Can't convert scalars to iterable");
        }

        [Fact]
        public void ConvertsObjectsToEnumerableTyped()
        {
            Assert.True(
                new LengthOf(
                    AsEnumerable._(
                        AsText._("a"), AsText._("b"), AsText._("c")
                    )
                ).Value() == 3,
            "Can't convert objects to enumerable");
        }

        [Fact]
        public void SensesChanges()
        {
            var lst = new List<string>();
            var length =
                new LengthOf(
                    AsEnumerable._(() =>
                    {
                        lst.Add("something");
                        return lst.GetEnumerator();
                    })
                );

            var a = length.Value();
            var b = length.Value();
            Assert.NotEqual(a, b);
        }

        [Fact]
        public void NonGenericEnumerates()
        {
            Assert.Equal(
                new List<string>() { "one", "two", "eight" },
                AsEnumerable._("one", "two", "eight")
            );
        }

        [Fact]
        public void CanBeEmpty()
        {
            Assert.False(
                new None().GetEnumerator().MoveNext()
            );
        }
    }
}
