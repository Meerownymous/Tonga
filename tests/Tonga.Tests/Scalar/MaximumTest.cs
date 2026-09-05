using System;
using Tonga.Enumerable;
using Xunit;

namespace Tonga.Tests.Scalar
{
    public sealed class MaximumTest
    {
        [Fact]
        public void RejectsEmpty()
        {
            Assert.Throws<ArgumentException>(
                () => new Maximum<int>(new Empty<int>()).Value());
        }

        [Fact]
        public void WorksWithOneElement()
        {
            int num = 10;
            Assert.Equal(
                num,
                new Maximum<int>(() => num).Value()
            );
        }

        [Fact]
        public void MaxAmongManyTest()
        {
            int num = 10;
            Assert.Equal(
                num,
                new Maximum<int>(
                    () => num,
                    () => 0,
                    () => -1,
                    () => 2
                 ).Value()
            );
        }
    }
}
