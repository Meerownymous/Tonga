using System;
using System.IO;
using System.Text;
using Tonga.IO;
using Tonga.Text;
using Xunit;

namespace Tonga.Tests.IO
{
    public sealed class TeeOnWriteTest
    {
        [Fact]
        public void CopiesContent()
        {
            var baos = new MemoryStream();
            var copy = new MemoryStream();
            String content = "Hello, товарищ!";

            AssertText.Equal(
                new TeeOnRead(
                    content,
                    new TeeOnWrite(
                        baos,
                        copy
                    )
                ),
                Encoding.UTF8.GetString(copy.ToArray())
            );
        }
    }
}
