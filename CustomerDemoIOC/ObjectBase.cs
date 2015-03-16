using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomerDemoIOC
{
    public abstract class ObjectBase
    {
        protected IPersistData _persistData;

        public Guid Id { get; set; }

        /// <summary>
        /// New objects get their own unique identifier. There was no specified constraint on the Id property
        /// so I went with Guid which is easy to make unique.
        /// </summary>
        public ObjectBase()
        {
            Id = Guid.NewGuid();
            _persistData = new PersistToXMLFile();
        }

        public ObjectBase(IPersistData persistData)
        {
            Id = Guid.NewGuid();

            if (persistData == null)
            {
                throw new System.Exception("Must pass a valid persistData object");
            }

            _persistData = persistData;
        }

        public void Save()
        {
            _persistData.Save(this);
        }

        public void Delete()
        {
            _persistData.Delete(Id);
        }   
        
        public object Find()
        {
            return _persistData.Find(Id);
        }     

        public object Find(Guid id)
        {
            return _persistData.Find(id);
        }
    }
}
