using System;

namespace Career_Hub.My_Exception
{
    public class InvalidEmailException : Exception
    {
        public InvalidEmailException()
            : base("The email format is invalid.")
        {
        }

        public InvalidEmailException(string message)
            : base(message)
        {
        }
    }

}
