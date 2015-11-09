using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Samples.Common;

namespace Samples.Data
{
	public class CustomerRepository : ICustomerRepository
	{
		private readonly Dictionary<int, Customer> customers = new Dictionary<int, Customer>();
		private readonly IDbConnection connection = null;
		private readonly ILogger logger = null;

		public CustomerRepository(IConnectionFactory factory, ILogger logger)
		{
			this.connection = factory.Get();
			this.logger = logger;
		}

		public Customer Create()
		{
			var customer = new Customer();
			customer.Id = customers.Any() ? customers.Max(item => item.Key) + 1 : 1;
			customers.Add(customer.Id, customer);
			logger.Debug($"Customer {customer.Id} created");
			return customer;
		}

		public void Delete(int customerId)
		{
			customers.Remove(customerId);
			logger.Debug($"Customer {customerId} deleted");
		}

		public IList<Customer> Read()
		{
			return customers.Select(item => item.Value).ToList();
		}

		public Customer Read(int customerId)
		{
			var entry = customers.FirstOrDefault(item => item.Key == customerId);
			return entry.Value;
		}

		public void Update(Customer customer)
		{
			if (customer == null) throw new ArgumentNullException(nameof(customer));

			if (customers.Any(item => item.Key == customer.Id))
			{
				customers[customer.Id] = customer;
				logger.Debug($"Customer {customer.Id} updated");
			}
		}
	}
}