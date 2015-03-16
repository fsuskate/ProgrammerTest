using System;
using System.IO;
using System.Reflection;
using System.Xml;
using System.Xml.Serialization;

namespace CustomerDemo
{
    public abstract class PersistToXMLFile : IPersistData
    {
        public Guid Id { get; set; }

        /// <summary>
        /// New objects get their own unique identifier. There was no specified constraint on the Id property
        /// so I went with Guid which is easy to make unique.
        /// </summary>
        public PersistToXMLFile()
        {
            Id = Guid.NewGuid();
        }

        /// <summary>
        /// Save to a xml files with name constructed from the Id.
        /// </summary>
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

        /// <summary>
        /// This Generic version can find any subclass of SaveToFileObjectBase. The caller must specify a type
        /// the derives from SaveToFileObjectBase. There is a seperate test case to test this version of Find
        /// since the original test did not allow for a Generic version of Find.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="id"></param>
        /// <returns></returns>
        public static T Find<T>(Guid id) where T : PersistToXMLFile
        {
            string fileName = GetFilePath(id);
            if (!string.IsNullOrEmpty(fileName) && File.Exists(fileName))
            {
                XmlSerializer xmlSerializer = new XmlSerializer(typeof(T));
                using (StreamReader readfile = new StreamReader(fileName))
                {
                    return xmlSerializer.Deserialize(readfile) as T;
                }
            }
            return null;
        }

        /// <summary>
        /// Find implemented exactly as requested in test. The constraints specified forced me to dynamically 
        /// determine the type from the XML file at runtime. This code is tightly coupled to the implementation
        /// of the models since it has to know about the type that the Assembly contains in order to return
        /// a properly typed object.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
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
            string fileName = PersistToXMLFile.GetFilePath(Id);
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
