using System.ComponentModel.Composition.Hosting;
using System.ComponentModel.Composition.Primitives;
using System.Linq;
using System.Reflection;

namespace Heliar.Composition.Core
{
	/// <summary>
	/// A class that bootstraps a MEF <see cref="CompositionContainer"/> automatically in an application using types that implement <see cref="ILibraryDependencyRegistrar"/>s
	/// and <see cref="IApplicationDependencyRegistrar"/>s. This class cannot be inherited.
	/// </summary>
	public sealed class ContainerBootstrapper : BootstrapperBehavior, IContainerBootstrapper
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="ContainerBootstrapper"/> class.
		/// </summary>
		public ContainerBootstrapper() : base() { }

		/// <summary>
		/// Initializes a new instance of the <see cref="ContainerBootstrapper" /> class.
		/// </summary>
		/// <param name="assemblyNamingConvention">The assembly naming convention to use to find assemblies.</param>
		/// <param name="catalogs">An optional list of pre-wired-up catalogs.</param>
		public ContainerBootstrapper(string assemblyNamingConvention, params ComposablePartCatalog[] catalogs) : base(assemblyNamingConvention, catalogs) { }

		/// <summary>
		/// Bootstraps a <see cref="CompositionContainer"/> by convention and/or specified params.
		/// </summary>
		/// <param name="assemblies">An optional list of assemblies.</param>
		/// <returns>System.ComponentModel.Composition.Hosting.CompositionContainer.</returns>
		public CompositionContainer Bootstrap(params Assembly[] assemblies)
		{
			base.BootstrapAssemblies(assemblies);

			var container = new CompositionContainer(this.Catalog, CompositionOptions.DisableSilentRejection | CompositionOptions.IsThreadSafe);

			var bootstrapExports = container.GetExports<ILibraryDependencyRegistrar>();

			foreach (var bootstrapExport in bootstrapExports)
			{
				bootstrapExport.Value.Register(this.Catalog);
			}

			var appBootstrapperExports = container.GetExports<IApplicationDependencyRegistrar>();
			var count = appBootstrapperExports.Count();
			if (count == 1)
			{
				var appBootstrapper = appBootstrapperExports.Single().Value;
				appBootstrapper.Register(this.Catalog);
			}
			else
			{
				throw new ApplicationDependencyRegistrarImplementationException(count);
			}

			return container;
		}
	}
}