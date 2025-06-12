using System;
using Tonga.Pipe;
using Xunit;

namespace Tonga.Tests.Pipe
{
    public sealed class RetryTest
    {
        [Fact]
        public void RunsFuncMultipleTimes()
        {
            Assert.Equal(
                0,
                new Retry<bool, int>(
                    _ =>
                    {
                        if (new Random().NextDouble() > 0.3d)
                        {
                            throw new ArgumentException("May happen");
                        }
                        return 0;
                    },
                    int.MaxValue
                ).Yield(true)
            );
        }
    }
}
