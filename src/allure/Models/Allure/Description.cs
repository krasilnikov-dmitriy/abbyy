using System;
using System.ComponentModel;
using System.Xml.Serialization;

namespace AllureNUnitAdapter.Models.Allure
{
    [Serializable]
    [XmlType(TypeName = "description-type", Namespace = "urn:model.allure.qatools.yandex.ru")]
    public enum DescriptionType
    {
        [XmlEnum("markdown")]
        Markdown,
        [XmlEnum("text")]
        Text,
        [XmlEnum("html")]
        Html,
    }

    [Serializable]
    [XmlType(Namespace = "urn:model.allure.qatools.yandex.ru")]
    public class Description
    {
        [XmlAttribute(AttributeName = "type")]
        [DefaultValueAttribute(DescriptionType.Text)]
        public DescriptionType Type { get; set; }

        [XmlText]
        public String Value { get; set; }
    }
}

