using System;
using System.Reactive.Linq;
using System.Reactive.Subjects;

namespace Rxing
{
    class Program
    {
        static void Main(string[] args)
        {
            Observable.Interval(TimeSpan.FromMilliseconds(500))
                .ReverseThrottle(TimeSpan.FromSeconds(1.5))
                .Subscribe(i => System.Console.WriteLine(i));
            System.Console.WriteLine("Subscribed!");
            Console.ReadLine();
            System.Console.WriteLine("Done!");
        }
    }

    public static class MyExtension
    {
        public static IObservable<T> ReverseThrottle<T>(this IObservable<T> observable, TimeSpan interval)
        {
            return new ReverseThrottleObservable<T>(observable, interval);
        }

        class ReverseThrottleObservable<T> : IObservable<T>
        {
            private bool _canNext = true;
            private readonly IObservable<T> _observable;
            private readonly TimeSpan _interval;

            public ReverseThrottleObservable(IObservable<T> source, TimeSpan interval)
            {
                _interval = interval;
                    
                _observable = Observable.Create<T>(o => source.Subscribe(
                    value =>
                    {
                        if (!_canNext) 
                            return;
                        
                        _canNext = false;
                        Observable.Timer(_interval).Subscribe(_ => _canNext = true);
                        o.OnNext(value);
                    },
                    o.OnError,
                    o.OnCompleted
                ));
            }

            public IDisposable Subscribe(IObserver<T> observer)
            {
                return _observable.Subscribe(observer);
            }
        }
    }
}
