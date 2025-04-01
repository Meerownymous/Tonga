using System.Collections.Generic;
using Tonga.Enumerable;
using Xunit;

namespace Tonga.Tests.Enumerable
{
    public sealed class DivergencyTests
    {
        [Fact]
        public void Empty()
        {
            Assert.Empty(
                Divergency._(
                    AsEnumerable._("a", "b"),
                    AsEnumerable._("a", "b")
                )
            );
        }

        [Theory]
        [InlineData(new[] { "a", "b", "c" }, new[] { "a", "b", "e" }, new[] { "c", "e" })]
        [InlineData(new[] { "a", "b" }, new[] { "c", "d" }, new[] { "a", "b", "c", "d" })]
        public void MatchesString(IEnumerable<string> a, IEnumerable<string> b, IEnumerable<string> expected)
        {
            Assert.Equal(
                expected,
                Divergency._(
                   b, a
                )
            );
        }

        [Theory]
        [InlineData(new[] { 5, 6 }, new[] { 1, 2 }, new[] { 1, 2, 5, 6 })]
        [InlineData(new[] { 1, 2 }, new[] { 1 }, new[] { 2 })]
        public void MatchesInt(IEnumerable<int> a, IEnumerable<int> b, IEnumerable<int> expected)
        {
            Assert.Equal(
                expected,
                Divergency._(a, b)
            );
        }
    }
}
