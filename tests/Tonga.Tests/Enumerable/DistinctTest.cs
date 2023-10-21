

using System.Collections.Generic;
using Xunit;
using Tonga.Number;
using Tonga.Scalar;

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
                        AsEnumerable._(1, 2, 3),
                        AsEnumerable._(10, 2, 30)
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
                        AsEnumerable._(
                            AsEnumerable._(
                                new NumberOf(1),
                                new NumberOf(2),
                                new NumberOf(3)
                            ),
                             AsEnumerable._(
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
                        AsEnumerable._(
                            AsEnumerable._(1, 2, 3),
                            AsEnumerable._(10, 2, 30)
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
                        new None(),
                        new None()
                    )
                ).Value() == 0);
        }

        [Fact]
        public void DoubleRunDistinct()
        {
            var dst =
                new Distinct<string>(
                    AsEnumerable._("test", "test")
                );

            Assert.Equal(
                new LengthOf(dst).Value(),
                new LengthOf(dst).Value()
            );
        }
    }
}
