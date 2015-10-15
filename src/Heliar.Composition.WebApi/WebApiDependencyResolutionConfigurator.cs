using System.Linq;
using System.Web.Http;
using System.Web.Http.Filters;

using Heliar.Composition.Web;

namespace Heliar.Composition.WebApi
{
	/// <summary>
	/// Class that configures WebAPI's dependency resolver.
	/// </summary>
	public class WebApiDependencyResolutionConfigurator : IDependencyResolutionConfigurator
	{
		HttpConfiguration config = GlobalConfiguration.Configuration;
		
		/// <summary>
		/// Configures WebAPI dependency resolution.
		/// </summary>
		public void Configure()
		{
			this.SetResolver();
			//var providers = config.Services.GetFilterProviders().ToList();
			//var defaultProvider = providers.Single(i => i is ActionDescriptorFilterProvider);
			//config.Services.Remove(typeof(IFilterProvider), defaultProvider);

			//config.Services.Add(typeof(IFilterProvider), HeliarCompositionProvider.Current.

		}

		private void SetResolver()
		{
			var resolver = new CompositionScopedDependencyResolver();
			config.DependencyResolver = resolver;
		}
	}
}