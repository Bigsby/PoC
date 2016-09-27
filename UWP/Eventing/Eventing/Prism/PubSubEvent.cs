using System;
using System.Linq;

namespace Oneview.Client.Base.Events
{
    public abstract class PubSubEvent<TPayload> : EventBase
    {
        public SubscriptionToken Subscribe(Action<TPayload> action)
        {
            return Subscribe(action, null);
        }
        public SubscriptionToken Subscribe(Action<TPayload> action, Predicate<TPayload> filter)
        {
            return Subscribe(action, filter, false);
        }

        public virtual SubscriptionToken Subscribe(Action<TPayload> action, Predicate<TPayload> filter, bool keepSubscriberReferenceAlive)
        {
            IDelegateReference actionReference = new DelegateReference(action, keepSubscriberReferenceAlive);
            IDelegateReference filterReference;
            if (filter != null)
            {
                filterReference = new DelegateReference(filter, keepSubscriberReferenceAlive);
            }
            else
            {
                filterReference = new DelegateReference(new Predicate<TPayload>(delegate { return true; }), true);
            }


            return base.InternalSubscribe(new EventSubscription<TPayload>(actionReference, filterReference));
        }



        public virtual void Publish(TPayload payload)
        {
            base.InternalPublish(payload);
        }

        public virtual void Unsubscribe(Action<TPayload> subscriber)
        {
            lock (Subscriptions)
            {
                IEventSubscription eventSubscription = Subscriptions.Cast<EventSubscription<TPayload>>().FirstOrDefault(evt => evt.Action == subscriber);
                if (eventSubscription != null)
                {
                    Subscriptions.Remove(eventSubscription);
                }
            }
        }

        public virtual bool Contains(Action<TPayload> subscriber)
        {
            IEventSubscription eventSubscription;
            lock (Subscriptions)
            {
                eventSubscription = Subscriptions.Cast<EventSubscription<TPayload>>().FirstOrDefault(evt => evt.Action == subscriber);
            }
            return eventSubscription != null;
        }

    }

    public abstract class PubSubEvent : EventBase
    {
        public SubscriptionToken Subscribe(Action action)
        {
            return Subscribe(action, false);
        }

        public SubscriptionToken Subscribe(Action action, bool keepSubscriberReferenceAlive)
        {
            IDelegateReference actionReference = new DelegateReference(action, keepSubscriberReferenceAlive);

            return base.InternalSubscribe(new EventSubscription(actionReference));
        }

        public void Publish()
        {
            base.InternalPublish(null);
        }
    }
}