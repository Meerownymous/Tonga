

using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Tonga.Map;

namespace Tonga.Func
{
    /// <summary>
    /// An Function which waits for a trigger to return true before executing.
    /// </summary>
    public sealed class BowFunc<T> : IAction<T>
    {
        private readonly Func<bool> trigger;
        private readonly Action prepare;
        private readonly Action<T> shoot;
        private readonly IMap<string, TimeSpan> timespans;

        /// <summary>
        /// A Function which waits for a trigger to return true before executing.
        /// </summary>
        public BowFunc(Func<bool> trigger, Action<T> shoot) : this(
            trigger,
            () => { },
            shoot,
            new TimeSpan(0, 0, 10)
        )
        { }

        /// <summary>
        /// A Function which waits for a trigger to return true before executing.
        /// </summary>
        public BowFunc(Func<bool> trigger, Action prepare, Action<T> shoot, TimeSpan timeout) : this(
            trigger,
            prepare,
            shoot,
            timeout,
            new TimeSpan(0, 0, 0, 0, 250)
        )
        { }

        /// <summary>
        /// A Function which waits for a trigger to return true before executing.
        /// </summary>
        public BowFunc(Func<bool> trigger, Action prepare, Action<T> shoot, TimeSpan timeout, TimeSpan interval) : this(
            trigger,
            prepare,
            shoot,
            AsMap._(
                AsPair._("timeout", timeout),
                AsPair._("interval", interval)
            )
        )
        { }

        /// <summary>
        /// A Function which waits for a trigger to return true before executing.
        /// </summary>
        private BowFunc(Func<bool> trigger, Action prepare, Action<T> shoot, IMap<string, TimeSpan> timespans)
        {
            this.trigger = trigger;
            this.prepare = prepare;
            this.shoot = shoot;
            this.timespans = timespans;
        }

        public void Invoke(T parameter)
        {
            this.prepare();
            var completed = false;

            var parallel =
                new Task(() =>
                {
                    while (true)
                    {
                        if (this.trigger.Invoke())
                        {
                            this.shoot(parameter);
                            completed = true;
                            break;
                        }
                        Task.Delay(this.timespans["interval"]).Wait();
                    }
                }
                );
            try
            {
                parallel.Start();
                parallel.Wait(this.timespans["timeout"]);
            }
            catch (AggregateException ex)
            {
                throw ex.InnerException;
            }

            if (parallel.Status == TaskStatus.Faulted)
            {
                throw parallel.Exception.InnerException;
            }

            if (!completed)
            {
                throw new ApplicationException($"The task did not complete within {this.timespans["timeout"].TotalMilliseconds}ms.");
            }
        }
    }

    public static class BowFunc
    {
        /// <summary>
        /// A Function which waits for a trigger to return true before executing.
        /// </summary>
        public static IAction<T> _<T>(Func<bool> trigger, Action<T> shoot)
            => new BowFunc<T>(trigger, shoot);

        /// <summary>
        /// A Function which waits for a trigger to return true before executing.
        /// </summary>
        public static IAction<T> _<T>(Func<bool> trigger, Action prepare, Action<T> shoot, TimeSpan timeout)
            => new BowFunc<T>(trigger, prepare, shoot, timeout);


        /// <summary>
        /// A Function which waits for a trigger to return true before executing.
        /// </summary>
        public static IAction<T> _<T>(Func<bool> trigger, Action prepare, Action<T> shoot, TimeSpan timeout, TimeSpan interval)
            => new BowFunc<T>(trigger, prepare, shoot, timeout, interval);

    }
}
