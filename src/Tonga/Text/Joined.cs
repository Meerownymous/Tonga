

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
        /// Joins A <see cref="IText"/>s together with the delimiter between them.
        /// </summary>
        /// <param name="delimit">delimiter</param>
        /// <param name="strs">texts to join</param>
        public Joined(String delimit, params String[] strs) : this(delimit, false, strs)
        { }

        /// <summary>
        /// Joins A <see cref="IText"/>s together with the delimiter between them.
        /// </summary>
        /// <param name="delimit">delimiter</param>
        /// <param name="strs">texts to join</param>
        /// <param name="live">should the object build its value live, every time it is used?</param>
        public Joined(String delimit, bool live, params String[] strs) :
            this(
                delimit,
                new ManyOf<string>(strs),
                live
            )
        { }

        /// <summary>
        /// Joins texts together with the delimiter between them.
        /// </summary>
        /// <param name="delimit">delimiter</param>
        /// <param name="strs">texts to join</param>
        /// <param name="live">should the object build its value live, every time it is used?</param>
        public Joined(String delimit, IEnumerable<string> strs, bool live = false) :
            this(
                new LiveText(delimit),
                new Mapped<string, IText>(
                    (text) => new LiveText(text),
                    strs
                ),
                live
            )
        { }

        /// <summary>
        /// Joins texts together with the delimiter between them.
        /// </summary>
        /// <param name="delimit">delimiter</param>
        /// <param name="txts">texts to join</param>
        public Joined(IText delimit, params IText[] txts) : this(delimit, false, txts)
        { }

        /// <summary>
        /// Joins texts together with the delimiter between them.
        /// </summary>
        /// <param name="delimit">delimiter</param>
        /// <param name="txts">texts to join</param>
        /// <param name="live">should the object build its value live, every time it is used?</param>
        public Joined(IText delimit, bool live, params IText[] txts) : this(delimit, new ManyOf<IText>(txts), live)
        { }

        /// <summary>
        /// Joins texts together with the delimiter between them.
        /// </summary>
        /// <param name="delimit">delimiter</param>
        /// <param name="txts">texts to join</param>
        public Joined(String delimit, params IText[] txts) : this(delimit, false, txts)
        { }

        /// <summary>
        /// Joins texts together with the delimiter between them.
        /// </summary>
        /// <param name="delimit">delimiter</param>
        /// <param name="txts">texts to join</param>
        /// <param name="live">should the object build its value live, every time it is used?</param>
        public Joined(String delimit, bool live, params IText[] txts) : this(
            new LiveText(delimit),
            () => new LiveMany<IText>(txts),
            live
        )
        { }

        /// <summary>
        /// Joins texts together with the delimiter between them.
        /// </summary>
        /// <param name="delimit">delimiter</param>
        /// <param name="txts">texts to join</param>
        /// <param name="live">should the object build its value live, every time it is used?</param>
        public Joined(String delimit, IEnumerable<IText> txts, bool live = false) : this(
            new LiveText(delimit),
            () => txts,
            live
        )
        { }

        /// <summary>
        /// Joins texts together with the delimiter between them.
        /// </summary>
        /// <param name="delimit">delimiter</param>
        /// <param name="txts">texts to join</param>
        /// <param name="live">should the object build its value live, every time it is used?</param>
        public Joined(String delimit, System.Func<IEnumerable<IText>> txts, bool live = false) : this(
            new LiveText(delimit),
            txts,
            live
        )
        { }

        /// <summary>
        /// Joins texts together with the delimiter between them.
        /// </summary>
        /// <param name="delimit">delimiter</param>
        /// <param name="txts">texts to join</param>
        /// <param name="live">should the object build its value live, every time it is used?</param>
        public Joined(IText delimit, IScalar<IEnumerable<IText>> txts, bool live = false) : this(
            delimit,
            () => txts.Value(),
            live
        )
        { }

        /// <summary>
        /// Joins texts together with the delimiter between them.
        /// </summary>
        /// <param name="delimit">delimiter</param>
        /// <param name="txts">texts to join</param>
        /// <param name="live">should the object build its value live, every time it is used?</param>
        public Joined(IText delimit, IEnumerable<IText> txts, bool live = false) : this(
            delimit,
            () => txts,
            live
        )
        { }

        /// <summary>
        /// Joins texts together with the delimiter between them.
        /// </summary>
        /// <param name="delimit">delimiter</param>
        /// <param name="txts">scalars of texts to join</param>
        /// <param name="live">should the object build its value live, every time it is used?</param>
        private Joined(IText delimit, Func<IEnumerable<IText>> txts, bool live = false) : base(() =>
            String.Join(
                delimit.AsString(),
                new Mapped<IText, string>(
                    text => text.AsString(),
                    txts()
                )
            ),
            live
        )
        { }
    }
}
