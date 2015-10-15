using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace Heliar.Composition.Web
{
	public static class HttpContextExtensions
	{
		/// <summary>
		/// Gets the <see cref="Assembly"/> for the web application associated with the current <see cref="HttpContext"/>.
		/// </summary>
		/// <param name="context">The context.</param>
		/// <returns>System.Reflection.Assembly.</returns>
		public static Assembly GetWebApplicationAssembly(this HttpContext context) => context.ApplicationInstance.GetType().BaseType.Assembly;
	}
}