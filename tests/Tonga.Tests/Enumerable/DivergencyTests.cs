

using System.Collections.Generic;
using Xunit;

namespace Tonga.Enumerable.Test
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
        [InlineData(new string[] { "a", "b", "c" }, new string[] { "a", "b", "e" }, new string[] { "c", "e" })]
        [InlineData(new string[] { "a", "b" }, new string[] { "c", "d" }, new string[] { "a", "b", "c", "d" })]
        public void MatchesString(IEnumerable<string> a, IEnumerable<string> b, IEnumerable<string> expected)
        {
            Assert.Equal(
                expected,
                Divergency._(
                   a, b
                )
            );
        }

        [Theory]
        [InlineData(new int[] { 5, 6 }, new int[] { 1, 2 }, new int[] { 5, 6, 1, 2 })]
        [InlineData(new int[] { 1, 2 }, new int[] { 1 }, new int[] { 2 })]
        public void MatchesInt(IEnumerable<int> a, IEnumerable<int> b, IEnumerable<int> expected)
        {
            Assert.Equal(
                expected,
                Divergency._(a, b)
            );
        }
    }
}
