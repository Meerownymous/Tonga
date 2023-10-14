

namespace Tonga
{
    /// <summary>
    /// Represents a function that you call without an argument and which returns something.
    /// </summary>
    /// <typeparam name="Out"></typeparam>
    public interface IFunc<Out>
    {
        /// <summary>
        /// Call the function and retrieve the output.
        /// </summary>
        /// <returns></returns>
        Out Invoke();
    }

    /// <summary>
    /// A function that has one input and an output.
    /// </summary>
    /// <typeparam name="In"></typeparam>
    /// <typeparam name="Out"></typeparam>
    public interface IFunc<In, Out>
    {
        /// <summary>
        /// Apply it
        /// </summary>
        /// <param name="input">the input</param>
        /// <returns>the output</returns>
        Out Invoke(In input);
    }

    /// <summary>
    /// A function that has one input and an output.
    /// </summary>
    public interface IFunc<In1, In2, Out>
    {
        /// <summary>
        /// Apply it
        /// </summary>
        /// <param name="input1">the input 1</param>
        /// <param name="input2">the input 2</param>
        /// <returns>the output</returns>
        Out Invoke(In1 input1, In2 input2);
    }

    /// <summary>
    /// A function that has one input and an output.
    /// </summary>
    public interface IFunc<In1, In2, In3, Out>
    {
        /// <summary>
        /// Apply it
        /// </summary>
        /// <param name="input1">the input 1</param>
        /// <param name="input2">the input 2</param>
        /// <param name="input3">the input 3</param>
        /// <returns>the output</returns>
        Out Invoke(In1 input1, In2 input2, In3 input3);
    }
}
