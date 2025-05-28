using System;
using System.Collections.Generic;
using Tonga.Enumerable;

namespace Tonga.Fact;

/// <summary> Logical AND </summary>
public sealed class And : FactEnvelope
{
    /// <summary> Logical AND </summary>
    public And(params Func<bool>[] funcs) : this(funcs.AsEnumerable())
    { }

    /// <summary> Logical AND </summary>
    public And(IEnumerable<Func<bool>> funcs) : this(
        funcs.AsMapped(func => new AsFact(func))
    )
    { }

    /// <summary> Logical AND </summary>
    public And(params IFact[] src) : this(
        src.AsEnumerable()
    )
    { }

    /// <summary> Logical AND </summary>
    public And(params bool[] src) : this(
        src.AsMapped(tBool => new AsFact(tBool))
    )
    { }

    /// <summary> Logical AND </summary>
    /// <param name="src"> list of items </param>
    public And(IEnumerable<bool> src) : this(
        src.AsMapped(tBool => new AsFact(tBool))
    )
    { }

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
