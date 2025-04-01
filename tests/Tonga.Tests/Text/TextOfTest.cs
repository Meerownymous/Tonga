using System;
using System.Globalization;
using System.IO;
using System.Text;
using Tonga.Bytes;
using Tonga.IO;
using Tonga.Text;
using Xunit;

#pragma warning disable MaxPublicMethodCount // a public methods count maximum
namespace Tonga.Tests.Text;

public sealed class TextOfTest
{
    [Fact]
    public void ReadsUriIntoTextWithEncoding()
    {
        var path = new Uri(Path.GetFullPath("Assets/TextOf/readfile.txt"));
        new Tidy(() =>
            {
                Directory.CreateDirectory(Path.GetDirectoryName(path.AbsolutePath));

                var content = "el file";
                File.WriteAllText(path.AbsolutePath, content, Encoding.BigEndianUnicode);

                Assert.True(
                    AsText._(
                        path,
                        Encoding.BigEndianUnicode
                    ).AsString() == content,
                    "Can't read text from Input");
            },
            path
        ).Invoke();
    }

    [Fact]
    public void ReadsUriIntoText()
    {
        var path = new Uri(Path.GetFullPath("Assets/TextOf/readfile.txt"));
        new Tidy(() =>
            {
                Directory.CreateDirectory(Path.GetDirectoryName(path.AbsolutePath));

                var content = "el file";
                File.WriteAllText(path.AbsolutePath, content);

                Assert.True(
                    AsText._(
                        path
                    ).AsString() == content,
                    "Can't read text from Input");
            },
            path
        ).Invoke();
    }

    [Fact]
    public void ReadsFileIntoTextWithEncoding()
    {
        var path = new Uri(Path.GetFullPath("Assets/TextOf/readfile.txt"));
        new Tidy(() =>
            {
                Directory.CreateDirectory(Path.GetDirectoryName(path.AbsolutePath));

                var content = "el file";
                File.WriteAllText(path.AbsolutePath, content, Encoding.BigEndianUnicode);

                Assert.True(
                    AsText._(
                        new FileInfo(path.AbsolutePath),
                        Encoding.BigEndianUnicode
                    ).AsString() == content,
                    "Can't read text from Input");
            },
            path
        ).Invoke();
    }

    [Fact]
    public void ReadsFileIntoText()
    {
        var path = new Uri(Path.GetFullPath("Assets/TextOf/readfile.txt"));
        new Tidy(() =>
            {
                Directory.CreateDirectory(Path.GetDirectoryName(path.AbsolutePath));

                var content = "el file";
                File.WriteAllText(path.AbsolutePath, content);

                Assert.True(
                    AsText._(
                        new FileInfo(path.AbsolutePath),
                        Encoding.UTF8
                    ).AsString() == content,
                    "Can't read text from Input");
            },
            path
        ).Invoke();
    }

    [Fact]
    public void ReadsStreamIntoText()
    {
        var content = "hello girl";

        Assert.Equal(
            content,
            AsText._(
                new MemoryStream(new AsBytes(content).Bytes())
            ).AsString()
        );
    }

    [Fact]
    public void ReadsInputIntoText()
    {
        var content = "привет, друг!";

        Assert.True(
            AsText._(
                new AsInput(content),
                Encoding.UTF8
            ).AsString() == content,
            "Can't read text from Input");
    }

    [Fact]
    public void ReadsInputIntoTextWithDefaultCharset()
    {
        var content = "Hello, друг! with default charset";
        Assert.True(
            AsText._(
                new AsInput(content)
            ).AsString() == content,
            "Can't read text from Input with default charset");
    }

    [Fact]
    public void ReadsDoubleIntoText()
    {
        double doub = 0.2545;
        var content = doub.ToString(CultureInfo.InvariantCulture);

        Assert.True(
            AsText._(doub).AsString() == content
        );
    }

    [Fact]
    public void ReadsDoubleWithNumberFormatIntoText()
    {
        var content = "0.2545";
        double doub = 0.2545;
        CultureInfo inf = new CultureInfo("en-US");

        Assert.True(
            AsText._(doub,
                inf
            ).AsString() == content,
            "Can't read text from double with format");
    }

    [Fact]
    public void ReadsFloatIntoText()
    {
        float doub = 0.2545f;
        var content = doub.ToString(CultureInfo.InvariantCulture);

        Assert.True(
            AsText._(doub
            ).AsString() == content,
            "Can't read text from float");
    }

    [Fact]
    public void ReadsFloatWithNumberFormatIntoText()
    {
        var content = "0.2545";
        float doub = 0.2545f;

        CultureInfo inf = new CultureInfo("en-US");

        Assert.True(
            AsText._(doub,
                inf
            ).AsString() == content,
            "Can't read text with format from float");
    }

    [Theory]
    [InlineData(true, "True")]
    [InlineData(false, "False")]
    public void ReadsBoolIntoText(bool input, string expected)
    {
        Assert.True(
            AsText._(input).AsString() == expected,
            "Can't read text from bool"
        );
    }

    [Theory]
    [InlineData(true, "True")]
    [InlineData(false, "False")]
    public void ReadsBoolIntoTextWithCultureInfo(bool input, string expected)
    {
        Assert.True(
            AsText._(
                input,
                new CultureInfo("en-US")
            ).AsString() == expected,
            "Can't read text from bool with CultureInfo"
        );
    }

    [Fact]
    public void ReadsInputIntoTextWithSmallBuffer()
    {
        var content = "Hi, товарищ! with small buffer";

        Assert.True(
            AsText._(
                new AsInput(content),
                2,
                Encoding.UTF8
            ).AsString() == content,
            "Can't read text with a small reading buffer");
    }

    [Fact]
    public void ReadsInputIntoTextWithSmallBufferAndDefaultCharset()
    {
        var content = "Hello, товарищ! with default charset";

        Assert.True(
            AsText._(
                new AsInput(content),
                2
            ).AsString() == content,
            "Can't read text with a small reading buffer and default charset");
    }

    [Fact]
    public void ReadsFromReader()
    {
        String source = "hello, друг!";
        Assert.True(
            AsText._(
                new StringReader(source),
                Encoding.UTF8
            ).AsString() == Encoding.UTF8.GetString(new AsBytes(source).Bytes()),
            "Can't read string through a reader");
    }


    [Fact]
    public void ReadsFromReaderWithDefaultEncoding()
    {
        String source = "hello, друг! with default encoding";
        Assert.True(
            AsText._(new StringReader(source)).AsString() ==
            Encoding.UTF8.GetString(new AsBytes(source).Bytes()),
            "Can't read string with default encoding through a reader");
    }

    [Fact]
    public void readsEncodedArrayOfCharsIntoText()
    {
        Assert.True(
            AsText._(
                'O', ' ', 'q', 'u', 'e', ' ', 's', 'e', 'r', 'a',
                ' ', 'q', 'u', 'e', ' ', 's', 'e', 'r', 'a'
            ).AsString().CompareTo("O que sera que sera") == 0,
            "Can't read array of encoded chars into text.");
    }

    [Fact]
    public void ReadsAnArrayOfBytes()
    {
        byte[] bytes = new byte[] { (byte)0xCA, (byte)0xFE };
        Assert.True(
            AsText._(
                bytes
            ).AsString().CompareTo(Encoding.UTF8.GetString(bytes)) == 0,
            "Can't read array of bytes");
    }

    [Fact]
    public void ReadsBytesWithEncoding()
    {
        byte[] bytes = new byte[] { (byte)0xCA, (byte)0xFE };
        Assert.True(
            AsText._(
                new AsBytes(bytes),
                Encoding.ASCII
            ).AsString().CompareTo(Encoding.ASCII.GetString(bytes)) == 0,
            "Can't read array of bytes");
    }

    [Fact]
    public void ComparesWithASubtext()
    {
        Assert.True(
            new Comparable(AsText._("here to there")).CompareTo(
                new SubText("from here to there", 5)
            ) == 0,
            "Can't compare sub texts");
    }

    [Fact]
    public void ReadsStringBuilder()
    {
        String starts = "Name it, ";
        String ends = "then it exists!";
        Assert.True(
            AsText._(
                new StringBuilder(starts).Append(ends)
            ).AsString() == starts + ends,
            "Can't process a string builder");
    }

    [Fact]
    public void PrintsStackTrace()
    {
        Assert.True(
            AsText._(
                new IOException(
                    "It doesn't work at all"
                )
            ).AsString().Contains("It doesn't work at all"),
            "Can't print exception stacktrace");
    }

    [Fact]
    public void ReadsLongIntoText()
    {
        long value = 68574581791096912;
        var text = "68574581791096912";
        Assert.True(
            AsText._(
                value
            ).AsString() == text,
            "Can't read long into text"
        );
    }
}
#pragma warning restore MaxPublicMethodCount // a public methods count maximum
