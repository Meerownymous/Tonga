

using System;
using System.IO;
using System.Text;
using Tonga.Scalar;

namespace Tonga.IO
{
    /// <summary>
    /// Append <see cref="IOutput"/> to a target.
    /// </summary>
    public sealed class AppendTo : IOutput, IDisposable
    {
        private readonly IScalar<IOutput> stream;

        /// <summary>
        /// a path
        /// </summary>
        /// <param name="path"></param>
        public AppendTo(string path) : this(new Uri(path))
        { }

        /// <summary>
        /// Append <see cref="IOutput"/> to a target file Uri.
        /// </summary>
        /// <param name="path">a file uri, retrieve with Path.GetFullPath(absOrRelativePath) or prefix with file://. Must be absolute</param>
        public AppendTo(Uri path) : this(
            () => new FileStream(Uri.UnescapeDataString(path.AbsolutePath), FileMode.OpenOrCreate))
        { }

        /// <summary>
        /// Append <see cref="IOutput"/> to a target <see cref="StreamWriter"/>.
        /// </summary>
        /// <param name="wtr">a writer</param>
        public AppendTo(StreamWriter wtr) : this(wtr, Encoding.UTF8)
        { }

        /// <summary>
        /// Append <see cref="IOutput"/> to a target <see cref="StreamWriter"/>.
        /// </summary>
        /// <param name="wtr">a writer</param>
        /// <param name="enc"><see cref="Encoding"/> of the writer</param>
        public AppendTo(StreamWriter wtr, Encoding enc) : this(
            new WriterAsOutputStream(wtr, enc)
        )
        { }

        /// <summary>
        /// Append <see cref="IOutput"/> to a target <see cref="Stream"/> returned by a <see cref="Func{TResult}"/>.
        /// </summary>
        /// <param name="fnc">target stream returning function</param>
        public AppendTo(Func<Stream> fnc) : this(new OutputTo(fnc))
        { }

        /// <summary>
        /// Append <see cref="IOutput"/> to a target <see cref="Stream"/>.
        /// </summary>
        /// <param name="stream">target stream</param>
        public AppendTo(Stream stream) : this(new OutputTo(stream))
        { }

        /// <summary>
        /// Append <see cref="IOutput"/> to a target <see cref="IOutput"/>.
        /// </summary>
        /// <param name="output">target output</param>
        public AppendTo(IOutput output) : this(AsScalar._(output))
        { }

        /// <summary>
        /// Append <see cref="IOutput"/> to a target <see cref="IScalar{IOutput}"/>.
        /// </summary>
        /// <param name="outputSc">target output scalar</param>
        private AppendTo(IScalar<IOutput> outputSc)
        {
            this.stream = (outputSc);
        }

        /// <summary>
        /// Disposes the stream
        /// </summary>
        public void Dispose()
        {
            (stream.Value() as IDisposable)?.Dispose();
        }

        /// <summary>
        /// Get the stream to append
        /// </summary>
        /// <returns>the stream</returns>
        public Stream Stream()
        {
            var result = stream.Value().Stream();
            result.Seek(0, SeekOrigin.End);

            return result;
        }
    }
}
