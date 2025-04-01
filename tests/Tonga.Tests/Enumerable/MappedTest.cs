using Tonga.Enumerable;
using Tonga.List;
using Tonga.Scalar;
using Tonga.Text;
using Xunit;
using Mapped = Tonga.Enumerable.Mapped;

namespace Tonga.Tests.Enumerable
{
    public sealed class MappedTest
    {
        [Fact]
        public void TransformsList()
        {
            Assert.Equal(
                "HELLO",
                ItemAt._(
                    Mapped._(
                        input => new Upper(AsText._((string)input)),
                        AsEnumerable._("hello", "world", "damn")),
                    0
                ).Value().AsString()
            );
        }

        [Fact]
        public void SensesChanges()
        {
            var mappings = 0;
            var mapping =
                Mapped._(
                    input =>
                    {
                        mappings++;
                        return Upper._(AsText._(input));
                    },
                    AsEnumerable._("hello", "world", "damn")
                );

            var enm1 = mapping.GetEnumerator();
            enm1.MoveNext(); var current = enm1.Current;
            var enm2 = mapping.GetEnumerator();
            enm2.MoveNext(); var current2 = enm2.Current;

            Assert.Equal(2, mappings);
        }

        [Fact]
        public void MappedResultCanBeLive()
        {
            var mappings = 0;
            var mapping =
                new Tonga.Enumerable.Mapped<string, string>(
                    input =>
                    {
                        mappings++;
                        return input;
                    },
                    AsEnumerable._("hello", "world", "damn")
                );

            var enm1 = mapping.GetEnumerator();
            enm1.MoveNext(); var current = enm1.Current;
            var enm2 = mapping.GetEnumerator();
            enm2.MoveNext(); var current2 = enm2.Current;

            Assert.Equal(2, mappings);
        }

        [Fact]
        public void TransformsEmptyList()
        {
            Assert.Equal(
                0,
                Length._(
                    Mapped._(
                        input => new Upper(AsText._(input)),
                        None._<string>()
                    )
                ).Value()
            );
        }

        [Fact]
        public void TransformsListUsingIndex()
        {
            Assert.Equal(
                "WORLD1",
                ItemAt._(
                    Mapped._(
                        (input, index) => new Upper(AsText._(input + index)),
                        AsEnumerable._("hello", "world", "damn")
                    ),
                    1
                ).Value().AsString()
            );
        }


        [Fact]
        public void AdvancesOnlyNecessary()
        {
            var advances = 0;
            var list =
                Mapped._(
                    item => item,
                    Logging._(
                        AsList._("item1", "item2", "item3"),
                        idx => advances++
                    )
                );

            list.GetEnumerator().MoveNext();

            Assert.Equal(1, advances);

        }

        [Fact]
        public void CopyCtorDoesNotAdvance()
        {
            var advances = 0;
            var origin =  AsList._("item1", "item2", "item3");

            var list =
                Mapped._(
                    item => item,
                    Logging._(
                        origin,
                        idx => advances++
                    )
                );
            list.GetEnumerator();

            Assert.Equal(0, advances);

        }
    }
}
