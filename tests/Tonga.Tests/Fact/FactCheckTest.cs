using Tonga.Enumerable;
using Tonga.Fact;
using Xunit;

#pragma warning disable MaxPublicMethodCount // a public methods count maximum
namespace Tonga.Tests.Fact
{
    public sealed class FactCheckTest
    {
        [Fact]
        public void AllTrue()
        {
            Assert.True(
                new FactCheck(
                    new True(),
                    new True(),
                    new True()
                ).IsTrue()
            );
        }

        [Fact]
        public void OneFalse()
        {
            Assert.True(
                new FactCheck(
                    new True(),
                    new False(),
                    new True()
                ).IsFalse()
            );
        }

        [Fact]
        public void AllFalse()
        {
            Assert.False(
                new FactCheck(
                    AsEnumerable._(
                        new False(),
                        new False(),
                        new False()
                    )
                ).IsTrue()
            );
        }

        [Fact]
        public void EmptyIterator()
        {
            Assert.True(
                new FactCheck(
                        new None<IFact>()
                )
                .IsTrue()
            );
        }
    }
}
#pragma warning restore MaxPublicMethodCount // a public methods count maximum
