using System;
using System.Xml.Serialization;

namespace AllureNUnitAdapter.Models.NUnit.EventArgs
{
    [Serializable]
    public enum TestSuiteType
    {
        Assembly,
        GenericFixture,
        ParameterizedFixture,
        Theory,
        TestSuite,
        TestFixture,
        TestMethod,
        GenericMethod,
        ParameterizedMethod
    }

    [Serializable]
    [XmlRoot(ElementName = "test-suite")]
    public class StopTestSuiteEventArgs
    {
        [XmlAttribute(AttributeName = "id")]
        public String Id { get; set; }

        [XmlAttribute(AttributeName = "type")]
        public TestSuiteType Type { get; set; }

        [XmlAttribute(AttributeName = "parentId")]
        public String ParentId { get; set; }

        [XmlAttribute(AttributeName = "name")]
        public String Name { get; set; }

        [XmlAttribute(AttributeName = "fullname")]
        public String FullName { get; set; }

        [XmlAttribute(AttributeName = "start-time")]
        public string StartTimeValue { get; set; }

        [XmlIgnore]
        public DateTime StartTime { get { return DateTime.Parse(StartTimeValue); } }

        [XmlAttribute(AttributeName = "end-time")]
        public String EndTimeValue { get; set; }

        [XmlIgnore]
        public DateTime EndTime { get { return DateTime.Parse(EndTimeValue); } }


    }
}

