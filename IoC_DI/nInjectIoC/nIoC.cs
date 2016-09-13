using InterfacesLibrary;
using System;
using System.Collections.Generic;

namespace nInjectIoC
{
    public sealed class nIoC
    {
        public nIoC()
        {

        }

        public void Register(Type registrationType, object instance)
        {
            throw new NotImplementedException();
        }

        public void Register<TInterface>(TInterface instance)
        {
            throw new NotImplementedException();
        }

        public void RegisterMultiple(Type registrationType, params object[] instance)
        {
            throw new NotImplementedException();
        }

        public void RegisterMultiple<TInterface>(TInterface instance)
        {
            throw new NotImplementedException();
        }

        public TInterface Resolve<TInterface>()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<TInterface> ResolveMultiple<TInterface>()
        {
            throw new NotImplementedException();
        }
    }
}
