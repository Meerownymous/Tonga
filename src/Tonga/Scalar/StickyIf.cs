using System;
using System.Collections.Generic;

namespace Tonga.Scalar
{
    /// <summary>
    /// Scalar which is sticky but refreshes if given condition is
    /// true.
    /// </summary>
    public sealed class StickyIf<T> : IScalar<T>
    {
        private readonly Func<T, bool> condition;
        private readonly IScalar<T> origin;
        private readonly List<T> memory;

        /// <summary>
        /// Scalar which is sticky but refreshes if given condition is
        /// true.
        /// </summary>
        public StickyIf(
            Func<T, bool> condition,
            IScalar<T> origin
        )
        {
            this.condition = condition;
            this.origin = origin;
            this.memory = new List<T>();
        }

        public T Value()
        {
            if (this.memory.Count == 1 && !this.condition(this.memory[0]))
                this.memory.Clear();

            if (this.memory.Count == 0)
                this.memory.Add(this.origin.Value());

            return this.memory[0];
        }
    }

    public static class StickyIf
    {
        public static StickyIf<T> _<T>(Func<T, bool> condition, IScalar<T> src) =>
            new StickyIf<T>(condition, src);
    }
}

