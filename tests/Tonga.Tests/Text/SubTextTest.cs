using Tonga.Text;
using Xunit;

namespace Tonga.Tests.Text
{
    public sealed class SubTextText
    {
        [Fact]
        public void CutString()
        {
            Assert.Equal(
                "the_end",
                new SubText(
                    "this_is:the_end",
                    8
                ).AsString()
            );
        }

        [Fact]
        public void CutStringwithLength()
        {
            Assert.Equal(
                "the",
                new SubText(
                    "this_is:the_end",
                    8,
                    3
                ).AsString()
            );
        }

        [Fact]
        public void CutIText()
        {
            Assert.Equal(
                "the_end",
                new SubText(
                    AsText._("this_is:the_end"),
                    8
                ).AsString()
            );
        }

        [Fact]
        public void CutITextwithLength()
        {
            Assert.Equal(
                "the",
                new SubText(
                    AsText._("this_is:the_end"),
                    8,
                    3
                ).AsString()
            );
        }
    }
}
