using System;
using Tonga.Map;
using Xunit;

namespace Tonga.Tests.Map;

public class MapEnvelopeTest
{
    [Fact]
    public void GetsValueByExistingKey()
    {
        var map = new NonAbstractIntEnvelope((7, 42).AsMap());
        var outValue = map[7];
        Assert.Equal(42, outValue);
    }

    [Fact]
    public void RejectsGettingMissingKey()
    {
        var map = new NonAbstractIntEnvelope((7, 42).AsMap());

        var ex = Assert.Throws<ArgumentException>(() => map[0]);
        Assert.StartsWith("The given key '0' was not present in the", ex.Message);
    }

    private class NonAbstractIntEnvelope : MapEnvelope<int, int>
    {
        public NonAbstractIntEnvelope(IMap<int, int> map) : base(map)
        { }
    }
}
