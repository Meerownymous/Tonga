

using System;
using System.IO;
using System.Text;
using Tonga.Scalar;

#pragma warning disable CS1591

namespace Tonga.IO
{
    /// <summary>
    /// <see cref="IOutput"/> to a target.
    /// </summary>
    public sealed class OutputTo : IOutput, IDisposable
    {
        /// <summary>
        /// the output
        /// </summary>
        private readonly IScalar<Stream> origin;

        /// <summary>
        /// a path
        /// </summary>
        /// <param name="path"></param>
        public OutputTo(string path) : this(new Uri(path))
        { }

        /// <summary>
        /// <see cref="IOutput"/> to a target file Uri.
        /// </summary>
        /// <param name="path">a file uri, retrieve with Path.GetFullPath(absOrRelativePath) or prefix with file://. Must be absolute</param>
        public OutputTo(Uri path) : this(
            () => new FileStream(Uri.UnescapeDataString(path.AbsolutePath), FileMode.OpenOrCreate))
        { }

        /// <summary>
        /// <see cref="IOutput"/> to a target <see cref="StreamWriter"/>.
        /// </summary>
        /// <param name="wtr">a writer</param>
        public OutputTo(StreamWriter wtr) : this(wtr, Encoding.UTF8)
        { }

        /// <summary>
        /// <see cref="IOutput"/> to a target <see cref="StreamWriter"/>.
        /// </summary>
        /// <param name="wtr">a writer</param>
        /// <param name="enc"><see cref="Encoding"/> of the writer</param>
        public OutputTo(StreamWriter wtr, Encoding enc) : this(
            new WriterAsOutputStream(wtr, enc))
        { }

        /// <summary>
        /// <see cref="IOutput"/> to a target <see cref="Stream"/> returned by a <see cref="System.Func{TResult}"/>.
        /// </summary>
        /// <param name="fnc">target stream returning function</param>
        public OutputTo(Func<Stream> fnc) : this(AsScalar._(fnc))
        { }

        /// <summary>
        /// <see cref="IOutput"/> to a target <see cref="Stream"/>.
        /// </summary>
        /// <param name="stream">target stream</param>
        public OutputTo(Stream stream) : this(AsScalar._(() => stream))
        { }

        private OutputTo(IScalar<Stream> origin)
        {
            this.origin =
                StickyIf._(
                    value => value.CanWrite,
                    origin
                );
        }

        public Stream Stream()
        {
            return this.origin.Value();
        }

        public void Dispose()
        {
            origin.Value().Dispose();
        }
    }
}
