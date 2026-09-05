[![EO principles respected here](http://www.elegantobjects.org/badge.svg)](http://www.elegantobjects.org)
[![NuGet](https://img.shields.io/nuget/v/Tonga.svg)](https://www.nuget.org/packages/Tonga)

# Tonga

Object-oriented primitives for .NET. Nachfolger von [Yaapii.Atoms](https://github.com/icarus-consulting/Yaapii.Atoms), in der Linie von [Cactoos](https://github.com/yegor256/cactoos). Folgt den Regeln der beiden [Elegant Objects](http://www.elegantobjects.org)-Bände.

```
dotnet add package Tonga
```

Ziel: `net9.0`.

## Prinzip

Jede Operation ist ein Objekt. Objekte werden dekoriert, nicht aufgerufen. Ein Ergebnis entsteht erst, wenn man danach fragt.

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

Jedes Glied der Kette ist eine Klasse, die man auch direkt bauen kann:

```csharp
new ItemAt<IText>(
    new Mapped<string, IText>(
        word => new Upper(new AsText(word)),
        new AsEnumerable<string>("hello", "world", "damn")
    ),
    0
).Value().Str();
```

Beide Formen erzeugen dieselben Objekte. Die Extensions heißen `…Smarts` (`EnumerableSmarts`, `TextSmarts`, `IOSmarts`, …) und kommen mit dem `using` des jeweiligen Namespaces.

## Gegen LINQ

### Enumerable

| LINQ | Tonga | Anmerkung |
|---|---|---|
| `Select` | `AsMapped` | Überladung mit Index vorhanden |
| `Where` | `AsFiltered` | |
| `OrderBy` | `AsSortedBy` | `AsSorted` sortiert ohne Schlüssel |
| `Take` | `AsHead` | |
| `Skip` | `AsSkipped` | |
| `Distinct` | `AsDistinct` | |
| `Concat` | `AsJoined` | verbindet beliebig viele |
| `Reverse` | `AsReversed` | |
| `Aggregate` | `AsReduced` | |
| `Intersect` | `AsIntersection` | |
| `Union` | `AsUnion` | |
| `Chunk` | `AsPartitioned` | |
| `ElementAt` | `ItemAt` | Überladung mit Fallback |
| `First` | `FirstOne` | Überladung mit Fallback |
| `Last` | `LastOne` | |
| `Count()` | `Length` | |
| `Any(x => …)` | `Contains` | liefert `IFact` |
| `!Any()` | `IsEmpty` | liefert `IFact` |
| `Count() > n` | `HasMoreThan` | bricht ab, sobald entschieden |
| `Count() < n` | `HasLessThan` | bricht ab, sobald entschieden |
| `DefaultIfEmpty` | `AsBackFalling` | Ersatzquelle statt Ersatzwert |
| `ToList` | `AsList` / `AsSticky` | |
| `ToDictionary` | `AsDictionary` | |
| — | `AsCycled`, `AsEndless`, `AsRepeated` | |
| — | `AsReplaced` | Elemente ersetzen, die eine Bedingung erfüllen |
| — | `new Divergency<T>(…)` | symmetrische Differenz |
| — | `Sibling` | Nachbar eines Elements |
| — | `OnEach` | Lambda beim Weiterrücken |

`AsSingle` ist die Konstruktion einer einelementigen Sequenz und hat mit `Single()` nichts zu tun.

### Text

LINQ hat hier keine Entsprechung; die Spalte zeigt das, was man sonst schreibt.

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

### Was LINQ nicht hat

| Abstraktion | Rolle |
|---|---|
| `IFact` | Aussage, die wahr oder falsch ist — komponierbar über `And`, `Or`, `Not` |
| `IPipe<In,Out>` | Transformation als Objekt, inklusive `Conditional` und `Mux` |
| `ITap` | Seiteneffekt als Objekt |
| `IConduit` | Stream-Quelle, dekorierbar über `TeeOnRead`, `GZipCompressing`, `LoggingOnReadConduit` |
| `IOptional` | Wert, der fehlen darf, ohne `null` |
| `IMap` | Unveränderliche Abbildung mit `With` und `Lazy` |
| `IScalar` | Wert, der später entsteht — mit `RetryOnError`, `BackFalling`, `ExceptionSwap` |

### Unterschied im Rückgabetyp

LINQ liefert Werte. Tonga liefert Objekte, die den Wert auf Anfrage herstellen. `AsFiltered` gibt kein gefiltertes Ergebnis zurück, sondern eine Sequenz, die beim Enumerieren filtert. `Length` gibt `IScalar<long>` zurück, nicht `long`. Damit bleibt die Kette bis zum letzten `.Value()` oder `.Str()` unausgewertet, und sie ist an jeder Stelle weiterdekorierbar.

### Wenn beides im selben File liegt

`Max` und `Min` existieren in Tonga und in `System.Linq` mit derselben Signatur auf `IEnumerable<T>`. Stehen beide `using` im selben File, ist der Aufruf mehrdeutig (CS0121). Entweder das LINQ-`using` weglassen oder die Klasse direkt bauen: `new Max<int>(items)`.

## Warum kein Caching per Default

Yaapii.Atoms puffert seit Version 2.0 standardmäßig: Envelopes haben `live: false` als Vorgabe und wickeln sich selbst in ein `Sticky`. Der Puffer ist eine `List<T>` plus Lock plus Ende-Flag — pro Dekorator.

Eine Kette aus vier Dekoratoren legt damit vier vollständige Kopien der Daten an, jede mit eigenem Lock, obwohl eine Materialisierung am Ende genügt. Der Aufwand wächst mit der Dekoratortiefe, also genau dort, wo objektorientierter Code tief wird. Wer die Kette nur einmal durchläuft, zahlt für Puffer, die nie ein zweites Mal gelesen werden.

Tonga wertet deshalb lazy aus. `EnumerableEnvelope` reicht durch und belegt nichts. Wo ein Puffer hingehört, setzt man ihn:

```csharp
var names =
    people
        .AsMapped(p => p.Name)
        .AsFiltered(n => n.Length > 3)
        .AsSticky();          // ein Puffer, an der Stelle, wo mehrfach gelesen wird
```

Die Entscheidung liegt damit dort, wo die Information über das Zugriffsmuster liegt: an der Aufrufstelle. Eine Bibliothek kann nicht wissen, ob ein Zwischenergebnis einmal oder zehnmal gelesen wird.

**Beim Portieren von Atoms beachten:** Objekte, die dort gepuffert waren, rechnen hier bei jedem Durchlauf neu. Wo eine Sequenz mehrfach enumeriert wird, gehört ein `AsSticky` hin.

## Warum die Fluent-API EO nicht verletzt

Der Einwand liegt nahe: Extension-Methoden sind statisch, und EO lehnt statische Methoden ab.

Der Grund für dieses Verbot ist, dass statische Methoden Verhalten tragen, das keinem Objekt gehört — Logik ohne Zustand, ohne Identität, nicht ersetzbar, nicht dekorierbar, nicht testbar durch Austausch. Genau das tun Tongas Extensions nicht. Jede besteht aus einer Zeile:

```csharp
public static IEnumerable<Out> AsMapped<In, Out>(this IEnumerable<In> src, Func<In, Out> fnc) =>
    new Mapped<In, Out>(fnc, src);
```

Sie enthält keine Logik, keine Bedingung, keinen Zustand. Sie ruft einen Konstruktor auf. Das Verhalten liegt vollständig in `Mapped<In, Out>`, einer normalen, dekorierbaren, ersetzbaren Klasse.

Damit ist die Extension Syntax, keine Implementierung:

- **Kein verstecktes Verhalten.** Was `AsMapped` tut, steht in `Mapped`. Die Extension fügt nichts hinzu.
- **Kein Zustand.** Nichts wird zwischen Aufrufen gehalten.
- **Keine Kopplung.** `new Mapped<…>(…)` bleibt jederzeit gleichwertig möglich; jeder Test in diesem Repo, der die Klasse direkt baut, beweist das.
- **Keine Vererbung von Verhalten.** Die Extension kann nicht überschrieben werden, weil sie nichts entscheidet.

Was sie leistet, ist Lesereihenfolge. Die verschachtelte Form zwingt zum Lesen von innen nach außen und stellt den letzten Schritt an den Anfang; die Typargumente muss man ausschreiben, weil Konstruktoren sie nicht herleiten. Die Kette liest sich in Ausführungsreihenfolge und leitet die Typen ab.

Das Prinzip, das EO schützt — Verhalten gehört in Objekte —, bleibt unberührt. Was sich ändert, ist die Art, wie man die Objekte hinschreibt.

## Warum es keine Fail-Objekte gibt

Yaapii.Atoms hat einen `Error`-Namespace mit `IFail`:

```csharp
public interface IFail { void Go(); }
```

Dazu `FailNull`, `FailWhen`, `FailZero`, `FailPrecise` und weitere. Diese Objekte sind in Tonga nicht enthalten, und zwar aus drei Gründen:

**Sie sind Prozeduren.** Die einzige Methode gibt `void` zurück. Ein Objekt, dessen gesamter Zweck ein Seiteneffekt ist, hat kein Verhalten, das man abfragen kann — es hat nur eine Wirkung. Das ist eine Prozedur in Klassensyntax.

**Sie sind nach Tätigkeiten benannt.** `FailWhen`, `FailNull` sind Imperative. EO benennt Objekte danach, was sie sind, nicht danach, was sie tun.

**Sie stehen neben dem Wert, den sie schützen.** Man baut ein `FailNull`, ruft `Go()` und arbeitet danach mit dem ursprünglichen Objekt weiter. Die Prüfung ist damit ein separater Schritt, den man vergessen kann, und `FailPrecise` macht daraus vollends Kontrollfluss als Objekt:

```csharp
public void Go()
{
    try { _origin.Go(); }
    catch (Exception) { throw _precision; }
}
```

In Tonga ist eine Prüfung ein Dekorator um den geprüften Wert. Sie gibt den Wert zurück und liegt im Fluss:

```csharp
public sealed class AssertNotEmpty<T>(IEnumerable<T> origin, Exception ex) : IEnumerable<T>
```

```csharp
items.AssertNotEmpty().AsMapped(…)                 // prüft beim Enumerieren
new NullRejecting<string>(value).Value()           // prüft beim Auswerten
text.AsStrict("red", "green", "blue").Str()        // prüft gegen erlaubte Werte
```

Das Objekt kann nicht mehr ohne seine Prüfung verwendet werden, weil die Prüfung Teil des Objekts ist. Für Bedingungen ohne Wert dahinter gibt es `IFact` mit `Check`:

```csharp
new Check(
    (() => number > 0).AsFact(),
    (() => number < 100).AsFact()
).IsTrue();
```

## Kernabstraktionen

| Interface | Methode | Namespace mit Implementierungen |
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

## Unterschiede zu Yaapii.Atoms

| | Yaapii.Atoms | Tonga |
|---|---|---|
| Caching | sticky per Default, `Live`-Dekoratoren | lazy per Default, `AsSticky` bei Bedarf |
| Funktionen | `IFunc`, `IBiFunc`, `IAction` | `System.Func` direkt |
| Stream-Richtung | `IInput` und `IOutput` | `IConduit` für beide |
| Fehlerprüfung | `IFail` im `Error`-Namespace | Dekoratoren am geprüften Wert |
| Prädikate | `IScalar<bool>` | `IFact` |
| Aufrufform | verschachtelte Konstruktoren | Konstruktoren oder `…Smarts`-Kette |
| Ziel | `netstandard2.0`, `net461` | `net9.0` |

Es gibt keinen automatischen Migrationspfad. Namen haben sich geändert (`AsString` → `Str`, `ManyOf` → `AsEnumerable`, `TextOf` → `AsText`, `First` → `FirstOne`, `None` → `Empty`), und das Auswertungsverhalten ist umgekehrt.

## Was fehlt

Es gibt keine Entsprechung zu `Sync`, `Solid`, `SyncList` oder `Synced` aus Atoms. Objekte in Tonga sind für nebenläufigen Zugriff nicht abgesichert; `Sticky` ist die einzige Klasse mit einem Lock. Für geteilten Zugriff aus mehreren Threads muss die Synchronisation außerhalb liegen.

## Lizenz

MIT. Siehe [LICENSE](LICENSE).
