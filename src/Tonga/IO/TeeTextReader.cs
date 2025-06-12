using System;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace Tonga.IO;

/// <summary>
/// A <see cref="TextReader"/> which copies to a <see cref="StreamWriter"/> while reading.
/// </summary>
public sealed class TeeTextReader : TextReader
{
    private readonly TextReader source;
    private readonly StreamWriter destination;

    public TeeTextReader(TextReader source, StreamWriter destination)
    {
        this.source = source;
        this.destination = destination;
    }

    public override int Read()
    {
        int c = source.Read();
        if (c > -1)
        {
            destination.Write((char)c);
        }
        return c;
    }

    public override int ReadBlock(char[] buffer, int index, int count)
    {
        int done = source.ReadBlock(buffer, index, count);
        if (done > 0)
        {
            destination.Write(buffer, index, done);
        }
        return done;
    }

    public override Task<string> ReadLineAsync()
    {
        var str = source.ReadLineAsync();
        return str.ContinueWith(t =>
        {
            destination.WriteLine(t.Result);
            return t.Result;
        });
    }

    public override async Task<int> ReadBlockAsync(char[] buffer, int index, int count)
    {
        var done = await source.ReadBlockAsync(buffer, index, count);
        if (done > 0)
        {
            await destination.WriteAsync(buffer, index, done);
        }
        return done;
    }

    public override async Task<string> ReadToEndAsync()
    {
        var s = await source.ReadToEndAsync();
        await destination.WriteAsync(s);
        return s;
    }

    public override int Peek() => source.Peek();

    public override string ReadToEnd()
    {
        string s = source.ReadToEnd();
        destination.Write(s);
        return s;
    }

    public override string ReadLine()
    {
        var s = source.ReadLine();
        destination.WriteLine(s);
        return s;
    }

    public override async Task<int> ReadAsync(char[] buffer, int index, int count)
    {
        int done = await source.ReadAsync(buffer, index, count);
        if (done > 0)
        {
            await destination.WriteAsync(buffer, index, done);
        }
        return done;
    }

    public override int Read(char[] buffer, int index, int count)
    {
        int done = source.Read(buffer, index, count);
        if (done > 0)
        {
            destination.Write(buffer, index, done);
        }
        return done;
    }

    protected override void Dispose(bool disposing)
    {
        try
        {
            source.Dispose();
        }
        catch (Exception) { }

        try
        {
            destination.Flush();
            destination.Dispose();
        }
        catch (Exception) { }
    }

    public Encoding CurrentEncoding => ((StreamReader)source).CurrentEncoding;

    public Stream BaseStream => ((StreamReader)source).BaseStream;
}
