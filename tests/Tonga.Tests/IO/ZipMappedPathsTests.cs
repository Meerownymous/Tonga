

using Tonga.IO;
using Xunit;

namespace Tonga.Tests.IO;

public class ZipMappedPathsTests
{
    [Fact]
    public void Maps()
    {
        Assert.Contains(
            "directory/File1",
            new ZipFiles(
                new ZipMappedPaths(
                    path => "directory/" + path,
                    new Resource(
                        "Assets/Zip/ZipWithThreeFiles.zip",
                        this.GetType()
                    )
                )
            )
        );
    }
}
