using System;
namespace Tonga.Scalar
{
    /// <summary>
    /// If an exception of the given type happens in the given function,
    /// it will be rerouted with its original message to the specified type.
    /// </summary>
    public sealed class ExceptionSwap<Val, ExToSwap, NewEx> : IScalar<Val>
        where ExToSwap : Exception
        where NewEx : Exception, new()
    {
        private readonly Func<Val> value;
        private readonly Func<ExToSwap, NewEx> swap;

        /// <summary>
        /// If an exception of the given type happens in the given function,
        /// it will be rerouted with its original message to the specified type.
        /// </summary>
        public ExceptionSwap(Func<Val> value, Func<ExToSwap, NewEx> swap)
        {
            this.value = value;
            this.swap = swap;
        }

        public Val Value()
        {
            try
            {
                return this.value();
            }
            catch (ExToSwap ex)
            {
                throw swap(ex);
            }
        }
    }

    /// <summary>
    /// If an exception of the given type happens in the given function,
    /// it will be rerouted with its original message to the specified type.
    /// </summary>
    public static class ExceptionSwap
    {
        /// <summary>
        /// If an exception of the given type happens in the given function,
        /// it will be rerouted with its original message to the specified type.
        /// </summary>
        public static ExceptionSwap<Value, ExToSwap, NewEx> _<Value, ExToSwap, NewEx>(
            Func<Value> value, Func<ExToSwap, NewEx> swap
        )
        where ExToSwap : Exception
        where NewEx : Exception, new()
        => new ExceptionSwap<Value, ExToSwap, NewEx>(value, swap);

        /// <summary>
        /// If an exception of the given type happens in the given function,
        /// it will be rerouted with its original message to the specified type.
        /// </summary>
        public static ExceptionSwap<Value, Exception, Exception> _<Value>(
            Func<Value> value, Func<Exception, Exception> swap
        )
        => new ExceptionSwap<Value, Exception, Exception>(value, swap);

        /// <summary>
        /// If an exception of the given type happens in the given function,
        /// it will be rerouted with its original message to the specified type.
        /// </summary>
        public static ExceptionSwap<Value, Exception, Exception> _<Value>(
            Func<Value> value, Exception swap
        )
        => new ExceptionSwap<Value, Exception, Exception>(value, (old) => swap);
    }
}

