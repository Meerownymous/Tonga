using System.IO;
using Tonga.IO;
using Xunit;

namespace Tonga.Tests.IO;

public sealed class TempFileTest
{

    [Fact]
    public void CreateAndDeleteTemporyFile()
    {
        var file = new TempFile();
        using (file)
        {
            Assert.True(File.Exists(file.Value()));
        }
        Assert.False(File.Exists(file.Value()));
    }


    [Theory]
    [InlineData("txt")]
    [InlineData(".txt")]
    public void CreateAndDeleteTemporyFileWithGivenExtension(string extension)
    {
        var file = new TempFile(extension);
        using (file)
        {
            Assert.True(File.Exists(file.Value()));
        }
        Assert.False(File.Exists(file.Value()));
    }

    [Fact]
    public void CreateAndDeleteTemporyFileByFileInfo()
    {
        var file = Path.GetTempFileName();
        using (var tmpFile = new TempFile(new FileInfo(file)))
        {
            Assert.True(File.Exists(tmpFile.Value()));
        }
        Assert.False(File.Exists(file));
    }
}
