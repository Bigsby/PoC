using System;
using System.Collections.Generic;
using System.Reactive;
using System.Reactive.Linq;
using System.Reactive.Subjects;
using System.Threading.Tasks;

namespace Rxing
{
    class Program
    {
        static Action<Action> run;
        static void Main(string[] args)
        {

            TestTestScheduler();

            Console.ReadLine();
            System.Console.WriteLine("Done!");


        }

        static void TestTestScheduler()
        { 
        }

        static void TestReverseThrottle()
        {
            Observable.Interval(TimeSpan.FromMilliseconds(500))
                .ReverseThrottle(TimeSpan.FromSeconds(1.5))
                .Subscribe(i => System.Console.WriteLine(i));
            System.Console.WriteLine("Subscribed!");
        }

        static void TestSubject()
        {
            var sub = new Subject<int>();
            sub.Subscribe(i => System.Console.WriteLine("got " + i));
            sub.OnNext(1);
            sub.OnNext(2);
            sub.Dispose();
            if (!sub.IsDisposed)
                sub.OnNext(3);
        }

        static void TestList()
        {
            var list = new List<int>(new[] { 1, 2, 3 });

            var o = list.ToObservable().Subscribe(i => Console.WriteLine("got " + i));

            list.Add(5); //not processed
        }

        static void TestCreateObserver()
        {
            var obserrver = Observer.Create<Action>(a => a());
            obserrver.OnNext(() => Console.WriteLine("One"));
            obserrver.OnNext(() => Console.WriteLine("Two"));
        }

        static void TestActionProxy()
        {
            run = _ => Console.WriteLine("Run called");

            var ob =
            Observable.FromEvent<Action>(
            eh => run += eh,
            eh => run -= eh)
            .Throttle(TimeSpan.FromSeconds(1)).Subscribe(a => a());

            Task.Delay(500).Wait();

            run(() => Console.WriteLine("One"));
            Task.Delay(200).Wait();
            run(() => Console.WriteLine("Two"));
            Task.Delay(3000).Wait();
            ob.Dispose();
            run(() => Console.WriteLine("Three"));
            Task.Delay(500).Wait();
            run(() => Console.WriteLine("Four"));
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
