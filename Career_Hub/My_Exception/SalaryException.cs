using System;

namespace Career_Hub.My_Exception
{
    public class SalaryException : Exception
    {
        public SalaryException()
            : base("The salary value cannot be negative. Please enter a valid salary.")
        {
        }
    }
}
