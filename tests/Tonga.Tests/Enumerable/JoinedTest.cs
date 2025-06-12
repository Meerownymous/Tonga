using System.Linq;
using Tonga.Enumerable;
using Xunit;

namespace Tonga.Tests.Enumerable
{
    public sealed class JoinedTest
    {
        [Fact]
        public void TransformsList()
        {
            Assert.Equal(
                ["hello", "world", "друг", "how", "are", "you", "what's", "up"],
                new[]{
                    ("hello", "world", "друг").AsEnumerable(),
                    ("how", "are", "you").AsEnumerable(),
                    ("what's", "up").AsEnumerable()
                }.AsJoined()
                .ToArray()
            );
        }

        [Fact]
        public void JoinsEnumerables()
        {
            Assert.Equal(
                new[] { "x", "x" },
                "x".AsRepeated(2)
                    .AsMapped(item => item.AsSingle())
                    .AsEnumerable()
                    .AsJoined()
                    .ToArray()
            );
        }

        [Fact]
        public void JoinsSingleElements()
        {
            Assert.Equal(
                ["hello", "world", "друг", "how", "are", "you", "what's", "up"],
                ("hello", "world", "друг")
                    .AsEnumerable()
                    .AsJoined(
                        "how",
                        "are",
                        "you",
                        "what's",
                        "up"
                    )
            );
        }
    }
}
