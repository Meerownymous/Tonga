

using System;
using System.IO;
using Xunit;
using Tonga.Bytes;

namespace Tonga.IO.Tests
{
    public sealed class LoggingOutputTest
    {
        [Fact]
        public void LogsZeroBytesOnEmptyInput()
        {
            var res =
                new LengthOf(
                    new TeeInput(
                        new InputOf(""),
                        new LoggingOutput(
                            new ConsoleOutput(),
                            "memory"
                        )
                    )
                ).Value();

            Assert.Equal(
                0L,
                res
            );
        }

        [Fact]
        public void LogsWriteOneBytesToTextFile()
        {
            using (var tempfile = new TempFile("txt"))
            {
                using (var append = new AppendTo(tempfile.Value()))
                {
                    var output =
                        new LoggingOutput(
                            append,
                            "memory"
                        ).Stream();

                    output.Write(new BytesOf("a").AsBytes(), 0, 1);

                }
                var inputStream = new InputOf(new Uri(tempfile.Value())).Stream();
                var content = "";
                using (var reader = new StreamReader(inputStream))
                {
                    content = reader.ReadToEnd();
                }
                Assert.Equal(
                    "a",
                    content
                );
            }
        }

        [Fact]
        public void LogsWriteTextToTextFile()
        {
            using (var tempfile = new TempFile("txt"))
            {
                var bytes = new BytesOf("Hello World!").AsBytes();

                using (var append = new AppendTo(tempfile.Value()))
                {
                    var output =
                    new LoggingOutput(
                        append,
                        "memory"
                    ).Stream();


                    output.Write(bytes, 0, bytes.Length);
                }

                var inputStream = new InputOf(new Uri(tempfile.Value())).Stream();
                var content = "";
                using (var reader = new StreamReader(inputStream))
                {
                    content = reader.ReadToEnd();
                }

                Assert.Equal(
                    bytes,
                    new BytesOf(content).AsBytes()
                );
            }
        }

        [Fact]
        public void LogsWriteLargeTextToTextFile()
        {
            using (var tempfile = new TempFile("txt"))
            {

                using (var append = new AppendTo(tempfile.Value()))
                {
                    var output =
                    new LoggingOutput(
                        append,
                        "text file"
                    ).Stream();

                    var length =
                        new LengthOf(
                            new TeeInput(
                                new ResourceOf("Assets/Txt/large-text.txt", this.GetType()),
                                new OutputTo(output)
                            )
                        ).Value();

                }

                var inputStream = new InputOf(new Uri(tempfile.Value())).Stream();
                var content = "";
                var input = "";
                using (var reader = new StreamReader(inputStream))
                {
                    content = reader.ReadToEnd();
                }
                using (var reader = new StreamReader(new ResourceOf("Assets/Txt/large-text.txt", this.GetType()).Stream()))
                {
                    input = reader.ReadToEnd();
                }

                Assert.Equal(
                    input,
                    content
                );
            }
        }
    }
}
