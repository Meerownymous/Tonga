using System;
using Tonga.Enumerable;
using Tonga.Scalar;
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

        [Fact]
        public void FindsMaximumAmongScalarEnumerable()
        {
            Assert.Equal(
                7,
                new Maximum<int>(
                    (1.AsScalar(), 7.AsScalar(), 3.AsScalar()).AsEnumerable()
                ).Value()
            );
        }

        [Fact]
        public void FindsMaximumAsSmart()
        {
            Assert.Equal(
                7,
                (1, 7, 3).AsEnumerable().Maximum().Value()
            );
        }

        [Fact]
        public void FindsMaximumAmongFunctionsAsSmart()
        {
            Assert.Equal(
                7,
                new Func<int>[] { () => 1, () => 7, () => 3 }
                    .Maximum()
                    .Value()
            );
        }
    }
}
