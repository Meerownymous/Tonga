

using System;
using System.IO;
using Xunit;
using Tonga.Bytes;
using Tonga.Scalar;
using Tonga.Func;

namespace Tonga.IO.Tests
{
    public sealed class LoggingOutputTest
    {
        [Fact]
        public void LogsZeroBytesOnEmptyInput()
        {
            var res =
                Length._(
                    new TeeInput(
                        new AsInput(""),
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
                using (var append = new AppendTo(new Uri(tempfile.Value())))
                {
                    var output =
                        new LoggingOutput(
                            append,
                            "memory"
                        ).Stream();

                    output.Write(new AsBytes("a").Bytes(), 0, 1);

                }
                var inputStream = new AsInput(new Uri(tempfile.Value())).Stream();
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
                var bytes = new AsBytes("Hello World!").Bytes();

                using (var append = new AppendTo(new Uri(tempfile.Value())))
                {
                    var output =
                    new LoggingOutput(
                        append,
                        "memory"
                    ).Stream();


                    output.Write(bytes, 0, bytes.Length);
                }

                var inputStream = new AsInput(new Uri(tempfile.Value())).Stream();
                var content = "";
                using (var reader = new StreamReader(inputStream))
                {
                    content = reader.ReadToEnd();
                }

                Assert.Equal(
                    bytes,
                    new AsBytes(content).Bytes()
                );
            }
        }

        [Fact]
        public void LogsWriteLargeTextToTextFile()
        {
            using (var tempfile = new TempFile("txt"))
            {
                using (var append = new AppendTo(new Uri(tempfile.Value())))
                {
                    var output =
                    new LoggingOutput(
                        append,
                        "text file"
                    ).Stream();

                    ReadAll._(
                        new TeeInput(
                            new Resource("Assets/Txt/large-text.txt", this.GetType()),
                            new OutputTo(output)
                        )
                    ).Invoke();
                }

                var inputStream = new AsInput(new Uri(tempfile.Value())).Stream();
                var content = "";
                var input = "";
                using (var reader = new StreamReader(inputStream))
                {
                    content = reader.ReadToEnd();
                }
                using (var reader = new StreamReader(new Resource("Assets/Txt/large-text.txt", this.GetType()).Stream()))
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
