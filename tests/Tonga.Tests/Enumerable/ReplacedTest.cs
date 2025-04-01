using Tonga.Enumerable;
using Tonga.Scalar;
using Xunit;

namespace Tonga.Tests.Enumerable
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
