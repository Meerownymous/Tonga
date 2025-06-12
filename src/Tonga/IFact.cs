using System;

namespace Tonga;

/// <summary>
/// A fact that can be checked.
/// </summary>
public interface IFact
{
    /// <summary>
    /// Is fact true?
    /// </summary>
    /// <returns></returns>
    bool IsTrue();

    /// <summary>
    /// Is fact true?
    /// </summary>
    /// <returns></returns>
    bool IsFalse();
}
