using System;
using System.IO;
using System.Text;
using Tonga.Bytes;
using Tonga.IO;
using Tonga.Text;
using Xunit;

namespace Tonga.Tests.IO;

public sealed class AsStreamTest
{
    [Fact]
    public void ReadsSimpleFileContent()
    {
        using var file = new TempFile();
        String content = "Hello, товарищ!";
        File.WriteAllBytes(
            file.Value(),
            content
                .AsText(Encoding.UTF8)
                .AsBytes()
                .Raw()
        );

        Assert.Equal(
            content,
            new ConduitAsBytes(
                new AsConduit(
                    new AsStream(
                        new Uri(file.Value())
                    )
                )
            ).AsText()
            .Str()
        );
    }

    [Fact]
    public void ReadsFromReader()
    {
        String content = "Hello, дорогой товарищ!";
        Assert.Equal(
            content,
            new AsConduit(
                new AsStream(
                    new StreamReader(
                        new AsConduit(content).Stream()
                    )
                )
            ).AsText().Str()
        );
    }

    [Fact]
    public void ReadsFromReaderThroughSmallBuffer()
    {
        String content = "Hello, صديق!";
        Assert.Equal(
            content,
            new StreamReader(
                new AsConduit(content).Stream()
            )
            .AsStream()
            .AsText()
            .Str()
        );
    }

    [Fact]
    public void MakesDataAvailable()
    {
        Assert.True(
            "Hello,חבר!".AsStream().Length > 0
        );
    }

    [Fact]
    public void ReadsSimpleFileContentWithWhitespacesInUri()
    {
        var dir = "artifacts/Input StreamOf Test";
        var file = "txt-1";
        var path = Path.GetFullPath(Path.Combine(dir, file));

        Directory.CreateDirectory(dir);
        if (File.Exists(path)) File.Delete(path);

        String content = "Hello, товарищ!";
        File.WriteAllBytes(path, new AsBytes(content.AsText(Encoding.UTF8)).Raw());

        Assert.Equal(
            content,
            new Uri(path).AsStream().AsText().Str()
        );
    }
}
