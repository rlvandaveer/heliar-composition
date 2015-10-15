using System.ComponentModel.Composition.Hosting;
using System.ComponentModel.Composition;
using System.Reflection;

using Heliar.Composition.Core;

namespace Heliar.Composition.Web
{
	/// <summary>
	/// Class that bootstraps all of the dependency resolvers for the current application by discovering all of the <see cref="IDependencyResolverConfigurator"/>s
	/// and executing them.
	/// </summary>
	public class DependencyResolverBootstrapper : BootstrapperBehavior, IDependencyResolverBootstrapper
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="DependencyResolverBootstrapper"/> class.
		/// </summary>
		public DependencyResolverBootstrapper()
		{
			this.AssemblyNamingConvention = "Heliar*.dll";
			this.Conventions.ForTypesDerivedFrom<IDependencyResolverConfigurator>()
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
				var configurators = container.GetExportedValues<IDependencyResolverConfigurator>();

				foreach (var configurator in configurators)
				{
					configurator.Configure();
				}
			}
		}
	}
}
