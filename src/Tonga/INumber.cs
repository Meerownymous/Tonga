

namespace Tonga;

/// <summary>
/// Representation of a number.
/// </summary>
public interface INumber
{
    /// <summary>
    /// The number represented as LONG
    /// </summary>
    /// <returns>the long</returns>
    long Long();

    /// <summary>
    /// The number represented as INTEGER
    /// </summary>
    /// <returns>the integer</returns>
    int Int();

    /// <summary>
    /// The number represented as DOUBLE
    /// </summary>
    /// <returns>the double</returns>
    double Double();

    /// <summary>
    /// The number represented as FLOAT
    /// </summary>
    /// <returns>the float</returns>
    float Float();
}
