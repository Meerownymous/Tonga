

using System;
using System.IO;
using Xunit;
using Tonga.Enumerable;
using Tonga.Tests;
using Tonga.Scalar;

namespace Tonga.IO.Tests
{
    public sealed class DirectoryOfTests
    {
        [Fact]
        public void EnumeratesFiles()
        {
            var file1 = Path.GetFullPath("assets/directoryof/subdir/garbage.txt");
            var file2 = Path.GetFullPath("assets/directoryof/subdir/rubbish.txt");

            new Tidy(() =>
            {
                var dir = Path.GetDirectoryName(file1);
                Directory.CreateDirectory(dir);

                File.Create(file1).Close();
                File.Create(file2).Close();

                Assert.True(
                    new Contains<string>(
                        new DirectoryOf(Path.GetFullPath(dir)),
                        file1
                    ).Value() &&
                    new Contains<string>(
                        new DirectoryOf(Path.GetFullPath(dir)),
                        file2
                    ).Value()
                );

            },
            new Uri(file1), new Uri(file2)).Invoke();
        }

        [Fact]
        public void EnumeratesDirectories()
        {
            var dir = Path.GetFullPath("assets/directoryof/dir");
            var subdir = Path.GetFullPath("assets/directoryof/dir/fancy-subdir");

            Directory.CreateDirectory(dir);
            Directory.CreateDirectory(subdir);

            Assert.True(
                new Contains<string>(
                    new DirectoryOf(dir),
                    subdir
                ).Value()
            );
        }

        [Fact]
        public void RejectsNonDirectory()
        {
            var file = Path.GetFullPath("assets/directoryof/subdir/garbage.txt");

            new Tidy(() =>
                {
                    var dir = Path.GetDirectoryName(file);
                    Directory.CreateDirectory(dir);

                    File.Create(file).Close();

                    Assert.Throws<ArgumentException>(() =>
                        new DirectoryOf(file).GetEnumerator()
                    );

                },
                new Uri(Path.GetFullPath(file))
            ).Invoke();
        }

        [Fact]
        public void EnumeratesFilesInSubDirectories()
        {
            using (var directory = new TempDirectory())
            {
                var dir = directory.Value().FullName;
                var subdir = Path.GetFullPath(directory.Value().FullName + "/subdir/subdir2/subdir3/");
                var file = Path.GetFullPath(directory.Value().FullName + "/subdir/subdir2/subdir3/test.txt");

                Directory.CreateDirectory(subdir);
                File.Create(file).Close();

                Assert.True(
                    new Contains<string>(
                        new DirectoryOf(dir, true),
                        file
                    ).Value()
                );
            }
        }
    }
}
