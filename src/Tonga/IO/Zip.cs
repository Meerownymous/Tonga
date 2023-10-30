
using System.IO;
using System.IO.Compression;
using Tonga;
using Tonga.Bytes;
using Tonga.Func;
using Tonga.IO;
using Tonga.Scalar;
using Tonga.Text;
using Tonga.Text;

///<summary>
///Zips all Files in a Directory
///</summary>
public sealed class Zip : IInput
{
    private readonly string path;

    /// <summary>
    /// Zips all Files in a Directory
    /// </summary>
    /// <param name="path"> the directory with the files to zip</param>
    public Zip(string path)
    {
        this.path = path;
    }

    /// <summary>
    /// The Zipped Files as a Stream
    /// </summary>
    /// <returns></returns>
    public Stream Stream()
    {
        AssumeIsDirectory(this.path);
        var memory = new MemoryStream();
        using (var zip = new ZipArchive(memory, ZipArchiveMode.Create, true))
        {
            foreach (var file in Directory.GetFiles(this.path, "*", SearchOption.AllDirectories))
            {
                var entry = zip.CreateEntry(file);
                using (var entryStream = entry.Open())
                {
                    ReadAll._(
                        new TeeInput(
                            new InputOf(file),
                            new OutputTo(entryStream)
                        )
                    ).Invoke();
                }
            }
        }
        return memory;
    }

    private void AssumeIsDirectory(string path)
    {
        if (!Directory.Exists(path))
        {
            throw
                new DirectoryNotFoundException(
                    new Formatted(
                        "Path is not a directory or does not exist: {0}",
                        path
                    ).AsString()
                );
        }
    }
}
