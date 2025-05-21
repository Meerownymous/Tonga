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
            var piStr = Math.PI.ToString("G17");
            Assert.Equal(
                piStr,
                new DoubleOf(piStr, CultureInfo.CurrentCulture)
                    .Value()
                    .ToString("G17")
            );
        }
    }
}
