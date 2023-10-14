

using Xunit;
using Tonga.Enumerable;
using System;

namespace Tonga.Scalar.Tests
{
    public sealed class MinTest
    {
        [Fact]
        public void MinAmongEmptyTest()
        {
            Assert.Throws<ArgumentException>(
                () => new Min<int>(new ManyOf<int>()).Value());
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
