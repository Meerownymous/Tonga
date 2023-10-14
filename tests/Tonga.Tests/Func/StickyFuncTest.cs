

using System;
using System.Collections.Generic;
using Xunit;

namespace Tonga.Func.Tests
{
    public sealed class StickyFuncTest
    {
        [Fact]
        public void CachesFuncResults()
        {
            IFunc<Boolean, int> func =
                new StickyFunc<bool, int>(
                    input => new Random().Next()
            );

            Assert.True(
                func.Invoke(true) == func.Invoke(true),
                "cannot return function result from cache"
            );
        }

        [Fact]
        public void ReloadStickyFuncResults()
        {
            IFunc<Boolean, List<int>> func =
                new StickyFunc<bool, List<int>>(
                    input => new List<int>() { new Random().Next() },
                    lst => lst.Count > 1);

            var lst1 = func.Invoke(true);
            System.Threading.Thread.Sleep(2);

            Assert.True(lst1.GetHashCode() == func.Invoke(true).GetHashCode(), "cannot return value from cache");
            lst1.Add(42);

            Assert.False(lst1.GetHashCode() == func.Invoke(true).GetHashCode(), "reload doesn't work");
        }
    }
}
