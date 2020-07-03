using System;
using System.Collections.Generic;
using System.Text;

namespace PersonData
{
    public class PersonAlreadyExistsException : Exception
    {
        public PersonAlreadyExistsException(string message) : base(message)
        {

        }
    }
}
