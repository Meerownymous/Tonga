using System;
using Tonga.Enumerable;
using Xunit;

namespace Tonga.Tests.Enumerable
{
    public sealed class SortedByTests
    {
        [Fact]
        public void SortsAnArrayByTextNumber()
        {
            Assert.Equal(
                ["nr-6", "nr0", "nr2", "nr3", "nr10", "nr44"],
                ("nr3", "nr2", "nr10", "nr44", "nr-6", "nr0")
                    .AsEnumerable()
                    .AsSortedBy(s => Convert.ToInt32(s.Substring(2)))
            );
        }

        [Fact]
        public void SortsAnArrayWithComparator()
        {
            Assert.Equal(
                "hello9, Friend7, dude6, c3, a2",
                new global::Tonga.Text.Joined(", ",
                    new SortedBy<string, int>(
                        s => Convert.ToInt32(s.Substring(s.Length-1)),
                        AsReverseCompare<int>.Default,
                        ("a2", "c3", "hello9", "dude6", "Friend7").AsEnumerable()
                    )
                ).Str());
        }

        [Fact]
        public void SortsAnEmptyArray()
        {
            Assert.Empty(
                new None<int>()
                    .AsSortedBy(i => i)
            );
        }
    }
}
