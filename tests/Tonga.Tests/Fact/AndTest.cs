using System.Collections.Generic;
using Tonga.Enumerable;
using Tonga.Fact;
using Xunit;

#pragma warning disable MaxPublicMethodCount // a public methods count maximum
namespace Tonga.Tests.Fact
{
    public sealed class AndTest
    {
        [Fact]
        public void AllTrue()
        {
            Assert.True(
                And._(
                    new True(),
                    new True(),
                    new True()
                ).IsTrue()
            );
        }

        [Fact]
        public void OneFalse()
        {
            Assert.True(
                new And(
                    new True(),
                    new False(),
                    new True()
                ).IsFalse()
            );
        }

        [Fact]
        public void AllFalse()
        {
            Assert.False(
                new And(
                    AsEnumerable._(
                        new False(),
                        new False(),
                        new False()
                    )
                ).IsTrue()
            );
        }

        [Fact]
        public void EmptyIterator()
        {
            Assert.True(
                new And(new None<IFact>())
                    .IsTrue()
            );
        }

        [Fact]
        public void EnumeratesList()
        {
            var list = new LinkedList<string>();
            Assert.True(
                new And<string>(
                        str => { list.AddLast(str); return true; },
                        AsEnumerable._("hello", "world")

                ).IsTrue()
            );

            Assert.True(
                new global::Tonga.Text.Joined(" ", list).AsString() == "hello world",
            "Can't iterate a list with a procedure");
        }

        [Fact]
        public void EnumeratesEmptyList()
        {
            var list = new LinkedList<string>();

            Assert.True(
                And._(
                    str => { list.AddLast(str); return true; },
                    None._<string>()
                ).IsTrue()
            );

            Assert.True(list.Count == 0);
        }

        [Fact]
        public void TestFunc()
        {
            Assert.False(
                And._(
                    input => input > 0,
                    1, -1, 0
                ).IsTrue()
            );
        }

        [Fact]
        public void TestIFunc()
        {
            Assert.True(
                new And<int>(
                    input => input > 0,
                    1, 2, 3
                ).IsTrue()
            );
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

            Assert.Equal(expected, and.IsTrue());
        }

        [Fact]
        public void InputBoolValuesToTrue()
        {
            Assert.True(new And(true, true, true).IsTrue());
        }

        [Fact]
        public void InputBoolValuesToFalse()
        {
            Assert.False(new And(new List<bool>() { true, false, true }).IsTrue());
        }

        [Fact]
        public void InputBoolFunctionsToFalse()
        {
            Assert.False(new And(() => true, () => false).IsTrue());
        }
    }
}
#pragma warning restore MaxPublicMethodCount // a public methods count maximum
