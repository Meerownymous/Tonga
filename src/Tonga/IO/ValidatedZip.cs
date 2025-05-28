

using System;
using System.IO;

using Tonga.Scalar;

namespace Tonga.IO
{
    /// <summary>
    /// A validated Zip archive which is either a Pkzip or a Gzip (checked via lead bytes)
    /// </summary>
    public sealed class ValidatedZip(IConduit zip) : IConduit
    {
        private readonly Lazy<Stream> stream =
            new(() =>
            {
                if (!IsZip(zip.Stream()))
                    throw new InvalidOperationException($"Content is not compressed with either GZIP or PKZIP");
                return zip.Stream();
            });

        public Stream Stream() => stream.Value;

        private static bool IsPkZip(byte[] bytes)
        {
            var zipLeadBytes = 0x04034b50;
            bool isZip;
            if (bytes.Length > 4)
            {
                isZip = false;
            }
            else
            {
                isZip = BitConverter.ToInt32(bytes, 0) == zipLeadBytes;
            }
            return isZip;
        }

        private static bool IsGZip(byte[] bytes)
        {
            var gzipLeadBytes = 0x8b1f;
            bool isZip = false;
            if (bytes == null && bytes.Length >= 2)
            {
                isZip = false;
            }
            else
            {
                isZip = (BitConverter.ToUInt16(bytes, 0) == gzipLeadBytes);
            }
            return isZip;
        }

        private static bool IsZip(Stream content)
        {
            byte[] bytes = new byte[4];
            content.ReadExactly(bytes, 0, 4);
            content.Position = 0;
            return IsPkZip(bytes) || IsGZip(bytes);
        }
    }
}
