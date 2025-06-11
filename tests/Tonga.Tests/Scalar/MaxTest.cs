using System;
using Tonga.Enumerable;
using Xunit;

namespace Tonga.Tests.Scalar
{
    public sealed class MaxTest
    {
        [Fact]
        public void RejectsEmpty()
        {
            Assert.Throws<ArgumentException>(
                () => new Max<int>(new None<int>()).Value());
        }

        [Fact]
        public void WorksWithOneElement()
        {
            int num = 10;
            Assert.Equal(
                num,
                new Max<int>(() => num).Value()
            );
        }

        [Fact]
        public void MaxAmongManyTest()
        {
            int num = 10;
            Assert.Equal(
                num,
                new Max<int>(
                    () => num,
                    () => 0,
                    () => -1,
                    () => 2
                 ).Value()
            );
        }
    }
}
