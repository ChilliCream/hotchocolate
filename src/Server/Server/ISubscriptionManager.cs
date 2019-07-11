using System;
using System.Collections.Generic;
using HotChocolate.Execution;

namespace HotChocolate.Server
{
    public interface ISubscriptionManager
        : IEnumerable<ISubscription>
        , IDisposable
    {
        void Register(string subscriptionId, IResponseStream responseStream);

        void Register(ISubscription subscription);

        void Unregister(ISubscription subscription);

        void Unregister(string subscriptionId);
    }
}
