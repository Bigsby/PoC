using System;

namespace Oneview.Client.Base.Events
{
    internal interface IEventSubscription
    {
        SubscriptionToken SubscriptionToken { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1024:UsePropertiesWhereAppropriate")]
        Action<object[]> GetExecutionStrategy();
    }
}