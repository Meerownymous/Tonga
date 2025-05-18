

using Tonga.IO;
using Xunit;

namespace Tonga.Tests.IO;

public class ZipFilesTest
{
    [Theory]
    [InlineData("File1")]
    [InlineData("File2")]
    [InlineData("File3")]
    public void HasData(string expected)
    {
        Assert.Contains(
            expected,
            new ZipFiles(
                new Resource(
                    "Assets/Zip/ZipWithThreeFiles.zip",
                    this.GetType()
                )
            )
        );
    }

    [Fact]
    public void InputStreamRemainsPositionZero()
    {
        var res = new Resource(
            "Assets/Zip/ZipWithThreeFiles.zip",
            this.GetType());
        new ZipFiles(res);

        Assert.InRange(res.Stream().Position, 0, 0);
    }
}
