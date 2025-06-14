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
            Assert.Equal(
                expected,
                new Strict(expected, "not valid", "also not", "valid", "ending").Str()
            );
        }

        [Fact]
        public void IgnoresCase()
        {
            var expected = "LargeValid";
            Assert.Equal(
                expected,
                new Strict(expected, "not valid", "also not", "LargeValid", "ending").Str()
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
            Assert.Equal(
                expected,
                new Strict(expected,
                    ("NotValid", expected).AsEnumerable()
                ).Str()
            );
        }

        [Fact]
        public void SensesChanges()
        {
            var expected = "expected";
            var counter = 0;
            var text =
                new Strict(
                    new AsText(() => expected),
                    new AsEnumerable<IText>(
                        new AsText(() => counter++.ToString()),
                        new AsText(expected)
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
            Assert.Equal(
                expected,
                new Strict(
                    new AsText(expected),
                    false,
                    (
                        "Expected",
                        "Not Valid",
                        expected
                    ).AsEnumerable()
                    .AsMapped(t => t.AsText())
                ).Str()
            );
        }

        [Fact]
        public void IgnoresCaseList()
        {
            var expected = "expected";
            Assert.Equal(
                expected,
                new Strict(
                    expected,
                    true,
                    (
                        "Not Valid",
                        "As well not valid",
                        "ExpEcteD"
                    ).AsEnumerable()
                ).Str()
            );
        }

        [Fact]
        public void IgnoresCaseByDefault()
        {
            var expected = "expected";
            Assert.Equal(
                expected,
                new Strict(
                    expected,
                    "Not Valid",
                    "As well not valid",
                    "ExpEcteD"
                ).Str()
            );
        }
    }
}
