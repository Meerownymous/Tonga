using System.Collections;
using Tonga.Collection;
using Tonga.Enumerable;
using Xunit;

namespace Tonga.Tests.Collection
{
    public class ArrayListAsCollectionTest
    {
        [Fact]
        public void BuildsFromStrings()
        {
            Assert.Equal(
                "A",
                new ArrayListAsCollection(
                        new ArrayList { "A", "B", "C" }
                    )
                    .ItemAt(1)
                    .Value()
            );
        }
    }
}
