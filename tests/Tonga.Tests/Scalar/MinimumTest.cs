using System;
using Tonga.Enumerable;
using Tonga.Scalar;
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

        [Fact]
        public void FindsMinimumAmongScalarEnumerable()
        {
            Assert.Equal(
                1,
                new Minimum<int>(
                    (1.AsScalar(), 7.AsScalar(), 3.AsScalar()).AsEnumerable()
                ).Value()
            );
        }

        [Fact]
        public void FindsMinimumAsSmart()
        {
            Assert.Equal(
                1,
                (1, 7, 3).AsEnumerable().Minimum().Value()
            );
        }

        [Fact]
        public void FindsMinimumAmongFunctionsAsSmart()
        {
            Assert.Equal(
                1,
                new Func<int>[] { () => 1, () => 7, () => 3 }
                    .Minimum()
                    .Value()
            );
        }
    }
}
