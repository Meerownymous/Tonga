using Xunit;
using Tonga.Text;

namespace Tonga.IO.Tests
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
