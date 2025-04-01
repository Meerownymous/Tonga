

using Tonga.Func;
using Xunit;

namespace Tonga.Tests.Func
{
    public sealed class FuncOfTest
    {
        [Fact]
        public void ConvertsSystemFuncIntoAtomsFunc()
        {
            Assert.True(
            new FuncOf<int>(
                () => 1
            ).Invoke() == 1,
            "cannot convert func into callable");
        }
    }
}
