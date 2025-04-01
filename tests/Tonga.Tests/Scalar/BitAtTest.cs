using System;
using Tonga.Bytes;
using Tonga.Func;
using Tonga.Scalar;
using Xunit;

#pragma warning disable MaxPublicMethodCount // a public methods count maximum
namespace Tonga.Tests.Scalar
{
    public sealed class BitAtTests
    {
        [Fact]
        public void DeliversFirstElement()
        {
            Assert.True(
                new BitAt(new AsBytes(1)).Value()
            );
        }

        [Fact]
        public void DeliversElementAtPosition()
        {
            Assert.True(
                new BitAt(
                    new AsBytes(2),
                    1
                ).Value()
            );
        }

        [Fact]
        public void FailsWhenBytesEmpty()
        {
            Assert.Throws<ArgumentException>(() =>
                new BitAt(new AsBytes("")).Value()
            );
        }

        [Fact]
        public void FailsWithCustomExceptionOfFirstPosition()
        {
            Assert.Throws<ApplicationException>(() =>
                new BitAt(new AsBytes(""), new ApplicationException("Custom exception message")).Value()
            );
        }

        [Fact]
        public void DeliversFallback()
        {
            Assert.True(
                new BitAt(
                    new AsBytes(""),
                    2,
                    true
                ).Value()
            );
        }

        [Fact]
        public void DeliversFallbackOnFirstPosition()
        {
            Assert.True(
                new BitAt(
                    new AsBytes(""),
                    true
                ).Value()
            );
        }

        [Fact]
        public void ExecutesFallbackFunc()
        {
            Assert.True(
                new BitAt(
                    new AsBytes(""),
                    2,
                    bytes => true
                ).Value()
            );
        }

        [Fact]
        public void ExecutesFallbackIFunc()
        {
            Assert.True(
                new BitAt(
                    new AsBytes(""),
                    2,
                    new FuncOf<IBytes, bool>(bytes => true)
                ).Value()
            );
        }

        [Fact]
        public void ExecutesFallbackFuncOnFirstPosition()
        {
            Assert.True(
                new BitAt(
                    new AsBytes(""),
                    bytes => true
                ).Value()
            );
        }

        [Fact]
        public void UsesInjectedExcption()
        {
            Assert.Throws<ApplicationException>(() =>
                new BitAt(
                    new AsBytes(""),
                    1,
                    new ApplicationException()
                ).Value()
            );
        }
    }
}
