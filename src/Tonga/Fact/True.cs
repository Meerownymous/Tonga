namespace Tonga.Fact;

/// <summary>
/// Logical truth.
/// </summary>
public sealed class True() : FactEnvelope(new AsFact(() => true));
