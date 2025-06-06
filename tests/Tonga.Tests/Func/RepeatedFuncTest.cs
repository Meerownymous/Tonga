using System;
using Tonga.Func;
using Tonga.Scalar;
using Xunit;

namespace Tonga.Tests.Func
{
    public sealed class RepeatedFuncTest
    {
        [Fact]
        public void RunsFuncMultipleTimes()
        {
            var iter = Tonga.Enumerable.AsEnumerable._(1, 2, 5, 6).GetEnumerator();
            var func = new RepeatedFunc<bool, IScalar<int>>(
                input =>
                {
                    iter.MoveNext();
                    return AsScalar._(iter.Current);
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
            Assert.Throws<ArgumentException>((Func<object>)(() =>
                new RepeatedFunc<bool, IScalar<int>>(
                    (Func<bool, IScalar<int>>)(                    input =>
                    {
                        return
                            (IScalar<int>)AsScalar._(
                                new Random().Next()
                            );
                    }),
                    0
                ).Invoke(true))
            );
        }
    }
}
