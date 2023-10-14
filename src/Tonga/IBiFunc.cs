

namespace Tonga
{
    /// <summary>
    /// Function that accepts two arguments
    /// </summary>
    /// <typeparam name="X">Type of Input</typeparam>
    /// <typeparam name="Y">Type of Input</typeparam>
    /// <typeparam name="Z">Type of Output</typeparam>
    public interface IBiFunc<X, Y, Z>
    {
        /// <summary>
        /// Apply it
        /// </summary>
        /// <param name="first"></param>
        /// <param name="second"></param>
        /// <returns>Output</returns>
        Z Invoke(X first, Y second);
    }
}
