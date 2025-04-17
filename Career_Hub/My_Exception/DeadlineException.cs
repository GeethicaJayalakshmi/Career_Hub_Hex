using System;

namespace Career_Hub.My_Exception
{
    public class DeadlineException : Exception
    {
        public DeadlineException()
            : base("The job application deadline has already passed. You can no longer apply.")
        {
        }
    }
}
