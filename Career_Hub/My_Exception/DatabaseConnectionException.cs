using System;

namespace Career_Hub.My_Exception
{
    public class DatabaseConnectionException : Exception
    {
        public DatabaseConnectionException()
            : base("A database connection error occurred.")
        {
        }

        public DatabaseConnectionException(string message)
            : base(message)
        {
        }
    }

}
