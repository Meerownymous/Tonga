

using Xunit;
using Tonga.Enumerable;
using Tonga.Enumerator;

namespace Tonga.Enumerator.Test
{
    public sealed class LiveLengthOfTests
    {
        [Fact]
        public void Counts()
        {
            Assert.True(
                new LiveLengthOf(
                    new ManyOf<int>(1, 2, 3, 4, 5).GetEnumerator()).Value() == 5,
                "cannot count items"
            );
        }
    }
}
