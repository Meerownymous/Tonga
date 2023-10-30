

using System;
using Xunit;
using Tonga.Text;

namespace Tonga.IO.Tests
{
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
                            new Rersource(
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
                       new Rersource(
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
                    new Rersource(
                        "Assets/Zip/NotAZip",
                        this.GetType()
                    ),
                    "irrelevant"
                ).Stream();
            });
        }
    }
}
