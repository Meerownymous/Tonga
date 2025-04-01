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
                ).Value()
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
                ).Value()
            );
        }

        [Fact]
        public void FindsTextInText()
        {
            Assert.True(
                new Contains(
                    AsText._("Hallo Welt!"),
                    AsText._("Welt")
                ).Value()
            );
        }

        [Fact]
        public void FindsTextInTextIgnoreCase()
        {
            Assert.True(
                new Contains(
                    AsText._("Hallo Welt!"),
                    AsText._("welt"),
                    true
                ).Value()
            );
        }

        [Fact]
        public void FindsStringScalarInStringScalar()
        {
            Assert.True(
                new Contains(
                    AsScalar._("Hallo Welt!"),
                    AsScalar._("Welt")
                ).Value()
            );
        }

        [Fact]
        public void FindsITextInITextIgnoreCase()
        {
            Assert.True(
                new Contains(
                    AsScalar._("Hallo Welt!"),
                    AsScalar._("welt"),
                    AsScalar._(StringComparison.CurrentCultureIgnoreCase)
                ).Value()
            );
        }

        [Fact]
        public void FindsNotStringInString()
        {
            Assert.False(
                new Contains(
                    "Hallo Welt!",
                    "welt"
                ).Value()
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
                ).Value()
            );
        }

        [Fact]
        public void FindsNotTextInText()
        {
            Assert.False(
                new Contains(
                    AsText._("Hallo Welt!"),
                    AsText._("welt")
                ).Value()
            );
        }

        [Fact]
        public void FindsNotTextInTextIgnoreCase()
        {
            Assert.False(
                new Contains(
                    AsText._("Hallo Welt!"),
                    AsText._("world"),
                    true
                ).Value()
            );
        }

        [Fact]
        public void FindsNotStringScalarInStringScalar()
        {
            Assert.False(
                new Contains(
                    AsScalar._("Hallo Welt!"),
                    AsScalar._("welt")
                ).Value()
            );
        }

        [Fact]
        public void FindsNotITextInITextIgnoreCase()
        {
            Assert.False(
                new Contains(
                    AsScalar._("Hallo Welt!"),
                    AsScalar._("world"),
                    AsScalar._(
                        StringComparison.CurrentCultureIgnoreCase
                    )
                ).Value()
            );
        }
    }
}
