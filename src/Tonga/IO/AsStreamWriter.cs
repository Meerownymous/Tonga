

using System;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace Tonga.IO;

/// <summary>
/// A <see cref="StreamWriter"/> to a target.
/// </summary>
public sealed class AsStreamWriter(Func<StreamWriter> tgt) : StreamWriter(new DeadStream())
{
    /// <summary>
    /// the target
    /// </summary>
    private readonly Lazy<StreamWriter> target = new(tgt);

    /// <summary>
    /// A <see cref="StreamWriter"/> to a file <see cref="Uri"/>.
    /// </summary>
    /// <param name="path">a file Uri. Get with Path.GetFullPath(relOrAbsPath) or prefix with file:/// </param>
    public AsStreamWriter(Uri path) : this(new AsConduit(path))
    { }

    /// <summary>
    /// A <see cref="StreamWriter"/> to a <see cref="Stream"/>.
    /// </summary>
    /// <param name="stream">the output stream</param>
    public AsStreamWriter(Stream stream) : this(new AsConduit(stream))
    { }

    /// <summary>
    /// A <see cref="StreamWriter"/> to a <see cref="IConduit"/>.
    /// </summary>
    /// <param name="output">the output</param>
    public AsStreamWriter(IConduit output) : this(output, Encoding.UTF8)
    { }

    /// <summary>
    /// A <see cref="StreamWriter"/> to a <see cref="IConduit"/>.
    /// </summary>
    /// <param name="output">the output</param>
    /// <param name="enc">encoding of the output</param>
    public AsStreamWriter(IConduit output, Encoding enc) : this(
        () => new StreamWriter(output.Stream(), enc))
    { }

    /// <summary>
    /// A <see cref="StreamWriter"/> to a <see cref="IConduit"/>.
    /// </summary>
    /// <param name="output">the output</param>
    /// <param name="encoding">encoding of the output</param>
    public AsStreamWriter(IConduit output, string encoding) : this(
         () => new StreamWriter(output.Stream(), Encoding.GetEncoding(encoding)))
    { }

    #pragma warning disable CS1591
    public override void Write(char[] cbuf) => target.Value.Write(cbuf);
    public override async Task FlushAsync() => target.Value.FlushAsync();
    public override void Write(bool value) =>  target.Value.Write(value);

    public override void Write(char value) => target.Value.Write(value);

    public override void Write(char[] buffer, int index, int count) =>
        target.Value.Write(buffer, index, count);

    public override void Write(decimal value) => target.Value.Write(value);
    public override void Write(double value) => target.Value.Write(value);
    public override void Write(int value) => target.Value.Write(value);
    public override void Write(long value) => target.Value.Write(value);
    public override void Write(object value) => target.Value.Write(value);

    public override void Write(string format, object arg0) => target.Value.Write(format, arg0);

    public override void Write(string format, object arg0, object arg1) =>
        target.Value.Write(format, arg0, arg1);

    public override void Write(string format, object arg0, object arg1, object arg2) =>
        target.Value.Write(format, arg0, arg1, arg2);
    public override void Write(string format, params object[] arg) => target.Value.Write(format, arg);
    public override void Write(string value) => target.Value.Write(value);
    public override void Write(uint value) => target.Value.Write(value);
    public override void Write(ulong value) => target.Value.Write(value);

    public override async Task WriteAsync(char value) =>
        await target.Value.WriteAsync(value);
    public override async Task WriteAsync(char[] buffer, int index, int count) =>
        await target.Value.WriteAsync(buffer, index, count);
    public override async Task WriteAsync(string value) =>
        await target.Value.WriteAsync(value);

    public override void WriteLine() => target.Value.WriteLine();
    public override void WriteLine(bool value) => target.Value.WriteLine(value);
    public override void WriteLine(char value) => target.Value.WriteLine(value);
    public override void WriteLine(char[] buffer) => target.Value.WriteLine(buffer);
    public override void WriteLine(char[] buffer, int index, int count) =>
        target.Value.WriteLine(buffer, index, count);

    public override void WriteLine(decimal value) => target.Value.WriteLine(value);
    public override void WriteLine(double value) => target.Value.WriteLine(value);
    public override void WriteLine(float value) => target.Value.WriteLine(value);
    public override void WriteLine(int value) => target.Value.WriteLine(value);
    public override void WriteLine(long value) => target.Value.WriteLine(value);
    public override void WriteLine(object value) => target.Value.WriteLine(value);
    public override void WriteLine(string format, object arg0) =>
        target.Value.WriteLine(format, arg0);
    public override void WriteLine(string format, object arg0, object arg1) =>
        target.Value.WriteLine(format, arg0, arg1);
    public override void WriteLine(string format, object arg0, object arg1, object arg2) =>
        target.Value.WriteLine(format, arg0, arg1, arg2);
    public override void WriteLine(string format, params object[] arg) =>
        target.Value.WriteLine(format, arg);
    public override void WriteLine(string value) =>
        target.Value.WriteLine(value);
    public override void WriteLine(uint value) =>
        target.Value.WriteLine(value);
    public override void WriteLine(ulong value) =>
        target.Value.WriteLine(value);
    public override async Task WriteLineAsync() =>
        await this.target.Value.WriteLineAsync();
    public override async Task WriteLineAsync(char value) =>
        await base.WriteLineAsync(value);
    public override async Task WriteLineAsync(char[] buffer, int index, int count) =>
        await this.target.Value.WriteLineAsync(buffer, index, count);
    public override async Task WriteLineAsync(string value) =>
        await this.target.Value.WriteLineAsync(value);

    public override string NewLine
    {
        get => target.Value.NewLine;
        set => target.Value.NewLine = value;
    }

    public override void Write(float value) => target.Value.Write(value);
    public override void Flush() => target.Value.Flush();

    protected override void Dispose(bool disposing)
    {
        try
        {
            target.Value.Flush();
        }
        catch (Exception) { }

        try
        {
            //this._target.Value().BaseStream.Dispose();
            ((IDisposable)target.Value).Dispose();
        }
        catch (Exception) { }
        base.Dispose(disposing);
    }
}
