

using System;
using System.Globalization;
using Xunit;
using Tonga.Text;

namespace Tonga.Primitives.Tests
{
    public sealed class DoubleOfTests
    {
        [Fact]
        public void ConvertsDouble()
        {
            var piStr = Math.PI.ToString("G17");
            var piFromStr = new DoubleOf(piStr, CultureInfo.CurrentCulture).Value();
            var piStr2 = piFromStr.ToString("G17");

            Assert.True(piStr == piStr2);
        }
    }
}
