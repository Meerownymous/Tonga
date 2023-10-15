

using System;
using System.Collections.Generic;
using System.Threading;
using Xunit;
using Tonga.Enumerable;
using Tonga.Scalar;

namespace Tonga.List.Tests
{
    public sealed class LiveListTest
    {
        [Fact]
        public void WorksWithMultipleValues()
        {
            var list = new LiveList<string>("one", "two");
            Assert.NotNull(
                list[1]
            );
        }

        [Fact]
        public void KnowsIfEmpty()
        {
            Assert.Empty(
                new LiveList<int>(() =>
                    new List<int>()
                )
            );
        }

        [Fact]
        public void LowBoundTest()
        {
            Assert.Throws<ArgumentOutOfRangeException>(() =>
                new LiveList<int>(() =>
                    new List<int>() { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 }
                )
                [-1]
            );
        }

        [Fact]
        public void HighBoundTest()
        {
            Assert.Throws<ArgumentOutOfRangeException>(() =>
                new LiveList<int>(() =>
                    new List<int>() { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 }
                )
                [11]
            );
        }

        [Fact]
        public void SensesChangesFromArray()
        {
            var volatileArray = new int[] { 1, 2 };
            var live =
                new LiveList<int>(
                    volatileArray
                );

            var a = new List<int>(live);
            volatileArray[0] = 3;
            var b = new List<int>(live);
            Assert.NotEqual(a, b);
        }

        [Fact]
        public void SensesChangesFromFunc()
        {
            int size = 2;
            var list =
                new LiveList<int>(() =>
                    new ListOf<int>(
                        new Head<int>(
                            new Endless<int>(size),
                            new Scalar.Live<int>(() =>
                                {
                                    return Interlocked.Increment(ref size);
                                }
                            )
                        )
                    )
                );

            Assert.NotEqual(list.Count, list.Count);
        }

        [Fact]
        public void SensesChangesFromEnumerator()
        {
            int size = 2;
            var list =
                new LiveList<int>(
                    new ListOf<int>(
                        new Head<int>(
                            new Endless<int>(1),
                            new Scalar.Live<int>(() =>
                                Interlocked.Increment(ref size)
                            )
                        )
                    ).GetEnumerator()
                );

            Assert.NotEqual(list.Count, list.Count);
        }
    }
}
