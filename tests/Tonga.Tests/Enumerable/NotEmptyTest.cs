

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
                LengthOf._(
                    NotEmpty._(
                        None._<bool>()
                    )
                ).Value()
            );
        }

        [Fact]
        public void NotEmptyEnumerableThrowsNoExeption()
        {
            Assert.True(
                LengthOf._(
                    NotEmpty._(
                        AsEnumerable._(false)
                    )
                ).Value() == 1
            );
        }

        [Fact]
        public void EmptyCollectionThrowsCustomExeption()
        {
            Assert.Throws<OperationCanceledException>(() =>
                LengthOf._(
                    NotEmpty._(
                        None._<object>(),
                        new OperationCanceledException()
                    )
                ).Value()
            );
        }
    }
}
