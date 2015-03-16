namespace CustomerDemo
{
    class Program
    {
        static void Main(string[] args)
        {
            var address = new Address("56 Main St", "Mesa", "AZ", "85225");
            var customer = new Customer("John", "Doe", address);
            var company = new Company("Alliance Reservations Network", address);

            customer.Save();
            company.Save();

            var savedCustomer = Customer.Find(customer.Id);

            var savedCustomer2 = SaveToFileObjectBase.Find<Customer>(customer.Id);

            var savedCompany = Company.Find(company.Id);
            customer.Delete();

            company.Delete();
        }
    }
}
