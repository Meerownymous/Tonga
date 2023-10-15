

using System.Threading;
using Xunit;
using Tonga.Enumerable;
using Tonga.Scalar;

namespace Tonga.List.Tests
{
    public sealed class ListOfTest
    {
        [Fact]
        public void IgnoresChangesInList()
        {
            int size = 2;
            var list =
                new ListOf<int>(
                    new Tonga.Enumerable.Head<int>(
                        new Tonga.Enumerable.Endless<int>(1),
                        () => Interlocked.Increment(ref size)
                ));

            Assert.Equal(
                new Scalar.LengthOf(list).Value(),
                new Scalar.LengthOf(list).Value()
            );
        }

        [Fact]
        public void ContainsWorksWithFirstItem()
        {
            var list = new ListOf<string>("item");
            Assert.Contains("item", list);
        }

        [Fact]
        public void ContainsWorksWithHigherItem()
        {
            var list = new ListOf<string>("item1", "item2", "item3");
            Assert.Contains("item2", list);
        }

        [Fact]
        public void CountingAdvancesAll()
        {
            var advances = 0;
            var origin = new ListOf<string>("item1", "item2", "item3");

            var list =
                new ListOf<string>(
                    new Enumerator.Sticky<string>(
                        new Enumerator.Sticky<string>.Cache<string>(() =>
                            new Logging<string>(
                                origin,
                                idx => advances++
                            ).GetEnumerator()
                        )
                    )
                );

            var count = list.Count;

            Assert.Equal(3, advances);

        }

        [Fact]
        public void FindsIndexOf()
        {
            var lst = new ListOf<string>("item1", "item2", "item3");

            Assert.Equal(
                2,
                lst.IndexOf("item3")
            );
        }

        [Fact]
        public void DeliversIndexWhenNoFinding()
        {
            var lst = new ListOf<string>("item1", "item2", "item3");

            Assert.Equal(
                -1,
                lst.IndexOf("item100")
            );
        }

        [Fact]
        public void CanCopyTo()
        {
            var array = new string[5];
            var origin = new ListOf<string>("item1", "item2", "item3");
            origin.CopyTo(array, 2);

            Assert.Equal(
                new string[] { null, null, "item1", "item2", "item3" },
                array
            );
        }

        [Fact]
        public void ContainsWorksWithEmptyList()
        {
            var list = new ListOf<string>();
            Assert.DoesNotContain("item", list);
        }
    }
}
