using System;
using System.IO;
using System.IO.Compression;
using System.Net.Http.Headers;
using Tonga.Enumerable;
using Tonga.Scalar;

namespace Tonga.IO;

/// <summary>
/// A zip input mapped from a given zip input
/// Maps the zip entry paths according to the given mapping function
/// </summary>
public sealed class ZipMappedPaths(Func<string, string> mapping, IConduit zip) : IConduit
{
    private readonly Lazy<Stream> mappedZip = new(
    () =>
    {
        Stream inMemory = new ValidatedZip(zip).Stream();
        var newMemory = new MemoryStream();

        using (var archive = new ZipArchive(inMemory, ZipArchiveMode.Read, true))
        using (var newArchive = new ZipArchive(newMemory, ZipArchiveMode.Create, true))
        {
            foreach (var entry in archive.Entries)
            {
                Move(entry, newArchive, mapping);
            }
            inMemory.Position = 0;
        }
        newMemory.Seek(0, SeekOrigin.Begin);
        return newMemory;
    });
    public Stream Stream() => mappedZip.Value;

    private static void Move(ZipArchiveEntry source, ZipArchive archive, Func<string, string> mapping)
    {
        var mapped = mapping(source.FullName);
        using var sourceStream = source.Open();
        using var stream = archive.CreateEntry(mapped).Open();

        new TeeOnRead(
            new AsConduit(sourceStream),
            new AsConduit(stream)
        ).Length()
        .Value();
    }
}
