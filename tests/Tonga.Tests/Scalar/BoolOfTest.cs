using System.IO;
using Tonga.Primitives;
using Tonga.Text;
using Xunit;

namespace Tonga.Tests.Scalar
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
