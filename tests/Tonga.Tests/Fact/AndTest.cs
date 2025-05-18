using System.Collections.Generic;
using Tonga.Enumerable;
using Tonga.Fact;
using Xunit;

#pragma warning disable MaxPublicMethodCount // a public methods count maximum
namespace Tonga.Tests.Fact
{
    public sealed class AndTest
    {
        [Fact]
        public void AllTrue()
        {
            Assert.True(
                new And(
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
                new And(
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
                new And(
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
                new And(new None<IFact>())
                    .IsTrue()
            );
        }

        [Fact]
        public void ValidatesBooleansToTrue()
        {
            Assert.True(new And(true, true, true).IsTrue());
        }

        [Fact]
        public void ValidatesBooleansToFalse()
        {
            Assert.False(new And(new List<bool> { true, false, true }).IsTrue());
        }
    }
}
#pragma warning restore MaxPublicMethodCount // a public methods count maximum
