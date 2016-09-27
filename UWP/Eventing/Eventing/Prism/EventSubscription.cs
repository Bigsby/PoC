using System;
using System.Globalization;

namespace Oneview.Client.Base.Events
{
    internal class EventSubscription<TPayload> : IEventSubscription
    {
        private readonly IDelegateReference _actionReference;
        private readonly IDelegateReference _filterReference;

		public EventSubscription(IDelegateReference actionReference, IDelegateReference filterReference)
        {
            if (actionReference == null)
                throw new ArgumentNullException("actionReference");
            if (!(actionReference.Target is Action<TPayload>))
                throw new ArgumentException(String.Format(CultureInfo.CurrentCulture, "InvalidDelegateRerefenceTypeException", typeof(Action<TPayload>).FullName), "actionReference");

            if (filterReference == null)
                throw new ArgumentNullException("filterReference");
            if (!(filterReference.Target is Predicate<TPayload>))
                throw new ArgumentException(String.Format(CultureInfo.CurrentCulture, "InvalidDelegateRerefenceTypeException", typeof(Predicate<TPayload>).FullName), "filterReference");

            _actionReference = actionReference;
            _filterReference = filterReference;
        }

        public Action<TPayload> Action
        {
            get { return (Action<TPayload>)_actionReference.Target; }
        }

        public Predicate<TPayload> Filter
        {
            get { return (Predicate<TPayload>)_filterReference.Target; }
        }

        public SubscriptionToken SubscriptionToken { get; set; }

        public virtual Action<object[]> GetExecutionStrategy()
        {
            Action<TPayload> action = this.Action;
            Predicate<TPayload> filter = this.Filter;
            if (action != null && filter != null)
            {
                return arguments =>
                {
                    TPayload argument = default(TPayload);
                    if (arguments != null && arguments.Length > 0 && arguments[0] != null)
                    {
                        argument = (TPayload)arguments[0];
                    }
                    if (filter(argument))
                    {
                        InvokeAction(action, argument);
                    }
                };
            }
            return null;
        }

        public virtual void InvokeAction(Action<TPayload> action, TPayload argument)
        {
            if (action == null) throw new System.ArgumentNullException("action");

            action(argument);
        }
    }

    internal class EventSubscription : IEventSubscription
    {
        private readonly IDelegateReference _actionReference;

        public EventSubscription(IDelegateReference actionReference)
        {
            if (actionReference == null)
                throw new ArgumentNullException("actionReference");
            if (!(actionReference.Target is Action))
                throw new ArgumentException(string.Format(CultureInfo.CurrentCulture, "InvalidDelegateRerefenceTypeException", typeof(Action).FullName), "actionReference");

            _actionReference = actionReference;
        }

        public SubscriptionToken SubscriptionToken { get; set; }

        public Action Action
        {
            get { return (Action)_actionReference.Target; }
        }

        public Action<object[]> GetExecutionStrategy()
        {
            Action action = Action;
            if (action != null)
            {
                return arguments => InvokeAction(action);
            }
            return null;
        }


        public virtual void InvokeAction(Action action)
        {
            if (action == null) throw new System.ArgumentNullException("action");

            action();
        }
    }
}