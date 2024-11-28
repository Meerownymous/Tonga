using System.Collections.Generic;
using System.IO;
using Tonga.Enumerable;
using Tonga.List;
using Tonga.Scalar;
using Xunit;

namespace Tonga.Tests.Enumerable
{
    public sealed class IntersectionTests
    {
        [Fact]
        public void IntersectsToEmpty()
        {
            Assert.Empty(
                new Intersection<string>(
                    ["a", "b"],
                    ["c", "d"]
                )
            );
        }

        [Theory]
        [InlineData(new[] { "v", "g", "x" }, new[] { "a", "b", "x" }, new[] { "x" })]
        [InlineData(new[] { "a", "a", "b", "c" }, new[] { "a", "b" }, new[] { "a", "b" })]
        public void SolvesStrings(IEnumerable<string> left, IEnumerable<string> right, IEnumerable<string> expected)
        {
            Assert.Equal(
                expected,
                new Intersection<string>(left, right)
            );
        }

        [Theory]
        [InlineData(new[] { 1, 1, 2, 4 }, new[] { 1, 2, 2, 3, 5 }, new[] { 1, 2 })]
        [InlineData(new[] { 1, 2 }, new int[0], new int[0])]
        public void SolvesInts(IEnumerable<int> a, IEnumerable<int> b, IEnumerable<int> expected)
        {
            Assert.Equal(
                expected,
                new Sorted<int>(
                    new Intersection<int>(a, b)
                )
            );
        }

        [Fact]
        public void UsesCompareFunction()
        {
            Assert.Equal(
                "a:/a.jpg",
                new Text.Joined(" ",
                    new Intersection<string>(
                        new AsList<string>("a:/a.jpg", "b:/b.jpg", "c:/c.jpg"),
                        new AsList<string>("a"),
                        (left, right) =>
                        {
                            var result =
                                new Equals<string>(
                                    Path.GetFileNameWithoutExtension(left),
                                    Path.GetFileNameWithoutExtension(right)
                                ).Value();

                            return result;
                        }
                    )
                ).AsString()
            );
        }
    }
}
