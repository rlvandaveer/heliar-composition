using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Web.Http;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;

using Heliar.Composition.Web;

namespace Heliar.Composition.WebApi
{
	/// <summary>
	/// Class uses <see cref="HeliarCompositionProvider"/>'s current scope to resolve filter attributes.
	/// </summary>
	public class CompositionScopedFilterProvider : ActionDescriptorFilterProvider, IFilterProvider
	{
		/// <summary>
		/// Gets the action filters.
		/// </summary>
		/// <param name="configuration">The configuration.</param>
		/// <param name="actionDescriptor">The action descriptor.</param>
		/// <returns>IEnumerable&lt;FilterInfo&gt;.</returns>
		public new IEnumerable<FilterInfo> GetFilters(HttpConfiguration configuration, HttpActionDescriptor actionDescriptor)
		{
			var filters = base.GetFilters(configuration, actionDescriptor).ToArray();
			this.ComposeFilters(filters);
			return filters;
		}

		/// <summary>
		/// Composes the filters.
		/// </summary>
		/// <param name="filters">The filters.</param>
		private void ComposeFilters(FilterInfo[] filters)
		{
			HeliarCompositionProvider.Current.ComposeParts(filters);
		}
	}
}