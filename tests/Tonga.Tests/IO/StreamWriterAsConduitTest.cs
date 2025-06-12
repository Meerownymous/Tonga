using System;
using System.IO;
using Tonga.Bytes;
using Tonga.Enumerable;
using Tonga.IO;
using Tonga.Scalar;
using Xunit;

namespace Tonga.Tests.IO;

public sealed class StreamWriterAsConduitTest
{
    [Fact]
    public void WritesLargeContentToFile()
    {
        var dir = "artifacts/WriterAsOutputTest"; var inputFile = "large-text.txt";
        var inputPath = Path.GetFullPath(Path.Combine(dir, inputFile));
        var outputFile = "text-copy.txt";
        var outputPath = Path.GetFullPath(Path.Combine(dir, outputFile));

        Directory.CreateDirectory(dir);
        if (File.Exists(inputPath)) File.Delete(inputPath);
        if (File.Exists(outputPath)) File.Delete(outputPath);

        //Create large file
        new FullRead(
            new AsConduit(
                new TeeOnReadStream(
                    new MemoryStream(
                        new global::Tonga.Text.Joined(",",
                            "Hello World".AsRepeated(1000)
                        ).AsBytes()
                        .Raw()
                    ),
                    new Uri(inputPath).AsStream()
                )
            )
        ).Trigger();

        //Read from large file and write to output file (make a copy)
        var filestream = new FileStream(outputPath, FileMode.OpenOrCreate, FileAccess.Write, FileShare.Write);

        new FullRead(
            new TeeOnRead(
                new AsConduit(
                    new Uri(Path.GetFullPath(inputPath))
                ),
                new StreamWriterAsConduit(
                    new StreamWriter(filestream)
                )
            )
        ).Trigger();

        Assert.Equal(
            new AsConduit(
                    new Uri(Path.GetFullPath(inputPath))
                )
                .Length()
                .Long(),
            new AsConduit(
                    new Uri(Path.GetFullPath(outputPath))
                )
                .Length()
                .Long()
        );
    }

}
