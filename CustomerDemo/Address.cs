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
