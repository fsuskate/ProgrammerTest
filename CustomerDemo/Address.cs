using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomerDemo
{
    public class Address : SaveToFileObjectBase
    {
        public string Street { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string ZipCode { get; set; }

        public Address() { }

        public Address(string street, string city, string state, string zipCode)
        {
            Street = street;
            City = city;
            State = state;
            ZipCode = zipCode;
        }

        public static Address Find(Guid id)
        {
            return SaveToFileObjectBase.FindBase(id, typeof(Address)) as Address;
        }

        public override bool Equals(object obj)
        {
            Address other = obj as Address;
            if (other != null && Street.Equals(other.Street) && City.Equals(other.City) && State.Equals(other.State) && ZipCode.Equals(other.ZipCode))
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
