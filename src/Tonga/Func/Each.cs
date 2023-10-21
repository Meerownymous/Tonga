

using System;
using System.Collections.Generic;
using Tonga.Enumerable;
using Tonga.Func;

namespace Tonga.Func
{
    /// <summary>
    /// Does to all elements in a <see cref="IEnumerable{T}"/>
    /// </summary>
    /// <typeparam name="In"></typeparam>
    public sealed class Each<In> : IAction
    {
        private readonly IAction<In> act;
        private readonly IEnumerable<In> enumerable;

        /// <summary>
        /// Executes the given Action for every element in the params.
        /// Replaces ForEach in LinQ. 
        /// <para>Object is <see cref="IAction"/></para>
        /// </summary>
        /// <param name="proc">the condition to apply</param>
        /// <param name="src">list of items</param>
        public Each(Action<In> proc, params In[] src) : this(new ActionOf<In>(proc.Invoke), src)
        { }

        /// <summary>
        ///  Executes the given Action for every element in the Enumerable.
        /// Replaces ForEach in LinQ.
        /// <para>Object is <see cref="IAction"/></para>
        /// </summary>
        /// <param name="proc">the condition to apply</param>
        /// <param name="src">list of items</param>
        public Each(Action<In> proc, IEnumerable<In> src) : this(new ActionOf<In>(proc.Invoke), src)
        { }

        /// <summary>
        ///  Executes the given IAction for every element in the params.
        /// Replaces ForEach in LinQ.
        /// <para>Object is <see cref="IAction"/></para>        /// </summary>
        /// <param name="proc">the condition to apply</param>
        /// <param name="src">list of items</param>
        public Each(IAction<In> proc, params In[] src) : this(
            proc, AsEnumerable._(src)
            )
        { }


        /// <summary>
        ///  Executes the given IAction for every element in the Enumerable.
        /// Replaces ForEach in LinQ.
        /// <para>Object is <see cref="IAction"/></para>
        /// </summary>
        /// <param name="action"></param>
        /// <param name="enumerable"></param>
        public Each(IAction<In> action, IEnumerable<In> enumerable)
        {
            act = action;
            this.enumerable = enumerable;
        }

        /// <summary>
        /// Execute Action for each element
        /// </summary>
        public void Invoke()
        {
            foreach (var item in enumerable)
            {
                act.Invoke(item);
            }
        }
    }

    public static class Each
    {
        /// <summary>
        /// Executes the given Action for every element in the params.
        /// Replaces ForEach in LinQ. 
        /// <para>Object is <see cref="IAction"/></para>
        /// </summary>
        /// <param name="act">the condition to apply</param>
        /// <param name="src">list of items</param>
        public static IAction _<In>(Action<In> act, params In[] src)
            => new Each<In>(act, src);

        /// <summary>
        ///  Executes the given Action for every element in the Enumerable.
        /// Replaces ForEach in LinQ.
        /// <para>Object is <see cref="IAction"/></para>
        /// </summary>
        /// <param name="act">the condition to apply</param>
        /// <param name="src">list of items</param>
        public static IAction _<In>(Action<In> act, IEnumerable<In> src)
            => new Each<In>(act, src);

        /// <summary>
        ///  Executes the given IAction for every element in the params.
        /// Replaces ForEach in LinQ.
        /// <para>Object is <see cref="IAction"/></para>        /// </summary>
        /// <param name="act">the condition to apply</param>
        /// <param name="src">list of items</param>
        public static IAction _<In>(IAction<In> act, params In[] src)
            => new Each<In>(act, src);


        /// <summary>
        ///  Executes the given IAction for every element in the Enumerable.
        /// Replaces ForEach in LinQ.
        /// <para>Object is <see cref="IAction"/></para>
        /// </summary>
        /// <param name="action"></param>
        /// <param name="enumerable"></param>
        public static IAction _<In>(IAction<In> action, IEnumerable<In> enumerable)
            => new Each<In>(action, enumerable);
    }
}
