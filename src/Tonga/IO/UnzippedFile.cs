

using System;
using System.IO;
using System.IO.Compression;
using Tonga.Enumerable;
using Tonga.Scalar;

namespace Tonga.IO;

/// <summary>
/// Content of a file in a zip archive
/// is tolerant to slash style
/// </summary>
public sealed class UnzippedFile(IConduit zip, string targetPath, bool leaveOpen = true) : IConduit
{
    private readonly Lazy<Stream> stream =
        new(() =>
        {
            Stream content;
            using (var archive =
               new ZipArchive(
                   new ValidatedZip(zip).Stream(),
                   ZipArchiveMode.Read,
                   leaveOpen
                )
            )
            {
                var zipEntry =
                    new First<ZipArchiveEntry>(
                        new Filtered<ZipArchiveEntry>(entry =>
                                Path.GetFullPath(entry.FullName) == Path.GetFullPath(targetPath),
                            archive.Entries
                        ),
                        new ArgumentException($"Cannot extract file '{targetPath}' because it doesn't exist in the zip archive.")
                    ).Value();
                content = Content(zipEntry);
            }
            zip.Stream().Position = 0;
            return content;
        });

    public Stream Stream() => stream.Value;

    private static Stream Content(ZipArchiveEntry zipEntry)
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
