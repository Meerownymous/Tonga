namespace Tonga.Tap;

/// <summary>
/// Envelope for taps.
/// </summary>
public abstract class TapEnvelope(System.Action act) : ITap
{
    public TapEnvelope(ITap tap) : this(tap.Trigger)
    { }

    public void Trigger() => act();
}
