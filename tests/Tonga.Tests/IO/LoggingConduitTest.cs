using System;
using System.IO;
using Tonga.Bytes;
using Tonga.IO;
using Xunit;

namespace Tonga.Tests.IO
{
    public sealed class LoggingConduitTest
    {
        [Fact]
        public void LogsZeroBytesOnEmptyInput()
        {
            var res =
                new TeeOnRead(
                    new AsConduit(""),
                    new LoggingOnReadConduit(
                        new ConsoleOutput(),
                        "memory"
                    )
                )
                .Length()
                .Long();

            Assert.Equal(
                0L,
                res
            );
        }

        [Fact]
        public void LogsWriteOneBytesToTextFile()
        {
            using var tempfile = new TempFile("txt");
            using (var append = new Appending(new Uri(tempfile.Value())))
            {
                var output =
                    new LoggingOnReadConduit(
                        append,
                        "memory"
                    ).Stream();

                output.Write(new AsBytes("a").Raw(), 0, 1);

            }
            string content;
            using (var reader = new Uri(tempfile.Value()).AsStreamReader())
            {
                content = reader.ReadToEnd();
            }
            Assert.Equal(
                "a",
                content
            );
        }

        [Fact]
        public void LogsWriteTextToTextFile()
        {
            using var tempfile = new TempFile("txt");
            var bytes = new AsBytes("Hello World!").Raw();

            using (var append = new Appending(new Uri(tempfile.Value())))
            {
                var output =
                    new LoggingOnReadConduit(
                        append,
                        "memory"
                    ).Stream();


                output.Write(bytes, 0, bytes.Length);
            }


            string content;
            using (var reader = new Uri(tempfile.Value()).AsStreamReader())
            {
                content = reader.ReadToEnd();
            }

            Assert.Equal(
                bytes,
                new AsBytes(content).Raw()
            );
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

                    new FullRead(
                        new TeeOnRead(
                            new Resource("Assets/Txt/large-text.txt", this.GetType()),
                            new AsConduit(output)
                        )
                    ).Trigger();
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
