

using System;
using Xunit;
using Tonga.Map;

namespace Yaapii.Lookup.Tests
{
    public sealed class VersionMapTests
    {
        [Fact]
        public void MatchesKeyLow()
        {
            Assert.True(
                VersionMap._(
                    true,
                    AsPair._(new Version(1, 0, 0, 0), "ainz"),
                    AsPair._(new Version(2, 0, 0, 0), "zway")
                )
                .Keys()
                .Contains(new Version(1, 0, 0, 0))
            );
        }

        [Fact]
        public void MatchesKeyBetween()
        {
            Assert.True(
                VersionMap._(true,
                    AsPair._(new Version(1, 0, 0, 0), "ainz"),
                    AsPair._(new Version(5, 0, 0, 0), "zway")
                )
                .Keys()
                .Contains(new Version(2, 0, 0, 0))
            );
        }

        [Fact]
        public void MatchesOpenEnd()
        {
            Assert.True(
                VersionMap._(true,
                    AsPair._(new Version(1, 0, 0, 0), "ainz"),
                    AsPair._(new Version(5, 0, 0, 0), "zway")
                ).Keys()
                .Contains(new Version(10, 0, 0, 0))
            );
        }

        [Fact]
        public void RejectsOverClosedEnd()
        {
            Assert.False(
                VersionMap._(false,
                    AsPair._(new Version(1, 0, 0, 0), "ainz"),
                    AsPair._(new Version(5, 0, 0, 0), "zway")
                ).Keys()
                .Contains(new Version(10, 0, 0, 0))
            );
        }

        [Fact]
        public void MatchesWithinClosedEnd()
        {
            Assert.True(
                VersionMap._(false,
                    AsPair._(new Version(1, 0, 0, 0), "ainz"),
                    AsPair._(new Version(5, 0, 0, 0), "zway")
                ).Keys()
                .Contains(new Version(2, 0, 0, 0))
            );
        }

        [Fact]
        public void MatchesEndingZero()
        {
            Assert.True(
                VersionMap._(false,
                    AsPair._(new Version(1, 0, 0, 0), "ainz"),
                    AsPair._(new Version(5, 0, 0, 0), "zway")
                ).Keys()
                .Contains(new Version(1, 0))
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
