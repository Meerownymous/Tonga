

using System.Threading;
using Xunit;
using Tonga.Enumerable;
using Tonga.Scalar;

namespace Tonga.List.Tests
{
    public sealed class ListOfTest
    {
        [Fact]
        public void SensesChangesInList()
        {
            int size = 2;
            var list =
                ListOf.Pipe(
                    Head.Pipe(
                        Endless.New(1),
                        () => Interlocked.Increment(ref size)
                    )
                );

            Assert.NotEqual(
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
            var list =
                ListOf.Pipe(
                    Lambda.Pipe(() => advances++,
                        EnumerableOf.Pipe("item1", "item2", "item3")
                    )
                );

            var count = list.Count;

            Assert.Equal(3, advances);

        }

        [Fact]
        public void FindsIndexOf()
        {
            Assert.Equal(
                2,
                ListOf.Pipe("item1", "item2", "item3")
                    .IndexOf("item3")
            );
        }

        [Fact]
        public void DeliversIndexWhenNoFinding()
        {
            Assert.Equal(
                -1,
                ListOf.Pipe("item1", "item2", "item3")
                    .IndexOf("item100")
            );
        }

        [Fact]
        public void CanCopyTo()
        {
            var array = new string[5];
            var origin = ListOf.Pipe("item1", "item2", "item3");
            origin.CopyTo(array, 2);

            Assert.Equal(
                new string[] { null, null, "item1", "item2", "item3" },
                array
            );
        }

        [Fact]
        public void ContainsWorksWithEmptyList()
        {
            Assert.DoesNotContain(
                "item",
                ListOf.Pipe()
            );
        }
    }
}