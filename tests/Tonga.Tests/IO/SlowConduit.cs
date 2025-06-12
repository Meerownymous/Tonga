

using System.IO;
using Tonga.IO;

namespace Tonga.Tests.IO;

internal sealed class SlowConduit(IConduit origin) : IConduit
{
    internal SlowConduit(long size) : this((int)size)
    { }


    internal SlowConduit(int size) : this(
        new MemoryStream(new byte[size])
            .AsConduit()
    )
    { }

    public Stream Stream() => new SlowInputStream(origin.Stream());
}

