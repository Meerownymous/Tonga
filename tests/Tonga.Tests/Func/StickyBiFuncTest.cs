using System;
using Tonga.Func;
using Xunit;

namespace Tonga.Tests.Func
{
    public sealed class StickyBiFuncTest
    {
        [Fact]
        public void CachesFuncResults()
        {
            var func = new StickyBiFunc<bool, bool, Int32>(
                (_, _) => new Random().Next()
            );

            Assert.Equal(
                func.Invoke(true, true) + func.Invoke(true, true),
                func.Invoke(true, true) + func.Invoke(true, true)
            );
        }
    }
}
