using System;
using System.IO;

namespace Tonga.IO;

public abstract class ConduitEnvelope(Func<Stream> stream) : IConduit
{
    public Stream Stream() => stream();
}
