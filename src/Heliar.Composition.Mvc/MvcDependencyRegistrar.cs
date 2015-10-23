using System.ComponentModel.Composition;
using System.ComponentModel.Composition.Hosting;
using System.ComponentModel.Composition.Registration;
using System.Reflection;
using System.Web.Mvc;

using Heliar.Composition.Core;
using Heliar.Composition.Web;

namespace Heliar.Composition.Mvc
{
	/// <summary>
	/// Class responsible for registering MVC dependencies.
	/// </summary>
	public class MvcDependencyRegistrar : ILibraryDependencyRegistrar
	{
		/// <summary>
		/// Registers the specified catalog.
		/// </summary>
		/// <param name="catalog">The catalog.</param>
		public void Register(AggregateCatalog catalog)
		{
			var conventions = new RegistrationBuilder();
			conventions.ForTypesDerivedFrom<IController>()
				.SetCreationPolicy(CreationPolicy.NonShared)
				.Export();
			conventions.ForTypesDerivedFrom<FilterAttribute>()
				.SetCreationPolicy(CreationPolicy.NonShared)
				.Export();
			conventions.ForTypesDerivedFrom<IModelBinder>()
				.SetCreationPolicy(CreationPolicy.NonShared)
				.Export();
			var mvcCatalog = new AssemblyCatalog(HeliarCompositionProvider.WebApplicationAssembly, conventions);
			catalog.Catalogs.Add(mvcCatalog);
		}
	}
}