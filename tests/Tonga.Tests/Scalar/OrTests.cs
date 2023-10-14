

using System.Collections.Generic;
using Xunit;
using Tonga.Enumerable;
using Tonga.Func;

namespace Tonga.Scalar.Tests
{
    public sealed class OrTests
    {
        [Fact]
        public void TrueOrGenEnumerable()
        {
            Assert.True(
                new Or(
                    Params.Of(
                        new Live<bool>(true),
                        new Live<bool>(false)
                    )
                ).Value()
            );
        }

        [Fact]
        public void TrueOrGenFuncs()
        {
            Assert.True(
                new Or<bool>(
                    func => func,
                    new True().Value(),
                    new False().Value()
                    ).Value() == true
            );
        }

        [Fact]
        public void TrueOrGenScalar()
        {
            Assert.True(
                new Or(
                    new True(),
                    new False(),
                    new True()
                    ).Value() == true
            );
        }

        [Fact]
        public void WorksWithFuncAndParamItems()
        {
            Assert.True(
                    new Or<int>(
                        input => input > 0,
                        1, -1, 0
                    ).Value());
        }

        [Fact]
        public void WorksWithFuncAndListItems()
        {
            Assert.True(
                    new Or<int>(
                        input => input > 0,
                        new List<int>() { 1, -1, 0 }
                    ).Value());
        }

        [Fact]
        public void WorksWithIFunc()
        {
            Assert.False(
                    new Or<int>(
                        new FuncOf<int, bool>(input => input > 0),
                        -1, -2, -3
                    ).Value());
        }

        [Theory]
        [InlineData("DB", true)]
        [InlineData("ABC", true)]
        [InlineData("DEF", false)]
        public void WorksWithValueAndFunctions(string value, bool expected)
        {
            var or =
                new Or<string>(
                    value,
                    str => str.Contains("A"),
                    str => str.Contains("B"),
                    str => str.Contains("C"));

            Assert.Equal(expected, or.Value());
        }

        [Fact]
        public void InputBoolValuesToTrue()
        {
            Assert.True(new Or(false, true, false).Value());
        }

        [Fact]
        public void InputBoolValuesToFalse()
        {
            Assert.False(new Or(new List<bool>() { false, false, false }).Value());
        }

        [Fact]
        public void InputBoolFunctionsToTrue()
        {
            Assert.True(new Or(() => true, () => false).Value());
        }
    }
}
