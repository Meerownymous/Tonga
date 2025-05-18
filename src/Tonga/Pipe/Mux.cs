using System;
using System.Collections.Generic;
using Tonga.Enumerable;

namespace Tonga.Pipe;

/// <summary>
/// Multiplexer which outputs based on condition.
/// </summary>
public class Mux<TInput, TOutput>(
    IEnumerable<(Func<TInput, bool> condition, Func<TInput, TOutput> consequence)> paths,
    Func<TInput, TOutput> fbk
) : IPipe<TInput, TOutput>
{
    /// <summary>
    /// Multiplexer which outputs based on condition.
    /// </summary>
    public Mux(
        (Func<TInput, bool> condition, Func<TInput, TOutput> consequence)[] paths,
        Func<TInput, TOutput> fbk
    ) : this(
        new AsEnumerable<(Func<TInput, bool> condition, Func<TInput, TOutput> consequence)>(paths), fbk
    ){ }

    /// <summary>
    /// Multiplexer which outputs based on condition.
    /// </summary>
    public Mux(params (Func<TInput, bool> condition, Func<TInput, TOutput> consequence)[] paths) : this(
        paths, noMatch => throw new ArgumentException($"Cannot find a matching case for the input ({noMatch})")
    ){ }

    /// <summary>
    /// Multiplexer which outputs based on condition.
    /// </summary>
    public Mux(IEnumerable<(Func<TInput, bool> condition, Func<TInput, TOutput> consequence)> paths) : this(
       paths, noMatch => throw new ArgumentException($"Cannot find a matching case for the input ({noMatch})")
    ){ }

    /// <summary>
    /// Multiplexer which outputs based on condition.
    /// </summary>
    public Mux(IEnumerable<(IFact fact, Func<TInput, TOutput> consequence)> paths, Func<TInput, TOutput> fbk) : this(
        new Mapped<
            (IFact fact, Func<TInput, TOutput> consequence),
            (Func<TInput, bool> condition, Func<TInput, TOutput> consequence)
        >
        (
            path => (_ => path.fact.IsTrue(), path.consequence),
            paths
        ),
        fbk
    ){ }

    /// <summary>
    /// Multiplexer which outputs based on condition.
    /// </summary>
    public Mux(params (IFact fact, Func<TInput, TOutput> consequence)[] paths) : this(
        paths,
        noMatch => throw new ArgumentException($"Cannot find a matching case for the input ({noMatch})")
    ){ }

    /// <summary>
    /// Multiplexer which outputs based on condition.
    /// </summary>
    public Mux(IEnumerable<(IFact fact, Func<TInput, TOutput> consequence)> paths) : this(
        paths,
        noMatch => throw new ArgumentException($"Cannot find a matching case for the input ({noMatch})")
    ){ }

    /// <summary>
    /// Multiplexer which outputs based on condition.
    /// </summary>
    public Mux(IEnumerable<(IFact fact, Func<TOutput> consequence)> factPaths, Func<TOutput> fbk) : this(
        new Mapped<
            (IFact fact, Func<TOutput> consequence),
            (Func<TInput, bool> condition, Func<TInput, TOutput> consequence)
        >
        (
            path => (_ => path.fact.IsTrue(), _ => path.consequence()),
            factPaths
        ),
        _ => fbk()
    ){ }

    /// <summary>
    /// Multiplexer which outputs based on condition.
    /// </summary>
    public Mux((IFact fact, Func<TOutput> consequence)[] paths, Func<TOutput> fbk) : this(
        new Mapped<
            (IFact fact, Func<TOutput> consequence),
            (Func<TInput, bool> condition, Func<TInput, TOutput> consequence)
        >
        (
            path => (_ => path.fact.IsTrue(), _ => path.consequence()),
            paths
        ),
        _ => fbk()
    ){ }

    /// <summary>
    /// Multiplexer which outputs based on condition.
    /// </summary>
    public Mux((IFact fact, TOutput consequence)[] fixedFactPaths, Func<TOutput> fbk) : this(
        new Mapped<
            (IFact fact, TOutput consequence),
            (Func<TInput, bool> condition, Func<TInput, TOutput> consequence)
        >
        (
            path => (_ => path.fact.IsTrue(), _ => path.consequence),
            fixedFactPaths
        ),
        _ => fbk()
    ){ }

    /// <summary>
    /// Multiplexer which outputs based on condition.
    /// </summary>
    public Mux(params (IFact fact, TOutput consequence)[] paths) : this(
        fixedFactPaths: paths,
        fbk: () => throw new ArgumentException($"Cannot find a matching case.")
    ){ }

    /// <summary>
    /// Multiplexer which outputs based on condition.
    /// </summary>
    public Mux(params (IFact fact, Func<TOutput> consequence)[] paths) : this(
        factPaths: new AsEnumerable<(IFact condition, Func<TOutput> consequence)>(paths),
        fbk: () => throw new ArgumentException("Cannot find a matching case")
    ){ }

    public TOutput Yield(TInput input)
    {
        bool matched = false;
        TOutput result = default;
        foreach (var path in paths)
        {
            if (path.condition(input))
            {
                result = path.consequence(input);
                matched = true;
                break;
            }
        }
        if (!matched)
            result = fbk(input);
        return result;
    }
}
