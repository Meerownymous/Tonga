using Tonga.Fact;
using Xunit;

namespace Tonga.Tests.Fact
{
    public sealed class IsEqualTest
    {
        [Fact]
        public void CompareEquals()
        {
            Assert.True(
                new IsEqual<int>(
                    () => 1,
                    () => 1
                ).IsTrue(),
                "Can't compare if two integers are equals");
        }

        [Fact]
        public void CompareNotEquals()
        {
            Assert.True(
                new IsEqual<int>(
                    () => 1,
                    () => 2
                ).IsFalse());
        }

        [Fact]
        public void CompareEqualsText()
        {
            var str = "hello";
            Assert.True(
            new IsEqual<string>(
                () => str,
                () => str
            ).IsTrue(),
            "Can't compare if two strings are equals");
        }

        [Fact]
        public void CompareNotEqualsText()
        {
            Assert.True(
            new IsEqual<string>(
                () => "world",
                () => "worle"
            ).IsFalse());
        }
    }
}
