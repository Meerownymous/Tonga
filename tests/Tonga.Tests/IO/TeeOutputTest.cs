using System;
using System.IO;
using System.Text;
using Tonga.IO;
using Tonga.Text;
using Xunit;

namespace Tonga.Tests.IO
{
    public sealed class TeeOutputTest
    {
        [Fact]
        public void CopiesContent()
        {
            var baos = new MemoryStream();
            var copy = new MemoryStream();
            String content = "Hello, товарищ!";

            Assert.True(
                AsText._(
                    new TeeInput(
                        new Tonga.IO.AsInput(content),
                        new TeeOutput(
                            new OutputTo(baos),
                            new OutputTo(copy)
                        )
                    )
                ).AsString() == Encoding.UTF8.GetString(copy.ToArray()),
                "Can't copy Output to Output and return Input");
        }
    }
}
