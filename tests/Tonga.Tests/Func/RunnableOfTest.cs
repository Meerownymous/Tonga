

using Tonga.Func;
using Xunit;

namespace Tonga.Tests.Func
{
    public sealed class ActionOfTest
    {
        [Fact]
        public void ConvertsFuncIntoRunnable()
        {
            var i = 0;

            new ActionOf<int>(
                (input) =>
                {
                    i = input;
                }
            ).Invoke(1);

            Assert.True(i == 1,
                "cannot convert func to runnable");
        }
    }
}
