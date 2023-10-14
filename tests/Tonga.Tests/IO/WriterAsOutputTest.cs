

using System;
using System.IO;
using Xunit;
using Tonga.Bytes;
using Tonga.Enumerable;

namespace Tonga.IO.Tests
{
    public sealed class WriterAsOutputTest
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
            new LengthOf(
                new InputOf(
                    new TeeInputStream(
                        new MemoryStream(
                            new BytesOf(
                                new Text.Joined(",",
                                    new HeadOf<string>(
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

            //Read from large file and write to output file (make a copy)
            var filestream = new FileStream(outputPath, FileMode.OpenOrCreate, FileAccess.Write, FileShare.Write);

            long left;
            left =
                new LengthOf(
                    new TeeInput(
                        new InputOf(
                            new Uri(Path.GetFullPath(inputPath))
                        ),
                        new WriterAsOutput(
                            new StreamWriter(filestream)
                        )
                    )
                ).Value();

            long right =
                new LengthOf(
                    new InputOf(
                        new Uri(Path.GetFullPath(outputPath))
                    )
                ).Value();

            Assert.True(left == right, "input and output are not the same size");
        }

    }
}
