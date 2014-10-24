using System;

namespace se.skoggy.utils.Services.Locators
{
    internal class Service
    {
        public Type Type { get; set; }
        public Type ImplementationType { get; set; }
        public object Instance { get; set; }
    }
}