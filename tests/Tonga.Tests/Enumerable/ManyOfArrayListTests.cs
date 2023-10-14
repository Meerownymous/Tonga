

using System.Collections;
using Xunit;

namespace Tonga.Enumerable.Test
{
    public class ManyOfArrayListTests
    {
        [Fact]
        public void BuildsFromStrings()
        {
            var arr = new ArrayList() { "A", "B", "C" };

            Assert.True(
                new ItemAt<object>(
                    new ManyOfArrayList(arr)
                ).Value().ToString() == "A");
        }
    }
}
