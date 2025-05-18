

using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using Tonga.Enumerable;
using Tonga.Fact;
using Tonga.Scalar;
using Tonga.Text;

namespace Tonga.IO
{
    public sealed class DirectoryOf : IEnumerable<string>
    {

        /// <summary>
        /// Path of the directory.
        /// </summary>
        private readonly IScalar<string> dir;

        /// <summary>
        /// include all files from sub directories
        /// </summary>
        private readonly IFact recursive;

        /// <summary>
        /// Ctor.
        /// </summary>
        /// <param name="dir">DirectoryInfo</param>
        public DirectoryOf(DirectoryInfo dir) : this(dir, false)
        { }

        /// <summary>
        /// Ctor.
        /// </summary>
        /// <param name="dir">DirectoryInfo</param>
        /// <param name="recursive">include all files from sub directories</param>
        public DirectoryOf(DirectoryInfo dir, bool recursive) : this(AsScalar._(() =>
            {
                return dir.FullName;
            }),
            new AsFact(recursive)
        )
        { }

        /// <summary>
        /// Ctor.
        /// </summary>
        /// <param name="file">File as a uri</param>
        public DirectoryOf(Uri file) : this(file, false)
        { }

        /// <summary>
        /// Ctor.
        /// </summary>
        /// <param name="file">File as a uri</param>
        /// <param name="recursive">include all files from sub directories</param>
        public DirectoryOf(Uri file, bool recursive) : this(AsScalar._(() =>
            {
                if (file.Scheme != "file")
                {
                    throw
                     new ArgumentException(
                         new Formatted("'{0}' is not a directory.", file.ToString()).AsString()
                     );
                }
                return file.AbsolutePath;
            }),
            new AsFact(recursive)
        )
        { }

        /// <summary>
        /// Ctor.
        /// </summary>
        /// <param name="file">File as a path to directory.</param>
        public DirectoryOf(FileInfo file) : this(file, false)
        { }

        /// <summary>
        /// Ctor.
        /// </summary>
        /// <param name="file">File as a path to directory.</param>
        /// <param name="recursive">include all files from sub directories</param>
        public DirectoryOf(FileInfo file, bool recursive) : this(
            AsScalar._(file.Directory.FullName),
            new AsFact(recursive)
        )
        { }

        /// <summary>
        /// ctor
        /// </summary>
        /// <param name="path"></param>
        public DirectoryOf(string path) : this(path, false)
        { }

        /// <summary>
        /// ctor
        /// </summary>
        /// <param name="path"></param>
        /// <param name="recursive">include all files from sub directories</param>
        public DirectoryOf(string path, bool recursive) : this(
            AsScalar._(path),
            new AsFact(recursive)
        )
        { }

        /// <summary>
        /// ctor
        /// </summary>
        /// <param name="path"></param>
        public DirectoryOf(IScalar<string> path) : this(path, new False())
        { }

        /// <summary>
        /// ctor
        /// </summary>
        /// <param name="path"></param>
        /// <param name="recursive">include all files from sub directories</param>
        public DirectoryOf(IScalar<string> path, IFact recursive)
        {
            this.dir = new AsScalar<string>(() =>
            {
                var val = Path.GetFullPath(path.Value());
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
                            new Formatted("'{0}' is not a directory.", path.Value()
                        ).AsString()
                    );
                }

                return val;
            });
            this.recursive = recursive;
        }

        /// <summary>
        /// Enumerate directory and file paths as string.
        /// </summary>
        /// <returns></returns>
        public IEnumerator<string> GetEnumerator()
        {
            return
                new Scalar.Conditional<IEnumerator<string>>(
                    this.recursive,
                    new Sorted<string>(
                        Directory.EnumerateFiles(dir.Value(), "*", SearchOption.AllDirectories)

                    ).GetEnumerator(),
                    new Sorted<string>(
                        new Joined<string>(
                            Directory.EnumerateDirectories(dir.Value()),
                            Directory.EnumerateFiles(dir.Value())
                        )
                    ).GetEnumerator()
                ).Value();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
