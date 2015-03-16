﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomerDemo
{
    public class Customer : SaveToFileObjectBase
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public Address Address { get; set; }

        public Customer() {}

        public Customer(string firstName, string lastName, Address address)
        {
            FirstName = firstName;
            LastName = lastName;
            Address = address;
        }

        public static Customer Find(Guid id)
        {
            return SaveToFileObjectBase.FindBase(id, typeof(Customer)) as Customer;
        }

        public override bool Equals(object obj)
        {
            Customer other = obj as Customer;
            if (other != null && FirstName.Equals(other.FirstName) && LastName.Equals(other.LastName) && Address.Equals(other.Address))
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