

using System;
using System.Collections.Generic;
using Xunit;
using Tonga.Enumerable;
using Tonga.Func;
using Tonga.Text;

#pragma warning disable MaxPublicMethodCount // a public methods count maximum
namespace Tonga.Scalar.Tests
{
    public sealed class AndTest
    {
        [Fact]
        public void AllTrue()
        {
            Assert.True(
            new And(
                new True(),
                new True(),
                new True()
            ).Value() == true);
        }

        [Fact]
        public void OneFalse()
        {
            Assert.True(
                new And(
                    new True(),
                    new False(),
                    new True()
                ).Value() == false
            );
        }

        [Fact]
        public void AllFalse()
        {
            Assert.False(
                new And(
                    Enumerable.EnumerableOf.Pipe(
                        new False(),
                        new False(),
                        new False()
                    )
                ).Value()
            );
        }

        [Fact]
        public void EmptyIterator()
        {
            Assert.True(
                new And(new None<IScalar<Boolean>>())
                    .Value()
            );
        }

        [Fact]
        public void EnumeratesList()
        {
            var list = new LinkedList<string>();
            Assert.True(
                new And<string>(
                        str => { list.AddLast(str); return true; },
                        Enumerable.EnumerableOf.Pipe("hello", "world")

                ).Value() == true);

            Assert.True(
                new Text.Joined(" ", list).AsString() == "hello world",
            "Can't iterate a list with a procedure");
        }

        [Fact]
        public void EnumeratesEmptyList()
        {
            var list = new LinkedList<string>();

            Assert.True(
                new And<string>(
                        str => { list.AddLast(str); return true; },
                        new None()
                ).Value() == true,
                "Can't enumerate a list"
                );

            Assert.True(list.Count == 0);
        }

        [Fact]
        public void TestFunc()
        {
            Assert.True(
                    new And<int>(
                        input => input > 0,
                        1, -1, 0
                    ).Value() == false);
        }

        [Fact]
        public void TestIFunc()
        {
            Assert.True(
                    new And<int>(
                        new FuncOf<int, bool>(input => input > 0),
                        1, 2, 3
                    ).Value());
        }

        [Theory]
        [InlineData("AB", false)]
        [InlineData("ABC", true)]
        public void TestValueAndFunctionList(string value, bool expected)
        {
            var and =
                new And<string>(
                    value,
                    str => str.Contains("A"),
                    str => str.Contains("B"),
                    str => str.Contains("C"));

            Assert.Equal(expected, and.Value());
        }

        [Fact]
        public void InputBoolValuesToTrue()
        {
            Assert.True(new And(true, true, true).Value());
        }

        [Fact]
        public void InputBoolValuesToFalse()
        {
            Assert.False(new And(new List<bool>() { true, false, true }).Value());
        }

        [Fact]
        public void InputBoolFunctionsToFalse()
        {
            Assert.False(new And(() => true, () => false).Value());
        }
    }
}
#pragma warning restore MaxPublicMethodCount // a public methods count maximum
