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

        public void Register<T, T1>() where T1 : new()
        {
            if (_services.Any(x => x.Type == typeof (T)))
                throw new DuplicateServiceRegisterException(string.Format("Duplicate registration for type '{0}'", typeof(T).Name));
            
            _services.Add(new Service()
            {
                Type = typeof(T),
                ImplementationType = typeof(T1)
            });
        }

        public void RegisterSingleton<T>(object instance)
        {
            if (_services.Any(x => x.Type == typeof(T)))
                throw new DuplicateServiceRegisterException(string.Format("Duplicate registration for type '{0}'", typeof(T).Name));
            
            _services.Add(new Service()
            {
                Type = typeof(T),
                ImplementationType = instance.GetType(),
                Instance = instance
            });
        }

        public T Locate<T>()
        {
            if (_services.Any(x => x.Type == typeof(T)))
                return (T)_services.First(x => x.Type == typeof (T)).Instance;
            
            throw new NotRegisteredTypeException(string.Format("Unable to find registration for type '{0}'", typeof(T).Name));
        }

        public T Create<T>()
        {
            if (_services.Any(x => x.Type == typeof (T)))
            {
                return (T)Activator.CreateInstance(_services.First(x => x.Type == typeof (T)).ImplementationType);
            }
            throw new NotRegisteredTypeException(string.Format("Unable to find registration for type '{0}'", typeof(T).Name));
       
        }
    }
}
