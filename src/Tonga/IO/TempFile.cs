

using System;
using System.IO;
using Tonga.Scalar;

namespace Tonga.IO
{
    /// <summary>
    /// Temporary file.
    /// The temporary file is deleted when the object is disposed.
    /// </summary>
    public sealed class TempFile : IScalar<String>, IDisposable
    {
        private readonly IScalar<string> path;

        /// <summary>
        /// Temporary file with given extension.
        /// The temporary file is deleted when the object is disposed.
        /// </summary>
        /// <param name="extension">The file extension for the temprary file</param>
        public TempFile(string extension) : this(AsScalar._(() =>
            {
                var file = Path.GetTempFileName();
                extension = extension.TrimStart('.');
                var renamed = $"{file.Substring(0, file.LastIndexOf('.'))}.{extension}";
                File.Move(file, renamed);
                return renamed;
            })
        )
        { }

        /// <summary>
        /// The temporary file is deleted when the object is disposed.
        /// </summary>
        /// <param name="file">The file</param>
        public TempFile(FileInfo file) : this(new AsScalar<string>(() => file.FullName))
        { }

        /// <summary>
        /// Ctor
        /// </summary>
        public TempFile() : this(
            AsScalar._(Path.GetTempFileName))
        { }

        private TempFile(IScalar<string> path)
        {
            this.path = Sticky._(path);
        }

        /// <summary>
        /// Temporary file's path.
        /// The first call create the temporary file.
        /// </summary>
        public string Value()
        {
            return path.Value();
        }

        /// <summary>
        /// Delete the temporary file.
        /// </summary>
        public void Dispose()
        {
            File.Delete(path.Value());
        }
    }
}
