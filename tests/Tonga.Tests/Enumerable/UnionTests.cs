

using System.Collections.Generic;
using System.IO;
using Xunit;
using Tonga.List;
using Tonga.Scalar;

namespace Tonga.Enumerable.Test
{
    public sealed class UnionTests
    {
        [Fact]
        public void Empty()
        {
            Assert.Empty(
                new Union<string>(
                    AsEnumerable._("a", "b"),
                    AsEnumerable._("c")
                )
            );
        }

        [Theory]
        [InlineData(new string[] { "a", "b" }, new string[] { "a", "b" }, new string[] { "a", "b" })]
        [InlineData(new string[] { "a", "b" }, new string[] { "a" }, new string[] { "a" })]
        public void MatchesString(IEnumerable<string> a, IEnumerable<string> b, IEnumerable<string> expected)
        {
            Assert.Equal(
                expected,
                new Union<string>(
                   a, b
                )
            );
        }

        [Theory]
        [InlineData(new[] { 1, 1, 2 }, new[] { 1, 2, 2 }, new[] { 1, 2 })]
        [InlineData(new[] { 1, 2 }, new[] { 1 }, new[] { 1 })]
        public void MatchesInt(IEnumerable<int> a, IEnumerable<int> b, IEnumerable<int> expected)
        {
            Assert.Equal(
                expected,
                new Union<int>(
                   a, b
                )
            );
        }

        [Fact]
        public void UsesCompareFunction()
        {
            Assert.Equal(
                "c:/abraham/a.jpg c:/caesar/c.jpg",
                new Text.Joined(" ",
                    new Union<string>(
                        new AsList<string>("c:/abraham/a.jpg", "c:/bertram/b.jpg", "c:/caesar/c.jpg"),
                        new AsList<string>("a", "c"),
                        (aItem, bItem) =>
                            new Equals<string>(
                                Path.GetFileNameWithoutExtension(aItem),
                                bItem
                            ).Value()
                    )
                ).AsString()
            );
        }
    }
}
