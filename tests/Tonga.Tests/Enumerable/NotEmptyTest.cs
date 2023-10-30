

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
                Length._(
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
                Length._(
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
                Length._(
                    NotEmpty._(
                        None._<object>(),
                        new OperationCanceledException()
                    )
                ).Value()
            );
        }
    }
}
