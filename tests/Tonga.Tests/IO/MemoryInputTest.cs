using System.IO;
using Tonga.IO;
using Xunit;

namespace Tonga.Tests.IO
{
    public sealed class MemoryInputTest
    {

        [Fact]
        public void MemorizesInput()
        {
            var memoryInput =
                new MemoryInput(
                    new Tonga.IO.AsInput(
                        "This is my input!"
                    )
                );

            var memoryContent = "";
            using (var reader = new StreamReader(memoryInput.Stream()))
            {
                memoryContent = reader.ReadToEnd();
            }

            Assert.Equal(
                "This is my input!",
                memoryContent
            );
        }

        [Fact]
        public void MemorizesEmptyInput()
        {

            var memoryInput =
                new MemoryInput(
                    new Tonga.IO.AsInput(
                        ""
                    )
                );

            var memoryContent = "";
            using (var reader = new StreamReader(memoryInput.Stream()))
            {
                memoryContent = reader.ReadToEnd();
            }

            Assert.Equal(
                "",
                memoryContent
            );
        }
    }
}
