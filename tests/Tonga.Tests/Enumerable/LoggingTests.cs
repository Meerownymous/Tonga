

using System.Collections.Generic;
using Tonga.Scalar;
using Xunit;

namespace Tonga.Enumerable.Test
{
    public sealed class LoggingTests
    {
        [Fact]
        public void DumpsItemsOnRead()
        {
            string result = string.Empty;

            new LengthOf(
                new Logging<string>(
                    new List<string>() { "A", "B" },
                    item => result += item
                )
            ).Value();

            Assert.Equal("AB", result);
        }
    }
}
