

using System;
using System.Collections;
using Tonga.Scalar;

namespace Tonga.Enumerable
{
    /// <summary>
    /// Length of an <see cref="IEnumerable"/>.
    /// Important: You must understand that this object will iterate over the passed items 
    /// every time when you call .Value(). It is recommended to use a StickyScalar, 
    /// if you want to re-use its value.
    /// </summary>
    public sealed class LengthOf : ScalarEnvelope<Int32>
    {
        /// <summary>
        /// Length of an <see cref="IEnumerable"/>
        /// </summary>
        /// <param name="items">the enumerable</param>
        public LengthOf(IEnumerable items) : base(() =>
            new Enumerator.LengthOf(items.GetEnumerator()).Value()
        )
        { }
    }
}
