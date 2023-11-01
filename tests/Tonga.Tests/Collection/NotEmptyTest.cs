

using System;
using Xunit;
using Tonga.Enumerable;
using Tonga.Scalar;

namespace Tonga.Collection.Tests
{
    public sealed class NotEmptyTest
    {
        [Fact]
        public void EmptyCollectionThrowsExeption()
        {
            Assert.Throws<Exception>(() =>
                Length._(
                    NotEmpty._(
                        Empty._<bool>()
                    )
                ).Value()
            );
        }

        [Fact]
        public void NotEmptyCollectionThrowsNoExeption()
        {
            Assert.Equal(
                1,
                Length._(
                    NotEmpty._(
                        AsCollection._(
                            AsEnumerable._(false)
                        )
                    )
                ).Value()
            );
        }

        [Fact]
        public void EmptyCollectionThrowsCustomExeption()
        {
            Assert.Throws<OperationCanceledException>(() =>
                Length._(
                    NotEmpty._(
                        Empty._<bool>(),
                        new OperationCanceledException()
                    )
                ).Value()
            );
        }
    }
}
