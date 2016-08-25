using System;
using System.ComponentModel.Composition.Hosting;
using System.ComponentModel.Composition.Primitives;
using System.ComponentModel.Composition.Registration;
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
		public CompositionContainer Bootstrap(Action<RegistrationBuilder> registrationFinisher = null, params Assembly[] assemblies)
		{
			base.BootstrapAssemblies(assemblies);

			var registrations = new RegistrationBuilder();

			using (var bootstrapContainer = new CompositionContainer(this.Catalog, CompositionOptions.DisableSilentRejection))
			{
				var bootstrapExports = bootstrapContainer.GetExports<ILibraryDependencyRegistrar>();

				foreach (var bootstrapExport in bootstrapExports)
				{
					bootstrapExport.Value.Register(registrations);
				}

				//TODO: RLV - Reconsider whether requiring application registrar is best approach
				var appBootstrapperExports = bootstrapContainer.GetExports<IApplicationDependencyRegistrar>();
				var count = appBootstrapperExports.Count();
				if (count == 1)
				{
					var appBootstrapper = appBootstrapperExports.Single().Value;
					appBootstrapper.Register(registrations);
				}
				else
				{
					throw new ApplicationDependencyRegistrarImplementationException(count);
				}
			}

			// Allow caller to complete any last minute registrations
			if (registrationFinisher != null)
				registrationFinisher(registrations);

			this.Catalog.Catalogs.Add(new ApplicationCatalog(registrations));

			return new CompositionContainer(this.Catalog, CompositionOptions.DisableSilentRejection | CompositionOptions.IsThreadSafe);
		}
	}
}