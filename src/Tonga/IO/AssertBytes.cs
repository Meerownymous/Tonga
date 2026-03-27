using Tonga.Bytes;
using Xunit;

namespace Tonga.IO;

public static class AssertBytes
{
    public static void Equal(IBytes expected, IBytes actual) =>
        Assert.Equal(expected.Raw(), actual.Raw());

    public static void NotEqual(IBytes expected, IBytes actual) =>
        Assert.NotEqual(expected.Raw(), actual.Raw());

    public static void Empty(IBytes actual) =>
        Assert.Empty(actual.Raw());

    public static void NotEmpty(IBytes actual) =>
        Assert.NotEmpty(actual.Raw());

    public static void Contains(byte expected, IBytes actual) =>
        Assert.Contains(expected, actual.Raw());

    public static void DoesNotContain(byte expected, IBytes actual) =>
        Assert.DoesNotContain(expected, actual.Raw());

    public static void Single(IBytes actual) =>
        Assert.Single(actual.Raw());

    public static void Length(int expectedLength, IBytes actual) =>
        Assert.Equal(expectedLength, actual.Raw().Length);
}
