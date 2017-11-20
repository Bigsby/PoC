using System;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;

namespace EventSubscription
{
    class Program
    {
        static void Main(string[] args)
        {
            var publisher = new EventPublisher();
            var subscriber = new EventSubscriber(publisher);

            var isSubscribed = IsEventSubscribed(publisher, nameof(EventPublisher.TheEvent), subscriber);

            var isActionSubscribed = IsEventSubscribed(publisher, p => p.TheEvent += null, subscriber);
            var stop = "here";
        }

        static bool IsEventSubscribed<TPublisher>(TPublisher publisher, string eventName, object subscriber)
        {
            if (null == publisher || null == subscriber)
                return false;

            var eventField = typeof(TPublisher).GetField(eventName, BindingFlags.Instance | BindingFlags.NonPublic);

            if (null == eventField)
                return false;

            var eventHandler = eventField.GetValue(publisher);

            if (eventHandler is MulticastDelegate multicastDelegate)
                return multicastDelegate.GetInvocationList().Any(s => 
                    s.Target == subscriber 
                    || s.Method.DeclaringType == subscriber.GetType() 
                    || s.Method.DeclaringType?.DeclaringType == subscriber.GetType());
            
            return false;
        }

        static bool IsEventSubscribed<TPublisher>(TPublisher publisher, Action<TPublisher> eventExpression, object subscriber)
        {
            if (null == publisher || null == subscriber || null == eventExpression)
                return false;

            var body = eventExpression.GetMethodInfo()?.GetMethodBody();
            if (null == body)
                return false;


            if (null == publisher || null == subscriber)
                return false;


            //var eventField = typeof(TPublisher).GetField(eventName, BindingFlags.Instance | BindingFlags.NonPublic);

            //if (null == eventField)
            //    return false;

            //var eventHandler = eventField.GetValue(publisher);

            //if (eventHandler is MulticastDelegate multicastDelegate)
            //    return multicastDelegate.GetInvocationList().Any(s =>
            //        s.Target == subscriber
            //        || s.Method.DeclaringType == subscriber.GetType()
            //        || s.Method.DeclaringType?.DeclaringType == subscriber.GetType());

            return false;
        }
    }

    public class EventPublisher
    {
        public event EventHandler TheEvent;
    }

    public class EventSubscriber
    {
        public EventSubscriber(EventPublisher publisher)
        {
            publisher.TheEvent += (s, e) => { };
        }

        private void Publisher_TheEvent(object sender, EventArgs e)
        {
            throw new NotImplementedException();
        }
    }
}
