using Tonga.IO;
using Tonga.Text;
using Xunit;

namespace Tonga.Tests.IO
{
    public sealed class DeadConduitTest
    {
        [Fact]
        public void ReadsEmptyContent()
        {
            Assert.Equal(
                string.Empty,
                new DeadConduit().AsText().Str()
            );
        }

    }
}
