using Tonga.IO;
using Tonga.Text;
using Xunit;

namespace Tonga.Tests.IO
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
                        new Tonga.IO.AsInput("How are you, мой друг?"),
                        new DeadOutput()
                    )
                ).AsString()
            );
        }
    }
}
