using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InterfacesLibrary
{
    public interface IIoCContainer
    {
        void Register<TInterface>(TInterface instance);
        void Register(Type registrationType, object instance);

        void RegisterMultiple<TInterface>(params TInterface[] instances);
        void RegisterMultiple(Type registrationType, params object[] instances);

        TInterface Resolve<TInterface>();
        object Resolve(Type registrationType);

        IEnumerable<TInterface> ResolveMultiple<TInterface>();
        IEnumerable<object> ResolveMultiple(Type registrationType);
    }
}
