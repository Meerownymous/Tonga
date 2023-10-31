using System;
using Tonga.Map;
using Xunit;

namespace Tonga.Tests.Map
{
    public sealed class AsDictionaryTests
    {
        [Fact]
        public void ConvertsToDictionary()
        {
            Assert.Equal(
                "Rock",
                AsDictionary._(
                    AsMap._("Castle", "Rock")
                )["Castle"]
            );
        }

        [Fact]
        public void OverwritesValue()
        {
            var dict =
                AsDictionary._(
                    AsMap._("Castle", "Rock")
                );

            dict["Castle"] = "Wolfenstein";

            Assert.Equal(
                "Wolfenstein",
                dict["Castle"]
            );
        }
    }
}

