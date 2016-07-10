using System;
using System.Collections.Generic;
using AllureNUnitAdapter.Models.Allure;

namespace AllureNUnitAdapter.Storages
{
    public class AttachmentStorage
    {
        private readonly Dictionary<String, List<Attachment>> storage = new Dictionary<String, List<Attachment>>();

        public List<Attachment> Get(String caseId)
        {
            List<Attachment> result;
            if (storage.TryGetValue(caseId, out result))
            {
                return result;
            }

            return new List<Attachment>();
        }

        public bool Put(string caseId, Attachment result)
        {
            if (!storage.ContainsKey(caseId))
            {
                storage.Add(caseId, new List<Attachment>() { result });
            }
            else
            {
                storage[caseId].Add(result);
            }
            return true;
        }

        public void Remove(string caseId)
        {
            storage.Remove(caseId);
        }
    }
}

