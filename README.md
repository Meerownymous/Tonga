[![EO principles respected here](http://www.elegantobjects.org/badge.svg)](http://www.elegantobjects.org)
[![NuGet](https://img.shields.io/nuget/v/Tonga.svg)](https://www.nuget.org/packages/Tonga)

# Tonga

Object-oriented primitives for .NET. Successor of [Yaapii.Atoms](https://github.com/icarus-consulting/Yaapii.Atoms), in the line of [Cactoos](https://github.com/yegor256/cactoos). Follows the rules of both [Elegant Objects](http://www.elegantobjects.org) volumes.

```
dotnet add package Tonga
```

Target: `net9.0`.

## Principle

Every operation is an object. Objects are composed by decoration. A result is produced when it is asked for.

```csharp
using Tonga.Enumerable;
using Tonga.Text;

("hello", "world", "damn")
    .AsEnumerable()
    .AsMapped(word => word.AsText().AsUpper())
    .ItemAt(0)
    .Value()
    .Str();                                     // "HELLO"
```

Every link of the chain is a class and can be constructed directly:

```csharp
new ItemAt<IText>(
    new Mapped<string, IText>(
        word => new Upper(new AsText(word)),
        new AsEnumerable<string>("hello", "world", "damn")
    ),
    0
).Value().Str();
```

Both forms create the same objects. The extensions are named `…Smarts` (`EnumerableSmarts`, `TextSmarts`, `IOSmarts`, …) and arrive with the `using` of their namespace.

## Compared to LINQ

### Enumerable

| LINQ | Tonga | Note |
|---|---|---|
| `Select` | `AsMapped` | overload with index available |
| `Where` | `AsFiltered` | |
| `OrderBy` | `AsSortedBy` | `AsSorted` sorts without a key |
| `Take` | `AsHead` | |
| `Skip` | `AsSkipped` | |
| `Distinct` | `AsDistinct` | |
| `Concat` | `AsJoined` | joins any number of sources |
| `Reverse` | `AsReversed` | |
| `Aggregate` | `AsReduced` | |
| `Intersect` | `AsIntersection` | |
| `Union` | `AsUnion` | |
| `Chunk` | `AsPartitioned` | |
| `ElementAt` | `ItemAt` | overload with fallback |
| `First` | `FirstOne` | overload with fallback |
| `Last` | `LastOne` | |
| `Count()` | `Length` | |
| `Any(x => …)` | `Contains` | returns `IFact` |
| `!Any()` | `IsEmpty` | returns `IFact` |
| `Count() >= n` | `HasAtLeast` | stops as soon as the answer is known |
| `Count() > n` | `HasMoreThan` | stops as soon as the answer is known |
| `Count() < n` | `HasLessThan` | stops as soon as the answer is known |
| `DefaultIfEmpty` | `AsBackFalling` | takes a fallback source, not a fallback value |
| `ToList` | `AsList` / `AsSticky` | |
| `ToDictionary` | `AsDictionary` | |
| — | `AsCycled`, `AsEndless`, `AsRepeated` | |
| — | `AsReplaced` | replaces items matching a condition |
| — | `new Divergency<T>(…)` | symmetric difference |
| — | `Sibling` | neighbour of an item |
| — | `OnEach` | lambda invoked while advancing |

`AsSingle` constructs a one-item sequence. It is not the equivalent of LINQ's `Single()`.

### Text

LINQ has no counterpart here; the left column shows what is written otherwise.

| .NET | Tonga |
|---|---|
| `s.ToUpper()` | `AsUpper` |
| `s.Trim()` | `AsTrimmed` |
| `s.Split(…)` | `AsSplit` |
| `string.Join(…)` | `AsJoined` |
| `string.Format(…)` | `AsFormatted` |
| `s.Contains(…)` | `AsContains` → `IFact` |
| `s.StartsWith(…)` | `AsStartsWith` → `IFact` |
| `string.IsNullOrWhiteSpace` | `new IsBlank(…)` → `IFact` |
| `Convert.ToBase64String` | `AsBase64Encoded` |
| `s.Substring(…)` | `AsSubText` |

### What LINQ does not have

| Abstraction | Role |
|---|---|
| `IFact` | a statement that is true or false — composable through `And`, `Or`, `Not` |
| `IPipe<In,Out>` | a transformation as an object, including `Conditional` and `Mux` |
| `ITap` | a side effect as an object |
| `IConduit` | a stream source, decorable through `TeeOnRead`, `GZipCompressing`, `LoggingOnReadConduit` |
| `IOptional` | a value that may be absent, without `null` |
| `IMap` | an immutable mapping with `With` and `Lazy` |
| `IScalar` | a value produced later — with `RetryOnError`, `BackFalling`, `ExceptionSwap` |

### Difference in return type

LINQ methods return values. Tonga objects return objects that produce the value when it is asked for. `AsFiltered` returns a sequence that filters while being enumerated. The return type of `Length` is `IScalar<long>`. The chain stays unevaluated until the closing `.Value()` or `.Str()` and can be decorated further at any point.

### Name clash with System.Linq

`Max` and `Min` exist in Tonga and in `System.Linq` with the same signature on `IEnumerable<T>`. With both `using` directives in one file the call is ambiguous (CS0121). Either drop the LINQ `using` or construct the class directly: `new Max<int>(items)`.

## Evaluation without default caching

Yaapii.Atoms buffers by default since version 2.0: envelopes default to `live: false` and wrap themselves in a `Sticky`. The buffer is a `List<T>` plus a lock plus an end flag — per decorator.

A chain of four decorators therefore allocates four complete copies of the data, each with its own lock. One materialization at the end is enough for the result. The cost grows linearly with decorator depth. On a single pass all buffers are allocated and none is read a second time.

Tonga evaluates lazily. `EnumerableEnvelope` passes through and allocates nothing. A buffer is placed where it is needed:

```csharp
var names =
    people
        .AsMapped(p => p.Name)
        .AsFiltered(n => n.Length > 3)
        .AsSticky();          // one buffer, where the data is read more than once
```

The access pattern is known only at the call site. Whether an intermediate result is read once or several times cannot be determined by the library, so the decision belongs to the caller.

**When porting from Atoms:** objects that were buffered there recompute on every pass here. Wherever a sequence is enumerated more than once, an `AsSticky` belongs.

## Fluent API and EO principles

Premise: an extension that wraps is a constructor call without `new`. At runtime it consists of one allocation and one call, the same as the nested form. No additional object, no indirection and no copy is created.

```csharp
public static IEnumerable<Out> AsMapped<In, Out>(this IEnumerable<In> src, Func<In, Out> fnc) =>
    new Mapped<In, Out>(fnc, src);
```

The rule that EO puts on constructors therefore applies to extensions as well: **wrapping only, no code execution.** The body holds one `new` call and nothing else — no condition, no loop, no computation, no state.

The rule has these consequences:

- **No hidden behaviour.** What `AsMapped` does is in `Mapped`. The extension adds nothing.
- **Nothing is done ahead of time.** The call allocates the object; evaluation still happens when the value is asked for.
- **No coupling.** `new Mapped<…>(…)` remains equally available. The tests in this repository use both forms.
- **Nothing to override.** The extension decides nothing, so there is no behaviour that inheritance could concern.

EO forbids static methods because they carry behaviour that belongs to no object: logic without state and without identity, which cannot be replaced, decorated or tested by substitution. An extension under the rule above carries no behaviour. The behaviour lives in `Mapped<In, Out>`, a decorable and replaceable class.

The difference to the nested form concerns reading order and type inference. The nested form is read from the inside out and puts the last step first; type arguments have to be spelled out, since constructors do not infer them. The chained form follows execution order and infers the types.

### Rule for new smarts

A body consists of `new X(…)`. Anything beyond that belongs in the class. An extension may call another wrapper as long as that one follows the rule too — `AsScalars` is composed of `AsMapped` and `AsScalar` in this way.

A missing `new` goes unnoticed at compile time: `AsStream(this byte[] bytes) => AsStream(bytes)` called itself and ran into endless recursion.

## No fail objects

Yaapii.Atoms has an `Error` namespace built on `IFail`:

```csharp
public interface IFail { void Go(); }
```

Along with `FailNull`, `FailWhen`, `FailZero`, `FailPrecise` and others. Tonga omits these objects for three reasons:

**They are procedures.** The only method returns `void`. An object whose purpose is a side effect has no behaviour that can be queried, only an effect. That is a procedure in class syntax.

**They are named after activities.** `FailWhen`, `FailNull` are imperatives. EO names objects for what they are, not for what they do.

**They stand beside the value they guard.** A `FailNull` is constructed, `Go()` is called, and afterwards the code continues with the original object. The check is a separate step and can be left out. `FailPrecise` additionally models control flow as an object:

```csharp
public void Go()
{
    try { _origin.Go(); }
    catch (Exception) { throw _precision; }
}
```

In Tonga a check is a decorator around the value it checks. The decorator returns the value and stays part of the chain:

```csharp
public sealed class AssertNotEmpty<T>(IEnumerable<T> origin, Exception ex) : IEnumerable<T>
```

```csharp
items.AssertNotEmpty().AsMapped(…)                 // checks while enumerating
new NullRejecting<string>(value).Value()           // checks while evaluating
text.AsStrict("red", "green", "blue").Str()        // checks against allowed values
```

Since the check is part of the object, the object cannot be used without it. For conditions with no value attached there is `IFact` with `Check`:

```csharp
new Check(
    (() => number > 0).AsFact(),
    (() => number < 100).AsFact()
).IsTrue();
```

## Core abstractions

| Interface | Method | Namespace with implementations |
|---|---|---|
| `IText` | `Str()` | `Tonga.Text` |
| `IScalar<T>` | `Value()` | `Tonga.Scalar` |
| `IBytes` | `Raw()` | `Tonga.Bytes` |
| `INumber` | `Int()`, `Long()`, `Double()`, `Float()` | `Tonga.Number` |
| `IConduit` | `Stream()` | `Tonga.IO` |
| `IFact` | `IsTrue()`, `IsFalse()` | `Tonga.Fact` |
| `IPipe<In,Out>` | `Yield(In)` | `Tonga.Pipe` |
| `ITap` | `Trigger()` | `Tonga.Tap` |
| `IPair<K,V>` | `Key()`, `Value()` | `Tonga.Map` |
| `IMap<K,V>` | `this[K]`, `Keys()`, `Pairs()`, `With()` | `Tonga.Map` |
| `IOptional<T>` | `Has()`, `Value()`, `IfHas()`, `IfNot()` | `Tonga.Optional` |

## Differences to Yaapii.Atoms

| | Yaapii.Atoms | Tonga |
|---|---|---|
| Caching | sticky by default, `Live` decorators | lazy by default, `AsSticky` where needed |
| Functions | `IFunc`, `IBiFunc`, `IAction` | `System.Func` directly |
| Stream direction | `IInput` and `IOutput` | `IConduit` for both |
| Checks | `IFail` in the `Error` namespace | decorators on the checked value |
| Predicates | `IScalar<bool>` | `IFact` |
| Call form | nested constructors | constructors or a `…Smarts` chain |
| Target | `netstandard2.0`, `net461` | `net9.0` |

There is no automatic migration path. Names have changed (`AsString` → `Str`, `ManyOf` → `AsEnumerable`, `TextOf` → `AsText`, `First` → `FirstOne`, `None` → `Empty`), and the evaluation behaviour is inverted.

## What is missing

There is no counterpart to `Sync`, `Solid`, `SyncList` or `Synced` from Atoms. Objects in Tonga are not guarded for concurrent access; `Sticky` is the only class holding a lock. For shared access from several threads the synchronization has to live outside.

## License

MIT. See [LICENSE](LICENSE).
