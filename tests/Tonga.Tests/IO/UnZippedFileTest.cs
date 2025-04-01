using System;
using Tonga.IO;
using Tonga.Text;
using Xunit;

namespace Tonga.Tests.IO;

public class UnZippedFileTest
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
            AsText._(
                new UnzippedFile(
                    new Resource(
                        "Assets/Zip/ZipWithThreeFiles.zip",
                        this.GetType()
                    ),
                    fileName
                )
            ).AsString()
        );
    }

    [Fact]
    public void ThrowsExcWhenNotAZip()
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
