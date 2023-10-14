

using Xunit;
using Tonga.Text;

namespace Tonga.IO.Tests
{
    public sealed class DeadOutputTest
    {
        [Fact]
        public void ReadsEmptyContent()
        {
            Assert.EndsWith(
                "друг?",
                new LiveText(
                new TeeInput(
                    new InputOf("How are you, мой друг?"),
                    new DeadOutput()
                )).AsString());
        }
    }
}
