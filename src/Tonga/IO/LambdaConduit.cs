using System;
using System.IO;

namespace Tonga.IO;

public sealed class LambdaConduit(Func<Stream> stream) : IConduit
{
    public Stream Stream() => stream();
}
