

using System;
using System.Collections;
using Tonga.Scalar;

namespace Tonga.Scalar
{
    /// <summary>
    /// Length of an <see cref="IEnumerable"/>.
    /// </summary>
    public sealed class Length : ScalarEnvelope<Int64>
    {
        private Length(Func<long> length) : base(length)
        { }

        /// <summary>
        /// Length of an <see cref="IEnumerable"/>
        /// </summary>
        public static Length _(IEnumerable items) =>
            new Length(() => new Enumerator.LengthOf(items.GetEnumerator()).Value());

        /// <summary>
        /// Length of a stream
        /// </summary>
        public static Length _(IInput input) => new Length(() =>
        {
            var result = 0L;
            var stream = input.Stream();
            if(stream.CanSeek)
            {
                result = input.Stream().Length;
                input.Stream().Close();
            }
            else
            {
                long size = 0;
                var memorizedPosition = 0L;
                if (stream.CanSeek)
                    memorizedPosition = stream.Position;
                byte[] buf = new byte[16 << 10];

                int bytesRead;
                while ((bytesRead = stream.Read(buf, 0, buf.Length)) > 0)
                {
                    size += (long)bytesRead;
                }
                result = size;
                stream.Close();
            }
            return result;
        });
    }
}
