namespace CustomerDemo
{
    public class Customer : PersistToXMLFile
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
