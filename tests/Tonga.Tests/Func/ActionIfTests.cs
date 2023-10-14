using Xunit;

namespace Tonga.Func.Tests
{
    public sealed class ActionIfTests
    {
        [Fact]
        public void HasKey()
        {
            Assert.Equal(
                "my-action",
                new ActionIf<int>("my-action", (input) => { }).Key()
            );
        }

        [Fact]
        public void InvokesGivenAction()
        {
            var result = int.MinValue;
            new ActionIf<int>(
                "my-action",
                (input) => result = input
            )
            .Value()
            .Invoke(int.MaxValue);

            Assert.Equal(
                int.MaxValue,
                result
            );
        }
    }
}
