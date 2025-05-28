using System;
using System.IO;

#pragma warning disable MaxPublicMethodCount // a public methods count maximum
namespace Tonga.IO;

/// <summary>
/// <see cref="Stream"/> which copies to another <see cref="Stream"/> while writing.
/// </summary>
public sealed class TeeOnWriteStream(Stream target, Stream copy) : Stream
{
    public override void Write(byte[] buffer, int offset, int count)
    {
        try
        {
            target.Write(buffer, offset, count);
        }
        finally
        {
            copy.Write(buffer, offset, count);
        }
    }


    public override void Flush()
    {
        try
        {
            target.Flush();
        }
        finally
        {
            copy.Flush();
        }
    }

    protected override void Dispose(bool disposing)
    {
        try
        {
            target.Dispose();
        }
        catch (Exception)
        {
            // ignored
        }
        finally
        {
            try
            {
                copy.Dispose();
            }
            catch (Exception)
            {
                // ignored
            }
        }
        base.Dispose(disposing);
    }

    public override bool CanRead => false;

    public override bool CanSeek => false;

    public override bool CanWrite => true;

    public override long Length => target.Length;

    public override long Position
    {
        get => target.Position;
        set => target.Position = value;
    }

    public override int Read(byte[] buffer, int offset, int count) =>
        throw new NotImplementedException();

    public override long Seek(long offset, SeekOrigin origin) =>
        throw new NotImplementedException();

    public override void SetLength(long value)
    {
        try
        {
            target.SetLength(value);
        }
        finally
        {
            copy.SetLength(value);
        }
    }
}
