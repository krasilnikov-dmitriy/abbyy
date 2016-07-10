using System;
using System.Xml.Serialization;

namespace AllureNUnitAdapter.Models.Allure
{
    [Serializable]
    [XmlType(Namespace = "urn:model.allure.qatools.yandex.ru")]
    public class Attachment
    {
        [XmlAttribute(AttributeName = "title")]
        public string Title { get; set; }

        [XmlAttribute(AttributeName = "source")]
        public string Source { get; set; }

        [XmlAttribute(AttributeName = "type")]
        public string Type { get; set; }
    }
}

