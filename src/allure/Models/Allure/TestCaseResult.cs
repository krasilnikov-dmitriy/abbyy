using System;
using System.Collections.Generic;
using System.Xml.Schema;
using System.Xml.Serialization;

namespace AllureNUnitAdapter.Models.Allure
{
    [Serializable]
    [XmlType(Namespace = "urn:model.allure.qatools.yandex.ru")]
    public enum TestResult
    {
        [XmlEnum("failed")]
        Failed,
        [XmlEnum("broken")]
        Broken,
        [XmlEnum("passed")]
        Passed,
        [XmlEnum("canceled")]
        Canceled,
        [XmlEnum("pending")]
        Pending,
    }

    [Serializable]
    [XmlType(TypeName = "test-case", Namespace = "urn:model.allure.qatools.yandex.ru")]
    public class TestCaseResult
    {
        [XmlElement(ElementName = "name", Form = XmlSchemaForm.Unqualified)]
        public String Name { get; set; }

        [XmlElement(ElementName = "title", Form = XmlSchemaForm.Unqualified)]
        public String Title { get; set; }

        [XmlElement(ElementName = "description", Form = XmlSchemaForm.Unqualified)]
        public Description Description { get; set; }

        [XmlElement(ElementName = "failure", Form = XmlSchemaForm.Unqualified)]
        public Failure Failure { get; set; }

        [XmlArray(ElementName = "steps", Form = XmlSchemaForm.Unqualified)]
        [XmlArrayItem(Form = XmlSchemaForm.Unqualified, IsNullable = false)]
        public List<Step> Steps { get; set; } = new List<Step>();

        [XmlArray(ElementName = "attachments", Form = XmlSchemaForm.Unqualified)]
        [XmlArrayItem(Form = XmlSchemaForm.Unqualified, IsNullable = false)]
        public List<Attachment> Attachments { get; set; } = new List<Attachment>();

        [XmlArray(ElementName = "labels", Form = XmlSchemaForm.Unqualified)]
        [XmlArrayItem(Form = XmlSchemaForm.Unqualified, IsNullable = false)]
        public List<Label> Labels { get; set; } = new List<Label>();

        [XmlArray(ElementName = "parameters", Form = XmlSchemaForm.Unqualified)]
        [XmlArrayItem(Form = XmlSchemaForm.Unqualified, IsNullable = false)]
        public List<Parameter> Parameters { get; set; } = new List<Parameter>();

        [XmlAttribute(AttributeName = "start", Form = XmlSchemaForm.Unqualified)]
        public long Start { get; set; }

        [XmlAttribute(AttributeName = "stop", Form = XmlSchemaForm.Unqualified)]
        public long Stop { get; set; }

        [XmlAttribute(AttributeName = "status", Form = XmlSchemaForm.Unqualified)]
        public TestResult Status { get; set; }
    }
}

