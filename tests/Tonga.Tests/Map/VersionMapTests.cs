using System;
using Tonga.Map;
using Xunit;

namespace Tonga.Tests.Map
{
    public sealed class VersionMapTests
    {
        [Fact]
        public void MatchesLowerEnd()
        {
            Assert.Equal(
                "ainz",
                VersionMap._(
                    true,
                    AsPair._(new Version(1, 0, 0, 0), "ainz"),
                    AsPair._(new Version(2, 0, 0, 0), "zway")
                )[new Version(1, 0, 0, 0)]
            );
        }

        [Fact]
        public void MatchesKeyBetween()
        {
            Assert.Equal(
                "ainz",
                VersionMap._(true,
                    AsPair._(new Version(1, 0, 0, 0), "ainz"),
                    AsPair._(new Version(5, 0, 0, 0), "zway")
                )[new Version(2, 0, 0, 0)]
            );
        }

        [Fact]
        public void MatchesOpenEnd()
        {
            Assert.Equal(
                "zway",
                VersionMap._(true,
                    AsPair._(new Version(1, 0, 0, 0), "ainz"),
                    AsPair._(new Version(5, 0, 0, 0), "zway")
                )[new Version(10, 0, 0, 0)]
            );
        }

        [Fact]
        public void RejectsOverClosedEnd()
        {
            Assert.Throws<ArgumentException>(() =>
                VersionMap._(false,
                    AsPair._(new Version(1, 0, 0, 0), "ainz"),
                    AsPair._(new Version(5, 0, 0, 0), "zway")
                )[new Version(10, 0, 0, 0)]
            );
        }

        [Fact]
        public void MatchesWithinClosedEnd()
        {
            Assert.Equal(
                "ainz",
                VersionMap._(false,
                    AsPair._(new Version(1, 0, 0, 0), "ainz"),
                    AsPair._(new Version(5, 0, 0, 0), "zway")
                )[new Version(2, 0, 0, 0)]
            );
        }

        [Fact]
        public void MatchesEndingZero()
        {
            Assert.Equal(
                "ainz",
                VersionMap._(false,
                    AsPair._(new Version(1, 0, 0, 0), "ainz"),
                    AsPair._(new Version(5, 0, 0, 0), "zway")
                )[new Version(1, 0)]
            );
        }

        [Fact]
        public void MatchesMajorMinorVersion()
        {
            Assert.Equal(
                "zway",
                VersionMap._(true,
                    AsPair._(new Version(1, 0, 0, 0), "ainz"),
                    AsPair._(new Version(5, 0, 0, 0), "zway")
                )[new Version(5, 0)]
            );
        }
    }
}
