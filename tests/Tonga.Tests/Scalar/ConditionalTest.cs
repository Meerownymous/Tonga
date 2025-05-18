using Tonga.Fact;
using Tonga.Scalar;
using Xunit;

namespace Tonga.Tests.Scalar
{
    public sealed class ConditionalTest
    {

        [Fact]
        public void ConditionTrue()
        {
            Assert.True(
                new Conditional<int>(
                    new True(),
                    6,
                    16
                ).Value() == 6);
        }

        [Fact]
        public void ConditionFalse()
        {
            Assert.True(
                new Conditional<int>(
                    new False(),
                    6,
                    16
                ).Value() == 16);
        }

        [Fact]
        public void ConditionBoolean()
        {
            Assert.True(
                new Conditional<int>(
                    true,
                    6,
                    16
                ).Value() == 6
            );
        }

        [Fact]
        public void ConditionFunc()
        {
            var value = 5;
            Assert.True(
                new Conditional<int>(
                    () => value > 3,
                    () => 8,
                    () => 2
                ).Value() == 8
            );
        }
    }
}
