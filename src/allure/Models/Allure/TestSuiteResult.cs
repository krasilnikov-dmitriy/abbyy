using System;
using System.Collections.Generic;
using System.Xml.Schema;
using System.Xml.Serialization;

namespace AllureNUnitAdapter.Models.Allure
{
    [Serializable]
    [XmlType(TypeName = "test-suite-result", Namespace = "urn:model.allure.qatools.yandex.ru")]
    [XmlRoot("test-suite", Namespace = "urn:model.allure.qatools.yandex.ru", IsNullable = false)]
    public class TestSuiteResult
    {
        [XmlElement(ElementName = "name", Form = XmlSchemaForm.Unqualified)]
        public String Name { get; set; }

        [XmlElement(ElementName = "title", Form = XmlSchemaForm.Unqualified)]
        public String Title { get; set; }

        [XmlElement(ElementName = "description", Form = XmlSchemaForm.Unqualified)]
        public Description Description { get; set; } = new Description();

        [XmlArray(ElementName = "test-cases", Form = XmlSchemaForm.Unqualified)]
        [XmlArrayItem(Form = XmlSchemaForm.Unqualified, IsNullable = false)]
        public List<TestCaseResult> TestCaseResults { get; set; } = new List<TestCaseResult>();

        [XmlArray(ElementName = "labels", Form = XmlSchemaForm.Unqualified)]
        [XmlArrayItem(Form = XmlSchemaForm.Unqualified, IsNullable = false)]
        public List<Label> Labels { get; set; } = new List<Label>();

        [XmlAttribute(AttributeName = "start", Form = XmlSchemaForm.Unqualified)]
        public long Start { get; set; }

        [XmlAttribute(AttributeName = "stop", Form = XmlSchemaForm.Unqualified)]
        public long Stop { get; set; }

        [XmlAttribute(AttributeName = "version", Form = XmlSchemaForm.Unqualified)]
        public long Version { get; set; }
    }
}

