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
                    new TextMorph(
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

                AssertText.Equal(
                    content,
                    new TextMorph(uri)
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

                AssertText.Equal(
                    content,
                    new TextMorph(
                        new FileInfo(path.AbsolutePath),
                        Encoding.BigEndianUnicode
                    )
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

                AssertText.Equal(
                    content,
                    new TextMorph(
                        new FileInfo(path.AbsolutePath),
                        Encoding.UTF8
                    )
                );
            },
            path
        ).Invoke();
    }

    [Fact]
    public void ReadsStreamIntoText()
    {
        var content = "hello girl";

        AssertText.Equal(
            content,
            new TextMorph(content.AsStream())
        );
    }

    [Fact]
    public void ReadsInputIntoText()
    {
        var content = "привет, друг!";

        AssertText.Equal(
            content,
            new TextMorph(
                new ConduitMorph(content),
                Encoding.UTF8
            )
        );
    }

    [Fact]
    public void ReadsInputIntoTextWithDefaultCharset()
    {
        var content = "Hello, друг! with default charset";
        AssertText.Equal(
            content,
            new TextMorph(content)
        );
    }

    [Fact]
    public void ReadsDoubleIntoText()
    {
        double doub = 0.2545;
        var content = doub.ToString(CultureInfo.InvariantCulture);

        Assert.Equal(
            content,
            new TextMorph(doub)
        );
    }

    [Fact]
    public void ReadsDoubleWithNumberFormatIntoText()
    {
        Assert.Equal(
            "0.2545",
            new TextMorph(0.2545, new CultureInfo("en-US"))
        );
    }

    [Fact]
    public void ReadsFloatIntoText()
    {
        AssertText.Equal(
            0.2545f.ToString(CultureInfo.InvariantCulture),
            new TextMorph(2545f)
        );
    }

    [Fact]
    public void ReadsFloatWithNumberFormatIntoText()
    {
        AssertText.Equal(
            "0.2545",
            new TextMorph(0.2545f, new CultureInfo("en-US"))
        );
    }

    [Theory]
    [InlineData(true, "True")]
    [InlineData(false, "False")]
    public void ReadsBoolIntoText(bool input, string expected)
    {
        AssertText.Equal(
            expected,
            new TextMorph(input)
        );
    }

    [Theory]
    [InlineData(true, "True")]
    [InlineData(false, "False")]
    public void ReadsBoolIntoTextWithCultureInfo(bool input, string expected)
    {
        AssertText.Equal(
            expected,
            new TextMorph(input, new CultureInfo("en-US"))
        );
    }

    [Fact]
    public void ReadsInputIntoTextWithSmallBuffer()
    {
        var content = "Hi, товарищ! with small buffer";

        AssertText.Equal(
            content,
            new TextMorph(
                new ConduitMorph(content), 2, Encoding.UTF8
            )
        );
    }

    [Fact]
    public void ReadsInputIntoTextWithSmallBufferAndDefaultCharset()
    {
        var content = "Hello, товарищ! with default charset";

        AssertText.Equal(
            content,
            new TextMorph(
                new ConduitMorph(content), 2
            )
        );
    }

    [Fact]
    public void ReadsFromReader()
    {
        String source = "hello, друг!";
        AssertText.Equal(
            Encoding.UTF8.GetString(new BytesMorph(source).Raw()),
            new TextMorph(new StringReader(source), Encoding.UTF8)
        );
    }


    [Fact]
    public void ReadsFromReaderWithDefaultEncoding()
    {
        String source = "hello, друг! with default encoding";
        AssertText.Equal(
            new TextMorph(new StringReader(source)),
            Encoding.UTF8.GetString(new BytesMorph(source).Raw())
        );
    }

    [Fact]
    public void readsEncodedArrayOfCharsIntoText()
    {
        AssertText.Equal(
            "O que sera que sera",
            new TextMorph(
                [
                    'O', ' ', 'q', 'u', 'e', ' ', 's', 'e', 'r', 'a',
                        ' ', 'q', 'u', 'e', ' ', 's', 'e', 'r', 'a'
                ], Encoding.UTF8).Str()
            );
    }

    [Fact]
    public void ReadsAnArrayOfBytes()
    {
        byte[] bytes = [0xCA, 0xFE];
        AssertText.Equal(
            Encoding.UTF8.GetString(bytes),
            new TextMorph(bytes)
        );
    }

    [Fact]
    public void ReadsBytesWithEncoding()
    {
        byte[] bytes = [0xCA, 0xFE];
        AssertText.Equal(
            Encoding.ASCII.GetString(bytes),
            new TextMorph(new BytesMorph(bytes), Encoding.ASCII)
        );
    }

    [Fact]
    public void ComparesWithASubtext()
    {
        Assert.True(
            new Comparable("here to there").CompareTo(
                new SubText("from here to there", 5)
            ) == 0
        );
    }

    [Fact]
    public void ReadsStringBuilder()
    {
        String starts = "Name it, ";
        String ends = "then it exists!";
        AssertText.Equal(
            starts + ends,
            new TextMorph(
                new StringBuilder(starts)
                    .Append(ends)
            )
        );
    }

    [Fact]
    public void PrintsStackTrace()
    {
        AssertText.Contains(
            "It doesn't work at all",
            new TextMorph(
                new IOException(
                    "It doesn't work at all"
                )
            )
        );
    }

    [Fact]
    public void ReadsLongIntoText()
    {
        AssertText.Equal(
            "68574581791096912",
            new TextMorph(68574581791096912)
        );
    }
}
