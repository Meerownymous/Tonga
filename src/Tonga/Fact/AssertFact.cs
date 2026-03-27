using Xunit;

namespace Tonga.Fact;

public static class AssertFact
{
    public static void True(IFact fact) => Assert.True(fact.IsTrue());

    public static void False(IFact fact) => Assert.True(fact.IsFalse());
}
