

using System.IO;
using Xunit;

namespace Tonga.Text.Test
{
    public sealed class NotNullTests
    {
        [Fact]
        public void NotNull()
        {
            IText s = null;
            Assert.Throws<IOException>(
                () => new NotNull(s).AsString()
            );
        }
    }
}
