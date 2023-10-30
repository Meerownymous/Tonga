

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
                AsText._(
                    new TeeInput(
                        new AsInput("How are you, мой друг?"),
                        new DeadOutput()
                    )
                ).AsString()
            );
        }
    }
}
