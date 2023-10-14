

using System;
using Xunit;

namespace Tonga.Scalar.Tests
{
    /**
     * Test case for {@link RetryScalar}.
     *
     * @author Yegor Bugayenko (yegor256@gmail.com)
     * @version $Id$
     * @since 0.9
     * @checkstyle JavadocMethodCheck (500 lines)
     */
    public sealed class RetryTest
    {
        [Fact]
        public void RunsScalarMultipleTimes()
        {
            Assert.True(
                new Retry<int>(
                    () =>
                    {
                        if (new Random().NextDouble() > 0.3d)
                        {
                            throw new ArgumentException("May happen");
                        }
                        return 0;
                    },
                int.MaxValue
            ).Value() == 0);
        }

    }
}
