using System;
using System.IO;
using System.Reflection;
using System.Xml;
using System.Xml.Serialization;

namespace CustomerDemoIOC
{
    public class PersistToXMLFile : IPersistData
    {
        /// <summary>
        /// Save to a xml files with name constructed from the Id.
        /// </summary>
        public void Save(ObjectBase obj)
        {
            XmlSerializer xmlSerializer = new XmlSerializer(obj.GetType());
            using (StreamWriter outfile = new StreamWriter(GetFilePath(obj.Id)))
            {
                xmlSerializer.Serialize(outfile, obj);
            }
        }

        public void Delete(Guid id)
        {
            File.Delete(GetFilePath(id));
        }

        /// <summary>
        /// Find implemented exactly as requested in test. The constraints specified forced me to dynamically 
        /// determine the type from the XML file at runtime. This code is tightly coupled to the implementation
        /// of the models since it has to know about the type that the Assembly contains in order to return
        /// a properly typed object.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public object Find(Guid id)
        {
            string fileName = GetFilePath(id);
            if (!string.IsNullOrEmpty(fileName) && File.Exists(fileName))
            {
                Type type = GetTypeFromXML(fileName);
                if (type != null)
                {
                    XmlSerializer xmlSerializer = new XmlSerializer(type);
                    using (StreamReader readfile = new StreamReader(fileName))
                    {
                        return xmlSerializer.Deserialize(readfile);                                                
                    }
                }
            }
            return null;
        }

        public static string GetFilePath(Guid id)
        {
            return Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) + "\\" + id.ToString() + ".xml";
        }

        protected string GetFilePathInternal(Guid id)
        {
            string fileName = PersistToXMLFile.GetFilePath(id);
            if (string.IsNullOrEmpty(fileName))
            {
                throw new FileNotFoundException();
            }
            return fileName;
        }

        protected static Type GetTypeFromXML(string fileName)
        {
            XmlDocument xmlDocument = new XmlDocument();
            xmlDocument.Load(fileName);

            Type[] types = Assembly.GetExecutingAssembly().GetTypes();
            foreach (Type t in types)
            {
                if (t.Name.IndexOf(xmlDocument.DocumentElement.Name) > -1)
                {
                    return t;                    
                }
            }
            return null;
        }        
    }
}
