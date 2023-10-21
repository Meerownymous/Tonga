

using System;
using System.Collections.Generic;
using System.Threading;
using Xunit;
using Tonga.Enumerable;
using Tonga.Scalar;

namespace Tonga.Map.Tests
{
    public class MapOfTest
    {
        [Fact]
        public void BehavesAsMap()
        {
            var one = new KeyValuePair<string, string>("hello", "map");
            var two = new KeyValuePair<string, string>("goodbye", "dictionary");

            var m = new MapOf(one, two);

            Assert.True(m.Contains(one) && m.Contains(two));
        }

        [Theory]
        [InlineData("A", "V")]
        [InlineData("B", "Y")]
        public void BuildsFromInputs(string key, string value)
        {
            Assert.Equal(
                value,
                new MapOf(
                    new MapInputOf(new AsPair("A", "V")),
                    new MapInputOf(new AsPair("B", "Y"))
                )[key]
            );
        }

        [Fact]
        public void ConvertsEnumerableToMap()
        {
            var m =
                new MapOf(
                    new KeyValuePair<string, string>("0", "hello, "),
                    new KeyValuePair<string, string>("1", "world!")
                );


            Assert.True(m["0"] == "hello, ");
            Assert.True(m["1"] == "world!");
        }

        [Fact]
        public void MakesMapFromArraySequence()
        {
            Assert.Equal(
                "B",
                new MapOf(
                    "A", "B",
                    "C", "D"
                )["A"]
            );
        }

        [Fact]
        public void MakesMapFromEnumerableSequence()
        {
            Assert.Equal(
                "B",
                new MapOf(
                    Enumerable.AsEnumerable._(
                        "A", "B",
                        "C", "D"
                    )
                )["A"]
            );
        }

        [Fact]
        public void RejectsOddValueCount()
        {
            Assert.Throws<ArgumentException>(() =>
                new MapOf(
                    Enumerable.AsEnumerable._(
                        "A", "B",
                        "C"
                    )
                )["A"]
            );
        }

        [Fact]
        public void IsContentStickyTypedValue()
        {
            int size = 1;

            var map = new MapOf<int>(
                () =>
                    new Dictionary<string, int>()
                    {
                        { "a", 1 },
                        { "b", Interlocked.Increment(ref size) }
                    }
            );

            Assert.Equal(2, map.Count);
            Assert.Equal(2, map.Count);

            Assert.Equal(2, map["b"]);
            Assert.Equal(2, map["b"]);
        }

        [Fact]
        public void IsSticky()
        {
            int size = 1;
            var random = new Random();

            var map =
                new MapOf(
                    Repeated._(
                        () => new KeyValuePair<string, string>(random.Next() + "", "1"),
                        () =>
                        {
                            Interlocked.Increment(ref size);
                            return size;
                        }
                    )
                );

            var a = map.Count;
            var b = map.Count;

            Assert.Equal(a, b);
        }

        [Fact]
        public void BehavesAsMapTypedValue()
        {
            var one = new KeyValuePair<string, int>("hello", 10);
            var two = new KeyValuePair<string, int>("goodbye", 20);

            var m = new MapOf<int>(one, two);

            Assert.True(m.Contains(one) && m.Contains(two));
        }

        [Theory]
        [InlineData("A", 39478624)]
        [InlineData("B", 60208801)]
        public void BuildsFromInputsTypedValue(string key, int value)
        {
            Assert.Equal(
                value,
                new MapOf<int>(
                    new MapInputOf<int>(new AsPair<int>("A", 39478624)),
                    new MapInputOf<int>(new AsPair<int>("B", 60208801))
                )[key]
            );
        }

        [Theory]
        [InlineData("hello", 0)]
        [InlineData("world", 1)]
        public void ConvertsEnumerableToMapTypedValue(string key, int value)
        {
            var m =
                new MapOf<int>(
                    new KeyValuePair<string, int>("hello", 0),
                    new KeyValuePair<string, int>("world", 1)
                );


            Assert.Equal(m[key], value);
        }

        [Fact]
        public void IsStickyTypedValue()
        {
            int size = 1;
            var random = new Random();

            var map =
                new MapOf<int>(
                    Repeated._(
                        AsScalar._(() =>
                            new AsPair<int>(random.Next() + "", 1)),
                            AsScalar._(() =>
                            {
                                Interlocked.Increment(ref size);
                                return size;
                            }
                        )
                    )
                );

            var a = map.Count;
            var b = map.Count;

            Assert.Equal(a, b);
        }

        [Fact]
        public void BehavesAsMapTypedKeyValue()
        {
            var one = new KeyValuePair<int, int>(45, 10);
            var two = new KeyValuePair<int, int>(33, 20);

            var m = new MapOf<int, int>(one, two);

            Assert.True(m.Contains(one) && m.Contains(two));
        }

        [Theory]
        [InlineData(12, 39478624)]
        [InlineData(24, 60208801)]
        public void BuildsFromInputsTypedKeyValue(int key, int value)
        {
            Assert.Equal(
                value,
                new MapOf<int, int>(
                    new MapInputOf<int, int>(new AsPair<int, int>(12, 39478624)),
                    new MapInputOf<int, int>(new AsPair<int, int>(24, 60208801))
                )[key]
            );
        }

        [Theory]
        [InlineData(9, 0)]
        [InlineData(10, 1)]
        public void ConvertsEnumerableToMapTypedKeyValue(int key, int value)
        {
            var m =
                new MapOf<int, int>(
                    new KeyValuePair<int, int>(9, 0),
                    new KeyValuePair<int, int>(10, 1)
                );


            Assert.Equal(m[key], value);
        }

        [Fact]
        public void IsStickyWithTypedKeyValue()
        {
            int size = 1;
            var random = new Random();

            var map =
                new MapOf<int, int>(
                    Repeated._(
                        () => new AsPair<int, int>(random.Next(), 1),
                        () =>
                        {
                            Interlocked.Increment(ref size);
                            return size;
                        }
                    )
                );

            var a = map.Count;
            var b = map.Count;

            Assert.Equal(a, b);
        }

        [Fact]
        public void DoesNotBuildAllValues()
        {
            Assert.Equal(
                "works",
                new MapOf(
                    new AsPair("name", () => throw new ApplicationException()),
                    new AsPair("anothername", () => "works")
                )["anothername"]
            );
        }

        [Fact]
        public void IKvpRejectsBuildingAllValues()
        {
            var map =
                new MapOf(
                    new AsPair("name", () => "also works"),
                    new AsPair("name2", () => "works")
                );

            Assert.Throws<InvalidOperationException>(() => map.GetEnumerator());
        }

        [Fact]
        public void WorksWithEmptyList()
        {
            var map = new MapOf(new None());
            Assert.Equal(0, map.Keys.Count);
        }

        [Fact]
        public void ImplicitCtorWithMapWorks()
        {
            var map =
                MapOf._(
                    AsPair._("target",
                        new FallbackMap(
                            new MapOf(
                                "CONTAINS", "contains",
                                "GT", ">",
                                "LT", "<",
                                "EQ", "="
                            ),
                            unkown => unkown
                        )
                    ),
                    AsPair._("program",
                        new FallbackMap(
                            new MapOf(
                                "CONTAINS", "contains",
                                "GT", ">",
                                "LT", "<",
                                "EQ", "="
                            ),
                            unkown => unkown
                        )
                    )
                );
        }
    }
}
