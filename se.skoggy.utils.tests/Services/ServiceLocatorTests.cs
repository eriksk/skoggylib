using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using se.skoggy.utils.Services;
using se.skoggy.utils.Services.Locators;

namespace se.skoggy.utils.tests.Services
{
    internal interface ICar
    {
        void Vrrooomm();
    }

    class Car : ICar
    {
        public void Vrrooomm()
        {
            
        }
    }

    [TestClass]
    public class ServiceLocatorTests
    {
        [TestMethod]
        public void CreateInstance_OfRegisteredType_CreatesANewInstance()
        {
            var serviceLocator = new ServiceLocator();
            serviceLocator.Register<ICar, Car>();

            Assert.AreEqual(typeof(Car), serviceLocator.Create<ICar>().GetType());
        }

        [TestMethod]
        public void LocateSingleton_SingletonIsRegistered_InstanceReturned()
        {
            var serviceLocator = new ServiceLocator();
            serviceLocator.RegisterSingleton<ICar>(new Car());

            Assert.AreEqual(typeof(Car), serviceLocator.Locate<ICar>().GetType());
        }

        [TestMethod]
        [ExpectedException(typeof(DuplicateServiceRegisterException))]
        public void RegisteringSameInterfaceTwice_ThrowsException()
        {
            ServiceLocator.Context.Register<ICar, Car>();
            ServiceLocator.Context.Register<ICar, Car>();
        }

        [TestMethod]
        [ExpectedException(typeof(DuplicateServiceRegisterException))]
        public void RegisteringSameSingletonTwice_ThrowsException()
        {
            ServiceLocator.Context.RegisterSingleton<ICar>(new Car());
            ServiceLocator.Context.RegisterSingleton<ICar>(new Car());
        }

        [TestMethod]
        public void LocateSingleton_UsingStaticService_StillWorks()
        {
            ServiceLocator.Context.RegisterSingleton<ICar>(new Car());
            Assert.AreEqual(typeof(Car), ServiceLocator.Context.Locate<ICar>().GetType());
        }
    }
}
