

using System;
using System.Collections.Generic;
using Xunit;

namespace Tonga.Map.Tests
{
    public class FallbackMapTests
    {
        [Fact]
        public void TryGetValueWithExistingKey()
        {
            var map = new FallbackMap<int, int>(new Dictionary<int, int> { { 7, 42 } }, i => { throw new Exception("dont call fallback on tryGetValue"); });
            int outValue;
            Assert.True(map.TryGetValue(7, out outValue));
            Assert.Equal(42, outValue);
        }

        [Fact]
        public void TryGetValueWithMissingKey()
        {
            var map = new FallbackMap<int, int>(new Dictionary<int, int> { { 7, 42 } }, i => { throw new Exception("dont call fallback on tryGetValue"); });
            int outValue;
            Assert.False(map.TryGetValue(0, out outValue));
        }

        [Fact]
        public void GetsValueWithExistingKey()
        {
            var map = new FallbackMap<int, int>(new Dictionary<int, int> { { 7, 42 } }, i => { throw new Exception("dont call fallback when value exists"); });
            var outValue = map[7];
            Assert.Equal(42, outValue);
        }

        [Fact]
        public void GetsValueWithMissingKey()
        {
            var map = new FallbackMap<int, int>(new Dictionary<int, int> { { 7, 42 } }, key => key * 2);
            var outValue = map[2];
            Assert.Equal(4, outValue);
        }

        [Fact]
        public void GetsValueFromFallbackMap()
        {
            var map = new FallbackMap<int, int>(
                new Dictionary<int, int> { { 7, 42 } },
                new Dictionary<int, int> { { 13, 37 } }
            );

            var outValue = map[13];
            Assert.Equal(37, outValue);
        }

        [Fact]
        public void DoesNotGetValueWhenAlsoMissingInFallbackMap()
        {
            var map = new FallbackMap<int, int>(
                new Dictionary<int, int> { { 7, 42 } },
                new Dictionary<int, int> { { 13, 37 } }
            );

            Assert.Throws<KeyNotFoundException>(() => map[666]);
        }
    }
}
