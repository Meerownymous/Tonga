using Tonga.Map;
using Xunit;

namespace Tonga.Tests.Map
{
    public sealed class StickyTests
    {
        [Fact]
        public void RetrievesValueFromSource()
        {
            Assert.Equal(
                "one",
                Sticky._(
                    AsMap._(
                        AsPair._(1, "one")
                    )
                )[1]
            );
        }

        [Fact]
        public void RemembersValue()
        {
            var map =
                Sticky._(
                    AsMap._(
                        OneTimePair._(
                            AsPair._(1, "one")
                        )
                    )
                );

            _ = map[1];

            Assert.Equal(
                "one",
                map[1]
            );
        }
    }
}

