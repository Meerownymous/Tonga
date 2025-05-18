

using System;
using System.IO;
using System.IO.Compression;
using Tonga.Enumerable;
using Tonga.Scalar;

namespace Tonga.IO
{
    /// <summary>
    /// Content of a file in a zip archive
    /// is tolerant to slash style
    /// </summary>
    public sealed class UnzippedFile : IConduit
    {
        private readonly IConduit zip;
        private readonly string filePath;
        private readonly AsScalar<Stream> stream;
        /// <summary>
        /// Content of a file in a zip archive
        /// is tolerant to slash style
        /// leaves zip stream open
        /// </summary>
        public UnzippedFile(IConduit zip, string virtualPath) : this(zip, virtualPath, true)
        { }

        /// <summary>
        /// Content of a file in a zip archive
        /// is tolerant to slash style
        /// </summary>
        public UnzippedFile(IConduit zip, string virtualPath, bool leaveOpen)
        {
            this.zip = zip;
            this.filePath = virtualPath;

            this.stream = new AsScalar<Stream>(() =>
            {
                Stream content;
                using (var archive = new ZipArchive(
                                        new ValidatedZip(this.zip).Stream(),
                                        ZipArchiveMode.Read,
                                        leaveOpen))
                {
                    var zipEntry =
                        new First<ZipArchiveEntry>(
                            new Filtered<ZipArchiveEntry>(entry =>
                                Path.GetFullPath(entry.FullName) == Path.GetFullPath(this.filePath),
                                archive.Entries
                            ),
                            new ArgumentException($"Cannot extract file '{this.filePath}' because it doesn't exist in the zip archive.")
                        ).Value();
                    content = Content(zipEntry);
                }
                this.zip.Stream().Position = 0;
                return content;
            });
        }

        public Stream Stream()
        {
            return this.stream.Value();
        }

        private Stream Content(ZipArchiveEntry zipEntry)
        {
            MemoryStream content = new MemoryStream();
            using (Stream zipEntryStream = zipEntry.Open())
            {
                zipEntryStream.CopyTo(content);
                zipEntryStream.Close();
                content.Seek(0, SeekOrigin.Begin);
            }
            return content;
        }



    }
}
