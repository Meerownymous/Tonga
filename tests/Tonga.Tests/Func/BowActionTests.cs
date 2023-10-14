

using System;
using System.Collections.Generic;
using Xunit;
using Tonga.Text;

namespace Tonga.Func.Tests
{
    public sealed class BowActionTests
    {
        [Fact]
        public void WaitsForTrigger()
        {
            var actions = new List<string>();
            var count = 0;
            new BowAction(
                () => { actions.Add("ask trigger"); return count++ > 0; },
                () => actions.Add("shoot")
            ).Invoke();

            Assert.Equal(
                "ask trigger, ask trigger, shoot",
                new Joined(", ", actions).AsString()
            );
        }

        [Fact]
        public void Prepares()
        {
            var actions = new List<string>();
            var count = 0;
            new BowAction(
                () => { actions.Add("ask trigger"); return count++ > 0; },
                () => actions.Add("prepare"),
                () => actions.Add("shoot")
            ).Invoke();

            Assert.Equal(
                "prepare, ask trigger, ask trigger, shoot",
                new Joined(", ", actions).AsString()
            );
        }

        [Fact]
        public void CancelsAfterTimeout()
        {
            Assert.Throws<ApplicationException>(
                () =>
                    new BowAction(
                        () => false,
                        () => { },
                        new TimeSpan(0, 0, 0, 0, 100)
                    ).Invoke()
            );
        }

        [Fact]
        public void RejectsOnException()
        {
            Assert.Throws<InvalidOperationException>(() =>
                new BowAction(
                    () => throw new InvalidOperationException("fail"),
                    () => { }
                ).Invoke()
            );
        }
    }
}
