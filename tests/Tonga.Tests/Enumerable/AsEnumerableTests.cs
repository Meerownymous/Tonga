

using System.Collections.Generic;
using Xunit;
using Tonga.Text;
using Tonga.Scalar;
using Tonga.List;

namespace Tonga.Enumerable.Test
{
    public sealed class AsEnumerableTest
    {
        [Fact]
        public void ConvertsScalarsToEnumerable()
        {
            Assert.True(
                Length._(
                    AsEnumerable._(
                        "a", "b", "c"
                    )
                ).Value() == 3,
                "Can't convert scalars to iterable");
        }

        [Fact]
        public void ConvertsScalarsToEnumerableTyped()
        {
            Assert.Equal(
                3,
                Length._(
                    AsEnumerable._(
                        "a", "b", "c"
                    )
                ).Value()
            );
        }

        [Fact]
        public void ConvertsObjectsToEnumerableTyped()
        {
            Assert.Equal(
                3,
                Length._(
                    AsEnumerable._(
                        AsText._("a"), AsText._("b"), AsText._("c")
                    )
                ).Value()
            );
        }

        [Fact]
        public void SensesChanges()
        {
            var lst = new List<string>();
            var length =
                Length._(
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
        public void Enumerates()
        {
            Assert.Equal(
                AsList._("one", "two", "eight" ),
                AsEnumerable._("one", "two", "eight")
            );
        }

        [Fact]
        public void CanBeEmpty()
        {
            Assert.False(
                None._<string>().GetEnumerator().MoveNext()
            );
        }
    }
}
