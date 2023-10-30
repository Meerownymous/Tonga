

using System;
using Xunit;
using Tonga.Enumerable;
using Tonga.Scalar;

namespace Tonga.Func.Tests
{
    public class ChainedFuncTest
    {
        [Fact]
        public void WithoutIterable()
        {
            Assert.Equal(
                3,
                Length._(
                    Filtered._(
                        input => input.EndsWith("XY"),
                        Mapped._(
                            new ChainedFunc<String, String, String>(
                                input => input += "X",
                                input => input += "Y"
                            ),
                            AsEnumerable._("public", "final", "class")
                        ))
                ).Value()
            );
        }

        [Fact]
        public void WithIterable()
        {
            Assert.Equal(
                2,
                Length._(
                    Filtered._(
                        input => !input.StartsWith("st") && input.EndsWith("12"),
                         Enumerable.Mapped._(
                            new ChainedFunc<string, string, string>(
                                input => input += "1",
                                AsEnumerable._(
                                    new FuncOf<string, string>(input => input += ("2")),
                                    new FuncOf<string, string>(input => input.Replace("a", "b"))
                                ),
                                input => input.Trim()
                            ),
                            AsEnumerable._("private", "static", "String"))
                    )
                ).Value()
            );
        }
    }
}
