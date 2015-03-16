using System;

namespace CustomerDemoIOC
{
    public interface IPersistData
    {
        void Save(ObjectBase obj);
        void Delete(Guid id);
        object Find(Guid id);
    }
}
