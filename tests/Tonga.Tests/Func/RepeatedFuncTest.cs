

using System;
using Xunit;
using Tonga.Enumerable;
using Tonga.Scalar;

namespace Tonga.Func.Tests
{
    public sealed class RepeatedFuncTest
    {
        [Fact]
        public void RunsFuncMultipleTimes()
        {
            var iter = new ManyOf<int>(1, 2, 5, 6).GetEnumerator();
            var func = new RepeatedFunc<bool, IScalar<int>>(
                input =>
                {
                    iter.MoveNext();
                    return new Live<int>(iter.Current);
                },
                3
            );
            Assert.True(
                func.Invoke(true).Value() == 5);
        }

        [Fact]
        public void RepeatsNullsResults()
        {
            var func = new RepeatedFunc<bool, IScalar<int>>(
                input =>
                {
                    return null;
                },
                2
            );

            Assert.Null(func.Invoke(true));
        }

        [Fact]
        public void DoesntRepeatAny()
        {
            Assert.Throws<ArgumentException>(
            () => new RepeatedFunc<bool, IScalar<int>>(
                input =>
                {
                    return new Live<int>(
                            new Random().Next());
                },
                0
            ).Invoke(true));
        }
    }
}
