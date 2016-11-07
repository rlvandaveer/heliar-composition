using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Samples.Common;

namespace Samples.MvcApp.Controllers
{
	public class CustomersController : Controller
	{
		private ICustomerService service;

		public CustomersController(ICustomerService service)
		{
			this.service = service;
		}

		// GET: Customer
		public ActionResult Index()
		{
			var customers = this.service.GetCustomers();
			return View(customers);
		}
	}
}