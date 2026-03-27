using Tonga.IO;
using Tonga.Text;
using Xunit;

namespace Tonga.Tests.IO
{
    public sealed class Sha1DigestOfTest
    {
        [Fact]
        public void ChecksumOfEmptyString()
        {
            AssertText.Equal(
                 "da39a3ee5e6b4b0d3255bfef95601890afd80709",
                 new AsHex(
                     new Sha1DigestOf(string.Empty)
                 )
            );
        }

        [Fact]
        public void ChecksumOfString()
        {
            AssertText.Equal(
                "2ef7bde608ce5404e97d5f042f95f89f1c232871",
                new AsHex(
                    new Sha1DigestOf("Hello World!")
                )
            );
        }

        [Fact]
        public void ChecksumFromFile()
        {
            AssertText.Equal(
                "34f80bdab9b93af514004f127e440139aad63e2d",
                new AsHex(
                    new Sha1DigestOf(
                        new Resource(
                            "IO/Resources/digest-calculation.txt",
                            this.GetType()
                        )
                    )
                )
            );
        }
    }
}
