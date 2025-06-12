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
                new AsPipe<int, int>(_ => 1)
                    .Yield(2)
            );
        }
    }
}
