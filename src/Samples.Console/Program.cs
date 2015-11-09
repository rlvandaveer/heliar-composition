using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Heliar.Composition.Core;

using Samples.Common;
using Samples.Data;

namespace Samples.ConsoleApp
{
	class Program
	{
		static void Main(string[] args)
		{
			var boot = new ContainerBootstrapper();
			var container = boot.Bootstrap();

			ILogger logger = container.GetExportedValue<ILogger>();
			logger.Debug("Main");

			ICustomerRepository rep = container.GetExportedValue<ICustomerRepository>();
			var cust = rep.Create();
			cust.FirstName = "Robb";
			cust.LastName = "Vandaveer";
			cust.DateOfBirth = new DateTime(1973, 1, 1);
		}
	}
}
