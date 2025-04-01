using System;
using System.IO;
using Tonga.IO;
using Xunit;

namespace Tonga.Tests.IO
{
    public sealed class TempDirectoryTest
    {
        [Fact]
        public void CreatesDirectory()
        {
            var path = Path.Combine(Path.GetTempPath(), "SunnyDirectory");
            try
            {
                var td = new TempDirectory(path).Value();
                Assert.True(
                    Directory.Exists(path)
                );
            }
            finally
            {
                if (Directory.Exists(path))
                {
                    Directory.Delete(path, true);
                }
            }
        }

        [Fact]
        public void CreatesRandomDirectory()
        {
            string path = String.Empty;
            try
            {
                using (var td = new TempDirectory())
                {
                    path = td.Value().FullName;
                    Assert.True(
                        Directory.Exists(path)
                    );
                }
            }
            finally
            {
                if (Directory.Exists(path))
                {
                    Directory.Delete(path, true);
                }
            }
        }

        [Fact]
        public void DeletesEmptyDirectory()
        {
            var path = Path.Combine(Path.GetTempPath(), "SunnyDirectory");
            try
            {
                using (var td = new TempDirectory(path))
                {
                    td.Value();
                }
                Assert.False(
                    Directory.Exists(path)
                );
            }
            finally
            {
                if (Directory.Exists(path))
                {
                    Directory.Delete(path, true);
                }
            }
        }

        [Fact]
        public void DeletesNotEmptyDirectory()
        {
            var path = Path.Combine(Path.GetTempPath(), "SunnyDirectory");
            using (var td = new TempDirectory(path))
            {
                td.Value();
                File.WriteAllBytes(Path.Combine(path, "raining.txt"), new byte[] { 0xAB });
            }
            try
            {
                Assert.False(
                    Directory.Exists(path)
                );
            }
            finally
            {
                if (Directory.Exists(path))
                {
                    Directory.Delete(path, true);
                }
            }
        }

        [Fact]
        public void DeletesWhenWriteProtectedFilesInside()
        {
            var tempPath = Path.Combine(Path.GetTempPath(), "SunnyDirectory");
            using (var td = new TempDirectory(tempPath))
            {
                td.Value();
                var file = Path.Combine(tempPath, "raining.txt");
                File.WriteAllBytes(file, new byte[] { 0xAB });
                new FileInfo(file).Attributes = FileAttributes.ReadOnly;
            }
            try
            {
                Assert.False(
                    Directory.Exists(tempPath)
                );
            }
            finally
            {
                if (Directory.Exists(tempPath))
                {
                    Directory.Delete(tempPath, true);
                }
            }
        }
    }
}
