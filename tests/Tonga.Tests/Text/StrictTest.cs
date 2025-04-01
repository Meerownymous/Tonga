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
                new Strict("not valid", "valid", "also valid").AsString()
            );
        }

        [Fact]
        public void ReturnsText()
        {
            var expected = "valid";
            Assert.Equal(
                expected,
                new Strict(expected, "not valid", "also not", "valid", "ending").AsString()
            );
        }

        [Fact]
        public void IgnoresCase()
        {
            var expected = "LargeValid";
            Assert.Equal(
                expected,
                new Strict(expected, "not valid", "also not", "LargeValid", "ending").AsString()
            );
        }

        [Fact]
        public void DoesNotIgnoresCase()
        {
            Assert.Throws<ArgumentException>(
                () =>
                new Strict("valid", false, "not valid", "also not", "largeValid", "ending").AsString()
            );
        }

        [Fact]
        public void WorksWithList()
        {
            var expected = "TextWith!§$/()?`";
            Assert.Equal(
                expected,
                new Strict(expected,
                    AsEnumerable._("NotValid", expected)
                ).AsString()
            );
        }

        [Fact]
        public void SensesChanges()
        {
            var expected = "expected";
            var counter = 0;
            var text =
                new Strict(AsText._(() => expected),
                    new AsEnumerable<IText>(
                        AsText._(() => counter++.ToString()),
                        AsText._(expected)
                    )
                );
            text.AsString();
            text.AsString();
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
                new Strict(AsText._(expected), false,
                    AsEnumerable._(
                        AsText._("Expected"),
                        AsText._("Not Valid"),
                        AsText._(expected)
                    )
                ).AsString()
            );
        }

        [Fact]
        public void IgnoresCaseList()
        {
            var expected = "expected";
            Assert.Equal(
                expected,
                new Strict(
                    expected, true,
                    AsEnumerable._(
                        "Not Valid",
                        "As well not valid",
                        "ExpEcteD"
                    )
                ).AsString()
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
                ).AsString()
            );
        }
    }
}
