using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using Tonga.Enumerable;
using Tonga.Fact;
using Tonga.List;
using Xunit;

namespace Tonga.Tests.Enumerable
{
    public sealed class UnionTests
    {
        [Fact]
        public void UnitesEmpty()
        {
            Assert.Empty(
                new Union<string>(
                    new None<string>(),
                    new None<string>()
                )
            );
        }

        [Theory]
        [InlineData(new[] { "a", "b" }, new[] { "a", "b" }, new[] { "a", "b" })]
        [InlineData(new[] { "a", "a", "b" }, new[] { "a" }, new[] { "a", "b" })]
        public void UnitesStrings(IEnumerable<string> a, IEnumerable<string> b, IEnumerable<string> expected)
        {
            Assert.Equal(
                expected,
                new Union<string>(
                   a, b
                )
            );
        }

        [Theory]
        [InlineData(new[] { 1, 1, 2, 4 }, new[] { 1, 2, 2, 3, 5 }, new[] { 1, 2, 3, 4, 5 })]
        [InlineData(new[] { 1, 2 }, new int[0], new[] { 1, 2 })]
        public void MatchesInt(IEnumerable<int> a, IEnumerable<int> b, IEnumerable<int> expected)
        {
            Assert.Equal(
                expected,
                new Sorted<int>(
                    new Union<int>(
                       a, b
                    )
                )
            );
        }

        [Fact]
        public void UsesCompareFunction()
        {
            Assert.Equal(
                "a:/a.jpg b:/b.jpg c:/c.jpg",
                new Text.Joined(" ",
                    new Union<string>(
                        new AsList<string>("a:/a.jpg", "b:/b.jpg", "c:/c.jpg"),
                        new AsList<string>("a", "c"),
                        (left, right) =>
                        {
                            var result =
                                new Equals<string>(
                                    Path.GetFileNameWithoutExtension(left),
                                    Path.GetFileNameWithoutExtension(right)
                                ).IsTrue();

                            Debug.WriteLine($"{Path.GetFileNameWithoutExtension(left)}=={right} ? {result}");
                            return result;
                        }
                    )
                ).AsString()
            );
        }
    }
}
