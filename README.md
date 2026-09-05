[![EO principles respected here](http://www.elegantobjects.org/badge.svg)](http://www.elegantobjects.org)
[![NuGet](https://img.shields.io/nuget/v/Tonga.svg)](https://www.nuget.org/packages/Tonga)

# Tonga

Object-oriented primitives for .NET. Nachfolger von [Yaapii.Atoms](https://github.com/icarus-consulting/Yaapii.Atoms), in der Linie von [Cactoos](https://github.com/yegor256/cactoos). Folgt den Regeln der beiden [Elegant Objects](http://www.elegantobjects.org)-Bände.

```
dotnet add package Tonga
```

Ziel: `net9.0`.

## Prinzip

Jede Operation ist ein Objekt. Objekte werden durch Dekoration zusammengesetzt. Ein Ergebnis entsteht bei der Abfrage.

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
| `Count() >= n` | `HasAtLeast` | bricht ab, sobald entschieden |
| `Count() > n` | `HasMoreThan` | bricht ab, sobald entschieden |
| `Count() < n` | `HasLessThan` | bricht ab, sobald entschieden |
| `DefaultIfEmpty` | `AsBackFalling` | nimmt eine Ersatzquelle, keinen Ersatzwert |
| `ToList` | `AsList` / `AsSticky` | |
| `ToDictionary` | `AsDictionary` | |
| — | `AsCycled`, `AsEndless`, `AsRepeated` | |
| — | `AsReplaced` | Elemente ersetzen, die eine Bedingung erfüllen |
| — | `new Divergency<T>(…)` | symmetrische Differenz |
| — | `Sibling` | Nachbar eines Elements |
| — | `OnEach` | Lambda beim Weiterrücken |

`AsSingle` konstruiert eine einelementige Sequenz. Es entspricht nicht LINQs `Single()`.

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

LINQ-Methoden liefern Werte. Tonga-Objekte liefern Objekte, die den Wert bei der Abfrage herstellen. `AsFiltered` gibt eine Sequenz zurück, die beim Enumerieren filtert. Der Rückgabetyp von `Length` ist `IScalar<long>`. Die Kette bleibt bis zum abschließenden `.Value()` oder `.Str()` unausgewertet und ist an jeder Stelle weiter dekorierbar.

### Namenskonflikt mit System.Linq

`Max` und `Min` existieren in Tonga und in `System.Linq` mit derselben Signatur auf `IEnumerable<T>`. Stehen beide `using` im selben File, ist der Aufruf mehrdeutig (CS0121). Entweder das LINQ-`using` weglassen oder die Klasse direkt bauen: `new Max<int>(items)`.

## Auswertung ohne Default-Caching

Yaapii.Atoms puffert seit Version 2.0 standardmäßig: Envelopes haben `live: false` als Vorgabe und wickeln sich selbst in ein `Sticky`. Der Puffer ist eine `List<T>` plus Lock plus Ende-Flag — pro Dekorator.

Eine Kette aus vier Dekoratoren legt damit vier vollständige Kopien der Daten an, jede mit eigenem Lock. Für das Ergebnis genügt eine Materialisierung am Ende. Der Aufwand wächst linear mit der Dekoratortiefe. Bei einmaligem Durchlauf werden alle Puffer angelegt und keiner ein zweites Mal gelesen.

Tonga wertet daher lazy aus. `EnumerableEnvelope` reicht durch und allokiert nichts. Ein Puffer wird an der Stelle gesetzt, an der er gebraucht wird:

```csharp
var names =
    people
        .AsMapped(p => p.Name)
        .AsFiltered(n => n.Length > 3)
        .AsSticky();          // ein Puffer, an der Stelle, wo mehrfach gelesen wird
```

Das Zugriffsmuster ist nur an der Aufrufstelle bekannt. Ob ein Zwischenergebnis einmal oder mehrfach gelesen wird, kann die Bibliothek nicht bestimmen, deshalb liegt die Entscheidung beim Aufrufer.

**Beim Portieren von Atoms beachten:** Objekte, die dort gepuffert waren, rechnen hier bei jedem Durchlauf neu. Wo eine Sequenz mehrfach enumeriert wird, gehört ein `AsSticky` hin.

## Fluent-API und EO-Prinzipien

Grundannahme: eine Extension, die wrappt, ist ein Konstruktoraufruf ohne `new`. Zur Laufzeit besteht sie aus einer Allokation und einem Aufruf, wie die verschachtelte Form auch. Es entsteht kein zusätzliches Objekt, keine Indirektion und keine Kopie.

```csharp
public static IEnumerable<Out> AsMapped<In, Out>(this IEnumerable<In> src, Func<In, Out> fnc) =>
    new Mapped<In, Out>(fnc, src);
```

Damit gilt für Extensions dieselbe Regel wie für Konstruktoren in EO: **nur wrappen, keine Code-Ausführung.** Der Rumpf enthält einen `new`-Aufruf und sonst nichts — keine Bedingung, keine Schleife, keine Berechnung, keinen Zustand.

Aus dieser Regel folgt:

- **Kein verstecktes Verhalten.** Was `AsMapped` tut, steht in `Mapped`. Die Extension fügt nichts hinzu.
- **Nichts wird vorweggenommen.** Der Aufruf allokiert das Objekt; die Auswertung erfolgt weiterhin erst bei der Abfrage.
- **Keine Kopplung.** `new Mapped<…>(…)` bleibt gleichwertig möglich. Die Tests in diesem Repo verwenden beide Formen.
- **Nichts zu überschreiben.** Die Extension entscheidet nichts, deshalb gibt es kein Verhalten, das Vererbung betreffen könnte.

EO verbietet statische Methoden, weil sie Verhalten tragen, das keinem Objekt gehört: Logik ohne Zustand und ohne Identität, die sich nicht ersetzen, dekorieren oder durch Austausch testen lässt. Eine Extension unter der obigen Regel trägt kein Verhalten. Das Verhalten liegt in `Mapped<In, Out>`, einer dekorierbaren und ersetzbaren Klasse.

Der Unterschied zur verschachtelten Form betrifft Lesereihenfolge und Typinferenz. Die verschachtelte Form wird von innen nach außen gelesen und führt den letzten Schritt zuerst auf; Typargumente sind auszuschreiben, da Konstruktoren sie nicht herleiten. Die Kettenform folgt der Ausführungsreihenfolge und leitet die Typen ab.

### Regel für neue Smarts

Ein Rumpf besteht aus `new X(…)`. Alles, was darüber hinausgeht, gehört in die Klasse. Eine Extension darf einen anderen Wrapper aufrufen, solange auch dieser die Regel einhält — `AsScalars` setzt sich so aus `AsMapped` und `AsScalar` zusammen.

Ein fehlendes `new` bleibt beim Kompilieren unbemerkt: `AsStream(this byte[] bytes) => AsStream(bytes)` rief sich selbst auf und lief in eine Endlosrekursion.

## Keine Fail-Objekte

Yaapii.Atoms hat einen `Error`-Namespace mit `IFail`:

```csharp
public interface IFail { void Go(); }
```

Dazu `FailNull`, `FailWhen`, `FailZero`, `FailPrecise` und weitere. Tonga enthält diese Objekte aus drei Gründen nicht:

**Sie sind Prozeduren.** Die einzige Methode gibt `void` zurück. Ein Objekt, dessen Zweck ein Seiteneffekt ist, hat kein abfragbares Verhalten, sondern nur eine Wirkung. Das entspricht einer Prozedur in Klassensyntax.

**Sie sind nach Tätigkeiten benannt.** `FailWhen`, `FailNull` sind Imperative. EO benennt Objekte danach, was sie sind, nicht danach, was sie tun.

**Sie stehen neben dem Wert, den sie schützen.** Ein `FailNull` wird konstruiert, `Go()` wird gerufen, danach arbeitet der Code mit dem ursprünglichen Objekt weiter. Die Prüfung ist ein separater Schritt und kann ausgelassen werden. `FailPrecise` bildet zusätzlich Kontrollfluss als Objekt ab:

```csharp
public void Go()
{
    try { _origin.Go(); }
    catch (Exception) { throw _precision; }
}
```

In Tonga ist eine Prüfung ein Dekorator um den geprüften Wert. Der Dekorator gibt den Wert zurück und bleibt Teil der Kette:

```csharp
public sealed class AssertNotEmpty<T>(IEnumerable<T> origin, Exception ex) : IEnumerable<T>
```

```csharp
items.AssertNotEmpty().AsMapped(…)                 // prüft beim Enumerieren
new NullRejecting<string>(value).Value()           // prüft beim Auswerten
text.AsStrict("red", "green", "blue").Str()        // prüft gegen erlaubte Werte
```

Da die Prüfung Teil des Objekts ist, kann das Objekt nicht ohne sie verwendet werden. Für Bedingungen ohne zugehörigen Wert dient `IFact` mit `Check`:

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
