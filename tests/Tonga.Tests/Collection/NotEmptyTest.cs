

using System;
using Xunit;
using Tonga.Enumerable;

namespace Tonga.Collection.Tests
{
    public sealed class NotEmptyTest
    {
        [Fact]
        public void EmptyCollectionThrowsExeption()
        {
            Assert.Throws<Exception>(() =>
                new LengthOf(
                    new NotEmpty<bool>(
                        new LiveCollection<bool>()
                    )).Value());
        }

        [Fact]
        public void NotEmptyCollectionThrowsNoExeption()
        {
            Assert.Equal(
                1,
                new LengthOf(
                    new NotEmpty<bool>(
                        new LiveCollection<bool>(false)
                    )
                ).Value()
            );
        }

        [Fact]
        public void EmptyCollectionThrowsCustomExeption()
        {
            Assert.Throws<OperationCanceledException>(() =>
                new LengthOf(
                    new NotEmpty<bool>(
                        new LiveCollection<bool>(),
                        new OperationCanceledException()
                    )
                ).Value()
            );
        }
    }
}
