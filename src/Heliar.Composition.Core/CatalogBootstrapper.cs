using System.ComponentModel.Composition.Hosting;
using System.Reflection;

namespace Heliar.Composition.Core
{
	public sealed class CatalogBootstrapper : BootstrapperBehavior, ICatalogBootstrapper
	{
		public CatalogBootstrapper() : base() { }

		public AggregateCatalog Bootstrap(params Assembly[] assemblies)
		{
			if (this.UseAssemblyNamingConvention)
			{
				this.Catalog.Catalogs.Add(new DirectoryCatalog($"{Assembly.GetExecutingAssembly().GetCodeBaseDirectory()}", AssemblyNamingConvention, this.Conventions));
			}

			foreach (var assembly in assemblies)
			{
				this.Catalog.Catalogs.Add(new AssemblyCatalog(assembly, this.Conventions));
			}

			using (var container = new CompositionContainer(this.Catalog, CompositionOptions.DisableSilentRejection))
			{
				var bootstrappers = container.GetExportedValues<ILibraryDependencyRegistrar>();

				foreach (var bootstrapper in bootstrappers)
				{
					bootstrapper.Register(this.Catalog);
				}

				var appBootstrapper = container.GetExportedValue<IApplicationDependencyRegistrar>();

				appBootstrapper.Register(this.Catalog);
			}


			return this.Catalog;
		}
	}
}