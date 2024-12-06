

using Tonga.Fact;
using Xunit;

namespace Tonga.Scalar.Tests
{
    public sealed class EqualsTest
    {
        [Fact]
        public void CompareEquals()
        {
            Assert.True(
                new Equals<int>(
                    () => 1,
                    () => 1
                ).IsTrue(),
                "Can't compare if two integers are equals");
        }

        [Fact]
        public void CompareNotEquals()
        {
            Assert.True(
                new Equals<int>(
                    () => 1,
                    () => 2
                ).IsFalse());
        }

        [Fact]
        public void CompareEqualsText()
        {
            var str = "hello";
            Assert.True(
            new Equals<string>(
                () => str,
                () => str
            ).IsTrue(),
            "Can't compare if two strings are equals");
        }

        [Fact]
        public void CompareNotEqualsText()
        {
            Assert.True(
            new Equals<string>(
                () => "world",
                () => "worle"
            ).IsFalse());
        }
    }
}
