using System;
using System.IO;
using System.Runtime.Serialization;
using System.Text;
using System.Xml;
using System.Xml.Linq;
using System.Xml.Serialization;

namespace AllureNUnitAdapter.Helpers
{
    public static class XmlHelper
    {
        private static XmlWriterSettings xmlWriterSettings = new XmlWriterSettings
        {
            Encoding = new UTF8Encoding(false, true),
            CloseOutput = true,
            OmitXmlDeclaration = false,
            Indent = true
        };

        public static XElement CreateXmlElement(string text)
        {
            return XDocument.Parse(text).Root;
        }

        public static T Deserialize<T>(string xml) where T : class
        {
            var serializer = new XmlSerializer(typeof(T));
            using (var reader = new StringReader(xml))
            {
                return (T)serializer.Deserialize(reader);
            }
        }

        public static void SaveToFile<T>(T obj, string fileName)
        {
            if (!typeof(T).IsSerializable && !(typeof(ISerializable).IsAssignableFrom(typeof(T))))
                throw new InvalidOperationException("A serializable Type is required");

            var serializer = new XmlSerializer(typeof(T));
            using (XmlWriter xmlWriter = XmlWriter.Create(fileName, xmlWriterSettings))
            {
                serializer.Serialize(xmlWriter, obj);
                xmlWriter.Flush();
            }
        }
    }
}

