using Tonga.Enumerable;
using Xunit;

namespace Tonga.Tests.Enumerable;
public sealed class FilteredTests
{
    [Fact]
    public void Filters()
    {
        Assert.Equal(
            2,
            new []{ "A", "B", "C" }
                .AsEnumerable()
                .AsFiltered(input => input != "B")
                .Length()
                .Value()
        );
    }

    [Fact]
    public void SensesChanges()
    {
        var filterings = 0;
        var filtered =
            new[]{ "A", "B", "C" }.AsEnumerable()
                .AsFiltered(
                    input =>
                    {
                        filterings++;
                        return input != "B";
                    }
                );

        var enm1 = filtered.GetEnumerator();
        enm1.MoveNext();
        _ = enm1.Current;

        var enm2 = filtered.GetEnumerator();
        enm2.MoveNext();
        _ = enm2.Current;

        Assert.Equal(2, filterings);
    }

    [Fact]
    public void FiltersEmptyList()
    {
        Assert.Empty(
            new string[0]
                .AsEnumerable()
                .AsFiltered(input => input.Length > 1)
        );
    }

    [Fact]
    public void FiltersItemsGivenByParamsCtor()
    {
        Assert.Equal(
            2,
            new[]{"A", "B", "C"}
                .AsEnumerable()
                .AsFiltered(input => input != "B")
                .Length()
                .Value()
        );
    }
}
