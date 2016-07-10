using System;
namespace AllureNUnitAdapter.Exceptions
{
    public class TestCaseNotFoundException : Exception
    {
        public TestCaseNotFoundException(string message) : base(message)
        {
        }
    }
}

