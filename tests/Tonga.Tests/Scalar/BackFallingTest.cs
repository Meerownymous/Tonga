using System;
using Tonga.Scalar;
using Xunit;

namespace Tonga.Tests.Scalar
{
    public class BackFallingTest
    {
        [Fact]
        public void GivesFallback()
        {
            var fbk = "strong string";

            Assert.True(
                BackFalling<>._(
                    AsScalar._<string>(
                        () => throw new Exception("NO STRINGS ATTACHED HAHAHA")
                    ),
                    fbk
                ).Value() == fbk
            );
        }

        [Fact]
        public void GivesFallbackByFunc()
        {
            var fbk = "strong string";

            Assert.True(
                BackFalling<>._(
                    AsScalar._<string>(
                        () => throw new Exception("NO STRINGS ATTACHED HAHAHA")),
                    () => fbk
                    ).Value() == fbk);
        }

        [Fact]
        public void InjectsException()
        {
            var notAmused = new Exception("All is broken :(");

            Assert.Equal(
                notAmused.Message,
                BackFalling<>._(
                    AsScalar._<string>(() => throw notAmused),
                    (ex) => ex.Message
                ).Value()
            );
        }
    }
}
