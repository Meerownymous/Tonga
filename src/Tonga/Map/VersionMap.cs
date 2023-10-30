

using System;
using System.Collections;
using System.Collections.Generic;
using Tonga.Enumerable;
using Tonga.List;

#pragma warning disable NoProperties // No Properties
#pragma warning disable MaxPublicMethodCount // a public methods count maximum

namespace Tonga.Map
{
    /// <summary>
    /// A dictionary which matches a version. 
    /// It matches the version range, not the exact version.
    /// This means if you have two pairs inside: 1.0 and 3.0, and your key is 2.0, the version 1.0 is matched.
    /// </summary>
    /// <typeparam name="Value"></typeparam>
    public sealed class VersionMap<Value> : IMap<Version, Value>
    {
        private readonly IMap<Version, Value> map;
        private readonly bool openEnd;
        private readonly Func<Version, IEnumerable<Version>, ArgumentException> versionNotFound =
            (version, available) =>
            new ArgumentException(
                $"Cannot find value for version {version}, the version must be within: "
                +
                Text.Joined._(", ",
                    Enumerable.Mapped._(
                        v => v.ToString(),
                        available
                    )
                ).AsString()
            );

        /// <summary>
        /// A dictionary which matches a version. 
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
        public VersionMap(bool openEnd, params IPair<Version, Value>[] pairs) : this(AsEnumerable._(pairs), openEnd)
        { }

        /// <summary>
        /// A dictionary which matches a version. 
        /// It matches the version range, not the exact version.
        /// This means if you have two pairs inside: 1.0 and 3.0, and your key is 2.0, the version 1.0 is matched.
        /// </summary>
        public VersionMap(IEnumerable<IPair<Version, Value>> pairs, bool openEnd)
        {
            this.map = AsMap._(pairs);
            this.openEnd = openEnd;
        }

        public Value this[Version key]
        {
            get => this.Match(key);
        }

        public ICollection<Version> Keys() => this.map.Keys();

        public Func<Value> Lazy(Version version)
        {
            return () => this.Match(version);
        }

        public IEnumerable<IPair<Version, Value>> Pairs() =>
            this.map.Pairs();

        public IMap<Version, Value> With(IPair<Version, Value> pair)
        {
            return VersionMap._(this.map.With(pair).Pairs(), this.openEnd);
        }

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
                if (this.openEnd || AsList._(versions).IndexOf(match) < versions.Count - 1)
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

    public static class VersionMap
    {
        public static VersionMap<Value> _<Value>(IEnumerable<IPair<Version,Value>> pairs, bool openEnd) =>
            new VersionMap<Value>(pairs, openEnd);

        public static VersionMap<Value> _<Value>(bool openEnd, params IPair<Version, Value>[] pairs) =>
            new VersionMap<Value>(pairs, openEnd);
    }
}
