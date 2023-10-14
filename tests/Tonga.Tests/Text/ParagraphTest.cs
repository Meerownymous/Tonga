

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
                new Transit<IText>(
                    new LiveText("a"),
                    new LiveText("b"),
                    new LiveText("c")
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
                new LiveText("Hello"), new LiveText("World"),
                new string[] { "I", "was", "here" },
                new LiveText("foo"), new LiveText("bar")
            );
            Assert.Equal("Hello\nWorld\nI\nwas\nhere\nfoo\nbar".Replace("\n", Environment.NewLine), p.AsString());
        }
    }
}
