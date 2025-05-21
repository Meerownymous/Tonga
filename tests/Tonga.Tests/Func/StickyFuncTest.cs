using System;
using System.Collections.Generic;
using Tonga.Func;
using Xunit;
// ReSharper disable EqualExpressionComparison

namespace Tonga.Tests.Func
{
    public sealed class StickyFuncTest
    {
        [Fact]
        public void CachesFuncResults()
        {
            IFunc<Boolean, int> func =
                new StickyFunc<bool, int>(
                    _ => new Random().Next()
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
                    _ => [new Random().Next()],
                    lst => lst.Count > 1);

            var lst1 = func.Invoke(true);
            System.Threading.Thread.Sleep(2);

            Assert.True(lst1.GetHashCode() == func.Invoke(true).GetHashCode(), "cannot return value from cache");
            lst1.Add(42);

            Assert.False(lst1.GetHashCode() == func.Invoke(true).GetHashCode(), "reload doesn't work");
        }

        [Fact]
        public void CachesFuncResultsFromTwoInputs()
        {
            var func = new StickyFunc<bool, bool, Int32>(
                (_, _) => new Random().Next()
            );

            Assert.Equal(
                func.Invoke(true, true) + func.Invoke(true, true),
                func.Invoke(true, true) + func.Invoke(true, true)
            );
        }
    }
}
