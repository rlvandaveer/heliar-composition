using System.Linq;
using System.Web.Http;
using System.Web.Http.Filters;
using System.Web.Http.Routing;

using Heliar.Composition.Web;

namespace Heliar.Composition.WebApi
{
	/// <summary>
	/// Class that configures the types that resolve dependencies within WebAPI.
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
			this.SetFilterProvider();
			this.SetInlineConstraintResolver();
		}

		private void SetFilterProvider()
		{
			var providers = config.Services.GetFilterProviders().ToList();
			var defaultProvider = providers.Single(p => p is ActionDescriptorFilterProvider);
			config.Services.Remove(typeof(IFilterProvider), defaultProvider);
			config.Services.Add(typeof(IFilterProvider), HeliarCompositionProvider.ApplicationScopedContainer.GetExportedValue<IFilterProvider>());
		}

		private void SetInlineConstraintResolver()
		{
			config.MapHttpAttributeRoutes(HeliarCompositionProvider.ApplicationScopedContainer.GetExportedValue<IInlineConstraintResolver>());
		}

		private void SetResolver()
		{
			var resolver = new CompositionScopedDependencyResolver();
			config.DependencyResolver = resolver;
		}
	}
}