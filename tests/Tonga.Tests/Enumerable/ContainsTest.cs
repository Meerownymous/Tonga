

using System;
using System.Diagnostics;
using Tonga.Scalar;
using Tonga.Tests;
using Xunit;

namespace Tonga.Enumerable.Test
{
    public class ContainsTest
    {
        [Fact]
        public void FindsItem()
        {
            Assert.True(
                new Contains<string>(
                    EnumerableOf.Pipe("Hello", "my", "cat", "is", "missing"),
                    (str) => str == "cat"
                    ).Value());
        }

        [Fact]
        public void DoesntFindItem()
        {
            Assert.False(
                new Contains<string>(
                    EnumerableOf.Pipe("Hello", "my", "cat", "is", "missing"),
                    (str) => str == "elephant"
                    ).Value());
        }

        [Fact]
        public void DoesntFindInEmtyList()
        {
            Assert.False(new Contains<string>(
                new None(),
                (str) => str == "elephant"
                ).Value());
        }
    }
}
