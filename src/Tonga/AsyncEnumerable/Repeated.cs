using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Tonga.AsyncEnumerable;

/// <summary>
/// A <see cref="IAsyncEnumerable{T}"/> which repeats one element multiple times.
/// </summary>
public sealed class Repeated<T>(Func<CancellationToken, ValueTask<T>> elm, Func<int> cnt) : IAsyncEnumerable<T>
{
    /// <summary>
    /// A <see cref="IAsyncEnumerable{T}"/> which repeats one element multiple times.
    /// </summary>
    public Repeated(Func<CancellationToken, ValueTask<T>> elm, int cnt) : this(elm, () => cnt)
    { }

    /// <summary>
    /// A <see cref="IAsyncEnumerable{T}"/> which repeats one element multiple times.
    /// </summary>
    public Repeated(T elm, int cnt) : this(_ => new ValueTask<T>(elm), () => cnt)
    { }

    /// <summary>
    /// A <see cref="IAsyncEnumerable{T}"/> which repeats one element multiple times.
    /// </summary>
    public Repeated(T elm, Func<int> cnt) : this(_ => new ValueTask<T>(elm), cnt)
    { }

    /// <summary>
    /// A <see cref="IAsyncEnumerable{T}"/> which repeats one element multiple times.
    /// </summary>
    public Repeated(IAsyncScalar<T> elm, int cnt) : this(elm.Value, () => cnt)
    { }

    public async IAsyncEnumerator<T> GetAsyncEnumerator(CancellationToken cancellation = default)
    {
        var times = cnt();
        var element = await elm(cancellation);
        for (var i = 0; i < times; i++)
        {
            yield return element;
        }
    }
}

public static partial class AsyncEnumerableSmarts
{
    /// <summary>
    /// A <see cref="IAsyncEnumerable{T}"/> which repeats one element multiple times.
    /// </summary>
    public static IAsyncEnumerable<T> AsAsyncRepeated<T>(this T elm, int cnt) => new Repeated<T>(elm, cnt);

    /// <summary>
    /// A <see cref="IAsyncEnumerable{T}"/> which repeats one element multiple times.
    /// </summary>
    public static IAsyncEnumerable<T> AsAsyncRepeated<T>(this T elm, Func<int> repeats) =>
        new Repeated<T>(elm, repeats);

    /// <summary>
    /// A <see cref="IAsyncEnumerable{T}"/> which repeats one element multiple times.
    /// </summary>
    public static IAsyncEnumerable<T> AsAsyncRepeated<T>(this IAsyncScalar<T> elm, int cnt) =>
        new Repeated<T>(elm, cnt);
}
