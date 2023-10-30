

using Xunit;


namespace Tonga.IO.Tests
{
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
                        new Rersource(
                            "Assets/Zip/ZipWithThreeFiles.zip",
                            this.GetType()
                        )
                    )
                )
            );
        }
    }
}
