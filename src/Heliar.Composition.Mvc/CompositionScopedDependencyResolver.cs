using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Web.Mvc;

using Heliar.Composition.Web;

namespace Heliar.Composition.Mvc
{
	/// <summary>
	/// Resolves shared or web request scoped dependencies using a composition provider.
	/// </summary>
	public class CompositionScopedDependencyResolver : IDependencyResolver
	{
		/// <summary>
		/// Resolves singly registered services that support arbitrary object creation using the current
		/// composition provider.
		/// </summary>
		/// <param name="serviceType">The type of the requested service or object.</param>
		/// <returns>The requested service or object.</returns>
		public object GetService(Type serviceType)
		{
			return HeliarCompositionProvider.Current.GetExportedValueOrDefault<object>(AttributedModelServices.GetContractName(serviceType));
		}

		/// <summary>
		/// Resolves multiply registered services using the current composition provider.
		/// </summary>
		/// <param name="serviceType">The type of the requested services.</param>
		/// <returns>The requested services.</returns>
		public IEnumerable<object> GetServices(Type serviceType)
		{
			return HeliarCompositionProvider.Current.GetExportedValues<object>(AttributedModelServices.GetContractName(serviceType));
		}
	}
}