using System;
using System.Xml.Serialization;

namespace AllureNUnitAdapter.Models.Allure
{
    [Serializable]
    [XmlType(Namespace = "urn:model.allure.qatools.yandex.ru")]
    public class Failure
    {
        [XmlElement(ElementName = "message", Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public String Message { get; set; }

        [XmlElement(ElementName = "stacktrace", Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public String StackTrace { get; set; }
    }
}

