using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace se.skoggy.utils.Services.Locators
{
    public class ServiceLocator
    {
        private static ServiceLocator _context;
        private readonly List<Service> _services;

        public ServiceLocator()
        {
            _services = new List<Service>();
        }

        public static ServiceLocator Context
        {
            get { return _context ?? (_context = new ServiceLocator()); }
        }

        public void Register<TInterface, TImplementation>()
        {
            if (_services.Any(x => x.Type == typeof (TInterface)))
                throw new DuplicateServiceRegisterException(string.Format("Duplicate registration for type '{0}'", typeof(TInterface).Name));
            
            _services.Add(new Service()
            {
                Type = typeof(TInterface),
                ImplementationType = typeof(TImplementation)
            });
        }

        public void RegisterSingleton<TInterface>(object instance)
        {
            if (_services.Any(x => x.Type == typeof(TInterface)))
                throw new DuplicateServiceRegisterException(string.Format("Duplicate registration for type '{0}'", typeof(TInterface).Name));
            
            _services.Add(new Service()
            {
                Type = typeof(TInterface),
                ImplementationType = instance.GetType(),
                Instance = instance
            });
        }

        public TInterface Locate<TInterface>()
        {
            if (_services.Any(x => x.Type == typeof(TInterface)))
                return (TInterface)_services.First(x => x.Type == typeof(TInterface)).Instance;

            throw new NotRegisteredTypeException(string.Format("Unable to find registration for type '{0}'", typeof(TInterface).Name));
        }

        public object Locate(Type type)
        {
            if (_services.Any(x => x.Type == type))
                return _services.First(x => x.Type == type).Instance;

            throw new NotRegisteredTypeException(string.Format("Unable to find registration for type '{0}'", type.Name));
        }

        public object TryLocate(Type type)
        {
            if (_services.Any(x => x.Type == type))
                return _services.First(x => x.Type == type).Instance;

            return null;
        }

        public object Create(Type type)
        {
            if (_services.Any(x => x.Type == type))
            {
                var implementationType = _services.First(x => x.Type == type).ImplementationType;
                var parameterTypes = implementationType.GetConstructors().First().GetParameters().Select(x => x.ParameterType);
                var constructorParameters = parameterTypes.Select(x => TryLocate(x) ?? Create(x));

                return Activator.CreateInstance(implementationType, constructorParameters.ToArray());
            }
            throw new NotRegisteredTypeException(string.Format("Unable to find registration for type '{0}'", type.Name));
        }

        public TInterface Create<TInterface>()
        {
            if (_services.Any(x => x.Type == typeof(TInterface)))
            {
                var implementationType = _services.First(x => x.Type == typeof (TInterface)).ImplementationType;
                var parameterTypes = implementationType.GetConstructors().First().GetParameters().Select(x => x.ParameterType);
                var constructorParameters = parameterTypes.Select(x => TryLocate(x) ?? Create(x));

                return (TInterface)Activator.CreateInstance(implementationType, constructorParameters.ToArray());
            }
            throw new NotRegisteredTypeException(string.Format("Unable to find registration for type '{0}'", typeof(TInterface).Name));
       
        }
    }
}
