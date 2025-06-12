using System;
using System.Collections.Generic;
using Tonga.Enumerable;
using Tonga.List;
using Tonga.Text;

namespace Tonga.Map;

/// <summary>
/// A map which matches a version.
/// It matches the version range, not the exact version.
/// This means if you have two pairs inside: 1.0 and 3.0, and your key is 2.0, the version 1.0 is matched.
/// </summary>
public sealed class VersionMap<Value>(IEnumerable<IPair<Version, Value>> pairs, bool openEnd) : IMap<Version, Value>
{
    private readonly IMap<Version, Value> map = pairs.AsMap();
    private readonly Func<Version, IEnumerable<Version>, ArgumentException> versionNotFound =
        (version, available) =>
        new ArgumentException(
            $"Cannot find value for version {version}, the version must be within: "
            +
            available
                .AsMapped(v => v.ToString())
                .AsJoined(", ")
                .Str()
        );

    /// <summary>
    /// A map which matches a version.
    /// It matches the version range, not the exact version.
    /// This means if you have two pairs inside: 1.0 and 3.0, and your key is 2.0, the version 1.0 is matched.
    /// </summary>
    public VersionMap(params IPair<Version, Value>[] pairs) : this(false, pairs)
    { }

    /// <summary>
    /// A dictionary which matches a version.
    /// It matches the version range, not the exact version.
    /// This means if you have two pairs inside: 1.0 and 3.0, and your key is 2.0, the version 1.0 is matched.
    /// </summary>
    public VersionMap(bool openEnd, params IPair<Version, Value>[] pairs) : this(pairs.AsEnumerable(), openEnd)
    { }

    /// <summary>
    /// A dictionary which matches a version.
    /// It matches the version range, not the exact version.
    /// This means if you have two pairs inside: 1.0 and 3.0, and your key is 2.0, the version 1.0 is matched.
    /// </summary>
    public VersionMap(bool openEnd, params (Version version, Value value)[] pairs) : this(
        pairs.AsMapped(p => p.AsPair()), openEnd
    )
    { }

    public Value this[Version key] => this.Match(key);

    public ICollection<Version> Keys() => this.map.Keys();

    public Func<Value> Lazy(Version version) =>
        () => this.Match(version);

    public IEnumerable<IPair<Version, Value>> Pairs() =>
        this.map.Pairs();

    public IMap<Version, Value> With(IPair<Version, Value> pair) =>
        new VersionMap<Value>(this.map.With(pair).Pairs(), openEnd);

    private Value Match(Version candidate)
    {
        var versions = this.map.Keys();
        var prettyCandidate = new Version(
            candidate.Major,
            candidate.Minor,
            candidate.Build == -1 ? 0 : candidate.Build,
            candidate.Revision == -1 ? 0 : candidate.Revision
        );
        var match = new Version(0, 0);
        var matched = false;
        foreach (var lowerBound in versions)
        {
            if (prettyCandidate >= lowerBound)
            {
                match = lowerBound;
                matched = true;
            }
            else if (match < prettyCandidate)
            {
                break;
            }
        }

        if (matched)
        {
            if (openEnd || versions.AsList().IndexOf(match) < versions.Count - 1)
            {
                return this.map[match];
            }
            else
            {
                throw this.versionNotFound(prettyCandidate, versions);
            }
        }
        throw this.versionNotFound(prettyCandidate, this.map.Keys());
    }
}

public static partial class MapSmarts
{
    /// <summary>
    /// A map which matches a version.
    /// It matches the version range, not the exact version.
    /// This means if you have two pairs inside: 1.0 and 3.0, and your key is 2.0, the version 1.0 is matched.
    /// </summary>
    public static IMap<Version,Value> AsVersionMap<Value>(this IEnumerable<IPair<Version, Value>> pairs, bool openEnd = true) =>
        new VersionMap<Value>(pairs, openEnd);

    /// <summary>
    /// A map which matches a version.
    /// It matches the version range, not the exact version.
    /// This means if you have two pairs inside: 1.0 and 3.0, and your key is 2.0, the version 1.0 is matched.
    /// </summary>
    public static IMap<Version,Value> AsVersionMap<Value>(this IPair<Version, Value>[] pairs, bool openEnd = true) =>
        new VersionMap<Value>(pairs, openEnd);

    /// <summary>
    /// A map which matches a version.
    /// It matches the version range, not the exact version.
    /// This means if you have two pairs inside: 1.0 and 3.0, and your key is 2.0, the version 1.0 is matched.
    /// </summary>
    public static IMap<Version,Value> AsVersionMap<Value>(this (Version version, Value value) pair, bool openEnd = true) =>
        new VersionMap<Value>(openEnd, pair);

    /// <summary>
    /// A map which matches a version.
    /// It matches the version range, not the exact version.
    /// This means if you have two pairs inside: 1.0 and 3.0, and your key is 2.0, the version 1.0 is matched.
    /// </summary>
    public static IMap<Version,Value> AsVersionMap<Value>(this
        (
            (Version ver,Value v) p1,
            (Version ver,Value v) p2
        ) pairs,
        bool openEnd = true
    ) => new VersionMap<Value>(openEnd, pairs.p1, pairs.p2);

    /// <summary>
    /// A map which matches a version.
    /// It matches the version range, not the exact version.
    /// This means if you have two pairs inside: 1.0 and 3.0, and your key is 2.0, the version 1.0 is matched.
    /// </summary>
    public static IMap<Version,Value> AsVersionMap<Value>(this
        (
            (Version ver,Value v) p1,
            (Version ver,Value v) p2,
            (Version ver,Value v) p3
        ) pairs,
        bool openEnd = true
    ) => new VersionMap<Value>(openEnd, pairs.p1, pairs.p2, pairs.p3);

    /// <summary>
    /// A map which matches a version.
    /// It matches the version range, not the exact version.
    /// This means if you have two pairs inside: 1.0 and 3.0, and your key is 2.0, the version 1.0 is matched.
    /// </summary>
    public static IMap<Version,Value> AsVersionMap<Value>(this
        (
            (Version ver,Value v) p1,
            (Version ver,Value v) p2,
            (Version ver,Value v) p3,
            (Version ver,Value v) p4
        ) pairs,
        bool openEnd = true
    ) => new VersionMap<Value>(openEnd, pairs.p1, pairs.p2, pairs.p3, pairs.p4);

    /// <summary>
    /// A map which matches a version.
    /// It matches the version range, not the exact version.
    /// This means if you have two pairs inside: 1.0 and 3.0, and your key is 2.0, the version 1.0 is matched.
    /// </summary>
    public static IMap<Version,Value> AsVersionMap<Value>(this
        (
            (Version ver,Value v) p1,
            (Version ver,Value v) p2,
            (Version ver,Value v) p3,
            (Version ver,Value v) p4,
            (Version ver,Value v) p5
        ) pairs,
        bool openEnd = true
    ) => new VersionMap<Value>(openEnd, pairs.p1, pairs.p2, pairs.p3, pairs.p4, pairs.p5);

    /// <summary>
    /// A map which matches a version.
    /// It matches the version range, not the exact version.
    /// This means if you have two pairs inside: 1.0 and 3.0, and your key is 2.0, the version 1.0 is matched.
    /// </summary>
    public static IMap<Version,Value> AsVersionMap<Value>(this
        (
            (Version ver,Value v) p1,
            (Version ver,Value v) p2,
            (Version ver,Value v) p3,
            (Version ver,Value v) p4,
            (Version ver,Value v) p5,
            (Version ver,Value v) p6
        ) pairs,
        bool openEnd = true
    ) => new VersionMap<Value>(openEnd, pairs.p1, pairs.p2, pairs.p3, pairs.p4, pairs.p5, pairs.p6);

    /// <summary>
    /// A map which matches a version.
    /// It matches the version range, not the exact version.
    /// This means if you have two pairs inside: 1.0 and 3.0, and your key is 2.0, the version 1.0 is matched.
    /// </summary>
    public static IMap<Version,Value> AsVersionMap<Value>(this
        (
            (Version ver,Value v) p1,
            (Version ver,Value v) p2,
            (Version ver,Value v) p3,
            (Version ver,Value v) p4,
            (Version ver,Value v) p5,
            (Version ver,Value v) p6,
            (Version ver,Value v) p7
        ) pairs,
        bool openEnd = true
    ) => new VersionMap<Value>(openEnd, pairs.p1, pairs.p2, pairs.p3, pairs.p4, pairs.p5, pairs.p6 ,pairs.p7);

    /// <summary>
    /// A map which matches a version.
    /// It matches the version range, not the exact version.
    /// This means if you have two pairs inside: 1.0 and 3.0, and your key is 2.0, the version 1.0 is matched.
    /// </summary>
    public static IMap<Version,Value> AsVersionMap<Value>(this
        (
            (Version ver,Value v) p1,
            (Version ver,Value v) p2,
            (Version ver,Value v) p3,
            (Version ver,Value v) p4,
            (Version ver,Value v) p5,
            (Version ver,Value v) p6,
            (Version ver,Value v) p7,
            (Version ver,Value v) p8
        ) pairs,
        bool openEnd = true
    ) => new VersionMap<Value>(openEnd, pairs.p1, pairs.p2, pairs.p3, pairs.p4, pairs.p5, pairs.p6 ,pairs.p7, pairs.p8);

    /// <summary>
    /// A map which matches a version.
    /// It matches the version range, not the exact version.
    /// This means if you have two pairs inside: 1.0 and 3.0, and your key is 2.0, the version 1.0 is matched.
    /// </summary>
    public static IMap<Version,Value> AsVersionMap<Value>(this
        (IPair<Version,Value> p1, IPair<Version,Value> p2) pairs,
        bool openEnd = true
    ) => new VersionMap<Value>(openEnd, pairs.p1, pairs.p2);

    /// <summary>
    /// A map which matches a version.
    /// It matches the version range, not the exact version.
    /// This means if you have two pairs inside: 1.0 and 3.0, and your key is 2.0, the version 1.0 is matched.
    /// </summary>
    public static IMap<Version,Value> AsVersionMap<Value>(this
            (IPair<Version,Value> p1, IPair<Version,Value> p2, IPair<Version,Value> p3) pairs,
        bool openEnd = true
    ) => new VersionMap<Value>(openEnd, pairs.p1, pairs.p2, pairs.p3);

    /// <summary>
    /// A map which matches a version.
    /// It matches the version range, not the exact version.
    /// This means if you have two pairs inside: 1.0 and 3.0, and your key is 2.0, the version 1.0 is matched.
    /// </summary>
    public static IMap<Version,Value> AsVersionMap<Value>(this
            (IPair<Version,Value> p1, IPair<Version,Value> p2, IPair<Version,Value> p3, IPair<Version,Value> p4) pairs,
        bool openEnd = true
    ) => new VersionMap<Value>(openEnd, pairs.p1, pairs.p2, pairs.p3, pairs.p4);

    /// <summary>
    /// A map which matches a version.
    /// It matches the version range, not the exact version.
    /// This means if you have two pairs inside: 1.0 and 3.0, and your key is 2.0, the version 1.0 is matched.
    /// </summary>
    public static IMap<Version,Value> AsVersionMap<Value>(this
            (IPair<Version,Value> p1, IPair<Version,Value> p2, IPair<Version,Value> p3, IPair<Version,Value> p4,
            IPair<Version,Value> p5) pairs,
        bool openEnd = true
    ) => new VersionMap<Value>(openEnd, pairs.p1, pairs.p2, pairs.p3, pairs.p4, pairs.p5);

    /// <summary>
    /// A map which matches a version.
    /// It matches the version range, not the exact version.
    /// This means if you have two pairs inside: 1.0 and 3.0, and your key is 2.0, the version 1.0 is matched.
    /// </summary>
    public static IMap<Version,Value> AsVersionMap<Value>(this
            (IPair<Version,Value> p1, IPair<Version,Value> p2, IPair<Version,Value> p3, IPair<Version,Value> p4,
            IPair<Version,Value> p5, IPair<Version,Value> p6) pairs,
        bool openEnd = true
    ) => new VersionMap<Value>(openEnd, pairs.p1, pairs.p2, pairs.p3, pairs.p4, pairs.p5, pairs.p6);

    /// <summary>
    /// A map which matches a version.
    /// It matches the version range, not the exact version.
    /// This means if you have two pairs inside: 1.0 and 3.0, and your key is 2.0, the version 1.0 is matched.
    /// </summary>
    public static IMap<Version,Value> AsVersionMap<Value>(this
            (IPair<Version,Value> p1, IPair<Version,Value> p2, IPair<Version,Value> p3, IPair<Version,Value> p4,
            IPair<Version,Value> p5, IPair<Version,Value> p6, IPair<Version,Value> p7) pairs,
        bool openEnd = true
    ) => new VersionMap<Value>(openEnd, pairs.p1, pairs.p2, pairs.p3, pairs.p4, pairs.p5, pairs.p6 ,pairs.p7);

    /// <summary>
    /// A map which matches a version.
    /// It matches the version range, not the exact version.
    /// This means if you have two pairs inside: 1.0 and 3.0, and your key is 2.0, the version 1.0 is matched.
    /// </summary>
    public static IMap<Version,Value> AsVersionMap<Value>(this
            (IPair<Version,Value> p1, IPair<Version,Value> p2, IPair<Version,Value> p3, IPair<Version,Value> p4,
            IPair<Version,Value> p5, IPair<Version,Value> p6, IPair<Version,Value> p7, IPair<Version,Value> p8) pairs,
        bool openEnd = true
    ) => new VersionMap<Value>(openEnd, pairs.p1, pairs.p2, pairs.p3, pairs.p4, pairs.p5, pairs.p6 ,pairs.p7, pairs.p8);
}
