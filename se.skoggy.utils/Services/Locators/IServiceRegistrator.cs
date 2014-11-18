using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;

namespace se.skoggy.utils.Services.Locators
{
    public interface IServiceRegistrator
    {
        void Register();
    }
}
