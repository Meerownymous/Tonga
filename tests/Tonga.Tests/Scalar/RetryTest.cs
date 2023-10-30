

using System;
using Xunit;

namespace Tonga.Scalar.Tests
{
    public sealed class RetryTest
    {
        [Fact]
        public void RunsScalarMultipleTimes()
        {
            Assert.Equal(
                0,
                Retry._(
                    () =>
                    {
                        if (new Random().NextDouble() > 0.3d)
                        {
                            throw new ArgumentException("May happen");
                        }
                        return 0;
                    },
                    int.MaxValue
                ).Value()
            );
        }

    }
}
