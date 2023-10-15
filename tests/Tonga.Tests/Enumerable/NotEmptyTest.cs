

using System;
using Tonga.Scalar;
using Xunit;

namespace Tonga.Enumerable.Test
{
    public sealed class NotEmptyTest
    {
        [Fact]
        public void EmptyEnumerableThrowsExeption()
        {
            Assert.Throws<Exception>(() =>
                new LengthOf(
                    new NotEmpty<bool>(
                        new None<bool>()
                    )
                ).Value()
            );
        }

        [Fact]
        public void NotEmptyEnumerableThrowsNoExeption()
        {
            Assert.True(
                new LengthOf(
                    new NotEmpty<bool>(
                        EnumerableOf.Pipe(false)
                    )
                ).Value() == 1
            );
        }

        [Fact]
        public void EmptyCollectionThrowsCustomExeption()
        {
            Assert.Throws<OperationCanceledException>(() =>
                new LengthOf(
                    new NotEmpty<bool>(
                        new None<bool>(),
                        new OperationCanceledException()
                    )
                ).Value()
            );
        }
    }
}
