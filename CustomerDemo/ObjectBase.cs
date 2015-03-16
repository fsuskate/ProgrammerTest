using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
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

        public static object FindBase(Guid id, Type type)
        {
            string fileName = GetFilePath(id);
            if (!string.IsNullOrEmpty(fileName) && File.Exists(fileName))
            {
                XmlSerializer xmlSerializer = new XmlSerializer(type);
                using (StreamReader readfile = new StreamReader(fileName))
                {
                    return xmlSerializer.Deserialize(readfile);
                }
            }
            return null;
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

        public static string GetFilePath(Guid id)
        {
            return Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) + "\\" +  id.ToString() + ".xml";
        }
    }
}
