

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
                new VersionMap(true,
                    new AsPair<Version, string>(new Version(1, 0, 0, 0), "ainz"),
                    new AsPair<Version, string>(new Version(2, 0, 0, 0), "zway")
                ).ContainsKey(new Version(1, 0, 0, 0))
            );
        }

        [Fact]
        public void MatchesKeyBetween()
        {
            Assert.True(
                new VersionMap(true,
                    new AsPair<Version, string>(new Version(1, 0, 0, 0), "ainz"),
                    new AsPair<Version, string>(new Version(5, 0, 0, 0), "zway")
                ).ContainsKey(new Version(2, 0, 0, 0))
            );
        }

        [Fact]
        public void MatchesOpenEnd()
        {
            Assert.True(
                new VersionMap(true,
                    new AsPair<Version, string>(new Version(1, 0, 0, 0), "ainz"),
                    new AsPair<Version, string>(new Version(5, 0, 0, 0), "zway")
                ).ContainsKey(new Version(10, 0, 0, 0))
            );
        }

        [Fact]
        public void RejectsOverClosedEnd()
        {
            Assert.False(
                new VersionMap(false,
                    new AsPair<Version, string>(new Version(1, 0, 0, 0), "ainz"),
                    new AsPair<Version, string>(new Version(5, 0, 0, 0), "zway")
                ).ContainsKey(new Version(10, 0, 0, 0))
            );
        }

        [Fact]
        public void MatchesWithinClosedEnd()
        {
            Assert.True(
                new VersionMap(false,
                    new AsPair<Version, string>(new Version(1, 0, 0, 0), "ainz"),
                    new AsPair<Version, string>(new Version(5, 0, 0, 0), "zway")
                ).ContainsKey(new Version(2, 0, 0, 0))
            );
        }

        [Fact]
        public void MatchesEndingZero()
        {
            Assert.True(
                new VersionMap(false,
                    new AsPair<Version, string>(new Version(1, 0, 0, 0), "ainz"),
                    new AsPair<Version, string>(new Version(5, 0, 0, 0), "zway")
                ).ContainsKey(new Version(1, 0))
            );
        }

        [Fact]
        public void MatchesMajorMinorVersion()
        {
            Assert.Equal(
                "zway",
                new VersionMap(true,
                    new AsPair<Version, string>(new Version(1, 0, 0, 0), "ainz"),
                    new AsPair<Version, string>(new Version(5, 0, 0, 0), "zway")
                )[new Version(5, 0)]
            );
        }
    }
}
