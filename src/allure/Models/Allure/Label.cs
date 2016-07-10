using System;
using System.Xml.Serialization;

namespace AllureNUnitAdapter.Models.Allure
{
    [Serializable]
    [XmlType(Namespace = "urn:model.allure.qatools.yandex.ru")]
    public class Label
    {
        [XmlAttribute(AttributeName = "name")]
        public String Name { get; set; }

        [XmlAttribute(AttributeName = "value")]
        public String Value { get; set; }
    }
}

