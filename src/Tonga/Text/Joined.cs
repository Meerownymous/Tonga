

using System;
using System.Collections.Generic;
using Tonga.Enumerable;

namespace Tonga.Text
{
    /// <summary>
    /// A <see cref="IText"/> of texts joined together.
    /// </summary>
    public sealed class Joined : TextEnvelope
    {
        /// <summary>
        /// Joins texts together with the delimiter between them.
        /// </summary>
        /// <param name="delimit">delimiter</param>
        /// <param name="strs">texts to join</param>
        public Joined(String delimit, IEnumerable<string> strs) : this(
            AsText._(delimit),
                Mapped._(
                    AsText._,
                    strs
                )
            )
        { }

        /// <summary>
        /// Joins texts together with the delimiter between them.
        /// </summary>
        /// <param name="delimit">delimiter</param>
        /// <param name="strs">texts to join</param>
        public Joined(String delimit, params string[] strs) : this(
            AsText._(delimit),
                Mapped._(
                    AsText._,
                    strs
                )
            )
        { }

        /// <summary>
        /// Joins texts together with the delimiter between them.
        /// </summary>
        /// <param name="delimit">delimiter</param>
        /// <param name="txts">texts to join</param>
        public Joined(IText delimit, params IText[] txts) : this(delimit, AsEnumerable._(txts))
        { }

        /// <summary>
        /// Joins texts together with the delimiter between them.
        /// </summary>
        /// <param name="delimit">delimiter</param>
        /// <param name="txts">texts to join</param>
        public Joined(String delimit, params IText[] txts) : this(
            AsText._(delimit),
            () => AsEnumerable._(txts)
        )
        { }

        /// <summary>
        /// Joins texts together with the delimiter between them.
        /// </summary>
        /// <param name="delimit">delimiter</param>
        /// <param name="txts">texts to join</param>
        public Joined(String delimit, IEnumerable<IText> txts) : this(
            AsText._(delimit),
            () => txts
        )
        { }

        /// <summary>
        /// Joins texts together with the delimiter between them.
        /// </summary>
        /// <param name="delimit">delimiter</param>
        /// <param name="txts">texts to join</param>
        public Joined(String delimit, Func<IEnumerable<IText>> txts) : this(
            AsText._(delimit),
            txts
        )
        { }

        /// <summary>
        /// Joins texts together with the delimiter between them.
        /// </summary>
        /// <param name="delimit">delimiter</param>
        /// <param name="txts">texts to join</param>
        public Joined(IText delimit, IScalar<IEnumerable<IText>> txts) : this(
            delimit,
            txts.Value
        )
        { }

        /// <summary>
        /// Joins texts together with the delimiter between them.
        /// </summary>
        /// <param name="delimit">delimiter</param>
        /// <param name="txts">texts to join</param>
        public Joined(IText delimit, IEnumerable<IText> txts) : this(
            delimit,
            () => txts
        )
        { }

        /// <summary>
        /// Joins texts together with the delimiter between them.
        /// </summary>
        /// <param name="delimit">delimiter</param>
        /// <param name="txts">scalars of texts to join</param>
        private Joined(IText delimit, Func<IEnumerable<IText>> txts) : base(
            AsText._(() =>
                String.Join(
                    delimit.AsString(),
                    Mapped._(
                        text => text.AsString(),
                        txts()
                    )
                )
            )
        )
        { }


        /// <summary>
        /// Joins texts together with the delimiter between them.
        /// </summary>
        /// <param name="delimit">delimiter</param>
        /// <param name="strs">texts to join</param>
        public static Joined _(String delimit, IEnumerable<string> strs) => new(delimit, strs);

        /// <summary>
        /// Joins texts together with the delimiter between them.
        /// </summary>
        /// <param name="delimit">delimiter</param>
        /// <param name="strs">texts to join</param>
        public static Joined _(String delimit, params string[] strs) => new(delimit, strs);

        /// <summary>
        /// Joins texts together with the delimiter between them.
        /// </summary>
        /// <param name="delimit">delimiter</param>
        /// <param name="txts">texts to join</param>
        public static Joined _(IText delimit, params IText[] txts) => new(delimit, txts);

        /// <summary>
        /// Joins texts together with the delimiter between them.
        /// </summary>
        /// <param name="delimit">delimiter</param>
        /// <param name="txts">texts to join</param>
        public static Joined _(String delimit, params IText[] txts) => new(delimit, txts);

        /// <summary>
        /// Joins texts together with the delimiter between them.
        /// </summary>
        /// <param name="delimit">delimiter</param>
        /// <param name="txts">texts to join</param>
        public static Joined _(String delimit, IEnumerable<IText> txts) => new(delimit, txts);

        /// <summary>
        /// Joins texts together with the delimiter between them.
        /// </summary>
        /// <param name="delimit">delimiter</param>
        /// <param name="txts">texts to join</param>
        public static Joined _(String delimit, Func<IEnumerable<IText>> txts) => new(delimit, txts);

        /// <summary>
        /// Joins texts together with the delimiter between them.
        /// </summary>
        /// <param name="delimit">delimiter</param>
        /// <param name="txts">texts to join</param>
        public static Joined _(IText delimit, IScalar<IEnumerable<IText>> txts) => new(delimit, txts);

        /// <summary>
        /// Joins texts together with the delimiter between them.
        /// </summary>
        /// <param name="delimit">delimiter</param>
        /// <param name="txts">texts to join</param>
        public static Joined _(IText delimit, IEnumerable<IText> txts) => new(delimit, txts);
    }
}
