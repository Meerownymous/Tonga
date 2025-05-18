using System;
using System.Collections.Generic;
using Tonga.Enumerable;

namespace Tonga.Fact;

/// <summary> Logical AND </summary>
public sealed class And : FactEnvelope
{
    /// <summary> Logical AND </summary>
    public And(params Func<bool>[] funcs) : this(AsEnumerable._(funcs))
    { }

    /// <summary> Logical AND </summary>
    public And(IEnumerable<Func<bool>> funcs) : this(
        Mapped._(
            func => new AsFact(func),
            funcs
        )
    )
    { }

    /// <summary> Logical AND </summary>
    public And(params IFact[] src) : this(
        AsEnumerable._(src)
    )
    { }

    /// <summary> Logical AND </summary>
    public And(params bool[] src) : this(
        Mapped._(
            tBool => new AsFact(tBool),
            src
        )
    )
    {
    }

    /// <summary> Logical AND </summary>
    /// <param name="src"> list of items </param>
    public And(IEnumerable<bool> src) : this(
        Mapped._(
            tBool => new AsFact(tBool),
            src
        )
    )
    {
    }

    /// <summary> Logical AND </summary>
    public And(IEnumerable<IFact> src) : base(
        new AsFact(() =>
        {
            Boolean result = true;
            foreach (IFact item in src)
            {
                if (item.IsFalse())
                {
                    result = false;
                    break;
                }
            }

            return result;
        })
    )
    { }
}
