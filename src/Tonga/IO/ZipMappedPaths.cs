

using System;
using System.IO;
using System.IO.Compression;
using Tonga.Scalar;

namespace Tonga.IO
{
    /// <summary>
    /// A zip input mapped from a given zip input
    /// Maps the zip entry paths according to the given mapping function
    /// </summary>
    public sealed class ZipMappedPaths : IInput
    {
        private readonly IScalar<Stream> input;

        /// <summary>
        /// A zip input mapped from a given zip input
        /// Maps the zip entry paths according to the given mapping function
        /// </summary>
        public ZipMappedPaths(Func<string, string> mapping, IInput zip)
        {
            this.input =
                new ScalarOf<Stream>(() =>
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
        }
        public Stream Stream()
        {
            return this.input.Value();
        }

        private void Move(ZipArchiveEntry source, ZipArchive archive, Func<string, string> mapping)
        {
            var mapped = mapping(source.FullName);
            using (var sourceStream = source.Open())
            using (var stream = archive.CreateEntry(mapped).Open())
            {
                new IO.LengthOf(
                    new TeeInput(
                        new InputOf(sourceStream),
                        new OutputTo(stream)
                    )
                ).Value();
            }
        }
    }
}
