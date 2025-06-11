using Tonga.Enumerable;
using Xunit;

namespace Tonga.Tests.Enumerable
{
    public sealed class SortedTest
    {
        [Fact]
        public void SortsAnArray()
        {
            Assert.Equal(
                "-6, 0, 2, 3, 10, 44",
                new global::Tonga.Text.Joined(", ",
                            (3, 2, 10, 44, -6, 0).AsEnumerable().AsSorted().AsMapped(i => i.ToString())
                ).Str()
            );
        }

        [Fact]
        public void SortsAnArrayWithComparator()
        {
            Assert.Equal(
                "hello, Friend, dude, c, a",
                new global::Tonga.Text.Joined(", ",
                        ("a", "c", "hello", "dude", "Friend").AsEnumerable().AsSorted(AsReverseCompare<string>.Default)
                ).Str()
            );
        }

        [Fact]
        public void SortsAnEmptyArray()
        {
            Assert.Empty(
                new None<string>().AsSorted()
            );
        }
    }
}
