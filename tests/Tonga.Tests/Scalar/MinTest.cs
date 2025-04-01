using System;
using Tonga.Enumerable;
using Xunit;

namespace Tonga.Tests.Scalar
{
    public sealed class MinTest
    {
        [Fact]
        public void MinAmongEmptyTest()
        {
            Assert.Throws<ArgumentException>(
                () => new Min<int>(new None<int>()).Value()
            );
        }

        [Fact]
        public void MinAmongOneTest()
        {
            int num = 10;
            Assert.True(
                new Min<int>(() => num).Value() == num,
                "Can't find the smaller among one");
        }

        [Fact]
        public void MinAmongManyTest()
        {
            int num = -1;
            Assert.True(
                new Min<int>(
                    () => 1,
                    () => 0,
                    () => num,
                    () => 2
                 ).Value() == num);
        }
    }
}
