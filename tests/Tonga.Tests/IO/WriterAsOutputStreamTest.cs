using System;
using System.IO;
using Tonga.Bytes;
using Tonga.Enumerable;
using Tonga.Func;
using Tonga.IO;
using Tonga.Scalar;
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
                ReadAll._(
                    new AsInput(
                        new TeeInputStream(
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
                            new OutputTo(
                                new Uri(inputPath)
                            ).Stream()
                        )
                    )
                ).Invoke();

                using (var tee =
                       new AsInput(
                           new TeeInputStream(
                               new AsInput(
                                   inputPath
                               ).Stream(),
                               new WriterAsOutputStream(
                                   new StreamWriter(outputPath)
                               )
                           )
                       )
                      )
                {

                    ReadAll._(tee).Invoke(flush: true, close: false);

                    Assert.Equal(
                        Length._(
                            tee
                        ).Value(),
                        Length._(
                            new AsInput(
                                new Uri(Path.GetFullPath(outputPath))
                            )
                        ).Value()
                    );
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
            ).Read(new byte[] {}, 0, 1)
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
