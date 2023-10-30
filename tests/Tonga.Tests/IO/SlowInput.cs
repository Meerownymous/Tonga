

using System.IO;

namespace Tonga.IO.Tests
{
    internal sealed class SlowInput : IInput
    {

        /**
         * Original input.
         */
        private readonly IInput _origin;

        /**
         * Ctor.
         * @param size The size of the array to encapsulate
         */
        internal SlowInput(long size) : this((int)size)
        { }

        /**
         * Ctor.
         * @param size The size of the array to encapsulate
         */
        internal SlowInput(int size) : this(new AsInput(new MemoryStream(new byte[size])))
        { }

        /**
         * Ctor.
         * @param input Original input to encapsulate and make slower
         */
        internal SlowInput(IInput input)
        {
            this._origin = input;
        }

        public Stream Stream()
        {
            return new SlowInputStream(this._origin.Stream());
        }

    }
}
