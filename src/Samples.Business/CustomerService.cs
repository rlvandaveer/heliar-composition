using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Samples.Common;

namespace Samples.Business
{
	public class CustomerService : ICustomerService
	{
		private ICustomerRepository repository;

		public CustomerService(ICustomerRepository repository)
		{
			this.repository = repository;
		}

		public ICustomer GetCustomer(int Id)
		{
			throw new NotImplementedException();
		}

		public IList<ICustomer> GetCustomers()
		{
			return this.repository.Read();
		}

		public ICustomer CreateCustomer(string firstName, string lastName, DateTime dob)
		{
			throw new NotImplementedException();
		}

		public void UpdateCustomer(ICustomer customer)
		{
			throw new NotImplementedException();
		}
	}
}