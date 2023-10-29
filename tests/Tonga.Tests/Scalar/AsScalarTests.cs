

using Xunit;

namespace Tonga.Scalar.Tests
{
    public sealed class AsScalarTests
    {
        [Fact]
        public void ReloadsFunc()
        {
            var counts = 0;
            var live = AsScalar._(() =>
            {
                ++counts;
                return true;
            });
            live.Value();
            live.Value();
            Assert.Equal(2, counts);
        }
    }
}
