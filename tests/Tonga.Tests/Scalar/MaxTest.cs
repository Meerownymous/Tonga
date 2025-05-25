using System;
using Tonga.Enumerable;
using Tonga.Scalar;
using Xunit;

namespace Tonga.Tests.Scalar
{
    public sealed class MaxTest
    {
        [Fact]
        public void MaxAmongEmptyTest()
        {
            Assert.Throws<ArgumentException>(
                () => new Max<int>(new None<int>()).Value());
        }

        [Fact]
        public void MaxAmongOneTest()
        {
            int num = 10;
            Assert.True(
                new Max<int>(() => num).Value() == num,
                "Can't find the greater among one"
            );
        }

        [Fact]
        public void MaxAmongManyTest()
        {
            int num = 10;
            Assert.True(
                new Max<int>(
                    () => num,
                    () => 0,
                    () => -1,
                    () => 2
                 ).Value() == num,
                "Can't find the greater among many");
        }
    }
}
