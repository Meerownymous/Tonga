using System;
using Tonga.Scalar;
using Tonga.Text;
using Xunit;

namespace Tonga.Tests.Text
{
    public sealed class ContainsTest
    {
        [Fact]
        public void FindsStringInString()
        {
            Assert.True(
                new Contains(
                    "Hallo Welt!",
                    "Welt"
                ).IsTrue()
            );
        }

        [Fact]
        public void FindsStringInStringIgnoreCase()
        {
            Assert.True(
                new Contains(
                    "Hallo Welt!",
                    "welt",
                    true
                ).IsTrue()
            );
        }

        [Fact]
        public void FindsTextInText()
        {
            Assert.True(
                new Contains(
                    "Hallo Welt!".AsText(),
                    "Welt".AsText()
                ).IsTrue()
            );
        }

        [Fact]
        public void FindsTextInTextIgnoreCase()
        {
            Assert.True(
                new Contains(
                    "Hallo Welt!".AsText(),
                    "welt".AsText(),
                    true
                ).IsTrue()
            );
        }

        [Fact]
        public void FindsStringScalarInStringScalar()
        {
            Assert.True(
                new Contains(
                    "Hallo Welt!".AsScalar(),
                    "Welt".AsScalar()
                ).IsTrue()
            );
        }

        [Fact]
        public void FindsITextInITextIgnoreCase()
        {
            Assert.True(
                new Contains(
                    "Hallo Welt!".AsText(),
                    "welt".AsText(),
                    StringComparison.CurrentCultureIgnoreCase
                ).IsTrue()
            );
        }

        [Fact]
        public void FindsNotStringInString()
        {
            Assert.False(
                new Contains(
                    "Hallo Welt!",
                    "welt"
                ).IsTrue()
            );
        }

        [Fact]
        public void FindsNotStringInStringIgnoreCase()
        {
            Assert.False(
                new Contains(
                    "Hallo Welt!",
                    "world",
                    true
                ).IsTrue()
            );
        }

        [Fact]
        public void FindsNotTextInText()
        {
            Assert.False(
                new Contains(
                    "Hallo Welt!".AsText(),
                    "welt".AsText()
                ).IsTrue()
            );
        }

        [Fact]
        public void FindsNotTextInTextIgnoreCase()
        {
            Assert.False(
                new Contains(
                    "Hallo Welt!".AsText(),
                    "world".AsText(),
                    true
                ).IsTrue()
            );
        }

        [Fact]
        public void FindsNotStringScalarInStringScalar()
        {
            Assert.False(
                new Contains(
                    "Hallo Welt!".AsScalar(),
                    "welt".AsScalar()
                ).IsTrue()
            );
        }

        [Fact]
        public void FindsNotITextInITextIgnoreCase()
        {
            Assert.False(
                new Contains(
                    "Hallo Welt!".AsScalar(),
                    "world".AsScalar(),
                    StringComparison.CurrentCultureIgnoreCase
                ).IsTrue()
            );
        }
    }
}
