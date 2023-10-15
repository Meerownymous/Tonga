

using System.Collections.Generic;
using Xunit;
using Tonga.Text;
using Tonga.Scalar;

namespace Tonga.Enumerable.Test
{
    public sealed class ManyLiveTest
    {
        [Fact]
        public void ConvertsScalarsToEnumerable()
        {
            Assert.True(
                new LengthOf(
                    EnumerableOf.Pipe(
                        "a", "b", "c"
                    )
                ).Value() == 3,
                "Can't convert scalars to iterable");
        }

        [Fact]
        public void ConvertsObjectsToEnumerable()
        {
            Assert.True(
                new LengthOf(
                    EnumerableOf.Pipe(
                        new LiveText("a"),
                        new LiveText("b"),
                        new LiveText("c")
                    )
                ).Value() == 3
            );
        }

        [Fact]
        public void SensesChanges()
        {
            var lst = new List<string>();
            var live =
                new EnumerableOf<string>(() =>
                {
                    lst.Add("something");
                    return lst.GetEnumerator();
                });
            Assert.NotEqual(new LengthOf(live).Value(), new LengthOf(live).Value());
        }
    }

}
