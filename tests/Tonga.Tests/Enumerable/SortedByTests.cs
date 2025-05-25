using Tonga.Enumerable;
using Tonga.Primitives;
using Tonga.Text;
using Xunit;

namespace Tonga.Tests.Enumerable
{
    public sealed class SortedByTests
    {
        [Fact]
        public void SortsAnArrayByTextNumber()
        {
            Assert.Equal(
                "nr-6, nr0, nr2, nr3, nr10, nr44",
                new global::Tonga.Text.Joined(", ",
                    new SortedBy<string, int>(
                        s => new IntOf(s.Substring(2)).Value(),
                        AsEnumerable._("nr3", "nr2", "nr10", "nr44", "nr-6", "nr0")
                    )
                ).Str()
            );
        }

        [Fact]
        public void SortsAnArrayWithComparator()
        {
            Assert.True(
                new global::Tonga.Text.Joined(", ",
                    new SortedBy<string, int>(
                        s => new IntOf(s.Substring(s.Length-1)).Value(),
                        AsReverseCompare<int>.Default,
                        AsEnumerable._(
                            "a2", "c3", "hello9", "dude6", "Friend7"
                        )
                    )
                ).Str() == "hello9, Friend7, dude6, c3, a2",
                "Can't sort an enumerable with a custom comparator");
        }

        [Fact]
        public void SortsAnEmptyArray()
        {
            Assert.Empty(
                new SortedBy<int, int>(
                    i => i,
                    new None<int>()
                )
            );
        }
    }
}
