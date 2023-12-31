using System;
using System.Collections.Generic;
using Tonga.Enumerable;
using Tonga.IO;
using Tonga.Map;

namespace Tonga.Swap
{
    /// <summary>
    /// A swap that is chosen by given condition.
    /// </summary> 
    public class SwapSwitch<TInput, TOutput> : ISwap<string, TInput, TOutput>
    {
        private readonly IMap<string, ISwap<TInput, TOutput>> swaps;

        /// <summary>
        /// A set of conversions where the desired is selected by its name.
        /// </summary>
        public SwapSwitch(
            string key1, ISwap<TInput, TOutput> swap1,
            string key2, ISwap<TInput, TOutput> swap2
        ) : this(
            AsPair._(key1, swap1),
            AsPair._(key2, swap2)
        )
        { }

        /// <summary>
        /// A set of conversions where the desired is selected by its name.
        /// </summary>
        public SwapSwitch(
            string key1, ISwap<TInput, TOutput> swap1,
            string key2, ISwap<TInput, TOutput> swap2,
            string key3, ISwap<TInput, TOutput> swap3
        ) : this(
            AsPair._(key1, swap1),
            AsPair._(key2, swap2),
            AsPair._(key3, swap3)
        )
        { }

        /// <summary>
        /// A set of conversions where the desired is selected by its name.
        /// </summary>
        public SwapSwitch(
            string key1, ISwap<TInput, TOutput> swap1,
            string key2, ISwap<TInput, TOutput> swap2,
            string key3, ISwap<TInput, TOutput> swap3,
            string key4, ISwap<TInput, TOutput> swap4
        ) : this(
            AsPair._(key1, swap1),
            AsPair._(key2, swap2),
            AsPair._(key3, swap3),
            AsPair._(key4, swap4)
        )
        { }

        /// <summary>
        /// A set of conversions where the desired is selected by its name.
        /// </summary>
        public SwapSwitch(
            string key1, ISwap<TInput, TOutput> swap1,
            string key2, ISwap<TInput, TOutput> swap2,
            string key3, ISwap<TInput, TOutput> swap3,
            string key4, ISwap<TInput, TOutput> swap4,
            string key5, ISwap<TInput, TOutput> swap5
        ) : this(
            AsPair._(key1, swap1),
            AsPair._(key2, swap2),
            AsPair._(key3, swap3),
            AsPair._(key4, swap4),
            AsPair._(key5, swap5)
        )
        { }

        /// <summary>
        /// A set of conversions where the desired is selected by its name.
        /// </summary>
        public SwapSwitch(
            string key1, ISwap<TInput, TOutput> swap1,
            string key2, ISwap<TInput, TOutput> swap2,
            string key3, ISwap<TInput, TOutput> swap3,
            string key4, ISwap<TInput, TOutput> swap4,
            string key5, ISwap<TInput, TOutput> swap5,
            string key6, ISwap<TInput, TOutput> swap6
        ) : this(
            AsPair._(key1, swap1),
            AsPair._(key2, swap2),
            AsPair._(key3, swap3),
            AsPair._(key4, swap4),
            AsPair._(key5, swap5),
            AsPair._(key6, swap6)
        )
        { }

        /// <summary>
        /// A set of conversions where the desired is selected by its name.
        /// </summary>
        public SwapSwitch(
            string key1, ISwap<TInput, TOutput> swap1,
            string key2, ISwap<TInput, TOutput> swap2,
            string key3, ISwap<TInput, TOutput> swap3,
            string key4, ISwap<TInput, TOutput> swap4,
            string key5, ISwap<TInput, TOutput> swap5,
            string key6, ISwap<TInput, TOutput> swap6,
            string key7, ISwap<TInput, TOutput> swap7
        ) : this(
            AsPair._(key1, swap1),
            AsPair._(key2, swap2),
            AsPair._(key3, swap3),
            AsPair._(key4, swap4),
            AsPair._(key5, swap5),
            AsPair._(key6, swap6),
            AsPair._(key7, swap7)
        )
        { }

        /// <summary>
        /// A set of conversions where the desired is selected by its name.
        /// </summary>
        public SwapSwitch(
            string key1, ISwap<TInput, TOutput> swap1,
            string key2, ISwap<TInput, TOutput> swap2,
            string key3, ISwap<TInput, TOutput> swap3,
            string key4, ISwap<TInput, TOutput> swap4,
            string key5, ISwap<TInput, TOutput> swap5,
            string key6, ISwap<TInput, TOutput> swap6,
            string key7, ISwap<TInput, TOutput> swap7,
            string key8, ISwap<TInput, TOutput> swap8
        ) : this(
            AsPair._(key1, swap1),
            AsPair._(key2, swap2),
            AsPair._(key3, swap3),
            AsPair._(key4, swap4),
            AsPair._(key5, swap5),
            AsPair._(key6, swap6),
            AsPair._(key7, swap7),
            AsPair._(key8, swap8)
        )
        { }

        /// <summary>
        /// A set of conversions where the desired is selected by its name.
        /// </summary>
        public SwapSwitch(
            string key1, ISwap<TInput, TOutput> swap1,
            string key2, ISwap<TInput, TOutput> swap2,
            string key3, ISwap<TInput, TOutput> swap3,
            string key4, ISwap<TInput, TOutput> swap4,
            string key5, ISwap<TInput, TOutput> swap5,
            string key6, ISwap<TInput, TOutput> swap6,
            string key7, ISwap<TInput, TOutput> swap7,
            string key8, ISwap<TInput, TOutput> swap8,
            string key9, ISwap<TInput, TOutput> swap9
        ) : this(
            AsPair._(key1, swap1),
            AsPair._(key2, swap2),
            AsPair._(key3, swap3),
            AsPair._(key4, swap4),
            AsPair._(key5, swap5),
            AsPair._(key6, swap6),
            AsPair._(key7, swap7),
            AsPair._(key8, swap8),
            AsPair._(key9, swap9)
        )
        { }

        /// <summary>
        /// A set of conversions where the desired is selected by its name.
        /// </summary>
        public SwapSwitch(
            string key1, ISwap<TInput, TOutput> swap1,
            string key2, ISwap<TInput, TOutput> swap2,
            string key3, ISwap<TInput, TOutput> swap3,
            string key4, ISwap<TInput, TOutput> swap4,
            string key5, ISwap<TInput, TOutput> swap5,
            string key6, ISwap<TInput, TOutput> swap6,
            string key7, ISwap<TInput, TOutput> swap7,
            string key8, ISwap<TInput, TOutput> swap8,
            string key9, ISwap<TInput, TOutput> swap9,
            string key10, ISwap<TInput, TOutput> swap10
        ) : this(
            AsPair._(key1, swap1),
            AsPair._(key2, swap2),
            AsPair._(key3, swap3),
            AsPair._(key4, swap4),
            AsPair._(key5, swap5),
            AsPair._(key6, swap6),
            AsPair._(key7, swap7),
            AsPair._(key8, swap8),
            AsPair._(key9, swap9),
            AsPair._(key10, swap10)
        )
        { }

        /// <summary>
        /// A set of conversions where the desired is selected by its name.
        /// </summary>
        public SwapSwitch(
            string key1, ISwap<TInput, TOutput> swap1,
            string key2, ISwap<TInput, TOutput> swap2,
            string key3, ISwap<TInput, TOutput> swap3,
            string key4, ISwap<TInput, TOutput> swap4,
            string key5, ISwap<TInput, TOutput> swap5,
            string key6, ISwap<TInput, TOutput> swap6,
            string key7, ISwap<TInput, TOutput> swap7,
            string key8, ISwap<TInput, TOutput> swap8,
            string key9, ISwap<TInput, TOutput> swap9,
            string key10, ISwap<TInput, TOutput> swap10,
            string key11, ISwap<TInput, TOutput> swap11
        ) : this(
            AsPair._(key1, swap1),
            AsPair._(key2, swap2),
            AsPair._(key3, swap3),
            AsPair._(key4, swap4),
            AsPair._(key5, swap5),
            AsPair._(key6, swap6),
            AsPair._(key7, swap7),
            AsPair._(key8, swap8),
            AsPair._(key9, swap9),
            AsPair._(key10, swap10),
            AsPair._(key11, swap11)
        )
        { }

        /// <summary>
        /// A set of conversions where the desired is selected by its name.
        /// </summary>
        public SwapSwitch(
            string key1, ISwap<TInput, TOutput> swap1,
            string key2, ISwap<TInput, TOutput> swap2,
            string key3, ISwap<TInput, TOutput> swap3,
            string key4, ISwap<TInput, TOutput> swap4,
            string key5, ISwap<TInput, TOutput> swap5,
            string key6, ISwap<TInput, TOutput> swap6,
            string key7, ISwap<TInput, TOutput> swap7,
            string key8, ISwap<TInput, TOutput> swap8,
            string key9, ISwap<TInput, TOutput> swap9,
            string key10, ISwap<TInput, TOutput> swap10,
            string key11, ISwap<TInput, TOutput> swap11,
            string key12, ISwap<TInput, TOutput> swap12
        ) : this(
            AsPair._(key1, swap1),
            AsPair._(key2, swap2),
            AsPair._(key3, swap3),
            AsPair._(key4, swap4),
            AsPair._(key5, swap5),
            AsPair._(key6, swap6),
            AsPair._(key7, swap7),
            AsPair._(key8, swap8),
            AsPair._(key9, swap9),
            AsPair._(key10, swap10),
            AsPair._(key11, swap11),
            AsPair._(key12, swap12)
        )
        { }

        /// <summary>
        /// A set of conversions where the desired is selected by its name.
        /// </summary>
        public SwapSwitch(
            string key1, ISwap<TInput, TOutput> swap1,
            string key2, ISwap<TInput, TOutput> swap2,
            string key3, ISwap<TInput, TOutput> swap3,
            string key4, ISwap<TInput, TOutput> swap4,
            string key5, ISwap<TInput, TOutput> swap5,
            string key6, ISwap<TInput, TOutput> swap6,
            string key7, ISwap<TInput, TOutput> swap7,
            string key8, ISwap<TInput, TOutput> swap8,
            string key9, ISwap<TInput, TOutput> swap9,
            string key10, ISwap<TInput, TOutput> swap10,
            string key11, ISwap<TInput, TOutput> swap11,
            string key12, ISwap<TInput, TOutput> swap12,
            string key13, ISwap<TInput, TOutput> swap13
        ) : this(
            AsPair._(key1, swap1),
            AsPair._(key2, swap2),
            AsPair._(key3, swap3),
            AsPair._(key4, swap4),
            AsPair._(key5, swap5),
            AsPair._(key6, swap6),
            AsPair._(key7, swap7),
            AsPair._(key8, swap8),
            AsPair._(key9, swap9),
            AsPair._(key10, swap10),
            AsPair._(key11, swap11),
            AsPair._(key12, swap12),
            AsPair._(key13, swap13)
        )
        { }

        /// <summary>
        /// A set of conversions where the desired is selected by its name.
        /// </summary>
        public SwapSwitch(
            string key1, ISwap<TInput, TOutput> swap1,
            string key2, ISwap<TInput, TOutput> swap2,
            string key3, ISwap<TInput, TOutput> swap3,
            string key4, ISwap<TInput, TOutput> swap4,
            string key5, ISwap<TInput, TOutput> swap5,
            string key6, ISwap<TInput, TOutput> swap6,
            string key7, ISwap<TInput, TOutput> swap7,
            string key8, ISwap<TInput, TOutput> swap8,
            string key9, ISwap<TInput, TOutput> swap9,
            string key10, ISwap<TInput, TOutput> swap10,
            string key11, ISwap<TInput, TOutput> swap11,
            string key12, ISwap<TInput, TOutput> swap12,
            string key13, ISwap<TInput, TOutput> swap13,
            string key14, ISwap<TInput, TOutput> swap14
        ) : this(
            AsPair._(key1, swap1),
            AsPair._(key2, swap2),
            AsPair._(key3, swap3),
            AsPair._(key4, swap4),
            AsPair._(key5, swap5),
            AsPair._(key6, swap6),
            AsPair._(key7, swap7),
            AsPair._(key8, swap8),
            AsPair._(key9, swap9),
            AsPair._(key10, swap10),
            AsPair._(key11, swap11),
            AsPair._(key12, swap12),
            AsPair._(key13, swap13),
            AsPair._(key14, swap14)
        )
        { }

        /// <summary>
        /// A set of conversions where the desired is selected by its name.
        /// </summary>
        public SwapSwitch(
            string key1, ISwap<TInput, TOutput> swap1,
            string key2, ISwap<TInput, TOutput> swap2,
            string key3, ISwap<TInput, TOutput> swap3,
            string key4, ISwap<TInput, TOutput> swap4,
            string key5, ISwap<TInput, TOutput> swap5,
            string key6, ISwap<TInput, TOutput> swap6,
            string key7, ISwap<TInput, TOutput> swap7,
            string key8, ISwap<TInput, TOutput> swap8,
            string key9, ISwap<TInput, TOutput> swap9,
            string key10, ISwap<TInput, TOutput> swap10,
            string key11, ISwap<TInput, TOutput> swap11,
            string key12, ISwap<TInput, TOutput> swap12,
            string key13, ISwap<TInput, TOutput> swap13,
            string key14, ISwap<TInput, TOutput> swap14,
            string key15, ISwap<TInput, TOutput> swap15
        ) : this(
            AsPair._(key1, swap1),
            AsPair._(key2, swap2),
            AsPair._(key3, swap3),
            AsPair._(key4, swap4),
            AsPair._(key5, swap5),
            AsPair._(key6, swap6),
            AsPair._(key7, swap7),
            AsPair._(key8, swap8),
            AsPair._(key9, swap9),
            AsPair._(key10, swap10),
            AsPair._(key11, swap11),
            AsPair._(key12, swap12),
            AsPair._(key13, swap13),
            AsPair._(key14, swap14),
            AsPair._(key15, swap15)
        )
        { }

        /// <summary>
        /// A set of conversions where the desired is selected by its name.
        /// </summary>
        public SwapSwitch(
            string key1, ISwap<TInput, TOutput> swap1,
            string key2, ISwap<TInput, TOutput> swap2,
            string key3, ISwap<TInput, TOutput> swap3,
            string key4, ISwap<TInput, TOutput> swap4,
            string key5, ISwap<TInput, TOutput> swap5,
            string key6, ISwap<TInput, TOutput> swap6,
            string key7, ISwap<TInput, TOutput> swap7,
            string key8, ISwap<TInput, TOutput> swap8,
            string key9, ISwap<TInput, TOutput> swap9,
            string key10, ISwap<TInput, TOutput> swap10,
            string key11, ISwap<TInput, TOutput> swap11,
            string key12, ISwap<TInput, TOutput> swap12,
            string key13, ISwap<TInput, TOutput> swap13,
            string key14, ISwap<TInput, TOutput> swap14,
            string key15, ISwap<TInput, TOutput> swap15,
            string key16, ISwap<TInput, TOutput> swap16
        ) : this(
            AsPair._(key1, swap1),
            AsPair._(key2, swap2),
            AsPair._(key3, swap3),
            AsPair._(key4, swap4),
            AsPair._(key5, swap5),
            AsPair._(key6, swap6),
            AsPair._(key7, swap7),
            AsPair._(key8, swap8),
            AsPair._(key9, swap9),
            AsPair._(key10, swap10),
            AsPair._(key11, swap11),
            AsPair._(key12, swap12),
            AsPair._(key13, swap13),
            AsPair._(key14, swap14),
            AsPair._(key15, swap15),
            AsPair._(key16, swap16)
        )
        { }

        /// <summary>
        /// A set of conversions where the desired is selected by its name.
        /// </summary>
        public SwapSwitch(params IPair<string, ISwap<TInput, TOutput>>[] swaps) : this(
            AsEnumerable._(swaps)
        )
        { }

        /// <summary>
        /// A set of conversions where the desired is selected by its name.
        /// </summary>
        public SwapSwitch(Func<string, TInput, TOutput> fallback, params IPair<string, ISwap<TInput, TOutput>>[] swaps) : this(
            AsEnumerable._(swaps),
            fallback
        )
        { }

        /// <summary>
        /// A set of conversions where the desired is selected by its name.
        /// </summary>
        public SwapSwitch(IEnumerable<IPair<string, ISwap<TInput, TOutput>>> swap) : this(
            swap,
            (unknown, input) => throw new ArgumentException($"Cannot swap unknown type '{unknown}'")
        )
        { }

        /// <summary>
        /// A set of conversions where the desired is selected by its name.
        /// </summary>
        public SwapSwitch(
            IEnumerable<IPair<string, ISwap<TInput, TOutput>>> swap,
            Func<string, TInput, TOutput> fallback
        )
        {
            this.swaps =
                Fallback._(
                    AsMap._(swap),
                    unknown => new AsSwap<TInput, TOutput>((input) => fallback(unknown, input))
                );
        }

        public TOutput Flip(string key, TInput input)
        {
            return this.swaps[key].Flip(input);
        }
    }

    /// <summary>
    /// A set of conversions where the desired is selected by its name.
    /// </summary>
    public class SwapSwitch<TInput1, TInput2, TOutput> : ISwap<string, TInput1, TInput2, TOutput>
    {
        private readonly IMap<string, ISwap<TInput1, TInput2, TOutput>> swaps;

        /// <summary>
        /// A set of conversions where the desired is selected by its name.
        /// </summary>
        public SwapSwitch(
            string key1, ISwap<TInput1, TInput2, TOutput> swap1,
            string key2, ISwap<TInput1, TInput2, TOutput> swap2
        ) : this(
            AsPair._(key1, swap1),
            AsPair._(key2, swap2)
        )
        { }

        /// <summary>
        /// A set of conversions where the desired is selected by its name.
        /// </summary>
        public SwapSwitch(
            string key1, ISwap<TInput1, TInput2, TOutput> swap1,
            string key2, ISwap<TInput1, TInput2, TOutput> swap2,
            string key3, ISwap<TInput1, TInput2, TOutput> swap3
        ) : this(
            AsPair._(key1, swap1),
            AsPair._(key2, swap2),
            AsPair._(key3, swap3)
        )
        { }

        /// <summary>
        /// A set of conversions where the desired is selected by its name.
        /// </summary>
        public SwapSwitch(
            string key1, ISwap<TInput1, TInput2, TOutput> swap1,
            string key2, ISwap<TInput1, TInput2, TOutput> swap2,
            string key3, ISwap<TInput1, TInput2, TOutput> swap3,
            string key4, ISwap<TInput1, TInput2, TOutput> swap4
        ) : this(
            AsPair._(key1, swap1),
            AsPair._(key2, swap2),
            AsPair._(key3, swap3),
            AsPair._(key4, swap4)
        )
        { }

        /// <summary>
        /// A set of conversions where the desired is selected by its name.
        /// </summary>
        public SwapSwitch(
            string key1, ISwap<TInput1, TInput2, TOutput> swap1,
            string key2, ISwap<TInput1, TInput2, TOutput> swap2,
            string key3, ISwap<TInput1, TInput2, TOutput> swap3,
            string key4, ISwap<TInput1, TInput2, TOutput> swap4,
            string key5, ISwap<TInput1, TInput2, TOutput> swap5
        ) : this(
            AsPair._(key1, swap1),
            AsPair._(key2, swap2),
            AsPair._(key3, swap3),
            AsPair._(key4, swap4),
            AsPair._(key5, swap5)
        )
        { }

        /// <summary>
        /// A set of conversions where the desired is selected by its name.
        /// </summary>
        public SwapSwitch(
            string key1, ISwap<TInput1, TInput2, TOutput> swap1,
            string key2, ISwap<TInput1, TInput2, TOutput> swap2,
            string key3, ISwap<TInput1, TInput2, TOutput> swap3,
            string key4, ISwap<TInput1, TInput2, TOutput> swap4,
            string key5, ISwap<TInput1, TInput2, TOutput> swap5,
            string key6, ISwap<TInput1, TInput2, TOutput> swap6
        ) : this(
            AsPair._(key1, swap1),
            AsPair._(key2, swap2),
            AsPair._(key3, swap3),
            AsPair._(key4, swap4),
            AsPair._(key5, swap5),
            AsPair._(key6, swap6)
        )
        { }

        /// <summary>
        /// A set of conversions where the desired is selected by its name.
        /// </summary>
        public SwapSwitch(
            string key1, ISwap<TInput1, TInput2, TOutput> swap1,
            string key2, ISwap<TInput1, TInput2, TOutput> swap2,
            string key3, ISwap<TInput1, TInput2, TOutput> swap3,
            string key4, ISwap<TInput1, TInput2, TOutput> swap4,
            string key5, ISwap<TInput1, TInput2, TOutput> swap5,
            string key6, ISwap<TInput1, TInput2, TOutput> swap6,
            string key7, ISwap<TInput1, TInput2, TOutput> swap7
        ) : this(
            AsPair._(key1, swap1),
            AsPair._(key2, swap2),
            AsPair._(key3, swap3),
            AsPair._(key4, swap4),
            AsPair._(key5, swap5),
            AsPair._(key6, swap6),
            AsPair._(key7, swap7)
        )
        { }

        /// <summary>
        /// A set of conversions where the desired is selected by its name.
        /// </summary>
        public SwapSwitch(
            string key1, ISwap<TInput1, TInput2, TOutput> swap1,
            string key2, ISwap<TInput1, TInput2, TOutput> swap2,
            string key3, ISwap<TInput1, TInput2, TOutput> swap3,
            string key4, ISwap<TInput1, TInput2, TOutput> swap4,
            string key5, ISwap<TInput1, TInput2, TOutput> swap5,
            string key6, ISwap<TInput1, TInput2, TOutput> swap6,
            string key7, ISwap<TInput1, TInput2, TOutput> swap7,
            string key8, ISwap<TInput1, TInput2, TOutput> swap8
        ) : this(
            AsPair._(key1, swap1),
            AsPair._(key2, swap2),
            AsPair._(key3, swap3),
            AsPair._(key4, swap4),
            AsPair._(key5, swap5),
            AsPair._(key6, swap6),
            AsPair._(key7, swap7),
            AsPair._(key8, swap8)
        )
        { }

        /// <summary>
        /// A set of conversions where the desired is selected by its name.
        /// </summary>
        public SwapSwitch(
            string key1, ISwap<TInput1, TInput2, TOutput> swap1,
            string key2, ISwap<TInput1, TInput2, TOutput> swap2,
            string key3, ISwap<TInput1, TInput2, TOutput> swap3,
            string key4, ISwap<TInput1, TInput2, TOutput> swap4,
            string key5, ISwap<TInput1, TInput2, TOutput> swap5,
            string key6, ISwap<TInput1, TInput2, TOutput> swap6,
            string key7, ISwap<TInput1, TInput2, TOutput> swap7,
            string key8, ISwap<TInput1, TInput2, TOutput> swap8,
            string key9, ISwap<TInput1, TInput2, TOutput> swap9
        ) : this(
            AsPair._(key1, swap1),
            AsPair._(key2, swap2),
            AsPair._(key3, swap3),
            AsPair._(key4, swap4),
            AsPair._(key5, swap5),
            AsPair._(key6, swap6),
            AsPair._(key7, swap7),
            AsPair._(key8, swap8),
            AsPair._(key9, swap9)
        )
        { }

        /// <summary>
        /// A set of conversions where the desired is selected by its name.
        /// </summary>
        public SwapSwitch(
            string key1, ISwap<TInput1, TInput2, TOutput> swap1,
            string key2, ISwap<TInput1, TInput2, TOutput> swap2,
            string key3, ISwap<TInput1, TInput2, TOutput> swap3,
            string key4, ISwap<TInput1, TInput2, TOutput> swap4,
            string key5, ISwap<TInput1, TInput2, TOutput> swap5,
            string key6, ISwap<TInput1, TInput2, TOutput> swap6,
            string key7, ISwap<TInput1, TInput2, TOutput> swap7,
            string key8, ISwap<TInput1, TInput2, TOutput> swap8,
            string key9, ISwap<TInput1, TInput2, TOutput> swap9,
            string key10, ISwap<TInput1, TInput2, TOutput> swap10
        ) : this(
            AsPair._(key1, swap1),
            AsPair._(key2, swap2),
            AsPair._(key3, swap3),
            AsPair._(key4, swap4),
            AsPair._(key5, swap5),
            AsPair._(key6, swap6),
            AsPair._(key7, swap7),
            AsPair._(key8, swap8),
            AsPair._(key9, swap9),
            AsPair._(key10, swap10)
        )
        { }

        /// <summary>
        /// A set of conversions where the desired is selected by its name.
        /// </summary>
        public SwapSwitch(
            string key1, ISwap<TInput1, TInput2, TOutput> swap1,
            string key2, ISwap<TInput1, TInput2, TOutput> swap2,
            string key3, ISwap<TInput1, TInput2, TOutput> swap3,
            string key4, ISwap<TInput1, TInput2, TOutput> swap4,
            string key5, ISwap<TInput1, TInput2, TOutput> swap5,
            string key6, ISwap<TInput1, TInput2, TOutput> swap6,
            string key7, ISwap<TInput1, TInput2, TOutput> swap7,
            string key8, ISwap<TInput1, TInput2, TOutput> swap8,
            string key9, ISwap<TInput1, TInput2, TOutput> swap9,
            string key10, ISwap<TInput1, TInput2, TOutput> swap10,
            string key11, ISwap<TInput1, TInput2, TOutput> swap11
        ) : this(
            AsPair._(key1, swap1),
            AsPair._(key2, swap2),
            AsPair._(key3, swap3),
            AsPair._(key4, swap4),
            AsPair._(key5, swap5),
            AsPair._(key6, swap6),
            AsPair._(key7, swap7),
            AsPair._(key8, swap8),
            AsPair._(key9, swap9),
            AsPair._(key10, swap10),
            AsPair._(key11, swap11)
        )
        { }

        /// <summary>
        /// A set of conversions where the desired is selected by its name.
        /// </summary>
        public SwapSwitch(
            string key1, ISwap<TInput1, TInput2, TOutput> swap1,
            string key2, ISwap<TInput1, TInput2, TOutput> swap2,
            string key3, ISwap<TInput1, TInput2, TOutput> swap3,
            string key4, ISwap<TInput1, TInput2, TOutput> swap4,
            string key5, ISwap<TInput1, TInput2, TOutput> swap5,
            string key6, ISwap<TInput1, TInput2, TOutput> swap6,
            string key7, ISwap<TInput1, TInput2, TOutput> swap7,
            string key8, ISwap<TInput1, TInput2, TOutput> swap8,
            string key9, ISwap<TInput1, TInput2, TOutput> swap9,
            string key10, ISwap<TInput1, TInput2, TOutput> swap10,
            string key11, ISwap<TInput1, TInput2, TOutput> swap11,
            string key12, ISwap<TInput1, TInput2, TOutput> swap12
        ) : this(
            AsPair._(key1, swap1),
            AsPair._(key2, swap2),
            AsPair._(key3, swap3),
            AsPair._(key4, swap4),
            AsPair._(key5, swap5),
            AsPair._(key6, swap6),
            AsPair._(key7, swap7),
            AsPair._(key8, swap8),
            AsPair._(key9, swap9),
            AsPair._(key10, swap10),
            AsPair._(key11, swap11),
            AsPair._(key12, swap12)
        )
        { }

        /// <summary>
        /// A set of conversions where the desired is selected by its name.
        /// </summary>
        public SwapSwitch(
            string key1, ISwap<TInput1, TInput2, TOutput> swap1,
            string key2, ISwap<TInput1, TInput2, TOutput> swap2,
            string key3, ISwap<TInput1, TInput2, TOutput> swap3,
            string key4, ISwap<TInput1, TInput2, TOutput> swap4,
            string key5, ISwap<TInput1, TInput2, TOutput> swap5,
            string key6, ISwap<TInput1, TInput2, TOutput> swap6,
            string key7, ISwap<TInput1, TInput2, TOutput> swap7,
            string key8, ISwap<TInput1, TInput2, TOutput> swap8,
            string key9, ISwap<TInput1, TInput2, TOutput> swap9,
            string key10, ISwap<TInput1, TInput2, TOutput> swap10,
            string key11, ISwap<TInput1, TInput2, TOutput> swap11,
            string key12, ISwap<TInput1, TInput2, TOutput> swap12,
            string key13, ISwap<TInput1, TInput2, TOutput> swap13
        ) : this(
            AsPair._(key1, swap1),
            AsPair._(key2, swap2),
            AsPair._(key3, swap3),
            AsPair._(key4, swap4),
            AsPair._(key5, swap5),
            AsPair._(key6, swap6),
            AsPair._(key7, swap7),
            AsPair._(key8, swap8),
            AsPair._(key9, swap9),
            AsPair._(key10, swap10),
            AsPair._(key11, swap11),
            AsPair._(key12, swap12),
            AsPair._(key13, swap13)
        )
        { }

        /// <summary>
        /// A set of conversions where the desired is selected by its name.
        /// </summary>
        public SwapSwitch(
            string key1, ISwap<TInput1, TInput2, TOutput> swap1,
            string key2, ISwap<TInput1, TInput2, TOutput> swap2,
            string key3, ISwap<TInput1, TInput2, TOutput> swap3,
            string key4, ISwap<TInput1, TInput2, TOutput> swap4,
            string key5, ISwap<TInput1, TInput2, TOutput> swap5,
            string key6, ISwap<TInput1, TInput2, TOutput> swap6,
            string key7, ISwap<TInput1, TInput2, TOutput> swap7,
            string key8, ISwap<TInput1, TInput2, TOutput> swap8,
            string key9, ISwap<TInput1, TInput2, TOutput> swap9,
            string key10, ISwap<TInput1, TInput2, TOutput> swap10,
            string key11, ISwap<TInput1, TInput2, TOutput> swap11,
            string key12, ISwap<TInput1, TInput2, TOutput> swap12,
            string key13, ISwap<TInput1, TInput2, TOutput> swap13,
            string key14, ISwap<TInput1, TInput2, TOutput> swap14
        ) : this(
            AsPair._(key1, swap1),
            AsPair._(key2, swap2),
            AsPair._(key3, swap3),
            AsPair._(key4, swap4),
            AsPair._(key5, swap5),
            AsPair._(key6, swap6),
            AsPair._(key7, swap7),
            AsPair._(key8, swap8),
            AsPair._(key9, swap9),
            AsPair._(key10, swap10),
            AsPair._(key11, swap11),
            AsPair._(key12, swap12),
            AsPair._(key13, swap13),
            AsPair._(key14, swap14)
        )
        { }

        /// <summary>
        /// A set of conversions where the desired is selected by its name.
        /// </summary>
        public SwapSwitch(
            string key1, ISwap<TInput1, TInput2, TOutput> swap1,
            string key2, ISwap<TInput1, TInput2, TOutput> swap2,
            string key3, ISwap<TInput1, TInput2, TOutput> swap3,
            string key4, ISwap<TInput1, TInput2, TOutput> swap4,
            string key5, ISwap<TInput1, TInput2, TOutput> swap5,
            string key6, ISwap<TInput1, TInput2, TOutput> swap6,
            string key7, ISwap<TInput1, TInput2, TOutput> swap7,
            string key8, ISwap<TInput1, TInput2, TOutput> swap8,
            string key9, ISwap<TInput1, TInput2, TOutput> swap9,
            string key10, ISwap<TInput1, TInput2, TOutput> swap10,
            string key11, ISwap<TInput1, TInput2, TOutput> swap11,
            string key12, ISwap<TInput1, TInput2, TOutput> swap12,
            string key13, ISwap<TInput1, TInput2, TOutput> swap13,
            string key14, ISwap<TInput1, TInput2, TOutput> swap14,
            string key15, ISwap<TInput1, TInput2, TOutput> swap15
        ) : this(
            AsPair._(key1, swap1),
            AsPair._(key2, swap2),
            AsPair._(key3, swap3),
            AsPair._(key4, swap4),
            AsPair._(key5, swap5),
            AsPair._(key6, swap6),
            AsPair._(key7, swap7),
            AsPair._(key8, swap8),
            AsPair._(key9, swap9),
            AsPair._(key10, swap10),
            AsPair._(key11, swap11),
            AsPair._(key12, swap12),
            AsPair._(key13, swap13),
            AsPair._(key14, swap14),
            AsPair._(key15, swap15)
        )
        { }

        /// <summary>
        /// A set of conversions where the desired is selected by its name.
        /// </summary>
        public SwapSwitch(
            string key1, ISwap<TInput1, TInput2, TOutput> swap1,
            string key2, ISwap<TInput1, TInput2, TOutput> swap2,
            string key3, ISwap<TInput1, TInput2, TOutput> swap3,
            string key4, ISwap<TInput1, TInput2, TOutput> swap4,
            string key5, ISwap<TInput1, TInput2, TOutput> swap5,
            string key6, ISwap<TInput1, TInput2, TOutput> swap6,
            string key7, ISwap<TInput1, TInput2, TOutput> swap7,
            string key8, ISwap<TInput1, TInput2, TOutput> swap8,
            string key9, ISwap<TInput1, TInput2, TOutput> swap9,
            string key10, ISwap<TInput1, TInput2, TOutput> swap10,
            string key11, ISwap<TInput1, TInput2, TOutput> swap11,
            string key12, ISwap<TInput1, TInput2, TOutput> swap12,
            string key13, ISwap<TInput1, TInput2, TOutput> swap13,
            string key14, ISwap<TInput1, TInput2, TOutput> swap14,
            string key15, ISwap<TInput1, TInput2, TOutput> swap15,
            string key16, ISwap<TInput1, TInput2, TOutput> swap16
        ) : this(
            AsPair._(key1, swap1),
            AsPair._(key2, swap2),
            AsPair._(key3, swap3),
            AsPair._(key4, swap4),
            AsPair._(key5, swap5),
            AsPair._(key6, swap6),
            AsPair._(key7, swap7),
            AsPair._(key8, swap8),
            AsPair._(key9, swap9),
            AsPair._(key10, swap10),
            AsPair._(key11, swap11),
            AsPair._(key12, swap12),
            AsPair._(key13, swap13),
            AsPair._(key14, swap14),
            AsPair._(key15, swap15),
            AsPair._(key16, swap16)
        )
        { }

        /// <summary>
        /// A set of conversions where the desired is selected by its name.
        /// </summary>
        public SwapSwitch(Func<string, TInput1, TInput2, TOutput> fallback,
            params IPair<string, ISwap<TInput1, TInput2, TOutput>>[] swaps) : this(
            AsEnumerable._(swaps),
            fallback
        )
        { }

        /// <summary>
        /// A set of conversions where the desired is selected by its name.
        /// </summary>
        public SwapSwitch(params IPair<string, ISwap<TInput1, TInput2, TOutput>>[] swaps) : this(
            AsEnumerable._(swaps)
        )
        { }

        /// <summary>
        /// A set of conversions where the desired is selected by its name.
        /// </summary>
        public SwapSwitch(IEnumerable<IPair<string, ISwap<TInput1, TInput2, TOutput>>> swap) : this(
            swap,
            (unknown, input1, input2) => throw new ArgumentException($"Cannot swap unknown type '{unknown}'")
        )
        { }

        /// <summary>
        /// A set of conversions where the desired is selected by its name.
        /// </summary>
        public SwapSwitch(IEnumerable<IPair<string, ISwap<TInput1, TInput2, TOutput>>> swap, Func<string, TInput1, TInput2, TOutput> fallback)
        {
            this.swaps =
                Fallback._(
                    AsMap._(swap),
                    unknown =>
                        new AsSwap<TInput1, TInput2, TOutput>(
                            (input1, input2) => fallback(unknown, input1, input2)
                        )
                );
        }

        public TOutput Flip(string key, TInput1 input1, TInput2 input2)
        {
            return this.swaps[key].Flip(input1, input2);
        }
    }

    /// <summary>
    /// A set of conversions where the desired is selected by its name.
    /// </summary>
    public class SwapSwitch<TKey, TInput1, TInput2, TOutput> : ISwap<TKey, TInput1, TInput2, TOutput>
    {
        private readonly IMap<TKey, ISwap<TInput1, TInput2, TOutput>> swaps;

        /// <summary>
        /// A set of conversions where the desired is selected by its name.
        /// </summary>
        public SwapSwitch(
            TKey key1, ISwap<TInput1, TInput2, TOutput> swap1,
            TKey key2, ISwap<TInput1, TInput2, TOutput> swap2
        ) : this(
            AsPair._(key1, swap1),
            AsPair._(key2, swap2)
        )
        { }

        /// <summary>
        /// A set of conversions where the desired is selected by its name.
        /// </summary>
        public SwapSwitch(
            TKey key1, ISwap<TInput1, TInput2, TOutput> swap1,
            TKey key2, ISwap<TInput1, TInput2, TOutput> swap2,
            TKey key3, ISwap<TInput1, TInput2, TOutput> swap3
        ) : this(
            AsPair._(key1, swap1),
            AsPair._(key2, swap2),
            AsPair._(key3, swap3)
        )
        { }

        /// <summary>
        /// A set of conversions where the desired is selected by its name.
        /// </summary>
        public SwapSwitch(
            TKey key1, ISwap<TInput1, TInput2, TOutput> swap1,
            TKey key2, ISwap<TInput1, TInput2, TOutput> swap2,
            TKey key3, ISwap<TInput1, TInput2, TOutput> swap3,
            TKey key4, ISwap<TInput1, TInput2, TOutput> swap4
        ) : this(
            AsPair._(key1, swap1),
            AsPair._(key2, swap2),
            AsPair._(key3, swap3),
            AsPair._(key4, swap4)
        )
        { }

        /// <summary>
        /// A set of conversions where the desired is selected by its name.
        /// </summary>
        public SwapSwitch(
            TKey key1, ISwap<TInput1, TInput2, TOutput> swap1,
            TKey key2, ISwap<TInput1, TInput2, TOutput> swap2,
            TKey key3, ISwap<TInput1, TInput2, TOutput> swap3,
            TKey key4, ISwap<TInput1, TInput2, TOutput> swap4,
            TKey key5, ISwap<TInput1, TInput2, TOutput> swap5
        ) : this(
            AsPair._(key1, swap1),
            AsPair._(key2, swap2),
            AsPair._(key3, swap3),
            AsPair._(key4, swap4),
            AsPair._(key5, swap5)
        )
        { }

        /// <summary>
        /// A set of conversions where the desired is selected by its name.
        /// </summary>
        public SwapSwitch(
            TKey key1, ISwap<TInput1, TInput2, TOutput> swap1,
            TKey key2, ISwap<TInput1, TInput2, TOutput> swap2,
            TKey key3, ISwap<TInput1, TInput2, TOutput> swap3,
            TKey key4, ISwap<TInput1, TInput2, TOutput> swap4,
            TKey key5, ISwap<TInput1, TInput2, TOutput> swap5,
            TKey key6, ISwap<TInput1, TInput2, TOutput> swap6
        ) : this(
            AsPair._(key1, swap1),
            AsPair._(key2, swap2),
            AsPair._(key3, swap3),
            AsPair._(key4, swap4),
            AsPair._(key5, swap5),
            AsPair._(key6, swap6)
        )
        { }

        /// <summary>
        /// A set of conversions where the desired is selected by its name.
        /// </summary>
        public SwapSwitch(
            TKey key1, ISwap<TInput1, TInput2, TOutput> swap1,
            TKey key2, ISwap<TInput1, TInput2, TOutput> swap2,
            TKey key3, ISwap<TInput1, TInput2, TOutput> swap3,
            TKey key4, ISwap<TInput1, TInput2, TOutput> swap4,
            TKey key5, ISwap<TInput1, TInput2, TOutput> swap5,
            TKey key6, ISwap<TInput1, TInput2, TOutput> swap6,
            TKey key7, ISwap<TInput1, TInput2, TOutput> swap7
        ) : this(
            AsPair._(key1, swap1),
            AsPair._(key2, swap2),
            AsPair._(key3, swap3),
            AsPair._(key4, swap4),
            AsPair._(key5, swap5),
            AsPair._(key6, swap6),
            AsPair._(key7, swap7)
        )
        { }

        /// <summary>
        /// A set of conversions where the desired is selected by its name.
        /// </summary>
        public SwapSwitch(
            TKey key1, ISwap<TInput1, TInput2, TOutput> swap1,
            TKey key2, ISwap<TInput1, TInput2, TOutput> swap2,
            TKey key3, ISwap<TInput1, TInput2, TOutput> swap3,
            TKey key4, ISwap<TInput1, TInput2, TOutput> swap4,
            TKey key5, ISwap<TInput1, TInput2, TOutput> swap5,
            TKey key6, ISwap<TInput1, TInput2, TOutput> swap6,
            TKey key7, ISwap<TInput1, TInput2, TOutput> swap7,
            TKey key8, ISwap<TInput1, TInput2, TOutput> swap8
        ) : this(
            AsPair._(key1, swap1),
            AsPair._(key2, swap2),
            AsPair._(key3, swap3),
            AsPair._(key4, swap4),
            AsPair._(key5, swap5),
            AsPair._(key6, swap6),
            AsPair._(key7, swap7),
            AsPair._(key8, swap8)
        )
        { }

        /// <summary>
        /// A set of conversions where the desired is selected by its name.
        /// </summary>
        public SwapSwitch(
            TKey key1, ISwap<TInput1, TInput2, TOutput> swap1,
            TKey key2, ISwap<TInput1, TInput2, TOutput> swap2,
            TKey key3, ISwap<TInput1, TInput2, TOutput> swap3,
            TKey key4, ISwap<TInput1, TInput2, TOutput> swap4,
            TKey key5, ISwap<TInput1, TInput2, TOutput> swap5,
            TKey key6, ISwap<TInput1, TInput2, TOutput> swap6,
            TKey key7, ISwap<TInput1, TInput2, TOutput> swap7,
            TKey key8, ISwap<TInput1, TInput2, TOutput> swap8,
            TKey key9, ISwap<TInput1, TInput2, TOutput> swap9
        ) : this(
            AsPair._(key1, swap1),
            AsPair._(key2, swap2),
            AsPair._(key3, swap3),
            AsPair._(key4, swap4),
            AsPair._(key5, swap5),
            AsPair._(key6, swap6),
            AsPair._(key7, swap7),
            AsPair._(key8, swap8),
            AsPair._(key9, swap9)
        )
        { }

        /// <summary>
        /// A set of conversions where the desired is selected by its name.
        /// </summary>
        public SwapSwitch(
            TKey key1, ISwap<TInput1, TInput2, TOutput> swap1,
            TKey key2, ISwap<TInput1, TInput2, TOutput> swap2,
            TKey key3, ISwap<TInput1, TInput2, TOutput> swap3,
            TKey key4, ISwap<TInput1, TInput2, TOutput> swap4,
            TKey key5, ISwap<TInput1, TInput2, TOutput> swap5,
            TKey key6, ISwap<TInput1, TInput2, TOutput> swap6,
            TKey key7, ISwap<TInput1, TInput2, TOutput> swap7,
            TKey key8, ISwap<TInput1, TInput2, TOutput> swap8,
            TKey key9, ISwap<TInput1, TInput2, TOutput> swap9,
            TKey key10, ISwap<TInput1, TInput2, TOutput> swap10
        ) : this(
            AsPair._(key1, swap1),
            AsPair._(key2, swap2),
            AsPair._(key3, swap3),
            AsPair._(key4, swap4),
            AsPair._(key5, swap5),
            AsPair._(key6, swap6),
            AsPair._(key7, swap7),
            AsPair._(key8, swap8),
            AsPair._(key9, swap9),
            AsPair._(key10, swap10)
        )
        { }

        /// <summary>
        /// A set of conversions where the desired is selected by its name.
        /// </summary>
        public SwapSwitch(
            TKey key1, ISwap<TInput1, TInput2, TOutput> swap1,
            TKey key2, ISwap<TInput1, TInput2, TOutput> swap2,
            TKey key3, ISwap<TInput1, TInput2, TOutput> swap3,
            TKey key4, ISwap<TInput1, TInput2, TOutput> swap4,
            TKey key5, ISwap<TInput1, TInput2, TOutput> swap5,
            TKey key6, ISwap<TInput1, TInput2, TOutput> swap6,
            TKey key7, ISwap<TInput1, TInput2, TOutput> swap7,
            TKey key8, ISwap<TInput1, TInput2, TOutput> swap8,
            TKey key9, ISwap<TInput1, TInput2, TOutput> swap9,
            TKey key10, ISwap<TInput1, TInput2, TOutput> swap10,
            TKey key11, ISwap<TInput1, TInput2, TOutput> swap11
        ) : this(
            AsPair._(key1, swap1),
            AsPair._(key2, swap2),
            AsPair._(key3, swap3),
            AsPair._(key4, swap4),
            AsPair._(key5, swap5),
            AsPair._(key6, swap6),
            AsPair._(key7, swap7),
            AsPair._(key8, swap8),
            AsPair._(key9, swap9),
            AsPair._(key10, swap10),
            AsPair._(key11, swap11)
        )
        { }

        /// <summary>
        /// A set of conversions where the desired is selected by its name.
        /// </summary>
        public SwapSwitch(
            TKey key1, ISwap<TInput1, TInput2, TOutput> swap1,
            TKey key2, ISwap<TInput1, TInput2, TOutput> swap2,
            TKey key3, ISwap<TInput1, TInput2, TOutput> swap3,
            TKey key4, ISwap<TInput1, TInput2, TOutput> swap4,
            TKey key5, ISwap<TInput1, TInput2, TOutput> swap5,
            TKey key6, ISwap<TInput1, TInput2, TOutput> swap6,
            TKey key7, ISwap<TInput1, TInput2, TOutput> swap7,
            TKey key8, ISwap<TInput1, TInput2, TOutput> swap8,
            TKey key9, ISwap<TInput1, TInput2, TOutput> swap9,
            TKey key10, ISwap<TInput1, TInput2, TOutput> swap10,
            TKey key11, ISwap<TInput1, TInput2, TOutput> swap11,
            TKey key12, ISwap<TInput1, TInput2, TOutput> swap12
        ) : this(
            AsPair._(key1, swap1),
            AsPair._(key2, swap2),
            AsPair._(key3, swap3),
            AsPair._(key4, swap4),
            AsPair._(key5, swap5),
            AsPair._(key6, swap6),
            AsPair._(key7, swap7),
            AsPair._(key8, swap8),
            AsPair._(key9, swap9),
            AsPair._(key10, swap10),
            AsPair._(key11, swap11),
            AsPair._(key12, swap12)
        )
        { }

        /// <summary>
        /// A set of conversions where the desired is selected by its name.
        /// </summary>
        public SwapSwitch(
            TKey key1, ISwap<TInput1, TInput2, TOutput> swap1,
            TKey key2, ISwap<TInput1, TInput2, TOutput> swap2,
            TKey key3, ISwap<TInput1, TInput2, TOutput> swap3,
            TKey key4, ISwap<TInput1, TInput2, TOutput> swap4,
            TKey key5, ISwap<TInput1, TInput2, TOutput> swap5,
            TKey key6, ISwap<TInput1, TInput2, TOutput> swap6,
            TKey key7, ISwap<TInput1, TInput2, TOutput> swap7,
            TKey key8, ISwap<TInput1, TInput2, TOutput> swap8,
            TKey key9, ISwap<TInput1, TInput2, TOutput> swap9,
            TKey key10, ISwap<TInput1, TInput2, TOutput> swap10,
            TKey key11, ISwap<TInput1, TInput2, TOutput> swap11,
            TKey key12, ISwap<TInput1, TInput2, TOutput> swap12,
            TKey key13, ISwap<TInput1, TInput2, TOutput> swap13
        ) : this(
            AsPair._(key1, swap1),
            AsPair._(key2, swap2),
            AsPair._(key3, swap3),
            AsPair._(key4, swap4),
            AsPair._(key5, swap5),
            AsPair._(key6, swap6),
            AsPair._(key7, swap7),
            AsPair._(key8, swap8),
            AsPair._(key9, swap9),
            AsPair._(key10, swap10),
            AsPair._(key11, swap11),
            AsPair._(key12, swap12),
            AsPair._(key13, swap13)
        )
        { }

        /// <summary>
        /// A set of conversions where the desired is selected by its name.
        /// </summary>
        public SwapSwitch(
            TKey key1, ISwap<TInput1, TInput2, TOutput> swap1,
            TKey key2, ISwap<TInput1, TInput2, TOutput> swap2,
            TKey key3, ISwap<TInput1, TInput2, TOutput> swap3,
            TKey key4, ISwap<TInput1, TInput2, TOutput> swap4,
            TKey key5, ISwap<TInput1, TInput2, TOutput> swap5,
            TKey key6, ISwap<TInput1, TInput2, TOutput> swap6,
            TKey key7, ISwap<TInput1, TInput2, TOutput> swap7,
            TKey key8, ISwap<TInput1, TInput2, TOutput> swap8,
            TKey key9, ISwap<TInput1, TInput2, TOutput> swap9,
            TKey key10, ISwap<TInput1, TInput2, TOutput> swap10,
            TKey key11, ISwap<TInput1, TInput2, TOutput> swap11,
            TKey key12, ISwap<TInput1, TInput2, TOutput> swap12,
            TKey key13, ISwap<TInput1, TInput2, TOutput> swap13,
            TKey key14, ISwap<TInput1, TInput2, TOutput> swap14
        ) : this(
            AsPair._(key1, swap1),
            AsPair._(key2, swap2),
            AsPair._(key3, swap3),
            AsPair._(key4, swap4),
            AsPair._(key5, swap5),
            AsPair._(key6, swap6),
            AsPair._(key7, swap7),
            AsPair._(key8, swap8),
            AsPair._(key9, swap9),
            AsPair._(key10, swap10),
            AsPair._(key11, swap11),
            AsPair._(key12, swap12),
            AsPair._(key13, swap13),
            AsPair._(key14, swap14)
        )
        { }

        /// <summary>
        /// A set of conversions where the desired is selected by its name.
        /// </summary>
        public SwapSwitch(
            TKey key1, ISwap<TInput1, TInput2, TOutput> swap1,
            TKey key2, ISwap<TInput1, TInput2, TOutput> swap2,
            TKey key3, ISwap<TInput1, TInput2, TOutput> swap3,
            TKey key4, ISwap<TInput1, TInput2, TOutput> swap4,
            TKey key5, ISwap<TInput1, TInput2, TOutput> swap5,
            TKey key6, ISwap<TInput1, TInput2, TOutput> swap6,
            TKey key7, ISwap<TInput1, TInput2, TOutput> swap7,
            TKey key8, ISwap<TInput1, TInput2, TOutput> swap8,
            TKey key9, ISwap<TInput1, TInput2, TOutput> swap9,
            TKey key10, ISwap<TInput1, TInput2, TOutput> swap10,
            TKey key11, ISwap<TInput1, TInput2, TOutput> swap11,
            TKey key12, ISwap<TInput1, TInput2, TOutput> swap12,
            TKey key13, ISwap<TInput1, TInput2, TOutput> swap13,
            TKey key14, ISwap<TInput1, TInput2, TOutput> swap14,
            TKey key15, ISwap<TInput1, TInput2, TOutput> swap15
        ) : this(
            AsPair._(key1, swap1),
            AsPair._(key2, swap2),
            AsPair._(key3, swap3),
            AsPair._(key4, swap4),
            AsPair._(key5, swap5),
            AsPair._(key6, swap6),
            AsPair._(key7, swap7),
            AsPair._(key8, swap8),
            AsPair._(key9, swap9),
            AsPair._(key10, swap10),
            AsPair._(key11, swap11),
            AsPair._(key12, swap12),
            AsPair._(key13, swap13),
            AsPair._(key14, swap14),
            AsPair._(key15, swap15)
        )
        { }

        /// <summary>
        /// A set of conversions where the desired is selected by its name.
        /// </summary>
        public SwapSwitch(
            TKey key1, ISwap<TInput1, TInput2, TOutput> swap1,
            TKey key2, ISwap<TInput1, TInput2, TOutput> swap2,
            TKey key3, ISwap<TInput1, TInput2, TOutput> swap3,
            TKey key4, ISwap<TInput1, TInput2, TOutput> swap4,
            TKey key5, ISwap<TInput1, TInput2, TOutput> swap5,
            TKey key6, ISwap<TInput1, TInput2, TOutput> swap6,
            TKey key7, ISwap<TInput1, TInput2, TOutput> swap7,
            TKey key8, ISwap<TInput1, TInput2, TOutput> swap8,
            TKey key9, ISwap<TInput1, TInput2, TOutput> swap9,
            TKey key10, ISwap<TInput1, TInput2, TOutput> swap10,
            TKey key11, ISwap<TInput1, TInput2, TOutput> swap11,
            TKey key12, ISwap<TInput1, TInput2, TOutput> swap12,
            TKey key13, ISwap<TInput1, TInput2, TOutput> swap13,
            TKey key14, ISwap<TInput1, TInput2, TOutput> swap14,
            TKey key15, ISwap<TInput1, TInput2, TOutput> swap15,
            TKey key16, ISwap<TInput1, TInput2, TOutput> swap16
        ) : this(
            AsPair._(key1, swap1),
            AsPair._(key2, swap2),
            AsPair._(key3, swap3),
            AsPair._(key4, swap4),
            AsPair._(key5, swap5),
            AsPair._(key6, swap6),
            AsPair._(key7, swap7),
            AsPair._(key8, swap8),
            AsPair._(key9, swap9),
            AsPair._(key10, swap10),
            AsPair._(key11, swap11),
            AsPair._(key12, swap12),
            AsPair._(key13, swap13),
            AsPair._(key14, swap14),
            AsPair._(key15, swap15),
            AsPair._(key16, swap16)
        )
        { }

        /// <summary>
        /// A set of conversions where the desired is selected by its name.
        /// </summary>
        public SwapSwitch(Func<TKey, TInput1, TInput2, TOutput> fallback, params IPair<TKey, ISwap<TInput1, TInput2, TOutput>>[] swaps) : this(
            AsEnumerable._(swaps),
            fallback
        )
        { }

        /// <summary>
        /// A set of conversions where the desired is selected by its name.
        /// </summary>
        public SwapSwitch(params IPair<TKey, ISwap<TInput1, TInput2, TOutput>>[] swaps) : this(
            AsEnumerable._(swaps)
        )
        { }

        /// <summary>
        /// A set of conversions where the desired is selected by its name.
        /// </summary>
        public SwapSwitch(IEnumerable<IPair<TKey, ISwap<TInput1, TInput2, TOutput>>> swap) : this(
            swap,
            (unknown, input1, input2) => throw new ArgumentException($"Cannot swap unknown type '{unknown}'")
        )
        { }

        /// <summary>
        /// A set of conversions where the desired is selected by its name.
        /// </summary>
        public SwapSwitch(IEnumerable<IPair<TKey, ISwap<TInput1, TInput2, TOutput>>> swap, Func<TKey, TInput1, TInput2, TOutput> fallback)
        {
            this.swaps =
                Fallback._(
                    AsMap._(swap),
                    unknown =>
                        new AsSwap<TInput1, TInput2, TOutput>(
                            (input1, input2) => fallback(unknown, input1, input2)
                        )
                );
        }

        public TOutput Flip(TKey key, TInput1 input1, TInput2 input2)
        {
            return this.swaps[key].Flip(input1, input2);
        }
    }

    public static class SwapSwitch
    {
        /// <summary>
        /// Swaps depending on the given key.
        /// </summary>
        public static SwapSwitch<TInput, TOutput> _<TInput, TOutput>(
            string key1, ISwap<TInput, TOutput> swap1,
            string key2, ISwap<TInput, TOutput> swap2
        ) => new SwapSwitch<TInput, TOutput>(
            AsPair._(key1, swap1),
            AsPair._(key2, swap2)
        );

        /// <summary>
        /// Swaps depending on the given key.
        /// </summary>
        public static SwapSwitch<TInput, TOutput> _<TInput, TOutput>(
            string key1, ISwap<TInput, TOutput> swap1,
            string key2, ISwap<TInput, TOutput> swap2,
            string key3, ISwap<TInput, TOutput> swap3
        ) => new SwapSwitch<TInput, TOutput>(
            AsPair._(key1, swap1),
            AsPair._(key2, swap2),
            AsPair._(key3, swap3)
        );

        /// <summary>
        /// Swaps depending on the given key.
        /// </summary>
        public static SwapSwitch<TInput, TOutput> _<TInput, TOutput>(
            string key1, ISwap<TInput, TOutput> swap1,
            string key2, ISwap<TInput, TOutput> swap2,
            string key3, ISwap<TInput, TOutput> swap3,
            string key4, ISwap<TInput, TOutput> swap4
        ) => new SwapSwitch<TInput, TOutput>(
            AsPair._(key1, swap1),
            AsPair._(key2, swap2),
            AsPair._(key3, swap3),
            AsPair._(key4, swap4)
        );

        /// <summary>
        /// Swaps depending on the given key.
        /// </summary>
        public static SwapSwitch<TInput, TOutput> _<TInput, TOutput>(
            string key1, ISwap<TInput, TOutput> swap1,
            string key2, ISwap<TInput, TOutput> swap2,
            string key3, ISwap<TInput, TOutput> swap3,
            string key4, ISwap<TInput, TOutput> swap4,
            string key5, ISwap<TInput, TOutput> swap5
        ) => new SwapSwitch<TInput, TOutput>(
            AsPair._(key1, swap1),
            AsPair._(key2, swap2),
            AsPair._(key3, swap3),
            AsPair._(key4, swap4),
            AsPair._(key5, swap5)
        );

        /// <summary>
        /// Swaps depending on the given key.
        /// </summary>
        public static SwapSwitch<TInput, TOutput> _<TInput, TOutput>(
            string key1, ISwap<TInput, TOutput> swap1,
            string key2, ISwap<TInput, TOutput> swap2,
            string key3, ISwap<TInput, TOutput> swap3,
            string key4, ISwap<TInput, TOutput> swap4,
            string key5, ISwap<TInput, TOutput> swap5,
            string key6, ISwap<TInput, TOutput> swap6
        ) => new SwapSwitch<TInput, TOutput>(
            AsPair._(key1, swap1),
            AsPair._(key2, swap2),
            AsPair._(key3, swap3),
            AsPair._(key4, swap4),
            AsPair._(key5, swap5),
            AsPair._(key6, swap6)
        );

        /// <summary>
        /// Swaps depending on the given key.
        /// </summary>
        public static SwapSwitch<TInput, TOutput> _<TInput, TOutput>(
            string key1, ISwap<TInput, TOutput> swap1,
            string key2, ISwap<TInput, TOutput> swap2,
            string key3, ISwap<TInput, TOutput> swap3,
            string key4, ISwap<TInput, TOutput> swap4,
            string key5, ISwap<TInput, TOutput> swap5,
            string key6, ISwap<TInput, TOutput> swap6,
            string key7, ISwap<TInput, TOutput> swap7
        ) => new SwapSwitch<TInput, TOutput>(
            AsPair._(key1, swap1),
            AsPair._(key2, swap2),
            AsPair._(key3, swap3),
            AsPair._(key4, swap4),
            AsPair._(key5, swap5),
            AsPair._(key6, swap6),
            AsPair._(key7, swap7)
        );

        /// <summary>
        /// Swaps depending on the given key.
        /// </summary>
        public static SwapSwitch<TInput, TOutput> _<TInput, TOutput>(
            string key1, ISwap<TInput, TOutput> swap1,
            string key2, ISwap<TInput, TOutput> swap2,
            string key3, ISwap<TInput, TOutput> swap3,
            string key4, ISwap<TInput, TOutput> swap4,
            string key5, ISwap<TInput, TOutput> swap5,
            string key6, ISwap<TInput, TOutput> swap6,
            string key7, ISwap<TInput, TOutput> swap7,
            string key8, ISwap<TInput, TOutput> swap8
        ) => new SwapSwitch<TInput, TOutput>(
            AsPair._(key1, swap1),
            AsPair._(key2, swap2),
            AsPair._(key3, swap3),
            AsPair._(key4, swap4),
            AsPair._(key5, swap5),
            AsPair._(key6, swap6),
            AsPair._(key7, swap7),
            AsPair._(key8, swap8)
        );

        /// <summary>
        /// Swaps depending on the given key.
        /// </summary>
        public static SwapSwitch<TInput, TOutput> _<TInput, TOutput>(
            string key1, ISwap<TInput, TOutput> swap1,
            string key2, ISwap<TInput, TOutput> swap2,
            string key3, ISwap<TInput, TOutput> swap3,
            string key4, ISwap<TInput, TOutput> swap4,
            string key5, ISwap<TInput, TOutput> swap5,
            string key6, ISwap<TInput, TOutput> swap6,
            string key7, ISwap<TInput, TOutput> swap7,
            string key8, ISwap<TInput, TOutput> swap8,
            string key9, ISwap<TInput, TOutput> swap9
        ) => new SwapSwitch<TInput, TOutput>(
            AsPair._(key1, swap1),
            AsPair._(key2, swap2),
            AsPair._(key3, swap3),
            AsPair._(key4, swap4),
            AsPair._(key5, swap5),
            AsPair._(key6, swap6),
            AsPair._(key7, swap7),
            AsPair._(key8, swap8),
            AsPair._(key9, swap9)
        );

        /// <summary>
        /// Swaps depending on the given key.
        /// </summary>
        public static SwapSwitch<TInput, TOutput> _<TInput, TOutput>(
            string key1, ISwap<TInput, TOutput> swap1,
            string key2, ISwap<TInput, TOutput> swap2,
            string key3, ISwap<TInput, TOutput> swap3,
            string key4, ISwap<TInput, TOutput> swap4,
            string key5, ISwap<TInput, TOutput> swap5,
            string key6, ISwap<TInput, TOutput> swap6,
            string key7, ISwap<TInput, TOutput> swap7,
            string key8, ISwap<TInput, TOutput> swap8,
            string key9, ISwap<TInput, TOutput> swap9,
            string key10, ISwap<TInput, TOutput> swap10
        ) => new SwapSwitch<TInput, TOutput>(
            AsPair._(key1, swap1),
            AsPair._(key2, swap2),
            AsPair._(key3, swap3),
            AsPair._(key4, swap4),
            AsPair._(key5, swap5),
            AsPair._(key6, swap6),
            AsPair._(key7, swap7),
            AsPair._(key8, swap8),
            AsPair._(key9, swap9),
            AsPair._(key10, swap10)
        );

        /// <summary>
        /// Swaps depending on the given key.
        /// </summary>
        public static SwapSwitch<TInput, TOutput> _<TInput, TOutput>(
            string key1, ISwap<TInput, TOutput> swap1,
            string key2, ISwap<TInput, TOutput> swap2,
            string key3, ISwap<TInput, TOutput> swap3,
            string key4, ISwap<TInput, TOutput> swap4,
            string key5, ISwap<TInput, TOutput> swap5,
            string key6, ISwap<TInput, TOutput> swap6,
            string key7, ISwap<TInput, TOutput> swap7,
            string key8, ISwap<TInput, TOutput> swap8,
            string key9, ISwap<TInput, TOutput> swap9,
            string key10, ISwap<TInput, TOutput> swap10,
            string key11, ISwap<TInput, TOutput> swap11
        ) => new SwapSwitch<TInput, TOutput>(
            AsPair._(key1, swap1),
            AsPair._(key2, swap2),
            AsPair._(key3, swap3),
            AsPair._(key4, swap4),
            AsPair._(key5, swap5),
            AsPair._(key6, swap6),
            AsPair._(key7, swap7),
            AsPair._(key8, swap8),
            AsPair._(key9, swap9),
            AsPair._(key10, swap10),
            AsPair._(key11, swap11)
        );

        /// <summary>
        /// Swaps depending on the given key.
        /// </summary>
        public static SwapSwitch<TInput, TOutput> _<TInput, TOutput>(
            string key1, ISwap<TInput, TOutput> swap1,
            string key2, ISwap<TInput, TOutput> swap2,
            string key3, ISwap<TInput, TOutput> swap3,
            string key4, ISwap<TInput, TOutput> swap4,
            string key5, ISwap<TInput, TOutput> swap5,
            string key6, ISwap<TInput, TOutput> swap6,
            string key7, ISwap<TInput, TOutput> swap7,
            string key8, ISwap<TInput, TOutput> swap8,
            string key9, ISwap<TInput, TOutput> swap9,
            string key10, ISwap<TInput, TOutput> swap10,
            string key11, ISwap<TInput, TOutput> swap11,
            string key12, ISwap<TInput, TOutput> swap12
        ) => new SwapSwitch<TInput, TOutput>(
            AsPair._(key1, swap1),
            AsPair._(key2, swap2),
            AsPair._(key3, swap3),
            AsPair._(key4, swap4),
            AsPair._(key5, swap5),
            AsPair._(key6, swap6),
            AsPair._(key7, swap7),
            AsPair._(key8, swap8),
            AsPair._(key9, swap9),
            AsPair._(key10, swap10),
            AsPair._(key11, swap11),
            AsPair._(key12, swap12)
        );

        /// <summary>
        /// Swaps depending on the given key.
        /// </summary>
        public static SwapSwitch<TInput, TOutput> _<TInput, TOutput>(
            string key1, ISwap<TInput, TOutput> swap1,
            string key2, ISwap<TInput, TOutput> swap2,
            string key3, ISwap<TInput, TOutput> swap3,
            string key4, ISwap<TInput, TOutput> swap4,
            string key5, ISwap<TInput, TOutput> swap5,
            string key6, ISwap<TInput, TOutput> swap6,
            string key7, ISwap<TInput, TOutput> swap7,
            string key8, ISwap<TInput, TOutput> swap8,
            string key9, ISwap<TInput, TOutput> swap9,
            string key10, ISwap<TInput, TOutput> swap10,
            string key11, ISwap<TInput, TOutput> swap11,
            string key12, ISwap<TInput, TOutput> swap12,
            string key13, ISwap<TInput, TOutput> swap13
        ) => new SwapSwitch<TInput, TOutput>(
            AsPair._(key1, swap1),
            AsPair._(key2, swap2),
            AsPair._(key3, swap3),
            AsPair._(key4, swap4),
            AsPair._(key5, swap5),
            AsPair._(key6, swap6),
            AsPair._(key7, swap7),
            AsPair._(key8, swap8),
            AsPair._(key9, swap9),
            AsPair._(key10, swap10),
            AsPair._(key11, swap11),
            AsPair._(key12, swap12),
            AsPair._(key13, swap13)
        );

        /// <summary>
        /// Swaps depending on the given key.
        /// </summary>
        public static SwapSwitch<TInput, TOutput> _<TInput, TOutput>(
            string key1, ISwap<TInput, TOutput> swap1,
            string key2, ISwap<TInput, TOutput> swap2,
            string key3, ISwap<TInput, TOutput> swap3,
            string key4, ISwap<TInput, TOutput> swap4,
            string key5, ISwap<TInput, TOutput> swap5,
            string key6, ISwap<TInput, TOutput> swap6,
            string key7, ISwap<TInput, TOutput> swap7,
            string key8, ISwap<TInput, TOutput> swap8,
            string key9, ISwap<TInput, TOutput> swap9,
            string key10, ISwap<TInput, TOutput> swap10,
            string key11, ISwap<TInput, TOutput> swap11,
            string key12, ISwap<TInput, TOutput> swap12,
            string key13, ISwap<TInput, TOutput> swap13,
            string key14, ISwap<TInput, TOutput> swap14
        ) => new SwapSwitch<TInput, TOutput>(
            AsPair._(key1, swap1),
            AsPair._(key2, swap2),
            AsPair._(key3, swap3),
            AsPair._(key4, swap4),
            AsPair._(key5, swap5),
            AsPair._(key6, swap6),
            AsPair._(key7, swap7),
            AsPair._(key8, swap8),
            AsPair._(key9, swap9),
            AsPair._(key10, swap10),
            AsPair._(key11, swap11),
            AsPair._(key12, swap12),
            AsPair._(key13, swap13),
            AsPair._(key14, swap14)
        );

        /// <summary>
        /// Swaps depending on the given key.
        /// </summary>
        public static SwapSwitch<TInput, TOutput> _<TInput, TOutput>(
            string key1, ISwap<TInput, TOutput> swap1,
            string key2, ISwap<TInput, TOutput> swap2,
            string key3, ISwap<TInput, TOutput> swap3,
            string key4, ISwap<TInput, TOutput> swap4,
            string key5, ISwap<TInput, TOutput> swap5,
            string key6, ISwap<TInput, TOutput> swap6,
            string key7, ISwap<TInput, TOutput> swap7,
            string key8, ISwap<TInput, TOutput> swap8,
            string key9, ISwap<TInput, TOutput> swap9,
            string key10, ISwap<TInput, TOutput> swap10,
            string key11, ISwap<TInput, TOutput> swap11,
            string key12, ISwap<TInput, TOutput> swap12,
            string key13, ISwap<TInput, TOutput> swap13,
            string key14, ISwap<TInput, TOutput> swap14,
            string key15, ISwap<TInput, TOutput> swap15
        ) => new SwapSwitch<TInput, TOutput>(
            AsPair._(key1, swap1),
            AsPair._(key2, swap2),
            AsPair._(key3, swap3),
            AsPair._(key4, swap4),
            AsPair._(key5, swap5),
            AsPair._(key6, swap6),
            AsPair._(key7, swap7),
            AsPair._(key8, swap8),
            AsPair._(key9, swap9),
            AsPair._(key10, swap10),
            AsPair._(key11, swap11),
            AsPair._(key12, swap12),
            AsPair._(key13, swap13),
            AsPair._(key14, swap14),
            AsPair._(key15, swap15)
        );

        /// <summary>
        /// Swaps depending on the given key.
        /// </summary>
        public static SwapSwitch<TInput, TOutput> _<TInput, TOutput>(
            string key1, ISwap<TInput, TOutput> swap1,
            string key2, ISwap<TInput, TOutput> swap2,
            string key3, ISwap<TInput, TOutput> swap3,
            string key4, ISwap<TInput, TOutput> swap4,
            string key5, ISwap<TInput, TOutput> swap5,
            string key6, ISwap<TInput, TOutput> swap6,
            string key7, ISwap<TInput, TOutput> swap7,
            string key8, ISwap<TInput, TOutput> swap8,
            string key9, ISwap<TInput, TOutput> swap9,
            string key10, ISwap<TInput, TOutput> swap10,
            string key11, ISwap<TInput, TOutput> swap11,
            string key12, ISwap<TInput, TOutput> swap12,
            string key13, ISwap<TInput, TOutput> swap13,
            string key14, ISwap<TInput, TOutput> swap14,
            string key15, ISwap<TInput, TOutput> swap15,
            string key16, ISwap<TInput, TOutput> swap16
        ) => new SwapSwitch<TInput, TOutput>(
            AsPair._(key1, swap1),
            AsPair._(key2, swap2),
            AsPair._(key3, swap3),
            AsPair._(key4, swap4),
            AsPair._(key5, swap5),
            AsPair._(key6, swap6),
            AsPair._(key7, swap7),
            AsPair._(key8, swap8),
            AsPair._(key9, swap9),
            AsPair._(key10, swap10),
            AsPair._(key11, swap11),
            AsPair._(key12, swap12),
            AsPair._(key13, swap13),
            AsPair._(key14, swap14),
            AsPair._(key15, swap15),
            AsPair._(key16, swap16)
        );

        /// <summary>
        /// Swaps depending on the given key.
        /// </summary>
        public static SwapSwitch<TInput1, TOutput> _<TInput1, TOutput>(
            Func<string, TInput1, TOutput> fallback,
            params IPair<string, ISwap<TInput1, TOutput>>[] swaps
        ) =>
            new SwapSwitch<TInput1, TOutput>(
                AsEnumerable._(swaps),
                fallback
            );

        /// <summary>
        /// Swaps depending on the given key.
        /// </summary>
        public static SwapSwitch<TInput1, TOutput> _<TInput1, TOutput>(
            IPair<string, ISwap<TInput1, TOutput>>[] swaps
        ) =>
            new SwapSwitch<TInput1, TOutput>(
                AsEnumerable._(swaps)
            );

        /// <summary>
        /// Swaps depending on the given key.
        /// </summary>
        public static SwapSwitch<TInput1, TOutput> _<TInput1, TOutput>(
            IEnumerable<IPair<string, ISwap<TInput1, TOutput>>> swap
        ) =>
            new SwapSwitch<TInput1, TOutput>(
                swap,
                (unknown, input1) => throw new ArgumentException($"Cannot swap unknown type '{unknown}'")
            );

        /// <summary>
        /// Swaps depending on the given key.
        /// </summary>
        public static SwapSwitch<TInput1, TOutput> _<TInput1, TOutput>(
            IEnumerable<IPair<string, ISwap<TInput1, TOutput>>> swaps,
            Func<string, TInput1, TOutput> fallback
        ) =>
            new SwapSwitch<TInput1, TOutput>(swaps, fallback);

        /// <summary>
        /// Swaps depending on the given key.
        /// </summary>
        public static SwapSwitch<TInput1, TInput2, TOutput> _<TInput1, TInput2, TOutput>(
            string key1, ISwap<TInput1, TInput2, TOutput> swap1,
            string key2, ISwap<TInput1, TInput2, TOutput> swap2
        ) => new SwapSwitch<TInput1, TInput2, TOutput>(
            AsPair._(key1, swap1),
            AsPair._(key2, swap2)
        );

        /// <summary>
        /// Swaps depending on the given key.
        /// </summary>
        public static SwapSwitch<TInput1, TInput2, TOutput> _<TInput1, TInput2, TOutput>(
            string key1, ISwap<TInput1, TInput2, TOutput> swap1,
            string key2, ISwap<TInput1, TInput2, TOutput> swap2,
            string key3, ISwap<TInput1, TInput2, TOutput> swap3
        ) => new SwapSwitch<TInput1, TInput2, TOutput>(
            AsPair._(key1, swap1),
            AsPair._(key2, swap2),
            AsPair._(key3, swap3)
        );

        /// <summary>
        /// Swaps depending on the given key.
        /// </summary>
        public static SwapSwitch<TInput1, TInput2, TOutput> _<TInput1, TInput2, TOutput>(
            string key1, ISwap<TInput1, TInput2, TOutput> swap1,
            string key2, ISwap<TInput1, TInput2, TOutput> swap2,
            string key3, ISwap<TInput1, TInput2, TOutput> swap3,
            string key4, ISwap<TInput1, TInput2, TOutput> swap4
        ) => new SwapSwitch<TInput1, TInput2, TOutput>(
            AsPair._(key1, swap1),
            AsPair._(key2, swap2),
            AsPair._(key3, swap3),
            AsPair._(key4, swap4)
        );

        /// <summary>
        /// Swaps depending on the given key.
        /// </summary>
        public static SwapSwitch<TInput1, TInput2, TOutput> _<TInput1, TInput2, TOutput>(
            string key1, ISwap<TInput1, TInput2, TOutput> swap1,
            string key2, ISwap<TInput1, TInput2, TOutput> swap2,
            string key3, ISwap<TInput1, TInput2, TOutput> swap3,
            string key4, ISwap<TInput1, TInput2, TOutput> swap4,
            string key5, ISwap<TInput1, TInput2, TOutput> swap5
        ) => new SwapSwitch<TInput1, TInput2, TOutput>(
            AsPair._(key1, swap1),
            AsPair._(key2, swap2),
            AsPair._(key3, swap3),
            AsPair._(key4, swap4),
            AsPair._(key5, swap5)
        );

        /// <summary>
        /// Swaps depending on the given key.
        /// </summary>
        public static SwapSwitch<TInput1, TInput2, TOutput> _<TInput1, TInput2, TOutput>(
            string key1, ISwap<TInput1, TInput2, TOutput> swap1,
            string key2, ISwap<TInput1, TInput2, TOutput> swap2,
            string key3, ISwap<TInput1, TInput2, TOutput> swap3,
            string key4, ISwap<TInput1, TInput2, TOutput> swap4,
            string key5, ISwap<TInput1, TInput2, TOutput> swap5,
            string key6, ISwap<TInput1, TInput2, TOutput> swap6
        ) => new SwapSwitch<TInput1, TInput2, TOutput>(
            AsPair._(key1, swap1),
            AsPair._(key2, swap2),
            AsPair._(key3, swap3),
            AsPair._(key4, swap4),
            AsPair._(key5, swap5),
            AsPair._(key6, swap6)
        );

        /// <summary>
        /// Swaps depending on the given key.
        /// </summary>
        public static SwapSwitch<TInput1, TInput2, TOutput> _<TInput1, TInput2, TOutput>(
            string key1, ISwap<TInput1, TInput2, TOutput> swap1,
            string key2, ISwap<TInput1, TInput2, TOutput> swap2,
            string key3, ISwap<TInput1, TInput2, TOutput> swap3,
            string key4, ISwap<TInput1, TInput2, TOutput> swap4,
            string key5, ISwap<TInput1, TInput2, TOutput> swap5,
            string key6, ISwap<TInput1, TInput2, TOutput> swap6,
            string key7, ISwap<TInput1, TInput2, TOutput> swap7
        ) => new SwapSwitch<TInput1, TInput2, TOutput>(
            AsPair._(key1, swap1),
            AsPair._(key2, swap2),
            AsPair._(key3, swap3),
            AsPair._(key4, swap4),
            AsPair._(key5, swap5),
            AsPair._(key6, swap6),
            AsPair._(key7, swap7)
        );

        /// <summary>
        /// Swaps depending on the given key.
        /// </summary>
        public static SwapSwitch<TInput1, TInput2, TOutput> _<TInput1, TInput2, TOutput>(
            string key1, ISwap<TInput1, TInput2, TOutput> swap1,
            string key2, ISwap<TInput1, TInput2, TOutput> swap2,
            string key3, ISwap<TInput1, TInput2, TOutput> swap3,
            string key4, ISwap<TInput1, TInput2, TOutput> swap4,
            string key5, ISwap<TInput1, TInput2, TOutput> swap5,
            string key6, ISwap<TInput1, TInput2, TOutput> swap6,
            string key7, ISwap<TInput1, TInput2, TOutput> swap7,
            string key8, ISwap<TInput1, TInput2, TOutput> swap8
        ) => new SwapSwitch<TInput1, TInput2, TOutput>(
            AsPair._(key1, swap1),
            AsPair._(key2, swap2),
            AsPair._(key3, swap3),
            AsPair._(key4, swap4),
            AsPair._(key5, swap5),
            AsPair._(key6, swap6),
            AsPair._(key7, swap7),
            AsPair._(key8, swap8)
        );

        /// <summary>
        /// Swaps depending on the given key.
        /// </summary>
        public static SwapSwitch<TInput1, TInput2, TOutput> _<TInput1, TInput2, TOutput>(
            string key1, ISwap<TInput1, TInput2, TOutput> swap1,
            string key2, ISwap<TInput1, TInput2, TOutput> swap2,
            string key3, ISwap<TInput1, TInput2, TOutput> swap3,
            string key4, ISwap<TInput1, TInput2, TOutput> swap4,
            string key5, ISwap<TInput1, TInput2, TOutput> swap5,
            string key6, ISwap<TInput1, TInput2, TOutput> swap6,
            string key7, ISwap<TInput1, TInput2, TOutput> swap7,
            string key8, ISwap<TInput1, TInput2, TOutput> swap8,
            string key9, ISwap<TInput1, TInput2, TOutput> swap9
        ) => new SwapSwitch<TInput1, TInput2, TOutput>(
            AsPair._(key1, swap1),
            AsPair._(key2, swap2),
            AsPair._(key3, swap3),
            AsPair._(key4, swap4),
            AsPair._(key5, swap5),
            AsPair._(key6, swap6),
            AsPair._(key7, swap7),
            AsPair._(key8, swap8),
            AsPair._(key9, swap9)
        );

        /// <summary>
        /// Swaps depending on the given key.
        /// </summary>
        public static SwapSwitch<TInput1, TInput2, TOutput> _<TInput1, TInput2, TOutput>(
            string key1, ISwap<TInput1, TInput2, TOutput> swap1,
            string key2, ISwap<TInput1, TInput2, TOutput> swap2,
            string key3, ISwap<TInput1, TInput2, TOutput> swap3,
            string key4, ISwap<TInput1, TInput2, TOutput> swap4,
            string key5, ISwap<TInput1, TInput2, TOutput> swap5,
            string key6, ISwap<TInput1, TInput2, TOutput> swap6,
            string key7, ISwap<TInput1, TInput2, TOutput> swap7,
            string key8, ISwap<TInput1, TInput2, TOutput> swap8,
            string key9, ISwap<TInput1, TInput2, TOutput> swap9,
            string key10, ISwap<TInput1, TInput2, TOutput> swap10
        ) => new SwapSwitch<TInput1, TInput2, TOutput>(
            AsPair._(key1, swap1),
            AsPair._(key2, swap2),
            AsPair._(key3, swap3),
            AsPair._(key4, swap4),
            AsPair._(key5, swap5),
            AsPair._(key6, swap6),
            AsPair._(key7, swap7),
            AsPair._(key8, swap8),
            AsPair._(key9, swap9),
            AsPair._(key10, swap10)
        );

        /// <summary>
        /// Swaps depending on the given key.
        /// </summary>
        public static SwapSwitch<TInput1, TInput2, TOutput> _<TInput1, TInput2, TOutput>(
            string key1, ISwap<TInput1, TInput2, TOutput> swap1,
            string key2, ISwap<TInput1, TInput2, TOutput> swap2,
            string key3, ISwap<TInput1, TInput2, TOutput> swap3,
            string key4, ISwap<TInput1, TInput2, TOutput> swap4,
            string key5, ISwap<TInput1, TInput2, TOutput> swap5,
            string key6, ISwap<TInput1, TInput2, TOutput> swap6,
            string key7, ISwap<TInput1, TInput2, TOutput> swap7,
            string key8, ISwap<TInput1, TInput2, TOutput> swap8,
            string key9, ISwap<TInput1, TInput2, TOutput> swap9,
            string key10, ISwap<TInput1, TInput2, TOutput> swap10,
            string key11, ISwap<TInput1, TInput2, TOutput> swap11
        ) => new SwapSwitch<TInput1, TInput2, TOutput>(
            AsPair._(key1, swap1),
            AsPair._(key2, swap2),
            AsPair._(key3, swap3),
            AsPair._(key4, swap4),
            AsPair._(key5, swap5),
            AsPair._(key6, swap6),
            AsPair._(key7, swap7),
            AsPair._(key8, swap8),
            AsPair._(key9, swap9),
            AsPair._(key10, swap10),
            AsPair._(key11, swap11)
        );

        /// <summary>
        /// Swaps depending on the given key.
        /// </summary>
        public static SwapSwitch<TInput1, TInput2, TOutput> _<TInput1, TInput2, TOutput>(
            string key1, ISwap<TInput1, TInput2, TOutput> swap1,
            string key2, ISwap<TInput1, TInput2, TOutput> swap2,
            string key3, ISwap<TInput1, TInput2, TOutput> swap3,
            string key4, ISwap<TInput1, TInput2, TOutput> swap4,
            string key5, ISwap<TInput1, TInput2, TOutput> swap5,
            string key6, ISwap<TInput1, TInput2, TOutput> swap6,
            string key7, ISwap<TInput1, TInput2, TOutput> swap7,
            string key8, ISwap<TInput1, TInput2, TOutput> swap8,
            string key9, ISwap<TInput1, TInput2, TOutput> swap9,
            string key10, ISwap<TInput1, TInput2, TOutput> swap10,
            string key11, ISwap<TInput1, TInput2, TOutput> swap11,
            string key12, ISwap<TInput1, TInput2, TOutput> swap12
        ) => new SwapSwitch<TInput1, TInput2, TOutput>(
            AsPair._(key1, swap1),
            AsPair._(key2, swap2),
            AsPair._(key3, swap3),
            AsPair._(key4, swap4),
            AsPair._(key5, swap5),
            AsPair._(key6, swap6),
            AsPair._(key7, swap7),
            AsPair._(key8, swap8),
            AsPair._(key9, swap9),
            AsPair._(key10, swap10),
            AsPair._(key11, swap11),
            AsPair._(key12, swap12)
        );

        /// <summary>
        /// Swaps depending on the given key.
        /// </summary>
        public static SwapSwitch<TInput1, TInput2, TOutput> _<TInput1, TInput2, TOutput>(
            string key1, ISwap<TInput1, TInput2, TOutput> swap1,
            string key2, ISwap<TInput1, TInput2, TOutput> swap2,
            string key3, ISwap<TInput1, TInput2, TOutput> swap3,
            string key4, ISwap<TInput1, TInput2, TOutput> swap4,
            string key5, ISwap<TInput1, TInput2, TOutput> swap5,
            string key6, ISwap<TInput1, TInput2, TOutput> swap6,
            string key7, ISwap<TInput1, TInput2, TOutput> swap7,
            string key8, ISwap<TInput1, TInput2, TOutput> swap8,
            string key9, ISwap<TInput1, TInput2, TOutput> swap9,
            string key10, ISwap<TInput1, TInput2, TOutput> swap10,
            string key11, ISwap<TInput1, TInput2, TOutput> swap11,
            string key12, ISwap<TInput1, TInput2, TOutput> swap12,
            string key13, ISwap<TInput1, TInput2, TOutput> swap13
        ) => new SwapSwitch<TInput1, TInput2, TOutput>(
            AsPair._(key1, swap1),
            AsPair._(key2, swap2),
            AsPair._(key3, swap3),
            AsPair._(key4, swap4),
            AsPair._(key5, swap5),
            AsPair._(key6, swap6),
            AsPair._(key7, swap7),
            AsPair._(key8, swap8),
            AsPair._(key9, swap9),
            AsPair._(key10, swap10),
            AsPair._(key11, swap11),
            AsPair._(key12, swap12),
            AsPair._(key13, swap13)
        );

        /// <summary>
        /// Swaps depending on the given key.
        /// </summary>
        public static SwapSwitch<TInput1, TInput2, TOutput> _<TInput1, TInput2, TOutput>(
            string key1, ISwap<TInput1, TInput2, TOutput> swap1,
            string key2, ISwap<TInput1, TInput2, TOutput> swap2,
            string key3, ISwap<TInput1, TInput2, TOutput> swap3,
            string key4, ISwap<TInput1, TInput2, TOutput> swap4,
            string key5, ISwap<TInput1, TInput2, TOutput> swap5,
            string key6, ISwap<TInput1, TInput2, TOutput> swap6,
            string key7, ISwap<TInput1, TInput2, TOutput> swap7,
            string key8, ISwap<TInput1, TInput2, TOutput> swap8,
            string key9, ISwap<TInput1, TInput2, TOutput> swap9,
            string key10, ISwap<TInput1, TInput2, TOutput> swap10,
            string key11, ISwap<TInput1, TInput2, TOutput> swap11,
            string key12, ISwap<TInput1, TInput2, TOutput> swap12,
            string key13, ISwap<TInput1, TInput2, TOutput> swap13,
            string key14, ISwap<TInput1, TInput2, TOutput> swap14
        ) => new SwapSwitch<TInput1, TInput2, TOutput>(
            AsPair._(key1, swap1),
            AsPair._(key2, swap2),
            AsPair._(key3, swap3),
            AsPair._(key4, swap4),
            AsPair._(key5, swap5),
            AsPair._(key6, swap6),
            AsPair._(key7, swap7),
            AsPair._(key8, swap8),
            AsPair._(key9, swap9),
            AsPair._(key10, swap10),
            AsPair._(key11, swap11),
            AsPair._(key12, swap12),
            AsPair._(key13, swap13),
            AsPair._(key14, swap14)
        );

        /// <summary>
        /// Swaps depending on the given key.
        /// </summary>
        public static SwapSwitch<TInput1, TInput2, TOutput> _<TInput1, TInput2, TOutput>(
            string key1, ISwap<TInput1, TInput2, TOutput> swap1,
            string key2, ISwap<TInput1, TInput2, TOutput> swap2,
            string key3, ISwap<TInput1, TInput2, TOutput> swap3,
            string key4, ISwap<TInput1, TInput2, TOutput> swap4,
            string key5, ISwap<TInput1, TInput2, TOutput> swap5,
            string key6, ISwap<TInput1, TInput2, TOutput> swap6,
            string key7, ISwap<TInput1, TInput2, TOutput> swap7,
            string key8, ISwap<TInput1, TInput2, TOutput> swap8,
            string key9, ISwap<TInput1, TInput2, TOutput> swap9,
            string key10, ISwap<TInput1, TInput2, TOutput> swap10,
            string key11, ISwap<TInput1, TInput2, TOutput> swap11,
            string key12, ISwap<TInput1, TInput2, TOutput> swap12,
            string key13, ISwap<TInput1, TInput2, TOutput> swap13,
            string key14, ISwap<TInput1, TInput2, TOutput> swap14,
            string key15, ISwap<TInput1, TInput2, TOutput> swap15
        ) => new SwapSwitch<TInput1, TInput2, TOutput>(
            AsPair._(key1, swap1),
            AsPair._(key2, swap2),
            AsPair._(key3, swap3),
            AsPair._(key4, swap4),
            AsPair._(key5, swap5),
            AsPair._(key6, swap6),
            AsPair._(key7, swap7),
            AsPair._(key8, swap8),
            AsPair._(key9, swap9),
            AsPair._(key10, swap10),
            AsPair._(key11, swap11),
            AsPair._(key12, swap12),
            AsPair._(key13, swap13),
            AsPair._(key14, swap14),
            AsPair._(key15, swap15)
        );

        /// <summary>
        /// Swaps depending on the given key.
        /// </summary>
        public static SwapSwitch<TInput1, TInput2, TOutput> _<TInput1, TInput2, TOutput>(
            string key1, ISwap<TInput1, TInput2, TOutput> swap1,
            string key2, ISwap<TInput1, TInput2, TOutput> swap2,
            string key3, ISwap<TInput1, TInput2, TOutput> swap3,
            string key4, ISwap<TInput1, TInput2, TOutput> swap4,
            string key5, ISwap<TInput1, TInput2, TOutput> swap5,
            string key6, ISwap<TInput1, TInput2, TOutput> swap6,
            string key7, ISwap<TInput1, TInput2, TOutput> swap7,
            string key8, ISwap<TInput1, TInput2, TOutput> swap8,
            string key9, ISwap<TInput1, TInput2, TOutput> swap9,
            string key10, ISwap<TInput1, TInput2, TOutput> swap10,
            string key11, ISwap<TInput1, TInput2, TOutput> swap11,
            string key12, ISwap<TInput1, TInput2, TOutput> swap12,
            string key13, ISwap<TInput1, TInput2, TOutput> swap13,
            string key14, ISwap<TInput1, TInput2, TOutput> swap14,
            string key15, ISwap<TInput1, TInput2, TOutput> swap15,
            string key16, ISwap<TInput1, TInput2, TOutput> swap16
        ) => new SwapSwitch<TInput1, TInput2, TOutput>(
            AsPair._(key1, swap1),
            AsPair._(key2, swap2),
            AsPair._(key3, swap3),
            AsPair._(key4, swap4),
            AsPair._(key5, swap5),
            AsPair._(key6, swap6),
            AsPair._(key7, swap7),
            AsPair._(key8, swap8),
            AsPair._(key9, swap9),
            AsPair._(key10, swap10),
            AsPair._(key11, swap11),
            AsPair._(key12, swap12),
            AsPair._(key13, swap13),
            AsPair._(key14, swap14),
            AsPair._(key15, swap15),
            AsPair._(key16, swap16)
        );

        /// <summary>
        /// Swaps depending on the given key.
        /// </summary>
        public static SwapSwitch<TInput1, TInput2, TOutput> _<TInput1, TInput2, TOutput>(
            IEnumerable<IPair<string, ISwap<TInput1, TInput2, TOutput>>> swap
        ) =>
            new SwapSwitch<TInput1, TInput2, TOutput>(
                swap,
                (unknown, input1, input2) => throw new ArgumentException($"Cannot swap unknown type '{unknown}'")
            );

        /// <summary>
        /// Swaps depending on the given key.
        /// </summary>
        public static SwapSwitch<TInput1, TInput2, TOutput> _<TInput1, TInput2, TOutput>(
            IEnumerable<IPair<string, ISwap<TInput1, TInput2, TOutput>>> swaps,
            Func<string, TInput1, TInput2, TOutput> fallback
        ) =>
            new SwapSwitch<TInput1, TInput2, TOutput>(swaps, fallback);

        /// <summary>
        /// Swaps depending on a given key.
        /// </summary>
        public static SwapSwitch<TKey, TInput1, TInput2, TOutput> _<TKey, TInput1, TInput2, TOutput>(
            TKey key1, ISwap<TInput1, TInput2, TOutput> swap1,
            TKey key2, ISwap<TInput1, TInput2, TOutput> swap2
        ) =>
            new SwapSwitch<TKey, TInput1, TInput2, TOutput>(
                AsPair._(key1, swap1),
                AsPair._(key2, swap2)
            );

        /// <summary>
        /// Swaps depending on a given key.
        /// </summary>
        public static SwapSwitch<TKey, TInput1, TInput2, TOutput> _<TKey, TInput1, TInput2, TOutput>(
            TKey key1, ISwap<TInput1, TInput2, TOutput> swap1,
            TKey key2, ISwap<TInput1, TInput2, TOutput> swap2,
            TKey key3, ISwap<TInput1, TInput2, TOutput> swap3
        ) =>
            new SwapSwitch<TKey, TInput1, TInput2, TOutput>(
                AsPair._(key1, swap1),
                AsPair._(key2, swap2),
                AsPair._(key3, swap3)
            );

        /// <summary>
        /// Swaps depending on a given key.
        /// </summary>
        public static SwapSwitch<TKey, TInput1, TInput2, TOutput> _<TKey, TInput1, TInput2, TOutput>(
            TKey key1, ISwap<TInput1, TInput2, TOutput> swap1,
            TKey key2, ISwap<TInput1, TInput2, TOutput> swap2,
            TKey key3, ISwap<TInput1, TInput2, TOutput> swap3,
            TKey key4, ISwap<TInput1, TInput2, TOutput> swap4
        ) =>
            new SwapSwitch<TKey, TInput1, TInput2, TOutput>(
                AsPair._(key1, swap1),
                AsPair._(key2, swap2),
                AsPair._(key3, swap3),
                AsPair._(key4, swap4)
            );

        /// <summary>
        /// Swaps depending on a given key.
        /// </summary>
        public static SwapSwitch<TKey, TInput1, TInput2, TOutput> _<TKey, TInput1, TInput2, TOutput>(
            TKey key1, ISwap<TInput1, TInput2, TOutput> swap1,
            TKey key2, ISwap<TInput1, TInput2, TOutput> swap2,
            TKey key3, ISwap<TInput1, TInput2, TOutput> swap3,
            TKey key4, ISwap<TInput1, TInput2, TOutput> swap4,
            TKey key5, ISwap<TInput1, TInput2, TOutput> swap5
        ) =>
            new SwapSwitch<TKey, TInput1, TInput2, TOutput>(
                AsPair._(key1, swap1),
                AsPair._(key2, swap2),
                AsPair._(key3, swap3),
                AsPair._(key4, swap4),
                AsPair._(key5, swap5)
            );

        /// <summary>
        /// Swaps depending on a given key.
        /// </summary>
        public static SwapSwitch<TKey, TInput1, TInput2, TOutput> _<TKey, TInput1, TInput2, TOutput>(
            TKey key1, ISwap<TInput1, TInput2, TOutput> swap1,
            TKey key2, ISwap<TInput1, TInput2, TOutput> swap2,
            TKey key3, ISwap<TInput1, TInput2, TOutput> swap3,
            TKey key4, ISwap<TInput1, TInput2, TOutput> swap4,
            TKey key5, ISwap<TInput1, TInput2, TOutput> swap5,
            TKey key6, ISwap<TInput1, TInput2, TOutput> swap6
        ) =>
            new SwapSwitch<TKey, TInput1, TInput2, TOutput>(
                AsPair._(key1, swap1),
                AsPair._(key2, swap2),
                AsPair._(key3, swap3),
                AsPair._(key4, swap4),
                AsPair._(key5, swap5),
                AsPair._(key6, swap6)
            );

        /// <summary>
        /// Swaps depending on a given key.
        /// </summary>
        public static SwapSwitch<TKey, TInput1, TInput2, TOutput> _<TKey, TInput1, TInput2, TOutput>(
            TKey key1, ISwap<TInput1, TInput2, TOutput> swap1,
            TKey key2, ISwap<TInput1, TInput2, TOutput> swap2,
            TKey key3, ISwap<TInput1, TInput2, TOutput> swap3,
            TKey key4, ISwap<TInput1, TInput2, TOutput> swap4,
            TKey key5, ISwap<TInput1, TInput2, TOutput> swap5,
            TKey key6, ISwap<TInput1, TInput2, TOutput> swap6,
            TKey key7, ISwap<TInput1, TInput2, TOutput> swap7
        ) =>
            new SwapSwitch<TKey, TInput1, TInput2, TOutput>(
                AsPair._(key1, swap1),
                AsPair._(key2, swap2),
                AsPair._(key3, swap3),
                AsPair._(key4, swap4),
                AsPair._(key5, swap5),
                AsPair._(key6, swap6),
                AsPair._(key7, swap7)
            );

        /// <summary>
        /// Swaps depending on a given key.
        /// </summary>
        public static SwapSwitch<TKey, TInput1, TInput2, TOutput> _<TKey, TInput1, TInput2, TOutput>(
            TKey key1, ISwap<TInput1, TInput2, TOutput> swap1,
            TKey key2, ISwap<TInput1, TInput2, TOutput> swap2,
            TKey key3, ISwap<TInput1, TInput2, TOutput> swap3,
            TKey key4, ISwap<TInput1, TInput2, TOutput> swap4,
            TKey key5, ISwap<TInput1, TInput2, TOutput> swap5,
            TKey key6, ISwap<TInput1, TInput2, TOutput> swap6,
            TKey key7, ISwap<TInput1, TInput2, TOutput> swap7,
            TKey key8, ISwap<TInput1, TInput2, TOutput> swap8
        ) =>
            new SwapSwitch<TKey, TInput1, TInput2, TOutput>(
                AsPair._(key1, swap1),
                AsPair._(key2, swap2),
                AsPair._(key3, swap3),
                AsPair._(key4, swap4),
                AsPair._(key5, swap5),
                AsPair._(key6, swap6),
                AsPair._(key7, swap7),
                AsPair._(key8, swap8)
            );

        /// <summary>
        /// Swaps depending on a given key.
        /// </summary>
        public static SwapSwitch<TKey, TInput1, TInput2, TOutput> _<TKey, TInput1, TInput2, TOutput>(
            TKey key1, ISwap<TInput1, TInput2, TOutput> swap1,
            TKey key2, ISwap<TInput1, TInput2, TOutput> swap2,
            TKey key3, ISwap<TInput1, TInput2, TOutput> swap3,
            TKey key4, ISwap<TInput1, TInput2, TOutput> swap4,
            TKey key5, ISwap<TInput1, TInput2, TOutput> swap5,
            TKey key6, ISwap<TInput1, TInput2, TOutput> swap6,
            TKey key7, ISwap<TInput1, TInput2, TOutput> swap7,
            TKey key8, ISwap<TInput1, TInput2, TOutput> swap8,
            TKey key9, ISwap<TInput1, TInput2, TOutput> swap9
        ) =>
            new SwapSwitch<TKey, TInput1, TInput2, TOutput>(
                AsPair._(key1, swap1),
                AsPair._(key2, swap2),
                AsPair._(key3, swap3),
                AsPair._(key4, swap4),
                AsPair._(key5, swap5),
                AsPair._(key6, swap6),
                AsPair._(key7, swap7),
                AsPair._(key8, swap8),
                AsPair._(key9, swap9)
            );

        /// <summary>
        /// Swaps depending on a given key.
        /// </summary>
        public static SwapSwitch<TKey, TInput1, TInput2, TOutput> _<TKey, TInput1, TInput2, TOutput>(
            TKey key1, ISwap<TInput1, TInput2, TOutput> swap1,
            TKey key2, ISwap<TInput1, TInput2, TOutput> swap2,
            TKey key3, ISwap<TInput1, TInput2, TOutput> swap3,
            TKey key4, ISwap<TInput1, TInput2, TOutput> swap4,
            TKey key5, ISwap<TInput1, TInput2, TOutput> swap5,
            TKey key6, ISwap<TInput1, TInput2, TOutput> swap6,
            TKey key7, ISwap<TInput1, TInput2, TOutput> swap7,
            TKey key8, ISwap<TInput1, TInput2, TOutput> swap8,
            TKey key9, ISwap<TInput1, TInput2, TOutput> swap9,
            TKey key10, ISwap<TInput1, TInput2, TOutput> swap10
        ) =>
            new SwapSwitch<TKey, TInput1, TInput2, TOutput>(
                AsPair._(key1, swap1),
                AsPair._(key2, swap2),
                AsPair._(key3, swap3),
                AsPair._(key4, swap4),
                AsPair._(key5, swap5),
                AsPair._(key6, swap6),
                AsPair._(key7, swap7),
                AsPair._(key8, swap8),
                AsPair._(key9, swap9),
                AsPair._(key10, swap10)
            );

        /// <summary>
        /// Swaps depending on a given key.
        /// </summary>
        public static SwapSwitch<TKey, TInput1, TInput2, TOutput> _<TKey, TInput1, TInput2, TOutput>(
            TKey key1, ISwap<TInput1, TInput2, TOutput> swap1,
            TKey key2, ISwap<TInput1, TInput2, TOutput> swap2,
            TKey key3, ISwap<TInput1, TInput2, TOutput> swap3,
            TKey key4, ISwap<TInput1, TInput2, TOutput> swap4,
            TKey key5, ISwap<TInput1, TInput2, TOutput> swap5,
            TKey key6, ISwap<TInput1, TInput2, TOutput> swap6,
            TKey key7, ISwap<TInput1, TInput2, TOutput> swap7,
            TKey key8, ISwap<TInput1, TInput2, TOutput> swap8,
            TKey key9, ISwap<TInput1, TInput2, TOutput> swap9,
            TKey key10, ISwap<TInput1, TInput2, TOutput> swap10,
            TKey key11, ISwap<TInput1, TInput2, TOutput> swap11
        ) =>
            new SwapSwitch<TKey, TInput1, TInput2, TOutput>(
                AsPair._(key1, swap1),
                AsPair._(key2, swap2),
                AsPair._(key3, swap3),
                AsPair._(key4, swap4),
                AsPair._(key5, swap5),
                AsPair._(key6, swap6),
                AsPair._(key7, swap7),
                AsPair._(key8, swap8),
                AsPair._(key9, swap9),
                AsPair._(key10, swap10),
                AsPair._(key11, swap11)
            );

        /// <summary>
        /// Swaps depending on a given key.
        /// </summary>
        public static SwapSwitch<TKey, TInput1, TInput2, TOutput> _<TKey, TInput1, TInput2, TOutput>(
            TKey key1, ISwap<TInput1, TInput2, TOutput> swap1,
            TKey key2, ISwap<TInput1, TInput2, TOutput> swap2,
            TKey key3, ISwap<TInput1, TInput2, TOutput> swap3,
            TKey key4, ISwap<TInput1, TInput2, TOutput> swap4,
            TKey key5, ISwap<TInput1, TInput2, TOutput> swap5,
            TKey key6, ISwap<TInput1, TInput2, TOutput> swap6,
            TKey key7, ISwap<TInput1, TInput2, TOutput> swap7,
            TKey key8, ISwap<TInput1, TInput2, TOutput> swap8,
            TKey key9, ISwap<TInput1, TInput2, TOutput> swap9,
            TKey key10, ISwap<TInput1, TInput2, TOutput> swap10,
            TKey key11, ISwap<TInput1, TInput2, TOutput> swap11,
            TKey key12, ISwap<TInput1, TInput2, TOutput> swap12
        ) =>
            new SwapSwitch<TKey, TInput1, TInput2, TOutput>(
                AsPair._(key1, swap1),
                AsPair._(key2, swap2),
                AsPair._(key3, swap3),
                AsPair._(key4, swap4),
                AsPair._(key5, swap5),
                AsPair._(key6, swap6),
                AsPair._(key7, swap7),
                AsPair._(key8, swap8),
                AsPair._(key9, swap9),
                AsPair._(key10, swap10),
                AsPair._(key11, swap11),
                AsPair._(key12, swap12)
            );

        /// <summary>
        /// Swaps depending on a given key.
        /// </summary>
        public static SwapSwitch<TKey, TInput1, TInput2, TOutput> _<TKey, TInput1, TInput2, TOutput>(
            TKey key1, ISwap<TInput1, TInput2, TOutput> swap1,
            TKey key2, ISwap<TInput1, TInput2, TOutput> swap2,
            TKey key3, ISwap<TInput1, TInput2, TOutput> swap3,
            TKey key4, ISwap<TInput1, TInput2, TOutput> swap4,
            TKey key5, ISwap<TInput1, TInput2, TOutput> swap5,
            TKey key6, ISwap<TInput1, TInput2, TOutput> swap6,
            TKey key7, ISwap<TInput1, TInput2, TOutput> swap7,
            TKey key8, ISwap<TInput1, TInput2, TOutput> swap8,
            TKey key9, ISwap<TInput1, TInput2, TOutput> swap9,
            TKey key10, ISwap<TInput1, TInput2, TOutput> swap10,
            TKey key11, ISwap<TInput1, TInput2, TOutput> swap11,
            TKey key12, ISwap<TInput1, TInput2, TOutput> swap12,
            TKey key13, ISwap<TInput1, TInput2, TOutput> swap13
        ) =>
            new SwapSwitch<TKey, TInput1, TInput2, TOutput>(
                AsPair._(key1, swap1),
                AsPair._(key2, swap2),
                AsPair._(key3, swap3),
                AsPair._(key4, swap4),
                AsPair._(key5, swap5),
                AsPair._(key6, swap6),
                AsPair._(key7, swap7),
                AsPair._(key8, swap8),
                AsPair._(key9, swap9),
                AsPair._(key10, swap10),
                AsPair._(key11, swap11),
                AsPair._(key12, swap12),
                AsPair._(key13, swap13)
            );

        /// <summary>
        /// Swaps depending on a given key.
        /// </summary>
        public static SwapSwitch<TKey, TInput1, TInput2, TOutput> _<TKey, TInput1, TInput2, TOutput>(
            TKey key1, ISwap<TInput1, TInput2, TOutput> swap1,
            TKey key2, ISwap<TInput1, TInput2, TOutput> swap2,
            TKey key3, ISwap<TInput1, TInput2, TOutput> swap3,
            TKey key4, ISwap<TInput1, TInput2, TOutput> swap4,
            TKey key5, ISwap<TInput1, TInput2, TOutput> swap5,
            TKey key6, ISwap<TInput1, TInput2, TOutput> swap6,
            TKey key7, ISwap<TInput1, TInput2, TOutput> swap7,
            TKey key8, ISwap<TInput1, TInput2, TOutput> swap8,
            TKey key9, ISwap<TInput1, TInput2, TOutput> swap9,
            TKey key10, ISwap<TInput1, TInput2, TOutput> swap10,
            TKey key11, ISwap<TInput1, TInput2, TOutput> swap11,
            TKey key12, ISwap<TInput1, TInput2, TOutput> swap12,
            TKey key13, ISwap<TInput1, TInput2, TOutput> swap13,
            TKey key14, ISwap<TInput1, TInput2, TOutput> swap14
        ) =>
            new SwapSwitch<TKey, TInput1, TInput2, TOutput>(
                AsPair._(key1, swap1),
                AsPair._(key2, swap2),
                AsPair._(key3, swap3),
                AsPair._(key4, swap4),
                AsPair._(key5, swap5),
                AsPair._(key6, swap6),
                AsPair._(key7, swap7),
                AsPair._(key8, swap8),
                AsPair._(key9, swap9),
                AsPair._(key10, swap10),
                AsPair._(key11, swap11),
                AsPair._(key12, swap12),
                AsPair._(key13, swap13),
                AsPair._(key14, swap14)
            );

        /// <summary>
        /// Swaps depending on a given key.
        /// </summary>
        public static SwapSwitch<TKey, TInput1, TInput2, TOutput> _<TKey, TInput1, TInput2, TOutput>(
            TKey key1, ISwap<TInput1, TInput2, TOutput> swap1,
            TKey key2, ISwap<TInput1, TInput2, TOutput> swap2,
            TKey key3, ISwap<TInput1, TInput2, TOutput> swap3,
            TKey key4, ISwap<TInput1, TInput2, TOutput> swap4,
            TKey key5, ISwap<TInput1, TInput2, TOutput> swap5,
            TKey key6, ISwap<TInput1, TInput2, TOutput> swap6,
            TKey key7, ISwap<TInput1, TInput2, TOutput> swap7,
            TKey key8, ISwap<TInput1, TInput2, TOutput> swap8,
            TKey key9, ISwap<TInput1, TInput2, TOutput> swap9,
            TKey key10, ISwap<TInput1, TInput2, TOutput> swap10,
            TKey key11, ISwap<TInput1, TInput2, TOutput> swap11,
            TKey key12, ISwap<TInput1, TInput2, TOutput> swap12,
            TKey key13, ISwap<TInput1, TInput2, TOutput> swap13,
            TKey key14, ISwap<TInput1, TInput2, TOutput> swap14,
            TKey key15, ISwap<TInput1, TInput2, TOutput> swap15
        ) =>
            new SwapSwitch<TKey, TInput1, TInput2, TOutput>(
                AsPair._(key1, swap1),
                AsPair._(key2, swap2),
                AsPair._(key3, swap3),
                AsPair._(key4, swap4),
                AsPair._(key5, swap5),
                AsPair._(key6, swap6),
                AsPair._(key7, swap7),
                AsPair._(key8, swap8),
                AsPair._(key9, swap9),
                AsPair._(key10, swap10),
                AsPair._(key11, swap11),
                AsPair._(key12, swap12),
                AsPair._(key13, swap13),
                AsPair._(key14, swap14),
                AsPair._(key15, swap15)
            );

        /// <summary>
        /// Swaps depending on a given key.
        /// </summary>
        public static SwapSwitch<TKey, TInput1, TInput2, TOutput> _<TKey, TInput1, TInput2, TOutput>(
            TKey key1, ISwap<TInput1, TInput2, TOutput> swap1,
            TKey key2, ISwap<TInput1, TInput2, TOutput> swap2,
            TKey key3, ISwap<TInput1, TInput2, TOutput> swap3,
            TKey key4, ISwap<TInput1, TInput2, TOutput> swap4,
            TKey key5, ISwap<TInput1, TInput2, TOutput> swap5,
            TKey key6, ISwap<TInput1, TInput2, TOutput> swap6,
            TKey key7, ISwap<TInput1, TInput2, TOutput> swap7,
            TKey key8, ISwap<TInput1, TInput2, TOutput> swap8,
            TKey key9, ISwap<TInput1, TInput2, TOutput> swap9,
            TKey key10, ISwap<TInput1, TInput2, TOutput> swap10,
            TKey key11, ISwap<TInput1, TInput2, TOutput> swap11,
            TKey key12, ISwap<TInput1, TInput2, TOutput> swap12,
            TKey key13, ISwap<TInput1, TInput2, TOutput> swap13,
            TKey key14, ISwap<TInput1, TInput2, TOutput> swap14,
            TKey key15, ISwap<TInput1, TInput2, TOutput> swap15,
            TKey key16, ISwap<TInput1, TInput2, TOutput> swap16
        ) =>
            new SwapSwitch<TKey, TInput1, TInput2, TOutput>(
                AsPair._(key1, swap1),
                AsPair._(key2, swap2),
                AsPair._(key3, swap3),
                AsPair._(key4, swap4),
                AsPair._(key5, swap5),
                AsPair._(key6, swap6),
                AsPair._(key7, swap7),
                AsPair._(key8, swap8),
                AsPair._(key9, swap9),
                AsPair._(key10, swap10),
                AsPair._(key11, swap11),
                AsPair._(key12, swap12),
                AsPair._(key13, swap13),
                AsPair._(key14, swap14),
                AsPair._(key15, swap15),
                AsPair._(key16, swap16)
            );

        public static SwapSwitch<TKey, TInput1, TInput2, TOutput> _<TKey, TInput1, TInput2, TOutput>(
            Func<TKey, TInput1, TInput2, TOutput> fallback,
            params IPair<TKey, ISwap<TInput1, TInput2, TOutput>>[] swaps
        ) =>
            new SwapSwitch<TKey, TInput1, TInput2, TOutput>(
                AsEnumerable._(swaps),
                fallback
            );


        public static SwapSwitch<TKey, TInput1, TInput2, TOutput> _<TKey, TInput1, TInput2, TOutput>(
            IPair<TKey, ISwap<TInput1, TInput2, TOutput>>[] swaps
        ) =>
            new SwapSwitch<TKey, TInput1, TInput2, TOutput>(
                AsEnumerable._(swaps)
            );

        public static SwapSwitch<TKey, TInput1, TInput2, TOutput> _<TKey, TInput1, TInput2, TOutput>(
            IEnumerable<IPair<TKey, ISwap<TInput1, TInput2, TOutput>>> swap
        ) =>
            new SwapSwitch<TKey, TInput1, TInput2, TOutput>(
                swap,
                (unknown, input1, input2) => throw new ArgumentException($"Cannot swap unknown type '{unknown}'")
            );

        /// <summary>
        /// Swaps depending on the given key.
        /// </summary>
        public static SwapSwitch<TKey, TInput1, TInput2, TOutput> _<TKey, TInput1, TInput2, TOutput>(
            IEnumerable<IPair<TKey, ISwap<TInput1, TInput2, TOutput>>> swap,
            Func<TKey, TInput1, TInput2, TOutput> fallback
        ) =>
            new SwapSwitch<TKey, TInput1, TInput2, TOutput>(swap, fallback);
    }

}
