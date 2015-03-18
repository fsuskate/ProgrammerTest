using System;
using System.IO;
using System.Xml.Serialization;

namespace CustomerDemo
{
    /// <summary>
    /// Find extensions that leverage the generic Find in PersistToXMLFile so that we can
    /// return the strongly typed subclass versions
    /// </summary>
    public static class PersistToXMLFileExtensions
    {
        public static Customer FindEx(this Customer customer, Guid id)
        {
            return PersistToXMLFile.Find<Customer>(id);            
        }

        public static Company FindEx(this Company company, Guid id)
        {
            return PersistToXMLFile.Find<Company>(id);
        }
    }
}
