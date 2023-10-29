

using System.Collections.Generic;
using System.Linq;
using Xunit;
using Tonga.Enumerable;
using Tonga.Func;
using Tonga.List;
using System;

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
                ).Value();
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
                ).Value();
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
                ).Value();
            Assert.False(result);
        }

        [Fact]
        void EmtpyIterator()
        {
            var result =
                ParallelAnd._(
                    None._<IScalar<bool>>()
                ).Value();
            Assert.True(result);
        }

        [Fact]
        void IteratesList()
        {
            var list = new LinkedList<string>();
            Assert.True(
                ParallelAnd._(
                    Enumerable.Mapped._(
                        str =>
                        {
                            list.AddLast(str);
                            return new True();
                        },
                        AsEnumerable._("hello", "world")
                    )
                ).Value()
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
                    Enumerable.Mapped._(
                        str => { list.AddLast(str); return (IScalar<bool>)new True(); },
                        None._<string>()
                    )
                ).Value()
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
                ).Value()
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
                ).Value()
            );
        }

        [Fact]
        void WorksWithEmptyIterableScalarBool()
        {
            Assert.True(
                ParallelAnd._(
                    None._<IScalar<bool>>()
                ).Value()
            );
        }
    }
}
#pragma warning restore MaxPublicMethodCount // a public methods count maximum
