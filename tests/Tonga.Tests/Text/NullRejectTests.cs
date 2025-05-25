using System.IO;
using Tonga.Text;
using Xunit;

namespace Tonga.Tests.Text
{
    public sealed class NullRejectTests
    {
        [Fact]
        public void NotNull()
        {
            IText s = null;
            Assert.Throws<IOException>(
                () => new NullReject(s).Str()
            );
        }
    }
}
