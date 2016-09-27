using System;

namespace Oneview.Client.Base.Events
{
    internal interface IDelegateReference
    {
        Delegate Target { get; }
    }
}