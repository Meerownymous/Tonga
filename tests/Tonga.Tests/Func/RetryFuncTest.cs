

using System;
using Xunit;

namespace Tonga.Func.Tests
{
    public sealed class RetryFuncTest
    {
        [Fact]
        public void RunsFuncMultipleTimes()
        {
            Assert.True(
            new RetryFunc<bool, int>(
                input =>
                {
                    if (new Random().NextDouble() > 0.3d)
                    {
                        throw new ArgumentException("May happen");
                    }
                    return 0;
                },
                int.MaxValue
            ).Invoke(true) == 0,
            "cannot retry function"
            );
        }
    }
}
