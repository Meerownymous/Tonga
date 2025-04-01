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
                AsScalar._("52 Degree North").Value()
            );
        }

        [Fact]
        public void SensesChanges()
        {
            var scalar =
                AsScalar._(() => new Random().Next());

            Assert.NotEqual(
                scalar.Value(),
                scalar.Value()
            );
        }
    }
}
