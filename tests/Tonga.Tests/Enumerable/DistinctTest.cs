

using System.Collections.Generic;
using Xunit;
using Tonga.Number;

namespace Tonga.Enumerable.Test
{
    public sealed class DistinctTest
    {
        [Fact]
        public void MergesEntries()
        {
            Assert.True(
                new LengthOf(
                    new Distinct<int>(
                        new ManyOf<int>(1, 2, 3),
                        new ManyOf<int>(10, 2, 30)
                    )
                ).Value() == 5);
        }

        [Fact]
        public void MergesComparedEntries()
        {
            Assert.Equal(
                5,
                new LengthOf(
                    new Distinct<INumber>(
                        new ManyOf<IEnumerable<INumber>>(
                            new ManyOf<INumber>(
                                new NumberOf(1),
                                new NumberOf(2),
                                new NumberOf(3)
                            ),
                            new ManyOf<INumber>(
                                new NumberOf(10),
                                new NumberOf(2),
                                new NumberOf(30)
                            )
                        ),
                        (v1, v2) => v1.AsInt().Equals(v2.AsInt())
                    )
                ).Value()
            );
        }

        [Fact]
        public void MergesEntriesWithEnumCtor()
        {
            Assert.True(
                new LengthOf(
                    new Distinct<int>(
                        new ManyOf<IEnumerable<int>>(
                            new ManyOf<int>(1, 2, 3),
                            new ManyOf<int>(10, 2, 30)
                        )
                    )
                ).Value() == 5);
        }

        [Fact]
        public void WorksWithEmpties()
        {
            Assert.True(
                new LengthOf(
                    new Distinct<string>(
                        new ManyOf<string>(),
                        new ManyOf<string>()
                    )
                ).Value() == 0);
        }

        [Fact]
        public void DoubleRunDistinct()
        {
            var dst =
                new Distinct<string>(
                    new ManyOf<string>("test", "test")
                );

            var first = string.Join("", dst);
            var second = string.Join("", dst);
            Assert.Equal(
                new LengthOf(dst).Value(),
                new LengthOf(dst).Value()
            );
        }
    }
}
