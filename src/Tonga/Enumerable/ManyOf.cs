

using System;
using System.Collections;
using System.Collections.Generic;
using Tonga.Scalar;

namespace Tonga.Enumerable
{
    /// <summary>
    /// A <see cref="IEnumerable{T}"/> out of strings.
    /// </summary>

    public sealed class ManyOf : IEnumerable<string>
    {
        private readonly Func<IEnumerator<string>> origin;
        private readonly IEnumerable<string> items;

        /// <summary>
        /// A <see cref="IEnumerable{T}"/> out of an array.
        /// </summary>
        /// <param name="items"></param>
        public ManyOf(params string[] items) : this(() => new Params<string>(items))
        { }

        /// <summary>
        /// A <see cref="IEnumerable{T}"/> out of a <see cref="IEnumerator{T}"/>.
        /// </summary>
        /// <param name="e">a enumerator</param>
        public ManyOf(IEnumerator<string> e) : this(new Live<IEnumerator<string>>(e))
        { }

        /// <summary>
        /// A <see cref="IEnumerable{T}"/> out of a <see cref="IEnumerator{T}"/> returned by a <see cref="Func{T}"/>"/>.
        /// </summary>
        public ManyOf(IScalar<IEnumerator<string>> sc) : this(() => sc.Value())
        { }

        /// <summary>
        /// A <see cref="IEnumerable{T}"/> out of a <see cref="IEnumerator{T}"/> returned by a <see cref="Func{T}"/>"/>.
        /// </summary>
        /// <param name="fnc">function which retrieves enumerator</param>
        public ManyOf(Func<IEnumerable<string>> fnc) : this(() => fnc().GetEnumerator())
        { }

        /// <summary>
        /// A <see cref="IEnumerable{T}"/> out of a <see cref="IEnumerator{T}"/> encapsulated in a <see cref="IScalar{T}"/>"/>.
        /// </summary>
        /// <param name="origin">scalar to return the IEnumerator</param>
        public ManyOf(Func<IEnumerator<string>> origin, bool live = false)
        {
            this.origin = origin;
            this.items =
                Ternary.New(
                    LiveMany.New(Produced),
                    Sticky.New(this.Produced()),
                    live
                );
        }

        public IEnumerator<string> GetEnumerator() => this.items.GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator() => this.GetEnumerator();

        private IEnumerable<string> Produced()
        {
            var enumerator = this.origin();
            while(enumerator.MoveNext())
            {
                yield return enumerator.Current;
            }
        }

        /// <summary>
        /// A <see cref="IEnumerable{T}"/> out of an array.
        /// </summary>
        /// <param name="items"></param>
        public static IEnumerable<T> New<T>(params T[] items) => new ManyOf<T>(items);

        /// <summary>
        /// A <see cref="IEnumerable{T}"/> out of a <see cref="IEnumerator{T}"/>.
        /// </summary>
        /// <param name="e">a enumerator</param>
        public static IEnumerable<T> New<T>(IEnumerator<T> e) => new ManyOf<T>(e);

        /// <summary>
        /// A <see cref="IEnumerable{T}"/> out of a <see cref="IEnumerator{T}"/> returned by a <see cref="Func{T}"/>"/>.
        /// </summary>
        public static IEnumerable<T> New<T>(IScalar<IEnumerator<T>> sc) => new ManyOf<T>(sc);

        /// <summary>
        /// A <see cref="IEnumerable{T}"/> out of a <see cref="IEnumerator{T}"/> returned by a <see cref="Func{T}"/>"/>.
        /// </summary>
        /// <param name="fnc">function which retrieves enumerator</param>
        public static IEnumerable<T> New<T>(Func<IEnumerable<T>> fnc) => new ManyOf<T>(() => fnc().GetEnumerator());

        /// <summary>
        /// A <see cref="IEnumerable{T}"/> out of a <see cref="IEnumerator{T}"/> encapsulated in a <see cref="IScalar{T}"/>"/>.
        /// </summary>
        public static IEnumerable<T> New<T>(Func<IEnumerator<T>> origin) => new ManyOf<T>(origin);
    }

    /// <summary>
    /// A <see cref="IEnumerable{T}"/> out of other objects.
    /// </summary>
    /// <typeparam name="T"></typeparam>

    public sealed class ManyOf<T> : IEnumerable<T>
    {
        private readonly IEnumerable<T> result;
        private readonly Func<IEnumerable<T>> origin;

        /// <summary>
        /// A <see cref="IEnumerable{T}"/> out of an array.
        /// </summary>
        /// <param name="items"></param>
        public ManyOf(params T[] items) : this(
            new Params<T>(items)
        )
        { }

        /// <summary>
        /// A <see cref="IEnumerable{T}"/> out of a <see cref="IEnumerator{T}"/>.
        /// </summary>
        /// <param name="e">a enumerator</param>
        public ManyOf(IEnumerator<T> e) : this(new EnumeratorAsEnumerable<T>(e))
        { }

        /// <summary>
        /// A <see cref="IEnumerable{T}"/> out of a <see cref="IEnumerator{T}"/> returned by a <see cref="Func{T}"/>"/>.
        /// </summary>
        public ManyOf(IScalar<IEnumerator<T>> sc) : this(new EnumeratorAsEnumerable<T>(sc.Value()))
        { }

        /// <summary>
        /// A <see cref="IEnumerable{T}"/> out of a <see cref="IEnumerator{T}"/> returned by a <see cref="Func{T}"/>"/>.
        /// </summary>
        public ManyOf(Func<IEnumerator<T>> sc) : this(new EnumeratorAsEnumerable<T>(sc))
        { }

        /// <summary>
        /// A <see cref="IEnumerable{T}"/> out of a <see cref="IEnumerator{T}"/> encapsulated in a <see cref="IScalar{T}"/>"/>.
        /// </summary>
        /// <param name="origin">scalar to return the IEnumerator</param>
        public ManyOf(IEnumerable<T> origin, bool live = false) : this(() => origin, live)
        { }

        /// <summary>
        /// A <see cref="IEnumerable{T}"/> out of a <see cref="IEnumerator{T}"/> encapsulated in a <see cref="IScalar{T}"/>"/>.
        /// </summary>
        /// <param name="origin">scalar to return the IEnumerator</param>
        public ManyOf(Func<IEnumerable<T>> origin, bool live = false)
        {
            this.result =
                Ternary.New(
                    LiveMany.New(Produced),
                    Sticky.New(this.Produced()),
                    live
                );
            this.origin = origin;
        }

        public IEnumerator<T> GetEnumerator() => this.result.GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }

        private IEnumerable<T> Produced()
        {
            foreach (var item in this.origin())
            {
                yield return item;
            }
        }
    }
}
