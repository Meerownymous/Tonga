

using System;
using Xunit;
using Tonga.Enumerable;

namespace Tonga.Text.Test
{
    public sealed class ParagraphTest
    {
        [Fact]
        public void BuildsWithParamsString()
        {
            var p = new Paragraph("a", "b", "c");
            Assert.Equal("a\nb\nc".Replace("\n", Environment.NewLine), p.AsString());
        }

        [Fact]
        public void BuildsWithITextEnumerable()
        {
            var p = new Paragraph(
                new AsEnumerable<IText>(
                    AsText._("a"),
                    AsText._("b"),
                    AsText._("c")
                ));
            Assert.Equal("a\nb\nc".Replace("\n", Environment.NewLine), p.AsString());
        }

        [Fact]
        public void HeadArrayTailStrings()
        {
            var p = new Paragraph(
                "Hello", "World",
                new string[] { "I", "was", "here" },
                "foo", "bar"
            );
            Assert.Equal("Hello\nWorld\nI\nwas\nhere\nfoo\nbar".Replace("\n", Environment.NewLine), p.AsString());
        }

        [Fact]
        public void HeadsAndTailsMixedITextStrings()
        {
            var p = new Paragraph(
                AsText._("Hello"), AsText._("World"),
                new string[] { "I", "was", "here" },
                AsText._("foo"), AsText._("bar")
            );
            Assert.Equal("Hello\nWorld\nI\nwas\nhere\nfoo\nbar".Replace("\n", Environment.NewLine), p.AsString());
        }
    }
}
