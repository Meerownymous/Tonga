

using System;
using Xunit;
using Tonga.List;
using Tonga.Text;

namespace Tonga.Enumerable.Test
{
    public sealed class MappedTest
    {
        [Fact]
        public void TransformsList()
        {
            Assert.Equal(
                "HELLO",
                new ItemAt<IText>(
                    new Mapped<String, IText>(
                        input => new Upper(new LiveText(input)),
                        EnumerableOf.Pipe("hello", "world", "damn")),
                    0
                ).Value().AsString()
            );
        }

        [Fact]
        public void MappedResultIsSticky()
        {
            var mappings = 0;
            var mapping =
                new Mapped<String, IText>(
                    input =>
                    {
                        mappings++;
                        return new Upper(new LiveText(input));
                    },
                    EnumerableOf.Pipe("hello", "world", "damn")
                );

            var enm1 = mapping.GetEnumerator();
            enm1.MoveNext(); var current = enm1.Current;
            var enm2 = mapping.GetEnumerator();
            enm2.MoveNext(); var current2 = enm2.Current;

            Assert.Equal(1, mappings);
        }

        [Fact]
        public void MappedResultCanBeLive()
        {
            var mappings = 0;
            var mapping =
                new Mapped<string, string>(
                    input =>
                    {
                        mappings++;
                        return input;
                    },
                    EnumerableOf.Pipe("hello", "world", "damn"),
                    live: true
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
            Assert.True(
                new LengthOf(
                    new Mapped<String, IText>(
                        input => new Upper(new LiveText(input)),
                        new None()
                    )
                ).Value() == 0
            );
        }

        [Fact]
        public void TransformsListUsingIndex()
        {
            Assert.Equal(
                "WORLD1",
                new ItemAt<IText>(
                    new Mapped<String, IText>(
                        (input, index) => new Upper(new LiveText(input + index)),
                        EnumerableOf.Pipe("hello", "world", "damn")
                        ),
                    1
                ).Value().AsString()
            );
        }


        [Fact]
        public void AdvancesOnlyNecessary()
        {
            var advances = 0;
            var origin = new ListOf<string>("item1", "item2", "item3");

            var list =
                new Mapped<string, string>(
                    item => item,
                    new Logging<string>(
                        origin,
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
            var origin = new ListOf<string>("item1", "item2", "item3");

            var list =
                new Mapped<string, string>(
                    item => item,
                    new Logging<string>(
                        origin,
                        idx => advances++
                    )
                );
            list.GetEnumerator();

            Assert.Equal(0, advances);

        }
    }
}
