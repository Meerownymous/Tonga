

using System;
using System.Collections.Generic;
using Tonga.Enumerable;
using Tonga.Map;

namespace Tonga.Func
{
    /// <summary>
    /// An action fork that is dependant on a named condition.
    /// </summary>
    public sealed class ActionSwitch<In> : IAction<string, In>
    {
        private readonly MapOf<Action<In>> map;
        private readonly Action<string, In> fallback;

        /// <summary>
        /// An action fork that is dependant on a named condition.
        /// </summary>
        public ActionSwitch(params IKvp<Action<In>>[] consequences) : this(
            EnumerableOf.Pipe(consequences)
        )
        { }

        /// <summary>
        /// An action fork that is dependant on a named condition.
        /// </summary>
        public ActionSwitch(
            IKvp<Action<In>> consequence1,
            IKvp<Action<In>> consequence2
        ) : this(
                EnumerableOf.Pipe(
                    consequence1,
                    consequence2
                ),
                (unknown, input) => throw new ArgumentException($"Cannot find action for '{unknown}'")
        )
        { }

        /// <summary>
        /// An action fork that is dependant on a named condition.
        /// </summary>
        public ActionSwitch(
            IKvp<Action<In>> consequence1,
            IKvp<Action<In>> consequence2,
            IKvp<Action<In>> consequence3
        ) : this(
                EnumerableOf.Pipe(
                    consequence1,
                    consequence2,
                    consequence3
                ),
                (unknown, input) => throw new ArgumentException($"Cannot find action for '{unknown}'")
        )
        { }

        /// <summary>
        /// An action fork that is dependant on a named condition.
        /// </summary>
        public ActionSwitch(
            IKvp<Action<In>> consequence1,
            IKvp<Action<In>> consequence2,
            IKvp<Action<In>> consequence3,
            IKvp<Action<In>> consequence4
        ) : this(
                EnumerableOf.Pipe(
                    consequence1,
                    consequence2,
                    consequence3,
                    consequence4
                ),
                (unknown, input) => throw new ArgumentException($"Cannot find action for '{unknown}'")
        )
        { }

        /// <summary>
        /// An action fork that is dependant on a named condition.
        /// </summary>
        public ActionSwitch(
            IKvp<Action<In>> consequence1,
            IKvp<Action<In>> consequence2,
            IKvp<Action<In>> consequence3,
            IKvp<Action<In>> consequence4,
            IKvp<Action<In>> consequence5
        ) : this(
                EnumerableOf.Pipe(
                    consequence1,
                    consequence2,
                    consequence3,
                    consequence4,
                    consequence5
                ),
                (unknown, input) => throw new ArgumentException($"Cannot find action for '{unknown}'")
        )
        { }

        /// <summary>
        /// An action fork that is dependant on a named condition.
        /// </summary>
        public ActionSwitch(
            IKvp<Action<In>> consequence1,
            IKvp<Action<In>> consequence2,
            IKvp<Action<In>> consequence3,
            IKvp<Action<In>> consequence4,
            IKvp<Action<In>> consequence5,
            IKvp<Action<In>> consequence6
        ) : this(
                EnumerableOf.Pipe(
                    consequence1,
                    consequence2,
                    consequence3,
                    consequence4,
                    consequence5,
                    consequence6
                ),
                (unknown, input) => throw new ArgumentException($"Cannot find action for '{unknown}'")
        )
        { }

        /// <summary>
        /// An action fork that is dependant on a named condition.
        /// </summary>
        public ActionSwitch(
            IKvp<Action<In>> consequence1,
            IKvp<Action<In>> consequence2,
            IKvp<Action<In>> consequence3,
            IKvp<Action<In>> consequence4,
            IKvp<Action<In>> consequence5,
            IKvp<Action<In>> consequence6,
            IKvp<Action<In>> consequence7
        ) : this(
                EnumerableOf.Pipe(
                    consequence1,
                    consequence2,
                    consequence3,
                    consequence4,
                    consequence5,
                    consequence6,
                    consequence7
                ),
                (unknown, input) => throw new ArgumentException($"Cannot find action for '{unknown}'")
        )
        { }

        /// <summary>
        /// An action fork that is dependant on a named condition.
        /// </summary>
        public ActionSwitch(
            IKvp<Action<In>> consequence1,
            IKvp<Action<In>> consequence2,
            IKvp<Action<In>> consequence3,
            IKvp<Action<In>> consequence4,
            IKvp<Action<In>> consequence5,
            IKvp<Action<In>> consequence6,
            IKvp<Action<In>> consequence7,
            IKvp<Action<In>> consequence8
        ) : this(
                EnumerableOf.Pipe(
                    consequence1,
                    consequence2,
                    consequence3,
                    consequence4,
                    consequence5,
                    consequence6,
                    consequence7,
                    consequence8
                ),
                (unknown, input) => throw new ArgumentException($"Cannot find action for '{unknown}'")
        )
        { }

        /// <summary>
        /// An action fork that is dependant on a named condition.
        /// </summary>
        public ActionSwitch(
            IKvp<Action<In>> consequence1,
            IKvp<Action<In>> consequence2,
            Action<string, In> fallback
        ) : this(
                EnumerableOf.Pipe(
                    consequence1,
                    consequence2
                ),
                fallback
        )
        { }

        /// <summary>
        /// An action fork that is dependant on a named condition.
        /// </summary>
        public ActionSwitch(
            IKvp<Action<In>> consequence1,
            IKvp<Action<In>> consequence2,
            IKvp<Action<In>> consequence3,
            Action<string, In> fallback
        ) : this(
                EnumerableOf.Pipe(
                    consequence1,
                    consequence2,
                    consequence3
                ),
                fallback
        )
        { }

        /// <summary>
        /// An action fork that is dependant on a named condition.
        /// </summary>
        public ActionSwitch(
            IKvp<Action<In>> consequence1,
            IKvp<Action<In>> consequence2,
            IKvp<Action<In>> consequence3,
            IKvp<Action<In>> consequence4,
            Action<string, In> fallback
        ) : this(
                EnumerableOf.Pipe(
                    consequence1,
                    consequence2,
                    consequence3,
                    consequence4
                ),
                fallback
        )
        { }

        /// <summary>
        /// An action fork that is dependant on a named condition.
        /// </summary>
        public ActionSwitch(
            IKvp<Action<In>> consequence1,
            IKvp<Action<In>> consequence2,
            IKvp<Action<In>> consequence3,
            IKvp<Action<In>> consequence4,
            IKvp<Action<In>> consequence5,
            Action<string, In> fallback
        ) : this(
                EnumerableOf.Pipe(
                    consequence1,
                    consequence2,
                    consequence3,
                    consequence4,
                    consequence5
                ),
                fallback
        )
        { }

        /// <summary>
        /// An action fork that is dependant on a named condition.
        /// </summary>
        public ActionSwitch(
            IKvp<Action<In>> consequence1,
            IKvp<Action<In>> consequence2,
            IKvp<Action<In>> consequence3,
            IKvp<Action<In>> consequence4,
            IKvp<Action<In>> consequence5,
            IKvp<Action<In>> consequence6,
            Action<string, In> fallback
        ) : this(
                EnumerableOf.Pipe(
                    consequence1,
                    consequence2,
                    consequence3,
                    consequence4,
                    consequence5,
                    consequence6
                ),
                fallback
        )
        { }

        /// <summary>
        /// An action fork that is dependant on a named condition.
        /// </summary>
        public ActionSwitch(
            IKvp<Action<In>> consequence1,
            IKvp<Action<In>> consequence2,
            IKvp<Action<In>> consequence3,
            IKvp<Action<In>> consequence4,
            IKvp<Action<In>> consequence5,
            IKvp<Action<In>> consequence6,
            IKvp<Action<In>> consequence7,
            Action<string, In> fallback
        ) : this(
                EnumerableOf.Pipe(
                    consequence1,
                    consequence2,
                    consequence3,
                    consequence4,
                    consequence5,
                    consequence6,
                    consequence7
                ),
                fallback
        )
        { }

        /// <summary>
        /// An action fork that is dependant on a named condition.
        /// </summary>
        public ActionSwitch(
            IKvp<Action<In>> consequence1,
            IKvp<Action<In>> consequence2,
            IKvp<Action<In>> consequence3,
            IKvp<Action<In>> consequence4,
            IKvp<Action<In>> consequence5,
            IKvp<Action<In>> consequence6,
            IKvp<Action<In>> consequence7,
            IKvp<Action<In>> consequence8,
            Action<string, In> fallback
        ) : this(
                EnumerableOf.Pipe(
                    consequence1,
                    consequence2,
                    consequence3,
                    consequence4,
                    consequence5,
                    consequence6,
                    consequence7,
                    consequence8
                ),
                fallback
        )
        { }

        /// <summary>
        /// An action fork that is dependant on a named condition.
        /// </summary>
        public ActionSwitch(IEnumerable<IKvp<Action<In>>> consequences) : this(
            consequences,
            (unknown, input) => throw new ArgumentException($"Cannot find action for '{unknown}'")
        )
        { }

        /// <summary>
        /// An action fork that is dependant on a named condition.
        /// </summary>
        public ActionSwitch(IEnumerable<IKvp<Action<In>>> consequences, Action<string, In> fallback)
        {
            this.map = new MapOf<Action<In>>(consequences);
            this.fallback = fallback;
        }

        public void Invoke(string condition, In input)
        {
            if (!this.map.ContainsKey(condition))
            {
                this.fallback(condition, input);
            }
            else
            {
                this.map[condition].Invoke(input);
            }
        }
    }

    /// <summary>
    /// An action fork that is dependant on a named condition.
    /// </summary>
    public sealed class ActionSwitch<In1, In2> : IAction<string, In1, In2>
    {
        private readonly MapOf<Action<In1, In2>> map;
        private readonly Action<string, In1, In2> fallback;

        /// <summary>
        /// An action fork that is dependant on a named condition.
        /// </summary>
        public ActionSwitch(
            IKvp<Action<In1, In2>> consequence1,
            IKvp<Action<In1, In2>> consequence2
        ) : this(
                EnumerableOf.Pipe(
                    consequence1,
                    consequence2
                ),
                (unknown, input1, input2) => throw new ArgumentException($"Cannot find action for '{unknown}'")
        )
        { }

        /// <summary>
        /// An action fork that is dependant on a named condition.
        /// </summary>
        public ActionSwitch(
            IKvp<Action<In1, In2>> consequence1,
            IKvp<Action<In1, In2>> consequence2,
            IKvp<Action<In1, In2>> consequence3
        ) : this(
                EnumerableOf.Pipe(
                    consequence1,
                    consequence2,
                    consequence3
                ),
                (unknown, input1, input2) => throw new ArgumentException($"Cannot find action for '{unknown}'")
        )
        { }

        /// <summary>
        /// An action fork that is dependant on a named condition.
        /// </summary>
        public ActionSwitch(
            IKvp<Action<In1, In2>> consequence1,
            IKvp<Action<In1, In2>> consequence2,
            IKvp<Action<In1, In2>> consequence3,
            IKvp<Action<In1, In2>> consequence4
        ) : this(
                EnumerableOf.Pipe(
                    consequence1,
                    consequence2,
                    consequence3,
                    consequence4
                ),
                (unknown, input1, input2) => throw new ArgumentException($"Cannot find action for '{unknown}'")
        )
        { }

        /// <summary>
        /// An action fork that is dependant on a named condition.
        /// </summary>
        public ActionSwitch(
            IKvp<Action<In1, In2>> consequence1,
            IKvp<Action<In1, In2>> consequence2,
            IKvp<Action<In1, In2>> consequence3,
            IKvp<Action<In1, In2>> consequence4,
            IKvp<Action<In1, In2>> consequence5
        ) : this(
                EnumerableOf.Pipe(
                    consequence1,
                    consequence2,
                    consequence3,
                    consequence4,
                    consequence5
                ),
                (unknown, input1, input2) => throw new ArgumentException($"Cannot find action for '{unknown}'")
        )
        { }

        /// <summary>
        /// An action fork that is dependant on a named condition.
        /// </summary>
        public ActionSwitch(
            IKvp<Action<In1, In2>> consequence1,
            IKvp<Action<In1, In2>> consequence2,
            IKvp<Action<In1, In2>> consequence3,
            IKvp<Action<In1, In2>> consequence4,
            IKvp<Action<In1, In2>> consequence5,
            IKvp<Action<In1, In2>> consequence6
        ) : this(
                EnumerableOf.Pipe(
                    consequence1,
                    consequence2,
                    consequence3,
                    consequence4,
                    consequence5,
                    consequence6
                ),
                (unknown, input1, input2) => throw new ArgumentException($"Cannot find action for '{unknown}'")
        )
        { }

        /// <summary>
        /// An action fork that is dependant on a named condition.
        /// </summary>
        public ActionSwitch(
            IKvp<Action<In1, In2>> consequence1,
            IKvp<Action<In1, In2>> consequence2,
            IKvp<Action<In1, In2>> consequence3,
            IKvp<Action<In1, In2>> consequence4,
            IKvp<Action<In1, In2>> consequence5,
            IKvp<Action<In1, In2>> consequence6,
            IKvp<Action<In1, In2>> consequence7
        ) : this(
                EnumerableOf.Pipe(
                    consequence1,
                    consequence2,
                    consequence3,
                    consequence4,
                    consequence5,
                    consequence6,
                    consequence7
                ),
                (unknown, input1, input2) => throw new ArgumentException($"Cannot find action for '{unknown}'")
        )
        { }

        /// <summary>
        /// An action fork that is dependant on a named condition.
        /// </summary>
        public ActionSwitch(
            IKvp<Action<In1, In2>> consequence1,
            IKvp<Action<In1, In2>> consequence2,
            IKvp<Action<In1, In2>> consequence3,
            IKvp<Action<In1, In2>> consequence4,
            IKvp<Action<In1, In2>> consequence5,
            IKvp<Action<In1, In2>> consequence6,
            IKvp<Action<In1, In2>> consequence7,
            IKvp<Action<In1, In2>> consequence8
        ) : this(
                EnumerableOf.Pipe(
                    consequence1,
                    consequence2,
                    consequence3,
                    consequence4,
                    consequence5,
                    consequence6,
                    consequence7,
                    consequence8
                ),
                (unknown, input1, input2) => throw new ArgumentException($"Cannot find action for '{unknown}'")
        )
        { }

        /// <summary>
        /// An action fork that is dependant on a named condition.
        /// </summary>
        public ActionSwitch(
            IKvp<Action<In1, In2>> consequence1,
            IKvp<Action<In1, In2>> consequence2,
            Action<string, In1, In2> fallback
        ) : this(
                EnumerableOf.Pipe(
                    consequence1,
                    consequence2
                ),
                fallback
        )
        { }

        /// <summary>
        /// An action fork that is dependant on a named condition.
        /// </summary>
        public ActionSwitch(
            IKvp<Action<In1, In2>> consequence1,
            IKvp<Action<In1, In2>> consequence2,
            IKvp<Action<In1, In2>> consequence3,
            Action<string, In1, In2> fallback
        ) : this(
                EnumerableOf.Pipe(
                    consequence1,
                    consequence2,
                    consequence3
                ),
                fallback
        )
        { }

        /// <summary>
        /// An action fork that is dependant on a named condition.
        /// </summary>
        public ActionSwitch(
            IKvp<Action<In1, In2>> consequence1,
            IKvp<Action<In1, In2>> consequence2,
            IKvp<Action<In1, In2>> consequence3,
            IKvp<Action<In1, In2>> consequence4,
            Action<string, In1, In2> fallback
        ) : this(
                EnumerableOf.Pipe(
                    consequence1,
                    consequence2,
                    consequence3,
                    consequence4
                ),
                fallback
        )
        { }

        /// <summary>
        /// An action fork that is dependant on a named condition.
        /// </summary>
        public ActionSwitch(
            IKvp<Action<In1, In2>> consequence1,
            IKvp<Action<In1, In2>> consequence2,
            IKvp<Action<In1, In2>> consequence3,
            IKvp<Action<In1, In2>> consequence4,
            IKvp<Action<In1, In2>> consequence5,
            Action<string, In1, In2> fallback
        ) : this(
                EnumerableOf.Pipe(
                    consequence1,
                    consequence2,
                    consequence3,
                    consequence4,
                    consequence5
                ),
                fallback
        )
        { }

        /// <summary>
        /// An action fork that is dependant on a named condition.
        /// </summary>
        public ActionSwitch(
            IKvp<Action<In1, In2>> consequence1,
            IKvp<Action<In1, In2>> consequence2,
            IKvp<Action<In1, In2>> consequence3,
            IKvp<Action<In1, In2>> consequence4,
            IKvp<Action<In1, In2>> consequence5,
            IKvp<Action<In1, In2>> consequence6,
            Action<string, In1, In2> fallback
        ) : this(
                EnumerableOf.Pipe(
                    consequence1,
                    consequence2,
                    consequence3,
                    consequence4,
                    consequence5,
                    consequence6
                ),
                fallback
        )
        { }

        /// <summary>
        /// An action fork that is dependant on a named condition.
        /// </summary>
        public ActionSwitch(
            IKvp<Action<In1, In2>> consequence1,
            IKvp<Action<In1, In2>> consequence2,
            IKvp<Action<In1, In2>> consequence3,
            IKvp<Action<In1, In2>> consequence4,
            IKvp<Action<In1, In2>> consequence5,
            IKvp<Action<In1, In2>> consequence6,
            IKvp<Action<In1, In2>> consequence7,
            Action<string, In1, In2> fallback
        ) : this(
                EnumerableOf.Pipe(
                    consequence1,
                    consequence2,
                    consequence3,
                    consequence4,
                    consequence5,
                    consequence6,
                    consequence7
                ),
                fallback
        )
        { }

        /// <summary>
        /// An action fork that is dependant on a named condition.
        /// </summary>
        public ActionSwitch(
            IKvp<Action<In1, In2>> consequence1,
            IKvp<Action<In1, In2>> consequence2,
            IKvp<Action<In1, In2>> consequence3,
            IKvp<Action<In1, In2>> consequence4,
            IKvp<Action<In1, In2>> consequence5,
            IKvp<Action<In1, In2>> consequence6,
            IKvp<Action<In1, In2>> consequence7,
            IKvp<Action<In1, In2>> consequence8,
            Action<string, In1, In2> fallback
        ) : this(
                EnumerableOf.Pipe(
                    consequence1,
                    consequence2,
                    consequence3,
                    consequence4,
                    consequence5,
                    consequence6,
                    consequence7,
                    consequence8
                ),
                fallback
        )
        { }

        /// <summary>
        /// An action fork that is dependant on a named condition.
        /// </summary>
        public ActionSwitch(params IKvp<Action<In1, In2>>[] consequences) : this(
            EnumerableOf.Pipe(consequences)
        )
        { }

        /// <summary>
        /// An action fork that is dependant on a named condition.
        /// </summary>
        public ActionSwitch(IEnumerable<IKvp<Action<In1, In2>>> consequences) : this(
            consequences,
            (unknown, input1, input2) => throw new ArgumentException($"Cannot find action for '{unknown}'")
        )
        { }

        /// <summary>
        /// An action fork that is dependant on a named condition.
        /// </summary>
        public ActionSwitch(IEnumerable<IKvp<Action<In1, In2>>> consequences, Action<string, In1, In2> fallback)
        {
            this.map = new MapOf<Action<In1, In2>>(consequences);
            this.fallback = fallback;
        }

        public void Invoke(string condition, In1 input1, In2 input2)
        {
            if (!this.map.ContainsKey(condition))
            {
                this.fallback(condition, input1, input2);
            }
            else
            {
                this.map[condition].Invoke(input1, input2);
            }
        }
    }

    public static class ActionSwitch
    {
        /// <summary>
        /// An action fork that is dependant on a named condition.
        /// </summary>
        public static IAction<string, In1, In2> New<In1, In2>(
            IKvp<Action<In1, In2>> consequence1,
            IKvp<Action<In1, In2>> consequence2
        ) => new ActionSwitch<In1, In2>(
            consequence1,
            consequence2
        );

        /// <summary>
        /// An action fork that is dependant on a named condition.
        /// </summary>
        public static IAction<string, In1, In2> New<In1, In2>(
            IKvp<Action<In1, In2>> consequence1,
            IKvp<Action<In1, In2>> consequence2,
            IKvp<Action<In1, In2>> consequence3
        ) => new ActionSwitch<In1, In2>(
            consequence1,
            consequence2,
            consequence3
        );

        /// <summary>
        /// An action fork that is dependant on a named condition.
        /// </summary>
        public static IAction<string, In1, In2> New<In1, In2>(
            IKvp<Action<In1, In2>> consequence1,
            IKvp<Action<In1, In2>> consequence2,
            IKvp<Action<In1, In2>> consequence3,
            IKvp<Action<In1, In2>> consequence4
        ) => new ActionSwitch<In1, In2>(
            consequence1,
            consequence2,
            consequence3,
            consequence4
        );

        /// <summary>
        /// An action fork that is dependant on a named condition.
        /// </summary>
        public static IAction<string, In1, In2> New<In1, In2>(
            IKvp<Action<In1, In2>> consequence1,
            IKvp<Action<In1, In2>> consequence2,
            IKvp<Action<In1, In2>> consequence3,
            IKvp<Action<In1, In2>> consequence4,
            IKvp<Action<In1, In2>> consequence5
        ) => new ActionSwitch<In1, In2>(
            consequence1,
            consequence2,
            consequence3,
            consequence4,
            consequence5
        );

        /// <summary>
        /// An action fork that is dependant on a named condition.
        /// </summary>
        public static IAction<string, In1, In2> New<In1, In2>(
            IKvp<Action<In1, In2>> consequence1,
            IKvp<Action<In1, In2>> consequence2,
            IKvp<Action<In1, In2>> consequence3,
            IKvp<Action<In1, In2>> consequence4,
            IKvp<Action<In1, In2>> consequence5,
            IKvp<Action<In1, In2>> consequence6
        ) => new ActionSwitch<In1, In2>(
            consequence1,
            consequence2,
            consequence3,
            consequence4,
            consequence5,
            consequence6
        );

        /// <summary>
        /// An action fork that is dependant on a named condition.
        /// </summary>
        public static IAction<string, In1, In2> New<In1, In2>(
            IKvp<Action<In1, In2>> consequence1,
            IKvp<Action<In1, In2>> consequence2,
            IKvp<Action<In1, In2>> consequence3,
            IKvp<Action<In1, In2>> consequence4,
            IKvp<Action<In1, In2>> consequence5,
            IKvp<Action<In1, In2>> consequence6,
            IKvp<Action<In1, In2>> consequence7

        ) => new ActionSwitch<In1, In2>(
            consequence1,
            consequence2,
            consequence3,
            consequence4,
            consequence5,
            consequence6,
            consequence7
        );

        /// <summary>
        /// An action fork that is dependant on a named condition.
        /// </summary>
        public static IAction<string, In1, In2> New<In1, In2>(
            IKvp<Action<In1, In2>> consequence1,
            IKvp<Action<In1, In2>> consequence2,
            IKvp<Action<In1, In2>> consequence3,
            IKvp<Action<In1, In2>> consequence4,
            IKvp<Action<In1, In2>> consequence5,
            IKvp<Action<In1, In2>> consequence6,
            IKvp<Action<In1, In2>> consequence7,
            IKvp<Action<In1, In2>> consequence8
        ) => new ActionSwitch<In1, In2>(
            consequence1,
            consequence2,
            consequence3,
            consequence4,
            consequence5,
            consequence6,
            consequence7,
            consequence8
        );

        /// <summary>
        /// An action fork that is dependant on a named condition.
        /// </summary>
        public static IAction<string, In1, In2> New<In1, In2>(
            IKvp<Action<In1, In2>> consequence1,
            IKvp<Action<In1, In2>> consequence2,
            Action<string, In1, In2> fallback
        ) => new ActionSwitch<In1, In2>(consequence1, consequence2, fallback);

        /// <summary>
        /// An action fork that is dependant on a named condition.
        /// </summary>
        public static IAction<string, In1, In2> New<In1, In2>(
            IKvp<Action<In1, In2>> consequence1,
            IKvp<Action<In1, In2>> consequence2,
            IKvp<Action<In1, In2>> consequence3,
            Action<string, In1, In2> fallback
        ) => new ActionSwitch<In1, In2>(
            consequence1,
            consequence2,
            consequence3,
            fallback
        );



        /// <summary>
        /// An action fork that is dependant on a named condition.
        /// </summary>
        public static IAction<string, In1, In2> New<In1, In2>(
            IKvp<Action<In1, In2>> consequence1,
            IKvp<Action<In1, In2>> consequence2,
            IKvp<Action<In1, In2>> consequence3,
            IKvp<Action<In1, In2>> consequence4,
            Action<string, In1, In2> fallback
        ) => new ActionSwitch<In1, In2>(
            consequence1,
            consequence2,
            consequence3,
            fallback
        );

        /// <summary>
        /// An action fork that is dependant on a named condition.
        /// </summary>
        public static IAction<string, In1, In2> New<In1, In2>(
            IKvp<Action<In1, In2>> consequence1,
            IKvp<Action<In1, In2>> consequence2,
            IKvp<Action<In1, In2>> consequence3,
            IKvp<Action<In1, In2>> consequence4,
            IKvp<Action<In1, In2>> consequence5,
            Action<string, In1, In2> fallback
        ) => new ActionSwitch<In1, In2>(
            consequence1,
            consequence2,
            consequence3,
            consequence4,
            consequence5,
            fallback
        );

        /// <summary>
        /// An action fork that is dependant on a named condition.
        /// </summary>
        public static IAction<string, In1, In2> New<In1, In2>(
            IKvp<Action<In1, In2>> consequence1,
            IKvp<Action<In1, In2>> consequence2,
            IKvp<Action<In1, In2>> consequence3,
            IKvp<Action<In1, In2>> consequence4,
            IKvp<Action<In1, In2>> consequence5,
            IKvp<Action<In1, In2>> consequence6,
            Action<string, In1, In2> fallback
        ) => new ActionSwitch<In1, In2>(
            consequence1,
            consequence2,
            consequence3,
            consequence4,
            consequence5,
            consequence6,
            fallback
        );

        /// <summary>
        /// An action fork that is dependant on a named condition.
        /// </summary>
        public static IAction<string, In1, In2> New<In1, In2>(
            IKvp<Action<In1, In2>> consequence1,
            IKvp<Action<In1, In2>> consequence2,
            IKvp<Action<In1, In2>> consequence3,
            IKvp<Action<In1, In2>> consequence4,
            IKvp<Action<In1, In2>> consequence5,
            IKvp<Action<In1, In2>> consequence6,
            IKvp<Action<In1, In2>> consequence7,
            Action<string, In1, In2> fallback
        ) => new ActionSwitch<In1, In2>(
            consequence1,
            consequence2,
            consequence3,
            consequence4,
            consequence5,
            consequence6,
            consequence7,
            fallback
        );

        /// <summary>
        /// An action fork that is dependant on a named condition.
        /// </summary>
        public static IAction<string, In1, In2> New<In1, In2>(
            IKvp<Action<In1, In2>> consequence1,
            IKvp<Action<In1, In2>> consequence2,
            IKvp<Action<In1, In2>> consequence3,
            IKvp<Action<In1, In2>> consequence4,
            IKvp<Action<In1, In2>> consequence5,
            IKvp<Action<In1, In2>> consequence6,
            IKvp<Action<In1, In2>> consequence7,
            IKvp<Action<In1, In2>> consequence8,
            Action<string, In1, In2> fallback
        ) => new ActionSwitch<In1, In2>(
            consequence1,
            consequence2,
            consequence3,
            consequence4,
            consequence5,
            consequence6,
            consequence7,
            consequence8,
            fallback
        );

        /// <summary>
        /// An action fork that is dependant on a named condition.
        /// </summary>
        public static IAction<string, In1, In2> New<In1, In2>(params IKvp<Action<In1, In2>>[] consequences) =>
            new ActionSwitch<In1, In2>(consequences);

        /// <summary>
        /// An action fork that is dependant on a named condition.
        /// </summary>
        public static IAction<string, In1, In2> New<In1, In2>(IEnumerable<IKvp<Action<In1, In2>>> consequences) =>
            new ActionSwitch<In1, In2>(consequences);

        /// <summary>
        /// An action fork that is dependant on a named condition.
        /// </summary>
        public static IAction<string, In1, In2> New<In1, In2>(IEnumerable<IKvp<Action<In1, In2>>> consequences, Action<string, In1, In2> fallback) =>
            new ActionSwitch<In1, In2>(consequences);
    }
}
