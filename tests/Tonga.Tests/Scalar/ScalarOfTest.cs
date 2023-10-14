

using System;
using System.Collections.Generic;
using System.Diagnostics;
using Xunit;

namespace Tonga.Scalar.Tests
{
    public sealed class ScalarOfTest
    {
        [Fact]
        public void CachesScalarResults()
        {
            IScalar<int> scalar =
                new ScalarOf<int>(
                    () => new Random().Next());

            var val1 = scalar.Value();
            System.Threading.Thread.Sleep(2);

            Assert.True(val1 == scalar.Value(),
                "cannot return value from cache"
            );
        }

        [Fact]
        public void ReloadCachedScalarResults()
        {
            IScalar<List<int>> scalar =
                new ScalarOf<List<int>>(
                    () => new List<int>() { new Random().Next() },
                    lst => lst.Count > 1);

            var lst1 = scalar.Value();
            System.Threading.Thread.Sleep(2);

            Assert.True(lst1.GetHashCode() == scalar.Value().GetHashCode(), "cannot return value from cache");
            lst1.Add(42);

            Assert.False(lst1.GetHashCode() == scalar.Value().GetHashCode(), "reload doesn't work");
        }
    }
}
