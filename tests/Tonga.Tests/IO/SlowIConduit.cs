

using System.IO;
using Tonga.IO;
using Tonga.IO.Tests;

namespace Tonga.Tests.IO
{
    internal sealed class SlowIConduit : IConduit
    {

        /**
         * Original input.
         */
        private readonly IConduit _origin;

        /**
         * Ctor.
         * @param size The size of the array to encapsulate
         */
        internal SlowIConduit(long size) : this((int)size)
        { }

        /**
         * Ctor.
         * @param size The size of the array to encapsulate
         */
        internal SlowIConduit(int size) : this(new Tonga.IO.AsConduit(new MemoryStream(new byte[size])))
        { }

        /**
         * Ctor.
         * @param input Original input to encapsulate and make slower
         */
        internal SlowIConduit(IConduit iConduit)
        {
            this._origin = iConduit;
        }

        public Stream Stream()
        {
            return new SlowInputStream(this._origin.Stream());
        }

    }
}
