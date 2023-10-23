

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
        /// <param name="live">should the object build its value live, every time it is used?</param>
        public Joined(String delimit, IEnumerable<string> strs) : this(
            AsText._(delimit),
                Mapped._(AsText._,
                    strs
                )
            )
        { }

        /// <summary>
        /// Joins texts together with the delimiter between them.
        /// </summary>
        /// <param name="delimit">delimiter</param>
        /// <param name="strs">texts to join</param>
        /// <param name="live">should the object build its value live, every time it is used?</param>
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
        /// <param name="live">should the object build its value live, every time it is used?</param>
        public Joined(IText delimit, params IText[] txts) : this(delimit, AsEnumerable._(txts))
        { }

        /// <summary>
        /// Joins texts together with the delimiter between them.
        /// </summary>
        /// <param name="delimit">delimiter</param>
        /// <param name="txts">texts to join</param>
        /// <param name="live">should the object build its value live, every time it is used?</param>
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
        /// <param name="live">should the object build its value live, every time it is used?</param>
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
        /// <param name="live">should the object build its value live, every time it is used?</param>
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
        /// <param name="live">should the object build its value live, every time it is used?</param>
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
        /// <param name="live">should the object build its value live, every time it is used?</param>
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
        /// <param name="live">should the object build its value live, every time it is used?</param>
        private Joined(IText delimit, Func<IEnumerable<IText>> txts) : base(() =>
            String.Join(
                delimit.AsString(),
                new Mapped<IText, string>(
                    text => text.AsString(),
                    txts()
                )
            )
        )
        { }



        /// <summary>
        /// Joins texts together with the delimiter between them.
        /// </summary>
        /// <param name="delimit">delimiter</param>
        /// <param name="strs">texts to join</param>
        /// <param name="live">should the object build its value live, every time it is used?</param>
        public static Joined _(String delimit, IEnumerable<string> strs) => new Joined(delimit, strs);

        /// <summary>
        /// Joins texts together with the delimiter between them.
        /// </summary>
        /// <param name="delimit">delimiter</param>
        /// <param name="strs">texts to join</param>
        /// <param name="live">should the object build its value live, every time it is used?</param>
        public static Joined _(String delimit, params string[] strs) => new Joined(delimit, strs);

        /// <summary>
        /// Joins texts together with the delimiter between them.
        /// </summary>
        /// <param name="delimit">delimiter</param>
        /// <param name="txts">texts to join</param>
        /// <param name="live">should the object build its value live, every time it is used?</param>
        public static Joined _(IText delimit, params IText[] txts) => new Joined(delimit, txts);

        /// <summary>
        /// Joins texts together with the delimiter between them.
        /// </summary>
        /// <param name="delimit">delimiter</param>
        /// <param name="txts">texts to join</param>
        /// <param name="live">should the object build its value live, every time it is used?</param>
        public static Joined _(String delimit, params IText[] txts) => new Joined(delimit, txts);

        /// <summary>
        /// Joins texts together with the delimiter between them.
        /// </summary>
        /// <param name="delimit">delimiter</param>
        /// <param name="txts">texts to join</param>
        /// <param name="live">should the object build its value live, every time it is used?</param>
        public static Joined _(String delimit, IEnumerable<IText> txts) => new Joined(delimit, txts);

        /// <summary>
        /// Joins texts together with the delimiter between them.
        /// </summary>
        /// <param name="delimit">delimiter</param>
        /// <param name="txts">texts to join</param>
        /// <param name="live">should the object build its value live, every time it is used?</param>
        public static Joined _(String delimit, Func<IEnumerable<IText>> txts) => new Joined(delimit, txts);

        /// <summary>
        /// Joins texts together with the delimiter between them.
        /// </summary>
        /// <param name="delimit">delimiter</param>
        /// <param name="txts">texts to join</param>
        /// <param name="live">should the object build its value live, every time it is used?</param>
        public static Joined _(IText delimit, IScalar<IEnumerable<IText>> txts) => new Joined(delimit, txts);

        /// <summary>
        /// Joins texts together with the delimiter between them.
        /// </summary>
        /// <param name="delimit">delimiter</param>
        /// <param name="txts">texts to join</param>
        /// <param name="live">should the object build its value live, every time it is used?</param>
        public static Joined _(IText delimit, IEnumerable<IText> txts) => new Joined(delimit, txts);
    }
}
