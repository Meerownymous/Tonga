

using Xunit;
using Tonga.Misc;
using Tonga.Text;

namespace Tonga.Enumerable.Test
{
    public sealed class SortedByTests
    {
        [Fact]
        public void SortsAnArrayByTextNumber()
        {
            Assert.Equal(
                "nr-6, nr0, nr2, nr3, nr10, nr44",
                new Text.Joined(", ",
                    new SortedBy<string, int>(
                        s => new IntOf(s.Substring(2)).Value(),
                        Params.Of("nr3", "nr2", "nr10", "nr44", "nr-6", "nr0")
                    )
                ).AsString()
            );
        }

        [Fact]
        public void SortsAnArrayWithComparator()
        {
            Assert.True(
                new Text.Joined(", ",
                    new SortedBy<string, int>(
                        s => new IntOf(s.Substring(s.Length-1)).Value(),
                        IReverseCompare<int>.Default,
                        Params.Of(
                            "a2", "c3", "hello9", "dude6", "Friend7"
                        )
                    )
                ).AsString() == "hello9, Friend7, dude6, c3, a2",
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
