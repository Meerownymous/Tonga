

using System;
using Xunit;
using Tonga.Scalar;

namespace Tonga.Text.Test
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
                    new LiveText("Hallo Welt!"),
                    new LiveText("Welt")
                ).Value()
            );
        }

        [Fact]
        public void FindsTextInTextIgnoreCase()
        {
            Assert.True(
                new Contains(
                    new LiveText("Hallo Welt!"),
                    new LiveText("welt"),
                    true
                ).Value()
            );
        }

        [Fact]
        public void FindsStringScalarInStringScalar()
        {
            Assert.True(
                new Contains(
                    new Live<string>("Hallo Welt!"),
                    new Live<string>("Welt")
                ).Value()
            );
        }

        [Fact]
        public void FindsITextInITextIgnoreCase()
        {
            Assert.True(
                new Contains(
                    new Live<string>("Hallo Welt!"),
                    new Live<string>("welt"),
                    new Live<StringComparison>(StringComparison.CurrentCultureIgnoreCase)
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
                    new LiveText("Hallo Welt!"),
                    new LiveText("welt")
                ).Value()
            );
        }

        [Fact]
        public void FindsNotTextInTextIgnoreCase()
        {
            Assert.False(
                new Contains(
                    new LiveText("Hallo Welt!"),
                    new LiveText("world"),
                    true
                ).Value()
            );
        }

        [Fact]
        public void FindsNotStringScalarInStringScalar()
        {
            Assert.False(
                new Contains(
                    new Live<string>("Hallo Welt!"),
                    new Live<string>("welt")
                ).Value()
            );
        }

        [Fact]
        public void FindsNotITextInITextIgnoreCase()
        {
            Assert.False(
                new Contains(
                    new Live<string>("Hallo Welt!"),
                    new Live<string>("world"),
                    new Live<StringComparison>(
                        StringComparison.CurrentCultureIgnoreCase
                    )
                ).Value()
            );
        }
    }
}
