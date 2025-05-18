using System;
using System.Collections.Generic;
using Tonga.Enumerable;

namespace Tonga.Fact;

/// <summary>
/// Logical OR.
/// </summary>
public sealed class Or(IEnumerable<IFact> src) : FactEnvelope(
    new AsFact(() =>
    {
        bool foundTrue = false;
        foreach (var item in src)
        {
            if (item.IsTrue())
            {
                foundTrue = true;
                break;
            }
        }
        return foundTrue;
    })
)
{
    /// <summary>
    /// Logical or. Returns true if any calls to <see cref="Func{In, Out}"/>
    /// were true.
    /// </summary>
    /// <param name="funcs">the conditions to apply</param>
    public Or(params Func<bool>[] funcs) : this(AsEnumerable._(funcs))
    { }

    /// <summary>
    /// Logical or. Returns true if any calls to <see cref="Func{Out}"/> were
    /// true.
    /// </summary>
    /// <param name="funcs">the conditions to apply</param>
    public Or(IEnumerable<Func<bool>> funcs) : this(
        Mapped._(
            func => new AsFact(func),
            funcs
        )
    )
    { }

    /// <summary>
    /// ctor
    /// </summary>
    /// <param name="src">list of items</param>
    public Or(params IFact[] src) : this(
        AsEnumerable._(src))
    { }

    /// <summary>
    /// ctor
    /// </summary>
    /// <param name="src">list of items</param>
    public Or(params bool[] src) : this(
        Mapped._(
            item => new AsFact(item),
            src
        )
    )
    { }

    /// <summary>
    /// ctor
    /// </summary>
    /// <param name="src">list of items</param>
    public Or(IEnumerable<bool> src) : this(
        Mapped._(
            item => new AsFact(item),
            src
        )
    )
    { }
}
