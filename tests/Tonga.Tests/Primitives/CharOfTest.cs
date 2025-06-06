using System;
using Tonga.Scalar;
using Xunit;

namespace Tonga.Tests.Primitives
{
    public sealed class CharOfTest
    {
        [Fact]
        public void CharOfInteger()
        {
            Assert.True(
                new CharOf(1337).Value() == (char)1337);
        }

        [Fact]
        public void ChardOfIntegerOutsideOfRange()
        {
            Assert.Throws<OverflowException>(() => new CharOf(2147483647).Value());
        }

        [Fact]
        public void CharOfUInteger()
        {
            Assert.True(
                new CharOf((uint)1337).Value() == (char)1337);
        }

        [Fact]
        public void ChardOfUIntegerOutsideOfRange()
        {
            Assert.Throws<OverflowException>(() =>
                new CharOf((uint)2147483647).Value()
            );
        }

        [Fact]
        public void CharOfLong()
        {
            Assert.True(
                new CharOf((long)1338).Value() == (char)1338);
        }

        [Fact]
        public void ChardOfLongOutsideOfRange()
        {
            Assert.Throws<OverflowException>(() => new CharOf((long)2147483647).Value());
        }

        [Fact]
        public void CharOfULong()
        {
            Assert.True(
                new CharOf((ulong)1338).Value() == (char)1338);
        }

        [Fact]
        public void ChardOfULongOutsideOfRange()
        {
            Assert.Throws<OverflowException>(() => new CharOf((ulong)2147483647).Value());
        }

        [Fact]
        public void CharOfShort()
        {
            Assert.True(
                new CharOf((short)13).Value() == (char)13);
        }

        [Fact]
        public void CharOfUShort()
        {
            Assert.True(
                new CharOf((ushort)1338).Value() == (char)1338);
        }

        [Fact]
        public void CharOfDouble()
        {
            Assert.True(
                new CharOf(1338.0).Value() == (char)Convert.ToInt64(1338.0));
        }

        [Fact]
        public void ChardOfDoubleOutsideOfRange()
        {
            Assert.Throws<OverflowException>(() => new CharOf(2147483647.0).Value());
        }

        [Fact]
        public void CharOfFloat()
        {
            Assert.True(
                new CharOf((float)1338.0).Value() == (char)Convert.ToInt64((float)1338.0));
        }

        [Fact]
        public void ChardOfFloatOutsideOfRange()
        {
            Assert.Throws<OverflowException>(() => new CharOf((float)2147493647.0).Value());
        }

        [Fact]
        public void CharOfString()
        {
            Assert.True(
                new CharOf("b").Value() == 'b');
        }

        [Fact]
        public void CharOfByte()
        {
            Assert.True(
                new CharOf((byte)13).Value() == (char)13);
        }

        [Fact]
        public void CharOfSByte()
        {
            Assert.True(
                new CharOf((sbyte)13).Value() == (char)13);
        }
    }
}
