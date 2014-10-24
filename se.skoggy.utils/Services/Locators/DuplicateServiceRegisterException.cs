using System;

namespace se.skoggy.utils.Services.Locators
{
    public class DuplicateServiceRegisterException : Exception
    {
        public DuplicateServiceRegisterException(string message)
            :base(message)
        {
        }
    }
}