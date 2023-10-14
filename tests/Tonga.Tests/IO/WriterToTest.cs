

using System;
using System.IO;
using Xunit;
using Tonga.Bytes;
using Tonga.Text;

namespace Tonga.IO.Tests
{
    public sealed class WriterToTest
    {
        [Fact]
        public void WritesContentToFile()
        {
            var dir = "artifacts/WriterToTest"; var file = "txt.txt";
            var uri = new Uri(Path.GetFullPath(Path.Combine(dir, file)));
            Directory.CreateDirectory(dir);
            var content = "yada yada";

            string s;
            using (var output = new WriterTo(uri))
            {
                s =
                    new LiveText(
                        new TeeInput(
                            new InputOf(content),
                                new WriterAsOutput(
                                    output
                                )
                            )
                        ).AsString();
            }

            Assert.True(
                new LiveText(
                    new InputAsBytes(
                        new InputOf(uri)
                    )
                ).AsString().CompareTo(s) == 0 //.CompareTo is needed because Streamwriter writes UTF8 _with_ BOM, which results in a different encoding.
            );
        }
    }
}
