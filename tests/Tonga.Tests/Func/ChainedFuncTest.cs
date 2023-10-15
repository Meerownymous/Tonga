

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
            Assert.True(
            new LengthOf(
                new Filtered<string>(
                    input => input.EndsWith("XY"),
                    new Enumerable.Mapped<string, string>(
                        new ChainedFunc<String, String, String>(
                            input => input += "X",
                            input => input += "Y"
                        ),
                        EnumerableOf.Pipe("public", "final", "class")
                    ))
            ).Value() == 3,
            "cannot chain functions");
        }

        [Fact]
        public void WithIterable()
        {
            Assert.Equal(
                2,
                new LengthOf(
                    new Filtered<string>(
                        input => !input.StartsWith("st") && input.EndsWith("12"),
                         new Enumerable.Mapped<string, string>(
                            new ChainedFunc<string, string, string>(
                                input => input += "1",
                                EnumerableOf.Pipe(
                                    new FuncOf<string, string>(input => input += ("2")),
                                    new FuncOf<string, string>(input => input.Replace("a", "b"))
                                ),
                                input => input.Trim()
                            ),
                            EnumerableOf.Pipe("private", "static", "String"))
                    )
                ).Value()
            );
        }
    }
}
