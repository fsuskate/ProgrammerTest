using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomerDemoIOC
{
    class PersistToNull : IPersistData
    {
        public void Delete(Guid id)
        {
            throw new NotImplementedException();
        }

        public object Find(Guid id)
        {
            throw new NotImplementedException();
        }

        public void Save(ObjectBase obj)
        {
            throw new NotImplementedException();
        }
    }
}
