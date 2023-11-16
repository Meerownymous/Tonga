using System;
namespace Tonga.Text
{
    /// <summary>
    /// Space.
    /// </summary>
    public sealed class Blank : TextEnvelope
    {
        /// <summary>
        /// Space.
        /// </summary>
        public Blank() : base(AsText._(" "))
        { }
    }
}

