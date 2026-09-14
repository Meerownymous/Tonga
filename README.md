[![EO principles respected here](http://www.elegantobjects.org/badge.svg)](http://www.elegantobjects.org)
[![NuGet](https://img.shields.io/nuget/v/Tonga.svg)](https://www.nuget.org/packages/Tonga)

# Tonga

Object-oriented primitives for .NET, following the rules of both [Elegant Objects](http://www.elegantobjects.org) volumes.

Tonga is a fork of [Yaapii.Atoms](https://github.com/icarus-consulting/Yaapii.Atoms), and both of them port [Cactoos](https://github.com/yegor256/cactoos) by Yegor Bugayenko from Java to .NET. Tonga carries that further. It builds on `System.Func`, extension methods, tuples and primary constructors, and it changes evaluation, checks and the call form. [Differences to Yaapii.Atoms](#differences-to-yaapiiatoms) lists what moved.

```
dotnet add package Tonga
```

You need `net9.0`.

## Why Tonga

```csharp
people
    .AsFiltered(p => p.Age >= 18)
    .AsMapped(p => p.Name)
    .AsSorted()
    .AsJoined(", ")
    .Str();
```

You read that like any other fluent chain in .NET. Underneath it there is no pipeline, no query engine and no builder. Every call is a constructor, and every step is an object you can name and hold on to:

```csharp
var adults = people.AsFiltered(p => p.Age >= 18);   // a Filtered<Person>
var names  = adults.AsMapped(p => p.Name);          // a Mapped<Person, string>
```

Here is what the chain gives you, and what a fluent API usually takes away:

- **Wrap any step.** `AsSticky`, `RetryOnError`, `BackFalling` or `ExceptionSwap` go around any link. The link stays as it is and the rest of the chain does not notice.
- **Replace any step.** Each link sits behind an interface with one method. Your test double for an `IText` is a class that returns a string. No mocking framework, no setup.
- **Extend with your own types.** `MyConfig : MapEnvelope` is accepted wherever an `IMap` is, and it composes with everything here.
- **Nothing runs until you ask.** Building the chain allocates and computes nothing. The work starts at `Str()`.

### When it does not fit

Measured as a utility library, LINQ and the BCL win on reach, tooling, framework coverage and runtime optimization. What you gain here shows up in the shape of the surrounding code, while the operations themselves stay ordinary.

So one question decides it for you: should your domain consist of objects that you decorate, or of data that you pass through functions? Answer with the second one and everything here is in your way.

Three more things to know before you adopt it: `net9.0` only, no concurrency guards ([What is missing](#what-is-missing)), and a 0.x version, so names still move between releases.

## Principle

An object is the result of a behaviour. `Upper` is uppercase text. `Filtered` is a filtered sequence. `Maximum` is the greatest item of a sequence. Each name tells you what the object is. You compose objects by decorating them, and the result appears when you ask for it.

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

Every link of that chain is a class, and you can construct it yourself:

```csharp
new ItemAt<IText>(
    new Mapped<string, IText>(
        word => new Upper(new AsText(word)),
        new AsEnumerable<string>("hello", "world", "damn")
    ),
    0
).Value().Str();
```

Both forms create the same objects. We call the extensions `…Smarts` (`EnumerableSmarts`, `TextSmarts`, `IOSmarts`, …), and they arrive with the `using` of their namespace.

## When code runs

**Building objects runs nothing. Code runs when you make a materializing call.** Every type has one, named after what it hands back:

| Type | Materializes with |
|---|---|
| `IText` | `Str()` |
| `IScalar<T>` | `Value()` |
| `IBytes` | `Raw()` |
| `INumber` | `Int()`, `Long()`, `Double()`, `Float()` |
| `IFact` | `IsTrue()`, `IsFalse()` |
| `IConduit` | `Stream()` |
| `IOptional<T>` | `Value()` |
| `IPair<K,V>` | `Value()` |
| `IEnumerable<T>` | `foreach`, `GetEnumerator()` |

```csharp
var text =
    new Uri("https://example.org/data.txt")
        .AsConduit()
        .AsText();          // nothing requested, nothing read

var content = text.Str();   // the request happens here
```

Composition costs you close to nothing. Each step in a chain is one allocation that holds a reference to the step before it. Build ten decorators and never materialize them, and you paid ten allocations for no work. Build a chain in a branch that turns out to be unused, and the allocations are the whole bill.

**The `As` prefix marks composition.** A call named `As…` wraps and returns, and it computes nothing:

```csharp
text.AsUpper()              // an uppercase text — nothing uppercased yet
items.AsFiltered(…)         // a filtered sequence — nothing tested yet
items.AsSorted()            // a sorted sequence — nothing compared yet
conduit.AsText()            // a text over a stream — nothing read yet
items.AsSticky()            // a buffered sequence — the buffer is still empty
```

Calls without the prefix hand you a different abstraction and defer in the same way. `items.Length()` is an `IScalar<long>` that counts on `Value()`. `items.Contains(…)` is an `IFact` that searches on `IsTrue()`:

```csharp
var count = items.Length();   // nothing counted
count.Value();                // counted here
```

**Enumerables** work per item wherever the operation allows it. `AsMapped` maps the current item while `MoveNext` advances, and `AsFiltered` tests it there:

```csharp
var names =
    people
        .AsMapped(p => p.Name)          // p.Name not read yet
        .AsFiltered(n => n.Length > 3);

foreach (var name in names) { … }       // mapping and filtering run per item
```

So `AsHead(3)` reads three items, and `HasAtLeast(3)` stops after three.

A few operations need every item before they can hand out the first one. `AsSorted`, `AsSortedBy` and `AsReversed` copy the source into a list and sort or reverse it, and that work lands on the first step of the iteration:

```csharp
var sorted = items.AsSorted();   // nothing read, nothing compared

var e = sorted.GetEnumerator();
e.MoveNext();                    // the whole source is read and sorted here
```

They stay lazy in the sense that matters for composition, because building the chain still runs nothing. What you pay for is the first `MoveNext`, which costs you the whole sequence. `Maximum`, `Minimum`, `AsReduced` and `Length` behave the same way and drain the source when you call `Value()`.

**Maps are lazy.** Constructing one runs nothing. The first access builds the key index. A value you set up with a lambda runs when somebody asks for that key, and asking for one key leaves the others untouched:

```csharp
var config =
    new AsMap<string, string>(
        new AsPair<string, string>("host", () => "localhost"),
        new AsPair<string, string>("secret", () => ReadSecretFromVault())
    );

var host = config["host"];     // ReadSecretFromVault has not been called
```

`Keys()` builds the index and materializes no value. `Lazy(key)` hands you a `Func<Value>` and defers the lookup too.

**To keep a result, close the chain with `AsSticky`.** It buffers what it wraps and serves later reads from that buffer:

```csharp
var names =
    people
        .AsMapped(p => p.Name)
        .AsFiltered(n => n.Length > 3)
        .AsSticky();           // computed once, on first enumeration
```

You have `AsSticky` for enumerables, lists, maps and scalars. Put it at the end of a chain and the chain buffers once. Leave it out and every pass recomputes. [Evaluation without default caching](#evaluation-without-default-caching) says why we leave that to you.

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
| `Max()` | `Maximum` | |
| `Min()` | `Minimum` | |
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

Careful with `AsSingle`. It constructs a one-item sequence and has nothing to do with LINQ's `Single()`.

### Text

LINQ has no counterpart here, so the left column shows how you write it otherwise.

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
| `IPipe<In,Out>` | a transformation, including `Conditional` and `Mux` |
| `ITap` | a side effect |
| `IConduit` | a stream source, decorable through `TeeOnRead`, `GZipCompressing`, `LoggingOnReadConduit` |
| `IOptional` | a value that may be absent, without `null` |
| `IMap` | an immutable mapping with `With` and `Lazy` |
| `IScalar` | a value produced later — with `RetryOnError`, `BackFalling`, `ExceptionSwap` |

### Difference in return type

A LINQ method returns a value. A Tonga object returns an object that produces the value once you ask for it. `AsFiltered` returns a sequence that filters while you enumerate it. `Length` returns an `IScalar<long>`. The chain stays unevaluated until you call `.Value()` or `.Str()`, and up to that moment you can decorate it further.

## Evaluation without default caching

Buffer by default and you pay for it per decorator. Each envelope keeps its own `List<T>`, its own lock and its own end flag, so a chain of four allocates four complete copies of your data and four locks, where one buffer at the end would have done the job. The cost grows linearly with decorator depth, and on a single pass every one of those buffers is filled and then never read again.

So Tonga evaluates lazily. `EnumerableEnvelope` passes through and allocates nothing, and you place a buffer where you need one:

```csharp
var names =
    people
        .AsMapped(p => p.Name)
        .AsFiltered(n => n.Length > 3)
        .AsSticky();          // one buffer, where the data is read more than once
```

You are the only one who knows the access pattern. The library cannot tell whether you read an intermediate result once or ten times, so we leave that decision with you.

## Fluent API and EO principles

Start from this premise: an extension that wraps is a constructor call without `new`. At runtime it is one allocation and one call, exactly what the nested form costs. It creates no extra object, no indirection and no copy.

```csharp
public static IEnumerable<Out> AsMapped<In, Out>(this IEnumerable<In> src, Func<In, Out> fnc) =>
    new Mapped<In, Out>(fnc, src);
```

So the rule EO puts on constructors applies to extensions as well: **wrapping only, no code execution.** The body holds one `new` call and nothing else — no condition, no loop, no computation, no state.

That rule has four consequences:

- **No hidden behaviour.** Whatever `AsMapped` does, it does it in `Mapped`. The extension adds nothing.
- **Nothing is done ahead of time.** The call allocates the object, and evaluation still waits until you ask for the value.
- **No coupling.** `new Mapped<…>(…)` stays equally available. The tests in this repository use both forms.
- **Nothing to override.** The extension decides nothing, so inheritance has no behaviour to concern itself with.

EO forbids static methods because they carry behaviour that belongs to no object: logic without state and without identity, which you cannot replace, decorate or test by substitution. An extension under the rule above carries no behaviour at all. The behaviour lives in `Mapped<In, Out>`, a class you can decorate and replace.

What separates the two forms is reading order and type inference. You read the nested form from the inside out, and it puts the last step first. You also spell out the type arguments there, because constructors do not infer them. The chained form follows execution order and infers the types for you.

### Rule for new smarts

Write the body as `new X(…)`. Anything beyond that belongs in the class. You may call another wrapper as long as that one follows the rule too — that is how `AsScalars` is composed of `AsMapped` and `AsScalar`.

Watch out for a missing `new`, because the compiler stays quiet about it. `AsStream(this byte[] bytes) => AsStream(bytes)` called itself and ran into endless recursion.

## Checks are decorators

A check belongs to the value it checks, so we build it as a decorator around that value. It returns the value and stays part of the chain:

```csharp
public sealed class AssertNotEmpty<T>(IEnumerable<T> origin, Exception ex) : IEnumerable<T>
```

```csharp
items.AssertNotEmpty().AsMapped(…)                 // checks while enumerating
new NullRejecting<string>(value).Value()           // checks while evaluating
text.AsStrict("red", "green", "blue").Str()        // checks against allowed values
```

The check is part of the object, so you cannot use the object without it. For conditions with no value attached, take `IFact` with `Check`:

```csharp
new Check(
    (() => number > 0).AsFact(),
    (() => number < 100).AsFact()
).IsTrue();
```

We have no standalone check objects. An interface of the shape

```csharp
public interface IFail { void Go(); }
```

with implementations named `FailNull`, `FailWhen` or `FailZero`, the way Cactoos and its ports carry them, we rejected for three reasons.

**They are procedures.** The only method returns `void`. An object whose purpose is a side effect gives you no behaviour to query, only an effect. Call that a procedure in class syntax.

**They are named after activities.** `FailWhen` and `FailNull` are imperatives. EO names an object for what it is.

**They stand beside the value they guard.** You construct such an object, you call `Go()`, and afterwards you continue with the original value. The check is a separate step, and somebody will leave it out.

## One stream interface

Cactoos has `Input` and `Output` because Java splits streams into two hierarchies, `InputStream` and `OutputStream`. A Java type therefore states its direction.

.NET does not split them. `System.IO.Stream` is one class covering both directions, and what a given stream permits is a runtime property: `CanRead`, `CanWrite`, `CanSeek`. Read-only streams exist — `File.OpenRead`, `new MemoryStream(buffer, writable: false)` — and they are the same type with `CanWrite` set to false.

Port the two Java interfaces to .NET and they collapse into the same declaration, with the same member and the same return type under a different name, and either of them hands you a `Stream` that may read, write or both. So Tonga has one interface:

```csharp
public interface IConduit { Stream Stream(); }
```

You read the direction from the stream, which is where .NET keeps it.

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
| Streams | `IInput` and `IOutput`, both declaring `Stream Stream()` | one `IConduit` |
| Checks | `IFail` in the `Error` namespace | decorators on the checked value |
| Predicates | `IScalar<bool>` | `IFact` |
| Call form | nested constructors | constructors or a `…Smarts` chain |
| Target | `netstandard2.0`, `net461` | `net9.0` |

Objects that Atoms buffered for you recompute on every pass here. So wherever you enumerate a sequence more than once, an `AsSticky` belongs.

Do not expect an automatic migration path. Names have changed (`AsString` → `Str`, `ManyOf` → `AsEnumerable`, `TextOf` → `AsText`, `First` → `FirstOne`, `None` → `Empty`), and the evaluation behaviour is inverted.

## What is missing

We have no synchronizing decorators. Objects in Tonga are not guarded for concurrent access, and `Sticky` is the only class that holds a lock. Share one between threads and the synchronization is yours to arrange outside.

## License

MIT. See [LICENSE](LICENSE).
