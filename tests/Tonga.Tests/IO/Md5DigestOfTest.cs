using Tonga.IO;
using Tonga.Text;
using Xunit;

namespace Tonga.Tests.IO
{
    public sealed class Md5DigestOfTest
    {
        [Fact]
        public void ChecksumOfEmptyString()
        {
            Assert.Equal(
                 "d41d8cd98f00b204e9800998ecf8427e",
                 new AsHex(
                     new Md5DigestOf(new Tonga.IO.AsConduit(string.Empty))
                 ).Str()
            );
        }

        [Fact]
        public void ChecksumOfString()
        {
            Assert.Equal(
                "ed076287532e86365e841e92bfc50d8c",
                new AsHex(
                    new Md5DigestOf(new Tonga.IO.AsConduit("Hello World!"))
                ).Str()
            );
        }

        [Fact]
        public void ChecksumFromFile()
        {
            Assert.Equal(
                "162665ab3d58424724f83f28e7a147d6",
                new AsHex(
                    new Md5DigestOf(
                        new Resource(
                            "IO/Resources/digest-calculation.txt",
                            this.GetType()
                        )
                    )
                ).Str()
            );
        }
    }
}
