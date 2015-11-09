using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Samples.Data
{
	public interface ICustomerRepository
	{
		Customer Create();
		IList<Customer> Read();
		Customer Read(int customerId);
		void Update(Customer customer);
		void Delete(int customerId);
	}
}
