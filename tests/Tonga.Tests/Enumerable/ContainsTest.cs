using System.Linq;
using Tonga.Enumerable;
using Tonga.Text;
using Xunit;

namespace Tonga.Tests.Enumerable
{
    public class ContainsTest
    {
        [Fact]
        public void FindsItem()
        {
            Assert.True(
                new AsEnumerable<string>("Hello", "my", "cat", "is", "missing")
                    .Contains(str => str == "cat")
                    .IsTrue()
            );
        }

        [Fact]
        public void DoesntFindItem()
        {
            Assert.False(
                new AsEnumerable<string>("Hello", "my", "cat", "is", "missing")
                    .Contains(str => str == "elephant")
                    .IsTrue()
            );
        }

        [Fact]
        public void DoesntFindInEmtyList()
        {
            Assert.False(
                new None<string>()
                    .Contains(str => str == "elephant")
                    .IsTrue()
            );
        }
    }
}
