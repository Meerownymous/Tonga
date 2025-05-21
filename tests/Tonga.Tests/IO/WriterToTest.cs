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
                    new TeeOnRead(
                        new AsConduit(content),
                        new WriterAsConduit(output)
                    )
                ).AsString();
        }

        Assert.Equal(
            0,
            String.Compare(AsText._(
                new ConduitAsBytes(
                    new AsConduit(uri)
                )
            ).AsString(), s, StringComparison.Ordinal)
            //.CompareTo is needed because Streamwriter writes UTF8 _with_ BOM, which results in a different encoding.
        );
    }
}
