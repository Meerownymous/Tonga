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
        Length._(
            new AsConduit(
                new TeeStream(
                    new MemoryStream(
                        new AsBytes(
                            new global::Tonga.Text.Joined(",",
                                new Head<string>(
                                    new Endless<string>("Hello World"),
                                    1000
                                )
                            )
                        ).Bytes()
                    ),
                    new AsConduit(
                        new Uri(inputPath)
                    ).Stream()
                )
            )
        ).Value();

        //Read from large file and write to output file (make a copy)
        var filestream = new FileStream(outputPath, FileMode.OpenOrCreate, FileAccess.Write, FileShare.Write);

        long left;
        left =
            Length._(
                new TeeOnRead(
                    new AsConduit(
                        new Uri(Path.GetFullPath(inputPath))
                    ),
                    new StreamWriterAsConduit(
                        new StreamWriter(filestream)
                    )
                )
            ).Value();

        long right =
            Length._(
                new Tonga.IO.AsConduit(
                    new Uri(Path.GetFullPath(outputPath))
                )
            ).Value();

        Assert.True(left == right, "input and output are not the same size");
    }

}
