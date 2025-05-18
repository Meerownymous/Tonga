using Tonga.Enumerable;
using Xunit;

namespace Tonga.Tests.Enumerable
{
    public class ContainsTest
    {
        [Fact]
        public void FindsItem()
        {
            Assert.True(
                Contains._(
                    AsEnumerable._("Hello", "my", "cat", "is", "missing"),
                    str => str == "cat"
                ).IsTrue()
            );
        }

        [Fact]
        public void DoesntFindItem()
        {
            Assert.False(
                Contains._(
                    AsEnumerable._("Hello", "my", "cat", "is", "missing"),
                    (str) => str == "elephant"
                ).IsTrue()
            );
        }

        [Fact]
        public void DoesntFindInEmtyList()
        {
            Assert.False(
                Contains._(
                    None._<string>(),
                    (str) => str == "elephant"
                ).IsTrue()
            );
        }
    }
}
