

using Xunit;
using Tonga.Text;

namespace Tonga.IO.Tests
{
    public sealed class Md5DigestOfTest
    {
        [Fact]
        public void ChecksumOfEmptyString()
        {
            Assert.Equal(
                 "d41d8cd98f00b204e9800998ecf8427e",
                 new HexOf(
                     new Md5DigestOf(new InputOf(string.Empty))
                 ).AsString()
            );
        }

        [Fact]
        public void ChecksumOfString()
        {
            Assert.Equal(
                "ed076287532e86365e841e92bfc50d8c",
                new HexOf(
                    new Md5DigestOf(new InputOf("Hello World!"))
                ).AsString()
            );
        }

        [Fact]
        public void ChecksumFromFile()
        {
            Assert.Equal(
                "162665ab3d58424724f83f28e7a147d6",
                new HexOf(
                    new Md5DigestOf(
                        new Rersource(
                            "IO/Resources/digest-calculation.txt",
                            this.GetType()
                        )
                    )
                ).AsString()
            );
        }
    }
}
