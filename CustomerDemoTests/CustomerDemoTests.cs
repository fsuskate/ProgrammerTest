using System;
using NUnit.Framework;
using CustomerDemo;

namespace CustomerDemoTests
{
    [TestFixture]
    public class CustomerDemoTests
    {
        [Test]
        public void ProgrammerTest()
        {
            var address = new Address("56 Main St", "Mesa", "AZ", "85225");
            var customer = new Customer("John", "Doe", address);
            var company = new Company("Alliance Reservations Network", address);

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

            var savedCustomer = Customer.Find(customer.Id);
            Assert.IsNotNull(savedCustomer);
            Assert.AreSame(customer.Address, address);
            Assert.AreEqual(savedCustomer.Address, address);
            Assert.AreEqual(customer.Id, savedCustomer.Id);
            Assert.AreEqual(customer.FirstName, savedCustomer.FirstName);
            Assert.AreEqual(customer.LastName, savedCustomer.LastName);
            Assert.AreEqual(customer, savedCustomer);
            Assert.AreNotSame(customer, savedCustomer);

            var savedCompany = Company.Find(company.Id);
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
            Assert.IsNull(Customer.Find(customer.Id));

            company.Delete();
            //Assert.IsNullOrEmpty(company.Id);
            //Assert.That(company.Id, Is.Null.Or.Empty);
            Assert.IsNull(Customer.Find(company.Id));
        }
    }
}

