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
		public ICustomer GetCustomer(int Id)
		{
			throw new NotImplementedException();
		}

		public IList<ICustomer> GetCustomers()
		{
			throw new NotImplementedException();
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