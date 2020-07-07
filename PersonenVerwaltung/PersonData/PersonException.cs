using System;

namespace PersonData
{
    public class PersonException : Exception
    {
        public PersonException(string message) : base(message)
        {
        }
    }
}