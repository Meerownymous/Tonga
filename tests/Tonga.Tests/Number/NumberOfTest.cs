

using System;
using Xunit;
using Tonga.Text;

namespace Tonga.Number.Tests
{
    public sealed class NumberOfTest
    {
        [Fact]
        public void ParsesText()
        {
            Assert.Equal(
                4673.453,
                new NumberOf(new TextOf("4673.453")).AsDouble()
            );
        }

        [Fact]
        public void ParsesFloat()
        {
            Assert.True(
                new NumberOf(4673.453F).AsFloat() == 4673.453F
            );
        }

        [Fact]
        public void RejectsNoFloatText()
        {
            Assert.Throws<ArgumentException>(() =>
                new NumberOf("ghki").AsFloat()
            );
        }

        [Fact]
        public void ParsesInt()
        {
            Assert.True(
                new NumberOf(1337).AsInt() == 1337
            );
        }

        [Fact]
        public void RejectsNoIntText()
        {
            Assert.Throws<ArgumentException>(() =>
                new NumberOf("ghki").AsInt()
            );
        }

        [Fact]
        public void ParsesDouble()
        {
            Assert.True(
                new NumberOf(843.23969274001D).AsDouble() == 843.23969274001D
            );
        }

        [Fact]
        public void RejectsNoDoubleText()
        {
            Assert.Throws<ArgumentException>(() =>
                new NumberOf("ghki").AsDouble()
            );
        }

        [Fact]
        public void ParsesLong()
        {
            Assert.True(
                new NumberOf(139807814253711).AsLong() == 139807814253711L
            );
        }

        [Fact]
        public void RejectsNoLongText()
        {
            Assert.Throws<ArgumentException>(() =>
                new NumberOf("ghki").AsLong()
            );
        }

        [Fact]
        public void IntToDouble()
        {
            Assert.True(
                new NumberOf(
                    5
                ).AsDouble() == 5d
            );
        }

        [Fact]
        public void DoubleToFloat()
        {
            Assert.True(
                new NumberOf(
                    (551515155.451d)
                ).AsFloat() == 551515155.451f
            );
        }

        [Fact]
        public void FloatAsDouble()
        {
            Assert.True(
                new NumberOf(
                    (5.243)
                ).AsDouble() == 5.243d
            );
        }

        [Fact]
        public void LongAsInt()
        {
            Assert.True(
                new NumberOf(
                    (50L)
                ).AsInt() == 50
            );
        }

        [Fact]
        public void IntAsLong()
        {
            Assert.True(
                new NumberOf(
                    (50)
                ).AsLong() == 50L
            );
        }

        [Fact]
        public void DoubleSeperator()
        {
            Assert.True(
                new NumberOf(
                    "10.100,11",
                    ",",
                    "."
                ).AsDouble() == 10100.11
            );
        }

        [Fact]
        public void StringAsInt()
        {
            Assert.True(
                new NumberOf(
                    "100"
                ).AsInt() == 100
            );
        }
    }
}
