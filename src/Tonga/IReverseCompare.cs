

using System.Collections.Generic;

#pragma warning disable NoStatics // No Statics
#pragma warning disable VariablesArePrivate // Fields are private
#pragma warning disable CS1591

namespace Tonga.Misc
{
    /// <summary>
    /// <see cref="Comparer{T}"/> that can compare reverse to help reversing lists.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public sealed class IReverseCompare<T> : Comparer<T>
    {
        public static new readonly IReverseCompare<T> Default = new IReverseCompare<T>(Comparer<T>.Default);


        public static IReverseCompare<T> Reverse(Comparer<T> comparer)
        {
            return new IReverseCompare<T>(comparer);
        }

        private readonly Comparer<T> comparer = Default;

        private IReverseCompare(Comparer<T> comparer)
        {
            this.comparer = comparer;
        }

        public override int Compare(T x, T y)
        {
            return comparer.Compare(y, x);
        }
    }
}
#pragma warning restore NoStatics // No Statics
#pragma warning restore VariablesArePrivate // Fields are private
