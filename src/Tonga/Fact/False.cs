namespace Tonga.Fact;

/// <summary>
/// Logical false.
/// </summary>
public sealed class False() : FactEnvelope(new AsFact(() => false));
