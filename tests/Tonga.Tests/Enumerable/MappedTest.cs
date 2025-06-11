using Tonga.Enumerable;
using Tonga.List;
using Tonga.Text;
using Xunit;


namespace Tonga.Tests.Enumerable
{
    public sealed class MappedTest
    {
        [Fact]
        public void TransformsEnumerable()
        {
            Assert.Equal(
                "HELLO",
                ("hello", "world", "damn")
                    .AsEnumerable()
                    .AsMapped(input => new Upper(input.AsText()))
                    .ItemAt(0)
                    .Value()
                    .Str()
            );
        }

        [Fact]
        public void SensesChanges()
        {
            var mappings = 0;
            var mapping =
                    ("hello", "world", "damn")
                        .AsEnumerable()
                        .AsMapped(
                            input =>
                            {
                                mappings++;
                                return input.AsText().AsUpper();
                            }
                        );

            var enm1 = mapping.GetEnumerator();
            enm1.MoveNext();
            _ = enm1.Current;
            var enm2 = mapping.GetEnumerator();
            enm2.MoveNext();
            _ = enm2.Current;

            Assert.Equal(2, mappings);
        }

        [Fact]
        public void MappedResultCanBeLive()
        {
            var mappings = 0;
            var mapping =
                    ("hello", "world", "damn")
                        .AsEnumerable()
                        .AsMapped(
                            input =>
                            {
                                mappings++;
                                return input;
                            }
                        );

            var enm1 = mapping.GetEnumerator();
            enm1.MoveNext();
            _ = enm1.Current;
            var enm2 = mapping.GetEnumerator();
            enm2.MoveNext();
            _ = enm2.Current;

            Assert.Equal(2, mappings);
        }

        [Fact]
        public void TransformsEmptyList()
        {
            Assert.Empty(
                new None<string>()
                    .AsMapped(input => input.AsText().AsUpper())
            );
        }

        [Fact]
        public void TransformsListUsingIndex()
        {
            Assert.Equal(
                "WORLD1",
                ("hello", "world", "damn")
                    .AsEnumerable()
                    .AsMapped((input, index) => new Upper((input + index)
                    .AsText())
                ).First()
                .Value()
                .Str()
            );
        }


        [Fact]
        public void AdvancesOnlyNecessary()
        {
            var advances = 0;
            ("item1", "item2", "item3")
                .AsEnumerable()
                .AsDebugLogging(_ => advances++)
                .AsMapped(item => item)
                .GetEnumerator()
                .MoveNext();

            Assert.Equal(1, advances);

        }

        [Fact]
        public void CopyCtorDoesNotAdvance()
        {
            var advances = 0;
            ("item1", "item2", "item3")
                .AsEnumerable()
                .AsDebugLogging(_ => advances++)
                .AsMapped(item => item)
                .GetEnumerator();

            Assert.Equal(0, advances);

        }
    }
}
