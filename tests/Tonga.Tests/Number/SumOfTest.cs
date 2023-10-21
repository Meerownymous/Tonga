

using Xunit;
using Tonga.Enumerable;

namespace Tonga.Number.Tests
{
    public sealed class SumOfTest
    {
        [Fact]
        public void CalculatesSumOfNumbers()
        {
            Assert.True(
                new SumOf(
                    1.5F, 2.5F, 3.5F
                ).AsFloat() == 7.5F);
        }

        [Fact]
        public void CalculatesSumOfEnumerables()
        {
            Assert.True(
                new SumOf(
                    AsEnumerable._(1.5F, 2.5F, 3.5F)
                ).AsFloat() == 7.5F
            );
        }
    }
}
