

using System;
using Xunit;

namespace Tonga.Text.Test
{
    public sealed class ReplacedTest
    {
        [Fact]
        public void ReplaceText()
        {
            Assert.True(
                new Replaced(
                    new LiveText("Hello!"),
                    "ello", "i"
                ).AsString() == "Hi!",
                "Can't replace a text");
        }

        [Fact]
        public void NotReplaceTextWhenSubstringNotFound()
        {
            String text = "HelloAgain!";
            Assert.True(
                new Replaced(
                    new LiveText(text),
                    "xyz", "i"
                ).AsString() == text,
                "Replace a text abnormally");
        }

        [Fact]
        public void ReplacesAllOccurrences()
        {
            Assert.True(
                new Replaced(
                    new LiveText("one cat, two cats, three cats"),
                    "cat",
                    "dog"
                ).AsString() == "one dog, two dogs, three dogs",
                "Can't replace a text with multiple needle occurrences");
        }
    }
}
