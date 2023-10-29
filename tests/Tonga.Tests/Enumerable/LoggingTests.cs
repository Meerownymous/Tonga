

using System.Collections.Generic;
using Tonga.List;
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

            LengthOf._(
                Logging._(
                    AsList._( "A", "B" ),
                    item => result += item
                )
            ).Value();

            Assert.Equal("AB", result);
        }
    }
}
