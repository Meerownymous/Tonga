

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
        public ActionSwitch(params IPair<Action<In>>[] consequences) : this(
            AsEnumerable._(consequences)
        )
        { }

        /// <summary>
        /// An action fork that is dependant on a named condition.
        /// </summary>
        public ActionSwitch(
            IPair<Action<In>> consequence1,
            IPair<Action<In>> consequence2
        ) : this(
                AsEnumerable._(
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
            IPair<Action<In>> consequence1,
            IPair<Action<In>> consequence2,
            IPair<Action<In>> consequence3
        ) : this(
                AsEnumerable._(
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
            IPair<Action<In>> consequence1,
            IPair<Action<In>> consequence2,
            IPair<Action<In>> consequence3,
            IPair<Action<In>> consequence4
        ) : this(
                AsEnumerable._(
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
            IPair<Action<In>> consequence1,
            IPair<Action<In>> consequence2,
            IPair<Action<In>> consequence3,
            IPair<Action<In>> consequence4,
            IPair<Action<In>> consequence5
        ) : this(
                AsEnumerable._(
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
            IPair<Action<In>> consequence1,
            IPair<Action<In>> consequence2,
            IPair<Action<In>> consequence3,
            IPair<Action<In>> consequence4,
            IPair<Action<In>> consequence5,
            IPair<Action<In>> consequence6
        ) : this(
                AsEnumerable._(
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
            IPair<Action<In>> consequence1,
            IPair<Action<In>> consequence2,
            IPair<Action<In>> consequence3,
            IPair<Action<In>> consequence4,
            IPair<Action<In>> consequence5,
            IPair<Action<In>> consequence6,
            IPair<Action<In>> consequence7
        ) : this(
                AsEnumerable._(
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
            IPair<Action<In>> consequence1,
            IPair<Action<In>> consequence2,
            IPair<Action<In>> consequence3,
            IPair<Action<In>> consequence4,
            IPair<Action<In>> consequence5,
            IPair<Action<In>> consequence6,
            IPair<Action<In>> consequence7,
            IPair<Action<In>> consequence8
        ) : this(
                AsEnumerable._(
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
            IPair<Action<In>> consequence1,
            IPair<Action<In>> consequence2,
            Action<string, In> fallback
        ) : this(
                AsEnumerable._(
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
            IPair<Action<In>> consequence1,
            IPair<Action<In>> consequence2,
            IPair<Action<In>> consequence3,
            Action<string, In> fallback
        ) : this(
                AsEnumerable._(
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
            IPair<Action<In>> consequence1,
            IPair<Action<In>> consequence2,
            IPair<Action<In>> consequence3,
            IPair<Action<In>> consequence4,
            Action<string, In> fallback
        ) : this(
                AsEnumerable._(
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
            IPair<Action<In>> consequence1,
            IPair<Action<In>> consequence2,
            IPair<Action<In>> consequence3,
            IPair<Action<In>> consequence4,
            IPair<Action<In>> consequence5,
            Action<string, In> fallback
        ) : this(
                AsEnumerable._(
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
            IPair<Action<In>> consequence1,
            IPair<Action<In>> consequence2,
            IPair<Action<In>> consequence3,
            IPair<Action<In>> consequence4,
            IPair<Action<In>> consequence5,
            IPair<Action<In>> consequence6,
            Action<string, In> fallback
        ) : this(
                AsEnumerable._(
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
            IPair<Action<In>> consequence1,
            IPair<Action<In>> consequence2,
            IPair<Action<In>> consequence3,
            IPair<Action<In>> consequence4,
            IPair<Action<In>> consequence5,
            IPair<Action<In>> consequence6,
            IPair<Action<In>> consequence7,
            Action<string, In> fallback
        ) : this(
                AsEnumerable._(
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
            IPair<Action<In>> consequence1,
            IPair<Action<In>> consequence2,
            IPair<Action<In>> consequence3,
            IPair<Action<In>> consequence4,
            IPair<Action<In>> consequence5,
            IPair<Action<In>> consequence6,
            IPair<Action<In>> consequence7,
            IPair<Action<In>> consequence8,
            Action<string, In> fallback
        ) : this(
                AsEnumerable._(
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
        public ActionSwitch(IEnumerable<IPair<Action<In>>> consequences) : this(
            consequences,
            (unknown, input) => throw new ArgumentException($"Cannot find action for '{unknown}'")
        )
        { }

        /// <summary>
        /// An action fork that is dependant on a named condition.
        /// </summary>
        public ActionSwitch(IEnumerable<IPair<Action<In>>> consequences, Action<string, In> fallback)
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
            IPair<Action<In1, In2>> consequence1,
            IPair<Action<In1, In2>> consequence2
        ) : this(
                AsEnumerable._(
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
            IPair<Action<In1, In2>> consequence1,
            IPair<Action<In1, In2>> consequence2,
            IPair<Action<In1, In2>> consequence3
        ) : this(
                AsEnumerable._(
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
            IPair<Action<In1, In2>> consequence1,
            IPair<Action<In1, In2>> consequence2,
            IPair<Action<In1, In2>> consequence3,
            IPair<Action<In1, In2>> consequence4
        ) : this(
                AsEnumerable._(
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
            IPair<Action<In1, In2>> consequence1,
            IPair<Action<In1, In2>> consequence2,
            IPair<Action<In1, In2>> consequence3,
            IPair<Action<In1, In2>> consequence4,
            IPair<Action<In1, In2>> consequence5
        ) : this(
                AsEnumerable._(
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
            IPair<Action<In1, In2>> consequence1,
            IPair<Action<In1, In2>> consequence2,
            IPair<Action<In1, In2>> consequence3,
            IPair<Action<In1, In2>> consequence4,
            IPair<Action<In1, In2>> consequence5,
            IPair<Action<In1, In2>> consequence6
        ) : this(
                AsEnumerable._(
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
            IPair<Action<In1, In2>> consequence1,
            IPair<Action<In1, In2>> consequence2,
            IPair<Action<In1, In2>> consequence3,
            IPair<Action<In1, In2>> consequence4,
            IPair<Action<In1, In2>> consequence5,
            IPair<Action<In1, In2>> consequence6,
            IPair<Action<In1, In2>> consequence7
        ) : this(
                AsEnumerable._(
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
            IPair<Action<In1, In2>> consequence1,
            IPair<Action<In1, In2>> consequence2,
            IPair<Action<In1, In2>> consequence3,
            IPair<Action<In1, In2>> consequence4,
            IPair<Action<In1, In2>> consequence5,
            IPair<Action<In1, In2>> consequence6,
            IPair<Action<In1, In2>> consequence7,
            IPair<Action<In1, In2>> consequence8
        ) : this(
                AsEnumerable._(
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
            IPair<Action<In1, In2>> consequence1,
            IPair<Action<In1, In2>> consequence2,
            Action<string, In1, In2> fallback
        ) : this(
                AsEnumerable._(
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
            IPair<Action<In1, In2>> consequence1,
            IPair<Action<In1, In2>> consequence2,
            IPair<Action<In1, In2>> consequence3,
            Action<string, In1, In2> fallback
        ) : this(
                AsEnumerable._(
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
            IPair<Action<In1, In2>> consequence1,
            IPair<Action<In1, In2>> consequence2,
            IPair<Action<In1, In2>> consequence3,
            IPair<Action<In1, In2>> consequence4,
            Action<string, In1, In2> fallback
        ) : this(
                AsEnumerable._(
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
            IPair<Action<In1, In2>> consequence1,
            IPair<Action<In1, In2>> consequence2,
            IPair<Action<In1, In2>> consequence3,
            IPair<Action<In1, In2>> consequence4,
            IPair<Action<In1, In2>> consequence5,
            Action<string, In1, In2> fallback
        ) : this(
                AsEnumerable._(
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
            IPair<Action<In1, In2>> consequence1,
            IPair<Action<In1, In2>> consequence2,
            IPair<Action<In1, In2>> consequence3,
            IPair<Action<In1, In2>> consequence4,
            IPair<Action<In1, In2>> consequence5,
            IPair<Action<In1, In2>> consequence6,
            Action<string, In1, In2> fallback
        ) : this(
                AsEnumerable._(
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
            IPair<Action<In1, In2>> consequence1,
            IPair<Action<In1, In2>> consequence2,
            IPair<Action<In1, In2>> consequence3,
            IPair<Action<In1, In2>> consequence4,
            IPair<Action<In1, In2>> consequence5,
            IPair<Action<In1, In2>> consequence6,
            IPair<Action<In1, In2>> consequence7,
            Action<string, In1, In2> fallback
        ) : this(
                AsEnumerable._(
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
            IPair<Action<In1, In2>> consequence1,
            IPair<Action<In1, In2>> consequence2,
            IPair<Action<In1, In2>> consequence3,
            IPair<Action<In1, In2>> consequence4,
            IPair<Action<In1, In2>> consequence5,
            IPair<Action<In1, In2>> consequence6,
            IPair<Action<In1, In2>> consequence7,
            IPair<Action<In1, In2>> consequence8,
            Action<string, In1, In2> fallback
        ) : this(
                AsEnumerable._(
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
        public ActionSwitch(params IPair<Action<In1, In2>>[] consequences) : this(
            AsEnumerable._(consequences)
        )
        { }

        /// <summary>
        /// An action fork that is dependant on a named condition.
        /// </summary>
        public ActionSwitch(IEnumerable<IPair<Action<In1, In2>>> consequences) : this(
            consequences,
            (unknown, input1, input2) => throw new ArgumentException($"Cannot find action for '{unknown}'")
        )
        { }

        /// <summary>
        /// An action fork that is dependant on a named condition.
        /// </summary>
        public ActionSwitch(IEnumerable<IPair<Action<In1, In2>>> consequences, Action<string, In1, In2> fallback)
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
        public static IAction<string, In1, In2> _<In1, In2>(
            IPair<Action<In1, In2>> consequence1,
            IPair<Action<In1, In2>> consequence2
        ) => new ActionSwitch<In1, In2>(
            consequence1,
            consequence2
        );

        /// <summary>
        /// An action fork that is dependant on a named condition.
        /// </summary>
        public static IAction<string, In1, In2> _<In1, In2>(
            IPair<Action<In1, In2>> consequence1,
            IPair<Action<In1, In2>> consequence2,
            IPair<Action<In1, In2>> consequence3
        ) => new ActionSwitch<In1, In2>(
            consequence1,
            consequence2,
            consequence3
        );

        /// <summary>
        /// An action fork that is dependant on a named condition.
        /// </summary>
        public static IAction<string, In1, In2> _<In1, In2>(
            IPair<Action<In1, In2>> consequence1,
            IPair<Action<In1, In2>> consequence2,
            IPair<Action<In1, In2>> consequence3,
            IPair<Action<In1, In2>> consequence4
        ) => new ActionSwitch<In1, In2>(
            consequence1,
            consequence2,
            consequence3,
            consequence4
        );

        /// <summary>
        /// An action fork that is dependant on a named condition.
        /// </summary>
        public static IAction<string, In1, In2> _<In1, In2>(
            IPair<Action<In1, In2>> consequence1,
            IPair<Action<In1, In2>> consequence2,
            IPair<Action<In1, In2>> consequence3,
            IPair<Action<In1, In2>> consequence4,
            IPair<Action<In1, In2>> consequence5
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
        public static IAction<string, In1, In2> _<In1, In2>(
            IPair<Action<In1, In2>> consequence1,
            IPair<Action<In1, In2>> consequence2,
            IPair<Action<In1, In2>> consequence3,
            IPair<Action<In1, In2>> consequence4,
            IPair<Action<In1, In2>> consequence5,
            IPair<Action<In1, In2>> consequence6
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
        public static IAction<string, In1, In2> _<In1, In2>(
            IPair<Action<In1, In2>> consequence1,
            IPair<Action<In1, In2>> consequence2,
            IPair<Action<In1, In2>> consequence3,
            IPair<Action<In1, In2>> consequence4,
            IPair<Action<In1, In2>> consequence5,
            IPair<Action<In1, In2>> consequence6,
            IPair<Action<In1, In2>> consequence7

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
        public static IAction<string, In1, In2> _<In1, In2>(
            IPair<Action<In1, In2>> consequence1,
            IPair<Action<In1, In2>> consequence2,
            IPair<Action<In1, In2>> consequence3,
            IPair<Action<In1, In2>> consequence4,
            IPair<Action<In1, In2>> consequence5,
            IPair<Action<In1, In2>> consequence6,
            IPair<Action<In1, In2>> consequence7,
            IPair<Action<In1, In2>> consequence8
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
        public static IAction<string, In1, In2> _<In1, In2>(
            IPair<Action<In1, In2>> consequence1,
            IPair<Action<In1, In2>> consequence2,
            Action<string, In1, In2> fallback
        ) => new ActionSwitch<In1, In2>(consequence1, consequence2, fallback);

        /// <summary>
        /// An action fork that is dependant on a named condition.
        /// </summary>
        public static IAction<string, In1, In2> _<In1, In2>(
            IPair<Action<In1, In2>> consequence1,
            IPair<Action<In1, In2>> consequence2,
            IPair<Action<In1, In2>> consequence3,
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
        public static IAction<string, In1, In2> _<In1, In2>(
            IPair<Action<In1, In2>> consequence1,
            IPair<Action<In1, In2>> consequence2,
            IPair<Action<In1, In2>> consequence3,
            IPair<Action<In1, In2>> consequence4,
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
        public static IAction<string, In1, In2> _<In1, In2>(
            IPair<Action<In1, In2>> consequence1,
            IPair<Action<In1, In2>> consequence2,
            IPair<Action<In1, In2>> consequence3,
            IPair<Action<In1, In2>> consequence4,
            IPair<Action<In1, In2>> consequence5,
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
        public static IAction<string, In1, In2> _<In1, In2>(
            IPair<Action<In1, In2>> consequence1,
            IPair<Action<In1, In2>> consequence2,
            IPair<Action<In1, In2>> consequence3,
            IPair<Action<In1, In2>> consequence4,
            IPair<Action<In1, In2>> consequence5,
            IPair<Action<In1, In2>> consequence6,
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
        public static IAction<string, In1, In2> _<In1, In2>(
            IPair<Action<In1, In2>> consequence1,
            IPair<Action<In1, In2>> consequence2,
            IPair<Action<In1, In2>> consequence3,
            IPair<Action<In1, In2>> consequence4,
            IPair<Action<In1, In2>> consequence5,
            IPair<Action<In1, In2>> consequence6,
            IPair<Action<In1, In2>> consequence7,
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
        public static IAction<string, In1, In2> _<In1, In2>(
            IPair<Action<In1, In2>> consequence1,
            IPair<Action<In1, In2>> consequence2,
            IPair<Action<In1, In2>> consequence3,
            IPair<Action<In1, In2>> consequence4,
            IPair<Action<In1, In2>> consequence5,
            IPair<Action<In1, In2>> consequence6,
            IPair<Action<In1, In2>> consequence7,
            IPair<Action<In1, In2>> consequence8,
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
        public static IAction<string, In1, In2> _<In1, In2>(params IPair<Action<In1, In2>>[] consequences) =>
            new ActionSwitch<In1, In2>(consequences);

        /// <summary>
        /// An action fork that is dependant on a named condition.
        /// </summary>
        public static IAction<string, In1, In2> _<In1, In2>(IEnumerable<IPair<Action<In1, In2>>> consequences) =>
            new ActionSwitch<In1, In2>(consequences);

        /// <summary>
        /// An action fork that is dependant on a named condition.
        /// </summary>
        public static IAction<string, In1, In2> _<In1, In2>(IEnumerable<IPair<Action<In1, In2>>> consequences, Action<string, In1, In2> fallback) =>
            new ActionSwitch<In1, In2>(consequences);
    }
}
