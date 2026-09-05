using System;
using Tonga.Fact;
using Tonga.Scalar;
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

        [Fact]
        public void ComparesValuesAsSmart()
        {
            Assert.True(
                1.IsEqual(1).IsTrue()
            );
        }

        [Fact]
        public void SeesDifferenceAsSmart()
        {
            Assert.True(
                "world".IsEqual("worle").IsFalse()
            );
        }

        [Fact]
        public void ComparesFunctionsAsSmart()
        {
            Assert.True(
                ((Func<int>)(() => 1)).IsEqual(() => 1).IsTrue()
            );
        }

        [Fact]
        public void ComparesScalarsAsSmart()
        {
            Assert.True(
                "hello".AsScalar().IsEqual("hello".AsScalar()).IsTrue()
            );
        }
    }
}
