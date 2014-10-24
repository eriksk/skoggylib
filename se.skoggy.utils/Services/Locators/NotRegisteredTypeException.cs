using System;

namespace se.skoggy.utils.Services.Locators
{
    public class NotRegisteredTypeException : Exception
    {
        public NotRegisteredTypeException(string message)
            :base(message)
        {
        }
    }
}