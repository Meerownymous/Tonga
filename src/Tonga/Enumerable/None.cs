

namespace Tonga.Enumerable
{
    /// <summary>
    /// Enumerable which is empty.
    /// </summary>
    public sealed class None : ManyEnvelope<string>
    {
        /// <summary>
        /// Enumerable which is empty.
        /// </summary>
        public None() : base(() => new ManyOf<string>(), true)
        { }
    }

    /// <summary>
    /// Enumerable which is empty.
    /// </summary>
    public sealed class None<T> : ManyEnvelope<T>
    {
        /// <summary>
        /// Enumerable which is empty.
        /// </summary>
        public None() : base(() => new ManyOf<T>(), true)
        { }
    }
}
