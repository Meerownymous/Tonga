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
                "Hello!"
                    .AsText()
                    .AsReplaced("ello", "i")
                    .Str()
            );
        }

        [Fact]
        public void NotReplaceTextWhenSubstringNotFound()
        {
            String text = "HelloAgain!";
            Assert.Equal(
                text,
                text.AsText().AsReplaced("xyz", "i").Str()
            );
        }

        [Fact]
        public void ReplacesAllOccurrences()
        {
            Assert.Equal(
                "one dog, two dogs, three dogs",
                "one cat, two cats, three cats".AsText().AsReplaced("cat","dog").Str()
            );
        }
    }
}
