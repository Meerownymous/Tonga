

using System.IO;
using System.IO.Compression;
using Xunit;

namespace Tonga.IO.Tests
{
    public class ZipTest
    {
        [Fact]
        public void HasData()
        {
            string folder = Path.Combine(Directory.GetCurrentDirectory(), "ZipTest");
            try
            {
                Directory.CreateDirectory(folder);
                var newFile = File.Create(folder + "\\FileToZipOne.txt");
                newFile.Close();
                newFile = File.Create(folder + "\\FileToZipTwo.txt");
                newFile.Close();
                newFile = File.Create(folder + "\\FileToZipThree.txt");
                newFile.Close();

                var archive = new Zip(folder);
                Assert.InRange<long>(archive.Stream().Length, 1, long.MaxValue);

            }
            finally
            {
                Directory.Delete(folder, true);
            }
        }

        [Fact]
        public void HasEntry()
        {
            string folder = Path.Combine(Directory.GetCurrentDirectory(), "ZipTest");
            try
            {
                Directory.CreateDirectory(folder);
                var newFile = File.Create(Path.Combine(folder, "FileToZipOne.txt"));
                newFile.Close();
                newFile = File.Create(Path.Combine(folder, "FileToZipTwo.txt"));
                newFile.Close();
                newFile = File.Create(Path.Combine(folder, "FileToZipThree.txt"));
                newFile.Close();

                var streamOfZipped = new Zip(folder);

                var archive = new ZipArchive(streamOfZipped.Stream());
                Assert.True(archive.GetEntry(Path.Combine(folder, "FileToZipTwo.txt")) != null);
            }
            finally
            {
                Directory.Delete(folder, true);
            }
        }
    }
}
