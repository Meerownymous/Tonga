using Tonga.Enumerable;
using Tonga.List;
using Tonga.Scalar;
using Xunit;

namespace Tonga.Tests.Enumerable
{
    public sealed class LoggingTests
    {
        [Fact]
        public void DumpsItemsOnRead()
        {
            string result = string.Empty;

            Length._(
                Logging._(
                    AsList._( "A", "B" ),
                    item => result += item
                )
            ).Value();

            Assert.Equal("AB", result);
        }
    }
}
