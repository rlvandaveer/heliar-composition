using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Samples.Common
{
	public interface ICustomer
	{
		int Id { get; set; }
		string FirstName { get; set; }
		string LastName { get; set; }
		DateTime DateOfBirth { get; set; }
	}
}
