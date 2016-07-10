using System;
using System.Xml.Serialization;

namespace AllureNUnitAdapter.Models.Allure
{
    [Serializable]
    [XmlType(TypeName = "parameter-kind", Namespace = "urn:model.allure.qatools.yandex.ru")]
    public enum ParameterKind
    {
        [XmlEnum("system-property")]
        Argument,
        [XmlEnum("system-property")]
        SystemProperty,
        [XmlEnum("environment-variable")]
        EnvironmentVariable,
    }

    [Serializable]
    [XmlType(Namespace = "urn:model.allure.qatools.yandex.ru")]
    public class Parameter
    {
        [XmlAttribute(AttributeName = "name")]
        public String Name { get; set; }

        [XmlAttribute(AttributeName = "value")]
        public String Value { get; set; }

        [XmlAttribute(AttributeName = "kind")]
        public ParameterKind Kind { get; set; }
    }
}

