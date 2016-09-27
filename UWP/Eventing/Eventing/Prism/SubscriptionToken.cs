using System;

namespace Oneview.Client.Base.Events
{
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1063:ImplementIDisposableCorrectly", Justification = "Should never have a need for a finalizer, hence no need for Dispole(bool)")]
    public class SubscriptionToken : IEquatable<SubscriptionToken>, IDisposable
    {
        private readonly Guid _token;
        private Action<SubscriptionToken> _unsubscribeAction;

        public SubscriptionToken(Action<SubscriptionToken> unsubscribeAction)
        {
            _unsubscribeAction = unsubscribeAction;
            _token = Guid.NewGuid();
        }

        public bool Equals(SubscriptionToken other)
        {
            return null != other && other._token == _token;
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(this, obj)) return true;
            return Equals(obj as SubscriptionToken);
        }

        public override int GetHashCode()
        {
            return _token.GetHashCode();
        }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1063:ImplementIDisposableCorrectly", Justification = "Should never have need for a finalizer, hence no need for Dispose(bool).")]
        public virtual void Dispose()
        {
            if (_unsubscribeAction != null)
            {
                _unsubscribeAction(this);
                _unsubscribeAction = null;
            }

            GC.SuppressFinalize(this);
        }
    }
}