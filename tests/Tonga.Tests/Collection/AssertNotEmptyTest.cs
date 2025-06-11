using System;
using Tonga.Collection;
using Tonga.Enumerable;
using Xunit;

namespace Tonga.Tests.Collection
{
    public sealed class AssertNotEmptyTest
    {
        [Fact]
        public void EmptyCollectionThrowsExeption()
        {
            Assert.Throws<Exception>(() =>
                new None<bool>()
                    .AsCollection()
                    .AssertNotEmpty()
                    .GetEnumerator()
                    .MoveNext()
            );
        }

        [Fact]
        public void NotEmptyCollectionThrowsNoExeption()
        {
            Assert.True(
                false.AsSingle()
                    .AsCollection()
                    .AssertNotEmpty()
                    .GetEnumerator()
                    .MoveNext()
            );
        }

        [Fact]
        public void EmptyCollectionThrowsCustomExeption()
        {
            Assert.Throws<OperationCanceledException>(() =>
                new None<bool>()
                    .AsCollection()
                    .AssertNotEmpty(new OperationCanceledException())
                    .GetEnumerator()
                    .MoveNext()
            );
        }
    }
}
