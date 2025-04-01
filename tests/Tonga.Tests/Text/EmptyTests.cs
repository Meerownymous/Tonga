using Tonga.Text;
using Xunit;

namespace Tonga.Tests.Text
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
