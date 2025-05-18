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

    /// <summary>
    /// Action to take if fact is checked and true.
    /// </summary>
    IFact IfTrue(Action then);

    /// <summary>
    /// Action to take if fact is checked and false.
    /// </summary>
    IFact IfFalse(Action then);

    /// <summary>
    /// Check the fact and invoke the following action.
    /// </summary>
    void Check();
}

/// <summary>
/// A fact that can be checked.
/// </summary>
public interface IFact<About>
{
    /// <summary>
    /// Is fact true?
    /// </summary>
    /// <returns></returns>
    bool IsTrue(About item);

    /// <summary>
    /// Is fact true?
    /// </summary>
    /// <returns></returns>
    bool IsFalse(About item);

    /// <summary>
    /// Action to take if fact is checked and true.
    /// </summary>
    IFact IfTrue(About item, Action then);

    /// <summary>
    /// Action to take if fact is checked and false.
    /// </summary>
    IFact IfFalse(About item, Action then);

    /// <summary>
    /// Check the fact and invoke the following action.
    /// </summary>
    void Check(About item);
}
