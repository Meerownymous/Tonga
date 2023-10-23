

using System;
using System.Collections.Generic;
using Tonga.Enumerable;

namespace Tonga.Map
{
    /// <summary>
    /// Simplified MapInput building.
    /// Since 9.9.2019
    /// </summary>
    public abstract class MapInputEnvelope : IMapInput
    {
        private readonly Func<IDictionary<string, string>, IDictionary<string, string>> apply;

        /// <summary>
        /// Simplified DictInput building.
        /// </summary>
        public MapInputEnvelope(Func<IMapInput> input) : this(
            dict => new Joined(dict, new AsMap(input()))
        )
        { }

        /// <summary>
        /// Simplified DictInput building.
        /// </summary>
        public MapInputEnvelope(IScalar<IMapInput> input) : this(
            dict => new Joined(dict, new AsMap(input.Value()))
        )
        { }

        /// <summary>
        /// Simplified DictInput building.
        /// </summary>
        public MapInputEnvelope(params IPair[] kvps) : this(
            new AsEnumerable<IPair>(kvps)
        )
        { }

        /// <summary>
        /// Simplified DictInput building.
        /// </summary>
        public MapInputEnvelope(IEnumerable<IPair> kvps) : this(
            input => new Joined(input, new LazyMap(kvps))
        )
        { }

        /// <summary>
        /// Simplified DictInput building.
        /// </summary>
        public MapInputEnvelope(IDictionary<string, string> dict) : this(
            input => new Joined(input, dict)
        )
        { }

        /// <summary>
        /// Simplified DictInput building.
        /// </summary>
        public MapInputEnvelope(Func<IDictionary<string, string>, IDictionary<string, string>> apply)
        {
            this.apply = apply;
        }

        /// <summary>
        /// Apply this input to a map.
        /// </summary>
        public IDictionary<string, string> Apply(IDictionary<string, string> dict)
        {
            return this.apply.Invoke(dict);
        }

        /// <summary>
        /// Return this as IMapInput.
        /// </summary>
        /// <returns></returns>
        public IMapInput Self()
        {
            return this;
        }
    }

    /// <summary>
    /// Simplified MapInput building.
    /// Since 9.9.2019
    /// </summary>
    public abstract class MapInputEnvelope<Value> : IMapInput<Value>
    {
        private readonly Func<IDictionary<string, Value>, IDictionary<string, Value>> apply;

        /// <summary>
        /// Simplified DictInput building.
        /// </summary>
        public MapInputEnvelope(Func<IMapInput<Value>> input) : this(
            dict =>
            Joined._(
                dict,
                AsMap._(input())
            )
        )
        { }

        /// <summary>
        /// Simplified DictInput building.
        /// </summary>
        public MapInputEnvelope(IScalar<IMapInput<Value>> input) : this(
            dict =>
            Joined._(dict, AsMap._(input.Value()))
        )
        { }

        /// <summary>
        /// Simplified DictInput building.
        /// </summary>
        public MapInputEnvelope(params IPair<Value>[] kvps) : this(
            new AsEnumerable<IPair<Value>>(kvps)
        )
        { }

        /// <summary>
        /// Simplified DictInput building.
        /// </summary>
        public MapInputEnvelope(IEnumerable<IPair<Value>> kvps) : this(
            input => new Joined<Value>(input, new LazyDict<Value>(kvps, false))
        )
        { }

        /// <summary>
        /// Simplified DictInput building.
        /// </summary>
        public MapInputEnvelope(IDictionary<string, Value> dict) : this(
            input => new Joined<Value>(input, dict)
        )
        { }

        /// <summary>
        /// Simplified DictInput building.
        /// </summary>
        public MapInputEnvelope(Func<IDictionary<string, Value>, IDictionary<string, Value>> apply)
        {
            this.apply = apply;
        }

        /// <summary>
        /// Apply this input to a map.
        /// </summary>
        public IDictionary<string, Value> Apply(IDictionary<string, Value> dict)
        {
            return this.apply.Invoke(dict);
        }

        /// <summary>
        /// Return this as IMapInput.
        /// </summary>
        /// <returns></returns>
        public IMapInput<Value> Self()
        {
            return this;
        }
    }

    /// <summary>
    /// Simplified MapInput building.
    /// Since 9.9.2019
    /// </summary>
    public abstract class MapInputEnvelope<Key, Value> : IMapInput<Key, Value>
    {
        private readonly Func<IDictionary<Key, Value>, IDictionary<Key, Value>> apply;

        /// <summary>
        /// Simplified DictInput building.
        /// </summary>
        public MapInputEnvelope(Func<IMapInput<Key, Value>> input) : this(
            dict => new Joined<Key, Value>(dict, AsMap._(input()))
        )
        { }

        /// <summary>
        /// Simplified DictInput building.
        /// </summary>
        public MapInputEnvelope(IScalar<IMapInput<Key, Value>> input) : this(
            dict => new Joined<Key, Value>(dict, AsMap._(input.Value()))
        )
        { }

        /// <summary>
        /// Simplified DictInput building.
        /// </summary>
        public MapInputEnvelope(params IPair<Key, Value>[] kvps) : this(
            new AsEnumerable<IPair<Key, Value>>(kvps)
        )
        { }

        /// <summary>
        /// Simplified DictInput building.
        /// </summary>
        public MapInputEnvelope(IEnumerable<IPair<Key, Value>> kvps) : this(
            input => new Joined<Key, Value>(input, new LazyMap<Key, Value>(kvps, false))
        )
        { }

        /// <summary>
        /// Simplified DictInput building.
        /// </summary>
        public MapInputEnvelope(IDictionary<Key, Value> dict) : this(
            input => new Joined<Key, Value>(input, dict)
        )
        { }

        /// <summary>
        /// Simplified DictInput building.
        /// </summary>
        public MapInputEnvelope(Func<IDictionary<Key, Value>, IDictionary<Key, Value>> apply)
        {
            this.apply = apply;
        }

        /// <summary>
        /// Apply this input to a map.
        /// </summary>
        public IDictionary<Key, Value> Apply(IDictionary<Key, Value> dict)
        {
            return this.apply.Invoke(dict);
        }

        /// <summary>
        /// Return this as IMapInput.
        /// </summary>
        /// <returns></returns>
        public IMapInput<Key, Value> Self()
        {
            return this;
        }
    }
}
