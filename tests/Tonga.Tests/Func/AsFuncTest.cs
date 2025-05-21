

using Tonga.Func;
using Xunit;

namespace Tonga.Tests.Func
{
    public sealed class AsFuncTest
    {
        [Fact]
        public void ConvertsSystemFuncIntoAtomsFunc()
        {
            Assert.True(
            new AsFunc<int>(
                () => 1
            ).Invoke() == 1,
            "cannot convert func into callable");
        }
    }
}
