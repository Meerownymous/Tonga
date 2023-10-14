

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
                new ItemAt<string>(
                    new Cycled<string>(
                        new ManyOf<string>(
                            "one", "two", "three"
                            )
                        ),
                    7
                ).Value()
            );
        }
    }
}
