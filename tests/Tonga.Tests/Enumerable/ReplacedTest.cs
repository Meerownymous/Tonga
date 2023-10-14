

using Xunit;

namespace Tonga.Enumerable.Test
{
    public sealed class ReplacedTest
    {
        [Fact]
        public void ReplacesElementAtIndex()
        {
            Assert.True(
                new ItemAt<string>(
                    new Replaced<string>(
                        new string[] { "A", "B", "C", "D", "E" },
                        2,
                        "F"
                    ),
                    2
                ).Value() == "F"
            );
        }
    }
}
