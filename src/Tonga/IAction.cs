

namespace Tonga;

/// <summary>
/// Represents a function that you call without input and that has no output.
/// </summary>
public interface IAction
{
    /// <summary>
    /// Execute the action.
    /// </summary>
    void Invoke();
}

/// <summary>
/// A function with input, but no output.
/// </summary>
/// <typeparam name="In"></typeparam>
public interface IAction<in In>
{
    /// <summary>
    /// Execute the action.
    /// </summary>
    /// <param name="input">input argument</param>
    void Invoke(In input);
}

/// <summary>
/// A function with two inputs, but no output.
/// </summary>
/// <typeparam name="In1"></typeparam>
/// <typeparam name="In2"></typeparam>
public interface IAction<In1, In2>
{
    /// <summary>
    /// Execute the action.
    /// </summary>
    /// <param name="input1"></param>
    /// <param name="input2"></param>
    void Invoke(In1 input1, In2 input2);
}

/// <summary>
/// A function with three inputs, but no output.
/// </summary>
/// <typeparam name="In1"></typeparam>
/// <typeparam name="In2"></typeparam>
/// <typeparam name="In3"></typeparam>
public interface IAction<In1, In2, In3>
{
    /// <summary>
    /// Execute the proc.
    /// </summary>
    /// <param name="input1"></param>
    /// <param name="input2"></param>
    /// <param name="input3"></param>
    void Invoke(In1 input1, In2 input2, In3 input3);
}
