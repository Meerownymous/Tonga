using Tonga.Enumerable;
using Xunit;

namespace Tonga.Tests.Enumerable
{
    public sealed class SortedTest
    {
        [Fact]
        public void SortsAnArray()
        {
            Assert.True(
                new global::Tonga.Text.Joined(", ",
                    new Mapped<int, string>(
                        i => i.ToString(),
                        new Sorted<int>(
                            AsEnumerable._(3, 2, 10, 44, -6, 0)
                        )
                    )
                ).Str() == "-6, 0, 2, 3, 10, 44",
            "Can't sort an enumerable");
        }

        [Fact]
        public void SortsAnArrayWithComparator()
        {
            Assert.True(
                new global::Tonga.Text.Joined(", ",
                    new Sorted<string>(
                        AsReverseCompare<string>.Default,
                        AsEnumerable._(
                            "a", "c", "hello", "dude", "Friend"
                        )
                    )
                ).Str() == "hello, Friend, dude, c, a",
                "Can't sort an enumerable with a custom comparator");
        }

        [Fact]
        public void SortsAnEmptyArray()
        {
            Assert.Empty(
                new Sorted<string>(
                    None._<string>()
                )
            );
        }
    }
}
