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
            new BytesMorph(
                new TextMorph(content, Encoding.UTF8)
            ).Raw()
        );

        AssertText.Equal(
            content,
            new ConduitAsBytes(
                    new AsStream(
                        new Uri(file.Value())
                    )
            )
        );
    }

    [Fact]
    public void ReadsFromReader()
    {
        String content = "Hello, дорогой товарищ!";
        AssertText.Equal(
            content,
            new ConduitMorph(
                new AsStream(
                    new StreamReader(
                        new ConduitMorph(content).Stream()
                    )
                )
            )
        );
    }

    [Fact]
    public void ReadsFromReaderThroughSmallBuffer()
    {
        String content = "Hello, صديق!";
        AssertText.Equal(
            content,
            new StreamReader(
                new ConduitMorph(content).Stream()
            )
            .AsStream()
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
        File.WriteAllBytes(path, new BytesMorph(new TextMorph(content, Encoding.UTF8)).Raw());

        AssertText.Equal(
            content,
            new Uri(path).AsStream()
        );
    }
}
