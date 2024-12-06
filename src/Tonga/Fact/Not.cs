namespace Tonga.Fact
{
    /// <summary>
    /// Logical negative.
    /// </summary>
    public sealed class Not : FactEnvelope
    {
        /// <summary>
        /// Logical negative.
        /// </summary>
        public Not(IFact fact) : base(new AsFact(fact.IsFalse))
        { }
    }
}
