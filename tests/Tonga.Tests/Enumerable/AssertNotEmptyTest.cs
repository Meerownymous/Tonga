using System;
using Tonga.Enumerable;
using Xunit;

namespace Tonga.Tests.Enumerable
{
    public sealed class AssertNotEmptyTest
    {
        [Fact]
        public void EmptyEnumerableThrowsExeption()
        {
            Assert.Throws<Exception>(() =>
                new None<bool>()
                    .AssertNotEmpty()
                    .Length()
                    .Value()
            );
        }

        [Fact]
        public void NotEmptyEnumerableThrowsNoExeption()
        {
            Assert.Equal(
                1,
                false.AsSingle()
                    .AssertNotEmpty()
                    .Length()
                    .Value()
            );
        }

        [Fact]
        public void EmptyCollectionThrowsCustomExeption()
        {
            Assert.Throws<OperationCanceledException>(() =>
                new None<object>()
                    .AssertNotEmpty(new OperationCanceledException())
                    .Length()
                    .Value()
            );
        }
    }
}
