

using System.Collections.Generic;
using Xunit;
using Tonga.Enumerable;
using Tonga.Func;
using Tonga.Fact;

#pragma warning disable MaxPublicMethodCount // a public methods count maximum
namespace Tonga.Scalar.Tests
{
    public sealed class ParallelAndTest
    {

        [Fact]
        void AllTrue()
        {
            var result =
                ParallelAnd._(
                    new True(),
                    new True(),
                    new True()
                ).IsTrue();
            Assert.True(result);
        }

        [Fact]
        void OneFalse()
        {
            var result =
                ParallelAnd._
                (
                    new True(),
                    new False(),
                    new True()
                ).IsTrue();
            Assert.False(result);
        }

        [Fact]
        void AllFalse()
        {
            var result =
                ParallelAnd._(
                    new False(),
                    new False(),
                    new False()
                ).IsTrue();
            Assert.False(result);
        }

        [Fact]
        void EmtpyIterator()
        {
            var result =
                ParallelAnd._(
                    None._<IFact>()
                ).IsTrue();
            Assert.True(result);
        }

        [Fact]
        void IteratesList()
        {
            var list = new LinkedList<string>();
            Assert.True(
                ParallelAnd._(
                    Mapped._(
                        str =>
                        {
                            list.AddLast(str);
                            return new True();
                        },
                        AsEnumerable._("hello", "world")
                    )
                ).IsTrue()
                &&
                list.Contains("hello")
                &&
                list.Contains("world")
            );
        }

        [Fact]
        void IteratesEmptyList()
        {
            var list = new LinkedList<string>();
            Assert.True(
                ParallelAnd._(
                    Mapped._(
                        str => { list.AddLast(str); return new True(); },
                        None._<string>()
                    )
                ).IsTrue()
            );
        }

        [Fact]
        void WorksWithFunc()
        {
            Assert.False(
                ParallelAnd._(
                    new FuncOf<int, bool>(i => i > 0),
                    1,
                    -1,
                    0
                ).IsTrue()
            );
        }

        [Fact]
        void WorksWithIterableScalarBool()
        {
            Assert.True(
                ParallelAnd._(
                    AsEnumerable._(
                        new True(),
                        new True()
                    )
                ).IsTrue()
            );
        }

        [Fact]
        void WorksWithEmptyIterableScalarBool()
        {
            Assert.True(
                ParallelAnd._(
                    None._<IFact>()
                ).IsTrue()
            );
        }
    }
}
#pragma warning restore MaxPublicMethodCount // a public methods count maximum
