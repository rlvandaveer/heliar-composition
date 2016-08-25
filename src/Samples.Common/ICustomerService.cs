using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Samples.Common
{
	public interface ICustomerService
	{
		ICustomer GetCustomer(int Id);
		IList<ICustomer> GetCustomers();
		ICustomer CreateCustomer(string firstName, string lastName, DateTime dob);
		void UpdateCustomer(ICustomer customer);
	}
}