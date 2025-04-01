using System.IO;
using Tonga.Func;
using Xunit;

namespace Tonga.Tests.Func
{
    public class NoNullsCallableTest
    {
        [Fact]
        public void RaisesError()
        {
            Assert.Throws<IOException>(
                () =>
                new NoNullsFunc<string>(
                    () => null
                ).Invoke());
        }

        [Fact]
        public void RaisesGivenError()
        {
            Assert.Throws<IOException>(() =>
           new NoNullsFunc<string>(
               () => null,
               new IOException("got NULL")
           ).Invoke());
        }

        [Fact]
        public void ReturnsFallback()
        {
            var fbk = "wtf! its null!";

            Assert.True(
                new NoNullsFunc<string>(
                    () => null,
                    fbk
                ).Invoke() == fbk);
        }
    }
}
