

using Xunit;
using Tonga.Text;

namespace Tonga.IO.Tests
{
    public sealed class Sha256DigestOfTest
    {
        [Fact]
        public void ChecksumOfEmptyString()
        {
            Assert.Equal(
                 "e3b0c44298fc1c149afbf4c8996fb92427ae41e4649b934ca495991b7852b855",
                 new HexOf(
                     new Sha256DigestOf(new InputOf(string.Empty))
                 ).AsString()
            );
        }

        [Fact]
        public void ChecksumOfString()
        {
            Assert.Equal(
                "7f83b1657ff1fc53b92dc18148a1d65dfc2d4b1fa3d677284addd200126d9069",
                new HexOf(
                    new Sha256DigestOf(new InputOf("Hello World!"))
                ).AsString()
            );
        }

        [Fact]
        public void ChecksumFromFile()
        {
            Assert.Equal(
                "c94451bd1476a3728669de11e22c645906d806e63a95c5797de1f3e84f126a3e",
                new HexOf(
                    new Sha256DigestOf(
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
