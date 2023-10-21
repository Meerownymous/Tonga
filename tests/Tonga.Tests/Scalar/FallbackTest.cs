

using System;
using Xunit;

namespace Tonga.Scalar.Tests
{
    public class FallbackTest
    {
        [Fact]
        public void GivesFallback()
        {
            var fbk = "strong string";

            Assert.True(
                new Fallback<string>(
                    AsScalar._<string>(
                        () => throw new Exception("NO STRINGS ATTACHED HAHAHA")
                    ),
                    fbk
                ).Value() == fbk);
        }

        [Fact]
        public void GivesFallbackByFunc()
        {
            var fbk = "strong string";

            Assert.True(
                new Fallback<string>(
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
                new Fallback<string>(
                    AsScalar._<string>(() => throw notAmused),
                    (ex) => ex.Message
                ).Value()
            );
        }
    }
}
