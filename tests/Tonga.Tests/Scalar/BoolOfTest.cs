

using System.IO;
using Xunit;
using Tonga.Text;

namespace Tonga.Scalar.Tests
{
    public sealed class BoolOfTest
    {
        [Fact]
        public void TrueTest()
        {
            Assert.True(
                new BoolOf("true").Value() == true,
                "Can't parse 'true' string");
        }

        [Fact]
        public void FalseTest()
        {
            Assert.True(
                new BoolOf("false").Value() == false,
                "Can't parse 'false' string"
            );
        }

        [Fact]
        public void IsFalseIfTextDoesNotRepresentABoolean()
        {
            Assert.Throws<IOException>(
                () => new BoolOf("abc").Value());
        }
    }
}
