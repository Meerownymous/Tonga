

using System;
using Xunit;
using Tonga.Enumerable;
using Tonga.Scalar;

namespace Tonga.List.Tests
{
    public sealed class NotEmptyTest
    {
        [Fact]
        public void EmptyCollectionThrowsExeption()
        {
            Assert.Throws<Exception>(() =>
                new LengthOf(
                    new NotEmpty<bool>(
                        new AsList<bool>()
                    )).Value());
        }

        [Fact]
        public void NotEmptyCollectionThrowsNoExeption()
        {
            Assert.Equal(
                1,
                new LengthOf(
                    new NotEmpty<bool>(
                        new AsList<bool>(false)
                    )).Value()
            );
        }

        [Fact]
        public void EmptyCollectionThrowsCustomExeption()
        {
            Assert.Throws<OperationCanceledException>(() =>
                new LengthOf(
                    new NotEmpty<bool>(
                        new AsList<bool>(),
                        new OperationCanceledException()
                    )).Value());
        }
    }
}
