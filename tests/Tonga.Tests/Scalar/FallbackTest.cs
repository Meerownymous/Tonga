

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
                    new Live<string>(
                        () => throw new Exception("NO STRINGS ATTACHED HAHAHA")),
                    fbk
                    ).Value() == fbk);
        }

        [Fact]
        public void GivesFallbackByFunc()
        {
            var fbk = "strong string";

            Assert.True(
                new Fallback<string>(
                    new Live<string>(
                        () => throw new Exception("NO STRINGS ATTACHED HAHAHA")),
                    () => fbk
                    ).Value() == fbk);
        }

        [Fact]
        public void InjectsException()
        {
            var notAmused = new Exception("All is broken :(");

            Assert.True(
                new Fallback<string>(
                    new Live<string>(
                        () => throw notAmused),
                    (ex) => ex.Message).Value() == notAmused.Message);
        }
    }
}
