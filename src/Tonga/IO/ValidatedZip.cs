

using System;
using System.IO;

using Tonga.Scalar;

namespace Tonga.IO
{
    /// <summary>
    /// A validated Zip archive which is either a Pkzip or a Gzip (checked via lead bytes)
    /// </summary>
    public sealed class ValidatedZip : IInput
    {
        private readonly AsScalar<Stream> stream;

        public ValidatedZip(IInput input)
        {
            this.stream = new AsScalar<Stream>(() =>
            {
                if (!IsZip(input.Stream()))
                {
                    throw new InvalidOperationException($"Content is not compressed with either GZIP or PKZIP");
                }
                return input.Stream();
            });

        }
        public Stream Stream()
        {
            return this.stream.Value();
        }

        private bool IsPkZip(byte[] bytes)
        {
            var zipLeadBytes = 0x04034b50;
            bool isZip = false;
            if (bytes.Length > 4)
            {
                isZip = false;
            }
            else
            {
                isZip = (BitConverter.ToInt32(bytes, 0) == zipLeadBytes);
            }
            return isZip;
        }

        private bool IsGZip(byte[] bytes)
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

        private bool IsZip(Stream content)
        {
            byte[] bytes = new byte[4];
            content.Read(bytes, 0, 4);
            content.Position = 0;
            return IsPkZip(bytes) || IsGZip(bytes);
        }
    }
}
