

using Xunit;
using Tonga.Text;

namespace Tonga.Text.Test
{
    public sealed class BlankTests
    {
        [Fact]
        public void IsBlank()
        {
            Assert.Equal(
                string.Empty,
                new Empty().AsString()
            );
        }
    }
}
