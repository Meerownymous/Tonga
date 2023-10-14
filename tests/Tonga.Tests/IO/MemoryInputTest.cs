

using System.IO;
using Xunit;

namespace Tonga.IO.Tests
{
    public sealed class MemoryInputTest
    {

        [Fact]
        public void MemorizesInput()
        {
            var memoryInput =
                new MemoryInput(
                    new InputOf(
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
                    new InputOf(
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
