

using System;
using System.IO;
using System.Text;
using Tonga.Scalar;

namespace Tonga.IO
{
    /// <summary>
    /// Append <see cref="IConduit"/> to a target.
    /// </summary>
    public sealed class Appending(Func<IConduit> target) : IConduit, IDisposable
    {
        private readonly Lazy<Stream> stream = new(target().Stream);

        /// <summary>
        /// Append <see cref="IConduit"/> to a target file Uri.
        /// </summary>
        /// <param name="path">a file uri, retrieve with Path.GetFullPath(absOrRelativePath) or prefix with file://. Must be absolute</param>
        public Appending(Uri path) : this(
            () => new FileStream(Uri.UnescapeDataString(path.AbsolutePath), FileMode.OpenOrCreate))
        { }

        /// <summary>
        /// Append <see cref="IConduit"/> to a target <see cref="StreamWriter"/>.
        /// </summary>
        /// <param name="wtr">a writer</param>
        public Appending(StreamWriter wtr) : this(wtr, Encoding.UTF8)
        { }

        /// <summary>
        /// Append <see cref="IConduit"/> to a target <see cref="StreamWriter"/>.
        /// </summary>
        /// <param name="wtr">a writer</param>
        /// <param name="enc"><see cref="Encoding"/> of the writer</param>
        public Appending(StreamWriter wtr, Encoding enc) : this(
            new WriterAsOutputStream(wtr, enc)
        )
        { }

        /// <summary>
        /// Append <see cref="IConduit"/> to a target <see cref="Stream"/> returned by a <see cref="Func{TResult}"/>.
        /// </summary>
        /// <param name="fnc">target stream returning function</param>
        public Appending(Func<Stream> fnc) : this(new AsConduit(fnc))
        { }

        /// <summary>
        /// Append <see cref="IConduit"/> to a target <see cref="Stream"/>.
        /// </summary>
        /// <param name="stream">target stream</param>
        public Appending(Stream stream) : this(new AsConduit(stream))
        { }

        /// <summary>
        /// Append <see cref="IConduit"/> to a target <see cref="IConduit"/>.
        /// </summary>
        /// <param name="output">target output</param>
        public Appending(IConduit output) : this(() => output)
        { }

        /// <summary>
        /// Disposes the stream
        /// </summary>
        public void Dispose() =>
            (stream.Value as IDisposable)?.Dispose();

        /// <summary>
        /// Get the stream to append
        /// </summary>
        /// <returns>the stream</returns>
        public Stream Stream()
        {
            var result = stream.Value;
            result.Seek(0, SeekOrigin.End);
            return result;
        }
    }
}
