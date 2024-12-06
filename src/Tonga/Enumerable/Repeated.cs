

using System;
using System.Collections;
using System.Collections.Generic;
using Tonga.Scalar;

namespace Tonga.Enumerable
{
    /// <summary>
    /// <see cref="IEnumerable{T}"/> which repeats one element multiple times.
    /// </summary>
    /// <typeparam name="T">type of element to repeat</typeparam>
    public sealed class Repeated<T>(IScalar<T> elm, IScalar<int> cnt) : IEnumerable<T>
    {
        /// <summary>
        /// <see cref="IEnumerable{T}"/> which repeats one element multiple times.
        /// </summary>
        /// <param name="elm">function to get element to repeat</param>
        /// <param name="cnt">how often to repeat</param>
        public Repeated(Func<T> elm, int cnt) : this(
            AsScalar._(elm),
            cnt
        )
        { }

        /// <summary>
        /// <see cref="IEnumerable{T}"/> which repeats one element multiple times.
        /// </summary>
        /// <param name="elm">function to get element to repeat</param>
        /// <param name="cnt">how often to repeat</param>
        public Repeated(Func<T> elm, Func<int> cnt) : this(
            AsScalar._(elm),
            AsScalar._(cnt)
        )
        { }

        /// <summary>
        /// <see cref="IEnumerable{T}"/> which repeats one element multiple times.
        /// </summary>
        /// <param name="elm">element to repeat</param>
        /// <param name="cnt">how often to repeat</param>
        public Repeated(T elm, int cnt) :
            this(AsScalar._(elm), cnt)
        { }

        /// <summary>
        /// ctor
        /// </summary>
        /// <param name="elm">scalar to get element to repeat</param>
        /// <param name="cnt">how often to repeat</param>
        public Repeated(IScalar<T> elm, int cnt) : this(
            elm, AsScalar._(cnt)
        )
        { }

        public IEnumerator<T> GetEnumerator()
        {
            var times = cnt.Value();
            for (int i = 0; i < times; i++)
            {
                yield return elm.Value();
            }
        }

        IEnumerator IEnumerable.GetEnumerator() => this.GetEnumerator();
    }

    /// <summary>
    /// <see cref="IEnumerable{T}"/> which repeats one element multiple times.
    /// </summary>
    public static class Repeated
    {
        /// <summary>
        /// <see cref="IEnumerable{T}"/> which repeats one element multiple times.
        /// </summary>
        /// <param name="elm">function to get element to repeat</param>
        /// <param name="cnt">how often to repeat</param>
        public static IEnumerable<T> _<T>(Func<T> elm, int cnt) => new Repeated<T>(elm, cnt);

        /// <summary>
        /// <see cref="IEnumerable{T}"/> which repeats one element multiple times.
        /// </summary>
        /// <param name="elm">function to get element to repeat</param>
        /// <param name="cnt">how often to repeat</param>
        public static IEnumerable<T> _<T>(Func<T> elm, Func<int> cnt) => new Repeated<T>(elm, cnt);

        /// <summary>
        /// <see cref="IEnumerable{T}"/> which repeats one element multiple times.
        /// </summary>
        /// <param name="elm">element to repeat</param>
        /// <param name="cnt">how often to repeat</param>
        public static IEnumerable<T> _<T>(T elm, int cnt) => new Repeated<T>(elm, cnt);

        /// <summary>
        /// ctor
        /// </summary>
        /// <param name="elm">scalar to get element to repeat</param>
        /// <param name="cnt">how often to repeat</param>
        public static IEnumerable<T> _<T>(IScalar<T> elm, int cnt) => new Repeated<T>(elm, cnt);

        /// <summary>
        /// ctor
        /// </summary>
        /// <param name="elm">scalar to get element to repeat</param>
        /// <param name="cnt">how often to repeat</param>
        public static IEnumerable<T> _<T>(IScalar<T> elm, IScalar<int> cnt) => new Repeated<T>(elm, cnt);
    }

    public static class RepeatedSmarts
    {
        /// <summary>
        /// <see cref="IEnumerable{T}"/> which repeats one element multiple times.
        /// </summary>
        /// <param name="elm">function to get element to repeat</param>
        /// <param name="cnt">how often to repeat</param>
        public static IEnumerable<T> Repeated<T>(this Func<T> elm, int cnt) => new Repeated<T>(elm, cnt);

        /// <summary>
        /// <see cref="IEnumerable{T}"/> which repeats one element multiple times.
        /// </summary>
        /// <param name="elm">function to get element to repeat</param>
        /// <param name="cnt">how often to repeat</param>
        public static IEnumerable<T> Repeated<T>(this Func<T> elm, Func<int> cnt) => new Repeated<T>(elm, cnt);

        /// <summary>
        /// <see cref="IEnumerable{T}"/> which repeats one element multiple times.
        /// </summary>
        /// <param name="elm">element to repeat</param>
        /// <param name="cnt">how often to repeat</param>
        public static IEnumerable<T> Repeated<T>(this T elm, int cnt) => new Repeated<T>(elm, cnt);

        /// <summary>
        /// ctor
        /// </summary>
        /// <param name="elm">scalar to get element to repeat</param>
        /// <param name="cnt">how often to repeat</param>
        public static IEnumerable<T> Repeated<T>(this IScalar<T> elm, int cnt) => new Repeated<T>(elm, cnt);

        /// <summary>
        /// ctor
        /// </summary>
        /// <param name="elm">scalar to get element to repeat</param>
        /// <param name="cnt">how often to repeat</param>
        public static IEnumerable<T> Repeated<T>(this IScalar<T> elm, IScalar<int> cnt) => new Repeated<T>(elm, cnt);
    }
}

