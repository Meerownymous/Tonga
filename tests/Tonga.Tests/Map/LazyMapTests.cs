

using System;
using System.Collections.Generic;
using Xunit;

namespace Tonga.Map.Tests
{
    public sealed class LazyMapTests
    {
        [Fact]
        public void AllValuesAreBuiltWhenPreventionIsDisabled()
        {
            var dict = new LazyMap<int, int>(false,
                new FkPair<int, int>(() => 1, () => throw new Exception("i shall not be called"), () => true)
            );

            var ex = Assert.Throws<Exception>(() => new List<KeyValuePair<int, int>>(dict));
            Assert.Equal("i shall not be called", ex.Message);
        }

        [Fact]
        public void ValuesAreNotBuiltWhenLazy()
        {
            var dict = new LazyMap<int, int>(true,
                new FkPair<int, int>(() => 1, () => throw new Exception("i shall not be called"), () => true)
            );

            var ex = Assert.Throws<InvalidOperationException>(() => dict.GetEnumerator());
            Assert.Equal("Cannot get the enumerator because this is a lazy dictionary. Enumerating the entries would build all values. If you need this behaviour, set the ctor param 'rejectBuildingAllValues' to false.", ex.Message);
        }

        [Fact]
        public void ValuesAreBuiltWhenRejectionEnabledButValuesNotLazy()
        {
            var dict = new LazyMap<int, int>(true,
               new FkPair<int, int>(() => 1, () => throw new Exception("i shall not be called"), () => false)
           );

            var ex = Assert.Throws<Exception>(() => new List<KeyValuePair<int, int>>(dict));
            Assert.Equal("i shall not be called", ex.Message);
        }

        [Fact]
        public void CanGetEnumeratorWhenEmpty()
        {
            var dict = new LazyMap<int, int>(true);
            dict.GetEnumerator();
        }
    }
}
