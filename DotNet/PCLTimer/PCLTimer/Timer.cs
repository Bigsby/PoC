using System;
using System.Threading;
using System.Threading.Tasks;

namespace PCLTimer
{
    public class Timer
    {
        private readonly Action<object> _callback;
        private readonly object _state;
        private CancellationTokenSource _delay;

        public Timer(Action<object> callback, object state, int millisecondsDueTime, long millisecondsPeriod)
        {
            _callback = callback;
            _state = state;

            _delay = new InnterTimer(callback, state, millisecondsDueTime, millisecondsPeriod);
        }

        public void Change(int i, long reviewLapse)
        {
            _delay.Dispose();

            if (i != Timeout.Infinite)
                _delay = new InnterTimer(_callback, _state, i, reviewLapse);
        }

        private class InnterTimer : CancellationTokenSource
        {
            public InnterTimer(Action<object> callback, object state, int millisecondsDueTime, long millisecondsPeriod)
            {
                Task.Delay(millisecondsDueTime, Token).ContinueWith(async (t, s) =>
                    {
                        var tuple = (Tuple<Action<object>, object>)s;
                        while (true)
                        {
                            if (IsCancellationRequested)
                                break;
                            tuple.Item1(tuple.Item2);

                            await Task.Delay((int)millisecondsPeriod);
                        }
                    }, Tuple.Create(callback, state), CancellationToken.None,
                    TaskContinuationOptions.ExecuteSynchronously | TaskContinuationOptions.OnlyOnRanToCompletion,
                    TaskScheduler.Default);
            }

            protected override void Dispose(bool disposing)
            {
                Cancel();
            }
        }
    }
}
