

using System;
using System.Collections.Generic;
using Xunit;

namespace Tonga.Map.Tests
{
    public class MapNoNullTest
    {
        [Fact]
        public void ContainsWithExistingItem()
        {
            Assert.Contains(
                new KeyValuePair<int, int>(0, 0),
                new NoNulls<int, int>(
                    new AsMap<int, int>(
                        new KeyValuePair<int, int>(0, 0)
                    )
                )
            );
        }

        [Fact]
        public void ContainsWithMissingItemKey()
        {
            Assert.DoesNotContain(
                new KeyValuePair<int, int>(1, 0),
                new NoNulls<int, int>(
                    new AsMap<int, int>(
                        new KeyValuePair<int, int>(0, 0)
                    )
                )
            );
        }

        [Fact]
        public void ContainsWithMissingValue()
        {
            Assert.DoesNotContain(
                new KeyValuePair<int, int>(0, 1),
                new NoNulls<int, int>(
                    new AsMap<int, int>(
                        new KeyValuePair<int, int>(0, 0)
                    )
                )
            );
        }

        [Fact]
        public void ContainsWithMissingItem()
        {
            Assert.DoesNotContain(
                new KeyValuePair<int, int>(1, 1),
                new NoNulls<int, int>(
                    new AsMap<int, int>(
                        new KeyValuePair<int, int>(0, 0)
                    )
                )
            );
        }

        [Fact]
        public void ContainsWithNullKey()
        {
            var map = new NoNulls<object, object>(
                new AsMap<object, object>(
                    new KeyValuePair<object, object>(0, 0)
                )
            );
            Assert.Throws<ArgumentException>(
                () => map.Contains(new KeyValuePair<object, object>(null, 0))
            );
        }

        [Fact]
        public void ContainsWithNullValue()
        {
            var map = new NoNulls<object, object>(
                new AsMap<object, object>(
                    new KeyValuePair<object, object>(0, 0)
                )
            );
            Assert.Throws<ArgumentException>(
                () => map.Contains(new KeyValuePair<object, object>(0, null))
            );
        }

        [Fact]
        public void ContainsWithNullKeyAndNullValue()
        {
            var map = new NoNulls<object, object>(
                new AsMap<object, object>(
                    new KeyValuePair<object, object>(0, 0)
                )
            );
            Assert.Throws<ArgumentException>(
                () => map.Contains(new KeyValuePair<object, object>(null, null))
            );
        }

        [Fact]
        public void ContainsKeyWithExistingKey()
        {
            var map = new NoNulls<int, int>(
                new AsMap<int, int>(
                    new KeyValuePair<int, int>(0, 0)
                )
            );
            Assert.True(map.ContainsKey(0));
        }

        [Fact]
        public void ContainsKeyWithMissingKey()
        {
            var map = new NoNulls<int, int>(
                new Dictionary<int, int> {
                    {0, 0}
                }
            );
            Assert.False(map.ContainsKey(1));
        }

        [Fact]
        public void ContainsKeyWithNullKey()
        {
            var map = new NoNulls<object, object>(
                new Dictionary<object, object> {
                    {0, 0}
                }
            );
            Assert.Throws<ArgumentException>(() => map.ContainsKey(null));
        }

        [Fact]
        public void AddEntry()
        {
            var map = new NoNulls<int, int>(new Dictionary<int, int>());
            map.Add(new KeyValuePair<int, int>(0, 0));
            Assert.Contains(new KeyValuePair<int, int>(0, 0), map);
        }

        [Fact]
        public void AddEntryWithNullKey()
        {
            var map = new NoNulls<object, object>(new AsMap<object, object>());
            Assert.Throws<ArgumentException>(
                () => map.Add(new KeyValuePair<object, object>(null, 0))
            );
        }

        [Fact]
        public void AddEntryWithNullValue()
        {
            var map = new NoNulls<object, object>(new AsMap<object, object>());
            Assert.Throws<ArgumentException>(
                () => map.Add(new KeyValuePair<object, object>(0, null))
            );
        }

        [Fact]
        public void AddEntryWithNullKeyAndNullValue()
        {
            var map = new NoNulls<object, object>(new AsMap<object, object>());
            Assert.Throws<ArgumentException>(
                () => map.Add(new KeyValuePair<object, object>(null, null))
            );
        }

        [Fact]
        public void AddKeyAndValue()
        {
            var map = new NoNulls<int, int>(new Dictionary<int, int>());
            map.Add(0, 0);
            Assert.Contains(new KeyValuePair<int, int>(0, 0), map);
        }

        [Fact]
        public void AddNullKeyAndValue()
        {
            var map = new NoNulls<object, object>(new Dictionary<object, object>());
            Assert.Throws<ArgumentException>(() => map.Add(null, 0));
        }

        [Fact]
        public void AddKeyAndNullValue()
        {
            var map = new NoNulls<object, object>(new Dictionary<object, object>());
            Assert.Throws<ArgumentException>(() => map.Add(0, null));
        }

        [Fact]
        public void AddNullKeyAndNullValue()
        {
            var map = new NoNulls<object, object>(new Dictionary<object, object>());
            Assert.Throws<ArgumentException>(() => map.Add(null, null));
        }

        [Fact]
        public void TryGetValueWithExistingKey()
        {
            var map = new NoNulls<int, int>(
                new Dictionary<int, int>{
                    {0, 0}
                }
            );
            int outValue;
            Assert.True(map.TryGetValue(0, out outValue));
            Assert.Equal(0, outValue);
        }

        [Fact]
        public void TryGetValueWithMissingKey()
        {
            var map = new NoNulls<int, int>(
                new Dictionary<int, int>{
                    {0, 0}
                }
            );
            int outValue;
            Assert.False(map.TryGetValue(1, out outValue));
        }

        [Fact]
        public void TryGetValueWithNullKey()
        {
            var map = new NoNulls<object, object>(
                new AsMap<object, object>(
                    new KeyValuePair<object, object>(0, 0)
                )
            );
            object outValue;
            Assert.Throws<ArgumentException>(() => map.TryGetValue(null, out outValue));
        }

        [Fact]
        public void TryGetNullValue()
        {
            var map = new NoNulls<object, object>(
                new Dictionary<object, object>{
                    {0, null}
                }
            );
            object outValue;
            Assert.Throws<ArgumentException>(() => map.TryGetValue(0, out outValue));
        }

        [Fact]
        public void GetValue()
        {
            var map = new NoNulls<int, int>(
                new AsMap<int, int>(
                    new KeyValuePair<int, int>(0, 0)
                )
            );
            Assert.Equal(0, map[0]);
        }

        [Fact]
        public void GetValueWithNullKey()
        {
            var map = new NoNulls<object, object>(
                new AsMap<object, object>(
                    new KeyValuePair<object, object>(0, 0)
                )
            );
            Assert.Throws<ArgumentException>(() => map[null]);
        }

        [Fact]
        public void GetNullValue()
        {
            var map = new NoNulls<object, object>(
                new AsMap<object, object>(
                    new KeyValuePair<object, object>(0, null)
                )
            );
            Assert.Throws<ArgumentException>(() => map[0]);
        }

        [Fact]
        public void SetValue()
        {
            var map = new NoNulls<int, int>(
                new Dictionary<int, int>{
                    {0, 0}
                }
            );
            map[0] = 1;
            Assert.Equal(1, map[0]);
        }

        [Fact]
        public void SetValueWithNullKey()
        {
            var map = new NoNulls<object, object>(
                new AsMap<object, object>(
                    new KeyValuePair<object, object>(0, 0)
                )
            );
            Assert.Throws<ArgumentException>(() => map[null] = 1);
        }

        [Fact]
        public void SetValueWithNullValue()
        {
            var map = new NoNulls<object, object>(
                new AsMap<object, object>(
                    new KeyValuePair<object, object>(0, 0)
                )
            );
            Assert.Throws<ArgumentException>(() => map[0] = null);
        }

        [Fact]
        public void SetValueWithNullKeyAndNullValue()
        {
            var map = new NoNulls<object, object>(
                new AsMap<object, object>(
                    new KeyValuePair<object, object>(0, 0)
                )
            );
            Assert.Throws<ArgumentException>(() => map[null] = null);
        }
    }
}
