using System;
using Xunit;

namespace Tonga.Text.Tests
{
    public sealed class BlankTest
    {
        [Fact]
        public void IsBlank()
        {
            Assert.Equal(
                " ",
                new Blank().AsString()
            );
        }
    }
}

