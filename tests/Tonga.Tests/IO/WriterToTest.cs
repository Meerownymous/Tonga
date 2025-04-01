using System;
using System.IO;
using Tonga.Bytes;
using Tonga.IO;
using Tonga.Text;
using Xunit;

namespace Tonga.Tests.IO;

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
                AsText._(
                    new TeeInput(
                        new AsInput(content),
                        new WriterAsOutput(
                            output
                        )
                    )
                ).AsString();
        }

        Assert.True(
            AsText._(
                new InputAsBytes(
                    new AsInput(uri)
                )
            ).AsString().CompareTo(s) == 0 //.CompareTo is needed because Streamwriter writes UTF8 _with_ BOM, which results in a different encoding.
        );
    }
}
