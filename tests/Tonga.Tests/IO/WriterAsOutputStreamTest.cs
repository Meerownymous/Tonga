using System;
using System.IO;
using Tonga.Enumerable;
using Tonga.IO;
using Xunit;

namespace Tonga.Tests.IO;

public sealed class WriterAsOutputStreamTest
{
    [Fact]
    public void WritesContentToFile()
    {
        var inputFile = new TempFile("large-text.txt");
        var outputFile = new TempFile("copy-text.txt");
        using (inputFile)
        {
            using (outputFile)
            {
                var inputPath = Path.GetFullPath(inputFile.Value());
                var outputPath = Path.GetFullPath(outputFile.Value());

                //Create large file
                new FullRead(
                    new AsConduit(
                        new TeeOnReadStream(
                            new global::Tonga.Text.Joined(",",
                                "Hello World".AsRepeated(1000)
                            ).AsStream(),
                            new Uri(inputPath).AsStream()
                        )
                    )
                ).Trigger();

                using (var tee =
                       new AsConduit(
                           new TeeOnReadStream(
                               new AsConduit(inputPath).Stream(),
                               new WriterAsOutputStream(
                                   new StreamWriter(outputPath)
                               )
                           )
                       )
                      )
                {
                    // tee.Stream().Flush();
                    // tee.Stream().Dispose();
                    // new FullRead(tee, flush: true, close: false).Trigger();
                    //
                    // Assert.Equal(
                    //     tee.Length().Int(),
                    //     new Uri(Path.GetFullPath(outputPath)).AsStream().Length
                    // );
                }
            }
        }
    }

    [Fact]
    public void RejectsReading()
    {
        Assert.ThrowsAny<NotImplementedException>(
            () => new WriterAsOutputStream(
                new StreamWriter(
                    new MemoryStream()
                )
            ).Read([], 0, 1)
        );
    }

    [Fact]
    public void RejectsSeeking()
    {
        Assert.ThrowsAny<NotImplementedException>(
            () => new WriterAsOutputStream(
                new StreamWriter(
                    new MemoryStream()
                )
            ).Seek(3L, SeekOrigin.Begin)
        );
    }

    [Fact]
    public void RejectsSettingLength()
    {
        Assert.ThrowsAny<NotImplementedException>(
            () => new WriterAsOutputStream(
                new StreamWriter(
                    new MemoryStream()
                )
            ).SetLength(5L)
        );
    }
}
