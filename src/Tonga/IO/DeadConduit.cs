namespace Tonga.IO
{
    /// <summary>
    /// Input with no data.
    /// </summary>
    public sealed class DeadConduit() : ConduitEnvelope(() => new DeadStream())
    { }
}
