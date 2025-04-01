using Tonga.Enumerable;
using Xunit;

namespace Tonga.Tests.Enumerable
{
    public class StickyTests
    {
        [Fact]
        public void ChangesOfSourceAreIgnored()
        {
            var content = 0;
            var items =
                new Sticky<string>(
                    new Repeated<string>(
                        () =>
                        {
                            content++;
                            return content.ToString();
                        },
                        1
                    )
                );

            Assert.Equal(
                new global::Tonga.Text.Joined(" ", items).AsString(),
                new global::Tonga.Text.Joined(" ", items).AsString()
            );

        }
    }
}

