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
            Assert.Equal(
                "Hi!",
                new Replaced(
                    AsText._("Hello!"),
                    "ello", "i"
                ).Str()
            );
        }

        [Fact]
        public void NotReplaceTextWhenSubstringNotFound()
        {
            String text = "HelloAgain!";
            Assert.Equal(
                text,
                new Replaced(
                    AsText._(text),
                    "xyz", "i"
                ).Str()
            );
        }

        [Fact]
        public void ReplacesAllOccurrences()
        {
            Assert.Equal(
                "one dog, two dogs, three dogs",
                new Replaced(
                    AsText._("one cat, two cats, three cats"),
                    "cat",
                    "dog"
                ).Str()
            );
        }
    }
}
