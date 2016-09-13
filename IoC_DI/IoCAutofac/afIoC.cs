using Autofac;
using InterfacesLibrary;
using System;
using System.Collections.Generic;

namespace IoCAutofac
{
    public sealed class afIoC : IIoCContainer
    {
        private readonly ContainerBuilder _container;
        
        public afIoC()
        {
            _container = new ContainerBuilder();
        }

        public void Register(Type registrationType, object instance)
        {
            _container.RegisterInstance(instance).As(registrationType).SingleInstance();
        }

        public void Register<TInterface>(TInterface instance)
        {
            _container.RegisterInstance(instance as object).As<TInterface>().SingleInstance();
        }

        public void RegisterMultiple(Type registrationType, params object[] instances)
        {
            foreach (var instance in instances)
                _container.RegisterInstance(instance).As(registrationType);
        }

        public void RegisterMultiple<TInterface>(params TInterface[] instances)
        {
            foreach (var instance in instances)
                _container.RegisterInstance(instance as object).As<TInterface>();
        }

        public object Resolve(Type registrationType)
        {
            return _container.Build().Resolve(registrationType);
        }

        public TInterface Resolve<TInterface>()
        {
            return _container.Build().Resolve<TInterface>();
        }

        public IEnumerable<object> ResolveMultiple(Type registrationType)
        {
            return (IEnumerable<object>)_container.Build().Resolve(GetGenericEnumerableType(registrationType));
        }

        public IEnumerable<TInterface> ResolveMultiple<TInterface>()
        {
            return _container.Build().Resolve<IEnumerable<TInterface>>();
        }

        private readonly IDictionary<Type, Type> _enumerableTypes = new Dictionary<Type, Type>();
        private readonly Type _enumerableType = typeof(IEnumerable<>);
        private Type GetGenericEnumerableType(Type registrationType)
        {
            Type result;

            if (!_enumerableTypes.TryGetValue(registrationType, out result))
                _enumerableTypes[registrationType] = result = _enumerableType.MakeGenericType(registrationType);

            return result;
        }
    }
}
