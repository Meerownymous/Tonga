

using System;
using System.Collections.Generic;
using System.Threading;
using Xunit;
using Tonga.Enumerable;
using Tonga.Scalar;

namespace Tonga.Map.Tests
{
    public class MapLiveTests
    {
        [Fact]
        public void BehavesAsMap()
        {
            var one = new KeyValuePair<string, string>("hello", "map");
            var two = new KeyValuePair<string, string>("goodbye", "dictionary");

            var m =
                new LiveMap(() =>
                    new Enumerable.AsEnumerable<IPair>(
                        new AsPair(() => one),
                        new AsPair(() => two)
                    )
                );

            Assert.True(m[one.Key] == one.Value && m[two.Key] == two.Value);
        }

        [Fact]
        public void DoesNotEnumerateDictionary()
        {
            var failed = false;
            var unused =
                new LiveMap(() =>
                    new Enumerable.AsEnumerable<IPair>(
                        new AsPair("key a", "value a"),
                        new AsPair("key b", () =>
                        {
                            failed = true;
                            return "value b";
                        })
                    )
                )["key a"];
            Assert.False(failed);
        }

        [Fact]
        public void ConvertsEnumerableToMap()
        {
            var m =
                new LiveMap(() =>
                    new MapOf(
                        new KeyValuePair<string, string>("0", "hello, "),
                        new KeyValuePair<string, string>("1", "world!")
                    )
                );


            Assert.True(m["0"] == "hello, ");
            Assert.True(m["1"] == "world!");
        }

        [Fact]
        public void SensesChangesInMap()
        {
            int size = 1;
            var random = new Random();

            var map =
                new LiveMap<int, int>(() =>
                    new MapOf<int, int>(
                        new Repeated<KeyValuePair<int, int>>(
                            AsScalar._(() =>
                                new KeyValuePair<int, int>(random.Next(), 1)
                            ),
                            AsScalar._(() =>
                            {
                                Interlocked.Increment(ref size);
                                return size;
                            })
                        )
                    )
                );

            var a = map.Count;
            var b = map.Count;

            Assert.NotEqual(a, b);
        }

        [Fact]
        public void SensesChangesInValues()
        {
            var value = 0;
            var map =
                new LiveMap(() =>
                    new Enumerable.AsEnumerable<IPair>(
                        new AsPair("key", () => (value++).ToString())
                    )
                );
            var a = map["key"];
            var b = map["key"];
            Assert.NotEqual(a, b);
        }

        [Fact]
        public void BehavesAsMapTypedValue()
        {
            var one = new KeyValuePair<string, int>("hello", 0);
            var two = new KeyValuePair<string, int>("goodbye", 1);

            var m =
                new LiveMap<int>(() =>
                    new Enumerable.AsEnumerable<IPair<int>>(
                        new AsPair<int>(() => one),
                        new AsPair<int>(() => two)
                    )
                );

            Assert.True(m[one.Key] == one.Value && m[two.Key] == two.Value);
        }

        [Fact]
        public void DoesNotEnumerateDictionaryTypedValue()
        {
            var failed = false;
            var unused =
                new LiveMap<int>(() =>
                    new Enumerable.AsEnumerable<IPair<int>>(
                        new AsPair<int>("key a", 0),
                        new AsPair<int>("key b", () =>
                        {
                            failed = true;
                            return 1;
                        })
                    )
                )["key a"];
            Assert.False(failed);
        }

        [Fact]
        public void ConvertsEnumerableToMapTypedValue()
        {
            var m =
                new LiveMap<int>(() =>
                    new MapOf<int>(
                        Enumerable.AsEnumerable._(
                            new KeyValuePair<string, int>("hello", 0),
                            new KeyValuePair<string, int>("world", 1)
                        )
                    )
                );


            Assert.True(m["hello"] == 0);
            Assert.True(m["world"] == 1);
        }

        [Fact]
        public void SensesChangesInMapTypedValue()
        {
            int size = 1;
            var random = new Random();

            var map =
                new LiveMap<int>(() =>
                    new MapOf<int>(
                        new Repeated<KeyValuePair<string, int>>(
                            AsScalar._(() =>
                                new KeyValuePair<string, int>(random.Next() + "", 1)),
                                AsScalar._(() =>
                                {
                                    Interlocked.Increment(ref size);
                                    return size;
                                })
                            )
                        )
                    );

            var a = map.Count;
            var b = map.Count;

            Assert.NotEqual(a, b);
        }

        [Fact]
        public void SensesChangesInValuesTypedValue()
        {
            var value = 0;
            var map =
                new LiveMap<int>(() =>
                    new Enumerable.AsEnumerable<IPair<int>>(
                        new AsPair<int>("key", () => value++)
                    )
                );
            var a = map["key"];
            var b = map["key"];
            Assert.NotEqual(a, b);
        }

        [Fact]
        public void BehavesAsMapTypedKeyValue()
        {
            var one = new KeyValuePair<string, string>("hello", "map");
            var two = new KeyValuePair<string, string>("goodbye", "dictionary");

            var m =
                new LiveMap<string, string>(() =>
                    new Enumerable.AsEnumerable<IPair<string, string>>(
                        new AsPair<string, string>(() => one),
                        new AsPair<string, string>(() => two)
                    )
                );

            Assert.True(m[one.Key] == one.Value && m[two.Key] == two.Value);
        }

        [Fact]
        public void DoesNotEnumerateDictionaryTypedKeyValue()
        {
            var failed = false;
            var unused =
                new LiveMap<int, int>(() =>
                    new Enumerable.AsEnumerable<IPair<int, int>>(
                        new AsPair<int, int>(10, 0),
                        new AsPair<int, int>(11, () =>
                        {
                            failed = true;
                            return 1;
                        })
                    )
                )[10];
            Assert.False(failed);
        }

        [Fact]
        public void ConvertsEnumerableToMapTypedKeyValue()
        {
            var m =
                new LiveMap<int, String>(() =>
                    new MapOf<int, string>(
                        new KeyValuePair<int, string>(0, "hello, "),
                        new KeyValuePair<int, string>(1, "world!")
                    )
                );


            Assert.True(m[0] == "hello, ");
            Assert.True(m[1] == "world!");
        }

        [Fact]
        public void SensesChangesInMapTypedKeyValue()
        {
            int size = 1;
            var random = new Random();

            var map =
                new LiveMap<int, int>(() =>
                    new MapOf<int, int>(
                        new Repeated<KeyValuePair<int, int>>(
                            AsScalar._(() =>
                                new KeyValuePair<int, int>(random.Next(), 1)),
                                AsScalar._(() =>
                                {
                                    Interlocked.Increment(ref size);
                                    return size;
                                })
                            )
                        )
                    );

            var a = map.Count;
            var b = map.Count;

            Assert.NotEqual(a, b);
        }

        [Fact]
        public void SensesChangesInValuesTypedKeyValue()
        {
            var value = 0;
            var map =
                new LiveMap<int, int>(() =>
                    AsEnumerable._(
                        new AsPair<int, int>(0, () => value++)
                    )
                );
            var a = map[0];
            var b = map[0];
            Assert.NotEqual(a, b);
        }
    }
}
