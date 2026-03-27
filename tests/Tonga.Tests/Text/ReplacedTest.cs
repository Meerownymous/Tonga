using System;
using Tonga.Text;
using Xunit;

namespace Tonga.Tests.Text
{
    public sealed class ReplacedTest
    {
        [Fact]
        public void ReplaceText()
        {
            AssertText.Equal(
                "Hi!",
                new Replaced("Hello!", "ello", "i")
            );
        }

        [Fact]
        public void NotReplaceTextWhenSubstringNotFound()
        {
            String text = "HelloAgain!";
            AssertText.Equal(
                text,
                new Replaced(text, "xyz", "i")
            );
        }

        [Fact]
        public void ReplacesAllOccurrences()
        {
            AssertText.Equal(
                "one dog, two dogs, three dogs",
                new Replaced("one cat, two cats, three cats", "cat","dog")
            );
        }
    }
}
