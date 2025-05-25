

using System;

namespace Tonga.Scalar;

/// <summary>
/// A conditional value.
/// </summary>
/// <typeparam name="Out">type of output</typeparam>
public sealed class Conditional<Out>(Func<bool> condition, Func<Out> consequent, Func<Out> alternative)
    : ScalarEnvelope<Out>(() =>
        condition() ? consequent() : alternative()
    )
{
    /// <summary>
    /// A conditional value.
    /// </summary>
    /// <param name="condition">condition</param>
    /// <param name="consequent">consequent</param>
    /// <param name="alternative">alternative</param>
    public Conditional(
        IFact condition,
        Func<Out> consequent,
        Func<Out> alternative
    ) : this(
        () => condition.IsTrue(),
        consequent,
        alternative
    )
    { }

    /// <summary>
    /// A ternary operation using the given input and functions.
    /// </summary>
    /// <param name="condition">condition</param>
    /// <param name="consequent">consequent</param>
    /// <param name="alternative">alternative</param>
    public Conditional(IFunc<Boolean> condition, IFunc<Out> consequent, IFunc<Out> alternative)
        : this(
            condition.Invoke(),
            consequent.Invoke(),
            alternative.Invoke()
        )
    { }

    /// <summary>
    /// A ternary operation using the given input and functions.
    /// </summary>
    /// <param name="condition">condition</param>
    /// <param name="consequent">consequent</param>
    /// <param name="alternative">alternative</param>
    public Conditional(Boolean condition, Out consequent, Out alternative) : this(
        () => condition, () => consequent, () => alternative
    )
    { }

    /// <summary>
    /// A ternary operation using the given input and functions.
    /// </summary>
    /// <param name="condition">condition</param>
    /// <param name="consequent">consequent</param>
    /// <param name="alternative">alternative</param>
    public Conditional(IFact condition, Out consequent, Out alternative) : this(
        condition.IsTrue, () => consequent, () => alternative
    )
    { }
}

public static class Conditional
{
    /// <summary>
    /// A ternary operation using the given input and functions.
    /// </summary>
    /// <param name="condition">condition</param>
    /// <param name="consequent">consequent</param>
    /// <param name="alternative">alternative</param>
    public static IScalar<Out> _<Out>(
        Func<Boolean> condition,
        Func<Out> consequent,
        Func<Out> alternative
    ) =>
        new Conditional<Out>(condition, consequent, alternative);

    /// <summary>
    /// A ternary operation using the given input and functions.
    /// </summary>
    /// <param name="condition">condition</param>
    /// <param name="consequent">consequent</param>
    /// <param name="alternative">alternative</param>
    public static IScalar<Out> _<Out>(IFunc<Boolean> condition, IFunc<Out> consequent, IFunc<Out> alternative)
        => new Conditional<Out>(condition, consequent, alternative);

    /// <summary>
    /// A ternary operation using the given input and functions.
    /// </summary>
    /// <param name="condition">condition</param>
    /// <param name="consequent">consequent</param>
    /// <param name="alternative">alternative</param>
    public static IScalar<Out> _<Out>(Boolean condition, Out consequent, Out alternative)
        => new Conditional<Out>(condition, consequent, alternative);

    /// <summary>
    /// A ternary operation using the given input and functions.
    /// </summary>
    /// <param name="condition">condition</param>
    /// <param name="consequent">consequent</param>
    /// <param name="alternative">alternative</param>
    public static IScalar<Out> _<Out>(IFact condition, Out consequent, Out alternative)
        => new Conditional<Out>(condition, consequent, alternative);

    /// <summary>
    /// A ternary operation using the given input and functions.
    /// </summary>
    /// <param name="condition">condition</param>
    /// <param name="consequent">consequent</param>
    /// <param name="alternative">alternative</param>
    public static IScalar<Out> _<Out>(IFact condition,
        IScalar<Out> consequent, IScalar<Out> alternative
    ) =>
        new Conditional<Out>(
            condition.IsTrue,
            consequent.Value,
            alternative.Value
        );
}
