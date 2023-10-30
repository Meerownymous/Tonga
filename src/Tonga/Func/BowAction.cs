

using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Tonga.Map;

namespace Tonga.Func
{
    /// <summary>
    /// An action which waits for a trigger to return true before executing.
    /// </summary>
    public sealed class BowAction : IAction
    {
        private readonly Func<bool> trigger;
        private readonly IMap<string, Action> actions;
        private readonly IMap<string, TimeSpan> timespans;

        /// <summary>
        /// An action which waits for a trigger to return true before executing.
        /// </summary>
        public BowAction(Func<bool> trigger, Action shoot) : this(
            trigger, () => { },
            shoot,
            new TimeSpan(0, 0, 10),
            new TimeSpan(0, 0, 0, 0, 250)
        )
        { }

        /// <summary>
        /// An action which waits for a trigger to return true before executing.
        /// </summary>
        public BowAction(Func<bool> trigger, Action shoot, TimeSpan timeout) : this(
            trigger,
            () => { },
            shoot,
            timeout,
            new TimeSpan(0, 0, 0, 0, 250)
        )
        { }

        /// <summary>
        /// An action which waits for a trigger to return true before executing.
        /// </summary>
        public BowAction(Func<bool> trigger, Action prepare, Action shoot) : this(
            trigger,
            prepare,
            shoot,
            new TimeSpan(0, 0, 10),
            new TimeSpan(0, 0, 0, 0, 250)
        )
        { }

        public BowAction(Func<bool> trigger, Action prepare, Action shoot, TimeSpan timeout, TimeSpan interval) : this(
            trigger,
            AsMap._(
                AsPair._("prepare", prepare),
                AsPair._("shoot", shoot)
            ),
            AsMap._(
                AsPair._("timeout", timeout),
                AsPair._("interval", interval)
            )
        )
        { }

        /// <summary>
        /// An action which waits for a trigger to return true before executing.
        /// </summary>
        private BowAction(Func<bool> trigger, IMap<string, Action> actions, IMap<string, TimeSpan> timespans)
        {
            this.trigger = trigger;
            this.actions = actions;
            this.timespans = timespans;
        }

        public void Invoke()
        {
            this.actions["prepare"]();
            var completed = false;

            var parallel =
                new Task(() =>
                {
                    while (true)
                    {
                        lock (this.timespans)
                        {
                            if (this.trigger.Invoke())
                            {
                                this.actions["shoot"]();
                                completed = true;
                                break;
                            }
                            Task.Delay(this.timespans["interval"]).Wait();
                        }
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
}
