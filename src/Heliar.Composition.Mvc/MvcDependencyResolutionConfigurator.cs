using System;
using System.Linq;
using System.Web.Mvc;

using Heliar.Composition.Web;

namespace Heliar.Composition.Mvc
{
	/// <summary>
	/// Class that configures dependency resolution for MVC.
	/// </summary>
	public class MvcDependencyResolutionConfigurator : IDependencyResolutionConfigurator
	{
		/// <summary>
		/// Configures dependency resolution.
		/// </summary>
		public void Configure()
		{
			this.SetResolver();
			this.SetFilterProviders();
			this.SetModelBinderProviders();
		}

		/// <summary>
		/// Sets the model binder providers.
		/// </summary>
		private void SetModelBinderProviders()
		{
			ModelBinderProviders.BinderProviders.Add(new CompositionScopedModelBinderProvider());
		}

		/// <summary>
		/// Sets the filter providers.
		/// </summary>
		private void SetFilterProviders()
		{
			FilterProviders.Providers.Remove(FilterProviders.Providers.OfType<FilterAttributeFilterProvider>().SingleOrDefault());
			FilterProviders.Providers.Add(new CompositionScopedFilterAttributeFilterProvider());
		}

		/// <summary>
		/// Sets the dependency resolver for MVC.
		/// </summary>
		private void SetResolver()
		{
			var resolver = new CompositionScopedDependencyResolver();
			DependencyResolver.SetResolver(resolver);
		}
	}
}