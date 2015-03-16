using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomerDemoIOC
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

            var savedCustomer = customer.Find() as Customer;

            var savedCompany = company.Find();

            var savedCustemer2 = new Customer();
            savedCustemer2 = savedCustemer2.Find(savedCustomer.Id) as Customer;


            customer.Delete();

            company.Delete();
        }
    }
}
