

using System;
using System.IO;
using Tonga.IO;

namespace Tonga
{
    /// Output.
    ///
    /// <para>Here is for example how <see cref="IOutput"/> can be used
    /// together with <see cref="IInput"/> in order to modify the content
    /// of a text file:</para>
    ///
    /// <code> new LengthOfInput(
    ///   new TeeInput(
    ///     new InputOf(new StringAsText("Hello, world!")),
    ///     new OutputTo(new Uri("file:///C:/tmp/names.txt"))
    ///   )
    /// ).AsValue();</code>
    ///
    /// <para>Here <see cref="IO.OutputTo"/> implements {@link Output} and behaves like
    /// one, providing write-only access to the encapsulated
    /// <see cref="Uri"/>. The <see cref="TeeInput"/> copies the content of the
    /// input to the output. The <see cref="LengthOf"/>
    /// calculates the size of the copied data.</para>
    ///
    /// <para>There is no thread-safety guarantee.</para>
    ///
    public interface IOutput
    {
        /// <summary>
        /// Get the stream.
        /// </summary>
        /// <returns>the stream</returns>
        Stream Stream();
    }
}
