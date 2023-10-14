

using Xunit;
using Tonga.Text;

namespace Tonga.IO.Tests
{
    public sealed class DeadInputTest
    {
        [Fact]
        public void ReadsEmptyContent()
        {
            Assert.True(
                new LiveText(
                    new DeadInput())
                .AsString() == "",
                "Can't read empty content");
        }

    }
}
