using NUnit.Framework;
using CustomerDemoIOC;

namespace CustomerDemoIOCTests
{
    [TestFixture]
    public class CustomerDemoIOCTests
    {
        [Test]
        public void ProgrammerTest()
        {
            var address = new Address("56 Main St", "Mesa", "AZ", "85225");
            var customer = new Customer(new PersistToNull());
            customer.FirstName = "John";
            customer.LastName = "Doe";
            customer.Address = address;

            var company = new Company(new PersistToNull());
            company.Name = "Alliance Reservations Network";
            company.Address = address;

            //Assert.IsNullOrEmpty(customer.Id);
            Assert.That(customer.Id, Is.Not.Null.Or.Empty);
            customer.Save();
            //Assert.IsNotNullOrEmpty(customer.Id);
            Assert.That(customer.Id, Is.Not.Null.Or.Empty);

            //Assert.IsNullOrEmpty(company.Id);
            Assert.That(company.Id, Is.Not.Null.Or.Empty);
            company.Save();
            //Assert.IsNotNullOrEmpty(company.Id);
            Assert.That(company.Id, Is.Not.Null.Or.Empty);

            var tempCustomer = new Customer();
            var savedCustomer = tempCustomer.Find(customer.Id) as Customer;
            Assert.IsNotNull(savedCustomer);
            Assert.AreSame(customer.Address, address);
            Assert.AreEqual(savedCustomer.Address, address);
            Assert.AreEqual(customer.Id, savedCustomer.Id);
            Assert.AreEqual(customer.FirstName, savedCustomer.FirstName);
            Assert.AreEqual(customer.LastName, savedCustomer.LastName);
            Assert.AreEqual(customer, savedCustomer);
            Assert.AreNotSame(customer, savedCustomer);

            var tempCompany = new Company();
            var savedCompany = tempCompany.Find(company.Id) as Company;
            Assert.IsNotNull(savedCompany);
            Assert.AreSame(company.Address, address);
            Assert.AreEqual(savedCompany.Address, address);
            Assert.AreEqual(company.Id, savedCompany.Id);
            Assert.AreEqual(company.Name, savedCompany.Name);
            Assert.AreEqual(company, savedCompany);
            Assert.AreNotSame(company, savedCompany);

            customer.Delete();
            //Assert.IsNullOrEmpty(customer.Id);
            //Assert.That(customer.Id, Is.Null.Or.Empty);
            Assert.IsNull(tempCustomer.Find(customer.Id));

            company.Delete();
            //Assert.IsNullOrEmpty(company.Id);
            //Assert.That(company.Id, Is.Null.Or.Empty);
            Assert.IsNull(tempCustomer.Find(company.Id));
        }
    }
}
