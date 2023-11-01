using System;
using System.Collections.Generic;
using Tonga.Collection;
using Tonga.Enumerable;

namespace Tonga.Collection
{
    /// <summary>
    /// Collection which is empty.
    /// </summary>
    public sealed class Empty<T> : CollectionEnvelope<T>
    {
        /// <summary>
        /// Collection which is empty.
        /// </summary>
        public Empty() : base(
            AsCollection._(
                None._<T>()
            )
        )
        { }
    }

    /// <summary>
    /// Collection which is empty.
    /// </summary>
    public static class Empty
    {
        /// <summary>
        /// Collection which is empty.
        /// </summary>
        public static Empty<T> _<T>() => new Empty<T>();
    }
}

