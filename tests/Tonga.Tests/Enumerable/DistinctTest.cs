using Tonga.Enumerable;
using Tonga.Number;
using Tonga.Scalar;
using Xunit;

namespace Tonga.Tests.Enumerable
{
    public sealed class DistinctTest
    {
        [Fact]
        public void MergesEntries()
        {
            Assert.True(
                Length._(
                    Distinct._(
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
                Length._(
                    Distinct._(
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
        public void MergesEntriesFromEnumerables()
        {
            Assert.Equal(
                5,
                Length._(
                    Distinct._(
                        AsEnumerable._(
                            AsEnumerable._(1, 2, 3),
                            AsEnumerable._(10, 2, 30)
                        )
                    )
                ).Value()
            );
        }

        [Fact]
        public void WorksWithEmpties()
        {
            Assert.True(
                Length._(
                    Distinct._(
                        None._<string>(),
                        None._<string>()
                    )
                ).Value() == 0);
        }

        [Fact]
        public void DoubleRunDistinct()
        {
            var dst =
                Distinct._(
                    AsEnumerable._("test", "test")
                );

            Assert.Equal(
                Length._(dst).Value(),
                Length._(dst).Value()
            );
        }
    }
}
