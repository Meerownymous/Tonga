using System;
using Tonga.Func;
using Xunit;

namespace Tonga.Tests.Func
{
    public sealed class ActionWithFallbackTest
    {
        [Fact]
        public void UsesParameterlessMainFunc()
        {
            int i = 0;
            new ActionWithFallback(
                    () => { i = 1; },
                    ex => { i = 2; }
                ).Invoke();

            Assert.True(
                i == 1,
                "Cannot use main action");
        }

        [Fact]
        public void UsesParameterlessFallbackFunc()
        {
            int i = 0;
            new ActionWithFallback(
                    () => { throw new Exception(); },
                    ex => { i = 2; }
                ).Invoke();

            Assert.True(
                i == 2,
                "Cannot use fallback action");
        }

        [Fact]
        public void UsesMainFunc()
        {
            int i = 0;
            new ActionWithFallback<int>(
                    (val) => { i = val; },
                    ex => { i = 2; }
                ).Invoke(1);

            Assert.True(
                i == 1,
                "Cannot use main action");
        }

        [Fact]
        public void UsesFallbackFunc()
        {
            int i = 0;
            new ActionWithFallback<int>(
                    (val) => { throw new Exception(); },
                    ex => { i = 2; }
                ).Invoke(1);

            Assert.True(
                i == 2,
                "Cannot use fallback action");
        }
    }
}
