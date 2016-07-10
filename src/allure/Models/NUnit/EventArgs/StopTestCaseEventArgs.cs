using System;
using System.Xml.Serialization;

namespace AllureNUnitAdapter.Models.NUnit.EventArgs
{
    [Serializable]
    [XmlRoot(ElementName = "test-case")]
    public class StopTestCaseEventArgs
    {
        [XmlAttribute(AttributeName = "id")]
        public String Id { get; set; }

        [XmlAttribute(AttributeName = "parentId")]
        public String ParentId { get; set; }

        [XmlAttribute(AttributeName = "name")]
        public String Name { get; set; }

        [XmlAttribute(AttributeName = "fullname")]
        public String FullName { get; set; }

        [XmlAttribute(AttributeName = "methodname")]
        public String MethodName { get; set; }

        [XmlAttribute(AttributeName = "classname")]
        public String ClassName { get; set; }

        [XmlAttribute(AttributeName = "result")]
        public String Result { get; set; }

        [XmlAttribute(AttributeName = "start-time")]
        public string StartTimeValue { get; set; }

        [XmlIgnore]
        public DateTime StartTime { get { return DateTime.Parse(StartTimeValue); } }

        [XmlAttribute(AttributeName = "end-time")]
        public String EndTimeValue { get; set; }

        [XmlIgnore]
        public DateTime EndTime { get { return DateTime.Parse(EndTimeValue); } }

        [XmlElement(ElementName="failure")]
        public Failure Failure { get; set; }
    }

    [Serializable]
    public class Failure
    {
        [XmlElement(ElementName = "message")]
        public String Message { get; set; }

        [XmlElement(ElementName = "stack-trace")]
        public String StackTrace { get; set; }
    }
}

