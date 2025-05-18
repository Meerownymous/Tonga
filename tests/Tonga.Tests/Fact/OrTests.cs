using System.Collections.Generic;
using Tonga.Enumerable;
using Tonga.Fact;
using Tonga.Func;
using Xunit;

namespace Tonga.Tests.Fact
{
    public sealed class OrTests
    {
        [Fact]
        public void TrueOrGenEnumerable()
        {
            Assert.True(
                new Or(
                    AsEnumerable._<IFact>(
                        new True(),
                        new False()
                    )
                ).IsTrue()
            );
        }

        [Fact]
        public void TrueOrGenScalar()
        {
            Assert.True(
                new Or(
                    new True(),
                    new False(),
                    new True()
                ).IsTrue()
            );
        }

        [Fact]
        public void InputBoolValuesToTrue()
        {
            Assert.True(
                new Or(false, true, false).IsTrue()
            );
        }

        [Fact]
        public void InputBoolValuesToFalse()
        {
            Assert.False(
                new Or(
                    new List<bool> { false, false, false }
                ).IsTrue()
            );
        }

        [Fact]
        public void InputBoolFunctionsToTrue()
        {
            Assert.True(new Or(() => true, () => false).IsTrue());
        }
    }
}
