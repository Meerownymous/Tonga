using Tonga.Pipe;
using Xunit;

namespace Tonga.Tests.Pipe
{
    public sealed class AsPipeTest
    {
        [Fact]
        public void Vields()
        {
            Assert.Equal(
                1,
                AsPipe._<int, int>(
                    _ => 1
                ).Yield(2)
            );
        }
    }
}
