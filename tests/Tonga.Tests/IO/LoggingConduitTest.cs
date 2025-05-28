using System;
using System.IO;
using Tonga.Bytes;
using Tonga.Enumerable;
using Tonga.Func;
using Tonga.IO;
using Tonga.Scalar;
using Xunit;

namespace Tonga.Tests.IO
{
    public sealed class LoggingConduitTest
    {
        [Fact]
        public void LogsZeroBytesOnEmptyInput()
        {
            var res =
                Length._(
                    new TeeOnRead(
                        new AsConduit(""),
                        new LoggingOnReadConduit(
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
                using (var append = new Appending(new Uri(tempfile.Value())))
                {
                    var output =
                        new LoggingOnReadConduit(
                            append,
                            "memory"
                        ).Stream();

                    output.Write(new AsBytes("a").Bytes(), 0, 1);

                }
                var inputStream = new Tonga.IO.AsConduit(new Uri(tempfile.Value())).Stream();
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

                using (var append = new Appending(new Uri(tempfile.Value())))
                {
                    var output =
                    new LoggingOnReadConduit(
                        append,
                        "memory"
                    ).Stream();


                    output.Write(bytes, 0, bytes.Length);
                }

                var inputStream = new Tonga.IO.AsConduit(new Uri(tempfile.Value())).Stream();
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
                using (var append = new Appending(new Uri(tempfile.Value())))
                {
                    var output =
                    new LoggingOnReadConduit(
                        append,
                        "text file"
                    ).Stream();

                    ReadAll._(
                        new TeeOnRead(
                            new Resource("Assets/Txt/large-text.txt", this.GetType()),
                            new AsConduit(output)
                        )
                    ).Invoke();
                }

                var inputStream = new AsConduit(new Uri(tempfile.Value())).Stream();
                string content;
                string input;
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
