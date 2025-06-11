using System;
using Tonga.Scalar;
using Xunit;

namespace Tonga.Tests.Scalar
{
    public sealed class RetryTest
    {
        [Fact]
        public void RunsScalarMultipleTimes()
        {
            Assert.Equal(
                0,
                new RetryOnError<int>(
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
