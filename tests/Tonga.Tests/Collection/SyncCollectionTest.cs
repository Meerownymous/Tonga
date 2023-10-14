

using Xunit;

namespace Tonga.Collection.Tests
{
    public sealed class SyncCollectionTest
    {
        [Fact]
        public void BehavesAsCollection()
        {
            Assert.Contains(
                -1,
                new Sync<int>(1, 2, 0, -1)
            );
        }

    }
}
