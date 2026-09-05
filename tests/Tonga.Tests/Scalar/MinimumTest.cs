using System;
using Tonga.Enumerable;
using Xunit;

namespace Tonga.Tests.Scalar
{
    public sealed class MinimumTest
    {
        [Fact]
        public void MinAmongEmptyTest()
        {
            Assert.Throws<ArgumentException>(
                () => new Minimum<int>(new Empty<int>()).Value()
            );
        }

        [Fact]
        public void MinAmongOneTest()
        {
            int num = 10;
            Assert.True(
                new Minimum<int>(() => num).Value() == num,
                "Can't find the smaller among one");
        }

        [Fact]
        public void MinAmongManyTest()
        {
            int num = -1;
            Assert.True(
                new Minimum<int>(
                    () => 1,
                    () => 0,
                    () => num,
                    () => 2
                 ).Value() == num);
        }
    }
}
