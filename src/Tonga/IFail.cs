

namespace Tonga
{
    /// <summary>
    /// Something that can fail when calling go.
    /// </summary>
    public interface IFail
    {
        /// <summary>
        /// Fail if necessary.
        /// </summary>
        void Go();
    }
}
