using System;
using System.Xml.Schema;
using System.Xml.Serialization;

namespace AllureNUnitAdapter.Models.Allure
{
    [Serializable]
    [XmlType(Namespace = "urn:model.allure.qatools.yandex.ru")]
    public class Step
    {
        [XmlElement(ElementName = "name", Form = XmlSchemaForm.Unqualified)]
        public String Name { get; set; }

        [XmlElement(ElementName = "title", Form = XmlSchemaForm.Unqualified)]
        public String Title { get; set; }

        [XmlArray(ElementName = "steps", Form = XmlSchemaForm.Unqualified)]
        [XmlArrayItem(Form = XmlSchemaForm.Unqualified, IsNullable = false)]
        public Step[] Steps { get; set; }

        [XmlArray(ElementName = "attachments", Form = XmlSchemaForm.Unqualified)]
        [XmlArrayItem(Form = XmlSchemaForm.Unqualified, IsNullable = false)]
        public Attachment[] Attachments { get; set; }

        [XmlElement(ElementName = "start", Form = XmlSchemaForm.Unqualified)]
        public long Start { get; set; }

        [XmlElement(ElementName = "stop", Form = XmlSchemaForm.Unqualified)]
        public long Stop { get; set; }
    }
}

