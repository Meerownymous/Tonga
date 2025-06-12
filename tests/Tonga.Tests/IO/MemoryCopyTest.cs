using System.IO;
using Tonga.IO;
using Xunit;

namespace Tonga.Tests.IO;

public sealed class MemoryCopyTest
{

    [Fact]
    public void MemorizesInput()
    {
        var memoryInput =
            new MemoryCopy(
                new AsConduit(
                    "This is my input!"
                )
            );

        string memoryContent;
        using (var reader = new StreamReader(memoryInput.Stream()))
        {
            memoryContent = reader.ReadToEnd();
        }

        Assert.Equal("This is my input!", memoryContent);
    }

    [Fact]
    public void MemorizesEmptyInput()
    {

        var memoryInput =
            new MemoryCopy(
                "".AsConduit()
            );

        var memoryContent = "";
        using (var reader = new StreamReader(memoryInput.Stream()))
        {
            memoryContent = reader.ReadToEnd();
        }

        Assert.Equal("", memoryContent);
    }
}
