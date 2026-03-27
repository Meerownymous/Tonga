using System;
using Tonga.Enumerable;
using Tonga.Text;
using Xunit;

namespace Tonga.Tests.Text
{
    public sealed class StrictTest
    {
        [Fact]
        public void Throws()
        {
            Assert.Throws<ArgumentException>(() =>
                new Strict("not valid", "valid", "also valid").Str()
            );
        }

        [Fact]
        public void ReturnsText()
        {
            var expected = "valid";
            AssertText.Equal(
                expected,
                new Strict(expected, "not valid", "also not", "valid", "ending")
            );
        }

        [Fact]
        public void IgnoresCase()
        {
            var expected = "LargeValid";
            AssertText.Equal(
                expected,
                new Strict(expected, "not valid", "also not", "LargeValid", "ending")
            );
        }

        [Fact]
        public void DoesNotIgnoresCase()
        {
            Assert.Throws<ArgumentException>(
                () =>
                new Strict("valid", false, "not valid", "also not", "largeValid", "ending").Str()
            );
        }

        [Fact]
        public void WorksWithList()
        {
            var expected = "TextWith!§$/()?`";
            AssertText.Equal(
                expected,
                new Strict(expected, "NotValid", expected)
            );
        }

        [Fact]
        public void SensesChanges()
        {
            var expected = "expected";
            var counter = 0;
            var text =
                new Strict(
                    new TextMorph(() => expected),
                    new AsEnumerable<IText>(
                        new TextMorph(() => counter++.ToString()),
                        new TextMorph(expected)
                    )
                );
            text.Str();
            text.Str();
            Assert.NotEqual(
                1,
                counter
            );
        }

        [Fact]
        public void NotIgnoresCaseList()
        {
            var expected = "expected";
            AssertText.Equal(
                expected,
                new Strict(
                    new TextMorph(expected),
                    false,
                    (
                        "Expected",
                        "Not Valid",
                        expected
                    ).AsEnumerable()
                    .AsMapped(t => new TextMorph(t))
                )
            );
        }

        [Fact]
        public void IgnoresCaseList()
        {
            var expected = "expected";
            AssertText.Equal(
                expected,
                new Strict(
                    expected,
                    true,
                    "Not Valid",
                    "As well not valid",
                    "ExpEcteD"
                )
            );
        }

        [Fact]
        public void IgnoresCaseByDefault()
        {
            var expected = "expected";
            AssertText.Equal(
                expected,
                new Strict(
                    expected,
                    "Not Valid",
                    "As well not valid",
                    "ExpEcteD"
                )
            );
        }
    }
}
