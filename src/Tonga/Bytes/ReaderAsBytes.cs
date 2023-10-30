

using System;
using System.IO;
using System.Text;
using Tonga.Scalar;

namespace Tonga.Bytes
{
    /// <summary>
    /// A <see cref="StringReader"/> as <see cref="IBytes"/>
    /// </summary>
    public sealed class ReaderAsBytes : IBytes, IDisposable
    {
        /// <summary>
        /// a reader
        /// </summary>
        private readonly IScalar<StreamReader> reader;

        /// <summary>
        /// encoding of the reader
        /// </summary>
        private readonly Encoding encoding;

        /// <summary>
        /// maximum buffer size
        /// </summary>
        private readonly int size;

        /// <summary>
        /// A <see cref="StringReader"/> as <see cref="IBytes"/>
        /// </summary>
        /// <param name="rdr">the reader</param>
        /// <param name="max">maximum buffer size</param>
        public ReaderAsBytes(StringReader rdr, int max = 16 << 10) : this(() =>
            {
                MemoryStream stream = new MemoryStream();
                StreamWriter writer = new StreamWriter(stream);
                writer.Write(rdr.ReadToEnd());
                writer.Flush();
                stream.Position = 0;
                return new StreamReader(stream);
            },
            Encoding.UTF8, max)
        { }

        /// <summary>
        /// A <see cref="StreamReader"/> as <see cref="IBytes"/>
        /// </summary>
        /// <param name="rdr">the reader</param>
        public ReaderAsBytes(StreamReader rdr) : this(rdr, Encoding.UTF8)
        { }

        /// <summary>
        /// A <see cref="StreamReader"/> as <see cref="IBytes"/>
        /// </summary>
        /// <param name="rdr">the reader</param>
        /// <param name="enc">encoding of the reader</param>
        /// <param name="max">maximum buffer size</param>
        public ReaderAsBytes(StreamReader rdr, Encoding enc, int max = 16 << 10) : this(
            AsScalar._(rdr), enc, max)
        { }

        /// <summary>
        /// A <see cref="StreamReader"/> returned by a <see cref="Func{TResult}"/>as <see cref="IBytes"/>
        /// </summary>
        /// <param name="rdr">function to retrieve the reader</param>
        /// <param name="enc">encoding of the reader</param>
        /// <param name="max">maximum buffer size</param>
        private ReaderAsBytes(Func<StreamReader> rdr, Encoding enc, int max = 16 << 10) : this(
            AsScalar._(rdr), enc, max)
        { }

        /// <summary>
        /// A <see cref="StreamReader"/> returned by a <see cref="IScalar{TResult}"/> as <see cref="IBytes"/>
        /// </summary>
        /// <param name="rdr">the reader</param>
        /// <param name="enc">encoding of the reader</param>
        /// <param name="max">maximum buffer size</param>
        public ReaderAsBytes(IScalar<StreamReader> rdr, Encoding enc, int max)
        {
            this.reader = rdr;
            this.encoding = enc;
            this.size = max;
        }

        /// <summary>
        /// Get the content as byte array.
        /// </summary>
        /// <returns>content as a byte array.</returns>
        public byte[] Bytes()
        {
            var rdr = this.reader.Value();
            var buffer = new char[this.size];
            var builder = new StringBuilder(this.size);
            var pos = 0;
            while (rdr.Peek() > -1)
            {
                pos = rdr.Read(buffer, 0, buffer.Length);
                builder.Append(buffer, 0, pos);
            }
            rdr.BaseStream.Position = 0;
            rdr.DiscardBufferedData();
            return this.encoding.GetBytes(builder.ToString());
        }

        /// <summary>
        /// Clean up.
        /// </summary>
        public void Dispose()
        {
            try
            {
                reader.Value().Dispose();
            }
            catch (Exception) { }
        }

    }
}
