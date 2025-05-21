using System;
using System.Globalization;
using Tonga.Text;
using Xunit;

namespace Tonga.Tests.Primitives
{
    public sealed class DoubleOfTests
    {
        [Fact]
        public void ConvertsDouble()
        {
            Assert.Equal(
                Math.PI.ToString("G17"),
                new DoubleOf(
                    Math.PI.ToString("G17"),
                    CultureInfo.CurrentCulture
                ).Value()
                .ToString(CultureInfo.InvariantCulture)
            );
        }
    }
}
