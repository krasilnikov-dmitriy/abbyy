using System;
using System.Collections.Generic;
using AllureNUnitAdapter.Exceptions;
using AllureNUnitAdapter.Models.Allure;

namespace AllureNUnitAdapter.Storages
{
    public class TestCaseStorage
    {
        private readonly Dictionary<String, TestCaseResult> storage = new Dictionary<String, TestCaseResult>();

        public TestCaseResult Get(String caseId)
        {
            TestCaseResult result;
            if (storage.TryGetValue(caseId, out result))
            {
                return result;
            }

            throw new TestCaseNotFoundException(String.Format("Case with Id '{0}' not found.", caseId));
        }

        public bool Put(string caseId, TestCaseResult result)
        {
            if (!storage.ContainsKey(caseId))
            {
                storage.Add(caseId, result);
                return true;
            }
            return false;
        }

        public bool Update(string caseId, TestCaseResult result)
        {
            if (storage.ContainsKey(caseId))
            {
                storage.Add(caseId, result);
                return true;
            }
            return false;
        }

        public void Remove(string caseId)
        {
            storage.Remove(caseId);
        }
    }
}

