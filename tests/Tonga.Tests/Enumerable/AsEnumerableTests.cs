using System.Collections.Generic;
using Tonga.Enumerable;
using Tonga.List;
using Xunit;

namespace Tonga.Tests.Enumerable
{
    public sealed class AsEnumerableTest
    {
        [Fact]
        public void SensesChanges()
        {
            var lst = new List<string>();
            var length =
                    new AsEnumerable<string>(() =>
                    {
                        lst.Add("something");
                        return lst.GetEnumerator();
                    }).Length();

            var a = length.Value();
            var b = length.Value();
            Assert.NotEqual(a, b);
        }

        [Fact]
        public void Enumerates()
        {
            Assert.Equal(
                new AsList<string>("one", "two", "eight" ),
                new AsEnumerable<string>("one", "two", "eight")
            );
        }

        [Fact]
        public void CanBeEmpty()
        {
            Assert.False(
                new AsEnumerable<string>()
                    .GetEnumerator()
                    .MoveNext()
            );
        }
    }
}
