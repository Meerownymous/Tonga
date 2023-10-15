

using Xunit;

namespace Tonga.Enumerable.Test
{
    public sealed class CycledTest
    {
        [Fact]
        public void RepeatsEnumerable()
        {
            Assert.Equal(
                "two",
                new ItemAt<string>(
                    new Cycled<string>(
                        EnumerableOf.Pipe("one", "two", "three")
                    ),
                    7
                ).Value()
            );
        }
    }
}
