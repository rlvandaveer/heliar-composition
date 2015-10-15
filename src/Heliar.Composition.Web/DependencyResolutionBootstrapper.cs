using System.ComponentModel.Composition.Hosting;
using System.ComponentModel.Composition;
using System.Reflection;

using Heliar.Composition.Core;

namespace Heliar.Composition.Web
{
	/// <summary>
	/// Class that bootstraps all of the dependency resolvers for the current application by discovering all of the <see cref="IDependencyResolutionConfigurator"/>s
	/// and executing them.
	/// </summary>
	public class DependencyResolutionBootstrapper : BootstrapperBehavior, IDependencyResolutionBootstrapper
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="DependencyResolutionBootstrapper"/> class.
		/// </summary>
		public DependencyResolutionBootstrapper()
		{
			this.AssemblyNamingConvention = "Heliar*.dll";
			this.Conventions.ForTypesDerivedFrom<IDependencyResolutionConfigurator>()
				.SetCreationPolicy(CreationPolicy.Shared)
				.ExportInterfaces()
				.Export();
		}

		/// <summary>
		/// Bootstraps the dependency resolvers for the application.
		/// </summary>
		public void Bootstrap()
		{
			this.Catalog.Catalogs.Add(new DirectoryCatalog($"{Assembly.GetExecutingAssembly().GetCodeBaseDirectory()}", AssemblyNamingConvention, this.Conventions));

			using (var container = new CompositionContainer(this.Catalog, CompositionOptions.DisableSilentRejection))
			{
				var configurators = container.GetExportedValues<IDependencyResolutionConfigurator>();

				foreach (var configurator in configurators)
				{
					configurator.Configure();
				}
			}
		}
	}
}
