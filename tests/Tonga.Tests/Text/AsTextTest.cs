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

public sealed class AsTextTest
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

                Assert.Equal(
                    content,
                    new AsText(
                        path,
                        Encoding.BigEndianUnicode
                    ).Str()
                );
            },
            path
        ).Invoke();
    }

    [Fact]
    public void ReadsUriIntoText()
    {
        var uri = new Uri(Path.GetFullPath("Assets/TextOf/readfile.txt"));
        new Tidy(() =>
            {
                Directory.CreateDirectory(Path.GetDirectoryName(uri.AbsolutePath));

                var content = "el file";
                File.WriteAllText(uri.AbsolutePath, content);

                Assert.Equal(
                    content,
                    uri.AsText().Str()
                );
            },
            uri
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

                Assert.Equal(
                    content,
                    new FileInfo(path.AbsolutePath)
                        .AsText(Encoding.BigEndianUnicode)
                        .Str()
                );
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

                Assert.Equal(
                    content,
                    new FileInfo(path.AbsolutePath)
                        .AsText(Encoding.UTF8).Str()
                );
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
            content
                .AsStream()
                .AsText()
                .Str()
        );
    }

    [Fact]
    public void ReadsInputIntoText()
    {
        var content = "привет, друг!";

        Assert.Equal(
            content,
            content.AsConduit()
                .AsText(Encoding.UTF8)
                .Str()
        );
    }

    [Fact]
    public void ReadsInputIntoTextWithDefaultCharset()
    {
        var content = "Hello, друг! with default charset";
        Assert.Equal(
            content,
            content.AsConduit()
                .AsText()
                .Str()
        );
    }

    [Fact]
    public void ReadsDoubleIntoText()
    {
        double doub = 0.2545;
        var content = doub.ToString(CultureInfo.InvariantCulture);

        Assert.Equal(
            content,
            doub.AsText().Str()
        );
    }

    [Fact]
    public void ReadsDoubleWithNumberFormatIntoText()
    {
        Assert.Equal(
            "0.2545",
            0.2545.AsText(new CultureInfo("en-US"))
            .Str()
        );
    }

    [Fact]
    public void ReadsFloatIntoText()
    {
        Assert.Equal(
            0.2545f.ToString(CultureInfo.InvariantCulture),
            0.2545f.AsText()
                .Str()
        );
    }

    [Fact]
    public void ReadsFloatWithNumberFormatIntoText()
    {
        Assert.Equal(
            "0.2545",
            0.2545f.AsText(new CultureInfo("en-US"))
                .Str()
        );
    }

    [Theory]
    [InlineData(true, "True")]
    [InlineData(false, "False")]
    public void ReadsBoolIntoText(bool input, string expected)
    {
        Assert.Equal(
            expected,
            input.AsText().Str()
        );
    }

    [Theory]
    [InlineData(true, "True")]
    [InlineData(false, "False")]
    public void ReadsBoolIntoTextWithCultureInfo(bool input, string expected)
    {
        Assert.Equal(
            expected,
            input.AsText(new CultureInfo("en-US"))
                .Str()
        );
    }

    [Fact]
    public void ReadsInputIntoTextWithSmallBuffer()
    {
        var content = "Hi, товарищ! with small buffer";

        Assert.Equal(
            content,
            content.AsConduit()
                .AsText(2,Encoding.UTF8)
                .Str()
        );
    }

    [Fact]
    public void ReadsInputIntoTextWithSmallBufferAndDefaultCharset()
    {
        var content = "Hello, товарищ! with default charset";

        Assert.Equal(
            content,
            content.AsConduit()
                .AsText(2)
                .Str()
        );
    }

    [Fact]
    public void ReadsFromReader()
    {
        String source = "hello, друг!";
        Assert.Equal(
            Encoding.UTF8.GetString(new AsBytes(source).Raw()),
            new StringReader(source).AsText(Encoding.UTF8)
                .Str()
        );
    }


    [Fact]
    public void ReadsFromReaderWithDefaultEncoding()
    {
        String source = "hello, друг! with default encoding";
        Assert.Equal(
            new StringReader(source)
                .AsText()
                .Str(),
            Encoding.UTF8.GetString(new AsBytes(source).Raw())
        );
    }

    [Fact]
    public void readsEncodedArrayOfCharsIntoText()
    {
        Assert.Equal(
            "O que sera que sera",
            new[]
            {
                'O', ' ', 'q', 'u', 'e', ' ', 's', 'e', 'r', 'a',
                ' ', 'q', 'u', 'e', ' ', 's', 'e', 'r', 'a'
            }.AsText(Encoding.UTF8).Str());
    }

    [Fact]
    public void ReadsAnArrayOfBytes()
    {
        byte[] bytes = [0xCA, 0xFE];
        Assert.Equal(
            Encoding.UTF8.GetString(bytes),
            bytes.AsText().Str()
        );
    }

    [Fact]
    public void ReadsBytesWithEncoding()
    {
        byte[] bytes = [0xCA, 0xFE];
        Assert.Equal(
            Encoding.ASCII.GetString(bytes),
            new AsBytes(bytes)
                .AsText(Encoding.ASCII)
                .Str()
        );
    }

    [Fact]
    public void ComparesWithASubtext()
    {
        Assert.True(
            new Comparable("here to there".AsText()).CompareTo(
                new SubText("from here to there", 5)
            ) == 0
        );
    }

    [Fact]
    public void ReadsStringBuilder()
    {
        String starts = "Name it, ";
        String ends = "then it exists!";
        Assert.Equal(
            starts + ends,
            new StringBuilder(starts)
                .Append(ends)
                .AsText()
                .Str()

        );
    }

    [Fact]
    public void PrintsStackTrace()
    {
        Assert.Contains(
            "It doesn't work at all",
            new IOException(
                "It doesn't work at all"
            ).AsText()
            .Str()
        );
    }

    [Fact]
    public void ReadsLongIntoText()
    {
        Assert.Equal(
            "68574581791096912",
            68574581791096912
                .AsText()
                .Str()
        );
    }
}
