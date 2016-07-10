using System;

namespace AllureNUnitAdapter.Exceptions
{
    public class TestSuiteNotFoundException : Exception
    {
        public TestSuiteNotFoundException(string message) : base(message)
        {
        }
    }
}

