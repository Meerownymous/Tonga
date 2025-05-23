using System;
using Tonga.Func;
using Xunit;

namespace Tonga.Tests.Func
{
    public sealed class BiFuncWithFallbackTest
    {
        [Fact]
        public void UsesMainFunc()
        {
            Assert.True(
                new BiFuncWithFallback<bool, bool, string>(
                    (in1, in2) => "It's success",
                    ex => "In case of failure..."
                ).Invoke(true, true).Contains("success"),
                "cannot use main function");
        }

        [Fact]
        public void UsesFallback()
        {
            Assert.True(
                new BiFuncWithFallback<bool, bool, string>(
                    (in1, in2) =>
                    {
                        throw new Exception("Failure");
                    },
                    ex => "Never mind"
                ).Invoke(true, true) == "Never mind"
            );
        }

        [Fact]
        public void UsesFollowUp()
        {
            Assert.True(
            new BiFuncWithFallback<bool, bool, string>(
                (in1, in2) => "works fine",
                ex => "won't happen",
                input => "follow up"
            ).Invoke(true, true) == "follow up");
        }
    }
}
