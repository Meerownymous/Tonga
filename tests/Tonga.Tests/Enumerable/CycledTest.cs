

using Tonga.Scalar;
using Xunit;

namespace Tonga.Enumerable.Test
{
    public sealed class CycledTest
    {
        [Fact]
        public void RepeatsEnumerable()
        {
            Assert.Equal(
                "two",
                ItemAt._(
                    Cycled._(
                        AsEnumerable._("one", "two", "three")
                    ),
                    7
                ).Value()
            );
        }
    }
}
