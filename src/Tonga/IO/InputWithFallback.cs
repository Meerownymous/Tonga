

using System;
using System.IO;
using Tonga.Func;

namespace Tonga.IO
{
    /// <summary>
    /// Input which returns an alternate value if it fails.
    /// </summary>
    public sealed class InputWithFallback : IInput, IDisposable
    {
        /// <summary>
        /// main input
        /// </summary>
        private readonly IInput _main;

        /// <summary>
        /// alternative input
        /// </summary>
        private readonly IFunc<IOException, IInput> _alternative;

        /// <summary>
        /// Input which returns an alternate value if it fails.
        /// </summary>
        /// <param name="input">the input</param>
        public InputWithFallback(IInput input) : this(input, new DeadInput())
        { }

        /// <summary>
        /// Input which returns an alternate value if it fails.
        /// </summary>
        /// <param name="input">the input</param>
        /// <param name="alt">a fallback input</param>
        public InputWithFallback(IInput input, IInput alt) : this(input, (error) => alt)
        { }

        ///// <summary>
        ///// Input which returns an alternate value from the given <see cref="IFunc{In, Out}"/> if it fails.
        ///// </summary>
        ///// <param name="input">the input</param>
        ///// <param name="alt">a fallback input</param>
        //public InputWithFallback(IInput input, IFunc<IOException, IInput> alt) : this(
        //    input, new IoCheckedFunc<IOException, IInput>(alt))
        //{ }

        /// <summary>
        /// Input which returns an alternate value from the given <see cref="Func{IOException, IInput}"/>if it fails.
        /// </summary>
        /// <param name="input">the input</param>
        /// <param name="alt">function to return alternative input</param>
        public InputWithFallback(IInput input, Func<IOException, IInput> alt) : this(input,
            new FuncOf<IOException, IInput>(alt))
        { }

        /// <summary>
        /// Input which returns an alternate value from the given <see cref="IFunc{IOException, IInput}"/>if it fails.
        /// </summary>
        /// <param name="input">the input</param>
        /// <param name="alt">an alternative input</param>
        public InputWithFallback(IInput input, IFunc<IOException, IInput> alt)
        {
            this._main = input;
            this._alternative = alt;
        }

        /// <summary>
        /// Get the stream.
        /// </summary>
        /// <returns>the stream</returns>
        public Stream Stream()
        {
            Stream stream;
            try
            {
                stream = this._main.Stream();
            }
            catch (IOException ex)
            {
                stream = this._alternative.Invoke(ex).Stream();
            }
            return stream;
        }

        /// <summary>
        /// Clean up.
        /// </summary>
        public void Dispose()
        {
            (_main as IDisposable)?.Dispose();
        }

    }
}
