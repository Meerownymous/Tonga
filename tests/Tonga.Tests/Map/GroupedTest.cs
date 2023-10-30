

using Xunit;
using Tonga.Func;
using Tonga.IO;
using Tonga.List;
using Tonga.Number;
using Tonga.Scalar;

namespace Tonga.Map.Tests
{
    public sealed class GroupedTest
    {
        [Fact]
        public void GroupsList()
        {
            var srcList = AsList._("ABC", "ABCD", "ABCDE");
            var keyFunc =
                new FuncOf<string, double>((str) =>
                    new NumberOf(
                        Length._(new AsInput(str)).Value()
                    ).AsDouble()
                );

            var valueFunc =
                new FuncOf<string, string>((str) =>
                    "ica" + str
                );
            Assert.Equal(
                "icaABCD",
                new Grouped<string, double, string>(srcList, keyFunc, valueFunc)[3.0][1]
            );
        }
    }
}
