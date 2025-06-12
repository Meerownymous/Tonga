

using System;
using System.IO;

namespace Tonga.IO;

/// <summary>
/// A directory that cleans up when disposed.
/// </summary>
public sealed class TempDirectory(Func<string> path) : IScalar<DirectoryInfo>, IDisposable
{
    private readonly Lazy<string> path = new(path);

    /// <summary>
    /// A directory that cleans up when disposed.
    /// </summary>
    public TempDirectory() : this(
        () => Path.Combine(Path.GetTempPath(), Guid.NewGuid().ToString())
    )
    { }

    /// <summary>
    /// A directory that cleans up when disposed.
    /// </summary>
    public TempDirectory(string path) : this(() => path)
    { }

    public void Dispose()
    {
        if (Directory.Exists(path.Value))
        {
            DeleteDirectory(path.Value);
        }
    }

    public DirectoryInfo Value()
    {
        if (!Directory.Exists(path.Value))
        {
            Directory.CreateDirectory(path.Value);
        }
        return new DirectoryInfo(path.Value);
    }

    private void DeleteDirectory(string pathToRemove)
    {
        foreach (string subDir in Directory.GetDirectories(pathToRemove))
            DeleteDirectory(subDir);

        foreach (string fileName in Directory.EnumerateFiles(pathToRemove))
        {
            var fileInfo = new FileInfo(fileName)
            {
                Attributes = FileAttributes.Normal
            };
            fileInfo.Delete();
        }
        Directory.Delete(pathToRemove, true);
    }
}
