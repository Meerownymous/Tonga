

using System;
using System.Collections.Generic;
using Xunit;
using Tonga.Enumerable;


namespace Tonga.Scalar.Tests
{
    public sealed class ItemAtTests
    {
        [Fact]
        public void DeliversFirstElement()
        {

            Assert.True(
                new ItemAt<int>(
                    Enumerable.AsEnumerable._(1, 2, 3)
                ).Value() == 1,
                "Can't take the first item from the enumerable"
            );
        }

        [Fact]
        public void DeliversFirstElementWithException()
        {

            Assert.True(
                new ItemAt<int>(
                    Enumerable.AsEnumerable._(1, 2, 3),
                    new NotFiniteNumberException("Cannot do this!")
                ).Value() == 1,
                "Can't take the first item from the enumerable"
            );
        }

        [Fact]
        public void DeliversElementByPos()
        {
            Assert.Equal(
                2,
                new ItemAt<int>(
                    Enumerable.AsEnumerable._(1, 2, 3),
                    1
                ).Value()
            );
        }

        [Fact]
        public void DeliversElementByPosWithFallback()
        {
            Assert.True(
                new ItemAt<int>(
                    Enumerable.AsEnumerable._(1, 2, 3),
                    1,
                    4
                ).Value() == 2,
                "Can't take the item by position from the enumerable"
            );
        }

        [Fact]
        public void FailsForEmptyCollection()
        {
            Assert.Throws<ArgumentException>(() =>
                new ItemAt<int>(
                    new List<int>()
                ).Value()
            );
        }

        [Fact]
        public void DeliversFallback()
        {
            String fallback = "fallback";
            Assert.Equal(
                fallback,
                ItemAt._(
                    None._<string>(),
                    12,
                    fallback
                ).Value()
            );
        }

        [Fact]
        public void FallbackShowsError()
        {
            Assert.Throws<InvalidOperationException>(() =>
                ItemAt._(
                    None._<string>(),
                    12,
                    (ex, enumerable) => throw ex
                ).Value()
            );
        }

        [Fact]
        public void FallbackShowsGivenErrorWithPosition()
        {
            Assert.Throws<NotFiniteNumberException>(() =>
                ItemAt._(
                    None._<string>(),
                    12,
                    new NotFiniteNumberException("Cannot do this!")
                ).Value()
            );
        }

        [Fact]
        public void FallbackShowsGivenErrorWithoutPosition()
        {
            Assert.Throws<NotFiniteNumberException>(() =>
                ItemAt._(
                    None._<string>(),
                    new NotFiniteNumberException("Cannot do this!")
                ).Value()
            );
        }

        [Fact]
        public void FallbackShowsGivenErrorForNegativePosition()
        {
            Assert.Throws<NotFiniteNumberException>(() =>
                ItemAt._(
                    None._<string>(),
                    -12,
                    new NotFiniteNumberException("Cannot do this!")
                ).Value()
            );
        }

        [Fact]
        public void SensesChanges()
        {
            var list = new List<string>{ "pre" };
            var transient = new ItemAt<string>(list);
            transient.Value();
            list.Clear();
            list.Add("post");

            Assert.Equal("post", transient.Value());
        }

        [Fact]
        public void DeliversLogicErrorMessage()
        {
            try
            {
                ItemAt._(
                    new string[] { "one", "two", "three" },
                    3
                ).Value();
            }
            catch (Exception ex)
            {
                Assert.Equal(
                    "Cannot get element at position 4: Cannot get item 4 - The enumerable has only 3 items.",
                    ex.Message
                );
            }
        }
    }
}
