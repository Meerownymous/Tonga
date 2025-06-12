using System.Collections.Generic;
using Tonga.Enumerable;
using Tonga.Number;
using Xunit;

namespace Tonga.Tests.Enumerable
{
    public sealed class DistinctTest
    {
        [Fact]
        public void MergesEntries()
        {
            Assert.Equal(
                5,
                (
                    (1, 2, 3).AsEnumerable(),
                    (10, 2, 30).AsEnumerable()
                )
                .AsEnumerable()
                .AsDistinct()
                .Length()
                .Value()
            );
        }

        [Fact]
        public void MergesComparedEntries()
        {
            Assert.Equal(
                5,
                new [] {
                    new[]
                    {
                        new AsNumber(1),
                        new AsNumber(2),
                        new AsNumber(3)
                    }.AsEnumerable(),
                    new[]
                    {
                        new AsNumber(10),
                        new AsNumber(2),
                        new AsNumber(30)
                    }.AsEnumerable()
                }
                .AsEnumerable()
                .AsDistinct((v1, v2) => v1.Int().Equals(v2.Int()))
                .Length()
                .Value()
            );
        }

        [Fact]
        public void MergesEntriesFromEnumerables()
        {
            Assert.Equal(
                5,
                new []
                {
                    new[]{ 1, 2, 3 }.AsEnumerable(),
                    new []{10, 2, 30}.AsEnumerable()
                }
                .AsEnumerable()
                .AsDistinct()
                .Length()
                .Value()
            );
        }

        [Fact]
        public void WorksWithEmpties()
        {
            Assert.Equal(
                0,
                new[]{
                    new string[0].AsEnumerable(),
                    new string[0].AsEnumerable()
                }
                .AsEnumerable()
                .AsDistinct()
                .Length()
                .Value()
            );
        }
    }
}
