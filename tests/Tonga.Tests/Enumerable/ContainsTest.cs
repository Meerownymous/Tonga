

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
                Contains._(
                    AsEnumerable._("Hello", "my", "cat", "is", "missing"),
                    (str) => str == "cat"
                ).Value()
            );
        }

        [Fact]
        public void DoesntFindItem()
        {
            Assert.False(
                Contains._(
                    AsEnumerable._("Hello", "my", "cat", "is", "missing"),
                    (str) => str == "elephant"
                ).Value()
            );
        }

        [Fact]
        public void DoesntFindInEmtyList()
        {
            Assert.False(
                Contains._(
                    None._<string>(),
                    (str) => str == "elephant"
                ).Value()
            );
        }
    }
}
