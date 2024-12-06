

using System;
using System.Globalization;
using Xunit;

namespace Tonga.Enumerable.Test
{
    public class SiblingTest
    {
        [Fact]
        public void Next()
        {
            Assert.Equal(
                3,
                new Sibling<int>(
                    2,
                    AsEnumerable._(1, 2, 3)
                ).Value()
            );
        }

        [Fact]
        public void ByPos()
        {
            Assert.Equal(
                1,
                new Sibling<int>(
                    2,
                    AsEnumerable._(1, 2, 3),
                    -1
                ).Value()
            );
        }

        [Fact]
        public void InvalidPositionResultsInFallback()
        {
            Assert.Equal(
                "15",
                new Sibling<string>(
                    "1",
                    AsEnumerable._("1", "2", "3"),
                    -1,
                    "15"
                ).Value()
            );
        }

        [Fact]
        public void FailForEmptyCollection()
        {
            Assert.Throws<ArgumentException>(
                () =>
                    new Sibling<int>(
                        1337,
                        new None<int>()
                    ).Value()
            );
        }

        [Fact]
        public void FallbackTest()
        {
            String fallback = "fallback";
            Assert.True(
                new Sibling<string>(
                    "Not-there",
                    new None<string>(),
                    12,
                    fallback
                ).Value() == fallback,
            "Can't fallback to default value");
        }

        [Fact]
        public void WithCustomComparable()
        {
            var format = "dd.MM.yyyy";
            var provider = CultureInfo.InvariantCulture;
            var nb1 = new FakeSibling(DateTime.ParseExact("11.10.2017", format, provider));
            var nb2 = new FakeSibling(DateTime.ParseExact("10.10.2017", format, provider));

            Assert.True(
                new Sibling<FakeSibling>(
                    nb1,
                    AsEnumerable._(nb1, nb2),
                    -1,
                    nb2
                ).Value().TimeStamp() == nb2.TimeStamp(),
            "Can't take the item by position from the enumerable");
        }

        internal class FakeSibling : IComparable<FakeSibling>
        {
            private readonly DateTime _stmp;

            public FakeSibling(DateTime stmp)
            {
                _stmp = stmp;
            }

            public DateTime TimeStamp() { return _stmp; }

            public int CompareTo(FakeSibling obj)
            {
                return _stmp.CompareTo(obj.TimeStamp());
            }
        }
    }
}
