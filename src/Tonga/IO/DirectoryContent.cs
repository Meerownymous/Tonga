using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using Tonga.Enumerable;
using Tonga.Fact;
using Tonga.Text;

namespace Tonga.IO;

public sealed class DirectoryContent : IEnumerable<string>
{

    /// <summary>
    /// Path of the directory.
    /// </summary>
    private readonly Lazy<string> directoryPath;

    /// <summary>
    /// include all files from sub directories
    /// </summary>
    private readonly Lazy<bool> recursive;

    /// <summary>
    /// Ctor.
    /// </summary>
    /// <param name="dir">DirectoryInfo</param>
    public DirectoryContent(DirectoryInfo dir) : this(dir, false)
    { }

    /// <summary>
    /// Ctor.
    /// </summary>
    /// <param name="dir">DirectoryInfo</param>
    /// <param name="recursive">include all files from sub directories</param>
    public DirectoryContent(DirectoryInfo dir, bool recursive) : this(
        () => dir.FullName,
        () => recursive
    )
    { }

    /// <summary>
    /// Ctor.
    /// </summary>
    /// <param name="file">File as a uri</param>
    public DirectoryContent(Uri file) : this(file, false)
    { }

    /// <summary>
    /// Ctor.
    /// </summary>
    /// <param name="file">File as a uri</param>
    /// <param name="recursive">include all files from sub directories</param>
    public DirectoryContent(Uri file, bool recursive) : this(
        () =>
        {
            if (file.Scheme != "file")
            {
                throw
                 new ArgumentException(
                     new Formatted("'{0}' is not a directory.", file.ToString()).Str()
                 );
            }
            return file.AbsolutePath;
        },
        () => recursive
    )
    { }

    /// <summary>
    /// Ctor.
    /// </summary>
    /// <param name="file">File as a path to directory.</param>
    public DirectoryContent(FileInfo file) : this(file, false)
    { }

    /// <summary>
    /// Ctor.
    /// </summary>
    /// <param name="file">File as a path to directory.</param>
    /// <param name="recursive">include all files from sub directories</param>
    public DirectoryContent(FileInfo file, bool recursive) : this(
        () => file.Directory.FullName,
        () => recursive
    )
    { }

    /// <summary>
    /// ctor
    /// </summary>
    /// <param name="path"></param>
    public DirectoryContent(string path) : this(path, false)
    { }

    /// <summary>
    /// ctor
    /// </summary>
    /// <param name="path"></param>
    /// <param name="recursive">include all files from sub directories</param>
    public DirectoryContent(string path, bool recursive) : this(
        () => path,
        () => recursive
    )
    { }

    /// <summary>
    /// ctor
    /// </summary>
    /// <param name="path"></param>
    public DirectoryContent(Func<string> path) : this(path, () => false)
    { }

    /// <summary>
    /// ctor
    /// </summary>
    /// <param name="path"></param>
    /// <param name="recursive">include all files from sub directories</param>
    public DirectoryContent(Func<string> path, Func<bool> recursive)
    {
        var pathStr = path();
        this.directoryPath = new(() =>
        {
            var val = Path.GetFullPath(pathStr);
            try
            {
                //check if path is a directory
                if ((File.GetAttributes(val) & FileAttributes.Directory) != FileAttributes.Directory)
                {
                    throw new ArgumentException();
                }
            }
            catch (ArgumentException)
            {
                throw
                    new ArgumentException(
                        new Formatted("'{0}' is not a directory.", pathStr).Str()
                );
            }

            return val;
        });
        this.recursive = new(recursive);
    }

    /// <summary>
    /// Enumerate directory and file paths as string.
    /// </summary>
    /// <returns></returns>
    public IEnumerator<string> GetEnumerator() =>
        this.recursive.Value
        ?
        Directory
            .EnumerateFiles(directoryPath.Value, "*", SearchOption.AllDirectories)
            .AsSorted()
            .GetEnumerator()
        :
        Directory
            .EnumerateDirectories(directoryPath.Value)
            .AsJoined(Directory.EnumerateFiles(directoryPath.Value))
            .AsSorted()
            .GetEnumerator();

    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
}


