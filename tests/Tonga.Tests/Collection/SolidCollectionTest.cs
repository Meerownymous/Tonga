

using Xunit;
using Tonga.Enumerable;

namespace Tonga.Collection.Tests
{
    public sealed class SolidCollectionTest
    {

        [Fact]
        public void BehavesAsCollection()
        {
            Assert.Contains(
                -1,
                new Solid<int>(1, 2, 0, -1)
            );
        }

        [Fact]
        public void MakesListFromMappedIterable()
        {
            var list =
                new Solid<int>(
                    new Mapped<int, int>(
                        i => i + 1,
                        Params.Of(1, -1, 0, 1)
                    )
                );

            Assert.True(list.Count == 4, "Can't turn a mapped iterable into a list");
            Assert.True(list.Count == 4, "Can't turn a mapped iterable into a list, again");
        }

    }
}
