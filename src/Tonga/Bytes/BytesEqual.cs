

using Tonga.Scalar;

namespace Tonga.Bytes
{
    /// <summary>
    /// Equality for <see cref="IBytes"/>
    /// </summary>
    public sealed class BytesEqual : IScalar<bool>
    {
        private readonly IScalar<bool> equal;

        /// <summary>
        /// Makes a truth about <see cref="IBytes"/> are equal or not.
        /// </summary>
        /// <param name="left"></param>
        /// <param name="right"></param>
        public BytesEqual(IBytes left, IBytes right)
        {
            this.equal = new AsScalar<bool>(() =>
            {
                var leftByte = left.Bytes();
                var rightByte = right.Bytes();
                var equal = leftByte.Length == rightByte.Length;

                for (var i = 0; i < leftByte.Length && equal; i++)
                {
                    if (leftByte[i] != rightByte[i])
                    {
                        equal = false;
                        break;
                    }
                }
                return equal;
            });
        }

        /// <summary>
        /// Equal or not
        /// </summary>
        /// <returns></returns>
        public bool Value()
        {
            return this.equal.Value();
        }
    }
}
