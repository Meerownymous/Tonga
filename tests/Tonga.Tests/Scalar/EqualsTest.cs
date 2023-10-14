

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
                ).Value() == true,
                "Can't compare if two integers are equals");
        }

        [Fact]
        public void CompareNotEquals()
        {
            Assert.True(
                new Equals<int>(
                    () => 1,
                    () => 2
                ).Value() == false);
        }

        [Fact]
        public void CompareEqualsText()
        {
            var str = "hello";
            Assert.True(
            new Equals<string>(
                () => str,
                () => str
            ).Value() == true,
            "Can't compare if two strings are equals");
        }

        [Fact]
        public void CompareNotEqualsText()
        {
            Assert.True(
            new Equals<string>(
                () => "world",
                () => "worle"
            ).Value() == false);
        }
    }
}
