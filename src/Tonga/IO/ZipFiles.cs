using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using Tonga.Enumerable;

namespace Tonga.IO;

/// <summary>
/// The files in a ZIP archive.
/// Note: Extraction is sticky.
/// </summary>
public sealed class ZipFiles : IEnumerable<string>
{
    private readonly Func<IEnumerable<string>> files;

    /// <summary>
    /// The files in a ZIP archive.
    /// Note: Extraction is sticky.
    /// </summary>
    /// <param name="conduit"></param>
    /// <param name="leaveOpen"></param>
    public ZipFiles(IConduit conduit, bool leaveOpen = true)
    {
        this.files =
            () =>
            {
                try
                {
                    IEnumerable<string> result;
                    var copy = new MemoryStream();
                    var inputStream = conduit.Stream();
                    inputStream.Position = 0;
                    inputStream.CopyTo(copy);
                    inputStream.Position = 0;
                    copy.Position = 0;

                    using var zip = new ZipArchive(copy, ZipArchiveMode.Read, leaveOpen);
                    result = zip.Entries.AsMapped(entry => entry.FullName);
                    return result;
                }
                finally
                {
                    if (!leaveOpen)
                    {
                        conduit.Stream().Close();
                    }
                }
            };
    }

    public IEnumerator<string> GetEnumerator() =>
        this.files().GetEnumerator();

    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
}
