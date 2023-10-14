

using System;
using System.IO;
using Tonga.Scalar;

namespace Tonga.IO
{
    /// <summary>
    /// A directory that cleans up when disposed.
    /// </summary>
    public sealed class TempDirectory : IScalar<DirectoryInfo>, IDisposable
    {
        private readonly IScalar<string> path;

        /// <summary>
        /// A directory that cleans up when disposed.
        /// </summary>
        public TempDirectory() : this(
            new ScalarOf<string>(() =>
                Path.Combine(Path.GetTempPath(), Guid.NewGuid().ToString())
            )
        )
        { }

        /// <summary>
        /// A directory that cleans up when disposed.
        /// </summary>
        public TempDirectory(string path) : this(
            new Live<string>(path)
        )
        { }

        /// <summary>
        /// A directory that cleans up when disposed.
        /// </summary>
        private TempDirectory(IScalar<string> path)
        {
            this.path = path;
        }

        public void Dispose()
        {
            if (Directory.Exists(path.Value()))
            {
                DeleteDirectory(path.Value());
            }
        }

        public DirectoryInfo Value()
        {
            if (!Directory.Exists(this.path.Value()))
            {
                Directory.CreateDirectory(this.path.Value());
            }
            return new DirectoryInfo(this.path.Value());
        }

        private void DeleteDirectory(string path)
        {
            foreach (string subDir in Directory.GetDirectories(path))
            {
                DeleteDirectory(subDir);
            }

            foreach (string fileName in Directory.EnumerateFiles(path))
            {
                var fileInfo = new FileInfo(fileName)
                {
                    Attributes = FileAttributes.Normal
                };
                fileInfo.Delete();
            }
            Directory.Delete(path, true);
        }
    }
}
