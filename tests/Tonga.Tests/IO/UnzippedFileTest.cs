using System;
using Tonga.IO;
using Tonga.Text;
using Xunit;

namespace Tonga.Tests.IO;

public class UnzippedFileTest
{
    [Theory]
    [InlineData("File1")]
    [InlineData("File2")]
    [InlineData("File3")]
    public void FindsFile(string fileName)
    {
        Assert.True(
            new UnzippedFile(
                new Resource(
                    "Assets/Zip/ZipWithThreeFiles.zip",
                    this.GetType()
                ),
                fileName
            ).Stream() != null
        );
    }

    [Theory]
    [InlineData("File1")]
    [InlineData("File2")]
    [InlineData("File3")]
    public void ReturnsSelectedFile(string fileName)
    {
        Assert.Contains(
            fileName,
            new UnzippedFile(
                new Resource(
                    "Assets/Zip/ZipWithThreeFiles.zip",
                    this.GetType()
                ),
                fileName
            ).AsText().Str()
        );
    }

    [Fact]
    public void RejectsNonZip()
    {
        Assert.Throws<InvalidOperationException>(() =>
        {
            new UnzippedFile(
                new Resource(
                    "Assets/Zip/NotAZip",
                    this.GetType()
                ),
                "irrelevant"
            ).Stream();
        });
    }
}
