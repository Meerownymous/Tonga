

using System.IO;
using Tonga.IO;

namespace Tonga.Tests.IO;

internal sealed class SlowConduit(ConduitMorph origin) : ConduitEnvelope(() =>
    new SlowInputStream(origin.Stream())
)
{
    internal SlowConduit(long size) : this((int)size)
    { }


    internal SlowConduit(int size) : this(new MemoryStream(new byte[size]))
    { }
}

