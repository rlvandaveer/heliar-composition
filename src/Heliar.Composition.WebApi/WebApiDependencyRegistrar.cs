using System.ComponentModel.Composition;
using System.ComponentModel.Composition.Hosting;
using System.ComponentModel.Composition.Registration;
using System.Web.Http.Controllers;

using Heliar.Composition.Core;
using Heliar.Composition.Web;

namespace Heliar.Composition.WebApi
{
	/// <summary>
	/// Class that registers Web API dependencies.
	/// </summary>
	public class WebApiDependencyRegistrar : ILibraryDependencyRegistrar
	{
		/// <summary>
		/// Registers the specified catalog.
		/// </summary>
		/// <param name="catalog">The catalog.</param>
		public void Register(AggregateCatalog catalog)
		{
			var conventions = new RegistrationBuilder();
			conventions.ForTypesDerivedFrom<IHttpController>()
				.SetCreationPolicy(CreationPolicy.NonShared)
				.Export();
			var apiCatalog = new AssemblyCatalog(HeliarCompositionProvider.WebApplicationAssembly, conventions);
			catalog.Catalogs.Add(apiCatalog);
		}
	}
}