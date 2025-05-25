using System;
using Tonga.Scalar;
using Xunit;

namespace Tonga.Tests.Scalar
{
    public sealed class RetryingTest
    {
        [Fact]
        public void RunsScalarMultipleTimes()
        {
            Assert.Equal(
                0,
                Retrying._(
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
