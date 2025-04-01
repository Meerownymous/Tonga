using Tonga.IO;
using Tonga.Text;
using Xunit;

namespace Tonga.Tests.IO
{
    public sealed class DeadInputTest
    {
        [Fact]
        public void ReadsEmptyContent()
        {
            Assert.Equal(
                string.Empty,
                AsText._(
                    new DeadInput())
                .AsString()
            );
        }

    }
}
