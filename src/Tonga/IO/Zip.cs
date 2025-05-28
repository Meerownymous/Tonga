using System.IO;
using System.IO.Compression;
using Tonga.Text;

namespace Tonga.IO;

///<summary>
///Zips all Files in a Directory
///</summary>
public sealed class Zip(string path) : IConduit
{
    /// <summary>
    /// The Zipped Files as a Stream
    /// </summary>
    /// <returns></returns>
    public Stream Stream()
    {
        AssumeIsDirectory(path);
        var memory = new MemoryStream();
        using var zip = new ZipArchive(memory, ZipArchiveMode.Create, true);
        foreach (var file in Directory.GetFiles(path, "*", SearchOption.AllDirectories))
        {
            var entry = zip.CreateEntry(file);
            using var entryStream = entry.Open();

            new TeeOnRead(
                new AsConduit(file),
                new AsConduit(entryStream)
            ).FullRead()
            .Yield();
        }

        return memory;
    }

    private static void AssumeIsDirectory(string directoryPath)
    {
        if (!Directory.Exists(directoryPath))
        {
            throw
                new DirectoryNotFoundException(
                    new Formatted(
                        "Path is not a directory or does not exist: {0}",
                        directoryPath
                    ).Str()
                );
        }
    }
}
