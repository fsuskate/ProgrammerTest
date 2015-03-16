using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomerDemo
{
    public class Company : SaveToFileObjectBase
    {
        public string Name { get; set; }
        public Address Address { get; set; }

        public Company() { }

        public Company(string name, Address address)
        {
            Name = name;
            Address = address;
        }

        public override bool Equals(object obj)
        {
            Company other = obj as Company;
            if (other != null && Name.Equals(other.Name) && Address.Equals(other.Address))
            {
                return true;
            }
            return false;
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
    }
}
