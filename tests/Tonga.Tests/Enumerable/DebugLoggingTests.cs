using Tonga.Enumerable;
using Tonga.List;
using Xunit;

namespace Tonga.Tests.Enumerable
{
    public sealed class DebugLoggingTests
    {
        [Fact]
        public void DumpsItemsOnRead()
        {
            string result = string.Empty;
            new AsList<string>( "A", "B" )
                .AsDebugLogging(item => result += item);

            Assert.Equal("AB", result);
        }
    }
}
