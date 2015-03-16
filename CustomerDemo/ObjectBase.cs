using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Serialization;

namespace CustomerDemo
{
    public class SaveToFileObjectBase : IPersistData
    {
        public Guid Id { get; set; }

        public SaveToFileObjectBase()
        {
            Id = Guid.NewGuid();
        }

        public void Save()
        {
            XmlSerializer xmlSerializer = new XmlSerializer(this.GetType());
            using (StreamWriter outfile = new StreamWriter(GetFilePath()))
            {
                xmlSerializer.Serialize(outfile, this);
            }
        }

        public void Delete()
        {
            File.Delete(GetFilePath());
        }

        public static dynamic Find(Guid id)
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
                        if (type == typeof(Customer))
                        {
                            return xmlSerializer.Deserialize(readfile) as Customer;
                        }
                        else if (type == typeof(Company))
                        {
                            return xmlSerializer.Deserialize(readfile) as Company;
                        }                        
                    }
                }
            }
            return null;
        }

        public static string GetFilePath(Guid id)
        {
            return Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) + "\\" + id.ToString() + ".xml";
        }

        protected string GetFilePath()
        {
            string fileName = SaveToFileObjectBase.GetFilePath(Id);
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
