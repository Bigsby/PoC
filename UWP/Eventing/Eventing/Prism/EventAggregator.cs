using System;
using System.Collections.Generic;

namespace Oneview.Client.Base.Events
{
    public sealed class EventAggregator : IEventAggregator
    {
        private readonly Dictionary<Type, EventBase> _events = new Dictionary<Type, EventBase>();

        public EventAggregator()
        {
        }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1004:GenericMethodsShouldProvideTypeParameter")]
        public TEventType GetEvent<TEventType>() where TEventType : EventBase, new()
        {
            lock (_events)
            {
                EventBase existingEvent = null;

                if (!_events.TryGetValue(typeof(TEventType), out existingEvent))
                {
                    var newEvent = new TEventType();
                    _events[typeof(TEventType)] = newEvent;

                    return newEvent;
                }
                else
                {
                    return (TEventType)existingEvent;
                }
            }
        }
    }
}