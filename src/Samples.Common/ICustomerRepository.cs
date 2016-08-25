using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Samples.Common
{
	public interface ICustomerRepository
	{
		ICustomer Create();
		IList<ICustomer> Read();
		ICustomer Read(int customerId);
		void Update(ICustomer customer);
		void Delete(int customerId);
	}
}