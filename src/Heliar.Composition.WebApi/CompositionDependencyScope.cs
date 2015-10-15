using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.ComponentModel.Composition.Hosting;
using System.Web.Http.Dependencies;

using Heliar.Composition.Web;

namespace Heliar.Composition.WebApi
{
	/// <summary>
	/// Class responsible for composing dependencies scoped to a single web request.
	/// </summary>
	public class CompositionDependencyScope : IDependencyScope
	{
		/// <summary>
		/// The composition container
		/// </summary>
		private CompositionContainer container;

		/// <summary>
		/// Initializes a new instance of the <see cref="CompositionDependencyScope"/> class.
		/// </summary>
		/// <param name="container">The container.</param>
		public CompositionDependencyScope(CompositionContainer container)
		{
			this.container = container;
		}

		/// <summary>
		/// Retrieves a service from the scoped composition container.
		/// </summary>
		/// <param name="serviceType">The <see cref="Type"/> to retrieve an instance of.</param>
		/// <returns>The retrieved service.</returns>
		public object GetService(Type serviceType)
		{
			return this.container.GetExportedValueOrDefault<object>(AttributedModelServices.GetContractName(serviceType));
		}

		/// <summary>
		/// Retrieves a collection of services from the scoped composition container.
		/// </summary>
		/// <param name="serviceType">The collection of <see cref="Type"/>s to retrieve instances of.</param>
		/// <returns>The retrieved collection of services.</returns>
		public IEnumerable<object> GetServices(Type serviceType)
		{
			return this.container.GetExportedValues<object>(AttributedModelServices.GetContractName(serviceType));
		}

		/// <summary>
		/// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
		/// </summary>
		public void Dispose()
		{
			Dispose(true);
		}

		/// <summary>
		/// Releases unmanaged and - optionally - managed resources.
		/// </summary>
		/// <param name="disposing"><c>true</c> to release both managed and unmanaged resources; <c>false</c> to release only unmanaged resources.</param>
		protected void Dispose(bool disposing)
		{
			if (disposing)
			{
				CompositionScopeDisposer.DisposeCompositionScope();
			}
		}
	}
}