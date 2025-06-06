

using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using Tonga.Enumerable;
using Tonga.Scalar;

namespace Tonga.IO
{
    /// <summary>
    /// The files in a ZIP archive.
    /// Note: Extraction is sticky.
    /// </summary>
    public sealed class ZipFiles : IEnumerable<string>
    {
        private readonly IScalar<IEnumerable<string>> files;

        /// <summary>
        /// The files in a ZIP archive.
        /// Note: Extraction is sticky.
        /// leaves zip stream open
        /// </summary>
        /// <param name="iConduit"></param>
        public ZipFiles(IConduit iConduit) : this(iConduit, true)
        {

        }

        /// <summary>
        /// The files in a ZIP archive.
        /// Note: Extraction is sticky.
        /// </summary>
        /// <param name="iConduit"></param>
        /// <param name="leaveOpen"></param>
        public ZipFiles(IConduit iConduit, bool leaveOpen)
        {
            this.files =
                new AsScalar<IEnumerable<string>>(() =>
                {
                    try
                    {
                        IEnumerable<string> files;
                        var copy = new MemoryStream();
                        var inputStream = iConduit.Stream();
                        inputStream.Position = 0;
                        inputStream.CopyTo(copy);
                        inputStream.Position = 0;
                        copy.Position = 0;

                        using (var zip = new ZipArchive(copy, ZipArchiveMode.Read, leaveOpen))
                        {
                            files =
                                new Mapped<ZipArchiveEntry, string>(
                                    entry => entry.FullName,
                                    zip.Entries
                                );
                        }
                        return files;
                    }
                    finally
                    {
                        if (!leaveOpen)
                        {
                            iConduit.Stream().Close();
                        }
                    }
                });
        }

        public IEnumerator<string> GetEnumerator()
        {
            return this.files.Value().GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
