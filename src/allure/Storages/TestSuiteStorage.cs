using System;
using System.Collections.Generic;
using AllureNUnitAdapter.Exceptions;
using AllureNUnitAdapter.Models.Allure;

namespace AllureNUnitAdapter.Storages
{
    public class TestSuiteStorage
    {
        private readonly Dictionary<String, TestSuiteResult> storage = new Dictionary<String, TestSuiteResult>();

        public TestSuiteResult Get(String suiteId)
        {
            TestSuiteResult result;
            if (storage.TryGetValue(suiteId, out result))
            {
                return result;
            }

            throw new TestSuiteNotFoundException(String.Format("Suite with Id '{0}' not found.", suiteId));
        }

        public TestSuiteResult GetOrCreate(String suiteId)
        {
            TestSuiteResult result;
            if (storage.TryGetValue(suiteId, out result))
            {
                return result;
            }

            result = new TestSuiteResult();
            storage.Add(suiteId, result);
            return result;
        }

        public bool Put(string suiteId, TestSuiteResult suite)
        {
            if (!storage.ContainsKey(suiteId))
            {
                storage.Add(suiteId, suite);
                return true;
            }
            return false;
        }

        public bool Update(string suiteId, TestSuiteResult suite)
        {
            if (storage.ContainsKey(suiteId))
            {
                storage[suiteId] =  suite;
                return true;
            }
            return false;
        }

        public void Remove(string suiteUid)
        {
            storage.Remove(suiteUid);
        }
    }
}

