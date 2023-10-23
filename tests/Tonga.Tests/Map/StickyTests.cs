using System;
using Xunit;

namespace Tonga.Map.Tests
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
    }
}

