using System;
using Tonga.Scalar;
using Xunit;

namespace Tonga.Tests.Scalar
{
    public sealed class AsScalarTest
    {
        [Fact]
        public void DeliversValue()
        {
            Assert.Equal(
                "52 Degree North",
                "52 Degree North".AsScalar().Value()
            );
        }

        [Fact]
        public void SensesChanges()
        {
            var scalar =
                new AsScalar<int>(() => new Random().Next());

            Assert.NotEqual(
                scalar.Value(),
                scalar.Value()
            );
        }
    }
}
