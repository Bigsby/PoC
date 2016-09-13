using System;
using System.Linq;

namespace Oneview.Client.Base.Events
{
    /// <summary>
    /// Defines a class that manages publication and subscription to events.
    /// </summary>
    /// <typeparam name="TPayload">The type of message that will be passed to the subscribers.</typeparam>
    public abstract class PubSubEvent<TPayload> : EventBase
    {
        /// <summary>
        /// Subscribes a delegate to an event.
        /// PubSubEvent will maintain a <see cref="WeakReference"/> to the Target of the supplied <paramref name="action"/> delegate.
        /// </summary>
        /// <param name="action">The delegate that gets executed when the event is published.</param>
        /// <returns>A <see cref="SubscriptionToken"/> that uniquely identifies the added subscription.</returns>
        /// <remarks>
        /// The PubSubEvent collection is thread-safe.
        /// </remarks>
        public SubscriptionToken Subscribe(Action<TPayload> action)
        {
            return Subscribe(action, null);
        }

        /// <summary>
        /// Subscribes a delegate to an event
        /// PubSubEvent will maintain a <see cref="WeakReference"/> to the Target of the supplied <paramref name="action"/> delegate.
        /// </summary>
        /// <param name="action">The delegate that gets executed when the event is published.</param>
        /// <param name="filter">Filter to evaluate if the subscriber should receive the event.</param>
        /// <returns></returns>
        public SubscriptionToken Subscribe(Action<TPayload> action, Predicate<TPayload> filter)
        {
            return Subscribe(action, filter, false);
        }

		/// <summary>
		/// Subscribes a delegate to an event.
		/// </summary>
		/// <param name="action">The delegate that gets executed when the event is published.</param>
		/// <param name="filter">Filter to evaluate if the subscriber should receive the event.</param>
		/// <param name="keepSubscriberReferenceAlive">When <see langword="true" />, the <see cref="PubSubEvent{TPayload}" /> keeps a reference to the subscriber so it does not get garbage collected.</param>
		/// <returns></returns>
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
		


        /// <summary>
        /// Publishes the <see cref="PubSubEvent{TPayload}"/>.
        /// </summary>
        /// <param name="payload">Message to pass to the subscribers.</param>
        public virtual void Publish(TPayload payload)
        {
            base.InternalPublish(payload);
        }

        /// <summary>
        /// Removes the first subscriber matching <see cref="Action{TPayload}"/> from the subscribers' list.
        /// </summary>
        /// <param name="subscriber">The <see cref="Action{TPayload}"/> used when subscribing to the event.</param>
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

        /// <summary>
        /// Returns <see langword="true"/> if there is a subscriber matching <see cref="Action{TPayload}"/>.
        /// </summary>
        /// <param name="subscriber">The <see cref="Action{TPayload}"/> used when subscribing to the event.</param>
        /// <returns><see langword="true"/> if there is an <see cref="Action{TPayload}"/> that matches; otherwise <see langword="false"/>.</returns>
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