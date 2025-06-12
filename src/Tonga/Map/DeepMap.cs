using System;
using System.Collections.Generic;
using Tonga.Enumerable;

namespace Tonga.Map;

/// <summary>
/// Maps a deeper level of a key to a value.
/// </summary>
public sealed class DeepMap<Surface, Deep, Value> : IMap<Surface, Value>
{
    private readonly Func<Surface, Deep> digDown;
    private readonly IMap<Deep, Value> deep;
    private readonly IMap<Surface, Value> shallow;

    /// <summary>
    /// Maps a deeper level of a key to a value.
    /// </summary>
    public DeepMap(Func<Surface, Deep> digDown, IMap<Surface, Value> origin)
    {
        this.digDown = digDown;
        this.shallow = origin;
        this.deep =
            new AsMap<Deep, Value>(() =>
                origin
                    .Pairs()
                    .AsMapped(pair => (digDown(pair.Key()), pair.Value()).AsPair())
            );
    }

    public Value this[Surface key] => this.deep[this.digDown(key)];
    public ICollection<Surface> Keys() => this.shallow.Keys();
    public Func<Value> Lazy(Surface key) => () => this[key];
    public IEnumerable<IPair<Surface, Value>> Pairs() => this.shallow.Pairs();
    public IMap<Surface, Value> With(IPair<Surface, Value> pair) =>
        new DeepMap<Surface, Deep, Value>(this.digDown, this.shallow.With(pair));
    }

    /// <summary>
    /// Maps a deeper level of a key to a value.
    /// </summary>
    public static partial class MapSmarts
    {
        /// <summary>
        /// Maps a deeper level of a key to a value.
        /// </summary>
        public static IMap<Surface, Value> AsDeepMap<Surface, Deep, Value>(
            Func<Surface, Deep> digDown, IMap<Surface, Value> origin
        ) =>
            new DeepMap<Surface, Deep, Value>(digDown, origin);
    }

