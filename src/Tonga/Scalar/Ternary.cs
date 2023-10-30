

using System;
using Tonga.Func;

namespace Tonga.Scalar
{
    /// <summary>
    /// A ternary operation.
    /// </summary>
    /// <typeparam name="In">type of input</typeparam>
    /// <typeparam name="Out">type of output</typeparam>
    public sealed class Ternary<In, Out> : ScalarEnvelope<Out>
    {
        /// <summary>
        /// A ternary operation using the given input and functions.
        /// </summary>
        /// <param name="input">input</param>
        /// <param name="condition">condition</param>
        /// <param name="consequent">consequent</param>
        /// <param name="alternative">alternative</param>
        public Ternary(
            In input,
            Func<In, Boolean> condition,
            Func<In, Out> consequent,
            Func<In, Out> alternative
        ) : this(input,
                new FuncOf<In, Boolean>(condition),
                new FuncOf<In, Out>(consequent),
                new FuncOf<In, Out>(alternative)
            )
        { }

        /// <summary>
        /// A ternary operation using the given input and functions.
        /// </summary>
        /// <param name="input">input</param>
        /// <param name="condition">condition</param>
        /// <param name="consequent">consequent</param>
        /// <param name="alternative">alternative</param>
        public Ternary(In input, IFunc<In, Boolean> condition, IFunc<In, Out> consequent, IFunc<In, Out> alternative)
            : this(
                AsScalar._(() => condition.Invoke(input)),
                AsScalar._(() => consequent.Invoke(input)),
                AsScalar._(() => alternative.Invoke(input))
            )
        { }

        /// <summary>
        /// A ternary operation using the given input and functions.
        /// </summary>
        /// <param name="condition">condition</param>
        /// <param name="consequent">consequent</param>
        /// <param name="alternative">alternative</param>
        public Ternary(Boolean condition, Out consequent, Out alternative) : this(
            AsScalar._(condition), consequent, alternative)
        { }

        /// <summary>
        /// A ternary operation using the given input and functions.
        /// </summary>
        /// <param name="condition">condition</param>
        /// <param name="consequent">consequent</param>
        /// <param name="alternative">alternative</param>
        public Ternary(IScalar<bool> condition, Out consequent, Out alternative) : this(
            condition, AsScalar._(consequent), AsScalar._(alternative)
        )
        { }

        /// <summary>
        /// A ternary operation using the given input and functions.
        /// </summary>
        /// <param name="condition">condition</param>
        /// <param name="consequent">consequent</param>
        /// <param name="alternative">alternative</param>
        public Ternary(IScalar<bool> condition, IScalar<Out> consequent, IScalar<Out> alternative)
            : base(() =>
            {
                IScalar<Out> result;
                if (condition.Value())
                {
                    result = consequent;
                }
                else
                {
                    result = alternative;
                }
                return result.Value();
            })
        { }
    }

    public static class Ternary
    {
        /// <summary>
        /// A ternary operation using the given input and functions.
        /// </summary>
        /// <param name="input">input</param>
        /// <param name="condition">condition</param>
        /// <param name="consequent">consequent</param>
        /// <param name="alternative">alternative</param>
        public static IScalar<Out> _<In, Out>(
            In input,
            Func<In, Boolean> condition,
            Func<In, Out> consequent,
            Func<In, Out> alternative
        ) =>
            new Ternary<In, Out>(input, condition, consequent, alternative);

        /// <summary>
        /// A ternary operation using the given input and functions.
        /// </summary>
        /// <param name="input">input</param>
        /// <param name="condition">condition</param>
        /// <param name="consequent">consequent</param>
        /// <param name="alternative">alternative</param>
        public static IScalar<Out> _<In, Out>(In input, IFunc<In, Boolean> condition, IFunc<In, Out> consequent, IFunc<In, Out> alternative)
            => new Ternary<In, Out>(input, condition, consequent, alternative);

        /// <summary>
        /// A ternary operation using the given input and functions.
        /// </summary>
        /// <param name="condition">condition</param>
        /// <param name="consequent">consequent</param>
        /// <param name="alternative">alternative</param>
        public static IScalar<Out> From<In, Out>(Boolean condition, Out consequent, Out alternative)
            => new Ternary<In, Out>(condition, consequent, alternative);

        /// <summary>
        /// A ternary operation using the given input and functions.
        /// </summary>
        /// <param name="condition">condition</param>
        /// <param name="consequent">consequent</param>
        /// <param name="alternative">alternative</param>
        public static IScalar<Out> _<In, Out>(IScalar<bool> condition, Out consequent, Out alternative)
            => new Ternary<In, Out>(condition, consequent, alternative);

        /// <summary>
        /// A ternary operation using the given input and functions.
        /// </summary>
        /// <param name="condition">condition</param>
        /// <param name="consequent">consequent</param>
        /// <param name="alternative">alternative</param>
        public static IScalar<Out> _<In, Out>(IScalar<bool> condition,
            IScalar<Out> consequent, IScalar<Out> alternative
        )
            => new Ternary<In, Out>(condition, consequent, alternative);
    }
}
