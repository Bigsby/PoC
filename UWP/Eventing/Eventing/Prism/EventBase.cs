using System;
using System.Collections.Generic;
using System.Linq;

namespace Oneview.Client.Base.Events
{
    public abstract class EventBase
    {
        private const string logName = "PeripheralManager";

        private readonly List<IEventSubscription> _subscriptions = new List<IEventSubscription>();

        internal IList<IEventSubscription> Subscriptions
        {
            get { return _subscriptions; }
        }

        internal virtual SubscriptionToken InternalSubscribe(IEventSubscription eventSubscription)
        {
            if (eventSubscription == null) throw new System.ArgumentNullException("eventSubscription");

            eventSubscription.SubscriptionToken = new SubscriptionToken(Unsubscribe);

            lock (Subscriptions)
            {
                Subscriptions.Add(eventSubscription);
            }
            return eventSubscription.SubscriptionToken;
        }

        protected virtual void InternalPublish(params object[] arguments)
        {
            List<Action<object[]>> executionStrategies = PruneAndReturnStrategies();
            foreach (var executionStrategy in executionStrategies)
            {
                try
                {
                    executionStrategy(arguments);
                }
                catch (Exception ex)
                {
                    //TODO: Logo
                }
            }
        }

        public virtual void Unsubscribe(SubscriptionToken token)
        {
            lock (Subscriptions)
            {
                IEventSubscription subscription = Subscriptions.FirstOrDefault(evt => evt.SubscriptionToken == token);
                if (subscription != null)
                {
                    Subscriptions.Remove(subscription);
                }
            }
        }

        public virtual bool Contains(SubscriptionToken token)
        {
            lock (Subscriptions)
            {
                IEventSubscription subscription = Subscriptions.FirstOrDefault(evt => evt.SubscriptionToken == token);
                return subscription != null;
            }
        }

        private List<Action<object[]>> PruneAndReturnStrategies()
        {
            List<Action<object[]>> returnList = new List<Action<object[]>>();

            lock (Subscriptions)
            {
                for (var i = Subscriptions.Count - 1; i >= 0; i--)
                {
                    Action<object[]> listItem =
                        Subscriptions[i].GetExecutionStrategy();

                    if (listItem == null)
                    {
                        Subscriptions.RemoveAt(i);
                    }
                    else
                    {
                        returnList.Add(listItem);
                    }
                }
            }

            return returnList;
        }
    }
}