namespace Tonga.Tap;

/// <summary>
/// Tap that can be triggered.
/// </summary>
public sealed class AsTap(System.Action act) : ITap
{
    /// <summary>
    /// Tap that can be triggered.
    /// </summary>
    public void Trigger() => act();
}

public static partial class TapSmarts
{
    /// <summary>
    /// Tap that can be triggered.
    /// </summary>
    public static ITap AsTap(this System.Action act) =>
        new AsTap(act);
}
