using System;

namespace Oneview.Client.Base.Events
{
    public interface IEventAggregator
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1004:GenericMethodsShouldProvideTypeParameter")]
        TEventType GetEvent<TEventType>() where TEventType : EventBase, new();

        //SubscriptionToken Subscribe<TEvent>(Action callback) where TEvent : Event;
        //SubscriptionToken Subscribe<TEvent, TArgs>(Action<TArgs> callback) where TEvent : Event<TArgs>;
        //SubscriptionToken Subscribe(Action<TPayload> action, Predicate<TPayload> filter, bool keepSubscriberReferenceAlive);
    }

    public abstract class Event
    { }

    public abstract class Event<TPayload> : Event
    {
        public TPayload Args { get; set; }
    }
}
