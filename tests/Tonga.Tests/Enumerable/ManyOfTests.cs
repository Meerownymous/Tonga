

using System.Collections.Generic;
using Xunit;
using Tonga.Text;

namespace Tonga.Enumerable.Test
{
    public sealed class ManyOfTest
    {
        [Fact]
        public void ConvertsScalarsToEnumerable()
        {
            Assert.True(
                new LengthOf(
                    new ManyOf(
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
                    new ManyOf(() =>
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
                    new ManyOf<string>(
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
                    new ManyOf<IText>(
                        new LiveText("a"), new LiveText("b"), new LiveText("c")
                    )
                ).Value() == 3,
            "Can't convert objects to enumerable");
        }

        [Fact]
        public void IsStickyTyped()
        {
            var lst = new List<string>();
            var length =
                new LengthOf(
                    new ManyOf<string>(() =>
                    {
                        lst.Add("something");
                        return lst.GetEnumerator();
                    })
                );

            var a = length.Value();
            var b = length.Value();
            Assert.Equal(a, b);
        }

        [Fact]
        public void NonGenericEnumerates()
        {
            Assert.Equal(
                new List<string>() { "one", "two", "eight" },
                new ManyOf("one", "two", "eight")
            );
        }

        [Fact]
        public void CanBeEmpty()
        {
            Assert.False(
                new ManyOf().GetEnumerator().MoveNext()
            );
        }
    }
}
