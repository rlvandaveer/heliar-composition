using System.ComponentModel.Composition.Hosting;
using System.ComponentModel.Composition.Primitives;
using System.Reflection;

namespace Heliar.Composition.Core
{
	/// <summary>
	/// A class that bootstraps a MEF <see cref="CompositionContainer"/> automatically in an application using types that implement <see cref="ILibraryDependencyRegistrar"/>s
	/// and <see cref="IApplicationDependencyRegistrar"/>s. This class cannot be inherited.
	/// </summary>
	/// <remarks>Use this type if you are going to build a <see cref="CompositionContainer"/> elsewhere.</remarks>
	public sealed class CatalogBootstrapper : BootstrapperBehavior, ICatalogBootstrapper
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="CatalogBootstrapper"/> class.
		/// </summary>
		public CatalogBootstrapper() : base() { }

		/// <summary>
		/// Initializes a new instance of the <see cref="BootstrapperBehavior" /> class.
		/// </summary>
		/// <param name="assemblyNamingConvention">The assembly naming convention to use to find assemblies.</param>
		/// <param name="catalogs">An optional list of pre-wired-up catalogs.</param>
		public CatalogBootstrapper(string assemblyNamingConvention, params ComposablePartCatalog[] catalogs) : base(assemblyNamingConvention, catalogs) { }

		/// <summary>
		/// Bootstraps an <see cref="AggregateCatalog" /> by convention and/or specified params.
		/// </summary>
		/// <param name="assemblies">The assemblies that contain the desired dependencies.</param>
		/// <returns>An <see cref="AggregateCatalog" /> containing dependencies.</returns>
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