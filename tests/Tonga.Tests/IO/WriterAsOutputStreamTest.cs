

using System;
using System.IO;
using System.Text;
using Xunit;
using Tonga.Bytes;
using Tonga.Enumerable;
using Tonga.Text;

namespace Tonga.IO.Tests
{
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
                    new LengthOf(
                        new InputOf(
                            new TeeInputStream(
                                new MemoryStream(
                                    new BytesOf(
                                        new Text.Joined(",",
                                            new Head<string>(
                                                new Endless<string>("Hello World"),
                                                1000
                                            )
                                        )
                                    ).AsBytes()
                                ),
                                new OutputTo(
                                    new Uri(inputPath)
                                ).Stream()
                            )
                        )
                    ).Value();

                    Assert.True(
                        new LengthOf(
                            new InputOf(
                                new TeeInputStream(
                                    new InputOf(
                                        inputPath
                                        ).Stream(),
                                    new WriterAsOutputStream(
                                        new StreamWriter(outputPath)
                                        )
                                    )
                                )
                            ).Value() ==
                        new LengthOf(
                            new InputOf(
                                new Uri(Path.GetFullPath(outputPath))
                                )
                            ).Value(),
                        "input and output are not the same size"
                        );
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
}

