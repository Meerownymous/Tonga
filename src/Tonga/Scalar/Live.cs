

using System;

namespace Tonga.Scalar
{
    /// <summary>
    /// A <see cref="IScalar{T}"/> out of other objects
    /// </summary>
    /// <typeparam name="T">type of the value</typeparam>
    public sealed class Live<T> : IScalar<T>
    {
        private readonly Func<T> func;

        /// <summary>
        /// A <see cref="IScalar{T}"/> out of an object.
        /// </summary>
        /// <param name="org"></param>
        public Live(T org) : this((b) => org)
        { }

        /// <summary>
        /// A <see cref="IScalar{T}"/> out of the return value from a <see cref="Func{T, TResult}"/>.
        /// </summary>
        /// <param name="func"></param>
        public Live(IFunc<T> func) : this(() => func.Invoke())
        { }

        /// <summary>
        /// A <see cref="IScalar{T}"/> out of the return value from a <see cref="Func{T, TResult}"/>.
        /// </summary>
        /// <param name="func"></param>
        public Live(IFunc<bool, T> func) : this(() => func.Invoke(true))
        { }

        /// <summary>
        /// A <see cref="IScalar{T}"/> out of the return value from an <see cref="IFunc{In, Out}"/>
        /// </summary>
        /// <param name="func"></param>
        public Live(Func<bool, T> func) : this(() => func.Invoke(true))
        { }

        /// <summary>
        /// Primary ctor
        /// </summary>
        /// <param name="func"></param>
        public Live(Func<T> func)
        {
            this.func = func;
        }

        /// <summary>
        /// Gives the value
        /// </summary>
        /// <returns></returns>
        public T Value()
        {
            return func.Invoke();
        }
    }

    public static class Live
    {
            /// <summary>
            /// A <see cref="IScalar{T}"/> out of an object.
            /// </summary>
            /// <param name="org"></param>
        public static IScalar<T> New<T>(T org)
            => new Live<T>(org);

        /// <summary>
        /// A <see cref="IScalar{T}"/> out of the return value from a <see cref="Func{T, TResult}"/>.
        /// </summary>
        /// <param name="func"></param>
        public static IScalar<T> New<T>(IFunc<T> func)
            => new Live<T>(func);

        /// <summary>
        /// A <see cref="IScalar{T}"/> out of the return value from a <see cref="Func{T, TResult}"/>.
        /// </summary>
        /// <param name="func"></param>
        public static IScalar<T> New<T>(IFunc<bool, T> func)
            => new Live<T>(func);

        /// <summary>
        /// A <see cref="IScalar{T}"/> out of the return value from an <see cref="IFunc{In, Out}"/>
        /// </summary>
        /// <param name="func"></param>
        public static IScalar<T> New<T>(Func<bool, T> func)
            => new Live<T>(func);

        /// <summary>
        /// Primary ctor
        /// </summary>
        /// <param name="func"></param>
        public static IScalar<T> New<T>(Func<T> func)
            => new Live<T>(func);
    }
}
