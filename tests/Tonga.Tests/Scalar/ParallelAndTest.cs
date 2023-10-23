

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
                new ParallelAnd<bool>(
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
                new ParallelAnd<bool>
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
                new ParallelAnd<bool>
                (
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
                new ParallelAnd<bool>
                (
                    (IEnumerable<IScalar<bool>>)new None<IScalar<bool>>()
                ).Value();
            Assert.True(result);
        }

        [Fact]
        void IteratesList()
        {
            var list = new LinkedList<string>();
            Assert.True(
                new ParallelAnd<bool>(
                    Enumerable.Mapped._(
                        str => { list.AddLast(str); return (IScalar<bool>)new True(); },
                        AsEnumerable._("hello", "world")
                    )
                ).Value() &&
                list.Contains("hello") &&
                list.Contains("world")
            );
        }

        [Fact]
        void IteratesEmptyList()
        {
            var list = new LinkedList<string>();
            Assert.True(
                new ParallelAnd<bool>(
                    Enumerable.Mapped._(
                        str => { list.AddLast(str); return (IScalar<bool>)new True(); },
                        new None()
                    )
                ).Value() &&
                !list.Any()
            );
        }

        [Fact]
        void WorksWithFunc()
        {
            var result =
                new ParallelAnd<int>(
                    new FuncOf<int, bool>(i => i > 0),
                    1,
                    -1,
                    0
                ).Value();
            Assert.False(result);
        }

        [Fact]
        void WorksWithIterableScalarBool()
        {
            var result =
                new ParallelAnd<bool>(
                    AsList._(
                        AsEnumerable._(
                            new True(),
                            new True()
                        )
                    )
                ).Value();
            Assert.True(result);
        }

        [Fact]
        void WorksWithEmptyIterableScalarBool()
        {

            var result =
                new ParallelAnd<bool>(
                    new None<IScalar<bool>>()
                ).Value();

            Assert.True(result);
        }
    }
}
#pragma warning restore MaxPublicMethodCount // a public methods count maximum
