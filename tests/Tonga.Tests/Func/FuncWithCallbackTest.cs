using System;
using Tonga.Func;
using Xunit;

namespace Tonga.Tests.Func
{
    public sealed class FuncWithCallbackTest
    {
        [Fact]
        public void UsesMainFunc()
        {
            Assert.True(
                new FuncWithFallback<bool, string>(
                    input => "It's success",
                    ex => "In case of failure..."
                ).Invoke(true).Contains("success"),
                "cannot use main function");
        }

        [Fact]
        public void UsesFallback()
        {
            Assert.True(
                new FuncWithFallback<bool, string>(
                    input =>
                    {
                        throw new Exception("Failure");
                    },
                    ex => "Never mind"
                ).Invoke(true) == "Never mind"
            );
        }

        [Fact]
        public void UsesFollowUp()
        {
            Assert.True(
            new FuncWithFallback<bool, string>(
                input => "works fine",
                ex => "won't happen",
                input => "follow up"
            ).Invoke(true) == "follow up");
        }

        [Fact]
        public void UsesParameterlessMainFunc()
        {
            Assert.True(
            new FuncWithFallback<string>(
                () => "It's success",
                ex => "In case of failure..."
            ).Invoke().Contains("success"),
            "cannot use main function");
        }

        [Fact]
        public void UsesParameterlessCallback()
        {
            Assert.True(
                new FuncWithFallback<string>(
                    () =>
                    {
                        throw new Exception("Failure");
                    },
                    ex => "Never mind"
                ).Invoke() == "Never mind"
            );
        }

        [Fact]
        public void ParameterlessUsesFollowUp()
        {
            Assert.True(
            new FuncWithFallback<string>(
                () => "works fine",
                ex => "won't happen",
                input => "follow up"
            ).Invoke() == "follow up");
        }
    }
}
