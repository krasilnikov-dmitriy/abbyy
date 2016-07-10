using System;
using System.Xml.Serialization;

namespace AllureNUnitAdapter.Models.NUnit.EventArgs
{
    [Serializable]
    [XmlRoot(ElementName = "start-suite")]
    public class StartTestSuiteEventArgs
    {
        [XmlAttribute(AttributeName ="id")]
        public String Id { get; set; }

        [XmlAttribute(AttributeName = "parentId")]
        public String ParentId { get; set; }

        [XmlAttribute(AttributeName = "name")]
        public String Name { get; set; }

        [XmlAttribute(AttributeName = "fullname")]
        public String FullName { get; set; }
    }
}

