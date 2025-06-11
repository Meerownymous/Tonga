using System;
using System.Globalization;
using Tonga.Enumerable;
using Xunit;

namespace Tonga.Tests.Enumerable
{
    public class SiblingTest
    {
        [Fact]
        public void FindsNext()
        {
            Assert.Equal(
                3,
                (1, 2, 3)
                    .AsEnumerable()
                    .Sibling(2)
                    .Value()
            );
        }

        [Fact]
        public void ByPos()
        {
            Assert.Equal(
                1,
                (1, 2, 3)
                    .AsEnumerable()
                    .Sibling(-1)
                    .Value()
            );
        }

        [Fact]
        public void InvalidPositionResultsInFallback()
        {
            Assert.Equal(
                "15",
                ("1", "2", "3")
                    .AsEnumerable()
                    .Sibling("1", -1, "15")
                    .Value()
            );
        }

        [Fact]
        public void FailForEmptyCollection()
        {
            Assert.Throws<ArgumentException>(
                () => new None<int>()
                    .Sibling(1337)
                    .Value()
            );
        }

        [Fact]
        public void FallbackTest()
        {
            String fallback = "fallback";
            Assert.Equal(
                fallback,
                new None<string>()
                    .Sibling("Not-there", 12, fallback)
                    .Value()
            );
        }

        [Fact]
        public void WithCustomComparable()
        {
            var format = "dd.MM.yyyy";
            var provider = CultureInfo.InvariantCulture;
            var nb1 = new FakeSibling(DateTime.ParseExact("11.10.2017", format, provider));
            var nb2 = new FakeSibling(DateTime.ParseExact("10.10.2017", format, provider));

            Assert.Equal(
                nb2.TimeStamp(),
                (nb1, nb2)
                    .AsEnumerable()
                    .Sibling(nb1, -1, nb2)
                    .Value()
                    .TimeStamp()
            );
        }

        internal class FakeSibling(DateTime stmp) : IComparable<FakeSibling>
        {
            public DateTime TimeStamp() =>stmp;
            public int CompareTo(FakeSibling obj) => stmp.CompareTo(obj.TimeStamp());
        }
    }
}
